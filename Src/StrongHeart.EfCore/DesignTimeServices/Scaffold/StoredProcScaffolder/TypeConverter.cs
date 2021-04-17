using System;
using System.Collections.Generic;
using System.Linq;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.StoredProcScaffolder
{
    internal static class TypeConverter
    {
        /// <summary>
        /// table which contains the mapping from sql to c#. It is not complete.
        /// Extend it as needed.
        /// All primitive types are nullable as output parameters must be nullable for now.
        /// </summary>
        private static readonly IDictionary<string, string> ConvertTable = new Dictionary<string, string>()
        {
            //Reference to Microsofts official mapping: https://docs.microsoft.com/en-us/sql/relational-databases/clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data?view=sql-server-2016&redirectedfrom=MSDN
            {"char", "string?"},
            {"varchar", "string?"},
            {"nvarchar", "string?"},
            {"int", "int?"},
            {"float", "double?"},
            {"uniqueidentifier", "Guid?"},
            {"datetime2", "DateTime?"},
            {"date", "DateTime?"},
            {"bit", "bool?"},
            {"smallint", "short?"},
            {"varbinary", "byte[]"},
            {"decimal", "decimal?"}
        };

        public static string GetCSharpType(this string sqlType, bool makePrimitiveTypesNullable)
        {
            //Sql server returns values like 'nvarchar(200)' - therefore strip the unneeded details.
            int parentParentheses = sqlType.IndexOf('(', StringComparison.CurrentCulture);
            if (parentParentheses > -1)
            {
                sqlType = sqlType.Substring(0, parentParentheses);
            }

            var csharpType = ConvertTable.Where(x => x.Key == sqlType).Select(x => x.Value);
            string? type = csharpType.FirstOrDefault();
            if (type == null)
            {
                throw new ArgumentException($"Unknown sql type: {sqlType}. Fix mapping table in {typeof(TypeConverter).FullName}");
            }

            return makePrimitiveTypesNullable ? type : type.Replace("?", string.Empty, StringComparison.CurrentCulture);
        }
    }
}