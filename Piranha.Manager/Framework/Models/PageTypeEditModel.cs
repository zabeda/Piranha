using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using Piranha.Entities;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the page type edit view.
	/// </summary>
	public class PageTypeEditModel
	{
		#region Properties
		/// <summary>
		/// Get/sets the current template.
		/// </summary>
		public Entities.PageTemplate Template { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageTypeEditModel() {
			Template = new Entities.PageTemplate() ;
		}

		/// <summary>
		/// Gets the edit model for the given id.
		/// </summary>
		/// <param name="id">The page template id</param>
		/// <returns>The model</returns>
		public static PageTypeEditModel GetById(Guid id) {
			var m = new PageTypeEditModel() ;

			using (var db = new DataContext()) {
				m.Template = db.PageTemplates.Include(t => t.RegionTemplates).Single() ;
			}
			return m ;
		}
	}
}