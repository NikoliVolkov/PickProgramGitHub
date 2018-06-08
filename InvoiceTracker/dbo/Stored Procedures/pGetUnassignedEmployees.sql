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

	SELECT [EmployeeId], [FirstName], [LastName], [Nickname] 
		FROM dbo.Employee e WHERE [EmployeeId] NOT IN (SELECT [AssignedEmployeeId] FROM dbo.Invoice WHERE [StatusId] = 1 AND [AssignedEmployeeId] IS NOT NULL)
END