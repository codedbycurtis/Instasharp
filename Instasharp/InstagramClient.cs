using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Instasharp.Profiles;
using Instasharp.Internal.Extensions;
using Instasharp.Exceptions;
using Instasharp.Search;

namespace Instasharp
{
    /// <summary>
    /// Provides a gateway to scraping various metadata from Instagram profiles.
    /// </summary>
    public sealed class InstagramClient
    {
        private readonly HttpClient _httpClient = new();

        /// <summary>
        /// Initializes a new instance of <see cref="InstagramClient"/> with an optional <paramref name="sessionId"/>.
        /// </summary>
        /// <param name="sessionId">A valid SessionID may be required to stop Instagram re-directing requests to the login page.</param>
        public InstagramClient(string? sessionId = default)
        {
            _httpClient.DefaultRequestHeaders.Add("cookie", $"sessionid={sessionId}");
        }

        /// <summary>
        /// Scrapes metadata from the Instagram profile with the specified <paramref name="usernameOrUrl"/>.
        /// </summary>
        /// <remarks>URLs must begin with 'http://' or 'https://'.</remarks>
        /// <param name="usernameOrUrl">The username (handle) of the user, or the link to the user's profile.</param>
        /// <returns>A <see cref="Profile"/> object representing the acquired metadata.</returns>
        public async Task<Profile> GetProfileMetadataAsync(string usernameOrUrl)
        {
            HttpResponseMessage response;

            // Check if the user has entered a complete URL or a username
            if (usernameOrUrl.StartsWith(StringComparison.OrdinalIgnoreCase, "https://www.instagram.com/", "http://www.instagram.com/"))
            {
                response = await _httpClient.GetAsync(usernameOrUrl);
            }

            else
            {
                response = await _httpClient.GetAsync($"https://www.instagram.com/{usernameOrUrl}/");
            }
            
            var html = await response.Content.ReadAsStringAsync();

            response.Dispose();

            // Splits the content between the specified tags into a substring - which is the JSON data we need
            // (Most of this JSON data is useless, so there may be room for optimization here to decrease parsing time)
            var json = html.SubstringBetween("<script type=\"text/javascript\">window._sharedData = ", ";</script>");

            // If appropriate JSON data cannot be found, the page we are trying to access must be unavailable, so an exception is thrown
            if (json is null)
            {
                throw ContentUnavailableException.PageUnavailable(usernameOrUrl);
            }

            var jObject = JObject.Parse(json);

#nullable disable

            // If a 'LoginAndSignupPage' token exists, Instagram has re-directed our request, so an exception is thrown
            if (jObject["entry_data"]["LoginAndSignupPage"] is not null)
            {
                throw ClientUnauthorizedException.InvalidSessionId();
            }

            // If a 'ProfilePage' token cannot be found, the username is invalid, so an exception is thrown
            if (jObject["entry_data"]["ProfilePage"] is null)
            {
                throw ProfileNotFoundException.InvalidUsernameOrUrl(usernameOrUrl);
            }

            var profilePictureUri = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["profile_pic_url_hd"].ToString();
            var handle = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["username"].ToString();
            var isVerified = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["is_verified"].ToObject<bool>();
            var fullName = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["full_name"].ToString();
            var postCount = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["edge_owner_to_timeline_media"]["count"].ToObject<double>();
            var followerCount = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["edge_followed_by"]["count"].ToObject<double>();
            var followingCount = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["edge_follow"]["count"].ToObject<double>();
            var isBusinessAccount = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["is_business_account"].ToObject<bool>();
            var businessCategoryName = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["category_name"].ToString();
            var bio = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["biography"].ToString();
            var website = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["external_url"].ToString();
            var isPrivate = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["is_private"].ToObject<bool>();

#nullable restore

            return new Profile(
                profilePictureUri,
                handle,
                isVerified,
                postCount,
                followerCount,
                followingCount,
                fullName,
                isBusinessAccount,
                businessCategoryName,
                bio,
                website,
                isPrivate);
        }

        /// <summary>
        /// Downloads the high-definition profile picture of the specified <paramref name="profile"/> to the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="profile">The profile which will have it's profile picture downloaded.</param>
        /// <param name="path">The profile picture's download location. (Must include a file name and extension.)</param>
        public async Task DownloadProfilePictureAsync(Profile profile, string path)
        {
            using var response = await _httpClient.GetAsync(profile.ProfilePictureUri);
            using var stream = new FileStream(path, FileMode.Create);
            await response.Content.CopyToAsync(stream);
        }

        /// <summary>
        /// Enumerates over Instagram profiles matching the specified <paramref name="searchQuery"/>.
        /// </summary>
        /// <param name="searchQuery">The profile to search for.</param>
        public async IAsyncEnumerable<ProfileSearchResult> SearchForProfilesAsync(string searchQuery)
        {
            using var response = await _httpClient.GetAsync($"https://www.instagram.com/web/search/topsearch/?query={searchQuery}/");
            var json = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(json);

#nullable disable

            for (int i = 0; i < jObject["users"].Count(); ++i)
            {
                var profilePictureUri = jObject["users"][i]["user"]["profile_pic_url"].ToString();
                var handle = jObject["users"][i]["user"]["username"].ToString();
                var isVerified = jObject["users"][i]["user"]["is_verified"].ToObject<bool>();
                var fullName = jObject["users"][i]["user"]["full_name"].ToString();
                var isPrivate = jObject["users"][i]["user"]["is_private"].ToObject<bool>();

                yield return new ProfileSearchResult(
                    profilePictureUri,
                    handle,
                    isVerified,
                    fullName,
                    isPrivate);
            }

#nullable restore

        }
    }
}