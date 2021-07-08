using System.Threading.Tasks;
using System.Collections.Generic;

namespace Instasharp.Utils.Extensions
{
    internal static class AsyncEnumerableExtensions
    {
        internal static async ValueTask<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
        {
            var collection = new List<T>();

            await foreach (var item in source)
            {
                collection.Add(item);
            }

            return collection;
        }

        internal static async ValueTask<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source, int count)
        {
            var collection = new List<T>();

            await foreach (var item in source)
            {
                if (collection.Count == count)
                {
                    break;                    
                }
                collection.Add(item);
            }

            return collection;
        }
    }
}
