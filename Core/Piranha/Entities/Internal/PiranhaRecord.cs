using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Base class for all active records in the Piranha framework.
	/// </summary>
	/// <typeparam name="T">The record type</typeparam>
	[Serializable]
	public abstract class PiranhaRecord<T> : GuidRecord<T>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		public abstract DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		public abstract DateTime Updated { get ; set ; }
		#endregion

		/// <summary>
		/// Saves the current record to the database.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		/// <returns>Wether the operation was successful</returns>
		public override bool Save(System.Data.IDbTransaction tx = null) {
			return Save(tx, true) ;
		}

		/// <summary>
		/// Saves the current record to the database.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		/// <param name="setdates">Whether to automatically set the dates</param>
		/// <returns>Wether the operation was successful</returns>
		protected bool Save(System.Data.IDbTransaction tx = null, bool setdates = true) {
			if (Database.Identity != Guid.Empty || App.Instance.UserProvider.IsAuthenticated) {
				if (IsNew) {
					if (setdates)
						Created = DateTime.Now ;
				}
				if (setdates)
					Updated = DateTime.Now ;
				return base.Save(tx) ;
			}
			throw new AccessViolationException("User must be logged in to save data.") ;
		}		
	}
}
