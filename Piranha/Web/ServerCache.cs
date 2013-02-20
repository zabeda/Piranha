using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Caching;

namespace Piranha.Web
{
	/// <summary>
	/// Cache wrapper.
	/// </summary>
	public class ServerCache
	{
		/// <summary>
		/// Gets/sets the cache object with the given key.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>The cached value</returns>
		public object this[string key] {
			get { return HttpContext.Current.Cache[key] ; }
			set { HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(5), Cache.NoSlidingExpiration) ; }
		}
	}
}