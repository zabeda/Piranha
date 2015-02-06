using System;

namespace Piranha.Models
{
	public sealed class Post : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public bool IsDraft { get; set; }
		public Guid TypeId { get; set; }
		public Guid PermalinkId { get; set; }
		public string Title { get; set; }
		public string Keywords { get; set; }
		public string Description { get; set; }
		public bool IncludeInFeed { get; set; }
		public string Excerpt { get; set; }
		public string Body { get; set; }
		public string PostAttchments { get; set; }
		public string Route { get; set; }
		public string View { get; set; }
		public string ArchiveRoute { get; set; }
		public DateTime? Published { get; set; }
		public DateTime? LastPublished { get; set; }
		public DateTime? LastModified { get; set; }
		public DateTime Created { get; set;	}
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public PostType Type { get; set; }
		public Permalink Permalink { get; set; }
		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }

		public Post() {
			IncludeInFeed = true;
		}
	}
}
