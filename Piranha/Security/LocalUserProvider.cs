using System;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

using Piranha.Models;

namespace Piranha.Security
{
	/// <summary>
	/// User provider that uses the internal membership system in Piranha CMS.
	/// </summary>
	internal sealed class LocalUserProvider : IUserProvider
	{
		#region Properties
		/// <summary>
		/// Gets if the current user is authenticated.
		/// </summary>
		public bool IsAuthenticated {
			get { return HttpContext.Current.User.Identity.IsAuthenticated ; }
		}

		/// <summary>
		/// Gets the user id of the current user.
		/// </summary>
		public Guid UserId {
			get { return new Guid(HttpContext.Current.User.Identity.Name) ; }
		}

		/// <summary>
		/// Get the current user.
		/// </summary>
		public MembershipUser User {
			get {
				if (IsAuthenticated) {
					var user = SysUser.GetSingle(UserId) ;
					return MapUser(user) ;
				}
				return null ;
			}
		}
		#endregion

		/// <summary>
		/// Authenticates the user.
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="password">The password</param>
		/// <returns>If the username and passwords matches</returns>
		public bool Authenticate(string username, string password) {
			using (var db = new DataContext()) {
				var user = db.Users
					.Where(u => u.Login == username && u.Password == Encrypt(password) && (!u.IsLocked || u.LockedUntil <= DateTime.Now))
					.SingleOrDefault() ;
				if (user != null)
					return true ;
			}
			return false ;
		}

		/// <summary>
		/// Creates a new user entity with the given credentials.
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="password">The password</param>
		/// <param name="email">The email address</param>
		/// <returns>The provider user key for the new user</returns>
		public object Create(string username, string password, string email) {
			using (var db = new DataContext()) {
				var id = Guid.NewGuid() ;

				var user = new Entities.User() {
					Id = id,
					Login = username,
					ExternalId = id.ToString(),
					Email = email,
					Password = Encrypt(password)
				} ;
				db.Users.Add(user) ;
				db.SaveChanges() ;

				return user.ExternalId ;
			}
		}

		/// <summary>
		/// Deletes the user with the given Piranha CMS user id.
		/// </summary>
		/// <param name="id">The Piranha CMS user id</param>
		/// <returns>If the user was deleted successfully</returns>
		public bool Delete(Guid id) {
			// Since the user information is stored in the same table as
			// the login information, let's just return true.
			return true ;
		}

		/// <summary>
		/// Deletes the user with the given membership provider key.
		/// </summary>
		/// <param name="id">The membership user id</param>
		/// <returns>If the user was deleted successfully</returns>
		public bool Delete(object id) {
			return Delete((Guid)id) ;
		}

		/// <summary>
		/// Gets the user with the given Piranha CMS user id.
		/// </summary>
		/// <param name="id">The Piranha CMS user id</param>
		/// <returns>The user</returns>
		public MembershipUser GetUser(Guid id) {
			var user = SysUser.GetSingle(id) ;

			if (user != null)
				return MapUser(user) ;
			return null ;
		}

		#region Private methods
		/// <summary>
		/// Maps a Piranha CMS user to a membership user.
		/// </summary>
		/// <param name="user">The user</param>
		/// <returns>The membership user</returns>
		private MembershipUser MapUser(SysUser user) {
			return new MembershipUser("Piranha.Security.InternalUserProvider", user.Login, user.Id, user.Email, "", "", true, 
				user.IsLocked, user.Created, user.LastLogin, user.LastLogin, user.Created, user.Created) ;
		}

		/// <summary>
		/// Encrypts the given string with SHA256.
		/// </summary>
		/// <param name="str">The string</param>
		/// <returns>The encrypted</returns>
		private string Encrypt(string str) {
			UTF8Encoding encoder = new UTF8Encoding() ;
			SHA256CryptoServiceProvider crypto = new SHA256CryptoServiceProvider() ;

			byte[] bytes = crypto.ComputeHash(encoder.GetBytes(str)) ;
			return Convert.ToBase64String(bytes) ;
		}
		#endregion
	}
}