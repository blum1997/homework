CREATE TABLE [dbo].[Users] (
    [Name]       NVARCHAR (50) NOT NULL,
    [SecondName] NVARCHAR (50) NOT NULL,
    [LastName]   NVARCHAR (50) NULL,
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

