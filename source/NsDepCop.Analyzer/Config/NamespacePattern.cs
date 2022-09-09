using System;
using System.Text.RegularExpressions;

namespace Codartis.NsDepCop.Config
{
    /// <summary>
    /// Represents a namespace pattern, eg. 'MyApp.*.Adapters.*'. Immutable.
    /// </summary>
    /// <remarks>
    /// The 'any namespace' (represented by a star '*') is also a namespace tree that contains every namespace.
    /// </remarks>
    [Serializable]
    public sealed class NamespacePattern : NamespaceSpecification
    {
        private readonly Regex _namespacePattern;

        /// <summary>
        /// Creates a new instance from a string representation.
        /// </summary>
        /// <param name="namespacePatternAsString">The string representation of a namespace tree.</param>
        /// <param name="validate">True means validate the input string.</param>
        public NamespacePattern(string namespacePatternAsString, bool validate = true)
            : base(namespacePatternAsString, validate, IsValid)
        {
            _namespacePattern = new Regex(namespacePatternAsString.Replace(".", "\\.").Replace("*", ".*"));
        }

        public override int GetMatchRelevance(Namespace ns)
        {
            var md = _namespacePattern.Match(NamespaceSpecificationAsString);
            if (!md.Success) return 0;

            return NamespaceSpecificationAsString.Length;
        }

        public static bool IsValid(string namespaceAsString)
        {
            return namespaceAsString != NamespaceTree.AnyNamespaceMarker && namespaceAsString.Contains("*");
        }
    }
}
