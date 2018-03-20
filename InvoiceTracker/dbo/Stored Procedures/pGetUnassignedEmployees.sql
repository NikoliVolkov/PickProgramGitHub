-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pGetUnassignedEmployees] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DISTINCT [EmployeeId], [FirstName], [LastName], [Nickname] 
	--get employee id's for currently assigned tickets
		FROM dbo.Employee e WHERE [EmployeeId] NOT IN (SELECT [AssignedEmployeeId] FROM dbo.Invoice WHERE [StatusId] = 1 AND [AssignedEmployeeId] IS NOT NULL)
		UNION
	--new employees who have never been assigned
	SELECT DISTINCT [EmployeeId], [FirstName], [LastName], [Nickname] 
		FROM dbo.Employee e LEFT JOIN dbo.Invoice i on i.[AssignedEmployeeId] = e.[EmployeeId]
		LEFT JOIN dbo.InvoiceStatus invs ON i.[StatusId] = invs.[StatusId] WHERE i.[StatusId] IS NULL
END