using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.WebPages;

namespace Piranha.Manager
{
	/// <summary>
	/// Extension methods for the manager module.
	/// </summary>
	internal static class Extensions
	{
		/// <summary>
		/// Adds a success message to the current view.
		/// </summary>
		/// <param name="page">The current page</param>
		/// <param name="msg">The message</param>
		public static void SuccessMessage(this BasePage page, string msg) {
			page.Page.MessageCss = "success" ;
			page.Page.Message = msg ;
		}

		/// <summary>
		/// Adds an error message to the current view.
		/// </summary>
		/// <param name="page">The current page</param>
		/// <param name="msg">The message</param>
		public static void ErrorMessage(this BasePage page, string msg) {
			page.Page.MessageCss = "error" ;
			page.Page.Message = msg ;
		}

		/// <summary>
		/// Adds an information message to the current view.
		/// </summary>
		/// <param name="page">The current page</param>
		/// <param name="msg">The message</param>
		public static void InformationMessage(this BasePage page, string msg) {
			page.Page.MessageCss = "" ;
			page.Page.Message = msg ;
		}
	}
}