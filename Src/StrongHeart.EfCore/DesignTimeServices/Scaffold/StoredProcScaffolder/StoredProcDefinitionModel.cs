using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.StoredProcScaffolder
{
    internal class StoredProcDefinitionModel
    {
        public string Name { get; }
        public string Schema { get; }
        public IEnumerable<StoredProcInputParameter> Input { get; }
        public IEnumerable<StoredProcOutputParameter> Output { get; }

        private StoredProcDefinitionModel(string name, string schema, IEnumerable<StoredProcInputParameter> input, IEnumerable<StoredProcOutputParameter> output)
        {
            Name = name;
            Schema = schema;
            Input = input;
            Output = output;
        }

        /// <summary>
        /// Extract needed stored procedure information from the database.
        /// </summary>
        public static IEnumerable<StoredProcDefinitionModel> GetStoredProcModel(string connectionString, GenerateStoredProcedureOptions options)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            using var command = con.CreateCommand();
            command.CommandText = GetInputParameterSql(options);

            var tmpList = new List<(string? schema, string? procName, string? sqlType, string? paramName, bool? isNullable)>();
            using (var reader = command.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    object isNullableObj = reader[4];
                    bool? isNullable = isNullableObj == DBNull.Value ? (bool?)null : Convert.ToBoolean(isNullableObj);
                    tmpList.Add((reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), isNullable));
                }
            }

            var grp = tmpList.GroupBy(x => x.procName);
            List<StoredProcDefinitionModel> modelDescriptions = new List<StoredProcDefinitionModel>();
            foreach (var g in grp)
            {
                IEnumerable<StoredProcInputParameter> inputs = Enumerable.Empty<StoredProcInputParameter>();
                if (g.All(x => x.isNullable != null))
                {
                    inputs = g.Select(x => new StoredProcInputParameter(x.paramName, x.sqlType, x.isNullable.Value));
                }

                List<StoredProcOutputParameter> outputs = new List<StoredProcOutputParameter>();

                command.CommandText = $"SELECT name, system_type_name, is_nullable FROM sys.dm_exec_describe_first_result_set (N'[{g.First().schema}].[{g.First().procName}]', null, 0);";

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        outputs.Add(new StoredProcOutputParameter(reader.GetString("name"), reader.GetString("system_type_name"), reader.GetBoolean("is_nullable")));
                    }
                }

                StoredProcDefinitionModel model = new StoredProcDefinitionModel(g.Key, g.First().schema, inputs, outputs);
                modelDescriptions.Add(model);
            }

            return modelDescriptions;
        }

        private static string AsSqlString(IEnumerable<string> items)
        {
            return string.Join(", ", items.Select(x => $"'{x}'"));
        }

        private static string GetInputParameterSql(GenerateStoredProcedureOptions options)
        {
            Console.Out.WriteLine(options.ToDebugString());

            string tableRestrictionString = string.Empty;
            if (options.ApplicationSpecificOptions.ProceduresToOmit.Any())
            {
                tableRestrictionString = $"WHERE FullName NOT IN({AsSqlString(options.ApplicationSpecificOptions.ProceduresToOmit)})";
            }
            string schemaClause = "WHERE sp.Name LIKE 'Read%'"; //only apply to procedures which have Read as prefix
            if (options.ExplicitSchemasToInclude.Any())
            {
                string schemaString = AsSqlString(options.ApplicationSpecificOptions.ProceduresToOmit);
                schemaClause = $"AND OBJECT_SCHEMA_NAME(sp.object_id) IN ({schemaString})";
            }

            return $@"
;WITH tmp AS(
	SELECT
		OBJECT_SCHEMA_NAME(sp.object_id) AS SchemaName,
		sp.name AS ProcedureName,
		t.name AS SqlTypeName,
		ap.name AS ParameterName,
		(OBJECT_SCHEMA_NAME(sp.object_id) + '.' + sp.name) AS FullName,
        ap.is_nullable as IsNullable,
        ap.parameter_id
	FROM sys.procedures AS sp
	LEFT JOIN sys.all_parameters ap ON ap.object_id=sp.object_id
	LEFT JOIN sys.types t ON ap.system_type_id = t.system_type_id
	{schemaClause}
)
SELECT SchemaName, ProcedureName, SqlTypeName, ParameterName, IsNullable
FROM tmp
{tableRestrictionString}
ORDER BY (SchemaName + ProcedureName), parameter_id";
        }
    }
}