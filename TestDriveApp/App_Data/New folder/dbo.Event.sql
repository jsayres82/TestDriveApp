CREATE TABLE [dbo].[Event] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Start] DATETIME       NOT NULL,
    [End]   DATETIME       NOT NULL,
    [Text]  NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

