using System;
using Codartis.NsDepCop.Config;
using FluentAssertions;
using Xunit;

namespace Codartis.NsDepCop.Test.Interface.Config
{
    public class NamespacePatternTests
    {
        [Theory]
        [InlineData("A.*.B")]
        [InlineData("A.*.B.*")]
        public void Create_Works(string namespaceTreeString)
        {
            new NamespacePattern(namespaceTreeString).ToString().Should().Be(namespaceTreeString);
        }

        [Fact]
        public void Create_WithNull_ThrowsArgumentNullExceptionn()
        {
            Assert.Throws<ArgumentNullException>(() => new NamespacePattern(null));
        }

        [Theory]
        [InlineData("A")]
        [InlineData("A.B")]
        public void Create_NotANamespacePattern_TopNamespace_ThrowsFormatException(string namespaceTreeString)
        {
            Assert.Throws<FormatException>(() => new NamespacePattern(namespaceTreeString));
        }

        [Fact]
        public void Equals_Works()
        {
            (new NamespacePattern("A.*") == new NamespacePattern("A.*")).Should().BeTrue();
            (new NamespacePattern("A.*") == new NamespacePattern("B.*")).Should().BeFalse();
        }

        [Theory]
        [InlineData("A.*.B")]
        [InlineData("A.*.B.*")]
        public void GetMatchRelevance_Succeeds(string namespaceTreeString)
        {
            new NamespacePattern(namespaceTreeString).GetMatchRelevance(new Namespace("A.F1.B.C")).Should().BeGreaterThan(0);
        }
    }
}
