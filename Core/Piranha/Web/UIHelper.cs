/*
 * Copyright (c) 2011-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

using AutoMapper;
using Piranha.Models;
using Piranha.WebPages;

namespace Piranha.Web
{
	/// <summary>
	/// The UI helper provides methods for genering url's and html for
	/// the web applications.
	/// </summary>
	public sealed class UIHelper
	{
		#region Members
		private Page currentPage = null;
		private Post currentPost = null;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current page.
		/// </summary>
		private Page CurrentPage {
			get {
				if (currentPage != null)
					return currentPage;
				return (Page)HttpContext.Current.Items["Piranha_CurrentPage"];
			}
			set { currentPage = value; }
		}

		/// <summary>
		/// Gets the current post.
		/// </summary>
		private Post CurrentPost {
			get {
				if (currentPost != null)
					return currentPost;
				return (Post)HttpContext.Current.Items["Piranha_CurrentPost"];
			}
			set { currentPost = value; }
		}

		/// <summary>
		/// Gets the culture helper.
		/// </summary>
		public CultureHelper Culture { get; private set; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new UI helper.
		/// </summary>
		public UIHelper() {
			Culture = new CultureHelper();
		}

		/// <summary>
		/// Sets the current page to the given value. This can be
		/// useful when using the UI helper in passive mode and the
		/// routing never sets the current page.
		/// </summary>
		/// <param name="page">The page</param>
		public void SetCurrent(Page page) {
			CurrentPage = page;
		}

		/// <summary>
		/// Sets the current post to the given value. This can be
		/// useful when using the UI helper in passive mode and the
		/// routing never sets the current post.
		/// </summary>
		/// <param name="page">The page</param>
		public void SetCurrent(Post post) {
			CurrentPost = post;
		}

		/// <summary>
		/// Generates the tags appropriate for the html head.
		/// </summary>
		/// <returns>The head information</returns>
		public IHtmlString Head() {
			StringBuilder str = new StringBuilder();
			HttpContext ctx = HttpContext.Current;

			str.AppendLine("<meta name=\"generator\" content=\"Piranha CMS " +
				FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion + "\" />");

			// http://www.validatethis.co.uk/news/fix-bad-value-x-ua-compatible-once-and-for-all/
			if (Config.RenderX_UA_CompatibleForIE && ctx != null && ctx.Request.ServerVariables["HTTP_USER_AGENT"] != null &&
				ctx.Request.ServerVariables["HTTP_USER_AGENT"].ToString().IndexOf("MSIE") > -1)
				str.AppendLine("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\" />");

			/**
			 * Basic meta tags
			 */
			if (CurrentPage != null || CurrentPost != null) {
				var keywords = CurrentPage != null ? CurrentPage.Keywords : CurrentPost.Keywords;
				var description = CurrentPage != null ? CurrentPage.Description :
					(!String.IsNullOrEmpty(CurrentPost.Description) ? CurrentPost.Description : CurrentPost.Excerpt);

				if (!String.IsNullOrEmpty(description))
					str.AppendLine("<meta name=\"description\" content=\"" + description + "\" />");
				if (!String.IsNullOrEmpty(keywords))
					str.AppendLine("<meta name=\"keywords\" content=\"" + keywords + "\" />");
			}

			/**
			 * Open graph meta tags
			 */
			str.AppendLine("<meta property=\"og:site_name\" content=\"" +
				WebPiranha.CurrentSite.MetaTitle + "\" />");
			str.AppendLine("<meta property=\"og:url\" content=\"" +
				"http://" + ctx.Request.Url.DnsSafeHost + HttpContext.Current.Request.RawUrl + "\" />");

			if (CurrentPage != null && CurrentPage.IsStartpage) {
				str.AppendLine("<meta property=\"og:type\" content=\"website\" />");
				str.AppendLine("<meta property=\"og:description\" content=\"" +
					WebPiranha.CurrentSite.MetaDescription + "\" />");
				str.AppendLine("<meta property=\"og:title\" content=\"" + WebPiranha.CurrentSite.MetaTitle + "\" />");
			} else if (CurrentPage != null || CurrentPost != null) {
				var title = CurrentPage != null ? CurrentPage.Title : CurrentPost.Title;
				var description = CurrentPage != null ? CurrentPage.Description :
					(!String.IsNullOrEmpty(CurrentPost.Description) ? CurrentPost.Description : CurrentPost.Excerpt);

				str.AppendLine("<meta property=\"og:type\" content=\"article\" />");
				if (!String.IsNullOrEmpty(description)) {
					str.AppendLine("<meta property=\"og:description\" content=\"" + description + "\" />");
				}
				str.AppendLine("<meta property=\"og:title\" content=\"" + title + "\" />");
			}

			/**
			 * RSS Feeds
			 */
			str.AppendLine("<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" +
				WebPiranha.CurrentSite.MetaTitle + "\" href=\"" + WebPages.WebPiranha.GetSiteUrl() + "/" +
				Application.Current.Handlers.GetUrlPrefix("rss") + "\" />");

			/**
			 * Check if hook is attached.
			 */
			if (Hooks.Head.Render != null)
				Hooks.Head.Render(this, str, CurrentPage, CurrentPost);

			return new HtmlString(str.ToString());
		}

		/// <summary>
		/// Generates a full site url from the virtual path.
		/// </summary>
		/// <param name="virtualpath">The virtual path.</param>
		/// <returns>The full site url</returns>
		public IHtmlString SiteUrl(string virtualpath) {
			var request = HttpContext.Current.Request;
			return new HtmlString(virtualpath.Replace("~/", request.ApplicationPath + (request.ApplicationPath != "/" ? "/" : "") + WebPiranha.CurrentSiteExtension));
		}

		/// <summary>
		/// Generates an absolute url from the virtual path or site url.
		/// </summary>
		/// <param name="url">The url</param>
		/// <returns>The absolute url</returns>
		public IHtmlString AbsoluteUrl(string url) {
			var request = HttpContext.Current.Request;

			// First, convert virtual paths to site url's
			if (url.StartsWith("~/"))
				url = SiteUrl(url).ToString(); ;

			// Now add server, scheme and port
			return new HtmlString(request.Url.Scheme + "://" + request.Url.DnsSafeHost +
				(!request.Url.IsDefaultPort ? ":" + request.Url.Port.ToString() : "") + url);
		}

		/// <summary>
		/// Generates the url to the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <param name="prefix">Optional culture prefix</param>
		/// <returns>The url</returns>
		public IHtmlString Permalink(string permalink = "", string prefix = "") {
			if (prefix == "") {
				prefix = WebPiranha.CulturePrefixes.ContainsKey(CultureInfo.CurrentUICulture.Name) ?
					WebPiranha.CulturePrefixes[CultureInfo.CurrentUICulture.Name] + "/" : "";
			}
			if (prefix == null)
				prefix = "";
			if (prefix != "" && !prefix.EndsWith("/"))
				prefix += "/";
			if (permalink == "" && CurrentPage != null)
				permalink = CurrentPage.Permalink;
			return SiteUrl("~/" + prefix + (!Config.PrefixlessPermalinks ?
				Application.Current.Handlers.GetUrlPrefix("PERMALINK").ToLower() + "/" : "") + permalink);
		}

		/// <summary>
		/// Generates the url to the given permalink.
		/// </summary>
		/// <param name="permalinkid">The id of the permalink</param>
		/// <param name="prefix">Optional culture prefix</param>
		/// <returns></returns>
		public IHtmlString Permalink(Guid permalinkid, string prefix = "") {
			if (prefix == "") {
				prefix = WebPiranha.CulturePrefixes.ContainsKey(CultureInfo.CurrentUICulture.Name) ?
					WebPiranha.CulturePrefixes[CultureInfo.CurrentUICulture.Name] + "/" : "";
			}
			if (prefix == null)
				prefix = "";
			if (prefix != "" && !prefix.EndsWith("/"))
				prefix += "/";
			var perm = Models.Permalink.GetSingle(permalinkid);
			if (perm != null)
				return SiteUrl("~/" + prefix + (!Config.PrefixlessPermalinks ?
				Application.Current.Handlers.GetUrlPrefix("PERMALINK").ToLower() + "/" : "") + perm.Name);
			return SiteUrl("~/" + prefix);
		}

		/// <summary>
		/// Gets the URL to the content with the given id.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <param name="width">Optional width</param>
		/// <param name="height">Optional height</param>
		/// <returns>The content url</returns>
		public IHtmlString Content(Guid id, int width = 0, int height = 0) {
			Content cnt = Models.Content.GetSingle(id);
			var draft = (CurrentPage != null && CurrentPage.IsDraft) || (CurrentPost != null && CurrentPost.IsDraft);

			if (cnt != null) {
				var perm = cnt.PermalinkId != Guid.Empty ? Models.Permalink.GetSingle(cnt.PermalinkId) : null;

				if (perm != null) {
					// Generate content url from permalink
					var segments = perm.Name.Split(new char[] { '.' });
					var name = segments[0];
					var suffix = segments.Length > 1 ? segments[1] : "";

					if (width > 0)
						name += "_" + width.ToString();
					if (height > 0)
						name += "_" + height.ToString();
					name += "." + suffix;

					return new HtmlString(SiteUrl("~/" + (!draft ? Application.Current.Handlers.GetUrlPrefix("CONTENTHANDLER") :
						Application.Current.Handlers.GetUrlPrefix("CONTENTDRAFT")) + "/") + name);
				}
				// Generate content url from id
				return new HtmlString(SiteUrl("~/" + (!draft ? Application.Current.Handlers.GetUrlPrefix("CONTENT") : Application.Current.Handlers.GetUrlPrefix("CONTENTDRAFT")) +
					"/" + id.ToString() + (width > 0 ? "/" + width.ToString() : "")) + (height > 0 ? "/" + height.ToString() : ""));
			}
			return new HtmlString(""); // TODO: Maybe a "missing content" url
		}

		/// <summary>
		/// Gets the URL to the content with the given id.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <param name="width">Optional width</param>
		/// <param name="height">Optional height</param>
		/// <returns>The content url</returns>
		public IHtmlString Content(string id, int width = 0, int height = 0) {
			return Content(new Guid(id), width, height);
		}

		/// <summary>
		/// Generates an image tag for the specified thumbnail.
		/// </summary>
		/// <param name="id">The content, page or post id.</param>
		/// <param name="size">Optional size</param>
		/// <returns>The image html string</returns>
		public IHtmlString Thumbnail(Guid id, int size = 0) {
			Content cnt = Models.Content.GetSingle(id);
			var draft = (CurrentPage != null && CurrentPage.IsDraft) || (CurrentPost != null && CurrentPost.IsDraft);

			if (cnt != null) {
				var thumbId = cnt.IsImage ? id : (cnt.IsFolder ? Drawing.Thumbnails.GetIdByType("folder") : Drawing.Thumbnails.GetIdByType(cnt.Type));

				return new HtmlString(String.Format("<img src=\"{0}\" alt=\"{1}\" />", SiteUrl("~/" +
					(!draft ? Application.Current.Handlers.GetUrlPrefix("THUMBNAIL") : Application.Current.Handlers.GetUrlPrefix("THUMBNAILDRAFT")) + "/" +
					thumbId.ToString() + (size > 0 ? "/" + size.ToString() : "")), cnt.AlternateText));
			} else {
				Page page = Page.GetSingle(id);
				if (page != null && page.Attachments.Count > 0) {
					return Thumbnail(page.Attachments[0], size);
				}
				Post post = Post.GetSingle(id);
				if (post != null && post.Attachments.Count > 0) {
					return Thumbnail(post.Attachments[0], size);
				}
			}
			return new HtmlString(""); // TODO: Maybe a "missing image" image
		}

		/// <summary>
		/// Generates an image tag for the specified thumbnail.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <param name="size">Optional size</param>
		/// <returns>The image html string</returns>
		public IHtmlString Thumbnail(string id, int size = 0) {
			return Thumbnail(new Guid(id), size);
		}

		/// <summary>
		/// Gets the url to the uploaded content with the given id.
		/// </summary>
		/// <param name="id">The upload id</param>
		/// <param name="width">Optional width</param>
		/// <param name="height">Optional height</param>
		/// <returns>The url</returns>
		public IHtmlString Upload(Guid id, int width = 0, int height = 0) {
			Upload ul = Models.Upload.GetSingle(id);

			if (ul != null)
				return new HtmlString(SiteUrl("~/" + Application.Current.Handlers.GetUrlPrefix("UPLOAD") +
					"/" + id.ToString() + (width > 0 ? "/" + width.ToString() : "")) + (height > 0 ? "/" + height.ToString() : ""));
			return new HtmlString(""); // TODO: Maybe a "missing content" url
		}

		/// <summary>
		/// Gets the url to the uploaded content with the given id.
		/// </summary>
		/// <param name="id">The upload id</param>
		/// <param name="width">Optional width</param>
		/// <param name="height">Optional height</param>
		/// <returns>The url</returns>
		public IHtmlString Upload(string id, int width = 0, int height = 0) {
			return Upload(new Guid(id), width, height);
		}

		/// <summary>
		/// Return the site structure as an ul/li list with the current page selected.
		/// </summary>
		/// <param name="StartLevel">The start level of the menu</param>
		/// <param name="StopLevel">The stop level of the menu</param>
		/// <param name="Levels">The number of levels. Use this if you don't know the start level</param>
		/// <returns>A html string</returns>
		public IHtmlString Menu(int StartLevel = 1, int StopLevel = Int32.MaxValue, int Levels = 0,
			string RootNode = "", string CssClass = "menu", bool RenderParent = false) {
			StringBuilder str = new StringBuilder();
			List<Sitemap> sm = null;

			Page Current = CurrentPage;

			if (Current != null || StartLevel == 1) {
				if (Current == null)
					Current = new Page();
				if (RootNode != "") {
					Permalink pr = Models.Permalink.GetSingle("permalink_name = @0 AND permalink_namespace_id = @1", RootNode, WebPiranha.CurrentSite.NamespaceId);
					if (pr != null) {
						Page p = Page.GetByPermalinkId(pr.Id);
						Sitemap page = Sitemap.GetStructure(true).GetRootNode(p.Id);
						if (page != null) {
							sm = new List<Sitemap>();
							if (RenderParent)
								sm.Add(Mapper.Map<Sitemap, Sitemap>(page));
							sm.AddRange(page.Pages);
						}
					}
				} else {
					sm = GetStartLevel(Sitemap.GetStructure(true),
						Current.Id, StartLevel, RenderParent);
				}
				if (sm != null) {
					if (StopLevel == Int32.MaxValue && Levels > 0 && sm.Count > 0)
						StopLevel = sm[0].Level + Math.Max(0, Levels - 1);
					RenderUL(Current, sm, str, StopLevel, CssClass);
				}
			}
			return new HtmlString(str.ToString());
		}

		/// <summary>
		/// Renders the current breadcrumb.
		/// </summary>
		/// <param name="StartLevel">Optional start level</param>
		/// <param name="RootNode">Optional root node</param>
		/// <returns>The breadcrumb</returns>
		public IHtmlString Breadcrumb(int StartLevel = 1, string RootNode = "") {
			StringBuilder str = new StringBuilder();
			List<Sitemap> sm = null;

			Page Current = CurrentPage;

			if (Current != null) {
				if (RootNode != "") {
					Permalink pr = Models.Permalink.GetSingle("permalink_name = @0", RootNode);
					if (pr != null) {
						Page p = Page.GetByPermalinkId(pr.Id);
						Sitemap page = Sitemap.GetStructure(true).GetRootNode(p.Id);
						if (page != null)
							sm = page.Pages;
					}
				} else {
					sm = GetStartLevel(Sitemap.GetStructure(true),
						Current.Id, StartLevel);
				}
				if (sm != null) {
					if (Hooks.Breadcrumb.RenderStart != null)
						Hooks.Breadcrumb.RenderStart(this, str);
					RenderBreadcrumb(Current, sm, str);
					if (Hooks.Breadcrumb.RenderEnd != null)
						Hooks.Breadcrumb.RenderEnd(this, str);
				}
			}
			return new HtmlString(str.ToString());
		}

		/// <summary>
		/// Creates the action input for a piranha post back. This is only applicable
		/// to ASP.NET WebPages. If your using MVC, use the standard BeginForm instead.
		/// </summary>
		/// <param name="action">The form action</param>
		/// <returns>A html string</returns>
		[Obsolete("Please use Piranha.WebPages.FormHelper.Action() instead")]
		public IHtmlString FormAction(string action) {
			return new HtmlString(String.Format("<input type=\"hidden\" name=\"piranha_form_action\" value=\"{0}\" />",
				action));
		}

		/// <summary>
		/// Gets an encrypted API-key valid for 30 minutes.
		/// </summary>
		/// <param name="apiKey">The API-key</param>
		/// <returns>The ecnrypted key</returns>
		public IHtmlString APIKey(Guid apiKey) {
			return new HtmlString(HttpUtility.UrlEncode(APIKeys.EncryptApiKey(apiKey)));
		}

		/// <summary>
		/// Gets an ecrypted API-key valid for 30 minutes. If no API-key is provided
		/// the key for the currently logged in user is used.
		/// </summary>
		/// <param name="apiKey"></param>
		/// <returns></returns>
		public IHtmlString APIKey(string apiKey = "") {
			if (String.IsNullOrEmpty(apiKey)) {
				var user = HttpContext.Current.User;

				if (Application.Current.UserProvider.IsAuthenticated && user.GetProfile().APIKey != Guid.Empty)
					return APIKey(user.GetProfile().APIKey);
				return new HtmlString("");
			}
			return APIKey(new Guid(apiKey));
		}

		#region Private methods
		private string Url(string virtualpath) {
			var request = HttpContext.Current.Request;
			return virtualpath.Replace("~/", request.ApplicationPath + (request.ApplicationPath != "/" ? "/" : ""));
		}

		/// <summary>
		/// Gets the current start level for the sitemap.
		/// </summary>
		/// <param name="sm">The sitemap</param>
		/// <param name="id">The id of the current page</param>
		/// <param name="start">The desired startlevel</param>
		/// <returns>The sitemap</returns>
		private List<Sitemap> GetStartLevel(List<Sitemap> sm, Guid id, int start, bool includedParent = false) {
			if (sm == null || sm.Count == 0 || sm[0].Level == start)
				return sm;
			foreach (Sitemap page in sm)
				if (ChildActive(page, id)) {
					if (includedParent && page.Level == start - 1) {
						var pages = new List<Sitemap>();

						pages.Add(Mapper.Map<Sitemap, Sitemap>(page));
						pages.AddRange(page.Pages);

						return pages;
					}
					return GetStartLevel(page.Pages, id, start);
				}
			return null;
		}

		/// <summary>
		/// Renders an UL list for the given sitemap elements
		/// </summary>
		/// <param name="curr">The current page</param>
		/// <param name="sm">The sitemap elements</param>
		/// <param name="str">The string builder</param>
		/// <param name="stoplevel">The desired stop level</param>
		private void RenderUL(Page curr, List<Sitemap> sm, StringBuilder str, int stoplevel, string cssclass = "") {
			if (sm != null && sm.CountVisible() > 0 && sm[0].Level <= stoplevel) {
				// Render level start
				if (Hooks.Menu.RenderLevelStart != null) {
					Hooks.Menu.RenderLevelStart(this, str, cssclass);
				} else {
					str.AppendLine("<ul class=\"" + cssclass + "\">");
				}
				// Render items
				foreach (Sitemap page in sm)
					if (!page.IsHidden) RenderLI(curr, page, str, stoplevel);
				// Render level end
				if (Hooks.Menu.RenderLevelEnd != null) {
					Hooks.Menu.RenderLevelEnd(this, str, cssclass);
				} else {
					str.AppendLine("</ul>");
				}
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
				var active = curr.Id == page.Id;
				var childactive = ChildActive(page, curr.Id);

				// Render item start
				if (Hooks.Menu.RenderItemStart != null) {
					Hooks.Menu.RenderItemStart(this, str, page, active, childactive);
				} else {
					var hasChild = page.Pages.Count > 0 ? " has-child" : "";
					str.AppendLine("<li" + (curr.Id == page.Id ? " class=\"active" + hasChild + "\"" :
						(ChildActive(page, curr.Id) ? " class=\"active-child" + hasChild + "\"" :
						(page.Pages.Count > 0 ? " class=\"has-child\"" : ""))) + ">");
				}
				// Render item link
				if (Hooks.Menu.RenderItemLink != null) {
					Hooks.Menu.RenderItemLink(this, str, page);
				} else {
					str.AppendLine(String.Format("<a href=\"{0}\">{1}</a>", GenerateUrl(page),
						!String.IsNullOrEmpty(page.NavigationTitle) ? page.NavigationTitle : page.Title));
				}
				// Render subpages
				if (page.Pages.Count > 0)
					RenderUL(curr, page.Pages, str, stoplevel);
				// Render item end
				if (Hooks.Menu.RenderItemEnd != null) {
					Hooks.Menu.RenderItemEnd(this, str, page, active, childactive);
				} else {
					str.AppendLine("</li>");
				}
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
						if (Hooks.Breadcrumb.RenderActiveItem != null) {
							Hooks.Breadcrumb.RenderActiveItem(this, str, page);
						} else {
							str.Append("<span>" + page.Title + "</span>");
						}
						return;
					} else if (ChildActive(page, curr.Id)) {
						if (Hooks.Breadcrumb.RenderItem != null) {
							Hooks.Breadcrumb.RenderItem(this, str, page);
						} else {
							str.Append("<span><a href=\"" + Permalink(page.Permalink).ToString() + "\">" + page.Title + "</a></span> / ");
						}
						RenderBreadcrumb(curr, page.Pages, str);
						return;
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
				return true;
			foreach (Sitemap sr in page.Pages) {
				if (ChildActive(sr, id))
					return true;
			}
			return false;
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
						return page.Redirect;
					else if (page.Redirect.StartsWith("~/"))
						return Url(page.Redirect);
				}
				if (page.IsStartpage)
					return Url("~/");
				return Url("~/" + (!Config.PrefixlessPermalinks ?
					Application.Current.Handlers.GetUrlPrefix("PERMALINK").ToLower() + "/" : "") + page.Permalink.ToLower());
			}
			return "";
		}
		#endregion
	}
}
