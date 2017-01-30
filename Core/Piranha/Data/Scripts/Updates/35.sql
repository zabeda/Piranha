--
-- Database version 35 Update script
--
-- 2016-10-10
--
-- Adds support reference media.

ALTER TABLE [content] ADD [content_reference] BIT NOT NULL default(0);