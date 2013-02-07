using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using Piranha.Entities;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the page list.
	/// </summary>
	public class PageListModel
	{
		#region Inner classes
		/// <summary>
		/// The different statuses a page can have.
		/// </summary>
		public enum PageStatus {
			UNPUBLISHED, DRAFT, PUBLISHED
		}

		/// <summary>
		/// The page structure model.
		/// </summary>
		public class PageStructure
		{
			#region Properties
			/// <summary>
			/// Gets/sets the id .
			/// </summary>
			public Guid Id { get ; set ; }

			/// <summary>
			/// Gets/sets the optional parent id.
			/// </summary>
			public Guid? ParentId { get ; set ; }

			/// <summary>
			/// Gets/sets the sequence number.
			/// </summary>
			public int Seqno { get ; set ; }

			/// <summary>
			/// Gets/sets the title.
			/// </summary>
			public string Title { get ; set ; }

			/// <summary>
			/// Gets/sets the navigation title.
			/// </summary>
			public string NavigationTitle { get ; set ; }

			/// <summary>
			/// Gets/sets the title to display.
			/// </summary>
			public string DisplayTitle { get { return !String.IsNullOrEmpty(NavigationTitle) ? NavigationTitle : Title ; } }

			/// <summary>
			/// Gets/sets the template name.
			/// </summary>
			public string TemplateName { get ; set ; }

			/// <summary>
			/// Gets/sets the site tree name.
			/// </summary>
			public string SiteTreeName { get ; set ; }

			/// <summary>
			/// Gets/sets the current level.
			/// </summary>
			public int Level { get ; set ; }

			/// <summary>
			/// Gets/sets the created date.
			/// </summary>
			public DateTime Created { get ; set ; }

			/// <summary>
			/// Gets/sets the updated date.
			/// </summary>
			public DateTime Updated { get ; set ; }

			/// <summary>
			/// Gets/sets the status of the page.
			/// </summary>
			public PageStatus Status { get ; set ; }

			/// <summary>
			/// Gets/sets the child pages.
			/// </summary>
			public IList<PageStructure> Pages { get ; set ; }
			#endregion

			/// <summary>
			/// Default constructor
			/// </summary>
			public PageStructure() {
				Pages = new List<PageStructure>() ;
			}

			/// <summary>
			/// Checks if the current page has a child with the given id.
			/// </summary>
			/// <param name="page">The page</param>
			/// <param name="id">The id to search for</param>
			/// <returns>Weather the id was found</returns>
			public bool HasChild(Guid? id) {
				if (id.HasValue) {
					foreach (var page in Pages)
						if (HasChild(page, id.Value))
							return true ;
				}
				return false ;
			}

			#region Private methods
			/// <summary>
			/// Checks if the given page has a child with the given id.
			/// </summary>
			/// <param name="page">The page</param>
			/// <param name="id">The id to search for</param>
			/// <returns>Weather the id was found</returns>
			private bool HasChild(PageStructure page, Guid id) {
				if (page.Id == id)
					return true ;
				foreach (var child in page.Pages) {
					if (HasChild(child, id))
						return true ;
				}
				return false ;
			}
			#endregion
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the available pages in a flat structure.
		/// </summary>
		public IList<PageStructure> Pages { get ; set ; }

		/// <summary>
		/// Gets/sets the recursively sorted page structure.
		/// </summary>
		public IList<PageStructure> PageMap { get ; set ; }

		/// <summary>
		/// Gets/sets the available page templates.
		/// </summary>
		public IList<PageTemplate> Templates { get ; set ; }

		/// <summary>
		/// Gets/sets the available sites
		/// </summary>
		public IList<SiteTree> Sites { get ; set ; }

		/// <summary>
		/// Gets/sets the currently active site.
		/// </summary>
		public string ActiveSite { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageListModel() {
			Pages = new List<PageStructure>() ;
			Templates = new List<PageTemplate>() ;
			Sites = new List<SiteTree>() ;
		}

		/// <summary>
		/// Gets the page list model for the site with the given internal id. If no
		/// internal id is provided the default site is used.
		/// </summary>
		/// <param name="internalId">The internal id of the site</param>
		/// <returns>The model</returns>
		public static PageListModel GetByInternalId(string internalId = "") {
			var m = new PageListModel() ;

			if (String.IsNullOrEmpty(internalId))
				internalId = Config.SiteTree ;

			using (var db = new DataContext()) {
				m.ActiveSite = internalId ;
				m.Templates = db.PageTemplates.OrderBy(t => t.Name).ToList() ;
				m.Sites = db.SiteTrees.OrderBy(s => s.Name).ToList() ;
				m.Pages = db.PageDrafts
					.Include(p => p.Template)
					.Include(p => p.SiteTree)
					.Where(p => p.SiteTree.InternalId == internalId).OrderBy(p => p.ParentId).ThenBy(p => p.Seqno).ToList()
					.Select(p => new PageStructure() {
						Id = p.Id,
						Title = p.Title,
						ParentId = p.ParentId,
						Seqno = p.Seqno,
						NavigationTitle = p.NavigationTitle,
						TemplateName = p.Template.Name,
						SiteTreeName = p.SiteTree.Name,
						Status = (!p.Published.HasValue ? PageStatus.UNPUBLISHED : (p.Updated > p.LastPublished.Value ? PageStatus.DRAFT : PageStatus.PUBLISHED)),
						Created = p.Created,
						Updated = p.Updated
					}).ToList() ;
				m.PageMap = Sort(m.Pages) ;
			}
			return m ;
		}

		#region Private method
		/// <summary>
		/// Sorts the page structure recursive.
		/// </summary>
		/// <param name="pages">The pages</param>
		/// <param name="parentid">The parent id</param>
		/// <returns>The sorted pages</returns>
		private static IList<PageStructure> Sort(IList<PageStructure> pages, Guid? parentid = null, int level = 1) {
			var ret = new List<PageStructure>() ;

			foreach (var page in pages) {
				if (page.ParentId == parentid) {
					page.Level = level ;
					page.Pages = Sort(pages, page.Id, level + 1) ;
					ret.Add(page) ;
				}
			}
			return ret ;
		}
		#endregion
	}
}