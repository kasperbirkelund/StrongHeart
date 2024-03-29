﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace StrongHeart.Features.Decorators.RequestValidation;

public class ValidationConclusion
{
    public ICollection<ValidationMessage> Messages { get; }
    public bool IsValid => !Messages.Any();

    public ValidationConclusion(ICollection<ValidationMessage> messages)
    {
        Messages = messages;
    }

    public override string ToString()
    {
        string text = string.Join(Environment.NewLine, Messages.Select(x => $"- {x}"));
        return @"Validation messages: 
" + text;
    }
}