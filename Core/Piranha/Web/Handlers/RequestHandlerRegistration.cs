using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Web.Handlers
{
	public class RequestHandlerRegistration
	{
		/// <summary>
		/// Gets/sets the url prefix.
		/// </summary>
		public string UrlPrefix { get ; set ; } 

		/// <summary>
		/// Gets/sets the handler id.
		/// </summary>
		public string Id { get ; set ; }

		/// <summary>
		/// Gets/sets the handler.
		/// </summary>
		public IRequestHandler Handler { get ; set ; }
	}
}
