using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

using AutoMapper;
using Piranha.Entities;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// Edit model for the permission view.
	/// </summary>
	public class PermissionEditModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the current permission.
		/// </summary>
		public Entities.Permission Permission { get ; set ; }

		/// <summary>
		/// Gets/sets the available groups.
		/// </summary>
		public IList<SelectListItem> Groups { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PermissionEditModel() {
			using (var db = new DataContext()) {
				Groups = Mapper.Map<List<SelectListItem>>(db.Groups.OrderBy(g => g.Name).ToList()) ;
			}
		}

		/// <summary>
		/// Gets the edit model for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The model</returns>
		public static PermissionEditModel GetById(Guid id) {
			var m = new PermissionEditModel() ;

			using (var db = new DataContext()) {
				m.Permission = db.Permissions.Where(p => p.Id == id).Single() ;
			}
			return m ;
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <returns>If the entity was updated in the database.</returns>
		public bool Save() {
			using (var db = new DataContext()) {
				var permission = db.Permissions.Where(u => u.Id == Permission.Id).SingleOrDefault() ;
				if (permission == null) {
					permission = new Entities.Permission() ;
					permission.Attach(db, EntityState.Added) ;
				}
				Mapper.Map<Entities.Permission, Entities.Permission>(Permission, permission) ;

				return db.SaveChanges() > 0 ;
			}
		}
	}
}