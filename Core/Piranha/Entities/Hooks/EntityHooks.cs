/*
 * Copyright (c) 2011-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Entities.Hooks
{
	/// <summary>
	/// Hooks for entity & model savings to intercept the processing
	/// </summary>
	public static class EntityHooks
	{
		/// <summary>
		/// The different hooks available for the Permalink.
		/// </summary>
		public static class Permalink
		{
			/// <summary>
			/// Reformat Permalink to make changes
			/// </summary>
			public static Delegates.ReformatPermalink OverridePermalinkGenerate;
		}

	}
}
