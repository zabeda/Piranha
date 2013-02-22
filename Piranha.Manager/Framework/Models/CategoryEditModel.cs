using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

using AutoMapper;
using Piranha.Entities;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the category edit view.
	/// </summary>
	public class CategoryEditModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the current category
		/// </summary>
		public Entities.Category Category { get ; set ; }

		/// <summary>
		/// Gets/sets the available extensions
		/// </summary>
		public IList<Extend.IExtension> Extensions { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CategoryEditModel() {
			Category = new Entities.Category() ;
			Category.Permalink = new Entities.Permalink() ;
			Extensions = new List<Extend.IExtension>() ;
		}

		/// <summary>
		/// Gets the edit model for the category with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The edit model</returns>
		public static CategoryEditModel GetById(Guid id) {
			var m = new CategoryEditModel() ;

			using (var db = new DataContext()) {
				m.Category = db.Categories.Include(c => c.Permalink).Where(c => c.Id == id).Single() ;
			}
			return m ;
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <returns>Weather the database was updated</returns>
		public bool Save() {
			using (var db = new DataContext()) {
				var category = db.Categories.Include(c => c.Permalink).Where(c => c.Id == Category.Id).SingleOrDefault() ;
				if (category == null) {
					category = new Entities.Category() ;
					category.Permalink = new Entities.Permalink() ;
					category.Permalink.Id = category.PermalinkId = Guid.NewGuid() ;
					category.Permalink.Type = "CATEGORY" ;

					db.Categories.Add(category) ;
					db.Permalinks.Add(category.Permalink) ;
				}
				Mapper.Map<Entities.Category, Entities.Category>(Category, category) ;

				var ret = db.SaveChanges() > 0 ;
				Category.Id = category.Id ;

				return ret ;
			}
		}

		/// <summary>
		/// Validates the current model and stores the result in the model state.
		/// </summary>
		/// <param name="state">The model state</param>
		public void Validate(ModelStateDictionary state) {
			// Name
			if (String.IsNullOrEmpty(Category.Name))
				state.AddError("m.Category.Name", Piranha.Resources.Category.NameRequired) ;
			else if (Category.Name.Length > 64)
				state.AddError("m.Category.Name", Piranha.Resources.Category.NameLength) ;
			// Description
			if (Category.Description.Length > 255)
				state.AddError("m.Category.Description", Piranha.Resources.Category.DescriptionLength) ;
		}
	}
}