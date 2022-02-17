
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/19/2013 13:36:42
-- Generated from EDMX file: E:\Work_freelance\telemetry_TMTLauncher\Sources\trunk\ShopEngine\DBAccess\DBApps.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_Telemetry_ShopApps];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Messages_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Messages_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAppsBOBApps_tblAppsBOBCategories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAppsBOBApps] DROP CONSTRAINT [FK_tblAppsBOBApps_tblAppsBOBCategories];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAppsBOBApp_tblUsersM2MUserApp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblUsersM2MUserApps] DROP CONSTRAINT [FK_tblAppsBOBApp_tblUsersM2MUserApp];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAppsBOBCategory_tblAppsBOBApp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAppsBOBApps] DROP CONSTRAINT [FK_tblAppsBOBCategory_tblAppsBOBApp];
GO
IF OBJECT_ID(N'[dbo].[FK_User_tblUsersM2MUserApp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblUsersM2MUserApps] DROP CONSTRAINT [FK_User_tblUsersM2MUserApp];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Review]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Review];
GO
IF OBJECT_ID(N'[dbo].[tblAppsBOBApps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAppsBOBApps];
GO
IF OBJECT_ID(N'[dbo].[tblAppsBOBCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAppsBOBCategories];
GO
IF OBJECT_ID(N'[dbo].[tblUsersM2MUserApps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUsersM2MUserApps];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Date] datetime  NOT NULL,
    [Name] varchar(1000)  NOT NULL,
    [Text] varchar(max)  NOT NULL,
    [id_User] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(1000)  NOT NULL,
    [ImageURL] varchar(1000)  NULL,
    [Text] varchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'Review'
CREATE TABLE [dbo].[Review] (
    [RevID] int IDENTITY(1,1) NOT NULL,
    [bylogin] varchar(200)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Message] varchar(1024)  NOT NULL,
    [id_App] int  NOT NULL
);
GO

-- Creating table 'tblAppsBOBApps'
CREATE TABLE [dbo].[tblAppsBOBApps] (
    [id] int IDENTITY(1,1) NOT NULL,
    [AppName] varchar(200)  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [Icon] varchar(200)  NOT NULL,
    [Banner250x250URL] varchar(200)  NULL,
    [Banner100x100URL] varchar(200)  NULL,
    [ScreenshotsURL] varchar(200)  NULL,
    [ActualFileName] varchar(200)  NOT NULL,
    [id_Category] int  NULL,
    [counterInstall] int  NULL,
    [rate] decimal(9,3)  NOT NULL,
    [CoutRated] int  NOT NULL,
    [SumAllRate] int  NOT NULL,
    [ActualVersion] float  NOT NULL,
    [ScreenshotsCount] int  NOT NULL,
    [ImageShortcut] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'tblAppsBOBCategories'
CREATE TABLE [dbo].[tblAppsBOBCategories] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(200)  NOT NULL
);
GO

-- Creating table 'tblUsersM2MUserApps'
CREATE TABLE [dbo].[tblUsersM2MUserApps] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_User] int  NOT NULL,
    [id_App] int  NOT NULL,
    [IsUserRated] bit  NOT NULL,
    [CurrentVersion] float  NOT NULL,
    [CurrentRate] decimal(18,0)  NOT NULL,
    [DateTime] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [UID] varchar(100)  NOT NULL,
    [LastReadedNewsID] int  NULL,
    [LastReadedMessageID] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [RevID] in table 'Review'
ALTER TABLE [dbo].[Review]
ADD CONSTRAINT [PK_Review]
    PRIMARY KEY CLUSTERED ([RevID] ASC);
GO

-- Creating primary key on [id] in table 'tblAppsBOBApps'
ALTER TABLE [dbo].[tblAppsBOBApps]
ADD CONSTRAINT [PK_tblAppsBOBApps]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tblAppsBOBCategories'
ALTER TABLE [dbo].[tblAppsBOBCategories]
ADD CONSTRAINT [PK_tblAppsBOBCategories]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tblUsersM2MUserApps'
ALTER TABLE [dbo].[tblUsersM2MUserApps]
ADD CONSTRAINT [PK_tblUsersM2MUserApps]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_User] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Messages_Users]
    FOREIGN KEY ([id_User])
    REFERENCES [dbo].[Users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Messages_Users'
CREATE INDEX [IX_FK_Messages_Users]
ON [dbo].[Messages]
    ([id_User]);
GO

-- Creating foreign key on [id_Category] in table 'tblAppsBOBApps'
ALTER TABLE [dbo].[tblAppsBOBApps]
ADD CONSTRAINT [FK_tblAppsBOBApps_tblAppsBOBCategories]
    FOREIGN KEY ([id_Category])
    REFERENCES [dbo].[tblAppsBOBCategories]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAppsBOBApps_tblAppsBOBCategories'
CREATE INDEX [IX_FK_tblAppsBOBApps_tblAppsBOBCategories]
ON [dbo].[tblAppsBOBApps]
    ([id_Category]);
GO

-- Creating foreign key on [id_App] in table 'tblUsersM2MUserApps'
ALTER TABLE [dbo].[tblUsersM2MUserApps]
ADD CONSTRAINT [FK_tblAppsBOBApp_tblUsersM2MUserApp]
    FOREIGN KEY ([id_App])
    REFERENCES [dbo].[tblAppsBOBApps]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAppsBOBApp_tblUsersM2MUserApp'
CREATE INDEX [IX_FK_tblAppsBOBApp_tblUsersM2MUserApp]
ON [dbo].[tblUsersM2MUserApps]
    ([id_App]);
GO

-- Creating foreign key on [id_Category] in table 'tblAppsBOBApps'
ALTER TABLE [dbo].[tblAppsBOBApps]
ADD CONSTRAINT [FK_tblAppsBOBCategory_tblAppsBOBApp]
    FOREIGN KEY ([id_Category])
    REFERENCES [dbo].[tblAppsBOBCategories]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAppsBOBCategory_tblAppsBOBApp'
CREATE INDEX [IX_FK_tblAppsBOBCategory_tblAppsBOBApp]
ON [dbo].[tblAppsBOBApps]
    ([id_Category]);
GO

-- Creating foreign key on [id_User] in table 'tblUsersM2MUserApps'
ALTER TABLE [dbo].[tblUsersM2MUserApps]
ADD CONSTRAINT [FK_User_tblUsersM2MUserApp]
    FOREIGN KEY ([id_User])
    REFERENCES [dbo].[Users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_User_tblUsersM2MUserApp'
CREATE INDEX [IX_FK_User_tblUsersM2MUserApp]
ON [dbo].[tblUsersM2MUserApps]
    ([id_User]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------