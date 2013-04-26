using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Piranha.Extend.Regions
{
	/// <summary>
	/// Region for building forms.
	/// </summary>
	//[Export(typeof(IExtension))]
	//[ExportMetadata("InternalId", "FormRegion")]
	//[ExportMetadata("Name", "FormRegionName")]
	//[ExportMetadata("ResourceType", typeof(Resources.Extensions))]
	//[ExportMetadata("Type", ExtensionType.Region)]
	[Serializable]
	public class FormRegion : Extension
	{
		#region Inner classes
		/// <summary>
		/// The different types of form elements.
		/// </summary>
		[Serializable]
		public enum FormElementType 
		{
			Textbox, Textarea, Name, Email
		}

		/// <summary>
		/// The form element class.
		/// </summary>
		[Serializable]
		public class FormElement 
		{
			/// <summary>
			/// Gets/sets the name of the label.
			/// </summary>
			[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="FormRegionElementName")]
			public string Label { get ; set ; }

			/// <summary>
			/// Gets/sets the element type.
			/// </summary>
			[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="FormRegionElementType")]
			public FormElementType Type { get ; set ; }

			/// <summary>
			/// Gets/sets whether or not the element is required.
			/// </summary>
			[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="FormRegionElementRequired")]
			public bool IsRequired { get ; set ; }

			/// <summary>
			/// Gets/sets the available element types.
			/// </summary>
			[ScriptIgnore]
			public SelectList Types { get ; set ; }

			/// <summary>
			/// Default constructor.
			/// </summary>
			public FormElement() {
				Label = "" ;
				Type = FormElementType.Textbox ;
				IsRequired = false ;
				Types = new SelectList(Enum.GetValues(typeof(FormElementType)).Cast<FormElementType>().Select(e => e.ToString())) ;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// The form name that will be used when submitting the data.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="FormRegionFormName")]
		public string Name { get ; set ; }

		/// <summary>
		/// The name for the submit.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="FormRegionFormButton")]
		public string ButtonName { get ; set ; }

		/// <summary>
		/// Gets/sets the available form elements.
		/// </summary>
		public IList<FormElement> Items { get ; set ; }

		/// <summary>
		/// Get/sets the private region id.
		/// </summary>
		public Guid RegionId { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public FormRegion() {
			Items = new List<FormElement>() ;
			ButtonName = "Send" ;
		}

		/// <summary>
		/// Renders the form to html.
		/// </summary>
		/// <returns>The html content</returns>
		public IHtmlString RenderForm() {
			var str = new StringBuilder() ;

			str.Append(String.Format("<form method=\"post\" action=\"{0}\">", WebPages.WebPiranha.GetSiteUrl() + "/form/submit" )) ;
			str.Append(String.Format("<input type=\"hidden\" name=\"formbuilder_name\" value=\"{0}\" />", Name)) ;
			str.Append(String.Format("<input type=\"hidden\" name=\"formbuilder_id\" value=\"{0}\" />", RegionId)) ;
			str.Append(String.Format("<input type=\"hidden\" name=\"formbuilder_key\" value=\"{0}\" />", HttpContext.Current.Session.SessionID)) ;
			str.Append("<div class=\"response-message\"></div>") ;

			foreach (var elm in Items)
				WriteElement(str, elm) ;

			str.Append(String.Format("<button type=\"submit\">{0}</button>", ButtonName)) ;
			str.Append("</form>") ;

			return new HtmlString(str.ToString()) ;
		}

		/// <summary>
		/// Initializes the region for the manager interface.
		/// </summary>
		/// <param name="model">The model</param>
		public override void InitManager(object model) {
			var m = (Models.Manager.PageModels.EditModel)model;

			var self = m.Regions.Where(r => r.Type == this.GetType().FullName && ((FormRegion)r.Body).Name == Name).Single() ;
			RegionId = self.Id ;
		}

		#region Private methods
		/// <summary>
		/// Writes the given form element to the given string builder.
		/// </summary>
		/// <param name="str">The string builder</param>
		/// <param name="element">The form element</param>
		private void WriteElement(StringBuilder str, FormElement element) {
			str.Append(String.Format("<label>{0}</label>", element.Label)) ;
			
			if (element.Type == FormElementType.Textbox || element.Type == FormElementType.Name || element.Type == FormElementType.Email) {
				str.Append(String.Format("<input name=\"{0}\" type=\"text\" {1} />", element.Label, element.IsRequired ? "class=\"required\"" : "")) ;
			} else if (element.Type == FormElementType.Textarea) {
				str.Append(String.Format("<textarea name=\"{0}\" {1}></textarea>", element.Label, element.IsRequired ? "class=\"required\"" : "")) ;
			}
		}

		/// <summary>
		/// Generates a form name from the given string.
		/// </summary>
		/// <param name="str">The string</param>
		/// <returns>The generated form name</returns>
		private string GenerateName(string str) {
			var name = Regex.Replace(str.ToLower().Replace(" ", "-").Replace("å", "a").Replace("ä", "a").Replace("ö", "o"),
				@"[^a-z0-9-]", "").Replace("--", "-") ;

			if (name.EndsWith("-"))
				name = name.Substring(0, name.LastIndexOf("-")) ;
			if (name.StartsWith("-"))
				name = name.Substring(Math.Min(name.IndexOf("-") + 1, name.Length)) ;

			return name ;
		}
		#endregion
	}
}