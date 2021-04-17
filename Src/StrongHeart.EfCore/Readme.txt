/*
KØRES KUN PÅ UDV-MILJØER
*/

Scaffold-DbContext "Server=.\sqlexpress;Database=xxx;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataModels\WriteModels -force -DataAnnotations -Context MyContext -Project MyProject.DataAccess -UseDatabaseNames