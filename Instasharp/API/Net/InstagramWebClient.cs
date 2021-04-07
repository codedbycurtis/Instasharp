using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Instasharp.Profiles;
using Instasharp.Internal.Extensions;
using Instasharp.Exceptions;

namespace Instasharp.Net
{
    /// <summary>
    /// Serves as an interface between Instagram and client applications.
    /// </summary>
    public class InstagramWebClient
    {
        private readonly HttpClient _httpClient = new();

        /// <summary>
        /// Initializes a new instance of <see cref="InstagramWebClient"/>.
        /// </summary>
        public InstagramWebClient()
        {
            // Without an authorized 'sessionid,' Instagram re-directs all requests to the login page
            _httpClient.DefaultRequestHeaders.Add("cookie", "sessionid=2602413198%3AG5NYxERN6kSgS6%3A14");            
        }

        /// <summary>
        /// Scrapes metadata from the Instagram profile with the specified <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username (handle) of the user.</param>
        /// <returns>A <see cref="Profile"/> object representing the acquired metadata.</returns>
        public async Task<Profile> GetProfileMetadataAsync(string username)
        {
            var response = _httpClient.GetAsync($"http://www.instagram.com/{username}/").Result;
            var html = await response.Content.ReadAsStringAsync();
            var json = html.Parse("<script type=\"text/javascript\">window._sharedData = ", ";</script>");
            var jObject = JObject.Parse(json);

#nullable disable

            // If a 'ProfilePage' token cannot be found, the username is invalid so an exception is thrown
            if (jObject["entry_data"]["ProfilePage"] is null)
            {
                throw ProfileNotFoundException.InvalidUsername(username);
            }

            var profilePictureUri = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["profile_pic_url_hd"].ToString();
            var handle = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["username"].ToString();
            var fullName = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["full_name"].ToString();
            var postCount = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["edge_owner_to_timeline_media"]["count"].ToObject<double>();
            var followerCount = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["edge_followed_by"]["count"].ToObject<double>();
            var followingCount = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["edge_follow"]["count"].ToObject<double>();
            var isBusinessAccount = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["is_business_account"].ToObject<bool>();
            var businessCategoryName = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["category_name"].ToString();
            var bio = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["biography"].ToString();
            var website = jObject["entry_data"]["ProfilePage"][0]["graphql"]["user"]["external_url"].ToString();

#nullable restore

            return new Profile(
                profilePictureUri,
                handle,
                postCount,
                followerCount,
                followingCount,
                fullName,
                isBusinessAccount,
                businessCategoryName,
                bio,
                website);
        }
    }
}
