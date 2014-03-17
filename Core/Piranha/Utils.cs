using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Piranha
{
	/// <summary>
	/// Helper class with utility methods
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Generates a slug of the given string
		/// </summary>
		/// <param name="str">The input string</param>
		/// <returns>The sluggified string</returns>
		public static string GenerateSlug(string str) { 
			var slug = Regex.Replace(str.ToLower()
				.Replace(" ", "-")
				.Replace("å", "a")
				.Replace("ä", "a")
				.Replace("á", "a")
				.Replace("à", "a")
				.Replace("ö", "o")
				.Replace("ó", "o")
				.Replace("ò", "o")
				.Replace("é", "e")
				.Replace("è", "e")
				.Replace("í", "i")
				.Replace("ì", "i"), @"[^a-z0-9-/]", "").Replace("--", "-");

			if (slug.EndsWith("-"))
				slug = slug.Substring(0, slug.LastIndexOf("-"));
			if (slug.StartsWith("-"))
				slug = slug.Substring(Math.Min(slug.IndexOf("-") + 1, slug.Length));
			return slug;
		}
	}
}