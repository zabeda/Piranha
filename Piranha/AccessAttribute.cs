using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha
{
	/// <summary>
	/// The access attribute can be used to mark which permission is needed
	/// to execute a method that is invoked through the core method bindind.
	/// 
	/// If the current user does not has access, he/she is redirected to the
	/// first valid value of:
	/// 
	/// 1: The provided redirect url.
	/// 2: The login url specified in the param LOGIN_URL.
	/// 3: The site root if none of the above applies.
	/// </summary>
	public class AccessAttribute : Attribute
	{
		/// <summary>
		/// The permission rule.
		/// </summary>
		public string Function { get ; set ; }

		/// <summary>
		/// The optional url to redirect to if the user does not have
		/// the specified permission.
		/// </summary>
		public string RedirectUrl { get ; set ; }
	}
}
