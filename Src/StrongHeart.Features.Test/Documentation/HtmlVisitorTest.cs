using System.Collections.Generic;
using FluentAssertions;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Test.Documentation.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Documentation
{
    public class HtmlVisitorTest
    {
        [Fact]
        public void DocumentationDescriberTest()
        {
            IDocumentationDescriber sut = new TestQueryFeature();
            IEnumerable<ISection> sections = sut.GetDocumentationSections(new TestAdminCaller());

            HtmlVisitor visitor = new();
            visitor.Accept(sections);

            string actual = visitor.AsString(includeHtmlTags: true);
            actual.Should().Be(Expected);
        }

        private const string Expected =
@"<html>
  <p>Regler</p>
  <table>
    <thead>
      <tr>
        <th scope=""col"">Kode</th>
        <th scope=""col"">Rule name</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>A</td>
        <td>RuleA</td>
      </tr>
      <tr>
        <td>B</td>
        <td>RuleB</td>
      </tr>
    </tbody>
  </table>
</html>";
    }
}