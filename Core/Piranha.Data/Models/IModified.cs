using System;

namespace Piranha.Models
{
	public interface IModified
	{
		DateTime Created { get; set; }
		DateTime Updated { get; set; }
	}
}
