CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "SportFields" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_SportFields" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Type" TEXT NOT NULL,
    "IsIndoor" INTEGER NOT NULL,
    "Capacity" INTEGER NOT NULL,
    "Description" TEXT NULL
);

CREATE TABLE "Reservations" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Reservations" PRIMARY KEY AUTOINCREMENT,
    "CustomerName" TEXT NOT NULL,
    "CustomerContact" TEXT NOT NULL,
    "FieldId" INTEGER NOT NULL,
    "Date" TEXT NOT NULL,
    "StartHour" INTEGER NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    CONSTRAINT "FK_Reservations_SportFields_FieldId" FOREIGN KEY ("FieldId") REFERENCES "SportFields" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Reservations_FieldId" ON "Reservations" ("FieldId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260112224122_InitialCreate', '9.0.0');

COMMIT;

