using System;

namespace Piranha.Models
{
	public sealed class Log : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public Guid ParentId { get; set; }
		public string Type { get; set; }
		public string Action { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
