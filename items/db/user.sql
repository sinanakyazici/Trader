DROP TABLE IF EXISTS "user" CASCADE;

CREATE TABLE "user" (
	id uuid NOT NULL,
	username varchar(100) NOT NULL,
	name varchar(100) NOT NULL,
	surname varchar(100) NOT NULL,
	gsm_phone varchar(100) NOT NULL,
	email varchar(100) NOT NULL,

	created_date timestamp NOT NULL,
	created_by varchar(50) NOT NULL,
	last_modified_date timestamp NULL,
	last_modified_by varchar(50) NULL,
	valid_for timestamp NULL,
	CONSTRAINT user_pk PRIMARY KEY (id)
);

INSERT INTO "user" (id, username, name, surname, gsm_phone, email, created_date, created_by, last_modified_date, last_modified_by, valid_for) 
VALUES ('78000985-c789-439a-9714-821f36c9c051', 'sinan.akyazici', 'Sinan', 'AKYAZICI', '+905559876541', 'sinanakyazici@admin.com', '2023-02-04 13:02:50.477296', 'sinan.akyazici', null, null, null);