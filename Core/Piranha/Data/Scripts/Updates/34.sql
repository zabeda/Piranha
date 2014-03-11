--
-- Database version 34 Update script
--
-- 2014-03-11
--
-- Increases the max length of the post excerpt

ALTER TABLE [post] ALTER COLUMN [post_excerpt] NVARCHAR(512) NULL;
