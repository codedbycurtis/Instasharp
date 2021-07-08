using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Instasharp.Profiles;
using Instasharp.Utils.Extensions;
using Instasharp.Exceptions;
using Instasharp.Search;
using Instasharp.Utils;

namespace Instasharp
{
    /// <summary>
    /// Provides a gateway to scraping various metadata from Instagram profiles.
    /// </summary>
    public class InstagramClient
    {
        private readonly HtmlScraper _htmlScraper;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstagramClient"/> class with an optional <paramref name="sessionId"/>.
        /// </summary>
        /// <param name="sessionId">A valid SessionID may be required to stop Instagram re-directing requests to the login page.</param>
        public InstagramClient(string? sessionId = default) : this(Http.Client, sessionId) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstagramClient"/> class with the specified <paramref name="httpClient"/> and an optional <paramref name="sessionId"/>.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> to use for requests.</param>
        /// <param name="sessionId">A valid SessionID may be required to stop Instagram re-directing requests to the login page.</param>
        public InstagramClient(HttpClient httpClient, string? sessionId = default)
        {
            this._httpClient = httpClient;
            if (sessionId is not null)
            {
                this._httpClient.DefaultRequestHeaders.Add("cookie", $"sessionid={sessionId}");
            }
            this._htmlScraper = new(this._httpClient);
        }

        /// <summary>
        /// Scrapes metadata from the Instagram profile with the specified <paramref name="usernameOrUrl"/>.
        /// </summary>
        /// <remarks>URLs must begin with 'http://' or 'https://'.</remarks>
        /// <param name="usernameOrUrl">The username (handle) of the user, or the link to the user's profile.</param>
        /// <returns>A <see cref="Profile"/> object representing the acquired metadata.</returns>
        public async Task<Profile> GetProfileMetadataAsync(string usernameOrUrl)
        {
            var uri = usernameOrUrl;

            // Check if the user has entered a complete URL or a username
            if (!usernameOrUrl.StartsWith(StringComparison.OrdinalIgnoreCase, "https://www.instagram.com/", "http://www.instagram.com/"))
            {
                uri = $"https://www.instagram.com/{usernameOrUrl}";
            }

            var html = await this._htmlScraper.GetPageSource(uri);

            // Splits the content between the specified tags into a substring - which is the JSON data we need
            // (Most of this JSON data is useless, so there may be room for optimization here to decrease parsing time)
            var json = html.SubstringBetween("<script type=\"text/javascript\">window._sharedData = ", ";</script>");

            // If appropriate JSON data cannot be found, the page we are trying to access must be unavailable, so an exception is thrown
            if (json is null)
            {
                throw ContentUnavailableException.PageUnavailable(usernameOrUrl);
            }

            var payload = JObject.Parse(json);

            // If a 'LoginAndSignupPage' token exists, Instagram has re-directed our request, so an exception is thrown
            if (payload["entry_data"]!["LoginAndSignupPage"] is not null)
            {
                throw ClientUnauthorizedException.InvalidSessionId();
            }

            // If a 'ProfilePage' token cannot be found, the username is invalid, so an exception is thrown
            if (payload["entry_data"]!["ProfilePage"] is null)
            {
                throw ProfileNotFoundException.InvalidUsernameOrUrl(usernameOrUrl);
            }

            return Json.ParseInstagramProfilePayload(payload);
        }

        /// <summary>
        /// Downloads the high-definition profile picture of the specified <paramref name="profile"/> to the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="profile">The profile which will have it's profile picture downloaded.</param>
        /// <param name="path">The profile picture's download location. (Must include a file name and extension.)</param>
        public async Task DownloadProfilePictureAsync(Profile profile, string path)
        {
            using var response = await this._httpClient.GetAsync(profile.ProfilePictureUri);
            using var stream = File.Create(path);
            await response.Content.CopyToAsync(stream);
        }

        /// <summary>
        /// Downloads the high-definition profile picture of the profile with the specified <paramref name="usernameOrUrl"/> to the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="usernameOrUrl">The username or URL of the profile which will have it's profile picture downloaded.</param>
        /// <param name="path">The profile picture's download location. (Must include a file name and extension.)</param>
        public async Task DownloadProfilePictureAsync(string usernameOrUrl, string path)
        {
            var profile = await this.GetProfileMetadataAsync(usernameOrUrl);
            using var profilePictureResponse = await this._httpClient.GetAsync(profile.ProfilePictureUri);
            using var stream = File.Create(path);
            await profilePictureResponse.Content.CopyToAsync(stream);
        }

#nullable disable

        /// <summary>
        /// Enumerates over Instagram profiles matching the specified <paramref name="searchQuery"/>.
        /// </summary>
        /// <param name="searchQuery">The profile to search for.</param>
        public async IAsyncEnumerable<ProfileSearchResult> SearchForProfilesAsync(string searchQuery)
        {
            var json = await this._htmlScraper.GetPageSource($"https://www.instagram.com/web/search/topsearch/?query={searchQuery}/");
            var payload = JObject.Parse(json);

            for (int i = 0; i < payload["users"].Count(); ++i)
            {
                yield return Json.ParseInstagramProfileSearchResultPayload(payload["users"][i]);
            }
        }

#nullable restore

    }
}