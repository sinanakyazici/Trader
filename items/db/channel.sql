DROP TABLE IF EXISTS "channel" CASCADE;
CREATE TABLE "channel"
(
    id   integer     NOT NULL,
    name varchar(50) NOT NULL,
    CONSTRAINT channel_pk PRIMARY KEY (id),
    CONSTRAINT channel_unique UNIQUE (name)
);


INSERT INTO "channel" (id, name)
VALUES (1, 'Sms');
INSERT INTO "channel" (id, name)
VALUES (2, 'Email');
INSERT INTO "channel" (id, name)
VALUES (3, 'PushNotification');