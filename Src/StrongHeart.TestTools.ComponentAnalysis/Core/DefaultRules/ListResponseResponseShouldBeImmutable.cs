using System;
using System.Linq;
using System.Reflection;
using StrongHeart.Features;
using StrongHeart.Features.Core;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;

public class ListResponseResponseShouldBeImmutable : IRule<Type>
{
    public string CorrectiveAction => "Ensure that the generic type of IGetListResponse<> only contains immutable properties";

    public bool DoVerifyItem(Type item)
    {
        return item.IsClass && item.DoesImplementInterface(typeof(IGetListResponse<>));
    }

    public bool IsValid(Type item, Action<string> output)
    {
        Type type = item.GetInterface(typeof(IGetListResponse<>).Name)!.GetGenericArguments().Single();

        PropertyInfo[] invalidItems = type.GetProperties().Where(x => !x.IsImmutable()).ToArray();
        foreach (PropertyInfo info in invalidItems)
        {
            output(info.Name);
        }
        return !invalidItems.Any();
    }
}