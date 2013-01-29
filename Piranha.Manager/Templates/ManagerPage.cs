using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	public abstract class ManagerPage : BasePage
	{
		/// <summary>
		/// The manager helper.
		/// </summary>
		public ManagerHelper Manager { get ; set ; }

		/// <summary>
		/// Default constructor. Creates a new manager layout page.
		/// </summary>
		public ManagerPage() : base() {
			Manager = new ManagerHelper(this) ;
		}
	}
}