CREATE OR ALTER VIEW dbo.DemoView  
WITH SCHEMABINDING
AS  
	SELECT Id
	FROM dbo.DemoTable
	WHERE Id > 3 -- Just a demo where clause
