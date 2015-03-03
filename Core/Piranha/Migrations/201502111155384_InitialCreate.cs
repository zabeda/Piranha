/*
 * Copyright (c) 2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

namespace Piranha.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class InitialCreate : DbMigration
	{
		public override void Up() {
			CreateTable(
				"dbo.category",
				c => new {
					category_id = c.Guid(nullable: false),
					category_parent_id = c.Guid(),
					category_permalink_id = c.Guid(nullable: false),
					category_name = c.String(nullable: false, maxLength: 64),
					category_description = c.String(maxLength: 255),
					category_created = c.DateTime(nullable: false),
					category_updated = c.DateTime(nullable: false),
					category_created_by = c.Guid(nullable: false),
					category_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.category_id)
				.ForeignKey("dbo.sysuser", t => t.category_created_by)
				.ForeignKey("dbo.permalink", t => t.category_permalink_id)
				.ForeignKey("dbo.sysuser", t => t.category_updated_by)
				.Index(t => t.category_permalink_id)
				.Index(t => t.category_created_by)
				.Index(t => t.category_updated_by);

			CreateTable(
				"dbo.sysuser",
				c => new {
					sysuser_id = c.Guid(nullable: false),
					sysuser_group_id = c.Guid(),
					sysuser_apikey = c.Guid(),
					sysuser_login = c.String(nullable: false, maxLength: 64),
					sysuser_password = c.String(maxLength: 64),
					sysuser_firstname = c.String(maxLength: 128),
					sysuser_surname = c.String(maxLength: 128),
					sysuser_email = c.String(maxLength: 128),
					sysuser_culture = c.String(maxLength: 5),
					sysuser_locked = c.Boolean(nullable: false),
					sysuser_locked_until = c.DateTime(),
					sysuser_last_login = c.DateTime(),
					sysuser_prev_login = c.DateTime(),
					sysuser_created = c.DateTime(nullable: false),
					sysuser_updated = c.DateTime(nullable: false),
					sysuser_created_by = c.Guid(),
					sysuser_updated_by = c.Guid(),
				})
				.PrimaryKey(t => t.sysuser_id);

			CreateTable(
				"dbo.permalink",
				c => new {
					permalink_id = c.Guid(nullable: false),
					permalink_namespace_id = c.Guid(nullable: false),
					permalink_type = c.String(nullable: false, maxLength: 16),
					permalink_name = c.String(nullable: false, maxLength: 128),
					permalink_created = c.DateTime(nullable: false),
					permalink_updated = c.DateTime(nullable: false),
					permalink_created_by = c.Guid(nullable: false),
					permalink_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.permalink_id)
				.ForeignKey("dbo.namespace", t => t.permalink_namespace_id)
				.Index(t => new { t.permalink_namespace_id, t.permalink_name }, unique: true, name: "index_permalink_name");

			CreateTable(
				"dbo.namespace",
				c => new {
					namespace_id = c.Guid(nullable: false),
					namespace_internal_id = c.String(nullable: false, maxLength: 32),
					namespace_name = c.String(nullable: false, maxLength: 64),
					namespace_description = c.String(maxLength: 255),
					namespace_created = c.DateTime(nullable: false),
					namespace_updated = c.DateTime(nullable: false),
					namespace_created_by = c.Guid(nullable: false),
					namespace_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.namespace_id)
				.ForeignKey("dbo.sysuser", t => t.namespace_created_by)
				.ForeignKey("dbo.sysuser", t => t.namespace_updated_by)
				.Index(t => t.namespace_created_by)
				.Index(t => t.namespace_updated_by);

			CreateTable(
				"dbo.comment",
				c => new {
					comment_id = c.Guid(nullable: false),
					comment_parent_id = c.Guid(nullable: false),
					comment_parent_draft = c.Boolean(nullable: false),
					comment_status = c.Int(nullable: false),
					comment_reported_count = c.Int(nullable: false),
					comment_title = c.String(maxLength: 64),
					comment_body = c.String(),
					comment_author_name = c.String(maxLength: 128),
					comment_author_email = c.String(maxLength: 128),
					comment_created = c.DateTime(nullable: false),
					comment_updated = c.DateTime(nullable: false),
					comment_created_by = c.Guid(),
					comment_updated_by = c.Guid(),
				})
				.PrimaryKey(t => t.comment_id)
				.ForeignKey("dbo.sysuser", t => t.comment_created_by)
				.ForeignKey("dbo.sysuser", t => t.comment_updated_by)
				.Index(t => t.comment_created_by)
				.Index(t => t.comment_updated_by);

			CreateTable(
				"dbo.extension",
				c => new {
					extension_id = c.Guid(nullable: false),
					extension_draft = c.Boolean(nullable: false),
					extension_parent_id = c.Guid(nullable: false),
					extension_body = c.String(),
					extension_type = c.String(nullable: false, maxLength: 255),
					extension_created = c.DateTime(nullable: false),
					extension_updated = c.DateTime(nullable: false),
					extension_created_by = c.Guid(nullable: false),
					extension_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.extension_id, t.extension_draft })
				.ForeignKey("dbo.sysuser", t => t.extension_created_by)
				.ForeignKey("dbo.sysuser", t => t.extension_updated_by)
				.Index(t => t.extension_created_by)
				.Index(t => t.extension_updated_by);

			CreateTable(
				"dbo.sysgroup",
				c => new {
					sysgroup_id = c.Guid(nullable: false),
					sysgroup_parent_id = c.Guid(),
					sysgroup_name = c.String(nullable: false, maxLength: 64),
					sysgroup_description = c.String(maxLength: 255),
					sysgroup_created = c.DateTime(nullable: false),
					sysgroup_updated = c.DateTime(nullable: false),
					sysgroup_created_by = c.Guid(),
					sysgroup_updated_by = c.Guid(),
				})
				.PrimaryKey(t => t.sysgroup_id)
				.ForeignKey("dbo.sysuser", t => t.sysgroup_created_by)
				.ForeignKey("dbo.sysuser", t => t.sysgroup_updated_by)
				.Index(t => t.sysgroup_created_by)
				.Index(t => t.sysgroup_updated_by);

			CreateTable(
				"dbo.syslog",
				c => new {
					syslog_id = c.Guid(nullable: false),
					syslog_parent_id = c.Guid(nullable: false),
					syslog_parent_type = c.String(nullable: false, maxLength: 128),
					syslog_action = c.String(nullable: false, maxLength: 64),
					syslog_created = c.DateTime(nullable: false),
					syslog_updated = c.DateTime(nullable: false),
					syslog_created_by = c.Guid(nullable: false),
					syslog_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.syslog_id)
				.ForeignKey("dbo.sysuser", t => t.syslog_created_by)
				.ForeignKey("dbo.sysuser", t => t.syslog_updated_by)
				.Index(t => t.syslog_created_by)
				.Index(t => t.syslog_updated_by);

			CreateTable(
				"dbo.content",
				c => new {
					content_id = c.Guid(nullable: false),
					content_draft = c.Boolean(nullable: false),
					content_parent_id = c.Guid(),
					content_permalink_id = c.Guid(),
					content_filename = c.String(maxLength: 128),
					content_url = c.String(maxLength: 255),
					content_synced = c.DateTime(),
					content_name = c.String(maxLength: 128),
					content_type = c.String(maxLength: 255),
					content_size = c.Int(nullable: false),
					content_image = c.Boolean(nullable: false),
					content_folder = c.Boolean(nullable: false),
					content_width = c.Int(),
					content_height = c.Int(),
					content_alt = c.String(maxLength: 128),
					content_description = c.String(maxLength: 255),
					content_published = c.DateTime(),
					content_last_published = c.DateTime(),
					content_created = c.DateTime(nullable: false),
					content_updated = c.DateTime(nullable: false),
					content_created_by = c.Guid(nullable: false),
					content_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.content_id, t.content_draft })
				.ForeignKey("dbo.sysuser", t => t.content_created_by)
				.ForeignKey("dbo.permalink", t => t.content_permalink_id)
				.ForeignKey("dbo.sysuser", t => t.content_updated_by)
				.Index(t => t.content_permalink_id)
				.Index(t => t.content_created_by)
				.Index(t => t.content_updated_by);

			CreateTable(
				"dbo.region",
				c => new {
					region_id = c.Guid(nullable: false),
					region_draft = c.Boolean(nullable: false),
					region_page_id = c.Guid(nullable: false),
					region_page_draft = c.Boolean(nullable: false),
					region_regiontemplate_id = c.Guid(nullable: false),
					region_name = c.String(maxLength: 64),
					region_body = c.String(),
					region_created = c.DateTime(nullable: false),
					region_updated = c.DateTime(nullable: false),
					region_created_by = c.Guid(nullable: false),
					region_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.region_id, t.region_draft })
				.ForeignKey("dbo.sysuser", t => t.region_created_by)
				.ForeignKey("dbo.page", t => new { t.region_page_id, t.region_page_draft })
				.ForeignKey("dbo.regiontemplate", t => t.region_regiontemplate_id, cascadeDelete: true)
				.ForeignKey("dbo.sysuser", t => t.region_updated_by)
				.Index(t => new { t.region_page_id, t.region_page_draft })
				.Index(t => t.region_regiontemplate_id)
				.Index(t => t.region_created_by)
				.Index(t => t.region_updated_by);

			CreateTable(
				"dbo.page",
				c => new {
					page_id = c.Guid(nullable: false),
					page_draft = c.Boolean(nullable: false),
					page_sitetree_id = c.Guid(nullable: false),
					page_template_id = c.Guid(nullable: false),
					page_permalink_id = c.Guid(nullable: false),
					page_original_id = c.Guid(),
					page_group_id = c.Guid(),
					page_group_disable_id = c.String(),
					page_parent_id = c.Guid(),
					page_seqno = c.Int(nullable: false),
					page_title = c.String(nullable: false, maxLength: 128),
					page_navigation_title = c.String(maxLength: 128),
					page_is_hidden = c.Boolean(nullable: false),
					page_keywords = c.String(maxLength: 128),
					page_description = c.String(maxLength: 255),
					page_attachments = c.String(),
					page_controller = c.String(maxLength: 128),
					page_view = c.String(maxLength: 128),
					page_redirect = c.String(maxLength: 128),
					page_published = c.DateTime(),
					page_last_published = c.DateTime(),
					page_last_modified = c.DateTime(),
					page_created = c.DateTime(nullable: false),
					page_updated = c.DateTime(nullable: false),
					page_created_by = c.Guid(nullable: false),
					page_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.page_id, t.page_draft })
				.ForeignKey("dbo.sysuser", t => t.page_created_by)
				.ForeignKey("dbo.permalink", t => t.page_permalink_id)
				.ForeignKey("dbo.pagetemplate", t => t.page_template_id)
				.ForeignKey("dbo.sysuser", t => t.page_updated_by)
				.Index(t => t.page_template_id)
				.Index(t => t.page_permalink_id)
				.Index(t => t.page_created_by)
				.Index(t => t.page_updated_by);

			CreateTable(
				"dbo.pagetemplate",
				c => new {
					pagetemplate_id = c.Guid(nullable: false),
					pagetemplate_name = c.String(nullable: false, maxLength: 64),
					pagetemplate_description = c.String(maxLength: 255),
					pagetemplate_preview = c.String(),
					pagetemplate_page_regions = c.String(),
					pagetemplate_properties = c.String(),
					pagetemplate_controller = c.String(maxLength: 128),
					pagetemplate_controller_show = c.Boolean(nullable: false),
					pagetemplate_view = c.String(maxLength: 128),
					pagetemplate_view_show = c.Boolean(nullable: false),
					pagetemplate_redirect = c.String(maxLength: 128),
					pagetemplate_redirect_show = c.Boolean(nullable: false),
					pagetemplate_site_template = c.Boolean(nullable: false),
					pagetemplate_type = c.String(maxLength: 255),
					pagetemplate_created = c.DateTime(nullable: false),
					pagetemplate_updated = c.DateTime(nullable: false),
					pagetemplate_created_by = c.Guid(nullable: false),
					pagetemplate_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.pagetemplate_id)
				.ForeignKey("dbo.sysuser", t => t.pagetemplate_created_by)
				.ForeignKey("dbo.sysuser", t => t.pagetemplate_updated_by)
				.Index(t => t.pagetemplate_created_by)
				.Index(t => t.pagetemplate_updated_by);

			CreateTable(
				"dbo.regiontemplate",
				c => new {
					regiontemplate_id = c.Guid(nullable: false),
					regiontemplate_template_id = c.Guid(nullable: false),
					regiontemplate_internal_id = c.String(nullable: false, maxLength: 32),
					regiontemplate_seqno = c.Int(nullable: false),
					regiontemplate_name = c.String(nullable: false, maxLength: 64),
					regiontemplate_description = c.String(maxLength: 255),
					regiontemplate_type = c.String(nullable: false, maxLength: 255),
					regiontemplate_created = c.DateTime(nullable: false),
					regiontemplate_updated = c.DateTime(nullable: false),
					regiontemplate_created_by = c.Guid(nullable: false),
					regiontemplate_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.regiontemplate_id)
				.ForeignKey("dbo.sysuser", t => t.regiontemplate_created_by)
				.ForeignKey("dbo.sysuser", t => t.regiontemplate_updated_by)
				.Index(t => t.regiontemplate_created_by)
				.Index(t => t.regiontemplate_updated_by);

			CreateTable(
				"dbo.sysparam",
				c => new {
					sysparam_id = c.Guid(nullable: false),
					sysparam_name = c.String(nullable: false, maxLength: 64),
					sysparam_description = c.String(maxLength: 255),
					sysparam_value = c.String(maxLength: 128),
					sysparam_locked = c.Boolean(nullable: false),
					sysparam_created = c.DateTime(nullable: false),
					sysparam_updated = c.DateTime(nullable: false),
					sysparam_created_by = c.Guid(),
					sysparam_updated_by = c.Guid(),
				})
				.PrimaryKey(t => t.sysparam_id)
				.ForeignKey("dbo.sysuser", t => t.sysparam_created_by)
				.ForeignKey("dbo.sysuser", t => t.sysparam_updated_by)
				.Index(t => t.sysparam_created_by)
				.Index(t => t.sysparam_updated_by);

			CreateTable(
				"dbo.sysaccess",
				c => new {
					sysaccess_id = c.Guid(nullable: false),
					sysaccess_group_id = c.Guid(nullable: false),
					sysaccess_function = c.String(nullable: false, maxLength: 64),
					sysaccess_description = c.String(maxLength: 255),
					sysaccess_locked = c.Boolean(nullable: false),
					sysaccess_created = c.DateTime(nullable: false),
					sysaccess_updated = c.DateTime(nullable: false),
					sysaccess_created_by = c.Guid(),
					sysaccess_updated_by = c.Guid(),
				})
				.PrimaryKey(t => t.sysaccess_id)
				.ForeignKey("dbo.sysuser", t => t.sysaccess_created_by)
				.ForeignKey("dbo.sysuser", t => t.sysaccess_updated_by)
				.Index(t => t.sysaccess_created_by)
				.Index(t => t.sysaccess_updated_by);

			CreateTable(
				"dbo.post",
				c => new {
					post_id = c.Guid(nullable: false),
					post_draft = c.Boolean(nullable: false),
					post_template_id = c.Guid(nullable: false),
					post_permalink_id = c.Guid(nullable: false),
					post_title = c.String(nullable: false, maxLength: 128),
					post_keywords = c.String(maxLength: 128),
					post_description = c.String(maxLength: 255),
					post_rss = c.Boolean(nullable: false),
					post_excerpt = c.String(maxLength: 255),
					post_body = c.String(),
					post_attachments = c.String(),
					post_controller = c.String(maxLength: 128),
					post_view = c.String(maxLength: 128),
					post_archive_controller = c.String(maxLength: 128),
					post_published = c.DateTime(),
					post_last_published = c.DateTime(),
					post_last_modified = c.DateTime(),
					post_created = c.DateTime(nullable: false),
					post_updated = c.DateTime(nullable: false),
					post_created_by = c.Guid(nullable: false),
					post_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.post_id, t.post_draft })
				.ForeignKey("dbo.sysuser", t => t.post_created_by)
				.ForeignKey("dbo.permalink", t => t.post_permalink_id)
				.ForeignKey("dbo.posttemplate", t => t.post_template_id)
				.ForeignKey("dbo.sysuser", t => t.post_updated_by)
				.Index(t => t.post_template_id)
				.Index(t => t.post_permalink_id)
				.Index(t => t.post_created_by)
				.Index(t => t.post_updated_by);

			CreateTable(
				"dbo.posttemplate",
				c => new {
					posttemplate_id = c.Guid(nullable: false),
					posttemplate_permalink_id = c.Guid(),
					posttemplate_name = c.String(nullable: false, maxLength: 64),
					posttemplate_description = c.String(maxLength: 255),
					posttemplate_preview = c.String(),
					posttemplate_properties = c.String(),
					posttemplate_controller = c.String(maxLength: 128),
					posttemplate_controller_show = c.Boolean(nullable: false),
					posttemplate_view = c.String(maxLength: 128),
					posttemplate_view_show = c.Boolean(nullable: false),
					posttemplate_archive_controller = c.String(maxLength: 128),
					posttemplate_archive_controller_show = c.Boolean(nullable: false),
					posttemplate_rss = c.Boolean(nullable: false),
					posttemplate_type = c.String(maxLength: 255),
					posttemplate_created = c.DateTime(nullable: false),
					posttemplate_updated = c.DateTime(nullable: false),
					posttemplate_created_by = c.Guid(nullable: false),
					posttemplate_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.posttemplate_id)
				.ForeignKey("dbo.sysuser", t => t.posttemplate_created_by)
				.ForeignKey("dbo.permalink", t => t.posttemplate_permalink_id)
				.ForeignKey("dbo.sysuser", t => t.posttemplate_updated_by)
				.Index(t => t.posttemplate_permalink_id)
				.Index(t => t.posttemplate_created_by)
				.Index(t => t.posttemplate_updated_by);

			CreateTable(
				"dbo.property",
				c => new {
					property_id = c.Guid(nullable: false),
					property_draft = c.Boolean(nullable: false),
					property_parent_id = c.Guid(nullable: false),
					property_name = c.String(nullable: false, maxLength: 64),
					property_value = c.String(),
					property_created = c.DateTime(nullable: false),
					property_updated = c.DateTime(nullable: false),
					property_created_by = c.Guid(nullable: false),
					property_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.property_id, t.property_draft })
				.ForeignKey("dbo.sysuser", t => t.property_created_by)
				.ForeignKey("dbo.sysuser", t => t.property_updated_by)
				.Index(t => t.property_created_by)
				.Index(t => t.property_updated_by);

			CreateTable(
				"dbo.relation",
				c => new {
					relation_id = c.Guid(nullable: false),
					relation_draft = c.Boolean(nullable: false),
					relation_type = c.String(nullable: false, maxLength: 16),
					relation_data_id = c.Guid(nullable: false),
					relation_related_id = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.relation_id, t.relation_draft });

			CreateTable(
				"dbo.sitetree",
				c => new {
					sitetree_id = c.Guid(nullable: false),
					sitetree_namespace_id = c.Guid(nullable: false),
					sitetree_internal_id = c.String(nullable: false, maxLength: 32),
					sitetree_name = c.String(nullable: false, maxLength: 64),
					sitetree_description = c.String(maxLength: 255),
					sitetree_meta_title = c.String(maxLength: 128),
					sitetree_meta_description = c.String(maxLength: 255),
					sitetree_hostnames = c.String(),
					sitetree_created = c.DateTime(nullable: false),
					sitetree_updated = c.DateTime(nullable: false),
					sitetree_created_by = c.Guid(nullable: false),
					sitetree_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.sitetree_id)
				.ForeignKey("dbo.sysuser", t => t.sitetree_created_by)
				.ForeignKey("dbo.namespace", t => t.sitetree_namespace_id)
				.ForeignKey("dbo.sysuser", t => t.sitetree_updated_by)
				.Index(t => t.sitetree_namespace_id)
				.Index(t => t.sitetree_created_by)
				.Index(t => t.sitetree_updated_by);

			CreateTable(
				"dbo.upload",
				c => new {
					upload_id = c.Guid(nullable: false),
					upload_parent_id = c.Guid(),
					upload_filename = c.String(nullable: false, maxLength: 128),
					upload_type = c.String(nullable: false, maxLength: 255),
					upload_created = c.DateTime(nullable: false),
					upload_updated = c.DateTime(nullable: false),
					upload_created_by = c.Guid(nullable: false),
					upload_updated_by = c.Guid(nullable: false),
				})
				.PrimaryKey(t => t.upload_id)
				.ForeignKey("dbo.sysuser", t => t.upload_created_by)
				.ForeignKey("dbo.sysuser", t => t.upload_updated_by)
				.Index(t => t.upload_created_by)
				.Index(t => t.upload_updated_by);

		}

		public override void Down() {
			DropForeignKey("dbo.upload", "upload_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.upload", "upload_created_by", "dbo.sysuser");
			DropForeignKey("dbo.sitetree", "sitetree_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.sitetree", "sitetree_namespace_id", "dbo.namespace");
			DropForeignKey("dbo.sitetree", "sitetree_created_by", "dbo.sysuser");
			DropForeignKey("dbo.property", "property_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.property", "property_created_by", "dbo.sysuser");
			DropForeignKey("dbo.post", "post_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.post", "post_template_id", "dbo.posttemplate");
			DropForeignKey("dbo.posttemplate", "posttemplate_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.posttemplate", "posttemplate_permalink_id", "dbo.permalink");
			DropForeignKey("dbo.posttemplate", "posttemplate_created_by", "dbo.sysuser");
			DropForeignKey("dbo.post", "post_permalink_id", "dbo.permalink");
			DropForeignKey("dbo.post", "post_created_by", "dbo.sysuser");
			DropForeignKey("dbo.sysaccess", "sysaccess_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.sysaccess", "sysaccess_created_by", "dbo.sysuser");
			DropForeignKey("dbo.sysparam", "sysparam_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.sysparam", "sysparam_created_by", "dbo.sysuser");
			DropForeignKey("dbo.region", "region_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.region", "region_regiontemplate_id", "dbo.regiontemplate");
			DropForeignKey("dbo.regiontemplate", "regiontemplate_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.regiontemplate", "regiontemplate_created_by", "dbo.sysuser");
			DropForeignKey("dbo.region", new[] { "region_page_id", "region_page_draft" }, "dbo.page");
			DropForeignKey("dbo.page", "page_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.page", "page_template_id", "dbo.pagetemplate");
			DropForeignKey("dbo.pagetemplate", "pagetemplate_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.pagetemplate", "pagetemplate_created_by", "dbo.sysuser");
			DropForeignKey("dbo.page", "page_permalink_id", "dbo.permalink");
			DropForeignKey("dbo.page", "page_created_by", "dbo.sysuser");
			DropForeignKey("dbo.region", "region_created_by", "dbo.sysuser");
			DropForeignKey("dbo.content", "content_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.content", "content_permalink_id", "dbo.permalink");
			DropForeignKey("dbo.content", "content_created_by", "dbo.sysuser");
			DropForeignKey("dbo.syslog", "syslog_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.syslog", "syslog_created_by", "dbo.sysuser");
			DropForeignKey("dbo.sysgroup", "sysgroup_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.sysgroup", "sysgroup_created_by", "dbo.sysuser");
			DropForeignKey("dbo.extension", "extension_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.extension", "extension_created_by", "dbo.sysuser");
			DropForeignKey("dbo.comment", "comment_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.comment", "comment_created_by", "dbo.sysuser");
			DropForeignKey("dbo.category", "category_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.category", "category_permalink_id", "dbo.permalink");
			DropForeignKey("dbo.permalink", "permalink_namespace_id", "dbo.namespace");
			DropForeignKey("dbo.namespace", "namespace_updated_by", "dbo.sysuser");
			DropForeignKey("dbo.namespace", "namespace_created_by", "dbo.sysuser");
			DropForeignKey("dbo.category", "category_created_by", "dbo.sysuser");
			DropIndex("dbo.upload", new[] { "upload_updated_by" });
			DropIndex("dbo.upload", new[] { "upload_created_by" });
			DropIndex("dbo.sitetree", new[] { "sitetree_updated_by" });
			DropIndex("dbo.sitetree", new[] { "sitetree_created_by" });
			DropIndex("dbo.sitetree", new[] { "sitetree_namespace_id" });
			DropIndex("dbo.property", new[] { "property_updated_by" });
			DropIndex("dbo.property", new[] { "property_created_by" });
			DropIndex("dbo.posttemplate", new[] { "posttemplate_updated_by" });
			DropIndex("dbo.posttemplate", new[] { "posttemplate_created_by" });
			DropIndex("dbo.posttemplate", new[] { "posttemplate_permalink_id" });
			DropIndex("dbo.post", new[] { "post_updated_by" });
			DropIndex("dbo.post", new[] { "post_created_by" });
			DropIndex("dbo.post", new[] { "post_permalink_id" });
			DropIndex("dbo.post", new[] { "post_template_id" });
			DropIndex("dbo.sysaccess", new[] { "sysaccess_updated_by" });
			DropIndex("dbo.sysaccess", new[] { "sysaccess_created_by" });
			DropIndex("dbo.sysparam", new[] { "sysparam_updated_by" });
			DropIndex("dbo.sysparam", new[] { "sysparam_created_by" });
			DropIndex("dbo.regiontemplate", new[] { "regiontemplate_updated_by" });
			DropIndex("dbo.regiontemplate", new[] { "regiontemplate_created_by" });
			DropIndex("dbo.pagetemplate", new[] { "pagetemplate_updated_by" });
			DropIndex("dbo.pagetemplate", new[] { "pagetemplate_created_by" });
			DropIndex("dbo.page", new[] { "page_updated_by" });
			DropIndex("dbo.page", new[] { "page_created_by" });
			DropIndex("dbo.page", new[] { "page_permalink_id" });
			DropIndex("dbo.page", new[] { "page_template_id" });
			DropIndex("dbo.region", new[] { "region_updated_by" });
			DropIndex("dbo.region", new[] { "region_created_by" });
			DropIndex("dbo.region", new[] { "region_regiontemplate_id" });
			DropIndex("dbo.region", new[] { "region_page_id", "region_page_draft" });
			DropIndex("dbo.content", new[] { "content_updated_by" });
			DropIndex("dbo.content", new[] { "content_created_by" });
			DropIndex("dbo.content", new[] { "content_permalink_id" });
			DropIndex("dbo.syslog", new[] { "syslog_updated_by" });
			DropIndex("dbo.syslog", new[] { "syslog_created_by" });
			DropIndex("dbo.sysgroup", new[] { "sysgroup_updated_by" });
			DropIndex("dbo.sysgroup", new[] { "sysgroup_created_by" });
			DropIndex("dbo.extension", new[] { "extension_updated_by" });
			DropIndex("dbo.extension", new[] { "extension_created_by" });
			DropIndex("dbo.comment", new[] { "comment_updated_by" });
			DropIndex("dbo.comment", new[] { "comment_created_by" });
			DropIndex("dbo.namespace", new[] { "namespace_updated_by" });
			DropIndex("dbo.namespace", new[] { "namespace_created_by" });
			DropIndex("dbo.permalink", "index_permalink_name");
			DropIndex("dbo.category", new[] { "category_updated_by" });
			DropIndex("dbo.category", new[] { "category_created_by" });
			DropIndex("dbo.category", new[] { "category_permalink_id" });
			DropTable("dbo.upload");
			DropTable("dbo.sitetree");
			DropTable("dbo.relation");
			DropTable("dbo.property");
			DropTable("dbo.posttemplate");
			DropTable("dbo.post");
			DropTable("dbo.sysaccess");
			DropTable("dbo.sysparam");
			DropTable("dbo.regiontemplate");
			DropTable("dbo.pagetemplate");
			DropTable("dbo.page");
			DropTable("dbo.region");
			DropTable("dbo.content");
			DropTable("dbo.syslog");
			DropTable("dbo.sysgroup");
			DropTable("dbo.extension");
			DropTable("dbo.comment");
			DropTable("dbo.namespace");
			DropTable("dbo.permalink");
			DropTable("dbo.sysuser");
			DropTable("dbo.category");
		}
	}
}
