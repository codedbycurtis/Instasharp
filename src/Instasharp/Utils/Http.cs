using System;
using System.Net.Http;

namespace Instasharp.Utils
{
    internal static class Http
    {
        private static readonly Lazy<HttpClient> LazyHttpClient = new();

        internal static HttpClient Client => LazyHttpClient.Value;
    }
}
