/*
 * Copyright (c) 2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Piranha
{
	/// <summary>
	/// The main db context for interfacing with the database.
	/// </summary>
	public sealed class Db : DbContext
	{
		#region Db sets
		public DbSet<Data.Category> Categories { get; set; }
		public DbSet<Data.Comment> Comments { get; set; }
		public DbSet<Data.Extensions> Extensions { get; set; }
		public DbSet<Data.Group> Groups { get; set; }
		public DbSet<Data.Log> Logs { get; set; }
		public DbSet<Data.Media> Media { get; set; }
		public DbSet<Data.Namespace> Namespaces { get; set; }
		public DbSet<Data.Page> Pages { get; set; }
		public DbSet<Data.PageRegion> PageRegions { get; set; }
		public DbSet<Data.PageType> PageTypes { get; set; }
		public DbSet<Data.PageTypeRegion> PageTypeRegions { get; set; }
		public DbSet<Data.Param> Params { get; set; }
		public DbSet<Data.Permalink> Permalinks { get; set; }
		public DbSet<Data.Permission> Permissions { get; set; }
		public DbSet<Data.Post> Posts { get; set; }
		public DbSet<Data.PostType> PostTypes { get; set; }
		public DbSet<Data.Property> Properties { get; set; }
		public DbSet<Data.Relation> Relations { get; set; }
		public DbSet<Data.SiteTree> SiteTrees { get; set; }
		public DbSet<Data.Upload> Uploads { get; set; }
		public DbSet<Data.User> Users { get; set; }
		#endregion

		#region Properties
		/// <summary>
		/// Checks if the current context is compatible with 
		/// the database.
		/// </summary>
		public bool IsCompatible {
			get {
				return Database.CompatibleWithModel(true);
			}
		}

		/// <summary>
		/// Checks if the database for the context exists.
		/// </summary>
		public bool Exists {
			get {
				return Database.Exists();
			}
		}
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Db() : this("piranha") { }

		/// <summary>
		/// Creates a new context on the given connection string.
		/// </summary>
		/// <param name="nameOrConnectionString">The name of, or connection string</param>
		public Db(string nameOrConnectionString = "piranha") : base(nameOrConnectionString) { }

		/// <summary>
		/// Migrates the database to the latest migration.
		/// </summary>
		public void Migrate() {
			var migrator = new MigrateDatabaseToLatestVersion<Db, Migrations.Configuration>();
			migrator.InitializeDatabase(this);
		}

		/// <summary>
		/// Saves the changes made to the context.
		/// </summary>
		/// <returns>The number of saved changes</returns>
		public override int SaveChanges() {
			OnSave();
			return base.SaveChanges();
		}

		/// <summary>
		/// Saves the changes made to the context.
		/// </summary>
		/// <returns>The number of saved changes</returns>
		public override Task<int> SaveChangesAsync() {
			OnSave();
			return base.SaveChangesAsync();
		}

		/// <summary>
		/// Saves the changes made to the context.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of saved changes</returns>
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
			OnSave();
			return base.SaveChangesAsync(cancellationToken);
		}

		/// <summary>
		/// Configures the data model.
		/// </summary>
		/// <param name="mb">The current model builder</param>
		protected override void OnModelCreating(DbModelBuilder mb) {
			#region Category
			mb.Entity<Data.Category>().ToTable("category");
			mb.Entity<Data.Category>().Property(c => c.Id).HasColumnName("category_id");
			mb.Entity<Data.Category>().Property(c => c.ParentId).HasColumnName("category_parent_id");
			mb.Entity<Data.Category>().Property(c => c.PermalinkId).HasColumnName("category_permalink_id");
			mb.Entity<Data.Category>().Property(c => c.Name).HasColumnName("category_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.Category>().Property(c => c.Description).HasColumnName("category_description").HasMaxLength(255);
			mb.Entity<Data.Category>().Property(c => c.Created).HasColumnName("category_created");
			mb.Entity<Data.Category>().Property(c => c.Updated).HasColumnName("category_updated");
			mb.Entity<Data.Category>().Property(c => c.CreatedById).HasColumnName("category_created_by");
			mb.Entity<Data.Category>().Property(c => c.UpdatedById).HasColumnName("category_updated_by");
			mb.Entity<Data.Category>().HasRequired(c => c.Permalink).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Category>().HasRequired(c => c.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Category>().HasRequired(c => c.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Comment
			mb.Entity<Data.Comment>().ToTable("comment");
			mb.Entity<Data.Comment>().Property(c => c.Id).HasColumnName("comment_id");
			mb.Entity<Data.Comment>().Property(c => c.ParentId).HasColumnName("comment_parent_id");
			mb.Entity<Data.Comment>().Property(c => c.ParentIsDraft).HasColumnName("comment_parent_draft");
			mb.Entity<Data.Comment>().Property(c => c.Status).HasColumnName("comment_status");
			mb.Entity<Data.Comment>().Property(c => c.ReportedCount).HasColumnName("comment_reported_count");
			mb.Entity<Data.Comment>().Property(c => c.Title).HasColumnName("comment_title").HasMaxLength(64);
			mb.Entity<Data.Comment>().Property(c => c.Body).HasColumnName("comment_body");
			mb.Entity<Data.Comment>().Property(c => c.AuthorName).HasColumnName("comment_author_name").HasMaxLength(128);
			mb.Entity<Data.Comment>().Property(c => c.AuthorEmail).HasColumnName("comment_author_email").HasMaxLength(128);
			mb.Entity<Data.Comment>().Property(c => c.Created).HasColumnName("comment_created");
			mb.Entity<Data.Comment>().Property(c => c.Updated).HasColumnName("comment_updated");
			mb.Entity<Data.Comment>().Property(c => c.CreatedById).HasColumnName("comment_created_by");
			mb.Entity<Data.Comment>().Property(c => c.UpdatedById).HasColumnName("comment_updated_by");
			#endregion

			#region Extension
			mb.Entity<Data.Extensions>().ToTable("extension");
			mb.Entity<Data.Extensions>().HasKey(p => new { p.Id, p.IsDraft });
			mb.Entity<Data.Extensions>().Property(p => p.Id).HasColumnName("extension_id");
			mb.Entity<Data.Extensions>().Property(p => p.IsDraft).HasColumnName("extension_draft");
			mb.Entity<Data.Extensions>().Property(p => p.ParentId).HasColumnName("extension_parent_id");
			mb.Entity<Data.Extensions>().Property(p => p.Body).HasColumnName("extension_body");
			mb.Entity<Data.Extensions>().Property(p => p.CLRType).HasColumnName("extension_type").HasMaxLength(255).IsRequired();
			mb.Entity<Data.Extensions>().Property(p => p.Created).HasColumnName("extension_created");
			mb.Entity<Data.Extensions>().Property(p => p.Updated).HasColumnName("extension_updated");
			mb.Entity<Data.Extensions>().Property(p => p.CreatedById).HasColumnName("extension_created_by");
			mb.Entity<Data.Extensions>().Property(p => p.UpdatedById).HasColumnName("extension_updated_by");
			mb.Entity<Data.Extensions>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Extensions>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Group
			mb.Entity<Data.Group>().ToTable("sysgroup");
			mb.Entity<Data.Group>().Property(g => g.Id).HasColumnName("sysgroup_id");
			mb.Entity<Data.Group>().Property(g => g.ParentId).HasColumnName("sysgroup_parent_id");
			mb.Entity<Data.Group>().Property(g => g.Name).HasColumnName("sysgroup_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.Group>().Property(g => g.Description).HasColumnName("sysgroup_description").HasMaxLength(255);
			mb.Entity<Data.Group>().Property(g => g.Created).HasColumnName("sysgroup_created");
			mb.Entity<Data.Group>().Property(g => g.Updated).HasColumnName("sysgroup_updated");
			mb.Entity<Data.Group>().Property(g => g.CreatedById).HasColumnName("sysgroup_created_by");
			mb.Entity<Data.Group>().Property(g => g.UpdatedById).HasColumnName("sysgroup_updated_by");
			#endregion

			#region Log
			mb.Entity<Data.Log>().ToTable("syslog");
			mb.Entity<Data.Log>().Property(l => l.Id).HasColumnName("syslog_id");
			mb.Entity<Data.Log>().Property(l => l.ParentId).HasColumnName("syslog_parent_id");
			mb.Entity<Data.Log>().Property(l => l.Type).HasColumnName("syslog_parent_type").HasMaxLength(128).IsRequired();
			mb.Entity<Data.Log>().Property(l => l.Action).HasColumnName("syslog_action").HasMaxLength(64).IsRequired();
			mb.Entity<Data.Log>().Property(l => l.Created).HasColumnName("syslog_created");
			mb.Entity<Data.Log>().Property(l => l.Updated).HasColumnName("syslog_updated");
			mb.Entity<Data.Log>().Property(l => l.CreatedById).HasColumnName("syslog_created_by");
			mb.Entity<Data.Log>().Property(l => l.UpdatedById).HasColumnName("syslog_updated_by");
			mb.Entity<Data.Log>().HasRequired(l => l.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Log>().HasRequired(l => l.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Media
			mb.Entity<Data.Media>().ToTable("content");
			mb.Entity<Data.Media>().HasKey(m => new { m.Id, m.IsDraft });
			mb.Entity<Data.Media>().Property(m => m.Id).HasColumnName("content_id");
			mb.Entity<Data.Media>().Property(m => m.IsDraft).HasColumnName("content_draft");
			mb.Entity<Data.Media>().Property(m => m.ParentId).HasColumnName("content_parent_id");
			mb.Entity<Data.Media>().Property(m => m.PermalinkId).HasColumnName("content_permalink_id");
			mb.Entity<Data.Media>().Property(m => m.Filename).HasColumnName("content_filename").HasMaxLength(128);
			mb.Entity<Data.Media>().Property(m => m.OriginalUrl).HasColumnName("content_url").HasMaxLength(255);
			mb.Entity<Data.Media>().Property(m => m.LastSynced).HasColumnName("content_synced");
			mb.Entity<Data.Media>().Property(m => m.Name).HasColumnName("content_name").HasMaxLength(128);
			mb.Entity<Data.Media>().Property(m => m.ContentType).HasColumnName("content_type").HasMaxLength(255);
			mb.Entity<Data.Media>().Property(m => m.Size).HasColumnName("content_size");
			mb.Entity<Data.Media>().Property(m => m.IsImage).HasColumnName("content_image");
			mb.Entity<Data.Media>().Property(m => m.IsFolder).HasColumnName("content_folder");
			mb.Entity<Data.Media>().Property(m => m.Width).HasColumnName("content_width");
			mb.Entity<Data.Media>().Property(m => m.Height).HasColumnName("content_height");
			mb.Entity<Data.Media>().Property(m => m.AltDescription).HasColumnName("content_alt").HasMaxLength(128);
			mb.Entity<Data.Media>().Property(m => m.Description).HasColumnName("content_description").HasMaxLength(255);
			mb.Entity<Data.Media>().Property(m => m.Published).HasColumnName("content_published");
			mb.Entity<Data.Media>().Property(m => m.LastPublished).HasColumnName("content_last_published");
			mb.Entity<Data.Media>().Property(m => m.Created).HasColumnName("content_created");
			mb.Entity<Data.Media>().Property(m => m.Updated).HasColumnName("content_updated");
			mb.Entity<Data.Media>().Property(m => m.CreatedById).HasColumnName("content_created_by");
			mb.Entity<Data.Media>().Property(m => m.UpdatedById).HasColumnName("content_updated_by");
			mb.Entity<Data.Media>().HasOptional(m => m.Permalink).WithMany();
			mb.Entity<Data.Media>().HasRequired(m => m.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Media>().HasRequired(m => m.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Namespace
			mb.Entity<Data.Namespace>().ToTable("namespace");
			mb.Entity<Data.Namespace>().Property(n => n.Id).HasColumnName("namespace_id");
			mb.Entity<Data.Namespace>().Property(n => n.InternalId).HasColumnName("namespace_internal_id").HasMaxLength(32).IsRequired();
			mb.Entity<Data.Namespace>().Property(n => n.Name).HasColumnName("namespace_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.Namespace>().Property(n => n.Description).HasColumnName("namespace_description").HasMaxLength(255);
			mb.Entity<Data.Namespace>().Property(n => n.Created).HasColumnName("namespace_created");
			mb.Entity<Data.Namespace>().Property(n => n.Updated).HasColumnName("namespace_updated");
			mb.Entity<Data.Namespace>().Property(n => n.CreatedById).HasColumnName("namespace_created_by");
			mb.Entity<Data.Namespace>().Property(n => n.UpdatedById).HasColumnName("namespace_updated_by");
			mb.Entity<Data.Namespace>().HasRequired(n => n.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Namespace>().HasRequired(n => n.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Page
			mb.Entity<Data.Page>().ToTable("page");
			mb.Entity<Data.Page>().HasKey(p => new { p.Id, p.IsDraft });
			mb.Entity<Data.Page>().Property(p => p.Id).HasColumnName("page_id");
			mb.Entity<Data.Page>().Property(p => p.IsDraft).HasColumnName("page_draft");
			mb.Entity<Data.Page>().Property(p => p.SiteId).HasColumnName("page_sitetree_id");
			mb.Entity<Data.Page>().Property(p => p.OriginalId).HasColumnName("page_original_id");
			mb.Entity<Data.Page>().Property(p => p.TypeId).HasColumnName("page_template_id");
			mb.Entity<Data.Page>().Property(p => p.PermalinkId).HasColumnName("page_permalink_id");
			mb.Entity<Data.Page>().Property(p => p.GroupId).HasColumnName("page_group_id");
			mb.Entity<Data.Page>().Property(p => p.DisabledGroups).HasColumnName("page_group_disable_id");
			mb.Entity<Data.Page>().Property(p => p.ParentId).HasColumnName("page_parent_id");
			mb.Entity<Data.Page>().Property(p => p.SortOrder).HasColumnName("page_seqno");
			mb.Entity<Data.Page>().Property(p => p.Title).HasColumnName("page_title").HasMaxLength(128).IsRequired();
			mb.Entity<Data.Page>().Property(p => p.NavigationTitle).HasColumnName("page_navigation_title").HasMaxLength(128);
			mb.Entity<Data.Page>().Property(p => p.IsHidden).HasColumnName("page_is_hidden");
			mb.Entity<Data.Page>().Property(p => p.Keywords).HasColumnName("page_keywords").HasMaxLength(128);
			mb.Entity<Data.Page>().Property(p => p.Description).HasColumnName("page_description").HasMaxLength(255);
			mb.Entity<Data.Page>().Property(p => p.PageAttachments).HasColumnName("page_attachments");
			mb.Entity<Data.Page>().Property(p => p.Route).HasColumnName("page_controller").HasMaxLength(128);
			mb.Entity<Data.Page>().Property(p => p.View).HasColumnName("page_view").HasMaxLength(128);
			mb.Entity<Data.Page>().Property(p => p.Redirect).HasColumnName("page_redirect").HasMaxLength(128);
			mb.Entity<Data.Page>().Property(p => p.Published).HasColumnName("page_published");
			mb.Entity<Data.Page>().Property(p => p.LastPublished).HasColumnName("page_last_published");
			mb.Entity<Data.Page>().Property(p => p.LastModified).HasColumnName("page_last_modified");
			mb.Entity<Data.Page>().Property(p => p.Created).HasColumnName("page_created");
			mb.Entity<Data.Page>().Property(p => p.Updated).HasColumnName("page_updated");
			mb.Entity<Data.Page>().Property(p => p.CreatedById).HasColumnName("page_created_by");
			mb.Entity<Data.Page>().Property(p => p.UpdatedById).HasColumnName("page_updated_by");
			mb.Entity<Data.Page>().HasRequired(p => p.Type).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Page>().HasRequired(p => p.Permalink).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Page>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Page>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region PageRegion
			mb.Entity<Data.PageRegion>().ToTable("region");
			mb.Entity<Data.PageRegion>().HasKey(r => new { r.Id, r.IsDraft });
			mb.Entity<Data.PageRegion>().Property(r => r.Id).HasColumnName("region_id");
			mb.Entity<Data.PageRegion>().Property(r => r.IsDraft).HasColumnName("region_draft");
			mb.Entity<Data.PageRegion>().Property(r => r.PageId).HasColumnName("region_page_id");
			mb.Entity<Data.PageRegion>().Property(r => r.PageIsDraft).HasColumnName("region_page_draft");
			mb.Entity<Data.PageRegion>().Property(r => r.RegionTypeId).HasColumnName("region_regiontemplate_id");
			mb.Entity<Data.PageRegion>().Property(r => r.Name).HasColumnName("region_name").HasMaxLength(64);
			mb.Entity<Data.PageRegion>().Property(r => r.Body).HasColumnName("region_body");
			mb.Entity<Data.PageRegion>().Property(r => r.Created).HasColumnName("region_created");
			mb.Entity<Data.PageRegion>().Property(r => r.Updated).HasColumnName("region_updated");
			mb.Entity<Data.PageRegion>().Property(r => r.CreatedById).HasColumnName("region_created_by");
			mb.Entity<Data.PageRegion>().Property(r => r.UpdatedById).HasColumnName("region_updated_by");
			mb.Entity<Data.PageRegion>().HasRequired(r => r.Page).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.PageRegion>().HasRequired(r => r.RegionType).WithMany();
			mb.Entity<Data.PageRegion>().HasRequired(r => r.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.PageRegion>().HasRequired(r => r.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region PageType
			mb.Entity<Data.PageType>().ToTable("pagetemplate");
			mb.Entity<Data.PageType>().Property(p => p.Id).HasColumnName("pagetemplate_id");
			mb.Entity<Data.PageType>().Property(p => p.Name).HasColumnName("pagetemplate_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.PageType>().Property(p => p.Description).HasColumnName("pagetemplate_description").HasMaxLength(255);
			mb.Entity<Data.PageType>().Property(p => p.VisualGuide).HasColumnName("pagetemplate_preview");
			mb.Entity<Data.PageType>().Property(p => p.PageRegions).HasColumnName("pagetemplate_page_regions");
			mb.Entity<Data.PageType>().Property(p => p.PageProperties).HasColumnName("pagetemplate_properties");
			mb.Entity<Data.PageType>().Property(p => p.Route).HasColumnName("pagetemplate_controller").HasMaxLength(128);
			mb.Entity<Data.PageType>().Property(p => p.IsRouteVirtual).HasColumnName("pagetemplate_controller_show");
			mb.Entity<Data.PageType>().Property(p => p.View).HasColumnName("pagetemplate_view").HasMaxLength(128);
			mb.Entity<Data.PageType>().Property(p => p.IsViewVirtual).HasColumnName("pagetemplate_view_show");
			mb.Entity<Data.PageType>().Property(p => p.Redirect).HasColumnName("pagetemplate_redirect").HasMaxLength(128);
			mb.Entity<Data.PageType>().Property(p => p.IsRedirectVirtual).HasColumnName("pagetemplate_redirect_show");
			mb.Entity<Data.PageType>().Property(p => p.IsSite).HasColumnName("pagetemplate_site_template");
			mb.Entity<Data.PageType>().Property(p => p.CLRType).HasColumnName("pagetemplate_type").HasMaxLength(255);
			mb.Entity<Data.PageType>().Property(p => p.Created).HasColumnName("pagetemplate_created");
			mb.Entity<Data.PageType>().Property(p => p.Updated).HasColumnName("pagetemplate_updated");
			mb.Entity<Data.PageType>().Property(p => p.CreatedById).HasColumnName("pagetemplate_created_by");
			mb.Entity<Data.PageType>().Property(p => p.UpdatedById).HasColumnName("pagetemplate_updated_by");
			mb.Entity<Data.PageType>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.PageType>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region PageTypeRegion
			mb.Entity<Data.PageTypeRegion>().ToTable("regiontemplate");
			mb.Entity<Data.PageTypeRegion>().Property(r => r.Id).HasColumnName("regiontemplate_id");
			mb.Entity<Data.PageTypeRegion>().Property(r => r.PageTypeId).HasColumnName("regiontemplate_template_id");
			mb.Entity<Data.PageTypeRegion>().Property(r => r.InternalId).HasColumnName("regiontemplate_internal_id").HasMaxLength(32).IsRequired();
			mb.Entity<Data.PageTypeRegion>().Property(r => r.SortOrder).HasColumnName("regiontemplate_seqno");
			mb.Entity<Data.PageTypeRegion>().Property(r => r.Name).HasColumnName("regiontemplate_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.PageTypeRegion>().Property(r => r.Description).HasColumnName("regiontemplate_description").HasMaxLength(255);
			mb.Entity<Data.PageTypeRegion>().Property(r => r.CLRType).HasColumnName("regiontemplate_type").HasMaxLength(255).IsRequired();
			mb.Entity<Data.PageTypeRegion>().Property(r => r.Created).HasColumnName("regiontemplate_created");
			mb.Entity<Data.PageTypeRegion>().Property(r => r.Updated).HasColumnName("regiontemplate_updated");
			mb.Entity<Data.PageTypeRegion>().Property(r => r.CreatedById).HasColumnName("regiontemplate_created_by");
			mb.Entity<Data.PageTypeRegion>().Property(r => r.UpdatedById).HasColumnName("regiontemplate_updated_by");
			mb.Entity<Data.PageTypeRegion>().HasRequired(r => r.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.PageTypeRegion>().HasRequired(r => r.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Param
			mb.Entity<Data.Param>().ToTable("sysparam");
			mb.Entity<Data.Param>().Property(p => p.Id).HasColumnName("sysparam_id");
			mb.Entity<Data.Param>().Property(p => p.Name).HasColumnName("sysparam_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.Param>().Property(p => p.Description).HasColumnName("sysparam_description").HasMaxLength(255);
			mb.Entity<Data.Param>().Property(p => p.Value).HasColumnName("sysparam_value").HasMaxLength(128);
			mb.Entity<Data.Param>().Property(p => p.IsSystem).HasColumnName("sysparam_locked");
			mb.Entity<Data.Param>().Property(p => p.Created).HasColumnName("sysparam_created");
			mb.Entity<Data.Param>().Property(p => p.Updated).HasColumnName("sysparam_updated");
			mb.Entity<Data.Param>().Property(p => p.CreatedById).HasColumnName("sysparam_created_by");
			mb.Entity<Data.Param>().Property(p => p.UpdatedById).HasColumnName("sysparam_updated_by");
			#endregion

			#region Permalink
			mb.Entity<Data.Permalink>().ToTable("permalink");
			mb.Entity<Data.Permalink>().Property(p => p.Id).HasColumnName("permalink_id");
			mb.Entity<Data.Permalink>().Property(p => p.NamespaceId).HasColumnName("permalink_namespace_id")
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("index_permalink_name") {
					IsUnique = true,
					Order = 1
				}));
			mb.Entity<Data.Permalink>().Property(p => p.Type).HasColumnName("permalink_type").HasMaxLength(16).IsRequired();
			mb.Entity<Data.Permalink>().Property(p => p.Name).HasColumnName("permalink_name").HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("index_permalink_name") {
					Order = 2
				}));
			mb.Entity<Data.Permalink>().Property(p => p.Created).HasColumnName("permalink_created");
			mb.Entity<Data.Permalink>().Property(p => p.Updated).HasColumnName("permalink_updated");
			mb.Entity<Data.Permalink>().Property(p => p.CreatedById).HasColumnName("permalink_created_by");
			mb.Entity<Data.Permalink>().Property(p => p.UpdatedById).HasColumnName("permalink_updated_by");
			mb.Entity<Data.Permalink>().HasRequired(p => p.Namespace).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Permission
			mb.Entity<Data.Permission>().ToTable("sysaccess");
			mb.Entity<Data.Permission>().Property(p => p.Id).HasColumnName("sysaccess_id");
			mb.Entity<Data.Permission>().Property(p => p.GroupId).HasColumnName("sysaccess_group_id");
			mb.Entity<Data.Permission>().Property(p => p.Name).HasColumnName("sysaccess_function").HasMaxLength(64).IsRequired();
			mb.Entity<Data.Permission>().Property(p => p.Description).HasColumnName("sysaccess_description").HasMaxLength(255);
			mb.Entity<Data.Permission>().Property(p => p.IsSystem).HasColumnName("sysaccess_locked");
			mb.Entity<Data.Permission>().Property(p => p.Created).HasColumnName("sysaccess_created");
			mb.Entity<Data.Permission>().Property(p => p.Updated).HasColumnName("sysaccess_updated");
			mb.Entity<Data.Permission>().Property(p => p.CreatedById).HasColumnName("sysaccess_created_by");
			mb.Entity<Data.Permission>().Property(p => p.UpdatedById).HasColumnName("sysaccess_updated_by");
			#endregion

			#region Post
			mb.Entity<Data.Post>().ToTable("post");
			mb.Entity<Data.Post>().HasKey(p => new { p.Id, p.IsDraft });
			mb.Entity<Data.Post>().Property(p => p.Id).HasColumnName("post_id");
			mb.Entity<Data.Post>().Property(p => p.IsDraft).HasColumnName("post_draft");
			mb.Entity<Data.Post>().Property(p => p.TypeId).HasColumnName("post_template_id");
			mb.Entity<Data.Post>().Property(p => p.PermalinkId).HasColumnName("post_permalink_id");
			mb.Entity<Data.Post>().Property(p => p.Title).HasColumnName("post_title").HasMaxLength(128).IsRequired();
			mb.Entity<Data.Post>().Property(p => p.Keywords).HasColumnName("post_keywords").HasMaxLength(128);
			mb.Entity<Data.Post>().Property(p => p.Description).HasColumnName("post_description").HasMaxLength(255);
			mb.Entity<Data.Post>().Property(p => p.IncludeInFeed).HasColumnName("post_rss");
			mb.Entity<Data.Post>().Property(p => p.Excerpt).HasColumnName("post_excerpt").HasMaxLength(255);
			mb.Entity<Data.Post>().Property(p => p.Body).HasColumnName("post_body");
			mb.Entity<Data.Post>().Property(p => p.PostAttchments).HasColumnName("post_attachments");
			mb.Entity<Data.Post>().Property(p => p.Route).HasColumnName("post_controller").HasMaxLength(128);
			mb.Entity<Data.Post>().Property(p => p.View).HasColumnName("post_view").HasMaxLength(128);
			mb.Entity<Data.Post>().Property(p => p.ArchiveRoute).HasColumnName("post_archive_controller").HasMaxLength(128);
			mb.Entity<Data.Post>().Property(p => p.Published).HasColumnName("post_published");
			mb.Entity<Data.Post>().Property(p => p.LastPublished).HasColumnName("post_last_published");
			mb.Entity<Data.Post>().Property(p => p.LastModified).HasColumnName("post_last_modified");
			mb.Entity<Data.Post>().Property(p => p.Created).HasColumnName("post_created");
			mb.Entity<Data.Post>().Property(p => p.Updated).HasColumnName("post_updated");
			mb.Entity<Data.Post>().Property(p => p.CreatedById).HasColumnName("post_created_by");
			mb.Entity<Data.Post>().Property(p => p.UpdatedById).HasColumnName("post_updated_by");
			mb.Entity<Data.Post>().HasRequired(p => p.Type).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Post>().HasRequired(p => p.Permalink).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Post>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Post>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region PostType
			mb.Entity<Data.PostType>().ToTable("posttemplate");
			mb.Entity<Data.PostType>().Property(p => p.Id).HasColumnName("posttemplate_id");
			mb.Entity<Data.PostType>().Property(p => p.PermalinkId).HasColumnName("posttemplate_permalink_id");
			mb.Entity<Data.PostType>().Property(p => p.Name).HasColumnName("posttemplate_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.PostType>().Property(p => p.Description).HasColumnName("posttemplate_description").HasMaxLength(255);
			mb.Entity<Data.PostType>().Property(p => p.VisualGuide).HasColumnName("posttemplate_preview");
			mb.Entity<Data.PostType>().Property(p => p.PostProperties).HasColumnName("posttemplate_properties");
			mb.Entity<Data.PostType>().Property(p => p.Route).HasColumnName("posttemplate_controller").HasMaxLength(128);
			mb.Entity<Data.PostType>().Property(p => p.IsRouteVirtual).HasColumnName("posttemplate_controller_show");
			mb.Entity<Data.PostType>().Property(p => p.View).HasColumnName("posttemplate_view").HasMaxLength(128);
			mb.Entity<Data.PostType>().Property(p => p.IsViewVirtual).HasColumnName("posttemplate_view_show");
			mb.Entity<Data.PostType>().Property(p => p.ArchiveRoute).HasColumnName("posttemplate_archive_controller").HasMaxLength(128);
			mb.Entity<Data.PostType>().Property(p => p.IsArchiveRouteVirtual).HasColumnName("posttemplate_archive_controller_show");
			mb.Entity<Data.PostType>().Property(p => p.EnableFeed).HasColumnName("posttemplate_rss");
			mb.Entity<Data.PostType>().Property(p => p.CLRType).HasColumnName("posttemplate_type").HasMaxLength(255);
			mb.Entity<Data.PostType>().Property(p => p.Created).HasColumnName("posttemplate_created");
			mb.Entity<Data.PostType>().Property(p => p.Updated).HasColumnName("posttemplate_updated");
			mb.Entity<Data.PostType>().Property(p => p.CreatedById).HasColumnName("posttemplate_created_by");
			mb.Entity<Data.PostType>().Property(p => p.UpdatedById).HasColumnName("posttemplate_updated_by");
			mb.Entity<Data.PostType>().HasOptional(p => p.Permalink).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.PostType>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.PostType>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Property
			mb.Entity<Data.Property>().ToTable("property");
			mb.Entity<Data.Property>().HasKey(p => new { p.Id, p.IsDraft });
			mb.Entity<Data.Property>().Property(p => p.Id).HasColumnName("property_id");
			mb.Entity<Data.Property>().Property(p => p.IsDraft).HasColumnName("property_draft");
			mb.Entity<Data.Property>().Property(p => p.ParentId).HasColumnName("property_parent_id");
			mb.Entity<Data.Property>().Property(p => p.Name).HasColumnName("property_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.Property>().Property(p => p.Value).HasColumnName("property_value");
			mb.Entity<Data.Property>().Property(p => p.Created).HasColumnName("property_created");
			mb.Entity<Data.Property>().Property(p => p.Updated).HasColumnName("property_updated");
			mb.Entity<Data.Property>().Property(p => p.CreatedById).HasColumnName("property_created_by");
			mb.Entity<Data.Property>().Property(p => p.UpdatedById).HasColumnName("property_updated_by");
			mb.Entity<Data.Property>().HasRequired(p => p.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Property>().HasRequired(p => p.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Relation
			mb.Entity<Data.Relation>().ToTable("relation");
			mb.Entity<Data.Relation>().HasKey(r => new { r.Id, r.IsDraft });
			mb.Entity<Data.Relation>().Property(r => r.Id).HasColumnName("relation_id");
			mb.Entity<Data.Relation>().Property(r => r.IsDraft).HasColumnName("relation_draft");
			mb.Entity<Data.Relation>().Property(r => r.Type).HasColumnName("relation_type").HasMaxLength(16).IsRequired();
			mb.Entity<Data.Relation>().Property(r => r.ModelId).HasColumnName("relation_data_id");
			mb.Entity<Data.Relation>().Property(r => r.RelatedId).HasColumnName("relation_related_id");
			#endregion

			#region SiteTree
			mb.Entity<Data.SiteTree>().ToTable("sitetree");
			mb.Entity<Data.SiteTree>().Property(s => s.Id).HasColumnName("sitetree_id");
			mb.Entity<Data.SiteTree>().Property(s => s.NamespaceId).HasColumnName("sitetree_namespace_id");
			mb.Entity<Data.SiteTree>().Property(s => s.InternalId).HasColumnName("sitetree_internal_id").HasMaxLength(32).IsRequired();
			mb.Entity<Data.SiteTree>().Property(s => s.Name).HasColumnName("sitetree_name").HasMaxLength(64).IsRequired();
			mb.Entity<Data.SiteTree>().Property(s => s.Description).HasColumnName("sitetree_description").HasMaxLength(255);
			mb.Entity<Data.SiteTree>().Property(s => s.MetaTitle).HasColumnName("sitetree_meta_title").HasMaxLength(128);
			mb.Entity<Data.SiteTree>().Property(s => s.MetaDescription).HasColumnName("sitetree_meta_description").HasMaxLength(255);
			mb.Entity<Data.SiteTree>().Property(s => s.Hostnames).HasColumnName("sitetree_hostnames");
			mb.Entity<Data.SiteTree>().Property(s => s.Created).HasColumnName("sitetree_created");
			mb.Entity<Data.SiteTree>().Property(s => s.Updated).HasColumnName("sitetree_updated");
			mb.Entity<Data.SiteTree>().Property(s => s.CreatedById).HasColumnName("sitetree_created_by");
			mb.Entity<Data.SiteTree>().Property(s => s.UpdatedById).HasColumnName("sitetree_updated_by");
			mb.Entity<Data.SiteTree>().HasRequired(s => s.Namespace).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.SiteTree>().HasRequired(s => s.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.SiteTree>().HasRequired(s => s.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region Upload
			mb.Entity<Data.Upload>().ToTable("upload");
			mb.Entity<Data.Upload>().Property(u => u.Id).HasColumnName("upload_id");
			mb.Entity<Data.Upload>().Property(u => u.ParentId).HasColumnName("upload_parent_id");
			mb.Entity<Data.Upload>().Property(u => u.Filename).HasColumnName("upload_filename").HasMaxLength(128).IsRequired();
			mb.Entity<Data.Upload>().Property(u => u.ContentType).HasColumnName("upload_type").HasMaxLength(255).IsRequired();
			mb.Entity<Data.Upload>().Property(u => u.Created).HasColumnName("upload_created");
			mb.Entity<Data.Upload>().Property(u => u.Updated).HasColumnName("upload_updated");
			mb.Entity<Data.Upload>().Property(u => u.CreatedById).HasColumnName("upload_created_by");
			mb.Entity<Data.Upload>().Property(u => u.UpdatedById).HasColumnName("upload_updated_by");
			mb.Entity<Data.Upload>().HasRequired(u => u.CreatedBy).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Data.Upload>().HasRequired(u => u.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			#endregion

			#region User
			mb.Entity<Data.User>().ToTable("sysuser");
			mb.Entity<Data.User>().Property(u => u.Id).HasColumnName("sysuser_id");
			mb.Entity<Data.User>().Property(u => u.GroupId).HasColumnName("sysuser_group_id");
			mb.Entity<Data.User>().Property(u => u.ApiKey).HasColumnName("sysuser_apikey");
			mb.Entity<Data.User>().Property(u => u.Username).HasColumnName("sysuser_login").HasMaxLength(64).IsRequired();
			mb.Entity<Data.User>().Property(u => u.Password).HasColumnName("sysuser_password").HasMaxLength(64);
			mb.Entity<Data.User>().Property(u => u.Firstname).HasColumnName("sysuser_firstname").HasMaxLength(128);
			mb.Entity<Data.User>().Property(u => u.Lastname).HasColumnName("sysuser_surname").HasMaxLength(128);
			mb.Entity<Data.User>().Property(u => u.Email).HasColumnName("sysuser_email").HasMaxLength(128);
			mb.Entity<Data.User>().Property(u => u.Culture).HasColumnName("sysuser_culture").HasMaxLength(5);
			mb.Entity<Data.User>().Property(u => u.LatestLogin).HasColumnName("sysuser_last_login");
			mb.Entity<Data.User>().Property(u => u.PreviousLogin).HasColumnName("sysuser_prev_login");
			mb.Entity<Data.User>().Property(u => u.IsLocked).HasColumnName("sysuser_locked");
			mb.Entity<Data.User>().Property(u => u.LockedUntil).HasColumnName("sysuser_locked_until");
			mb.Entity<Data.User>().Property(u => u.Created).HasColumnName("sysuser_created");
			mb.Entity<Data.User>().Property(u => u.Updated).HasColumnName("sysuser_updated");
			mb.Entity<Data.User>().Property(u => u.CreatedById).HasColumnName("sysuser_created_by");
			mb.Entity<Data.User>().Property(u => u.UpdatedById).HasColumnName("sysuser_updated_by");
			#endregion

			base.OnModelCreating(mb);
		}

		#region Private methods
		/// <summary>
		/// Processes the models before storing them in the database.
		/// </summary>
		private void OnSave() {
			foreach (var entry in ChangeTracker.Entries()) {
				// Ensure id
				if (entry.Entity is Data.IModel) {
					var model = (Data.IModel)entry.Entity;

					if (model.Id == Guid.Empty)
						model.Id = Guid.NewGuid();
				}

				// Ensure modified
				if (entry.Entity is Data.IModified) {
					var model = (Data.IModified)entry.Entity;
					var now = DateTime.Now;

					if (entry.State == EntityState.Added)
						model.Created = now;
					if (entry.State == EntityState.Modified)
						model.Updated = now;
				}
			}
		}
		#endregion
	}
}
