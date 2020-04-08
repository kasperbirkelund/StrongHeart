namespace StrongHeart.Features.Decorators.Audit
{
    public enum FeatureAuditStatus
    {
        Success = 1,
        ValidationException = 2,
        BusinessException = 3,
        OtherException = 4,
        ResultFailure = 5
    }
}