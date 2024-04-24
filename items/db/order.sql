DROP TABLE IF EXISTS "order_status" CASCADE;
CREATE TABLE "order_status"
(
    id   integer     NOT NULL,
    name varchar(50) NOT NULL,
    CONSTRAINT order_status_pk PRIMARY KEY (id),
    CONSTRAINT order_status_unique UNIQUE (name)
);

INSERT INTO "order_status" (id, name)
VALUES (1, 'Active');
INSERT INTO "order_status" (id, name)
VALUES (2, 'Completed');
INSERT INTO "order_status" (id, name)
VALUES (3, 'Cancelled');

DROP TABLE IF EXISTS "order_notification_status" CASCADE;
CREATE TABLE "order_notification_status"
(
    id   integer     NOT NULL,
    name varchar(50) NOT NULL,
    CONSTRAINT order_notification_status_pk PRIMARY KEY (id),
    CONSTRAINT order_notification_status_unique UNIQUE (name)
);

INSERT INTO "order_notification_status" (id, name)
VALUES (1, 'Waiting');
INSERT INTO "order_notification_status" (id, name)
VALUES (2, 'Completed');
INSERT INTO "order_notification_status" (id, name)
VALUES (3, 'Failed');


DROP TABLE IF EXISTS "order" CASCADE;

CREATE TABLE "order"
(
    id                 uuid        NOT NULL,
    user_id            uuid        NOT NULL,
    day_of_month       integer     NOT NULL,
    amount             money       NOT NULL,
    order_status_id    integer     NOT NULL,
    created_date       timestamp   NOT NULL,
    created_by         varchar(50) NOT NULL,
    last_modified_date timestamp   NULL,
    last_modified_by   varchar(50) NULL,
    valid_for          timestamp   NULL,
    CONSTRAINT order_pk PRIMARY KEY (id)
);

INSERT INTO "order" (id, user_id, day_of_month, amount, order_status_id, created_date, created_by, last_modified_date,
                     last_modified_by, valid_for)
VALUES ('531d95f4-ffd8-4e28-a422-fe4fd9e585ca', '78000985-c789-439a-9714-821f36c9c051', 7, 500, 1,
        '2023-02-04 13:02:50.477296', 'sinan.akyazici', null, null, null);

DROP TABLE IF EXISTS "order_channel" CASCADE;
CREATE TABLE "order_channel"
(
    id                 uuid        NOT NULL,
    order_id           uuid        NOT NULL,
    channel_id         integer     NOT NULL,
    created_date       timestamp   NOT NULL,
    created_by         varchar(50) NOT NULL,
    last_modified_date timestamp   NULL,
    last_modified_by   varchar(50) NULL,
    valid_for          timestamp   NULL,
    CONSTRAINT order_channel_pk PRIMARY KEY (id)
);

INSERT INTO "order_channel" (id, order_id, channel_id, created_date, created_by, last_modified_date, last_modified_by,
                             valid_for)
VALUES ('a231beb9-9c5b-48e8-a3a1-c458a1a31f0e', '531d95f4-ffd8-4e28-a422-fe4fd9e585ca', 1, '2023-02-04 13:02:50.477296',
        'sinan.akyazici', null, null, null);



DROP TABLE IF EXISTS "order_notification" CASCADE;
CREATE TABLE "order_notification"
(
    id                           uuid         NOT NULL,
    order_id                     uuid         NOT NULL,
    user_id                      uuid         NOT NULL,
    channel_id                   integer      NOT NULL,
    order_notification_status_id integer      NOT NULL,
    text                         varchar(500) NOT NULL,
    created_date                 timestamp    NOT NULL,
    created_by                   varchar(50)  NOT NULL,
    last_modified_date           timestamp    NULL,
    last_modified_by             varchar(50)  NULL,
    valid_for                    timestamp    NULL,
    CONSTRAINT order_notification_pk PRIMARY KEY (id)
);

INSERT INTO "order_notification" (id, order_id, user_id, channel_id, order_notification_status_id, text, created_date,
                             created_by, last_modified_date, last_modified_by, valid_for)
VALUES ('8e5f31e6-fb36-4da2-b0fd-6b1bf031dfaf', '531d95f4-ffd8-4e28-a422-fe4fd9e585ca',
        '78000985-c789-439a-9714-821f36c9c051', 1, 1, 'Test mesaj', '2023-02-04 13:02:50.477296', 'sinan.akyazici',
        null, null, null);
