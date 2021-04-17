using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold
{
    public class RemoveTemporalTableColumnsCSharpEntityTypeGenerator : CSharpEntityTypeGenerator
    {
        public RemoveTemporalTableColumnsCSharpEntityTypeGenerator(IAnnotationCodeGenerator annotationCodeGenerator,
            ICSharpHelper cSharpHelper) : base(annotationCodeGenerator, cSharpHelper)
        {
            
        }
        public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations)
        {
            string code = base.WriteCode(entityType, @namespace, useDataAnnotations);
            
            string replace1 = @$"[Column(TypeName = ""datetime2(0)"")]
        public DateTime RowValidFromUtc {{ get; set; }}";
            string replace2 = @$"[Column(TypeName = ""datetime2(0)"")]
        public DateTime RowValidToUtc {{ get; set; }}";

            code = code
                .Replace(replace1, $@"/*This column is reserved to temporal tables and should not be accessed from entity framework {replace1}*/")
                .Replace(replace2, $@"/*This column is reserved to temporal tables and should not be accessed from entity framework {replace2}*/");

            return code;
        }
    }
}