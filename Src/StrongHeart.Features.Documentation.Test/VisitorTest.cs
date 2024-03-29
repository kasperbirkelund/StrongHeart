﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using StrongHeart.Features.Documentation.Sections;
using StrongHeart.Features.Documentation.Test.Features.Queries.TestQuery;
using StrongHeart.Features.Documentation.Visitors;
using Xunit;

namespace StrongHeart.Features.Documentation.Test;

public class VisitorTest
{
    public VisitorTest()
    {
        CodeCommentSection.SourceCodeDir = CodeCommentSection.GetSourceCodeDirFromFeature<TestQueryFeature>(@"\Src\");
    }

    [Fact(Skip = "Fails while running from Cake")]
    public void HtmlVisitorTest()
    {
        VisitorTestRunner(new HtmlVisitor(), x => x.AsString(includeLeadingHtmlTags: true), ExpectedHtml);
    }

    [Fact(Skip = "Fails while running from Cake")]
    public void MarkDownVisitorTest()
    {
        VisitorTestRunner(new MarkDownVisitor(), x => x.AsString(), ExpectedMarkDown);
    }

    private static void VisitorTestRunner<T>(T visitor, Func<T, string> getActual, string expected) where T : ISectionVisitor
    {
        IDocumentationDescriber sut = new TestQueryFeature();
        IEnumerable<ISection> sections = sut.GetDocumentationSections(new DocumentationGenerationContext());

        visitor.Accept(sections);

        string actual = getActual(visitor);
        actual.Should().Be(expected);
    }

    private const string ExpectedHtml =
        @"<html>
  <p>Rules</p>
  <table>
    <thead>
      <tr>
        <th scope=""col"">Kode</th>
        <th scope=""col"">Rule name</th>
        <th scope=""col"">Description</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>A</td>
        <td>RuleA</td>
        <td>Any description</td>
      </tr>
      <tr>
        <td>B</td>
        <td>RuleB</td>
        <td>Any description</td>
      </tr>
    </tbody>
  </table>
  <p>TestQueryFeature.cs: This is the only line</p>
  <code>            return Task.FromResult(Result&lt;TestQueryResponse&gt;.Success(new TestQueryResponse(new PersonDto(""PersonA""))));</code>
</html>";

    private const string ExpectedMarkDown = @"Rules
|Kode|Rule name|Description|
|-|-|-|
|A|RuleA|Any description|
|B|RuleB|Any description|
TestQueryFeature.cs: This is the only line
```
            return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse(new PersonDto(""PersonA""))));
```";
}