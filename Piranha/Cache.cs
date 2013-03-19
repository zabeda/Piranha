using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Piranha
{
	/// <summary>
	/// The Piranha cache object.
	/// </summary>
	public static class Cache
	{
		#region Members
		/// <summary>
		/// The internal cache provider.
		/// </summary>
		private static ICacheProvider provider = null ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the currently active cache provider.
		/// </summary>
		public static ICacheProvider Current { 
			get { return EnsureProvider() ; }
		}
		#endregion

		/// <summary>
		/// Ensures that the provider is created and returns it.
		/// </summary>
		/// <returns>The current cache provider</returns>
		private static ICacheProvider EnsureProvider() {
			if (provider == null) {
				if (HttpContext.Current != null)
					provider = new WebCache() ;
				else provider = new MemCache() ;
			}
			return provider ;
		}
	}

	/// <summary>
	/// Interface defining a cache provider
	/// </summary>
	public interface ICacheProvider 
	{
		/// <summary>
		/// Gets/sets the object with the given id.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>The cached object</returns>
		object this[string key] { get ; set ; }

		/// <summary>
		/// Removes the object with the given key from the cache.
		/// </summary>
		/// <param name="key">The key</param>
		void Remove(string key) ;

		/// <summary>
		/// Checks if an object exists for the given key.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>Whether an object with the given key exists.</returns>
		bool Contains(string key) ;
	}

	/// <summary>
	/// Cache provider for web applications.
	/// </summary>
	internal class WebCache : ICacheProvider
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

	/// <summary>
	/// Cache provider for keeping objects in memory.
	/// </summary>
	internal class MemCache : ICacheProvider
	{
		#region Members
		private Dictionary<string,object> Cache = new Dictionary<string,object>() ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the cache object with the given key.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>The cached value</returns>
		public object this[string key] {
			get { return Cache.ContainsKey(key) ? Cache[key] : null ; }
			set { Cache[key] = value ; }
		}
		#endregion

		/// <summary>
		/// Removes the object with the given key from the cache.
		/// </summary>
		/// <param name="key">The key</param>
		public void Remove(string key) {
			if (Cache.ContainsKey(key))
				Cache.Remove(key) ;
		}

		/// <summary>
		/// Checks if an object exists for the given key.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>Whether an object with the given key exists.</returns>
		public bool Contains(string key) {
			return Cache.ContainsKey(key) ;
		}
	}
}