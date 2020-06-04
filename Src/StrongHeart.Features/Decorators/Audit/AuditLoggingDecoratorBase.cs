using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Audit
{
    //[DebuggerStepThrough]
    public abstract class AuditLoggingDecoratorBase : DecoratorBase
    {
        private readonly IFeatureAuditRepository _repository;

        protected AuditLoggingDecoratorBase(IFeatureAuditRepository repository)
        {
            _repository = repository;
        }

        protected override async Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            IEnumerable<CreateFeatureAuditDto>? items = null;

            Stopwatch sw = Stopwatch.StartNew();
            Guid?[]? correlationKeys = null;
            Guid featureId = GetUniqueFeatureId();

            try
            {
                TResponse result = await func(request);
                sw.Stop();

                correlationKeys = GetKeys(request);
                bool isOnBehalfOfOther = GetIsOnBehalfOfOther(request);

                if (result is IResult r)
                {
                    if (r.Status == ResultType.Failed)
                    {
                        items = correlationKeys.Select(x => CreateFeatureAuditDto.CreateResultFailure(featureId, request, r.Error, sw.Elapsed, x));
                        return result;
                    }
                }

                if (AuditOptions.LogResponse)
                {
                    items = correlationKeys.Select(x => CreateFeatureAuditDto.CreateSuccessWithResponse(featureId, request, result, sw.Elapsed, x, isOnBehalfOfOther));
                }
                else
                {
                    items = correlationKeys.Select(x => CreateFeatureAuditDto.CreateSuccessWithNoResponse(featureId, request, null as string, sw.Elapsed, x, isOnBehalfOfOther));
                }

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();

                if (correlationKeys == null)
                {
                    correlationKeys = GetKeys(request);
                }

                items = correlationKeys.Select(x => CreateFeatureAuditDto.CreateException(featureId, request, ex, sw.Elapsed, x));
                throw;
            }
            finally
            {
                await _repository.CreateFeatureAudit(items.ToImmutableArray());
            }
        }

        private Guid?[] GetKeys<TRequest>(TRequest request)
        {
            Guid?[] correlationKeys = (GetCorrelationKey(request) ?? new List<Guid?>()).ToArray();
            if (correlationKeys.Length == 0)
            {
                correlationKeys = new Guid?[] { null };//null will ensure that we get an audit recorded with no correlation key
            }
            return correlationKeys;
        }

        protected abstract Guid GetUniqueFeatureId();

        public abstract AuditOptions AuditOptions { get; }
        public abstract IEnumerable<Guid?> GetCorrelationKey<TRequest>(TRequest request);
        public abstract bool GetIsOnBehalfOfOther<TRequest>(TRequest request);
    }
}