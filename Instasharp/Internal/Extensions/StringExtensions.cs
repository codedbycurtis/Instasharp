using System;

namespace Instasharp.Internal.Extensions
{
    internal static class StringExtensions
    {
        internal static bool StartsWith(this string value, StringComparison comparisonType, params string[] args)
        {
            for (int i = 0; i < args.Length; ++i)
            {
                if (value.StartsWith(args[i], comparisonType))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
