using System;

namespace Piranha.Models
{
	public interface IModifiedBy
	{
		Guid CreatedById { get; set; }
		Guid UpdatedById { get; set; }
	}
}
