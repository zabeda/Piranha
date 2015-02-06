using System;

namespace Piranha.Models
{
	public sealed class SiteTree : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public Guid NamespaceId { get; set; }
		public string InternalId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public string Hostnames { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public Namespace Namespace { get; set; }
		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
