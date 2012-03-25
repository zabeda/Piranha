-- Default users
INSERT INTO sysuser (sysuser_id, sysuser_login, sysuser_group_id, sysuser_created, sysuser_updated)
VALUES ('ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'sys', '7c536b66-d292-4369-8f37-948b32229b83',
	GETDATE(), GETDATE());

-- Default groups
INSERT INTO sysgroup (sysgroup_id, sysgroup_parent_id, sysgroup_name, sysgroup_description, sysgroup_created,
	sysgroup_updated, sysgroup_created_by, sysgroup_updated_by)
VALUES ('7c536b66-d292-4369-8f37-948b32229b83', NULL, 'Systemadministrator', 'System administrator group with full permissions.',
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysgroup (sysgroup_id, sysgroup_parent_id, sysgroup_name, sysgroup_description, sysgroup_created,
	sysgroup_updated, sysgroup_created_by, sysgroup_updated_by)
VALUES ('8940b41a-e3a9-44f3-b564-bfd281416141', '7c536b66-d292-4369-8f37-948b32229b83', 'Administrator', 
	'Web site administrator group.', GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

-- Default access
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('4fbdedb7-10ec-4a10-8f82-7d4c5cf61f2c', '8940b41a-e3a9-44f3-b564-bfd281416141', 'ADMIN', 
	'Access to login to the admin panel.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('00074fd5-6c81-4181-8a09-ba6ef94f8364', '7c536b66-d292-4369-8f37-948b32229b83', 
	'ADMIN_PAGE_TEMPLATE', 'Access to add, update and delete page types.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('ff296d65-d24d-446a-8f02-d93a7ab57086', '7c536b66-d292-4369-8f37-948b32229b83', 
	'ADMIN_POST_TEMPLATE', 'Access to add, update and delete post types.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('0c19578a-d6c0-45f8-9ffd-bcffa5d84772', '7c536b66-d292-4369-8f37-948b32229b83', 
	'ADMIN_PARAM', 'Access to add, update and delete system parameters.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('0f367b04-ef7b-4007-88bd-7d78cbdea64a', '7c536b66-d292-4369-8f37-948b32229b83', 
	'ADMIN_ACCESS', 'Access to add, update and delete access rules.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('08d17dbf-cd1d-40a9-b558-0866210ac4ec', '8940b41a-e3a9-44f3-b564-bfd281416141', 
	'ADMIN_GROUP', 'Access to add, update and delete user groups.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('36fbc1ad-4e17-4767-9fdc-af92802e5ebb', '8940b41a-e3a9-44f3-b564-bfd281416141', 
	'ADMIN_PAGE', 'Access to add, update and delete pages.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('c8b44826-d3e6-4add-b241-8ce95429a17e', '8940b41a-e3a9-44f3-b564-bfd281416141', 
	'ADMIN_POST', 'Access to add, update and delete posts.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('79ED0E9E-188C-4C5B-81BA-DB15BB9F8AD5', '8940b41a-e3a9-44f3-b564-bfd281416141', 
	'ADMIN_CATEGORY', 'Access to add, update and delete categories.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('E08AE820-D438-4A38-B6E1-AC3ACA3CF933', '8940b41a-e3a9-44f3-b564-bfd281416141', 
	'ADMIN_CONTENT', 'Access to add, update and delete images & documents.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('8a4ca0f3-261b-4689-8c1f-98065b65f9ee', '8940b41a-e3a9-44f3-b564-bfd281416141', 
	'ADMIN_USER', 'Access to add, update and delete users.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

-- Default params
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('9a14664f-806d-4a4f-9a72-e8368fb358d5', 'SITE_VERSION', '3', 'The currently installed version of Piranha.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('EBB65F0A-F2CA-4932-B590-C899922DE847', 'SITE_TITLE', '', 'The site title.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('160C9971-3D04-40AA-A2A3-B25F11D11D29', 'SITE_DESCRIPTION', '', 'The site description', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('CF06BF4C-C426-4047-8E5E-6E0082AAF1BF', 'SITE_LAST_MODIFIED', GETDATE(), 'Global last modification date.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('C08B1891-ABA2-4E0D-AD61-2ABDFBA81A59', 'CACHE_PUBLIC_EXPIRES', 30, 'How many minutes browsers are allowed to cache public content.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('48BDF688-BA95-46B4-91C7-6A8430F387FF', 'CACHE_PUBLIC_MAXAGE', 30, 'How many minutes cached content is valid in the browser.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('08E8A582-7825-43B2-A12D-2522889F04BE', 'IMAGE_MAX_WIDTH', 940, 'Maximum width for uploaded images.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

-- Default templates
INSERT INTO pagetemplate (pagetemplate_id, pagetemplate_name, pagetemplate_description, pagetemplate_page_regions,
	pagetemplate_preview, pagetemplate_created, pagetemplate_updated, pagetemplate_created_by, pagetemplate_updated_by)
VALUES ('906761ea-6c04-4f4b-9365-f2c350ff4372', 'Standard page,Standard pages', 'Standard page type.',
	'Content', '<table class="template"><tr><td id="Content"></td></tr></table>', GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO posttemplate (posttemplate_id, posttemplate_name, posttemplate_description, posttemplate_preview,
	posttemplate_created, posttemplate_updated, posttemplate_created_by, posttemplate_updated_by)
VALUES ('5017dbe4-5685-4941-921b-ca922edc7a12', 'Standard post,Standard posts', 'Standard post type.', 
	'<table class="template"><tr><td></td></tr></table>', GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

-- Default page
INSERT INTO page (page_id, page_draft, page_template_id, page_seqno, page_title, page_keywords, page_description,
	page_created, page_updated, page_published, page_last_published, page_created_by, page_updated_by)
VALUES ('7849b6d6-dc43-43f6-8b5a-5770ab89fbcf', 1, '906761ea-6c04-4f4b-9365-f2c350ff4372', 1, 'Start', 
	'Piranha, Welcome', 'Welcome to Piranha', GETDATE(), GETDATE(), GETDATE(), GETDATE(),
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO page (page_id, page_draft, page_template_id, page_seqno, page_title, page_keywords, page_description,
	page_created, page_updated, page_published, page_last_published, page_created_by, page_updated_by)
VALUES ('7849b6d6-dc43-43f6-8b5a-5770ab89fbcf', 0, '906761ea-6c04-4f4b-9365-f2c350ff4372', 1, 'Start', 
	'Piranha, Welcome', 'Welcome to Piranha', GETDATE(), GETDATE(), GETDATE(), GETDATE(),
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

-- Permalink
INSERT INTO permalink (permalink_id, permalink_parent_id, permalink_type, permalink_name, permalink_created,
	permalink_updated, permalink_created_by, permalink_updated_by)
VALUES ('1e64c1d4-e24f-4c7c-8f61-f3a75ad2e2fe', '7849b6d6-dc43-43f6-8b5a-5770ab89fbcf', 'PAGE', 'start',
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

-- Region
INSERT INTO region (region_id, region_draft, region_page_id, region_page_draft, region_name, region_body, 
	region_created, region_updated, region_created_by, region_updated_by)
VALUES ('87ec4dbd-c3ba-4a6b-af49-78421528c363', 1, '7849b6d6-dc43-43f6-8b5a-5770ab89fbcf', 1, 'Content',
	'<p>Welcome to Piranha -  the fun, fast and lightweight framework for developing cms-based web applications with an extra bite.</p>', GETDATE(), GETDATE(),
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO region (region_id, region_draft, region_page_id, region_page_draft, region_name, region_body, 
	region_created, region_updated, region_created_by, region_updated_by)
VALUES ('87ec4dbd-c3ba-4a6b-af49-78421528c363', 0, '7849b6d6-dc43-43f6-8b5a-5770ab89fbcf', 0, 'Content',
	'<p>Welcome to Piranha -  the fun, fast and lightweight framework for developing cms-based web applications with an extra bite.</p>', GETDATE(), GETDATE(),
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
