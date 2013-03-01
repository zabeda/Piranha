using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Entities;
using Piranha.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the post type list.
	/// </summary>
	public sealed class PostTypeListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available templates.
		/// </summary>
		[ModelProperty(OnLoad="LoadTemplates")]
		public IList<Entities.PostTemplate> Templates { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PostTypeListModel() {
			Templates = new List<Entities.PostTemplate>() ;
		}

		/// <summary>
		/// Loads the available templates.
		/// </summary>
		public void LoadTemplates() {
			using (var db = new DataContext()) {
				Templates = db.PostTemplates.OrderBy(t => t.Name).ToList() ;		
			}
		}
	}
}