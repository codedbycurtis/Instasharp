namespace Instasharp.Internal.Extensions
{
    internal static class HtmlExtensions
    {
        internal static string? Parse(this string source, string tag, string separator)
        {
            var indexOf = source.IndexOf(tag);

            if (indexOf is -1)
            {
                return null;
            }

            var startIndex = indexOf + tag.Length;
            return source[startIndex..source.IndexOf(separator, startIndex)];
        }
    }
}
