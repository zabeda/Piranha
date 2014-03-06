using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Active record for a log entry.
	/// </summary>
	[PrimaryKey(Column="syslog_id")]
	[Serializable]
	public class SysLog : PiranhaRecord<SysLog>
	{
		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="syslog_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the id the log is related to.
		/// </summary>
		[Column(Name="syslog_parent_id")]
		public Guid ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the type of entity the log is related to.
		/// </summary>
		[Column(Name="syslog_parent_type")]
		public string ParentType { get ; set ; }

		/// <summary>
		/// Gets/sets the action the log is related to (INSERT/UPDATE/DELETE).
		/// </summary>
		[Column(Name="syslog_action")]
		public string Action { get ; set ; }

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="syslog_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="syslog_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the optional id of logged in user.
		/// </summary>
		[Column(Name="syslog_created_by")]
		public string CreatedBy { get ; set ; }	
		#endregion

		#region Events
		/// <summary>
		/// Saves the syslog.
		/// </summary>
		/// <param name="tx">The optional transaction</param>
		/// <returns>If the operation was successfull</returns>
		public override bool Save(System.Data.IDbTransaction tx = null) {
			if (App.Instance.UserProvider.IsAuthenticated) {
				CreatedBy = App.Instance.UserProvider.UserId.ToString();
			}
			return base.Save(tx);
		}
		#endregion
	}
}
