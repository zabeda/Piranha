using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Piranha.Entities
{
	/// <summary>
	/// The page template entity.
	/// </summary>
	public class PageTemplate : StandardEntity<PageTemplate>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the template name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the description for the manager interface.
		/// </summary>
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets the html-preview for the manager interface.
		/// </summary>
		public string Preview { get ; set ; }

		/// <summary>
		/// Gets/sets the html-regions defined. This field is NOT searchable in queries.
		/// </summary>
		public IList<string> Regions { get ; set ; }

		/// <summary>
		/// Gets/sets the properties defined. This field is NOT searchable in queries.
		/// </summary>
		public IList<string> Properties { get ; set ; }

		/// <summary>
		/// Gets/sets the optional template that should render the view.
		/// </summary>
		public string ViewTemplate { get ; set ; }

		/// <summary>
		/// Gets/sets weather the page should be able to override the template.
		/// </summary>
		public bool ShowViewTemplate { get ; set ; }

		/// <summary>
		/// Gets/sets the optional uri to which the page should redirect.
		/// </summary>
		public string ViewRedirect { get ; set ; }

		/// <summary>
		/// Gets/sets weather the page should be able to override the redirect.
		/// </summary>
		public bool ShowViewRedirect { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the available region templates.
		/// </summary>
		public IList<RegionTemplate> RegionTemplates { get ; set ; }
		#endregion

		#region Internal properties
		/// <summary>
		/// Gets/sets the persisted json data for the regions.
		/// </summary>
		internal string RegionsJson { get ; set ; }

		/// <summary>
		/// Gets/sets the persisted json data for the properties.
		/// </summary>
		internal string PropertiesJson { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates an empty page template.
		/// </summary>
		public PageTemplate() {
			Regions = new List<string>() ;
			Properties = new List<string>() ;
		}

		#region Events
		/// <summary>
		/// Called when the entity has been loaded.
		/// </summary>
		public override void OnLoad() {
			var js = new JavaScriptSerializer() ;

			Regions = !String.IsNullOrEmpty(RegionsJson) ? js.Deserialize<List<string>>(RegionsJson) : Regions ;
			Properties = !String.IsNullOrEmpty(PropertiesJson) ? js.Deserialize<List<string>>(PropertiesJson) : Properties ;

			base.OnLoad() ;
		}

		/// <summary>
		/// Called when the entity is about to be saved.
		/// </summary>
		/// <param name="state">The current entity state</param>
		public override void OnSave(System.Data.EntityState state) {
			var js = new JavaScriptSerializer() ;

			RegionsJson = js.Serialize(Regions) ;
			PropertiesJson = js.Serialize(Properties) ;

			base.OnSave(state);
		}
		#endregion
	}
}
