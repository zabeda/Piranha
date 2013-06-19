using System;
using System.Web;

namespace Piranha.Cache
{
	/// <summary>
	/// Basic web cache provider.
	/// </summary>
	public class WebCacheProvider : ICacheProvider
	{
		#region Properties
		/// <summary>
		/// Gets/sets the cache object with the given key.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>The cached value</returns>
		public object this[string key] {
			get { return HttpContext.Current.Cache[key] ; }
			set {
				if (value != null) {
					var param = Models.SysParam.GetByName("CACHE_SERVER_EXPIRES") ;

					if (param == null || param.Value == "0")
						HttpContext.Current.Cache[key] = value ;
					else HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(Convert.ToInt32(param.Value)), System.Web.Caching.Cache.NoSlidingExpiration) ; 
				} else {
					HttpContext.Current.Cache.Remove(key) ;
				}
			}
		}
		#endregion

		/// <summary>
		/// Removes the object with the given key from the cache.
		/// </summary>
		/// <param name="key">The key</param>
		public void Remove(string key) {
			HttpContext.Current.Cache.Remove(key) ;
		}

		/// <summary>
		/// Checks if an object exists for the given key.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>Whether an object with the given key exists.</returns>
		public bool Contains(string key) {
			return HttpContext.Current.Cache[key] != null ;
		}
	}
}