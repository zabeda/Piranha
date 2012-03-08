using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Piranha.Models;

namespace Piranha.Mvc
{
	/// <summary>
	/// View helper.
	/// </summary>
	public class PiranhaHelper : Web.UIHelper
	{
		#region Members
		private WebViewPage Parent ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current http context.
		/// </summary>
		protected override HttpContextBase Context {
			get { return Parent.Context ; }
		}

		/// <summary>
		/// Gets the current page.
		/// </summary>
		protected override Page CurrentPage {
			get { return (Page)Parent.ViewBag.Page ; }
		}
		#endregion

		/// <summary>
		/// Default constructor. Creates a new helper.
		/// </summary>
		/// <param name="parent">The parent controller.</param>
		public PiranhaHelper(WebViewPage parent) {
			Parent = parent ;
		}

		/// <summary>
		/// Converts the virtual path to an application url.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>An applicaiton url</returns>
		protected override string Url(string virtualpath) {
			return Parent.Url.Content(virtualpath) ;
		}
	}
}
