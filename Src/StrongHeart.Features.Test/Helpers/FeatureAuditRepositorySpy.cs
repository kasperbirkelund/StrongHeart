//using System.Collections.Generic;
//using System.Threading.Tasks;
//using StrongHeart.Features.Decorators.Audit;

//namespace StrongHeart.Features.Test.Helpers
//{
//    public class FeatureAuditRepositorySpy : IFeatureAuditRepository
//    {
//        public List<CreateFeatureAuditDto> Audits = new List<CreateFeatureAuditDto>();

//        public Task CreateFeatureAudit(ICollection<CreateFeatureAuditDto> items)
//        {
//            Audits.AddRange(items);
//            return Task.CompletedTask;
//        }
//    }
//}