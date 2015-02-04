using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.ServiceModel.Web;
using System.Text;
using System.Linq;
using System.Web.Script.Serialization;

namespace Piranha.Rest
{
	public abstract class BaseService
	{
		protected Stream Serialize(object obj) {
			var js = new JavaScriptSerializer() ;
			js.RegisterConverters(new List<JavaScriptConverter>() { new ExpandoObjectSerializer() }) ;
			var mem = new MemoryStream(Encoding.UTF8.GetBytes(js.Serialize(obj))) ;
			WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8" ;
			return mem ;
		}
	}
}