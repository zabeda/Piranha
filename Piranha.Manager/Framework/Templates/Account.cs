using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the account page.
	/// </summary>
	public abstract class Account : Piranha.WebPages.ContentPage<Models.AccountLoginModel>
	{
		/// <summary>
		/// Logs out the currently logged in user.
		/// </summary>
		public void Logout() {
			FormsAuthentication.SignOut() ;
			Session.Clear() ;

			Response.Redirect("~/manager/account") ;
		}
	}
}