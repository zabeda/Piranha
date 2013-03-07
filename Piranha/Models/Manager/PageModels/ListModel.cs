using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Data;

namespace Piranha.Models.Manager.PageModels
{
	/// <summary>
	/// Page list model for the manager area.
	/// </summary>
	public class ListModel 
	{
		#region Properties
		/// <summary>
		/// Gets/sets the pages.
		/// </summary>
		public List<Sitemap> Pages { get ; set ; }

		/// <summary>
		/// Gets/sets the sitemap.
		/// </summary>
		public List<Sitemap> SiteMap { get ; set ; }

		/// <summary>
		/// Gets/sets the page templates.
		/// </summary>
		public List<PageTemplate> Templates { get ; set ; }

		/// <summary>
		/// Gets/sets the currently available site trees.
		/// </summary>
		public List<Entities.SiteTree> SiteTrees { get ; set ; }

		/// <summary>
		/// Gets/sets the internal id of the active site.
		/// </summary>
		public string ActiveSite { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the active site.
		/// </summary>
		public Guid ActiveSiteId { get ; set ; }

		/// <summary>
		/// Gets/sets the list of all pages to create copies from.
		/// </summary>
		public List<Sitemap> AllPages { get ; set ; }

		/// <summary>
		/// Gets/sets weather the site with the given id has a site page.
		/// </summary>
		public Dictionary<Guid, Guid> SitePage { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor, creates a new model.
		/// </summary>
		public ListModel() {
			Pages = new List<Sitemap>() ;
			Templates = PageTemplate.GetFields("pagetemplate_id, pagetemplate_name, pagetemplate_preview, pagetemplate_description",
				"pagetemplate_site_template = 0", new Params() { OrderBy = "pagetemplate_name ASC" }) ;
			AllPages = Sitemap.GetFields("page_id, page_title, page_navigation_title, pagetemplate_name, sitetree_name", "page_draft = 1 AND page_original_id IS NULL AND page_parent_id NOT IN (SELECT sitetree_id FROM sitetree)", 
				new Params() { OrderBy = "sitetree_name, COALESCE(page_navigation_title, page_title)" }) ;
			SitePage = new Dictionary<Guid, Guid>() ;

			using (var db = new DataContext()) {
				SiteTrees = db.SiteTrees.OrderBy(s => s.Name).ToList() ;

				foreach (var site in SiteTrees)
					SitePage[site.Id] = db.Pages.Where(p => p.SiteTreeId == site.Id && p.ParentId == site.Id).Select(p => p.Id).SingleOrDefault() ;
			}
		}

		/// <summary>
		/// Gets the list model for all available pages.
		/// </summary>
		/// <param name="internalId">Optional internal id of the site tree</param>
		/// <returns>The model.</returns>
		public static ListModel Get(string internalId = "") {
			if (String.IsNullOrEmpty(internalId))
				internalId = Config.SiteTree ;

			ListModel m = new ListModel() ;
			m.SiteMap = Sitemap.GetStructure(internalId, false) ;
			m.Pages = Sitemap.GetStructure(internalId, false).Flatten() ;
			m.ActiveSite = internalId.ToUpper() ;

			using (var db = new DataContext()) {
				m.ActiveSiteId = db.SiteTrees.Where(s => s.InternalId == internalId).Select(s => s.Id).Single() ;
			}
			return m ;
		}
	}
}
