//using System;
//using System.Collections.Generic;

//namespace StrongHeart.Features.Decorators.Audit
//{
//    public interface IAuditable<in TRequest>
//    {
//        /// <summary>
//        /// Indicates that the action is performed on behalf on an other user
//        /// </summary>
//        Func<TRequest, bool> IsOnBehalfOfOtherSelector { get; }
//        AuditOptions AuditOptions { get; }

//        /// <summary>
//        /// Returns a list of CorrelationKeys. One audit row will be created for each return value
//        /// </summary>
//        Func<TRequest, IEnumerable<Guid?>> CorrelationKeySelector { get; }
//    }
//}
