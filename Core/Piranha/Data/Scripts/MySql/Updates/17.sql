--
-- Database version 17 Update script
--
-- 2012-12-06
--
-- Adds the comment table to the database.

CREATE TABLE comment (
	comment_id CHAR(38) NOT NULL,
	comment_parent_id CHAR(38) NOT NULL,
	comment_approved BIT DEFAULT(1),
	comment_title VARCHAR(64) NULL,
	comment_body TEXT NOT NULL,
	comment_author_name VARCHAR(128) NULL,
	comment_author_email VARCHAR(128) NULL,
	comment_created DATETIME NOT NULL,
	comment_updated DATETIME NOT NULL,
	comment_created_by CHAR(38) NULL,
	comment_updated_by CHAR(38) NULL,
	CONSTRAINT pk_comment_id PRIMARY KEY (comment_id),
	CONSTRAINT fk_comment_created_by FOREIGN KEY (comment_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_comment_updated_by FOREIGN KEY (comment_updated_by) REFERENCES sysuser (sysuser_id)
);
