CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "Orders" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Cost" numeric NOT NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251021154121_AddOrdersTable', '9.0.10');

COMMIT;

