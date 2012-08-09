using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Models;

namespace Piranha.Web
{
	/// <summary>
	/// Abstract base class for the UI helper.
	/// </summary>
	public abstract class UIHelper
	{
		#region Properties
		/// <summary>
		/// Gets the current http context.
		/// </summary>
		protected abstract HttpContextBase Context { get ; }

		/// <summary>
		/// Gets the current page.
		/// </summary>
		protected abstract Page CurrentPage { get ; }

		/// <summary>
		/// Gets the current post.
		/// </summary>
		protected abstract Post CurrentPost { get ; }

		/// <summary>
		/// Gets the gravatar helper.
		/// </summary>
		public GravatarHelper Gravatar { get ; protected set ; }

		/// <summary>
		/// Gets the culture helper.
		/// </summary>
		public CultureHelper Culture { get ; protected set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new UI helper.
		/// </summary>
		protected UIHelper() {
			Gravatar = new GravatarHelper() ;
			Culture = new CultureHelper() ;
		}

		/// <summary>
		/// Converts the given virtual path to a relative url.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>A relative url</returns>
		protected abstract string Url(string virtualpath) ;

		/// <summary>
		/// Generates the tags appropriate for the html head.
		/// </summary>
		/// <returns>The head information</returns>
		public IHtmlString Head() {
			StringBuilder str = new StringBuilder() ;

			str.AppendLine("<meta name=\"generator\" content=\"Piranha\" />") ;
	        str.AppendLine("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\" />") ;

			/**
			 * Basic meta tags
			 */
			if (CurrentPage != null || CurrentPost != null) {
				var keywords = CurrentPage != null ? CurrentPage.Keywords : CurrentPost.Keywords ;
				var description = CurrentPage != null ? CurrentPage.Description : 
					(!String.IsNullOrEmpty(CurrentPost.Description) ? CurrentPost.Description : CurrentPost.Excerpt) ;

				if (!String.IsNullOrEmpty(description))
					str.AppendLine("<meta name=\"description\" content=\"" + description + "\" />") ;
				if (!String.IsNullOrEmpty(keywords))
					str.AppendLine("<meta name=\"keywords\" content=\"" + keywords + "\" />") ;
			}

			/**
			 * Open graph meta tags
			 */
			str.AppendLine("<meta property=\"og:site_name\" content=\"" + 
				SysParam.GetByName("SITE_TITLE").Value + "\" />") ;
			str.AppendLine("<meta property=\"og:url\" content=\"" + 
				"http://" + Context.Request.Url.DnsSafeHost + Context.Request.RawUrl + "\" />") ;

			if (CurrentPage != null && CurrentPage.IsStartpage) {
				str.AppendLine("<meta property=\"og:type\" content=\"website\" />") ;
				str.AppendLine("<meta property=\"og:description\" content=\"" + 
					SysParam.GetByName("SITE_DESCRIPTION").Value + "\" />") ;
			} else if (CurrentPage != null || CurrentPost != null) {
				var title = CurrentPage != null ? CurrentPage.Title : CurrentPost.Title ;
				var description = CurrentPage != null ? CurrentPage.Description : 
					(!String.IsNullOrEmpty(CurrentPost.Description) ? CurrentPost.Description : CurrentPost.Excerpt) ;

				str.AppendLine("<meta property=\"og:type\" content=\"article\" />") ;
				if (!String.IsNullOrEmpty(description)) {
					str.AppendLine("<meta property=\"og:description\" content=\"" + description + "\" />") ;
				}
				str.AppendLine("<meta property=\"og:title\" content=\"" + title + "\" />") ;
			}

			/**
			 * RSS Feeds
			 */
			str.AppendLine("<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" +
				SysParam.GetByName("SITE_TITLE").Value + "\" href=\"" + WebPages.WebPiranha.GetSiteUrl() + "/" +
				WebPages.WebPiranha.GetUrlPrefixForHandlerId("rss") + "\" />") ;

			return new HtmlString(str.ToString()) ;
		}

		/// <summary>
		/// Generates a full site url from the virtual path.
		/// </summary>
		/// <param name="virtualpath">The virtual path.</param>
		/// <returns>The full site url</returns>
		public IHtmlString SiteUrl(string virtualpath) {
			var request = HttpContext.Current.Request ;

			return new HtmlString(virtualpath.Replace("~/", request.ApplicationPath + (request.ApplicationPath != "/" ? "/" : ""))) ;
		}

		/// <summary>
		/// Generates the url to the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <returns>The url</returns>
		public IHtmlString Permalink(string permalink) {
			return SiteUrl("~/" + WebPages.WebPiranha.GetUrlPrefixForHandlerId("PERMALINK").ToLower() + "/" + permalink) ;
		}

		/// <summary>
		/// Generates the url to the given permalink.
		/// </summary>
		/// <param name="permalinkid">The id of the permalink</param>
		/// <returns></returns>
		public IHtmlString Permalink(Guid permalinkid) {
			var perm = Models.Permalink.GetSingle(permalinkid) ;
			if (perm != null)
				return SiteUrl("~/" + WebPages.WebPiranha.GetUrlPrefixForHandlerId("PERMALINK").ToLower() + "/" + perm.Name) ;
			return SiteUrl("~/") ;
		}

		/// <summary>
		/// Gets the URL to the content with the given id.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <param name="size">Optional image size</param>
		/// <returns>The content url</returns>
		public IHtmlString Content(Guid id, int size = 0) {
			Content cnt = Models.Content.GetSingle(id) ;
			
			if (cnt != null)
				return new HtmlString(Url("~/" + WebPages.WebPiranha.GetUrlPrefixForHandlerId("CONTENT") +
					"/" + id.ToString() + (size > 0 ? "/" + size.ToString() : ""))) ;
			return new HtmlString("") ; // TODO: Maybe a "missing content" url
		}

		/// <summary>
		/// Gets the URL to the content with the given id.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <param name="size">Optional image size</param>
		/// <returns>The content url</returns>
		public IHtmlString Content(string id, int size = 0) {
			return Content(new Guid(id), size) ;
		}

		/// <summary>
		/// Generates an image tag for the specified thumbnail.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <param name="size">Optional size</param>
		/// <returns>The image html string</returns>
		public IHtmlString Thumbnail(Guid id, int size = 0) {
			Content cnt = Models.Content.GetSingle(id) ;
			
			if (cnt != null)
				return new HtmlString(String.Format("<img src=\"{0}\" alt=\"{1}\" />", Url("~/" + 
					WebPages.WebPiranha.GetUrlPrefixForHandlerId("THUMBNAIL") + "/" + 
					id.ToString() + (size > 0 ? "/" + size.ToString() : "")), cnt.AlternateText)) ;
			return new HtmlString("") ; // TODO: Maybe a "missing image" image
		}

		/// <summary>
		/// Generates an image tag for the specified thumbnail.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <param name="size">Optional size</param>
		/// <returns>The image html string</returns>
		public IHtmlString Thumbnail(string id, int size = 0) {
			return Thumbnail(new Guid(id), size) ;
		}

		/// <summary>
		/// Return the site structure as an ul/li list with the current page selected.
		/// </summary>
		/// <param name="StartLevel">The start level of the menu</param>
		/// <param name="StopLevel">The stop level of the menu</param>
		/// <returns>A html string</returns>
		public IHtmlString Menu(int StartLevel = 1, int StopLevel = Int32.MaxValue, 
			string RootNode = "", string CssClass = "menu") 
		{
			StringBuilder str = new StringBuilder() ;
			List<Sitemap> sm = null ;

			Page Current = CurrentPage ;

			if (Current != null || StartLevel == 1) {
				if (Current == null)
					Current = new Page() ;
				if (RootNode != "") {
					Permalink pr = Models.Permalink.GetSingle("permalink_name = @0", RootNode) ;
					if (pr != null) {
						Page p = Page.GetByPermalinkId(pr.Id) ;
						Sitemap page = Sitemap.GetStructure(true).GetRootNode(p.Id) ;
						if (page != null)
							sm = page.Pages ;
					}
				} else {
					sm = GetStartLevel(Sitemap.GetStructure(true), 
						Current.Id, StartLevel) ;
				}
				if (sm != null) {
					RenderUL(Current, sm, str, StopLevel, CssClass) ;
				}
			}
			return new HtmlString(str.ToString()) ;
		}

		/// <summary>
		/// Renders the current breadcrumb.
		/// </summary>
		/// <param name="StartLevel">Optional start level</param>
		/// <param name="RootNode">Optional root node</param>
		/// <returns>The breadcrumb</returns>
		public IHtmlString Breadcrumb(int StartLevel = 1, string RootNode = "") {
			StringBuilder str = new StringBuilder() ;
			List<Sitemap> sm = null ;

			Page Current = CurrentPage ;

			if (Current != null) {
				if (RootNode != "") {
					Permalink pr = Models.Permalink.GetSingle("permalink_name = @0", RootNode) ;
					if (pr != null) {
						Page p = Page.GetByPermalinkId(pr.Id) ;
						Sitemap page = Sitemap.GetStructure(true).GetRootNode(p.Id) ;
						if (page != null)
							sm = page.Pages ;
					}
				} else {
					sm =sm = GetStartLevel(Sitemap.GetStructure(true), 
						Current.Id, StartLevel) ;
				}
				if (sm != null) {
					RenderBreadcrumb(Current, sm, str) ;
				}
			}
			return new HtmlString(str.ToString()) ;
		}

		/// <summary>
		/// Creates the action input for a piranha post back. This is only applicable
		/// to ASP.NET WebPages. If your using MVC, use the standard BeginForm instead.
		/// </summary>
		/// <param name="action">The form action</param>
		/// <returns>A html string</returns>
		public virtual IHtmlString FormAction(string action) {
			return new HtmlString(String.Format("<input type=\"hidden\" name=\"piranha_form_action\" value=\"{0}\" />",
				action)) ;
		}

		#region Private methods
		/// <summary>
		/// Gets the current start level for the sitemap.
		/// </summary>
		/// <param name="sm">The sitemap</param>
		/// <param name="id">The id of the current page</param>
		/// <param name="start">The desired startlevel</param>
		/// <returns>The sitemap</returns>
		private List<Sitemap> GetStartLevel(List<Sitemap> sm, Guid id, int start) {
			if (sm == null || sm.Count == 0 || sm[0].Level == start)
				return sm ;
			foreach (Sitemap page in sm)
				if (ChildActive(page, id))
					return GetStartLevel(page.Pages, id, start) ;
			return null ;
		}

		/// <summary>
		/// Gets the page with the given id from the structure
		/// </summary>
		/// <param name="sm">The sitemap</param>
		/// <param name="id">The id</param>
		/// <returns>The record</returns>
		/*
		private Sitemap GetRootNode(List<Sitemap> sm, Guid id) {
			if (sm != null) {
				foreach (Sitemap page in sm) {
					if (page.Id == id)
						return page ;
					Sitemap subpage = GetRootNode(page.Pages, id) ;
					if (subpage != null)
						return subpage ;
				}
			}
			return null ;
		}*/

		/// <summary>
		/// Renders an UL list for the given sitemap elements
		/// </summary>
		/// <param name="curr">The current page</param>
		/// <param name="sm">The sitemap elements</param>
		/// <param name="str">The string builder</param>
		/// <param name="stoplevel">The desired stop level</param>
		private void RenderUL(Page curr, List<Sitemap> sm, StringBuilder str, int stoplevel, string cssclass = "") {
			if (sm != null && sm.CountVisible() > 0 && sm[0].Level <= stoplevel) {
				str.AppendLine("<ul class=\"" + cssclass + "\">") ;
				foreach (Sitemap page in sm)
					if (!page.IsHidden) RenderLI(curr, page, str, stoplevel) ;
				str.AppendLine("</ul>") ;
			}
		}

		/// <summary>
		/// Renders an LI element for the given sitemap node.
		/// </summary>
		/// <param name="curr">The current page</param>
		/// <param name="page">The sitemap element</param>
		/// <param name="str">The string builder</param>
		/// <param name="stoplevel">The desired stop level</param>
		private void RenderLI(Page curr, Sitemap page, StringBuilder str, int stoplevel) {
			if (page.GroupId == Guid.Empty || HttpContext.Current.User.IsMember(page.GroupId)) {
				str.AppendLine("<li" + (curr.Id == page.Id ? " class=\"active\"" : 
					(ChildActive(page, curr.Id) ? " class=\"active-child	\"" : "")) + ">") ;
				str.AppendLine(String.Format("<a href=\"{0}\">{1}</a>", GenerateUrl(page),
					!String.IsNullOrEmpty(page.NavigationTitle) ? page.NavigationTitle : page.Title)) ;
				if (page.Pages.Count > 0)
					RenderUL(curr, page.Pages, str, stoplevel) ;
				str.AppendLine("</li>") ;
			}
		}

		/// <summary>
		/// Renders the breadcrumb from the given sitemap.
		/// </summary>
		/// <param name="curr">The current page</param>
		/// <param name="sm">The sitemap element</param>
		/// <param name="str">The string builder</param>
		private void RenderBreadcrumb(Page curr, List<Sitemap> sm, StringBuilder str) {
			if (sm != null && sm.CountVisible() > 0) {
				foreach (Sitemap page in sm) {
					if (page.Id == curr.Id) {
						str.Append("<span>" + page.Title + "</span>") ;
						return ;
					} else if (ChildActive(page, curr.Id)) {
						str.Append("<span><a href=\"" + Permalink(page.Permalink).ToString() + "\">" + page.Title + "</a></span> / ") ;
						RenderBreadcrumb(curr, page.Pages, str) ;
						return ;
					}
				}
			}
		}

		/// <summary>
		/// Checks if the given sitemap is active or has an active child
		/// </summary>
		/// <param name="page">The sitemap element</param>
		/// <param name="id">The page id to search for</param>
		/// <returns>If a child is selected</returns>
		private bool ChildActive(Sitemap page, Guid id) {
			if (page.Id == id)
				return true ;
			foreach (Sitemap sr in page.Pages) {
				if (ChildActive(sr, id))
					return true ;
			}
			return false ;
		}

		/// <summary>
		/// Generate the correct URL for the given sitemap node
		/// </summary>
		/// <param name="page">The sitemap</param>
		/// <returns>An action url</returns>
		private string GenerateUrl(ISitemap page) {
			if (page != null) {
				if (!String.IsNullOrEmpty(page.Redirect)) {
					if (page.Redirect.Contains("://"))
						return page.Redirect ;
					else if (page.Redirect.StartsWith("~/"))
						return Url(page.Redirect) ;
				}
				if (page.IsStartpage)
					return Url("~/") ;
				return Url("~/" + WebPages.WebPiranha.GetUrlPrefixForHandlerId("PERMALINK").ToLower() + "/" + 
					page.Permalink.ToLower()) ;
			}
			return "" ;
		}
		#endregion  
	}
}
