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
	/// ReST API for pages.
	/// </summary>
	[ServiceContract()]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class PageService
	{
		/// <summary>
		/// Gets the page specified by the given id.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The page</returns>
		[OperationContract()]
		[WebGet(UriTemplate="get/{id}", ResponseFormat=WebMessageFormat.Json)]
		public Page Get(string id) {
			try {
				Models.PageModel pm = Models.PageModel.GetById(new Guid(id)) ;

				if (pm != null && (pm.Page.GroupId == Guid.Empty || HttpContext.Current.User.IsMember(pm.Page.GroupId))) {
					// Page data
					Page page = new Page() {
						Id = pm.Page.Id,
						Title = pm.Page.Title,
						Permalink = pm.Page.Permalink,
						IsHidden = ((Piranha.Models.Page)pm.Page).IsHidden,
						Attachments = ((Models.Page)pm.Page).Attachments,
						Created = pm.Page.Created.ToShortDateString(),
						Updated = pm.Page.Updated.ToShortDateString(),
						Published = pm.Page.Published.ToShortDateString(),
						LastPublished = pm.Page.LastPublished.ToShortDateString()
					} ;

					// Regions
					foreach (var key in ((IDictionary<string, object>)pm.Regions).Keys)
						page.Regions.Add(new Region() { Name = key, Body = 
							((HtmlString)((IDictionary<string, object>)pm.Regions)[key]).ToString() }) ;

					// Properties
					foreach (var key in ((IDictionary<string, object>)pm.Properties).Keys)
						page.Properties.Add(new Property() { Name = key, Value = (string)
							((string)((IDictionary<string, object>)pm.Properties)[key]) }) ;

					return page ;
				}
			} catch {}
			return null ;
		}

		/// <summary>
		/// Gets the page specified by the given id as xml.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The page</returns>
		[OperationContract()]
		[WebGet(UriTemplate="get/xml/{id}", ResponseFormat=WebMessageFormat.Xml)]
		public Page GetXml(string id) {
			return Get(id) ;
		}
	}
}
