using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using Piranha.Data;
using Piranha.Rest.DataContracts;

namespace Piranha.Rest
{
	/// <summary>
	/// This is the mail ReST API.
	/// </summary>
	[ServiceContract()]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class RestService
	{
		/// <summary>
		/// Gets the available contracts.
		/// </summary>
		/// <returns>The categories</returns>
		[OperationContract()]
		[WebGet(UriTemplate="categories", ResponseFormat=WebMessageFormat.Json)]
		public List<Category> GetCategories() {
			List<Category> categories = new List<Category>() ;

			Models.Category.Get(new Params() { OrderBy = "category_name" }).ForEach(c => 
				categories.Add(new Category() {
					Id = c.Id,
					Name = c.Name,
					Permalink = c.Permalink,
					Description = c.Description,
					Created = c.Created.ToShortDateString(),
					Updated = c.Updated.ToShortDateString()
				}));
			return categories ;
		}

		/// <summary>
		/// Gets the available contracts as xml.
		/// </summary>
		/// <returns>The categories</returns>
		[OperationContract()]
		[WebGet(UriTemplate="categories/xml", ResponseFormat=WebMessageFormat.Xml)]
		public List<Category> GetCategoriesXml() {
			return GetCategories() ;
		}

		/// <summary>
		/// Gets the sitemap.
		/// </summary>
		/// <returns>The sitemap</returns>
		[OperationContract()]
		[WebGet(UriTemplate="sitemap", ResponseFormat=WebMessageFormat.Json)]
		public List<Sitemap> GetSitemap() {
			List<Models.Sitemap> sm = Models.Sitemap.GetStructure(true) ;
			return BuildMap(sm) ;
		}

		/// <summary>
		/// Gets the sitemap as xml.
		/// </summary>
		/// <returns>The sitemap</returns>
		[OperationContract()]
		[WebGet(UriTemplate="sitemap/xml", ResponseFormat=WebMessageFormat.Xml)]
		public List<Sitemap> GetSitemapXml() {
			return GetSitemap() ;
		}

		/// <summary>
		/// Gets the page specified by the given id.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The page</returns>
		[OperationContract()]
		[WebGet(UriTemplate="page/{id}", ResponseFormat=WebMessageFormat.Json)]
		public Page GetPage(string id) {
			try {
				Models.PageModel pm = Models.PageModel.GetById(new Guid(id)) ;

				if (pm != null && (pm.Page.GroupId == Guid.Empty || HttpContext.Current.User.IsMember(pm.Page.GroupId))) {
					// Page data
					Page page = new Page() {
						Id = pm.Page.Id,
						Title = pm.Page.Title,
						Permalink = pm.Page.Permalink,
						IsHidden = ((Piranha.Models.Page)pm.Page).IsHidden,
						Attachments = ((Models.Page)pm.Page).Attachments,
						Created = pm.Page.Created.ToShortDateString(),
						Updated = pm.Page.Updated.ToShortDateString(),
						Published = pm.Page.Published.ToShortDateString(),
						LastPublished = pm.Page.LastPublished.ToShortDateString()
					} ;

					// Regions
					foreach (var key in ((IDictionary<string, object>)pm.Regions).Keys)
						page.Regions.Add(new Region() { Name = key, Body = 
							((HtmlString)((IDictionary<string, object>)pm.Regions)[key]).ToString() }) ;

					// Properties
					foreach (var key in ((IDictionary<string, object>)pm.Properties).Keys)
						page.Properties.Add(new Property() { Name = key, Value = (string)
							((string)((IDictionary<string, object>)pm.Properties)[key]) }) ;

					return page ;
				}
			} catch {}
			return null ;
		}

		/// <summary>
		/// Gets the page specified by the given id as xml.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The page</returns>
		[OperationContract()]
		[WebGet(UriTemplate="page/xml/{id}", ResponseFormat=WebMessageFormat.Xml)]
		public Page GetPageXml(string id) {
			return GetPage(id) ;
		}

		/// <summary>
		/// Gets the content specified by the given id.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <returns>The content</returns>
		[OperationContract()]
		[WebGet(UriTemplate="content/{id}", ResponseFormat=WebMessageFormat.Json)]
		public Content GetContent(string id = "") {
			try {
				Models.Content c = Models.Content.GetSingle(new Guid(id)) ;

				if (c != null) {
					return new Content() {
						Id = c.Id,
						Filename = c.Filename,
						Type = c.Type,
						ThumbnailUrl = WebPages.WebPiranha.ApplicationPath + WebPages.WebPiranha.GetUrlPrefixForHandlerId("THUMBNAIL") + "/" + c.Id,
						ContentUrl = WebPages.WebPiranha.ApplicationPath + WebPages.WebPiranha.GetUrlPrefixForHandlerId("CONTENT") + "/" + c.Id,
						Created = c.Created.ToShortDateString(),
						Updated = c.Updated.ToShortDateString()
					};
				}
			} catch {}
			return null ;
		}

		/// <summary>
		/// Gets the content specified by the given id as xml.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <returns>The content</returns>
		[OperationContract()]
		[WebGet(UriTemplate="content/xml/{id}", ResponseFormat=WebMessageFormat.Xml)]
		public Content GetContentXml(string id) {
			return GetContent(id) ;
		}

		[OperationContract()]
		[WebGet(UriTemplate="changes/{date}", ResponseFormat=WebMessageFormat.Json)]
		public Changes GetChanges(string date) {
			Changes changes = new Changes() ;
			DateTime latest = Convert.ToDateTime(date) ;

			// Check if we have pages publised after the given date. If so return the sitemap
			if (Models.Page.GetScalar("SELECT COUNT(page_id) FROM page WHERE page_published > @0", latest) > 0)
				changes.Sitemap = GetSitemap() ;

			// Get all pages last published after the given date.
			Models.Page.GetFields("page_id", "page_last_published > @0 AND page_is_hidden = 0", latest).ForEach(p => 
				changes.Pages.Add(GetPage(p.Id.ToString()))) ;

			// Get all categories updated after the given date.
			GetCategories().Where(c => Convert.ToDateTime(c.Updated) > latest).Each((i, c) => changes.Categories.Add(c)) ;

			// Get all content updated after the given date.
			Models.Content.GetFields("content_id", "content_updated > @0", latest).ForEach(c =>
				changes.Content.Add(GetContent(c.Id.ToString()))) ;

			return changes ;
		}

		[OperationContract()]
		[WebGet(UriTemplate="changes/xml/{date}", ResponseFormat=WebMessageFormat.Xml)]
		public Changes GetChangesXml(string date) {
			return GetChanges(date) ;
		}

		#region Private methods
		/// <summary>
		/// Builds the sitemap recursivly.
		/// </summary>
		private List<Sitemap> BuildMap(List<Models.Sitemap> sm) {
			List<Sitemap> sitemap = new List<Sitemap>() ;

			sm.ForEach(map => {
				if (map.GroupId == Guid.Empty || HttpContext.Current.User.IsMember(map.GroupId)) {
					sitemap.Add(new Sitemap() {
						Id = map.Id,
						Title = map.Title,
						Permalink = map.Permalink,
						HasChildren = map.Pages.Count > 0,
						ChildNodes = BuildMap(map.Pages),
						LastPublished = map.LastPublished.ToShortDateString()
					}) ;
				}
			}) ;
			return sitemap ;
		}
		#endregion
	}
}
