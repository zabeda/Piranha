using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Mvc
{
	/// <summary>
	/// Base view page for all Piranha MVC views.
	/// </summary>
	/// <typeparam name="T">The model type</typeparam>
	public abstract class BaseViewPage<T> : WebViewPage<T>
	{
		/// <summary>
		/// Gets the UI helper.
		/// </summary>
		public UIHelper UI { get ; private set ; }

		public Piranha.Web.SiteHelper Site { get; private set; }

		/// <summary>
		/// Gets the optional return url.
		/// </summary>
		public string ReturnUrl { get { return ViewBag.ReturnUrl ; } }

		/// <summary>
		/// Default constructor. Creates a new view page.
		/// </summary>
		public BaseViewPage() : base() {
			UI = new UIHelper(this) ;
			Site = new Piranha.Web.SiteHelper();
		}
	}
}
