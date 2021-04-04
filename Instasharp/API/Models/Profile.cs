namespace Instasharp.Models
{
    /// <summary>
    /// Publicly-available metadata regarding an Instagram profile.
    /// </summary>
    public class Profile
    {
        public string ProfilePictureUri { get; }
        public string Handle { get; }
        public double PostCount { get; }
        public double FollowerCount { get; }
        public double FollowingCount { get; }
        public string FullName { get; }
        public string Bio { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Profile"/>.
        /// </summary>
        public Profile(
            string profilePictureUri,
            string handle,
            double postCount,
            double followerCount,
            double followingCount,
            string fullName,
            string bio)
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
