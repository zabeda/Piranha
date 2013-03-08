using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Entities;
using Piranha.Extend;

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
		/// Gets/sets the optional meta title.
		/// </summary>
		public string MetaTitle { get ; set ; }

		/// <summary>
		/// Gets/sets the optional meta description.
		/// </summary>
		public string MetaDescription { get ; set ; }

		/// <summary>
		/// Gets/sets the site template.
		/// </summary>
		public PageTemplate Template { get ; set ; } 

		/// <summary>
		/// Gets/sets the regions of the site template.
		/// </summary>
		public IList<RegionTemplate> Regions { get ; set ; }

		/// <summary>
		/// Gets/sets the available namespaces.
		/// </summary>
		public SelectList Namespaces { get ; set ; }

		/// <summary>
		/// Gets/sets the available region types.
		/// </summary>
		public List<dynamic> RegionTypes { get ; set ; }

		/// <summary>
		/// Gets/sets weather the site tree can be deleted.
		/// </summary>
		public bool CanDelete { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new site tree model.
		/// </summary>
		public SiteTreeEditModel() : this(Config.DefaultNamespaceId) {
			CanDelete = true ;
		}

		/// <summary>
		/// Creates a new site tree model for the given namespace.
		/// </summary>
		/// <param name="id">Namespace id</param>
		public SiteTreeEditModel(Guid namespaceId) {
			// Get the namespaces
			using (var db = new DataContext()) {
				var ns = db.Namespaces.OrderBy(n => n.Name).ToList() ;
				if (namespaceId != Guid.Empty)
					Namespaces = new SelectList(ns, "Id", "Name", namespaceId) ;
				Namespaces = new SelectList(ns, "Id", "Name") ;
			}

			// Get the available region types
			RegionTypes = new List<dynamic>() ;
			ExtensionManager.Extensions.Where(e => e.ExtensionType == ExtensionType.Region).OrderBy(e => e.Name).Each((i, r) =>
				RegionTypes.Add(new { Name = r.Name, Type = r.Type.ToString() })) ;
			RegionTypes.Insert(0, new { Name = "", Type = "" }) ;

			// Initialize the new site
			Id = Guid.NewGuid() ;
			NamespaceId = namespaceId ;
			Template = new PageTemplate() {
				Id = Id,
				Name = Id.ToString(),
				IsSiteTemplate = true
			} ;
			Regions = Template.RegionTemplates ;
		}

		/// <summary>
		/// Gets the edit model for the comment with the given id.
		/// </summary>
		/// <param name="id">The comment id.</param>
		/// <returns></returns>
		public static SiteTreeEditModel GetById(Guid id) {
			using (var db = new DataContext()) {
				var site = db.SiteTrees.Where(s => s.Id == id).Single() ;
				var model = new SiteTreeEditModel(site.NamespaceId) {
					Id = site.Id,
					InternalId = site.InternalId,
					Name = site.Name,
					NamespaceId = site.NamespaceId,
					HostNames = site.HostNames,
					Description = site.Description,
					MetaTitle = site.MetaTitle,
					MetaDescription = site.MetaDescription,
					CanDelete = db.PageDrafts.Where(s => s.SiteTreeId == id && s.ParentId != id).Count() == 0,
					Template = db.PageTemplates.Include(pt => pt.RegionTemplates).Where(pt => pt.Id == site.Id && pt.IsSiteTemplate).SingleOrDefault()
				} ;
				if (model.Template == null) {
					model.Template = new PageTemplate() {
						Id = site.Id,
						Name = site.Id.ToString(),
						IsSiteTemplate = true
					} ;
				} else {
					model.Template.RegionTemplates = model.Template.RegionTemplates.OrderBy(r => r.Seqno).ToList() ;
				}
				model.Regions = model.Template.RegionTemplates ;
				return model ;
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
					site.Id = Id ;
					site.NamespaceId = NamespaceId ;
					db.SiteTrees.Add(site) ;
				}

				// If we've changed namespace, update all related permalinks.
				if (site.NamespaceId != NamespaceId)
					ChangeNamespace(db, Id, NamespaceId) ;

				// Update the site tree
				site.NamespaceId = NamespaceId ;
				site.InternalId = InternalId = !String.IsNullOrEmpty(InternalId) ? InternalId.Replace(" ", "") : Name.Replace(" ", "") ;
				site.Name = Name ;
				site.HostNames = HostNames ;
				site.Description = Description ;

				// Update the site template
				var template = db.PageTemplates.Include(pt => pt.RegionTemplates).Where(pt => pt.Id == Id && pt.IsSiteTemplate).SingleOrDefault() ;
				if (template == null) {
					template = new PageTemplate() ;
					db.PageTemplates.Add(template) ;
					
					template.Id = Id ;
					template.Name = Id.ToString() ;
					template.IsSiteTemplate = true ;
				}
				template.Preview = Template.Preview ;
				template.Properties = Template.Properties ;

				// Update the regions
				var currentRegions = new List<Guid>() ;
				foreach (var reg in Regions) {
					var region = template.RegionTemplates.Where(r => r.Id == reg.Id).SingleOrDefault() ;
					if (region == null) {
						region = new RegionTemplate() ;
						db.RegionTemplates.Add(region) ;
						template.RegionTemplates.Add(region) ;

						region.Id = Guid.NewGuid() ;
						region.TemplateId = template.Id ;
						region.Type = reg.Type ;
					}
					region.Name = reg.Name ;
					region.InternalId = reg.InternalId ;
					region.Seqno = reg.Seqno ;
					region.Description = reg.Description ;
					currentRegions.Add(region.Id) ;
				}
				// Delete removed regions
				foreach (var reg in template.RegionTemplates.Where(r => !currentRegions.Contains(r.Id)).ToList()) {
					db.RegionTemplates.Remove(reg) ;
				}

				// Check that we have a site page, if not, create it
				var page = db.Pages.Where(p => p.SiteTreeId == site.Id && p.TemplateId == site.Id).SingleOrDefault() ;
				if (page == null) {
					// Create page
					page = new Page() {
						Id = Guid.NewGuid(),
						SiteTreeId = site.Id,
						TemplateId = site.Id,
						ParentId = site.Id,
						Title = site.Id.ToString(),
						PermalinkId = Guid.NewGuid()
					} ;

					// Create published version
					var published = page.Clone() ;
					published.IsDraft = false ;

					// Create permalink
					var permalink = new Permalink() {
						Id = page.PermalinkId,
						NamespaceId = site.NamespaceId,
						Name = site.Id.ToString(),
						Type = "SITE"
					} ;

					// Attach to context
					page.Attach(db, EntityState.Added) ;
					published.Attach(db, EntityState.Added) ;
					permalink.Attach(db, EntityState.Added) ;
				}
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
				var template = db.PageTemplates.Where(p => p.Id == Id).SingleOrDefault() ;

				db.SiteTrees.Remove(site) ;
				if (template != null)
					db.PageTemplates.Remove(template) ;
				db.Database.ExecuteSqlCommand("DELETE FROM page WHERE page_sitetree_id={0} AND page_parent_id={0}", Id) ;
				db.Database.ExecuteSqlCommand("DELETE FROM permalink WHERE permalink_type='SITE' AND permalink_id NOT IN (SELECT page_permalink_id FROM page)") ;

				var ret = db.SaveChanges() > 0 ;
				// Refresh host name configuration
				if (ret)
					WebPages.WebPiranha.RegisterDefaultHostNames() ;
				return ret ;
			}
		}

		private void ChangeNamespace(DataContext db, Guid siteid, Guid namespaceid) {
			db.Database.ExecuteSqlCommand(
				"UPDATE permalink " +
				"SET permalink_namespace_id = {0} " +
				"WHERE permalink_id IN (" +
				"  SELECT permalink_id " +
				"  FROM permalink JOIN page ON page_permalink_id = permalink_id " +
				"  WHERE page_sitetree_id = {1})", namespaceid, siteid) ;
		}
	}
}