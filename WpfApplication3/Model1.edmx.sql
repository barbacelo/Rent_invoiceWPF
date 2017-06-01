
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/01/2017 16:48:52
-- Generated from EDMX file: C:\Users\shaw_d\Documents\GitHub\Rent_invoiceWPF\WpfApplication3\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [reversi];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_racuni_kupci]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[racuni] DROP CONSTRAINT [FK_racuni_kupci];
GO
IF OBJECT_ID(N'[dbo].[FK_RevRoba_Racuni]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[revroba] DROP CONSTRAINT [FK_RevRoba_Racuni];
GO
IF OBJECT_ID(N'[dbo].[FK_RevRoba_roba]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[revroba] DROP CONSTRAINT [FK_RevRoba_roba];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[kupci]', 'U') IS NOT NULL
    DROP TABLE [dbo].[kupci];
GO
IF OBJECT_ID(N'[dbo].[roba]', 'U') IS NOT NULL
    DROP TABLE [dbo].[roba];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[racuni]', 'U') IS NOT NULL
    DROP TABLE [dbo].[racuni];
GO
IF OBJECT_ID(N'[dbo].[revroba]', 'U') IS NOT NULL
    DROP TABLE [dbo].[revroba];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'kupci'
CREATE TABLE [dbo].[kupci] (
    [KupciID] int IDENTITY(1,1) NOT NULL,
    [Ime] char(40)  NOT NULL,
    [Jmbg] char(13)  NOT NULL,
    [Adresa] char(40)  NOT NULL,
    [Mesto] char(25)  NOT NULL,
    [Telefon] char(15)  NOT NULL,
    [Dug] decimal(12,2)  NOT NULL,
    [Pot] decimal(12,2)  NOT NULL,
    [Saldo] decimal(13,2)  NULL
);
GO

-- Creating table 'roba'
CREATE TABLE [dbo].[roba] (
    [RobaID] int IDENTITY(1,1) NOT NULL,
    [Naziv] char(40)  NOT NULL,
    [Jm] char(3)  NOT NULL,
    [Kol] decimal(6,0)  NOT NULL,
    [Zaliha] decimal(6,0)  NOT NULL,
    [Cena] decimal(9,2)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'racuni'
CREATE TABLE [dbo].[racuni] (
    [Brev] int  NOT NULL,
    [Datum] datetime  NOT NULL,
    [KupciID] int  NOT NULL,
    [RacuniID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'revroba'
CREATE TABLE [dbo].[revroba] (
    [RevRobaID] int IDENTITY(1,1) NOT NULL,
    [RacuniID] int  NOT NULL,
    [Datum] datetime  NOT NULL,
    [RobaID] int  NOT NULL,
    [Kolic] decimal(6,0)  NULL,
    [Utro] int  NULL,
    [Cena] decimal(9,2)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [KupciID] in table 'kupci'
ALTER TABLE [dbo].[kupci]
ADD CONSTRAINT [PK_kupci]
    PRIMARY KEY CLUSTERED ([KupciID] ASC);
GO

-- Creating primary key on [RobaID] in table 'roba'
ALTER TABLE [dbo].[roba]
ADD CONSTRAINT [PK_roba]
    PRIMARY KEY CLUSTERED ([RobaID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [RacuniID] in table 'racuni'
ALTER TABLE [dbo].[racuni]
ADD CONSTRAINT [PK_racuni]
    PRIMARY KEY CLUSTERED ([RacuniID] ASC);
GO

-- Creating primary key on [RevRobaID] in table 'revroba'
ALTER TABLE [dbo].[revroba]
ADD CONSTRAINT [PK_revroba]
    PRIMARY KEY CLUSTERED ([RevRobaID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [KupciID] in table 'racuni'
ALTER TABLE [dbo].[racuni]
ADD CONSTRAINT [FK_racuni_kupci]
    FOREIGN KEY ([KupciID])
    REFERENCES [dbo].[kupci]
        ([KupciID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_racuni_kupci'
CREATE INDEX [IX_FK_racuni_kupci]
ON [dbo].[racuni]
    ([KupciID]);
GO

-- Creating foreign key on [RacuniID] in table 'revroba'
ALTER TABLE [dbo].[revroba]
ADD CONSTRAINT [FK_RevRoba_Racuni]
    FOREIGN KEY ([RacuniID])
    REFERENCES [dbo].[racuni]
        ([RacuniID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RevRoba_Racuni'
CREATE INDEX [IX_FK_RevRoba_Racuni]
ON [dbo].[revroba]
    ([RacuniID]);
GO

-- Creating foreign key on [RobaID] in table 'revroba'
ALTER TABLE [dbo].[revroba]
ADD CONSTRAINT [FK_RevRoba_roba]
    FOREIGN KEY ([RobaID])
    REFERENCES [dbo].[roba]
        ([RobaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RevRoba_roba'
CREATE INDEX [IX_FK_RevRoba_roba]
ON [dbo].[revroba]
    ([RobaID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------