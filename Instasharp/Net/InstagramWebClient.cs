using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Instasharp.Profiles;
using Instasharp.Internal.Extensions;
using Instasharp.Exceptions;

namespace Instasharp.Net
{
    /// <summary>
    /// Serves as an interface between Instagram's website and client applications.
    /// </summary>
    public class InstagramWebClient
    {
        private readonly HttpClient _httpClient = new();

        /// <summary>
        /// Initializes a new instance of <see cref="InstagramWebClient"/>.
        /// </summary>
        public InstagramWebClient() { }

        /// <summary>
        /// Initializes a new instance of <see cref="InstagramWebClient"/> with the specified <paramref name="sessionId"/>.
        /// </summary>
        /// <param name="sessionId">A valid SessionID may be required to stop Instagram re-directing requests to the login page.</param>
        public InstagramWebClient(string sessionId)
        {
            // Without an authorized 'sessionid,' Instagram may re-direct requests to the login page.
            _httpClient.DefaultRequestHeaders.Add("cookie", $"sessionid={sessionId}");
        }

        /// <summary>
        /// Scrapes metadata from the Instagram profile with the specified <paramref name="usernameOrUrl"/>.
        /// <para>URLs must begin with 'https://'.</para>
        /// </summary>
        /// <param name="usernameOrUrl">The username (handle) of the user, or the link to the user's profile.</param>
        /// <returns>A <see cref="Profile"/> object representing the acquired metadata.</returns>
        public async Task<Profile> GetProfileMetadataAsync(string usernameOrUrl)
        {
            HttpResponseMessage response;

            if (usernameOrUrl.StartsWith(StringComparison.OrdinalIgnoreCase, "https://www.instagram.com/", "http://www.instagram.com/"))
            {
                response = await _httpClient.GetAsync(usernameOrUrl).ConfigureAwait(false);
            }

            else
            {
                response = await _httpClient.GetAsync($"http://www.instagram.com/{usernameOrUrl}/").ConfigureAwait(false);
            }
            
            var html = await response.Content.ReadAsStringAsync();

            // HttpResponseMessage is not used after this point, so we can release its resources here
            response.Dispose();

            var json = html.Parse("<script type=\"text/javascript\">window._sharedData = ", ";</script>");

            if (json is null)
            {
                throw ContentUnavailableException.Unavailable(usernameOrUrl);
            }

            var jObject = JObject.Parse(json);

#nullable disable

            // If a 'LoginAndSignupPage' token exists, Instagram has re-directed our request, so an exception is thrown
            if (jObject["entry_data"]["LoginAndSignupPage"] is not null)
            {
                throw ClientUnauthorizedException.SessionIDRequired();
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

        public async Task DownloadProfilePictureAsync(Profile profile, string path)
        {
            using var response = await _httpClient.GetAsync(profile.ProfilePictureUri).ConfigureAwait(false);
            using var stream = new FileStream(path, FileMode.Create);
            await response.Content.CopyToAsync(stream).ConfigureAwait(false);
        }
    }
}
