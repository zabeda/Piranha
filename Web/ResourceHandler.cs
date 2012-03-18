using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using Piranha.WebPages;
using Yahoo.Yui.Compressor;

namespace Piranha.Web
{
	public class ResourceHandler : IHttpHandler
	{
		/// <summary>
		/// Gets weather this handler is reusable or not.
		/// </summary>
		public bool IsReusable { 
			get { return false ; } 
		}

		/// <summary>
		/// Processes the http request.
		/// </summary>
		/// <param name="context">The current context</param>
		public void ProcessRequest(HttpContext context) {
			ResourcePathProvider res = new ResourcePathProvider() ;

			if (res.FileExists(context.Request.Path)) {
				var file = res.GetFile(context.Request.Path) ;

				DateTime mod = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime ;

				if (!WebPiranha.HandleClientCache(context, file.VirtualPath, mod)) {
					if (file.Name.EndsWith(".js")) {
						context.Response.ContentType = "text/javascript" ;
					} else if (file.Name.EndsWith(".css")) {
						context.Response.ContentType = "text/css" ;
					} else if (file.Name.EndsWith(".png")) {
						context.Response.ContentType = "image/png" ;
					} else if (file.Name.EndsWith(".eot")) {
						context.Response.ContentType = "application/vnd.ms-fontobject" ;
					} else if (file.Name.EndsWith(".ttf")) {
						context.Response.ContentType = "application/octet-stream" ;
					} else if (file.Name.EndsWith(".svg")) {
						context.Response.ContentType = "image/svg+xml" ;
					} else if (file.Name.EndsWith(".woff")) {
						context.Response.ContentType = "application/x-woff" ;
					}
					var stream = file.Open() ;
					byte[] bytes = new byte[stream.Length] ;

					stream.Read(bytes, 0, Convert.ToInt32(stream.Length)) ;
#if DEBUG
					context.Response.BinaryWrite(bytes) ;
#else
					context.Response.BinaryWrite(bytes) ;
#endif
					stream.Close() ;
				}
			}
		}
	}
}
