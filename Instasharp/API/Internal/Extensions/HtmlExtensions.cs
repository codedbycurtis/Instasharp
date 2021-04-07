namespace Instasharp.Internal.Extensions
{
    internal static class HtmlExtensions
    {
        internal static string Parse(this string source, string tag, string separator)
        {
            var startIndex = source.IndexOf(tag) + tag.Length;
            return source[startIndex..source.IndexOf(separator, startIndex)];
        }
    }
}
