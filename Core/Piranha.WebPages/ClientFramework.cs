using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using Piranha.Web;

namespace Piranha.WebPages
{
	/// <summary>
	/// Client framework class for ASP.NET Web Pages.
	/// </summary>
	[Export(typeof(IClientFramework))]
	public class ClientFramework : IClientFramework
	{
		/// <summary>
		/// Gets the current framework type.
		/// </summary>
		public FrameworkType Type {
			get { return FrameworkType.WebPages ; }
		}
	}
}
