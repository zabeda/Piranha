--
-- Database version 5 Update script
--
-- 2012-05-03

INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('3B074724-69DF-4165-81D8-2157B25FE96F', '7c536b66-d292-4369-8f37-948b32229b83', 
	'ADMIN_EXTERNAL', 'Access to configure connections to external systems.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
