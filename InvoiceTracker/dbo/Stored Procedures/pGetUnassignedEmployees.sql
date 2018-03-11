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

    -- Insert statements for procedure here
	SELECT DISTINCT EmployeeId, FirstName, LastName, Nickname 
	FROM Employee e LEFT JOIN Invoice i on i.AssignedEmployeeId = e.EmployeeId
	LEFT JOIN InvoiceStatus invs ON i.StatusId = invs.StatusId WHERE invs.Status <> 'Pending' OR i.StatusId IS NULL
END