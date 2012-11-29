using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Piranha.Web
{
	/// <summary>
	/// Attribute for marking a property to be automatically handled by the model loader.
	/// </summary>
	public class ModelPropertyAttribute : Attribute
	{
		/// <summary>
		/// Gets/sets the absolute expiration of the cache in minutes.
		/// </summary>
		public int AbsoluteExpiration { get ; set ; }

		/// <summary>
		/// Gets/sets the sliding expiration of the cache in minutes.
		/// </summary>
		public int SlidingExpiration { get ; set ; }

		/// <summary>
		/// Gets/sets the cache priority.
		/// </summary>
		public CacheItemPriority Priority { get ; set ; }

		/// <summary>
		/// Gets/sets the optional method name that should be executed to load the property data.
		/// </summary>
		public string OnLoad { get ; set ; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ModelPropertyAttribute() {
			Priority = CacheItemPriority.Normal ;
		}
	}
}