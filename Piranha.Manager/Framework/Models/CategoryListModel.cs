using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Entities;
using Piranha.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the category list.
	/// </summary>
	public sealed class CategoryListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available categories.
		/// </summary>
		[ModelProperty(OnLoad="LoadCategories")]
		public IList<Entities.Category> Categories { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CategoryListModel() {
			Categories = new List<Entities.Category>() ;
		}

		/// <summary>
		/// Loads the available categories.
		/// </summary>
		public void LoadCategories() {
			using (var db = new DataContext()) {
				Categories = db.Categories.OrderBy(c => c.Name).ToList() ;
			}
		}
	}
}