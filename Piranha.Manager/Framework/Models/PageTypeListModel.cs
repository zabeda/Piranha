using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Entities;
using Piranha.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the page type list.
	/// </summary>
	public sealed class PageTypeListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available templates.
		/// </summary>
		[ModelProperty(OnLoad="LoadTemplates")]
		public IList<Entities.PageTemplate> Templates { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageTypeListModel() {
			Templates = new List<Entities.PageTemplate>() ;
		}

		/// <summary>
		/// Loads the available templates.
		/// </summary>
		public void LoadTemplates() {
			using (var db = new DataContext()) {
				Templates = db.PageTemplates.OrderBy(t => t.Name).ToList() ;		
			}
		}
	}
}