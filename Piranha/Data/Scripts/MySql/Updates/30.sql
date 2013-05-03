--
-- Database version 30 Update script
--
-- 2013-05-03
--
-- Adds configurable views to pages & posts for MVC applications.

ALTER TABLE pagetemplate ADD pagetemplate_view VARCHAR(128) NULL;
ALTER TABLE pagetemplate ADD pagetemplate_view_show INT NOT NULL default '0';
ALTER TABLE posttemplate ADD posttemplate_view VARCHAR(128) NULL;
ALTER TABLE posttemplate ADD posttemplate_view_show INT NOT NULL default '0';

ALTER TABLE page ADD page_view VARCHAR(128) NULL;
ALTER TABLE post ADD post_view VARCHAR(128) NULL;
