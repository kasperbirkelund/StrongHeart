﻿using System;
using StrongHeart.Features;
using StrongHeart.Features.Core;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;

public class ResponseDtoShouldBeMoreSpecificRule : IRule<Type>
{
    public string CorrectiveAction => $"Ensure that the response class implements either {typeof(IGetSingleItemResponse<>).Name} or {typeof(IGetListResponse<>).Name}";

    public bool DoVerifyItem(Type item)
    {
        return item.IsClass && item.DoesImplementInterface(typeof(IResponseDto));
    }

    public bool IsValid(Type item, Action<string> output)
    {
        return 
            item.DoesImplementInterface(typeof(IGetSingleItemResponse<>)) || 
            item.DoesImplementInterface(typeof(IGetListResponse<>));
    }
}