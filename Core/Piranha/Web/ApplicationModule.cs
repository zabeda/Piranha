using System;
using System.Web;

namespace Piranha.Web
{
	/// <summary>
	/// The main http module for the Piranha CMS application.
	/// </summary>
	public class ApplicationModule : IHttpModule
	{
		/// <summary>
		/// Disposes all allicated resources.
		/// </summary>
		public void Dispose() {
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Executed for all requests in the application
		/// </summary>
		/// <param name="context">The current application context</param>
		public void Init(HttpApplication context) {
			context.BeginRequest += (sender, e) => {
				WebPages.WebPiranha.BeginRequest(((System.Web.HttpApplication)sender).Context);
			};
		}
	}
}