using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha
{
	/// <summary>
	/// The different delegates used by the framework.
	/// </summary>
	public static class Delegates
	{
		public delegate void BreadcrumbStartHook(Web.UIHelper ui, StringBuilder str) ;
		public delegate void BreadcrumbEndHook(Web.UIHelper ui, StringBuilder str) ;
		public delegate void BreadcrumbItemHook(Web.UIHelper ui, StringBuilder str, Models.Sitemap page) ;

		public delegate void HeadHook(Web.UIHelper ui, StringBuilder str, Models.Page page, Models.Post post) ;

		public delegate void MenuItemHook(Web.UIHelper ui, StringBuilder str, Models.Sitemap page, bool active, bool activechild) ;
		public delegate void MenuItemLinkHook(Web.UIHelper ui, StringBuilder str, string title, string url) ;
		public delegate void MenuLevelHook(Web.UIHelper ui, StringBuilder str, string cssclass) ;
	}
}
