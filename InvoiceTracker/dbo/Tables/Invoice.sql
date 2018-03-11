CREATE TABLE [dbo].[Invoice] (
    [InvoiceId]          INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceNumber]      VARCHAR (50)  NOT NULL,
    [NumberOfParts]      INT           NOT NULL,
    [PickLocationId]     INT           NOT NULL,
    [AssignedEmployeeId] INT           NULL,
    [StatusId]           INT           NOT NULL,
    [StartDate]          DATETIME2 (7) NOT NULL,
    [FinishDate]         DATETIME2 (7) NULL,
    [AssignedDate]       DATETIME2 (7) NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED ([InvoiceId] ASC),
    CONSTRAINT [FK_Invoice_Employee] FOREIGN KEY ([AssignedEmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_Invoice_InvoiceStatus] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[InvoiceStatus] ([StatusId]),
    CONSTRAINT [FK_Invoice_PickLocation] FOREIGN KEY ([PickLocationId]) REFERENCES [dbo].[PickLocation] ([LocationId])
);

