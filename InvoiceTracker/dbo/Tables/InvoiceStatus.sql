CREATE TABLE [dbo].[InvoiceStatus] (
    [StatusId] INT          IDENTITY (1, 1) NOT NULL,
    [Status]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_InvoiceStatus] PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

