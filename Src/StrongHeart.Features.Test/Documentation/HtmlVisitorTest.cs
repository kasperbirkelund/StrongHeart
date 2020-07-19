using FluentAssertions;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Test.Documentation.Features.Queries.TestQuery;
using Xunit;

namespace StrongHeart.Features.Test.Documentation
{
    public class HtmlVisitorTest
    {
        [Fact]
        public void DocumentationDescriberTest()
        {
            IDocumentationDescriber sut = new TestQueryFeature();

            HtmlVisitor visitor = new HtmlVisitor();
            visitor.Accept(sut.GetDocumentationSections(null));
            string actual = visitor.Sb.ToString().Trim();
            const string expected = @"<p>Regler</p><table><thead><tr><th scope=""col"">Kode</th><th scope=""col"">Rule name</th></tr></thead><tbody><tr><td>A</td><td>RuleA</td></tr><tr><td>B</td><td>RuleB</td></tr></tbody></table>";
            actual.Should().Be(expected); //could be more accurate :-)
        }
    }
}
