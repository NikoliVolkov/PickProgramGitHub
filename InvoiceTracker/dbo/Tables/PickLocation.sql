CREATE TABLE [dbo].[PickLocation] (
    [LocationId]          INT          IDENTITY (1, 1) NOT NULL,
    [LocationDescription] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PickLocation] PRIMARY KEY CLUSTERED ([LocationId] ASC)
);

