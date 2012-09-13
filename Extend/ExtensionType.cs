using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Extend
{
	/// <summary>
	/// Enum defining the different types of extensions.
	/// </summary>
	public enum ExtensionType
	{
		NotSet,
		Region,
		Property,
		User,
		Group,
		Page,
		Post,
		Category,
		Media
	}
}