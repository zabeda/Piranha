using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

using Piranha.Entities;
using Piranha.Extend;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the page type edit view.
	/// </summary>
	public sealed class PageTypeEditModel
	{
		#region Properties
		/// <summary>
		/// Get/sets the current template.
		/// </summary>
		public Entities.PageTemplate Template { get ; set ; }

		/// <summary>
		/// Gets/sets the different region types available.
		/// </summary>
		public IList<SelectListItem> RegionTypes { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageTypeEditModel() {
			Template = new Entities.PageTemplate() ;
			RegionTypes = ExtensionManager.Extensions.Where(e => e.ExtensionType == ExtensionType.Region).OrderBy(e => e.Name).Select(e =>
				new SelectListItem() { Text = e.Name, Value = e.Type.ToString() }).ToList() ;
		}

		/// <summary>
		/// Gets the edit model for the given id.
		/// </summary>
		/// <param name="id">The page template id</param>
		/// <returns>The model</returns>
		public static PageTypeEditModel GetById(Guid id) {
			var m = new PageTypeEditModel() ;

			using (var db = new DataContext()) {
				m.Template = db.PageTemplates.Include(t => t.RegionTemplates).Where(pt => pt.Id == id).Single() ;
			}
			return m ;
		}
	}
}