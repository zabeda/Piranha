using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Cache
{
	/// <summary>
	/// Dummy cache implementation for completely turning of server side
	/// memory caching
	/// </summary>
	public class NoCacheProvider : ICacheProvider
	{
		/// <summary>
		/// Gets/sets the cache object with the given key.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>The cached value</returns>
		public object this[string key] {
			get { return null ; }
			set {}
		}

		/// <summary>
		/// Removes the object with the given key from the cache.
		/// </summary>
		/// <param name="key">The key</param>
		public void Remove(string key) {}

		/// <summary>
		/// Checks if an object exists for the given key.
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>Whether an object with the given key exists.</returns>
		public bool Contains(string key) {
			return false ;
		}
	}
}