using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Areas.Manager.Models
{
	/// <summary>
	/// View model for the site tree list view.
	/// </summary>
	public class SiteTreeListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available sites.
		/// </summary>
		public IList<Entities.SiteTree> Sites { get ; set ; }

		/// <summary>
		/// Gets/sets the mappings if a site should be able to be deleted.
		/// </summary>
		public Dictionary<Guid, bool> CanDeleteSite { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SiteTreeListModel() {
			Sites = new List<Entities.SiteTree>() ;
			CanDeleteSite = new Dictionary<Guid,bool>() ;
		}

		/// <summary>
		/// Gets the list model for all available sites.
		/// </summary>
		/// <returns>The model</returns>
		public static SiteTreeListModel Get() {
			var m = new SiteTreeListModel() ;

			using (var db = new DataContext()) {
				m.Sites = db.SiteTrees.OrderBy(s => s.Name).ToList() ;

				foreach (var site in m.Sites) {
					m.CanDeleteSite.Add(site.Id,
						db.PageDrafts.Where(p => p.SiteTreeId == site.Id && p.ParentId != site.Id).Count() == 0) ;
				}
			}
			return m ;
		}
	}
}