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
		public delegate void BreadcrumbStart(Web.UIHelper ui, StringBuilder str) ;
		public delegate void BreadcrumbEnd(Web.UIHelper ui, StringBuilder str) ;
		public delegate void BreadcrumbItemHook(Web.UIHelper ui, StringBuilder str, Models.Sitemap page) ;
	}
}
