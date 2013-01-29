using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	public abstract class ManagerContentPage<T> : ContentPage<T> 
	{
		/// <summary>
		/// The manager helper.
		/// </summary>
		public ManagerHelper Manager { get ; set ; }

		/// <summary>
		/// Default constructor. Creates a new manager layout page.
		/// </summary>
		public ManagerContentPage() : base() {
			Manager = new ManagerHelper(this) ;
			Layout = SiteManager.VirtualPath + "Shared/_Layout.cshtml" ;
		}

		/// <summary>
		/// Adds a success message to the current view.
		/// </summary>
		/// <param name="msg">The message</param>
		protected void SuccessMessage(string msg) {
			Page.MessageCss = "success" ;
			Page.Message = msg ;
		}

		/// <summary>
		/// Adds an error message to the current view.
		/// </summary>
		/// <param name="msg"></param>
		protected void ErrorMessage(string msg) {
			Page.MessageCss = "error" ;
			Page.Message = msg ;
		}

		/// <summary>
		/// Adds an information message to the current view.
		/// </summary>
		/// <param name="msg"></param>
		protected void InformationMessage(string msg) {
			Page.MessageCss = "" ;
			Page.Message = msg ;
		}
	}
}