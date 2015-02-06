using System;

namespace Piranha.Models
{
	public sealed class Relation : IModel
	{
		public Guid Id { get; set; }
		public bool IsDraft { get; set; }
		public string Type { get; set; }
		public Guid ModelId { get; set; }
		public Guid RelatedId { get; set; }
	}
}
