namespace Instasharp.Internal.Extensions
{
    internal static class HtmlExtensions
    {
        internal static string? Parse(this string source, string tag, string separator)
        {
            var indexOf = source.IndexOf(tag);
            var startIndex = indexOf + tag.Length;

            return indexOf is -1 ? null : source[startIndex..source.IndexOf(separator, startIndex)];
        }
    }
}
