IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [LastLogin] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BirthDate', N'Email', N'FirstName', N'IsActive', N'LastLogin', N'LastName', N'Password') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [Address], [BirthDate], [Email], [FirstName], [IsActive], [LastLogin], [LastName], [Password])
VALUES (1, N'Address', '1998-01-01T00:00:00.0000000', N'lameyiy197@gholar.com', N'FirstName', CAST(1 AS bit), NULL, N'LastName', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BirthDate', N'Email', N'FirstName', N'IsActive', N'LastLogin', N'LastName', N'Password') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241229171111_InitialCreate', N'9.0.0');

COMMIT;
GO

