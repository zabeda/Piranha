--
-- Database version 37 Update script
--
-- 2017-01-31
--
-- Adds support for cropping options

ALTER TABLE [content] ADD [content_cropping_vertical] NVARCHAR(128) NULL default('Center');
ALTER TABLE [content] ADD [content_cropping_horizontal] NVARCHAR(128) NULL default('Center');