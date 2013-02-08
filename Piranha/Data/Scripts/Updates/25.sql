--
-- Database version 25 Update script
--
-- 2013-02-06
--
-- Adds more features to the site tree

ALTER TABLE [sitetree] ADD [sitetree_hostname] NVARCHAR(128) NULL;
