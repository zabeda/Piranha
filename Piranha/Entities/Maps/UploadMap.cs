using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Piranha.Entities.Maps
{
	/// <summary>
	/// Entity map for the upload entity.
	/// </summary>
	public class UploadMap : EntityTypeConfiguration<Upload>
	{
		public UploadMap() {
			ToTable("upload") ;

			Property(u => u.Id).HasColumnName("upload_id") ;
			Property(u => u.ParentId).HasColumnName("upload_parent_id") ;
			Property(u => u.Filename).HasColumnName("upload_filename").IsRequired().HasMaxLength(128) ;
			Property(u => u.Type).HasColumnName("upload_type").IsRequired().HasMaxLength(64) ;
			Property(u => u.Created).HasColumnName("upload_created") ;
			Property(u => u.Updated).HasColumnName("upload_updated") ;
			Property(u => u.CreatedById).HasColumnName("upload_created_by") ;
			Property(u => u.UpdatedById).HasColumnName("upload_updated_by") ;

			HasRequired(u => u.CreatedBy).WithRequiredDependent() ;
			HasRequired(u => u.UpdatedBy).WithRequiredDependent() ;

			HasMany(u => u.Comments).WithRequired().HasForeignKey(c => c.ParentId) ;

			Ignore(u => u.VirtualPath) ;
			Ignore(u => u.PhysicalPath) ;
		}
	}
}
