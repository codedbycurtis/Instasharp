using System;

namespace Instasharp.Internal.Extensions
{
    internal static class StringExtensions
    {
        internal static bool StartsWith(this string source, StringComparison comparison, params string[] args)
        {
            for (int i = 0; i < args.Length; ++i)
            {
                if (source.StartsWith(args[i], comparison))
                {
                    return true;
                }
            }

            return false;
        }

        internal static string? SubstringBetween(this string source, string start, string end)
        {
            var indexOf = source.IndexOf(start);
            var startIndex = indexOf + start.Length;

            return indexOf is -1 ? null : source[startIndex..source.IndexOf(end, startIndex)];
        }
    }
}
