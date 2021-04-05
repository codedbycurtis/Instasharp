namespace Instasharp.Models
{
    /// <summary>
    /// Publicly-available metadata of an Instagram profile.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// The URI to an Instagram account's profile picture.
        /// </summary>
        public string ProfilePictureUri { get; }

        /// <summary>
        /// The Instagram account's handle (username).
        /// </summary>
        public string Handle { get; }

        /// <summary>
        /// The number of posts on the account.
        /// </summary>
        public double PostCount { get; }

        /// <summary>
        /// The number of followers the account has.
        /// </summary>
        public double FollowerCount { get; }

        /// <summary>
        /// The number of accounts the user is following.
        /// </summary>
        public double FollowingCount { get; }

        /// <summary>
        /// The user's full name as displayed on their profile.
        /// </summary>
        public string? FullName { get; }

        /// <summary>
        /// The user's bio.
        /// </summary>
        public string? Bio { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Profile"/>.
        /// </summary>
        public Profile(
            string profilePictureUri,
            string handle,
            double postCount,
            double followerCount,
            double followingCount,
            string? fullName,
            string? bio)
        {
            ProfilePictureUri = profilePictureUri;
            Handle = handle;
            PostCount = postCount;
            FollowerCount = followerCount;
            FollowingCount = followingCount;
            FullName = fullName;
            Bio = bio;
        }
    }
}
