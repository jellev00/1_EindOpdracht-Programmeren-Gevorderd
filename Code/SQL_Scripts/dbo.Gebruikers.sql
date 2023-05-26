USE [GentseFeestenDB]
GO

/****** Object: Table [dbo].[Gebruikers] Script Date: 26/05/2023 19:18:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Gebruikers] (
    [Id]       INT          NOT NULL,
    [Naam]     VARCHAR (50) NOT NULL,
    [Voornaam] NCHAR (50)   NOT NULL,
    [Prijs]    DECIMAL (18) NOT NULL
);


