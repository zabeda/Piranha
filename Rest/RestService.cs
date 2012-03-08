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
	/// This is the mail ReST API.
	/// </summary>
	[ServiceContract()]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class RestService
	{
		/// <summary>
		/// Gets the available contracts.
		/// </summary>
		/// <returns>The categories</returns>
		[OperationContract()]
		[WebGet(UriTemplate="categories", ResponseFormat=WebMessageFormat.Json)]
		public List<Category> GetCategories() {
			List<Category> categories = new List<Category>() ;

			Models.Category.Get(new Params() { OrderBy = "category_name" }).ForEach(c => 
				categories.Add(new Category() {
					Id = c.Id,
					Name = c.Name,
					Permalink = c.Permalink,
					Description = c.Description
				}));
			return categories ;
		}

		/// <summary>
		/// Gets the page specified by the given permalink
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <returns>The page</returns>
		[OperationContract()]
		[WebGet(UriTemplate="page/{permalink}", ResponseFormat=WebMessageFormat.Json)]
		public Page GetPage(string permalink) {
			Models.PageModel pm = Models.PageModel.GetByPermalink(permalink) ;

			if (pm != null && (pm.Page.GroupId == Guid.Empty || HttpContext.Current.User.IsMember(pm.Page.GroupId))) {
				// Page data
				Page page = new Page() {
					Id = pm.Page.Id,
					Title = pm.Page.Title,
					Permalink = pm.Page.Permalink
				} ;

				// Regions
				foreach (var key in ((IDictionary<string, object>)pm.Regions).Keys)
					page.Regions.Add(new Region() { Name = key, Body = 
						((HtmlString)((IDictionary<string, object>)pm.Regions)[key]).ToString() }) ;

				// Properties
				foreach (var key in ((IDictionary<string, object>)pm.Properties).Keys)
					page.Properties.Add(new Property() { Name = key, Value = 
						((string)((IDictionary<string, object>)pm.Properties)[key]).ToString() }) ;

				return page ;
			}
			return null ;
		}
	}
}
