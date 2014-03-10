using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Piranha.Data
{
	/// <summary>
	/// Helper class used to create an open and ready to use database connection using the DbProviderFactory
	/// and the connectionString section in the application settings.
	/// </summary>
	public class Database
	{
		#region Members
		/// <summary>
		/// Private static member to cache the current provider factory.
		/// </summary>
		private static DbProviderFactory _factory = null ;

		/// <summary>
		/// Gets the current database version.
		/// </summary>
		public static int CurrentVersion = 33 ;

		/// <summary>
		/// Gets the currently logged in users identity.
		/// </summary>
		internal static Guid Identity ;

		/// <summary>
		/// Gets/sets the current provider name.
		/// </summary>
		public static string ProviderName { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets the version of the currently installed database.
		/// </summary>
		public static int InstalledVersion {
			get {
				Models.SysParam p = Models.SysParam.GetByName("SITE_VERSION") ;
				int version ;
				Int32.TryParse(p.Value, out version) ;
				return version ;
			}
		}

		/// <summary>
		/// Gets whether the current provider is SqlServer.
		/// </summary>
		public static bool IsSqlServer {
			get { return ProviderName.ToLower() == "system.data.sqlclient" ; }
		}

		/// <summary>
		/// Gets whether the current provider is MySql.
		/// </summary>
		public static bool IsMySql {
			get { return ProviderName.ToLower() == "mysql.data.mysqlclient" ; }
		}

		/// <summary>
		/// Gets the root namespace for all scripts for the current provider.
		/// </summary>
		public static string ScriptRoot {
			get {
				if (IsMySql)
					return "Piranha.Data.Scripts.MySql" ;
				return "Piranha.Data.Scripts" ;
			}
		}
		#endregion

		/// <summary>
		/// Delegate for the installed event.
		/// </summary>
		internal delegate void OnInstalledDelegate();

		/// <summary>
		/// Called after database has been installed.
		/// </summary>
		internal static OnInstalledDelegate OnInstalled;

		/// <summary>
		/// Gets if the database is installed and up to date.
		/// </summary>
		/// <returns>If the databse is installed</returns>
		public static bool IsInstalled {
			get {
				try {
					return Convert.ToInt32(Models.SysParam.GetByName("SITE_VERSION").Value) == Database.CurrentVersion;
				} catch { }
				return false;
			}
		}

		/// <summary>
		/// Logs in the given user when no HttpContext is available.
		/// </summary>
		/// <param name="login">Username</param>
		/// <param name="password">Password</param>
		/// <returns>If the login was successful</returns>
		public static bool Login(string login, string password) {
			var usr = Models.SysUser.Authenticate(login, password) ;

			if (usr != null) {
				Identity = usr.Id ;
				return true ;
			}
			return false ;
		}

		/// <summary>
		/// Logs in the default sys user.
		/// </summary>
		public static void LoginSys() {
			Identity = Config.SysUserId ;
		}

		/// <summary>
		/// Logs out the current user.
		/// </summary>
		public static void Logout() {
			Identity = Guid.Empty ;
		}

		/// <summary>
		/// Gets the database connection from the current config.
		/// </summary>
		/// <param name="name">Optional name of the connection string to use</param>
		/// <returns>An open connection</returns>
		public static IDbConnection OpenConnection(string name = "piranha") {
			if (_factory == null)
				_factory = GetFactory(name) ;
			IDbConnection conn = GetConnection(name) ;
			conn.Open() ;
			return conn ;
		}

		/// <summary>
		/// Opens a connection and creates a transaction on it.
		/// </summary>
		/// <param name="name">Optional name of the connection string to use</param>
		/// <returns>An open transaction</returns>
		public static IDbTransaction OpenTransaction(string name = "piranha") {
			return OpenConnection().BeginTransaction() ;
		}

		/// <summary>
		/// Creates a command on the given connection and sql statement and fills it
		/// with the given parameters.
		/// </summary>
		/// <param name="conn">The connection</param>
		/// <param name="sql">The sql statement</param>
		/// <param name="args">The parameters</param>
		/// <returns>A db command</returns>
		public static IDbCommand CreateCommand(IDbConnection conn, IDbTransaction tx, string sql, object[] args = null) {
			// Convert all enum arguments to string
			for (int n = 0; n < args.Length; n++)
				if (args[n] != null && typeof(Enum).IsAssignableFrom(args[n].GetType())) 
					args[n] = args[n].ToString() ;

			// Create command
			IDbCommand cmd = conn.CreateCommand() ;
			if (tx != null)
				cmd.Transaction = tx ;
			cmd.CommandText = sql ;
			if (args != null) {
				int pos = args.Length > 0 && args[0] is IDbTransaction ? 1 : 0 ;
				for (int n = 0 + pos; n < args.Length; n++) {
					if (!(args[n] is Params)) {
						IDbDataParameter p = cmd.CreateParameter() ;
						p.ParameterName = String.Format("@{0}", n) ; 
						if (args[n] == null || (args[n] is DateTime && ((DateTime)args[n]) == DateTime.MinValue)) {
							p.Value = DBNull.Value ;
						} else if (args[n] is Guid) {
							if (((Guid)args[n]) == Guid.Empty) {
								p.Value = DBNull.Value ;
							} else {
								p.Value = ((Guid)args[n]).ToString() ;
								p.DbType = DbType.String ;
							}
						} else {
							p.Value = args[n] ;
						}
						cmd.Parameters.Add(p) ;
					}
				}
			}
			return cmd ;
		}

		/// <summary>
		/// Installs of updates the database. If the database is installed and
		/// in sync no actions are performed.
		/// </summary>
		/// <param name="username">The admin username</param>
		/// <param name="password">The admin password</param>
		/// <param name="email">The admin email</param>
		public static void InstallOrUpdate(string username, string password, string email) {
			if (!IsInstalled) {
				int? version = null;

				try {
					version = Convert.ToInt32(Models.SysParam.GetByName("SITE_VERSION").Value);
				} catch { }

				if (!version.HasValue) {
					Install(username, password, email);
				} else {
					Update();
				}
			}
		}

		/// <summary>
		/// Installs and seeds the database.
		/// </summary>
		/// <param name="username">The admin username</param>
		/// <param name="password">The admin password</param>
		/// <param name="email">The admin email</param>
		public static void Install(string username, string password, string email) { 
			// Read embedded create script
			Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream(Database.ScriptRoot + ".Create.sql") ;
			String sql = new StreamReader(str).ReadToEnd() ;
			str.Close() ;

			// Read embedded data script
			str = Assembly.GetExecutingAssembly().GetManifestResourceStream(Database.ScriptRoot + ".Data.sql") ;
			String data = new StreamReader(str).ReadToEnd() ;
			str.Close() ;

			// Split statements and execute
			string[] stmts = sql.Split(new char[] { ';' }) ;
			using (IDbTransaction tx = Database.OpenTransaction()) {
				// Create database from script
				foreach (string stmt in stmts) {
					if (!String.IsNullOrEmpty(stmt.Trim()))
						Models.SysUser.Execute(stmt, tx) ;
				}
				tx.Commit() ;
			}

			// Split statements and execute
			stmts = data.Split(new char[] { ';' }) ;
			using (IDbTransaction tx = Database.OpenTransaction()) {
				// Create user
				Models.SysUser usr = new Models.SysUser() {
					Login = username,
					Email = email,
					GroupId = new Guid("7c536b66-d292-4369-8f37-948b32229b83"),
					Created = DateTime.Now,
					Updated = DateTime.Now
				} ;
				usr.Save(tx) ;

				// Create user password
				Models.SysUserPassword pwd = new Models.SysUserPassword() {
					Id = usr.Id,
					Password = password,
					IsNew = false
				} ;
				pwd.Save(tx) ;

				// Create default data
				foreach (string stmt in stmts) {
					if (!String.IsNullOrEmpty(stmt.Trim()))
						Models.SysUser.Execute(stmt, tx) ;
				}		
				tx.Commit() ;

				// Fire installed event
				if (OnInstalled != null)
					OnInstalled();
			}
		}

		/// <summary>
		/// Updates the database to the current version.
		/// </summary>
		public static void Update() { 
			// Execute all incremental updates in a transaction.
			using (IDbTransaction tx = Database.OpenTransaction()) {
				for (int n = Data.Database.InstalledVersion + 1; n <= Data.Database.CurrentVersion; n++) {
					// Read embedded create script
					Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream(Database.ScriptRoot + ".Updates." +
						n.ToString() + ".sql") ;
					String sql = new StreamReader(str).ReadToEnd() ;
					str.Close() ;

					// Split statements and execute
					string[] stmts = sql.Split(new char[] { ';' }) ;
					foreach (string stmt in stmts) {
						if (!String.IsNullOrEmpty(stmt.Trim()))
							Models.SysUser.Execute(stmt.Trim(), tx) ;
					}

					// Check for update class
					var utype = Type.GetType("Piranha.Data.Updates.Update" + n.ToString()) ;
					if (utype != null) {
						Data.Updates.IUpdate update = (Data.Updates.IUpdate)Activator.CreateInstance(utype) ;
						update.Execute(tx) ;
					}
				}
				// Now lets update the database version.
				Models.SysUser.Execute("UPDATE sysparam SET sysparam_value = @0 WHERE sysparam_name = 'SITE_VERSION'", 
					tx, Data.Database.CurrentVersion) ;
				Models.SysParam.InvalidateParam("SITE_VERSION") ;

				tx.Commit() ;

				// Fire installed event
				if (OnInstalled != null)
					OnInstalled();
			}
		}

		#region Private methods
		/// <summary>
		/// Gets the current provider factory specified in the connection string section.
		/// </summary>
		/// <param name="name">The connection string name</param>
		/// <returns>A provider factory</returns>
		private static DbProviderFactory GetFactory(string name) {
			if (ConfigurationManager.ConnectionStrings[name] == null)
				throw new ConfigurationErrorsException("No connection string found with name \"" + name + "\"") ;
			// Store the provider name.
			ProviderName = ConfigurationManager.ConnectionStrings[name].ProviderName ;
			// Create the factory.
			return DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings[name].ProviderName) ;
		}

		/// <summary>
		/// Gets a connection from the current provider factory.
		/// </summary>
		/// <param name="name">The name of the current connection string</param>
		/// <returns>A database connection</returns>
		private static IDbConnection GetConnection(string name) {
			IDbConnection conn = _factory.CreateConnection() ;
			conn.ConnectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString ;
			return conn ;
		}
		#endregion
	}
}
