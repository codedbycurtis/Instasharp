using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Instasharp.Models;
using Instasharp.Internal;

namespace Instasharp.Net
{
    /// <summary>
    /// Serves as an interface between Instagram and client applications.
    /// </summary>
    public class InstagramWebClient
    {
        private readonly HttpClient _httpClient = new();

        public async Task<Profile> GetProfileMetadata(string username)
        {
            string profilePictureUri = "", handle = "", fullName = "", bio = "";
            double postCount = 0, followerCount = 0, followingCount = 0;

            return new Profile(profilePictureUri, handle, postCount, followerCount, followingCount, fullName, bio);
        }

        public async Task SaveProfileMetadata(Profile profile, string path)
        {

        }

        /// <summary>
        /// Downloads an Instagram account's profile picture to the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="path">The directory to download the profile picture to.</param>
        public async Task DownloadProfilePictureAsync(Profile profile, string path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path), "Download path was not specified.");
            }

            using FileStream fs = new($"{path}\\{profile.Handle}.jpg", FileMode.Create);
            var image = _httpClient.GetAsync(profile.ProfilePictureUri).Result;
            await image.Content.CopyToAsync(fs);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InstagramWebClient"/>.
        /// </summary>
        public InstagramWebClient()
        {
            // Without an authorized 'sessionid,' Instagram re-directs all requests to the login page
            _httpClient.DefaultRequestHeaders.Add("cookie", "sessionid=2602413198%3AG5NYxERN6kSgS6%3A14");
        }

        [Obsolete("Use InstagramWebClient.DownloadProfilePictureAsync(Profile, string) instead.", true)]
        public async Task GetProfilePictureAsync(string username)
        {
            var response = _httpClient.GetAsync($"http://www.instagram.com/{username}/").Result;
            var html = await response.Content.ReadAsStringAsync();
            var profilePictureUri = html.Parse("<meta property=\"og:image\" content=");
        }
    }
}
