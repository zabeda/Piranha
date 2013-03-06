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
		/// Gets/sets the host names.
		/// </summary>
		public string HostNames { get ; set ; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets the available namespaces.
		/// </summary>
		public SelectList Namespaces { get ; set ; }

		/// <summary>
		/// Gets/sets weather the site tree can be deleted.
		/// </summary>
		public bool CanDelete { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new site tree model.
		/// </summary>
		public SiteTreeEditModel() : this(Guid.Empty) {
			CanDelete = true ;
		}

		/// <summary>
		/// Creates a new site tree model for the given namespace.
		/// </summary>
		/// <param name="id">Namespace id</param>
		public SiteTreeEditModel(Guid namespaceId) {
			using (var db = new DataContext()) {
				var ns = db.Namespaces.OrderBy(n => n.Name).ToList() ;
				if (namespaceId != Guid.Empty)
					Namespaces = new SelectList(ns, "Id", "Name", namespaceId) ;
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
				var site = db.SiteTrees.Where(s => s.Id == id).Single() ;
				return new SiteTreeEditModel(site.NamespaceId) {
					Id = site.Id,
					InternalId = site.InternalId,
					Name = site.Name,
					NamespaceId = site.NamespaceId,
					HostNames = site.HostNames,
					Description = site.Description,
					CanDelete = db.PageDrafts.Where(s => s.SiteTreeId == id).Count() == 0
				} ;
			}
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <returns>Weather the entity was updated or not</returns>
		public bool Save() {
			using (var db = new DataContext()) {
				var site = db.SiteTrees.Where(s => s.Id == Id).SingleOrDefault() ;
				if (site == null) {
					site = new SiteTree() ;
					site.NamespaceId = NamespaceId ;
					db.SiteTrees.Add(site) ;
				}

				// If we've changed namespace, update all related permalinks.
				if (site.NamespaceId != NamespaceId)
					ChangeNamespace(db, Id, NamespaceId) ;

				site.NamespaceId = NamespaceId ;
				site.InternalId = InternalId ;
				site.Name = Name ;
				site.HostNames = HostNames ;
				site.Description = Description ;

				var ret = db.SaveChanges() > 0 ;

				Id = site.Id ;

				// Refresh host name configuration
				if (ret)
					WebPages.WebPiranha.RegisterDefaultHostNames() ;
				return ret ;
			}
		}

		/// <summary>
		/// Deletes the current comment.
		/// </summary>
		/// <returns>Weather the entity was removed or not</returns>
		public bool Delete() {
			using (var db = new DataContext()) {
				var site = db.SiteTrees.Where(s => s.Id == Id).Single() ;

				db.SiteTrees.Remove(site) ;

				var ret = db.SaveChanges() > 0 ;
				// Refresh host name configuration
				if (ret)
					WebPages.WebPiranha.RegisterDefaultHostNames() ;
				return ret ;
			}
		}

		private void ChangeNamespace(DataContext db, Guid siteid, Guid namespaceid) {
			// TODO
		}
	}
}