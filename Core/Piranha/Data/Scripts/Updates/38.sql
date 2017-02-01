--
-- Database version 38 Update script
--
-- 2017-02-01
--
-- Adds missing default values for cropping alignment

UPDATE [content] SET [content_cropping_vertical] = 'Center' WHERE [content_cropping_vertical] IS NULL;
UPDATE [content] SET [content_cropping_horizontal] = 'Center' WHERE [content_cropping_horizontal] IS NULL;
