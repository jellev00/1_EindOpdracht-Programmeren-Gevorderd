USE [GentseFeestenDB]
GO

/****** Object: Table [dbo].[Evenementen] Script Date: 26/05/2023 19:18:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Evenementen] (
    [Id]           VARCHAR (50)  NOT NULL,
    [Titel]        VARCHAR (MAX) NULL,
    [Eindtijd]     DATETIME      NULL,
    [Starttijd]    DATETIME      NULL,
    [Prijs]        DECIMAL (18)  NULL,
    [Beschrijving] VARCHAR (MAX) NULL
);


