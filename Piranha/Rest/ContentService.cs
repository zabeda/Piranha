using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using Piranha.Data;
using Piranha.Rest.DataContracts;

namespace Piranha.Rest
{
	/// <summary>
	/// ReST API for content.
	/// </summary>
	[ServiceContract()]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ContentService
	{
		/// <summary>
		/// Gets the content specified by the given id.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <returns>The content</returns>
		[OperationContract()]
		[WebGet(UriTemplate="get/{id}", ResponseFormat=WebMessageFormat.Json)]
		public Content Get(string id = "") {
			if (!String.IsNullOrEmpty(id)) {
				return Get(new Guid(id)) ;
			}
			return null ;
		}

		/// <summary>
		/// Gets the content specified by the given id as xml.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <returns>The content</returns>
		[OperationContract()]
		[WebGet(UriTemplate="get/xml/{id}", ResponseFormat=WebMessageFormat.Xml)]
		public Content GetXml(string id) {
			return Get(id) ;
		}

		/// <summary>
		/// Gets the content specified by the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="draft">Weather to get the draft or not</param>
		/// <returns>The content</returns>
		internal Content Get(Guid id, bool draft = false) {
			try {
				Models.Content c = Models.Content.GetSingle(id, draft) ;

				if (c != null) {
					return new Content() {
						Id = c.Id,
						ParentId = c.ParentId,
						Filename = c.Filename,
						Name = c.Name,
						DisplayName = c.DisplayName,
						Description = c.Description,
						Type = c.Type,
						Size = c.Size,
						ThumbnailUrl = WebPages.WebPiranha.ApplicationPath + 
							(!draft ? WebPages.WebPiranha.GetUrlPrefixForHandlerId("THUMBNAIL") :
							WebPages.WebPiranha.GetUrlPrefixForHandlerId("THUMBNAILDRAFT")) + "/" + c.Id,
						ContentUrl = WebPages.WebPiranha.ApplicationPath + 
							(!draft ? WebPages.WebPiranha.GetUrlPrefixForHandlerId("CONTENT") :
							WebPages.WebPiranha.GetUrlPrefixForHandlerId("CONTENTDRAFT")) + "/" + c.Id,
						Created = c.Created.ToString(),
						Updated = c.Updated.ToString()
					};
				}
			} catch {}
			return null ;
		}
	}
}
