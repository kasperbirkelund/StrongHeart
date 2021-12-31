CREATE OR ALTER PROCEDURE [dbo].DemoStoredProc
	@Input1 nvarchar(400),
	@Input2 int
	AS
BEGIN
	SELECT Id, CreatedAtUtc
	FROM DemoTable
	WHERE 1=1
END