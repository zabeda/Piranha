using System;

namespace Piranha.Models
{
	public sealed class Param : IModel, IModified
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set ; }
		public string Value { get; set; }
		public bool IsSystem { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid? CreatedById { get; set; }
		public Guid? UpdatedById { get; set; }

		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
