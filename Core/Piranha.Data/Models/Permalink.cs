using System;

namespace Piranha.Models
{
	public sealed class Permalink : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public Guid NamespaceId { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public Namespace Namespace { get; set; }
	}
}
