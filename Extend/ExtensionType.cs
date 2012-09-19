using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Extend
{
	/// <summary>
	/// Enum defining the different types of extensions.
	/// </summary>
	[Flags]
	public enum ExtensionType
	{
		NotSet = 0,
		Region = 1,
		User = 2,
		Group = 4,
		Page = 8,
		Post = 16,
		Category = 32,
		Media = 64
	}
}