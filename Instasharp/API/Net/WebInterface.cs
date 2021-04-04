using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Instasharp.Internal;

namespace Instasharp.Net
{
    /// <summary>
    /// Provides easy client access to common Instagram components.
    /// </summary>
    public class WebInterface
    {
        private readonly HttpClient _httpClient = new();
        private readonly HtmlScraper _htmlScraper = new();

        public WebInterface()
        {
            
        }

        /// <summary>
        /// Gets (and optionally downloads) an Instagram user's profile picture.
        /// </summary>
        /// <param name="username">The username of the Instagrammer whose profile picture to get.</param>
        /// <param name="shouldDownload">Should the profile picture be downloaded?</param>
        /// <param name="downloadPath">Path to download the profile picture to.</param>
        /// <returns></returns>
        public async Task GetProfilePictureAsync(string username, bool shouldDownload, string downloadPath = null)
        {
            var response = _httpClient.GetAsync($"https://www.instagram.com/{username}/").Result;
            var parsed = response.Content.ReadAsStringAsync().Result.Parse();
            Console.WriteLine(parsed);

            if (shouldDownload)
            {
                // TODO: Download profile picture
            }
        }

        public async Task GetProfilePageSource(string username)
        {
            var response = _httpClient.GetAsync($"https://www.instagram.com/{username}/").Result;
            using FileStream fs = new($"{username}.html", FileMode.Create);
            await response.Content.CopyToAsync(fs);
        }
    }
}
