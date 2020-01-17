using System;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public interface IRule<in T>
    {
        /// <summary>
        /// A short help to developer on how to correct the error
        /// </summary>
        string CorrectiveAction { get; }
        bool DoVerifyItem(T item);
        bool IsValid(T item, Action<string> output);

        /// <summary>
        /// If no items are found to verify, an exception is thrown only if this property returns true.
        /// </summary>
        bool DoFailIfNoItemsToVerify { get; }
    }
}
