using System;
using System.Collections.Generic;
using FluentAssertions;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;
using StrongHeart.Features.Documentation.Visitors;
using StrongHeart.Features.Test.Documentation.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Documentation
{
    public class VisitorTest
    {
        [Fact]
        public void HtmlVisitorTest()
        {
            VisitorTestRunner(new HtmlVisitor(), x => x.AsString(includeLeadingHtmlTags: true), ExpectedHtml);
        }

        [Fact]
        public void MarkDownVisitorTest()
        {
            VisitorTestRunner(new MarkDownVisitor(), x => x.AsString(), ExpectedMarkDown);
        }

        private static void VisitorTestRunner<T>(T visitor, Func<T, string> getActual, string expected) where T : ISectionVisitor
        {
            IDocumentationDescriber sut = new TestQueryFeature();
            IEnumerable<ISection> sections = sut.GetDocumentationSections(new TestAdminCaller());

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
</html>";

        private const string ExpectedMarkDown = @"Rules
|Kode|Rule name|Description|
|-|-|-|
|A|RuleA|Any description|
|B|RuleB|Any description|";
    }
}