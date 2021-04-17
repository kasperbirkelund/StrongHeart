namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.StoredProcScaffolder
{
    internal class StoredProcOutputParameter
    {
        public StoredProcOutputParameter(string name, string sqlType, bool isNullable)
        {
            Name = name;
            CSharpType = sqlType.GetCSharpType(makePrimitiveTypesNullable: isNullable);
        }

        public string Name { get; }
        public string CSharpType { get; }
    }
}