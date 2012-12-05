using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

using Piranha.Extend;

namespace Piranha.Entities
{
	/// <summary>
	/// The region entity
	/// </summary>
	public class Region : StandardEntity<Region>
	{
		#region Members
		private IExtension body ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets weather this is a draft or not
		/// </summary>
		public bool IsDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the template id.
		/// </summary>
		public Guid RegionTemplateId { get ; set ; }

		/// <summary>
		/// Gets/sets the page id.
		/// </summary>
		public Guid PageId { get ; set ; }

		/// <summary>
		/// Gets/sets weather the page is a draft or not.
		/// </summary>
		public bool IsPageDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the region name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the body of the extension.
		/// </summary>
		public IExtension Body { 			
			get {
				if (body == null)
					body = GetBody() ;
				return body ;
			}
			set {
				SetBody(value) ;
			}
		}
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the page this region is related to.
		/// </summary>
		public Page Page { get ; set ; }

		/// <summary>
		/// Gets/sets the region template.
		/// </summary>
		public RegionTemplate RegionTemplate { get ; set ; }
		#endregion

		#region Internal properties
		/// <summary>
		/// Gets/sets the private Json serialized body.
		/// </summary>
		internal string InternalBody { get ; set ; }
		#endregion

		/// <summary>
		/// Gets the typed body element.
		/// </summary>
		/// <typeparam name="T">The body type</typeparam>
		/// <returns>The body</returns>
		public T GetBody<T>() where T : IExtension {
			return (T)Body ;
		}

		#region Private methods
		/// <summary>
		/// Gets the Json deserialized body for the region.
		/// </summary>
		/// <returns>The body</returns>
		private IExtension GetBody() {
			if (RegionTemplate == null) {
				using (var db = new DataContext()) {
					RegionTemplate = db.RegionTemplates.Where(t => t.Id == RegionTemplateId).Single() ;
				}
			}

			if (!String.IsNullOrEmpty(RegionTemplate.Type)) {
				var js = new JavaScriptSerializer() ;

				if (!String.IsNullOrEmpty(InternalBody)) {
					if (typeof(HtmlString).IsAssignableFrom(ExtensionManager.ExtensionTypes[RegionTemplate.Type]))
						return (IExtension)Activator.CreateInstance(ExtensionManager.ExtensionTypes[RegionTemplate.Type], InternalBody) ;
					return (IExtension)js.Deserialize(InternalBody, ExtensionManager.ExtensionTypes[RegionTemplate.Type]) ;
				}
				return (IExtension)Activator.CreateInstance(ExtensionManager.ExtensionTypes[RegionTemplate.Type]) ;
			}
			return null;
		}

		/// <summary>
		/// Updates the internal & external body data.
		/// </summary>
		private void SetBody(IExtension data) {
			JavaScriptSerializer js = new JavaScriptSerializer() ;

			body = data ;
			InternalBody = js.Serialize(data) ;
		}
		#endregion

		#region Events
		public override void OnSave(System.Data.EntityState state) {
			JavaScriptSerializer js = new JavaScriptSerializer() ;

			InternalBody = js.Serialize(Body) ;
		}
		#endregion
	}
}
