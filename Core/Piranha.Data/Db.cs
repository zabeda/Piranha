using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha
{
	public sealed class Db : DbContext
	{
		#region Properties
		public DbSet<Models.Category> Categories { get; set; }
		public DbSet<Models.Group> Groups { get; set; }
		public DbSet<Models.Log> Logs { get; set; }
		public DbSet<Models.Media> Media { get; set; }
		public DbSet<Models.Namespace> Namespaces { get; set; }
		public DbSet<Models.Page> Pages { get; set; }
		public DbSet<Models.PageRegion> PageRegions { get; set; }
		public DbSet<Models.PageType> PageTypes { get; set; }
		public DbSet<Models.PageTypeRegion> PageTypeRegions { get; set; }
		public DbSet<Models.Param> Params { get; set; }
		public DbSet<Models.Permalink> Permalinks { get; set; }
		public DbSet<Models.Permission> Permissions { get; set; }
		public DbSet<Models.Post> Posts { get; set; }
		public DbSet<Models.PostType> PostTypes { get; set; }
		public DbSet<Models.Property> Properties { get; set; }
		public DbSet<Models.Relation> Relations { get; set; }
		public DbSet<Models.SiteTree> SiteTrees { get; set; }
		public DbSet<Models.User> Users { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="nameOrConnectionString">The name of, or connection string</param>
		public Db(string nameOrConnectionString = "piranha") : base(nameOrConnectionString) { }

		/// <summary>
		/// Configures the data model.
		/// </summary>
		/// <param name="mb">The current model builder</param>
		protected override void OnModelCreating(DbModelBuilder mb) {
			#region Category
			mb.Entity<Models.Category>().ToTable("category");
			mb.Entity<Models.Category>().Property(c => c.Id).HasColumnName("category_id");
			mb.Entity<Models.Category>().Property(c => c.ParentId).HasColumnName("category_parent_id");
			mb.Entity<Models.Category>().Property(c => c.PermalinkId).HasColumnName("category_permalink_id");
			mb.Entity<Models.Category>().Property(c => c.Name).HasColumnName("category_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.Category>().Property(c => c.Description).HasColumnName("category_description").HasMaxLength(255);
			mb.Entity<Models.Category>().Property(c => c.Created).HasColumnName("category_created");
			mb.Entity<Models.Category>().Property(c => c.Updated).HasColumnName("category_updated");
			mb.Entity<Models.Category>().Property(c => c.CreatedById).HasColumnName("category_created_by");
			mb.Entity<Models.Category>().Property(c => c.UpdatedById).HasColumnName("category_updated_by");
			mb.Entity<Models.Category>().HasRequired(c => c.Permalink).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Category>().HasRequired(c => c.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Category>().HasRequired(c => c.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Group
			mb.Entity<Models.Group>().ToTable("sysgroup");
			mb.Entity<Models.Group>().Property(g => g.Id).HasColumnName("sysgroup_id");
			mb.Entity<Models.Group>().Property(g => g.ParentId).HasColumnName("sysgroup_parent_id");
			mb.Entity<Models.Group>().Property(g => g.Name).HasColumnName("sysgroup_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.Group>().Property(g => g.Description).HasColumnName("sysgroup_description").HasMaxLength(255);
			mb.Entity<Models.Group>().Property(g => g.Created).HasColumnName("sysgroup_created");
			mb.Entity<Models.Group>().Property(g => g.Updated).HasColumnName("sysgroup_updated");
			mb.Entity<Models.Group>().Property(g => g.CreatedById).HasColumnName("sysgroup_created_by");
			mb.Entity<Models.Group>().Property(g => g.UpdatedById).HasColumnName("sysgroup_updated_by");
			#endregion

			#region Log
			mb.Entity<Models.Log>().ToTable("syslog");
			mb.Entity<Models.Log>().Property(l => l.Id).HasColumnName("syslog_id");
			mb.Entity<Models.Log>().Property(l => l.ParentId).HasColumnName("syslog_parent_id");
			mb.Entity<Models.Log>().Property(l => l.Type).HasColumnName("syslog_parent_type").HasMaxLength(128).IsRequired();
			mb.Entity<Models.Log>().Property(l => l.Action).HasColumnName("syslog_action").HasMaxLength(64).IsRequired();
			mb.Entity<Models.Log>().Property(l => l.Created).HasColumnName("syslog_created");
			mb.Entity<Models.Log>().Property(l => l.Updated).HasColumnName("syslog_updated");
			mb.Entity<Models.Log>().Property(l => l.CreatedById).HasColumnName("syslog_created_by");
			mb.Entity<Models.Log>().Property(l => l.UpdatedById).HasColumnName("syslog_updated_by");
			mb.Entity<Models.Log>().HasRequired(l => l.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Log>().HasRequired(l => l.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Media
			mb.Entity<Models.Media>().ToTable("content");
			mb.Entity<Models.Media>().HasKey(m => new { m.Id, m.IsDraft });
			mb.Entity<Models.Media>().Property(m => m.Id).HasColumnName("content_id");
			mb.Entity<Models.Media>().Property(m => m.IsDraft).HasColumnName("content_draft");
			mb.Entity<Models.Media>().Property(m => m.ParentId).HasColumnName("content_parent_id");
			mb.Entity<Models.Media>().Property(m => m.PermalinkId).HasColumnName("content_permalink_id");
			mb.Entity<Models.Media>().Property(m => m.Filename).HasColumnName("content_filename").HasMaxLength(128);
			mb.Entity<Models.Media>().Property(m => m.OriginalUrl).HasColumnName("content_url").HasMaxLength(255);
			mb.Entity<Models.Media>().Property(m => m.LastSynced).HasColumnName("content_synced");
			mb.Entity<Models.Media>().Property(m => m.Name).HasColumnName("content_name").HasMaxLength(128);
			mb.Entity<Models.Media>().Property(m => m.ContentType).HasColumnName("content_type").HasMaxLength(255);
			mb.Entity<Models.Media>().Property(m => m.Size).HasColumnName("content_size");
			mb.Entity<Models.Media>().Property(m => m.IsImage).HasColumnName("content_image");
			mb.Entity<Models.Media>().Property(m => m.IsFolder).HasColumnName("content_folder");
			mb.Entity<Models.Media>().Property(m => m.Width).HasColumnName("content_width");
			mb.Entity<Models.Media>().Property(m => m.Height).HasColumnName("content_height");
			mb.Entity<Models.Media>().Property(m => m.AltDescription).HasColumnName("content_alt").HasMaxLength(128);
			mb.Entity<Models.Media>().Property(m => m.Description).HasColumnName("content_description").HasMaxLength(255);
			mb.Entity<Models.Media>().Property(m => m.Published).HasColumnName("content_published");
			mb.Entity<Models.Media>().Property(m => m.LastPublished).HasColumnName("content_last_published");
			mb.Entity<Models.Media>().Property(m => m.Created).HasColumnName("content_created");
			mb.Entity<Models.Media>().Property(m => m.Updated).HasColumnName("content_updated");
			mb.Entity<Models.Media>().Property(m => m.CreatedById).HasColumnName("content_created_by");
			mb.Entity<Models.Media>().Property(m => m.UpdatedById).HasColumnName("content_updated_by");
			mb.Entity<Models.Media>().HasOptional(m => m.Permalink).WithMany();
			mb.Entity<Models.Media>().HasRequired(m => m.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Media>().HasRequired(m => m.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Namespace
			mb.Entity<Models.Namespace>().ToTable("namespace");
			mb.Entity<Models.Namespace>().Property(n => n.Id).HasColumnName("namespace_id");
			mb.Entity<Models.Namespace>().Property(n => n.InternalId).HasColumnName("namespace_internal_id").HasMaxLength(32).IsRequired();
			mb.Entity<Models.Namespace>().Property(n => n.Name).HasColumnName("namespace_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.Namespace>().Property(n => n.Description).HasColumnName("namespace_description").HasMaxLength(255);
			mb.Entity<Models.Namespace>().Property(n => n.Created).HasColumnName("namespace_created");
			mb.Entity<Models.Namespace>().Property(n => n.Updated).HasColumnName("namespace_updated");
			mb.Entity<Models.Namespace>().Property(n => n.CreatedById).HasColumnName("namespace_created_by");
			mb.Entity<Models.Namespace>().Property(n => n.UpdatedById).HasColumnName("namespace_updated_by");
			mb.Entity<Models.Namespace>().HasRequired(n => n.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Namespace>().HasRequired(n => n.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Page
			mb.Entity<Models.Page>().ToTable("page");
			mb.Entity<Models.Page>().HasKey(p => new { p.Id, p.IsDraft });
			mb.Entity<Models.Page>().Property(p => p.Id).HasColumnName("page_id");
			mb.Entity<Models.Page>().Property(p => p.IsDraft).HasColumnName("page_draft");
			mb.Entity<Models.Page>().Property(p => p.SiteId).HasColumnName("page_sitetree_id");
			mb.Entity<Models.Page>().Property(p => p.OriginalId).HasColumnName("page_original_id");
			mb.Entity<Models.Page>().Property(p => p.TypeId).HasColumnName("page_template_id");
			mb.Entity<Models.Page>().Property(p => p.PermalinkId).HasColumnName("page_permalink_id");
			mb.Entity<Models.Page>().Property(p => p.GroupId).HasColumnName("page_group_id");
			mb.Entity<Models.Page>().Property(p => p.DisabledGroups).HasColumnName("page_group_disabled_id");
			mb.Entity<Models.Page>().Property(p => p.ParentId).HasColumnName("page_parent_id");
			mb.Entity<Models.Page>().Property(p => p.SortOrder).HasColumnName("page_seqno");
			mb.Entity<Models.Page>().Property(p => p.Title).HasColumnName("page_title").HasMaxLength(128).IsRequired();
			mb.Entity<Models.Page>().Property(p => p.NavigationTitle).HasColumnName("page_navigation_title").HasMaxLength(128);
			mb.Entity<Models.Page>().Property(p => p.IsHidden).HasColumnName("page_is_hidden");
			mb.Entity<Models.Page>().Property(p => p.Keywords).HasColumnName("page_keywords").HasMaxLength(128);
			mb.Entity<Models.Page>().Property(p => p.Description).HasColumnName("page_description").HasMaxLength(255);
			mb.Entity<Models.Page>().Property(p => p.PageAttachments).HasColumnName("page_attachments");
			mb.Entity<Models.Page>().Property(p => p.Route).HasColumnName("page_controller").HasMaxLength(128);
			mb.Entity<Models.Page>().Property(p => p.View).HasColumnName("page_view").HasMaxLength(128);
			mb.Entity<Models.Page>().Property(p => p.Redirect).HasColumnName("page_redirect").HasMaxLength(128);
			mb.Entity<Models.Page>().Property(p => p.Published).HasColumnName("page_published");
			mb.Entity<Models.Page>().Property(p => p.LastPublished).HasColumnName("page_last_published");
			mb.Entity<Models.Page>().Property(p => p.LastModified).HasColumnName("page_last_modified");
			mb.Entity<Models.Page>().Property(p => p.Created).HasColumnName("page_created");
			mb.Entity<Models.Page>().Property(p => p.Updated).HasColumnName("page_updated");
			mb.Entity<Models.Page>().Property(p => p.CreatedById).HasColumnName("page_created_by");
			mb.Entity<Models.Page>().Property(p => p.UpdatedById).HasColumnName("page_updated_by");
			mb.Entity<Models.Page>().HasRequired(p => p.Type).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Page>().HasRequired(p => p.Permalink).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Page>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Page>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region PageRegion
			mb.Entity<Models.PageRegion>().ToTable("region");
			mb.Entity<Models.PageRegion>().HasKey(r => new { r.Id, r.IsDraft });
			mb.Entity<Models.PageRegion>().Property(r => r.Id).HasColumnName("region_id");
			mb.Entity<Models.PageRegion>().Property(r => r.IsDraft).HasColumnName("region_draft");
			mb.Entity<Models.PageRegion>().Property(r => r.PageId).HasColumnName("region_page_id");
			mb.Entity<Models.PageRegion>().Property(r => r.PageIsDraft).HasColumnName("region_page_draft");
			mb.Entity<Models.PageRegion>().Property(r => r.RegionTypeId).HasColumnName("region_regiontemplate_id");
			mb.Entity<Models.PageRegion>().Property(r => r.Name).HasColumnName("region_name").HasMaxLength(64);
			mb.Entity<Models.PageRegion>().Property(r => r.Body).HasColumnName("region_body");
			mb.Entity<Models.PageRegion>().Property(r => r.Created).HasColumnName("region_created");
			mb.Entity<Models.PageRegion>().Property(r => r.Updated).HasColumnName("region_updated");
			mb.Entity<Models.PageRegion>().Property(r => r.CreatedById).HasColumnName("region_created_by");
			mb.Entity<Models.PageRegion>().Property(r => r.UpdatedById).HasColumnName("region_updated_by");
			mb.Entity<Models.PageRegion>().HasRequired(r => r.Page).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.PageRegion>().HasRequired(r => r.RegionType).WithMany();
			mb.Entity<Models.PageRegion>().HasRequired(r => r.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.PageRegion>().HasRequired(r => r.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region PageType
			mb.Entity<Models.PageType>().ToTable("pagetemplate");
			mb.Entity<Models.PageType>().Property(p => p.Id).HasColumnName("pagetemplate_id");
			mb.Entity<Models.PageType>().Property(p => p.Name).HasColumnName("pagetemplate_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.PageType>().Property(p => p.Description).HasColumnName("pagetemplate_description").HasMaxLength(255);
			mb.Entity<Models.PageType>().Property(p => p.VisualGuide).HasColumnName("pagetemplate_preview");
			mb.Entity<Models.PageType>().Property(p => p.PageRegions).HasColumnName("pagetemplate_page_regions");
			mb.Entity<Models.PageType>().Property(p => p.PageProperties).HasColumnName("pagetemplate_properties");
			mb.Entity<Models.PageType>().Property(p => p.Route).HasColumnName("pagetemplate_controller").HasMaxLength(128);
			mb.Entity<Models.PageType>().Property(p => p.IsRouteVirtual).HasColumnName("pagetemplate_controller_show");
			mb.Entity<Models.PageType>().Property(p => p.View).HasColumnName("pagetemplate_view").HasMaxLength(128);
			mb.Entity<Models.PageType>().Property(p => p.IsViewVirtual).HasColumnName("pagetemplate_view_show");
			mb.Entity<Models.PageType>().Property(p => p.Redirect).HasColumnName("pagetemplate_redirect").HasMaxLength(128);
			mb.Entity<Models.PageType>().Property(p => p.IsRedirectVirtual).HasColumnName("pagetemplate_redirect_show");
			mb.Entity<Models.PageType>().Property(p => p.IsSite).HasColumnName("pagetemplate_site_template");
			mb.Entity<Models.PageType>().Property(p => p.CLRType).HasColumnName("pagetemplate_type").HasMaxLength(255);
			mb.Entity<Models.PageType>().Property(p => p.Created).HasColumnName("pagetemplate_created");
			mb.Entity<Models.PageType>().Property(p => p.Updated).HasColumnName("pagetemplate_updated");
			mb.Entity<Models.PageType>().Property(p => p.CreatedById).HasColumnName("pagetemplate_created_by");
			mb.Entity<Models.PageType>().Property(p => p.UpdatedById).HasColumnName("pagetemplate_updated_by");
			mb.Entity<Models.PageType>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.PageType>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region PageTypeRegion
			mb.Entity<Models.PageTypeRegion>().ToTable("regiontemplate");
			mb.Entity<Models.PageTypeRegion>().Property(r => r.Id).HasColumnName("regiontemplate_id");
			mb.Entity<Models.PageTypeRegion>().Property(r => r.PageTypeId).HasColumnName("regiontemplate_template_id");
			mb.Entity<Models.PageTypeRegion>().Property(r => r.InternalId).HasColumnName("regiontemplate_internal_id").HasMaxLength(32).IsRequired();
			mb.Entity<Models.PageTypeRegion>().Property(r => r.SortOrder).HasColumnName("regiontemplate_seqno");
			mb.Entity<Models.PageTypeRegion>().Property(r => r.Name).HasColumnName("regiontemplate_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.PageTypeRegion>().Property(r => r.Description).HasColumnName("regiontemplate_description").HasMaxLength(255);
			mb.Entity<Models.PageTypeRegion>().Property(r => r.CLRType).HasColumnName("regiontemplate_type").HasMaxLength(255).IsRequired();
			mb.Entity<Models.PageTypeRegion>().Property(r => r.Created).HasColumnName("regiontemplate_created");
			mb.Entity<Models.PageTypeRegion>().Property(r => r.Updated).HasColumnName("regiontemplate_updated");
			mb.Entity<Models.PageTypeRegion>().Property(r => r.CreatedById).HasColumnName("regiontemplate_created_by");
			mb.Entity<Models.PageTypeRegion>().Property(r => r.UpdatedById).HasColumnName("regiontemplate_updated_by");
			mb.Entity<Models.PageTypeRegion>().HasRequired(r => r.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.PageTypeRegion>().HasRequired(r => r.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Param
			mb.Entity<Models.Param>().ToTable("sysparam");
			mb.Entity<Models.Param>().Property(p => p.Id).HasColumnName("sysparam_id");
			mb.Entity<Models.Param>().Property(p => p.Name).HasColumnName("sysparam_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.Param>().Property(p => p.Description).HasColumnName("sysparam_description").HasMaxLength(255);
			mb.Entity<Models.Param>().Property(p => p.Value).HasColumnName("sysparam_value").HasMaxLength(128);
			mb.Entity<Models.Param>().Property(p => p.IsSystem).HasColumnName("sysparam_locked");
			mb.Entity<Models.Param>().Property(p => p.Created).HasColumnName("sysparam_created");
			mb.Entity<Models.Param>().Property(p => p.Updated).HasColumnName("sysparam_updated");
			mb.Entity<Models.Param>().Property(p => p.CreatedById).HasColumnName("sysparam_created_by");
			mb.Entity<Models.Param>().Property(p => p.UpdatedById).HasColumnName("sysparam_updated_by");
			#endregion

			#region Permalink
			mb.Entity<Models.Permalink>().ToTable("permalink");
			mb.Entity<Models.Permalink>().Property(p => p.Id).HasColumnName("permalink_id");
			mb.Entity<Models.Permalink>().Property(p => p.NamespaceId).HasColumnName("permalink_namespace_id")
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("index_permalink_name") {
					IsUnique = true,
					Order = 1
				}));
			mb.Entity<Models.Permalink>().Property(p => p.Type).HasColumnName("permalink_type").HasMaxLength(16).IsRequired();
			mb.Entity<Models.Permalink>().Property(p => p.Name).HasColumnName("permalink_name").HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("index_permalink_name") {
					Order = 2
				}));
			mb.Entity<Models.Permalink>().Property(p => p.Created).HasColumnName("permalink_created");
			mb.Entity<Models.Permalink>().Property(p => p.Updated).HasColumnName("permalink_updated");
			mb.Entity<Models.Permalink>().Property(p => p.CreatedById).HasColumnName("permalink_created_by");
			mb.Entity<Models.Permalink>().Property(p => p.UpdatedById).HasColumnName("permalink_updated_by");
			mb.Entity<Models.Permalink>().HasRequired(p => p.Namespace).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Permission
			mb.Entity<Models.Permission>().ToTable("sysaccess");
			mb.Entity<Models.Permission>().Property(p => p.Id).HasColumnName("sysaccess_id");
			mb.Entity<Models.Permission>().Property(p => p.GroupId).HasColumnName("sysaccess_group_id");
			mb.Entity<Models.Permission>().Property(p => p.Name).HasColumnName("sysaccess_function").HasMaxLength(64).IsRequired();
			mb.Entity<Models.Permission>().Property(p => p.Description).HasColumnName("sysaccess_description").HasMaxLength(255);
			mb.Entity<Models.Permission>().Property(p => p.IsSystem).HasColumnName("sysaccess_locked");
			mb.Entity<Models.Permission>().Property(p => p.Created).HasColumnName("sysaccess_created");
			mb.Entity<Models.Permission>().Property(p => p.Updated).HasColumnName("sysaccess_updated");
			mb.Entity<Models.Permission>().Property(p => p.CreatedById).HasColumnName("sysaccess_created_by");
			mb.Entity<Models.Permission>().Property(p => p.UpdatedById).HasColumnName("sysaccess_updated_by");
			#endregion

			#region Post
			mb.Entity<Models.Post>().ToTable("post");
			mb.Entity<Models.Post>().HasKey(p => new { p.Id, p.IsDraft });
			mb.Entity<Models.Post>().Property(p => p.Id).HasColumnName("post_id");
			mb.Entity<Models.Post>().Property(p => p.IsDraft).HasColumnName("post_draft");
			mb.Entity<Models.Post>().Property(p => p.TypeId).HasColumnName("post_template_id");
			mb.Entity<Models.Post>().Property(p => p.PermalinkId).HasColumnName("post_permalink_id");
			mb.Entity<Models.Post>().Property(p => p.Title).HasColumnName("post_title").HasMaxLength(128).IsRequired();
			mb.Entity<Models.Post>().Property(p => p.Keywords).HasColumnName("post_keywords").HasMaxLength(128);
			mb.Entity<Models.Post>().Property(p => p.Description).HasColumnName("post_description").HasMaxLength(255);
			mb.Entity<Models.Post>().Property(p => p.IncludeInFeed).HasColumnName("post_rss");
			mb.Entity<Models.Post>().Property(p => p.Excerpt).HasColumnName("post_excerpt").HasMaxLength(255);
			mb.Entity<Models.Post>().Property(p => p.Body).HasColumnName("post_body");
			mb.Entity<Models.Post>().Property(p => p.PostAttchments).HasColumnName("post_attachments");
			mb.Entity<Models.Post>().Property(p => p.Route).HasColumnName("post_controller").HasMaxLength(128);
			mb.Entity<Models.Post>().Property(p => p.View).HasColumnName("post_view").HasMaxLength(128);
			mb.Entity<Models.Post>().Property(p => p.ArchiveRoute).HasColumnName("post_archive_controller").HasMaxLength(128);
			mb.Entity<Models.Post>().Property(p => p.Published).HasColumnName("post_published");
			mb.Entity<Models.Post>().Property(p => p.LastPublished).HasColumnName("post_last_published");
			mb.Entity<Models.Post>().Property(p => p.LastModified).HasColumnName("post_last_modified");
			mb.Entity<Models.Post>().Property(p => p.Created).HasColumnName("post_created");
			mb.Entity<Models.Post>().Property(p => p.Updated).HasColumnName("post_updated");
			mb.Entity<Models.Post>().Property(p => p.CreatedById).HasColumnName("post_created_by");
			mb.Entity<Models.Post>().Property(p => p.UpdatedById).HasColumnName("post_updated_by");
			mb.Entity<Models.Post>().HasRequired(p => p.Type).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Post>().HasRequired(p => p.Permalink).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Post>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Post>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region PostType
			mb.Entity<Models.PostType>().ToTable("posttemplate");
			mb.Entity<Models.PostType>().Property(p => p.Id).HasColumnName("posttemplate_id");
			mb.Entity<Models.PostType>().Property(p => p.PermalinkId).HasColumnName("posttemplate_permalink_id");
			mb.Entity<Models.PostType>().Property(p => p.Name).HasColumnName("posttemplate_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.PostType>().Property(p => p.Description).HasColumnName("posttemplate_description").HasMaxLength(255);
			mb.Entity<Models.PostType>().Property(p => p.VisualGuide).HasColumnName("posttemplate_preview");
			mb.Entity<Models.PostType>().Property(p => p.PostProperties).HasColumnName("posttemplate_properties");
			mb.Entity<Models.PostType>().Property(p => p.Route).HasColumnName("posttemplate_controller").HasMaxLength(128);
			mb.Entity<Models.PostType>().Property(p => p.IsRouteVirtual).HasColumnName("posttemplate_controller_show");
			mb.Entity<Models.PostType>().Property(p => p.View).HasColumnName("posttemplate_view").HasMaxLength(128);
			mb.Entity<Models.PostType>().Property(p => p.IsViewVirtual).HasColumnName("posttemplate_view_show");
			mb.Entity<Models.PostType>().Property(p => p.ArchiveRoute).HasColumnName("posttemplate_archive_controller").HasMaxLength(128);
			mb.Entity<Models.PostType>().Property(p => p.IsArchiveRouteVirtual).HasColumnName("posttemplate_archive_controller_show");
			mb.Entity<Models.PostType>().Property(p => p.EnableFeed).HasColumnName("posttemplate_rss");
			mb.Entity<Models.PostType>().Property(p => p.CLRType).HasColumnName("posttemplate_type").HasMaxLength(255);
			mb.Entity<Models.PostType>().Property(p => p.Created).HasColumnName("posttemplate_created");
			mb.Entity<Models.PostType>().Property(p => p.Updated).HasColumnName("posttemplate_updated");
			mb.Entity<Models.PostType>().Property(p => p.CreatedById).HasColumnName("posttemplate_created_by");
			mb.Entity<Models.PostType>().Property(p => p.UpdatedById).HasColumnName("posttemplate_updated_by");
			mb.Entity<Models.PostType>().HasOptional(p => p.Permalink).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.PostType>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.PostType>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Property
			mb.Entity<Models.Property>().ToTable("property");
			mb.Entity<Models.Property>().HasKey(p => new { p.Id, p.IsDraft });
			mb.Entity<Models.Property>().Property(p => p.Id).HasColumnName("property_id");
			mb.Entity<Models.Property>().Property(p => p.IsDraft).HasColumnName("property_draft");
			mb.Entity<Models.Property>().Property(p => p.ParentId).HasColumnName("property_parent_id");
			mb.Entity<Models.Property>().Property(p => p.Name).HasColumnName("property_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.Property>().Property(p => p.Value).HasColumnName("property_value");
			mb.Entity<Models.Property>().Property(p => p.Created).HasColumnName("property_created");
			mb.Entity<Models.Property>().Property(p => p.Updated).HasColumnName("property_updated");
			mb.Entity<Models.Property>().Property(p => p.CreatedById).HasColumnName("property_created_by");
			mb.Entity<Models.Property>().Property(p => p.UpdatedById).HasColumnName("property_updated_by");
			mb.Entity<Models.Property>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Property>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Relation
			mb.Entity<Models.Relation>().ToTable("relation");
			mb.Entity<Models.Relation>().HasKey(r => new { r.Id, r.IsDraft });
			mb.Entity<Models.Relation>().Property(r => r.Id).HasColumnName("relation_id");
			mb.Entity<Models.Relation>().Property(r => r.IsDraft).HasColumnName("relation_draft");
			mb.Entity<Models.Relation>().Property(r => r.Type).HasColumnName("relation_type").HasMaxLength(16).IsRequired();
			mb.Entity<Models.Relation>().Property(r => r.ModelId).HasColumnName("relation_data_id");
			mb.Entity<Models.Relation>().Property(r => r.RelatedId).HasColumnName("relation_related_id");
			#endregion

			#region SiteTree
			mb.Entity<Models.SiteTree>().ToTable("sitetree");
			mb.Entity<Models.SiteTree>().Property(s => s.Id).HasColumnName("sitetree_id");
			mb.Entity<Models.SiteTree>().Property(s => s.NamespaceId).HasColumnName("sitetree_namespace_id");
			mb.Entity<Models.SiteTree>().Property(s => s.InternalId).HasColumnName("sitetree_internal_id").HasMaxLength(32).IsRequired();
			mb.Entity<Models.SiteTree>().Property(s => s.Name).HasColumnName("sitetree_name").HasMaxLength(64).IsRequired();
			mb.Entity<Models.SiteTree>().Property(s => s.Description).HasColumnName("sitetree_description").HasMaxLength(255);
			mb.Entity<Models.SiteTree>().Property(s => s.MetaTitle).HasColumnName("sitetree_meta_title").HasMaxLength(128);
			mb.Entity<Models.SiteTree>().Property(s => s.MetaDescription).HasColumnName("sitetree_meta_description").HasMaxLength(255);
			mb.Entity<Models.SiteTree>().Property(s => s.Hostnames).HasColumnName("sitetree_hostnames");
			mb.Entity<Models.SiteTree>().Property(s => s.Created).HasColumnName("sitetree_created");
			mb.Entity<Models.SiteTree>().Property(s => s.Updated).HasColumnName("sitetree_updated");
			mb.Entity<Models.SiteTree>().Property(s => s.CreatedById).HasColumnName("sitetree_created_by");
			mb.Entity<Models.SiteTree>().Property(s => s.UpdatedById).HasColumnName("sitetree_updated_by");
			mb.Entity<Models.SiteTree>().HasRequired(s => s.Namespace).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.SiteTree>().HasRequired(s => s.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.SiteTree>().HasRequired(s => s.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region User
			mb.Entity<Models.User>().ToTable("sysuser");
			mb.Entity<Models.User>().Property(u => u.Id).HasColumnName("sysuser_id");
			mb.Entity<Models.User>().Property(u => u.GroupId).HasColumnName("sysuser_group_id");
			mb.Entity<Models.User>().Property(u => u.ApiKey).HasColumnName("sysuser_apikey");
			mb.Entity<Models.User>().Property(u => u.Username).HasColumnName("sysuser_login").HasMaxLength(64).IsRequired();
			mb.Entity<Models.User>().Property(u => u.Password).HasColumnName("sysuser_password").HasMaxLength(64);
			mb.Entity<Models.User>().Property(u => u.Firstname).HasColumnName("sysuser_firstname").HasMaxLength(128);
			mb.Entity<Models.User>().Property(u => u.Lastname).HasColumnName("sysuser_surname").HasMaxLength(128);
			mb.Entity<Models.User>().Property(u => u.Email).HasColumnName("sysuser_email").HasMaxLength(128);
			mb.Entity<Models.User>().Property(u => u.Culture).HasColumnName("sysuser_culture").HasMaxLength(5);
			mb.Entity<Models.User>().Property(u => u.LatestLogin).HasColumnName("sysuser_last_login");
			mb.Entity<Models.User>().Property(u => u.PreviousLogin).HasColumnName("sysuser_prev_login");
			mb.Entity<Models.User>().Property(u => u.IsLocked).HasColumnName("sysuser_locked");
			mb.Entity<Models.User>().Property(u => u.LockedUntil).HasColumnName("sysuser_locked_until");
			mb.Entity<Models.User>().Property(u => u.Created).HasColumnName("sysuser_created");
			mb.Entity<Models.User>().Property(u => u.Updated).HasColumnName("sysuser_updated");
			mb.Entity<Models.User>().Property(u => u.CreatedById).HasColumnName("sysuser_created_by");
			mb.Entity<Models.User>().Property(u => u.UpdatedById).HasColumnName("sysuser_updated_by");
			#endregion

			base.OnModelCreating(mb);
		}
	}
}
