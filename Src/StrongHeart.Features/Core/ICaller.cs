using System;
using System.Collections.Generic;

namespace StrongHeart.Features.Core
{
    public interface ICaller
    {
        Guid Id { get; }
        IReadOnlyList<IRole> Roles { get; }
        //IUser? CallOnBehalfOf { get; }
    }
}