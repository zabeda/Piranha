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
using System.Linq;

namespace Piranha.Data
{
	/// <summary>
	/// Handles seeding of required and default content in the database.
	/// </summary>
	public static class Seed
	{
		#region Members
		private static readonly Guid DefaultPageTypeId = new Guid("906761ea-6c04-4f4b-9365-f2c350ff4372");
		private static readonly Guid DefaultRegionTypeId = new Guid("96ADAC79-5DC5-453D-A0DE-A6871D74FD99");
		private static readonly Guid DefaultPostTypeId = new Guid("5017dbe4-5685-4941-921b-ca922edc7a12");
		private static readonly Guid DefaultPageId = new Guid("7849b6d6-dc43-43f6-8b5a-5770ab89fbcf");
		#endregion

		/// <summary>
		/// Seeds the required data needed to run the application.
		/// </summary>
		/// <param name="db">The current context</param>
		public static void SeedRequired(Db db) {
			#region Users
			var user = db.Users.Where(u => u.Id == Config.SysUserId).SingleOrDefault();
			if (user == null) {
				db.Users.Add(new User() {
					Id = Config.SysUserId,
					ApiKey = Guid.NewGuid(),
					GroupId = Config.SysAdminGroupId,
					Username = "sys"
				});
			}
			#endregion

			#region Groups
			var group = db.Groups.Where(g => g.Id == Config.SysAdminGroupId).SingleOrDefault();
			if (group == null) {
				db.Groups.Add(new Group() {
					Id = Config.SysAdminGroupId,
					Name = "Systemadministrator",
					Description = "System administrator group with full permissions.",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			group = db.Groups.Where(g => g.Id == Config.AdminGroupId).SingleOrDefault();
			if (group == null) {
				db.Groups.Add(new Group() {
					Id = Config.AdminGroupId,
					ParentId = Config.SysAdminGroupId,
					Name = "Administrator",
					Description = "Web site administrator group.",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			#endregion

			#region Permissions
			var perm = db.Permissions.Where(p => p.Name == "ADMIN").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN",
					Description = "Permission to login to the admin panel.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_PAGE_TEMPLATE").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_PAGE_TEMPLATE",
					Description = "Permission to add, update and delete page types.",
					GroupId = Config.SysAdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_POST_TEMPLATE").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_POST_TEMPLATE",
					Description = "Permission to add, update and delete post types.",
					GroupId = Config.SysAdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_PARAM").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_PARAM",
					Description = "Permission to add, update and delete system parameters.",
					GroupId = Config.SysAdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_ACCESS").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_ACCESS",
					Description = "Permission to add, update and delete permissions.",
					GroupId = Config.SysAdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_GROUP").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_GROUP",
					Description = "Permission to add, update and delete user groups.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_PAGE").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_PAGE",
					Description = "Permission to add and update pages.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_PAGE_PUBLISH").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_PAGE_PUBLISH",
					Description = "Permission to publish, depublish and delete pages.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_POST").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_POST",
					Description = "Permission to add and update posts.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_POST_PUBLISH").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_POST_PUBLISH",
					Description = "Permission to publish, depublish and delete posts.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_CATEGORY").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_CATEGORY",
					Description = "Permission to add and update categories.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_CONTENT").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_CONTENT",
					Description = "Permission to add and update media.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_CONTENT_PUBLISH").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_CONTENT_PUBLISH",
					Description = "Permission to publish, depublish and delete media.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_USER").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_USER",
					Description = "Permission to add, update and delete users.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_COMMENT").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_COMMENT",
					Description = "Permission to add, update and delete comments.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			perm = db.Permissions.Where(p => p.Name == "ADMIN_SITETREE").SingleOrDefault();
			if (perm == null) {
				db.Permissions.Add(new Permission() { 
					Name = "ADMIN_SITETREE",
					Description = "Permission to add, update and delete site trees.",
					GroupId = Config.AdminGroupId,
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			#endregion

			#region Params
			var param = db.Params.Where(p => p.Name == "SITE_LAST_MODIFIED").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "SITE_LAST_MODIFIED",
					Description = "Global last modification date.",
					Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			param = db.Params.Where(p => p.Name == "CACHE_PUBLIC_EXPIRES").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "CACHE_PUBLIC_EXPIRES",
					Description = "How many minutes browsers are allowed to cache public content.",
					Value = "0",
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			param = db.Params.Where(p => p.Name == "CACHE_PUBLIC_MAXAGE").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "CACHE_PUBLIC_MAXAGE",
					Description = "How many minutes cached content is valid in the browser.",
					Value = "0",
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			param = db.Params.Where(p => p.Name == "IMAGE_MAX_WIDTH").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "IMAGE_MAX_WIDTH",
					Description = "Maximum width for uploaded images.",
					Value = "1024",
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			param = db.Params.Where(p => p.Name == "SITEMAP_EXPANDED_LEVELS").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "SITEMAP_EXPANDED_LEVELS",
					Description = "The number of pre-expanded levels in the manager panel for the page list.",
					Value = "0",
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			param = db.Params.Where(p => p.Name == "RSS_NUM_POSTS").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "RSS_NUM_POSTS",
					Description = "The maximum number posts to be exported in a feed. For an infinite amount of posts, use the value 0.",
					Value = "10",
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			param = db.Params.Where(p => p.Name == "RSS_USE_EXCERPT").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "RSS_USE_EXCERPT",
					Description = "If the excerpt should be used instead of the full body in feeds.",
					Value = "1",
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			param = db.Params.Where(p => p.Name == "HIERARCHICAL_PERMALINKS").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "HIERARCHICAL_PERMALINKS",
					Description = "Weather or not permalink generation should be hierarchical.",
					Value = "0",
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			param = db.Params.Where(p => p.Name == "SITE_PRIVATE_KEY").SingleOrDefault();
			if (param == null) {
				db.Params.Add(new Param() { 
					Name = "SITE_PRIVATE_KEY",
					Description = "The private key used for public key encryption.",
					Value = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 16),
					IsSystem = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			#endregion

			#region Namespaces
			var ns = db.Namespaces.Where(n => n.Id == Config.DefaultNamespaceId).SingleOrDefault();
			if (ns == null) {
				db.Namespaces.Add(new Namespace() {
					Id = Config.DefaultNamespaceId,
					InternalId = "DEFAULT",
					Name = "Default namespace",
					Description = "This is the default namespace for all pages and posts.",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			ns = db.Namespaces.Where(n => n.Id == Config.CategoryNamespaceId).SingleOrDefault();
			if (ns == null) {
				db.Namespaces.Add(new Namespace() {
					Id = Config.CategoryNamespaceId,
					InternalId = "CATEGORY",
					Name = "Category namespace",
					Description = "This is the namespace for all categories.",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			ns = db.Namespaces.Where(n => n.Id == Config.ArchiveNamespaceId).SingleOrDefault();
			if (ns == null) {
				db.Namespaces.Add(new Namespace() {
					Id = Config.ArchiveNamespaceId,
					InternalId = "ARCHIVE",
					Name = "Archive namespace",
					Description = "This is the archive namespace for all post types.",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			ns = db.Namespaces.Where(n => n.Id == Config.MediaNamespaceId).SingleOrDefault();
			if (ns == null) {
				db.Namespaces.Add(new Namespace() {
					Id = Config.MediaNamespaceId,
					InternalId = "MEDIA",
					Name = "Media namespace",
					Description = "This is the media namespace for all images & documents.",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			#endregion

			#region Site tree
			var site = db.SiteTrees.Where(s => s.Id == Config.DefaultSiteTreeId).SingleOrDefault();
			if (site == null) {
				db.SiteTrees.Add(new SiteTree() { 
					Id = Config.DefaultSiteTreeId,
					NamespaceId = Config.DefaultNamespaceId,
					InternalId = "DEFAULT_SITE",
					Name = "Default site",
					Description = "This is the default site tree.",
					MetaTitle = "My site",
					MetaDescription = "Welcome the the template site",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}

			var pagetype = db.PageTypes.Where(t => t.Id == Config.DefaultSiteTreeId).SingleOrDefault();
			if (pagetype == null) {
				db.PageTypes.Add(new PageType() {
					Id = Config.DefaultSiteTreeId,
					Name = Config.DefaultSiteTreeId.ToString(),
					IsSite = true,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}

			var permalink = db.Permalinks.Where(p => p.Name == Config.DefaultSiteTreeId.ToString()).SingleOrDefault();
			if (permalink == null) {
				permalink = new Permalink() { 
					Id = Guid.NewGuid(),
					NamespaceId = Config.DefaultNamespaceId,
					Type = "SITE",
					Name = Config.DefaultSiteTreeId.ToString(),
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				};
			}
			db.Permalinks.Add(permalink);

			var draft = db.Pages.Where(p => p.TypeId == Config.DefaultSiteTreeId && p.IsDraft).SingleOrDefault();
			if (draft == null) {
				draft = new Page() {
					Id = Guid.NewGuid(),
					SiteId = Config.DefaultSiteTreeId,
					TypeId = Config.DefaultSiteTreeId,
					IsDraft = true,
					PermalinkId = permalink.Id,
					ParentId = Config.DefaultSiteTreeId,
					SortOrder = 1,
					Title = Config.DefaultSiteTreeId.ToString(),
					Published = DateTime.Now,
					LastPublished = DateTime.Now,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				};
				db.Pages.Add(draft);
			}
			var publ = db.Pages.Where(p => p.TypeId == Config.DefaultSiteTreeId && !p.IsDraft).SingleOrDefault();
			if (publ == null) {
				db.Pages.Add(new Page() {
					Id = draft.Id,
					SiteId = draft.SiteId,
					TypeId = draft.TypeId,
					IsDraft = false,
					PermalinkId = draft.PermalinkId,
					ParentId = draft.ParentId,
					SortOrder = draft.SortOrder,
					Title = draft.Title,
					Published = draft.Published,
					LastPublished = draft.LastPublished,
					CreatedById = draft.CreatedById,
					UpdatedById = draft.UpdatedById
				});
			}
			#endregion

			db.SaveChanges();
		}

		/// <summary>
		/// Seeds the default data. This is only performed when making
		/// a fresh install and is not called on updates.
		/// </summary>
		/// <param name="db">The current context</param>
		public static void SeedDefault(Db db) {
			#region Default page type
			var pagetype = db.PageTypes.Where(t => t.Id == DefaultPageTypeId).SingleOrDefault();
			if (pagetype == null) {
				db.PageTypes.Add(new PageType() {
					Id = DefaultPageTypeId,
					Name = "Standard page",
					Description = "Page with a single HTML region",
					VisualGuide =
						"<table>\n" +
						"  <tr>\n" +
						"    <td id=\"Content\"></td>\n" +
						"  </tr>\n" +
						"</table>",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
				db.PageTypeRegions.Add(new PageTypeRegion() {
					Id = DefaultRegionTypeId,
					PageTypeId = DefaultPageTypeId,
					InternalId = "Content",
					Name = "Content",
					SortOrder = 1,
					CLRType = typeof(Piranha.Extend.Regions.HtmlRegion).FullName,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			#endregion

			#region Default post type
			var posttype = db.PostTypes.Where(t => t.Id == DefaultPostTypeId).SingleOrDefault();
			if (posttype == null) {
				db.PostTypes.Add(new PostType() {
					Id = DefaultPostTypeId,
					Name = "Standard post",
					Description = "Standard post",
					VisualGuide =
						"<table>\n" +
						"  <tr>\n" +
						"    <td></td>\n" +
						"  </tr>\n" +
						"</table>",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			#endregion

			#region Default page
			var draft = db.Pages.Where(p => p.Id == DefaultPageId && p.IsDraft).SingleOrDefault();
			if (draft == null) {
				var permalink = new Permalink() { 
					Id = Guid.NewGuid(),
					NamespaceId = Config.DefaultNamespaceId,
					Type = "PAGE",
					Name = "start",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				};
				db.Permalinks.Add(permalink);

				// Add 10 seconds so the page isn't considered to
				// have a more recent draft.
				var now = DateTime.Now.AddSeconds(10);
				draft = new Page() { 
					Id = DefaultPageId,
					TypeId = DefaultPageTypeId,
					SiteId = Config.DefaultSiteTreeId,
					IsDraft = true,
					PermalinkId = permalink.Id,
					SortOrder = 1,
					Title = "Start",
					Keywords = "Piranha, Welcome",
					Description = "Welcome to Piranha",
					Published = now,
					LastPublished = now,
					LastModified = now,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				};
				db.Pages.Add(draft);

				db.Pages.Add(new Page() {
					Id = draft.Id,
					TypeId = draft.TypeId,
					SiteId = draft.SiteId,
					IsDraft = false,
					PermalinkId = draft.PermalinkId,
					SortOrder = draft.SortOrder,
					Title = draft.Title,
					Keywords = draft.Keywords,
					Description = draft.Description,
					Published = draft.Published,
					LastPublished = draft.LastPublished,
					LastModified = draft.LastModified,
					CreatedById = draft.CreatedById,
					UpdatedById = draft.UpdatedById
				});

				db.PageRegions.Add(new PageRegion() {
					PageId = draft.Id,
					PageIsDraft = true,
					IsDraft = true,
					RegionTypeId = DefaultRegionTypeId,
					Body = "<p>Welcome to Piranha -  the fun, fast and lightweight framework for developing cms-based web applications with an extra bite.</p>",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
				db.PageRegions.Add(new PageRegion() {
					PageId = draft.Id,
					PageIsDraft = false,
					IsDraft = false,
					RegionTypeId = DefaultRegionTypeId,
					Body = "<p>Welcome to Piranha -  the fun, fast and lightweight framework for developing cms-based web applications with an extra bite.</p>",
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
			}
			#endregion

			#region Default user
			db.Users.Add(new User() {
				ApiKey = Guid.NewGuid(),
				GroupId = Config.SysAdminGroupId,
				Username = "admin",
				Password = Models.SysUser.Encrypt("password")
			});
			#endregion

			db.SaveChanges();
		}
	}
}