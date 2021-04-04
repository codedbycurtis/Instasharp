namespace Instasharp.Internal
{
    internal static class Html
    {
        internal static string Parse(this string source)
        {
            string tag = "<meta property=\"og:image\" content=";
            var indexOf = source.IndexOf(tag) + tag.Length;
            return source.Substring(source.IndexOf(tag) + tag.Length, source.IndexOf('"', indexOf));
        }
    }
}
