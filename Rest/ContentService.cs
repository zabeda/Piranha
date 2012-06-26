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
			try {
				Models.Content c = Models.Content.GetSingle(new Guid(id)) ;

				if (c != null) {
					return new Content() {
						Id = c.Id,
						Filename = c.Filename,
						Type = c.Type,
						ThumbnailUrl = WebPages.WebPiranha.ApplicationPath + WebPages.WebPiranha.GetUrlPrefixForHandlerId("THUMBNAIL") + "/" + c.Id,
						ContentUrl = WebPages.WebPiranha.ApplicationPath + WebPages.WebPiranha.GetUrlPrefixForHandlerId("CONTENT") + "/" + c.Id,
						Created = c.Created.ToShortDateString(),
						Updated = c.Updated.ToShortDateString()
					};
				}
			} catch {}
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
	}
}
