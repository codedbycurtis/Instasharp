using System.Threading.Tasks;
using System.Collections.Generic;

namespace Instasharp.Internal.Extensions
{
    internal static class AsyncEnumerableExtensions
    {
        internal static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
        {
            var collection = new List<T>();
            await foreach (var item in source)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
