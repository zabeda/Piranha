using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Piranha.Web
{
	/// <summary>
	/// Gravatar helper.
	/// </summary>
	public class GravatarHelper {
		/// <summary>
		/// Gets the image URL for the gravatar with the given email
		/// </summary>
		/// <param name="email">The gravatar email</param>
		/// <param name="size">Optional size</param>
		/// <returns>The image URL</returns>
		public IHtmlString Image(string email, int size = 0) {
			var crypto = MD5.Create();
			var data = crypto.ComputeHash(Encoding.UTF8.GetBytes(email));
			var hash = "" ;

			foreach (var b in data)
				hash += b.ToString("x2") ;

			return new HtmlString("http://www.gravatar.com/avatar/" + hash.ToLower() +
				(size > 0 ? "?s=" + size : "")) ;
		}
	}
}
