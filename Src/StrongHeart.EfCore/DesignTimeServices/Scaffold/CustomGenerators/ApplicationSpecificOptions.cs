using System.Collections.Generic;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.CustomGenerators
{
    public class ApplicationSpecificOptions
    {
        public ICollection<string> ProceduresToOmit { get; }

        public ApplicationSpecificOptions(ICollection<string> proceduresToOmit)
        {
            ProceduresToOmit = proceduresToOmit;
        }

        public string ToDebugString()
        {
            return $"Excluding these stored procs: { string.Join(", ", ProceduresToOmit)}";
        }
    }
}