	--
-- Database version 27 Update script
--
-- 2013-02-27
--
-- Adds property templates

CREATE TABLE [propertytemplate] (
	[propertytemplate_id] UNIQUEIDENTIFIER NOT NULL,
	[propertytemplate_template_id] UNIQUEIDENTIFIER NOT NULL,
	[propertytemplate_internal_id] NVARCHAR(32) NOT NULL,
	[propertytemplate_seqno] INT NOT NULL default(1),
	[propertytemplate_name] NVARCHAR(64) NOT NULL,
	[propertytemplate_description] NVARCHAR(255) NULL,
	[propertytemplate_type] NVARCHAR(255) NOT NULL,
	[propertytemplate_created] DATETIME NOT NULL,
	[propertytemplate_updated] DATETIME NOT NULL,
	[propertytemplate_created_by] UNIQUEIDENTIFIER NOT NULL,
	[propertytemplate_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_propertytemplate_id PRIMARY KEY ([propertytemplate_id]),
	CONSTRAINT fk_propertytemplate_created_by FOREIGN KEY ([propertytemplate_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_propertytemplate_updated_by FOREIGN KEY ([propertytemplate_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);