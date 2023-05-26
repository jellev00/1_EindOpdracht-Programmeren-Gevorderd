USE [GentseFeestenDB]
GO

/****** Object: Table [dbo].[Dagplannen] Script Date: 26/05/2023 19:17:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Dagplannen] (
    [Id]          INT           NOT NULL,
    [GebruikerId] INT           NULL,
    [Datum]       DATE          NULL,
    [Evenement1]  VARCHAR (MAX) NULL,
    [Evenement2]  VARCHAR (MAX) NULL
);


