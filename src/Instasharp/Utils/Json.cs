using Newtonsoft.Json.Linq;
using Instasharp.Profiles;
using Instasharp.Search;

namespace Instasharp.Utils
{
    internal static class Json
    {

#nullable disable

        internal static Profile ParseInstagramProfilePayload(JObject payload)
        {
            var parsedProfile = payload["entry_data"]["ProfilePage"][0]["graphql"]["user"];
            var profilePictureUri = parsedProfile["profile_pic_url_hd"].ToString();
            var handle = parsedProfile["username"].ToString();
            var isVerified = parsedProfile["is_verified"].ToObject<bool>();
            var fullName = parsedProfile["full_name"].ToString();
            var postCount = parsedProfile["edge_owner_to_timeline_media"]["count"].ToObject<double>();
            var followerCount = parsedProfile["edge_followed_by"]["count"].ToObject<double>();
            var followingCount = parsedProfile["edge_follow"]["count"].ToObject<double>();
            var isBusinessAccount = parsedProfile["is_business_account"].ToObject<bool>();
            var businessCategoryName = parsedProfile["category_name"].ToString();
            var bio = parsedProfile["biography"].ToString();
            var website = parsedProfile["external_url"].ToString();
            var isPrivate = parsedProfile["is_private"].ToObject<bool>();

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

        internal static ProfileSearchResult ParseInstagramProfileSearchResultPayload(JToken payload)
        {
            var profilePictureUri = payload["user"]["profile_pic_url"].ToString();
            var handle = payload["user"]["username"].ToString();
            var isVerified = payload["user"]["is_verified"].ToObject<bool>();
            var fullName = payload["user"]["full_name"].ToString();
            var isPrivate = payload["user"]["is_private"].ToObject<bool>();

            return new ProfileSearchResult(
                profilePictureUri,
                handle,
                isVerified,
                fullName,
                isPrivate);
        }

#nullable restore

    }
}
