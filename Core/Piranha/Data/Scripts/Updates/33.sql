--
-- Database version 33 Update script
--
-- 2014-03-03
--
-- Removes all foreign key constraints to the sysuser

ALTER TABLE [syslog] DROP CONSTRAINT [fk_syslog_created_by];
ALTER TABLE [syslog] DROP CONSTRAINT [fk_syslog_updated_by];
ALTER TABLE [syslog] DROP COLUMN [syslog_updated_by];
ALTER TABLE [syslog] ALTER COLUMN [syslog_created_by] NVARCHAR(128) NULL;

ALTER TABLE [sysuser] DROP COLUMN [sysuser_created_by];
ALTER TABLE [sysuser] DROP COLUMN [sysuser_updated_by];

ALTER TABLE [sysgroup] DROP CONSTRAINT [fk_sysgroup_created_by];
ALTER TABLE [sysgroup] DROP CONSTRAINT [fk_sysgroup_updated_by];
ALTER TABLE [sysgroup] DROP COLUMN [sysgroup_created_by];
ALTER TABLE [sysgroup] DROP COLUMN [sysgroup_updated_by];

ALTER TABLE [sysaccess] DROP CONSTRAINT [fk_sysaccess_created_by];
ALTER TABLE [sysaccess] DROP CONSTRAINT [fk_sysaccess_updated_by];
ALTER TABLE [sysaccess] DROP COLUMN [sysaccess_created_by];
ALTER TABLE [sysaccess] DROP COLUMN [sysaccess_updated_by];

ALTER TABLE [sysparam] DROP CONSTRAINT [fk_sysparam_created_by];
ALTER TABLE [sysparam] DROP CONSTRAINT [fk_sysparam_updated_by];
ALTER TABLE [sysparam] DROP COLUMN [sysparam_created_by];
ALTER TABLE [sysparam] DROP COLUMN [sysparam_updated_by];

ALTER TABLE [namespace] DROP CONSTRAINT [fk_namespace_created_by];
ALTER TABLE [namespace] DROP CONSTRAINT [fk_namespace_updated_by];
ALTER TABLE [namespace] DROP COLUMN [namespace_created_by];
ALTER TABLE [namespace] DROP COLUMN [namespace_updated_by];

ALTER TABLE [sitetree] DROP CONSTRAINT [fk_sitetree_created_by];
ALTER TABLE [sitetree] DROP CONSTRAINT [fk_sitetree_updated_by];
ALTER TABLE [sitetree] DROP COLUMN [sitetree_created_by];
ALTER TABLE [sitetree] DROP COLUMN [sitetree_updated_by];

ALTER TABLE [permalink] DROP COLUMN [permalink_created_by];
ALTER TABLE [permalink] DROP COLUMN [permalink_updated_by];

ALTER TABLE [pagetemplate] DROP CONSTRAINT [fk_pagetemplate_created_by];
ALTER TABLE [pagetemplate] DROP CONSTRAINT [fk_pagetemplate_updated_by];
ALTER TABLE [pagetemplate] DROP COLUMN [pagetemplate_created_by];
ALTER TABLE [pagetemplate] DROP COLUMN [pagetemplate_updated_by];

ALTER TABLE [posttemplate] DROP CONSTRAINT [fk_posttemplate_created_by];
ALTER TABLE [posttemplate] DROP CONSTRAINT [fk_posttemplate_updated_by];
ALTER TABLE [posttemplate] DROP COLUMN [posttemplate_created_by];
ALTER TABLE [posttemplate] DROP COLUMN [posttemplate_updated_by];

ALTER TABLE [category] DROP CONSTRAINT [fk_category_created_by];
ALTER TABLE [category] DROP CONSTRAINT [fk_category_updated_by];
ALTER TABLE [category] DROP COLUMN [category_created_by];
ALTER TABLE [category] DROP COLUMN [category_updated_by];

ALTER TABLE [page] DROP CONSTRAINT [fk_page_created_by];
ALTER TABLE [page] DROP CONSTRAINT [fk_page_updated_by];
ALTER TABLE [page] DROP COLUMN [page_created_by];
ALTER TABLE [page] DROP COLUMN [page_updated_by];

ALTER TABLE [regiontemplate] DROP CONSTRAINT [fk_regiontemplate_created_by];
ALTER TABLE [regiontemplate] DROP CONSTRAINT [fk_regiontemplate_updated_by];
ALTER TABLE [regiontemplate] DROP COLUMN [regiontemplate_created_by];
ALTER TABLE [regiontemplate] DROP COLUMN [regiontemplate_updated_by];

ALTER TABLE [region] DROP CONSTRAINT [fk_region_created_by];
ALTER TABLE [region] DROP CONSTRAINT [fk_region_updated_by];
ALTER TABLE [region] DROP COLUMN [region_created_by];
ALTER TABLE [region] DROP COLUMN [region_updated_by];

ALTER TABLE [property] DROP CONSTRAINT [fk_property_created_by];
ALTER TABLE [property] DROP CONSTRAINT [fk_property_updated_by];
ALTER TABLE [property] DROP COLUMN [property_created_by];
ALTER TABLE [property] DROP COLUMN [property_updated_by];

ALTER TABLE [post] DROP CONSTRAINT [fk_post_created_by];
ALTER TABLE [post] DROP CONSTRAINT [fk_post_updated_by];
ALTER TABLE [post] DROP COLUMN [post_created_by];
ALTER TABLE [post] DROP COLUMN [post_updated_by];

ALTER TABLE [content] DROP CONSTRAINT [fk_content_created_by];
ALTER TABLE [content] DROP CONSTRAINT [fk_content_updated_by];
ALTER TABLE [content] DROP COLUMN [content_created_by];
ALTER TABLE [content] DROP COLUMN [content_updated_by];

ALTER TABLE [upload] DROP CONSTRAINT [fk_upload_created_by];
ALTER TABLE [upload] DROP CONSTRAINT [fk_upload_updated_by];
ALTER TABLE [upload] DROP COLUMN [upload_created_by];
ALTER TABLE [upload] DROP COLUMN [upload_updated_by];

ALTER TABLE [extension] DROP CONSTRAINT [fk_extension_created_by];
ALTER TABLE [extension] DROP CONSTRAINT [fk_extension_updated_by];
ALTER TABLE [extension] DROP COLUMN [extension_created_by];
ALTER TABLE [extension] DROP COLUMN [extension_updated_by];

ALTER TABLE [comment] DROP CONSTRAINT [fk_comment_created_by];
ALTER TABLE [comment] DROP CONSTRAINT [fk_comment_updated_by];
ALTER TABLE [comment] DROP COLUMN [comment_updated_by];
ALTER TABLE [comment] ALTER COLUMN [comment_created_by] NVARCHAR(128) NULL;
