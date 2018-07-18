CREATE TABLE [dbo].[Employee] (
    [EmployeeId] INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]  VARCHAR (50) NOT NULL,
    [LastName]   VARCHAR (50) NOT NULL,
    [Nickname]   VARCHAR (50) NULL,
    [DeactivateDate] DATETIME2 NULL, 
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);

