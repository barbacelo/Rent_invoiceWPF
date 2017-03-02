
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/28/2017 12:10:15
-- Generated from EDMX file: C:\Users\shaw_d\Documents\GitHub\Helpmee\WpfApplication3\Model1.edmx
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
IF OBJECT_ID(N'[dbo].[racuni]', 'U') IS NOT NULL
    DROP TABLE [dbo].[racuni];
GO
IF OBJECT_ID(N'[dbo].[roba]', 'U') IS NOT NULL
    DROP TABLE [dbo].[roba];
GO
IF OBJECT_ID(N'[dbo].[revroba]', 'U') IS NOT NULL
    DROP TABLE [dbo].[revroba];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'kupci'
CREATE TABLE [dbo].[kupci] (
    [idbroj] int IDENTITY(1,1) NOT NULL,
    [ime] char(40)  NOT NULL,
    [jmbg] char(13)  NOT NULL,
    [adresa] char(40)  NOT NULL,
    [mesto] char(25)  NOT NULL,
    [telefon] char(15)  NOT NULL,
    [dug] decimal(12,2)  NOT NULL,
    [pot] decimal(12,2)  NOT NULL,
    [saldo] decimal(13,2)  NULL
);
GO

-- Creating table 'racuni'
CREATE TABLE [dbo].[racuni] (
    [brev] char(10)  NOT NULL,
    [datum] datetime  NOT NULL,
    [idbrojk] int  NOT NULL,
    [godina] int  NULL,
    [pk] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'roba'
CREATE TABLE [dbo].[roba] (
    [idbroj] int IDENTITY(1,1) NOT NULL,
    [naziv] char(40)  NOT NULL,
    [jm] char(3)  NOT NULL,
    [kol] decimal(6,0)  NOT NULL,
    [zaliha] decimal(6,0)  NOT NULL,
    [cena] decimal(9,2)  NOT NULL
);
GO

-- Creating table 'revroba'
CREATE TABLE [dbo].[revroba] (
    [pk] int  NOT NULL,
    [brev] int  NOT NULL,
    [datum] datetime  NOT NULL,
    [idbrojr] int  NOT NULL,
    [kolu] decimal(6,0)  NULL,
    [kolv] decimal(6,0)  NULL,
    [utro] decimal(6,0)  NULL,
    [cena] decimal(9,2)  NOT NULL
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

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idbroj] in table 'kupci'
ALTER TABLE [dbo].[kupci]
ADD CONSTRAINT [PK_kupci]
    PRIMARY KEY CLUSTERED ([idbroj] ASC);
GO

-- Creating primary key on [pk] in table 'racuni'
ALTER TABLE [dbo].[racuni]
ADD CONSTRAINT [PK_racuni]
    PRIMARY KEY CLUSTERED ([pk] ASC);
GO

-- Creating primary key on [idbroj] in table 'roba'
ALTER TABLE [dbo].[roba]
ADD CONSTRAINT [PK_roba]
    PRIMARY KEY CLUSTERED ([idbroj] ASC);
GO

-- Creating primary key on [pk] in table 'revroba'
ALTER TABLE [dbo].[revroba]
ADD CONSTRAINT [PK_revroba]
    PRIMARY KEY CLUSTERED ([pk] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idbrojk] in table 'racuni'
ALTER TABLE [dbo].[racuni]
ADD CONSTRAINT [FK_racuni_kupci]
    FOREIGN KEY ([idbrojk])
    REFERENCES [dbo].[kupci]
        ([idbroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_racuni_kupci'
CREATE INDEX [IX_FK_racuni_kupci]
ON [dbo].[racuni]
    ([idbrojk]);
GO

-- Creating foreign key on [brev] in table 'revroba'
ALTER TABLE [dbo].[revroba]
ADD CONSTRAINT [FK_RevRoba_Racuni]
    FOREIGN KEY ([brev])
    REFERENCES [dbo].[racuni]
        ([pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RevRoba_Racuni'
CREATE INDEX [IX_FK_RevRoba_Racuni]
ON [dbo].[revroba]
    ([brev]);
GO

-- Creating foreign key on [idbrojr] in table 'revroba'
ALTER TABLE [dbo].[revroba]
ADD CONSTRAINT [FK_RevRoba_roba]
    FOREIGN KEY ([idbrojr])
    REFERENCES [dbo].[roba]
        ([idbroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RevRoba_roba'
CREATE INDEX [IX_FK_RevRoba_roba]
ON [dbo].[revroba]
    ([idbrojr]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------