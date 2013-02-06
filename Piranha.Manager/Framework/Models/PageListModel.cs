using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using Piranha.Entities;

namespace Piranha.Manager.Models
{
	public class PageListModel
	{
		#region Inner classes
		public enum PageStatus {
			UNPUBLISHED, DRAFT, PUBLISHED
		}

		public class PageStructure {
			public Guid Id { get ; set ; }
			public Guid? ParentId { get ; set ; }
			public int Seqno { get ; set ; }
			public string Title { get ; set ; }
			public string NavigationTitle { get ; set ; }
			public string DisplayTitle { get { return !String.IsNullOrEmpty(NavigationTitle) ? NavigationTitle : Title ; } }
			public string TemplateName { get ; set ; }
			public string SiteTreeName { get ; set ; }
			public int Level { get ; set ; }
			public DateTime Created { get ; set ; }
			public DateTime Updated { get ; set ; }
			public PageStatus Status { get ; set ; }
			public IList<PageStructure> Pages { get ; set ; }

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

		}
		#endregion

		#region Properties
		public IList<PageStructure> Pages { get ; set ; }
		public IList<PageTemplate> Templates { get ; set ; }
		public IList<SiteTree> Sites { get ; set ; }
		public string ActiveSite { get ; set ; }
		#endregion

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
				m.Pages = Sort(db.PageDrafts
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
					}).ToList()) ;
			}
			return m ;
		}

		/// <summary>
		/// Sorts the page structure recursive.
		/// </summary>
		/// <param name="pages">The pages</param>
		/// <param name="parentid">The parent id</param>
		/// <returns>The sorted pages</returns>
		private static List<PageStructure> Sort(List<PageStructure> pages, Guid? parentid = null, int level = 1) {
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
	}
}