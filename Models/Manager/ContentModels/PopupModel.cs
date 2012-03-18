using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Piranha.Data;

namespace Piranha.Models.Manager.ContentModels
{
	/// <summary>
	/// List model for the content view in the manager area.
	/// </summary>
	public class PopupModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available content.
		/// </summary>
		public List<Piranha.Models.Content> Content { get ; set ; }

		/// <summary>
		/// Gets/sets the new content for uploading.
		/// </summary>
		public Piranha.Models.Content NewContent { get ; set ; }

		/// <summary>
		/// Gets/sets the optional file.
		/// </summary>
		public HttpPostedFileBase UploadedFile { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new model.
		/// </summary>
		public PopupModel() {
			Content = new List<Piranha.Models.Content>() ;
			NewContent = new Piranha.Models.Content() ;
		}

		/// <summary>
		/// Gets all available content.
		/// </summary>
		/// <returns>A list of content records</returns>
		public static PopupModel Get() {
			PopupModel lm = new PopupModel() ;
			lm.Content = Piranha.Models.Content.Get(new Params() { OrderBy = "content_filename ASC" });

			return lm ;
		}
	}
}
