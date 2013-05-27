--
-- Database version 31 Update script
--
-- 2013-05-24
--
-- Adds support for using an external membership provider.

ALTER TABLE [sysuser] ADD [sysuser_external_id] NVARCHAR(128) NULL;
