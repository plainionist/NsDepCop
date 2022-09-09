namespace Codartis.NsDepCop.Config
{
    /// <summary>
    /// Converts a string to a namespace specification.
    /// </summary>
    public static class NamespaceSpecificationParser
    {
        /// <summary>
        /// Creates a namespace specification from a string representation.
        /// </summary>
        /// <param name="namespaceSpecificationAsString">A namespace specification in string format.</param>
        /// <returns>The namespace specification created from the given string.</returns>
        /// <remarks>
        /// Throws an exception if the string cannot be parsed.
        /// </remarks>
        public static NamespaceSpecification Parse(string namespaceSpecificationAsString)
        {
            var wildcardPos = namespaceSpecificationAsString.IndexOf(NamespaceTree.AnyNamespaceMarker);
            return wildcardPos > 0 && wildcardPos < namespaceSpecificationAsString.Length - 1
                ? (NamespaceSpecification)new NamespacePattern(namespaceSpecificationAsString)
                : wildcardPos == namespaceSpecificationAsString.Length - 1
                    ? (NamespaceSpecification)new NamespaceTree(namespaceSpecificationAsString)
                    : new Namespace(namespaceSpecificationAsString);
        }
    }
}