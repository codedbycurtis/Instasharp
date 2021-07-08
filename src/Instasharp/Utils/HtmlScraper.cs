using System.Net.Http;
using System.Threading.Tasks;

namespace Instasharp.Utils
{
    internal class HtmlScraper
    {
        private readonly HttpClient _httpClient;

        internal HtmlScraper(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        internal async Task<string> GetPageSource(string uri)
        {
            using var response = await this._httpClient.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
