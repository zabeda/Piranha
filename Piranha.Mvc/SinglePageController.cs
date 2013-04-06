using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Piranha.Models;

namespace Piranha.Mvc
{
	/// <summary>
	/// The single page controller is the base controller from which all controllers
	/// representing a page should derive from.
	/// </summary>
	public class SinglePageController : BaseController
	{
		/// <summary>
		/// Gets the current page model.
		/// </summary>
		/// <returns>The model</returns>
		public PageModel GetModel() {
			return GetModel<PageModel>(CurrentPermalink) ;
		}

		/// <summary>
		/// Gets the current model.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <returns>The model</returns>
		public T GetModel<T>() where T : PageModel {
			return GetModel<T>(CurrentPermalink) ;
		}

		/// <summary>
		/// Gets the page model identified by the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <returns>The model</returns>
		public PageModel GetModel(string permalink) {
			return GetModel<PageModel>(permalink) ;
		}

		/// <summary>
		/// Gets the model identified by the given permalink.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="permalink">The permalink</param>
		/// <returns>The model</returns>
		public T GetModel<T>(string permalink) where T : PageModel {
			var m = PageModel.GetByPermalink<T>(permalink) ;

			HttpContext.Items["Piranha_CurrentPage"] = m.Page ;
			HttpContext.Items["Piranha_CurrentPost"] = null ;

			return m ;
		}
	}
}
