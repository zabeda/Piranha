using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Piranha.Entities.Maps
{
	/// <summary>
	/// Entity map for the user.
	/// </summary>
	internal class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap() {
			ToTable("sysuser") ;

			Property(u => u.Id).HasColumnName("sysuser_id") ;
			Property(u => u.Login).HasColumnName("sysuser_login").IsRequired().HasMaxLength(64) ;
			Property(u => u.Password).HasColumnName("sysuser_password").HasMaxLength(64) ;
			Property(u => u.Firstname).HasColumnName("sysuser_firstname").HasMaxLength(128) ;
			Property(u => u.Surname).HasColumnName("sysuser_surname").HasMaxLength(128) ;
			Property(u => u.Email).HasColumnName("sysuser_email").HasMaxLength(128) ;
			Property(u => u.GroupId).HasColumnName("sysuser_group_id") ;
			Property(u => u.Culture).HasColumnName("sysuser_culture").HasMaxLength(5) ;
			Property(u => u.LastLogin).HasColumnName("sysuser_last_login") ;
			Property(u => u.PreviousLogin).HasColumnName("sysuser_prev_login") ;
			Property(u => u.IsLocked).HasColumnName("sysuser_locked") ;
			Property(u => u.LockedUntil).HasColumnName("sysuser_locked_until") ;
			Property(u => u.Created).HasColumnName("sysuser_created") ;
			Property(u => u.Updated).HasColumnName("sysuser_updated") ;
			Property(u => u.CreatedById).HasColumnName("sysuser_created_by") ;
			Property(u => u.UpdatedById).HasColumnName("sysuser_updated_by") ;
	
			HasOptional(u => u.Group) ;
			HasMany(u => u.Extensions).WithRequired().HasForeignKey(e => e.ParentId) ;
		}
	}
}
