using System;

namespace StrongHeart.Core
{
    /// <summary>
    /// This interface explicitly marks that a class has a reference to an external resource
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ExternalInterfaceAttribute : Attribute
    {
    }
}
