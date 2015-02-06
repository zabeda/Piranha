using System;

namespace Piranha.Models
{
	public sealed class Category : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public Guid? ParentId { get; set; }
		public Guid PermalinkId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public Permalink Permalink { get; set; }
		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
