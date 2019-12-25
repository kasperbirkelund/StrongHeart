using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrongHeart.Features.Decorators.Audit
{
    public interface IFeatureAuditRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items">Some features might want to spawn more audit actions. Eg bulk operations.</param>
        /// <returns></returns>
        Task CreateFeatureAudit(ICollection<CreateFeatureAuditDto> items);
    }
}