CREATE TABLE [dbo].[Activity] (
    [UserID]      INT            NOT NULL,
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Address]     NVARCHAR (MAX) NOT NULL,
    [Type]        NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (MAX) COLLATE Cyrillic_General_CI_AS NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_activity_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [FK_activity_UserID]
    ON [dbo].[Activity]([UserID] ASC);

