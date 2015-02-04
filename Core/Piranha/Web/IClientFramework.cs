using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piranha.Web
{
	/// <summary>
	/// Interface defining the currently active client framework. This is imported
	/// by the Piranha.Web.Application class.
	/// </summary>
	public interface IClientFramework
	{
		/// <summary>
		/// Gets the currently active framework.
		/// </summary>
		FrameworkType Type { get ; }
	}
}
