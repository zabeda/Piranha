using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.WebPages
{
	/// <summary>
	/// Attribute used to restrict a method to only be available in
	/// HTTP POST requests.
	/// </summary>
	public sealed class HttpPostAttribute : Attribute {}
}