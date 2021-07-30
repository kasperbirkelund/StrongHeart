﻿using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.Features.Documentation.Visitors
{
    public interface ISectionVisitor
    {
        void VisitHeader(HeaderSection section);
        void VisitText(TextSection section);
        void VisitTable<T>(TableSection<T> section);
    }
}