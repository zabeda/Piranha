using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Entities;

namespace Piranha.Areas.Manager.Models
{
	/// <summary>
	/// View model for the comment edit view.
	/// </summary>
	public class SiteTreeEditModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		public Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the namespace id.
		/// </summary>
		public Guid NamespaceId { get ; set ; }

		/// <summary>
		/// Gets/sets the internal id.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets the available namespaces.
		/// </summary>
		public SelectList Namespaces { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="id">Optional namespace id</param>
		public SiteTreeEditModel(Guid? namespaceId = null) {
			using (var db = new DataContext()) {
				var ns = db.Namespaces.OrderBy(n => n.Name).ToList() ;
				if (namespaceId.HasValue)
					Namespaces = new SelectList(ns, "Id", "Name", namespaceId.Value) ;
				Namespaces = new SelectList(ns, "Id", "Name") ;
			}
		}

		/// <summary>
		/// Gets the edit model for the comment with the given id.
		/// </summary>
		/// <param name="id">The comment id.</param>
		/// <returns></returns>
		public static SiteTreeEditModel GetById(Guid id) {
			using (var db = new DataContext()) {
				var m = db.SiteTrees.Where(s => s.Id == id).Select(s =>
					new SiteTreeEditModel(s.NamespaceId) {
						Id = s.Id,
						InternalId = s.InternalId,
						NamespaceId = s.NamespaceId,
						Description = s.Description
					}).Single() ;		
				return m ;
			}
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <returns>Weather the entity was updated or not</returns>
		public bool Save() {
			using (var db = new DataContext()) {
				return db.SaveChanges() > 0 ;
			}
		}

		/// <summary>
		/// Deletes the current comment.
		/// </summary>
		/// <returns>Weather the entity was removed or not</returns>
		public bool Delete() {
			using (var db = new DataContext()) {
				return false ;
			}
		}
	}
}