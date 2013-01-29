using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using Piranha.Entities;
using Piranha.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the permission list.
	/// </summary>
	public class PermissionListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available permissions.
		/// </summary>
		[ModelProperty(OnLoad="LoadPermissions")]
		public IList<Entities.Permission> Permissions { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PermissionListModel() {
			Permissions = new List<Entities.Permission>() ;
		}

		/// <summary>
		/// Loads the permission list.
		/// </summary>
		public void LoadPermissions() {
			using (var db = new DataContext()) {
				Permissions = db.Permissions.Include(p => p.Group).OrderBy(p => p.Name).ToList() ;
			}
		}
	}
}