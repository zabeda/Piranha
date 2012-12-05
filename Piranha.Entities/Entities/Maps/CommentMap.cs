using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Piranha.Entities.Maps
{
	/// <summary>
	/// Entity map for the comment.
	/// </summary>
	public class CommentMap : EntityTypeConfiguration<Comment>
	{
		public CommentMap() {
			ToTable("comment") ;

			Property(c => c.Id).HasColumnName("comment_id") ;
			Property(c => c.ParentId).HasColumnName("comment_parent_id") ;
			Property(c => c.Approved).HasColumnName("comment_approved") ;
			Property(c => c.Title).HasColumnName("comment_title") ;
			Property(c => c.Body).HasColumnName("comment_body").IsRequired() ;
			Property(c => c.AuthorName).HasColumnName("comment_author_name") ;
			Property(c => c.AuthorEmail).HasColumnName("comment_author_email") ;
			Property(c => c.Created).HasColumnName("comment_created").IsRequired() ;
			Property(c => c.Updated).HasColumnName("comment_updated").IsRequired() ;
			Property(c => c.CreatedById).HasColumnName("comment_created_by") ;
			Property(c => c.UpdatedById).HasColumnName("comment_updated_by") ;
		}
	}
}
