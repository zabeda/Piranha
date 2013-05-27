using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Entities;

namespace Piranha.Web
{
    public class FormController : Controller
    {
		#region Members
		private const string FORMBUILDER_NAME = "formbuilder_name" ;
		private const string FORMBUILDER_ID = "formbuilder_id" ;
		private const string FORMBUILDER_KEY = "formbuilder_key" ;
		#endregion

		[HttpPost()]
		public JsonResult Submit() {
			var name = Request.Form[FORMBUILDER_NAME] ;
			var key = Request.Form[FORMBUILDER_KEY] ;
			var id = new Guid(Request.Form[FORMBUILDER_ID]) ;

			if (key == Session.SessionID) {
				var region = Models.Region.GetSingle(id) ;

				if (region != null) {
					var body = (Extend.Regions.FormRegion)region.Body ;

					var form = new FormData() {
						Id = Guid.NewGuid(),
						Name = name,
						RegionId = region.Id,
						Created = DateTime.Now
					} ;
					if (Application.Current.UserProvider.IsAuthenticated)
						form.CreatedById = Application.Current.UserProvider.UserId ;

					foreach (var item in body.Items) {
						form.Items.Add(new FormItem() {
							Id = Guid.NewGuid(),
							FormId = form.Id,
							Name = item.Label,
							Type = item.Type,
							Value = Request.Form[item.Label]
						}) ;
					}

					return Json(form) ;
				} else {
					return Json("No region found") ;
				}
			} else {
				return Json("Failed session verification") ;
			}
		}
    }
}
