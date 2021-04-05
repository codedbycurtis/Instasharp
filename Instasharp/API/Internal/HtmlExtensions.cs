namespace Instasharp.Internal
{
    internal static class HtmlExtensions
    {
        internal static string Parse(this string source, string tag)
        {
            var startIndex = source.IndexOf(tag) + tag.Length + 1;
            return source[startIndex..source.IndexOf('"', startIndex)];
        }
    }
}
