--
-- Database version 36 Update script
--
-- 2017-01-12
--
-- Increases excerpt length for posts

ALTER TABLE [post] ALTER COLUMN [post_excerpt] NVARCHAR(512) NULL;
