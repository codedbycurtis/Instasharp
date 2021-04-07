namespace Instasharp.Profiles
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
        /// Is the account a 'Business Account.'
        /// </summary>
        public bool IsBusinessAccount { get; }

        /// <summary>
        /// If the account is a 'Business Account,' this is the type.
        /// </summary>
        public string? BusinessCategoryName { get; }

        /// <summary>
        /// The user's bio.
        /// </summary>
        public string? Bio { get; }

        /// <summary>
        /// The user's website as displayed on their profile.
        /// </summary>
        public string? Website { get; }

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
            bool isBusinessAccount,
            string? businessCategoryName,
            string? bio,
            string? website)
        {
            ProfilePictureUri = profilePictureUri;
            Handle = handle;
            PostCount = postCount;
            FollowerCount = followerCount;
            FollowingCount = followingCount;
            FullName = fullName;
            IsBusinessAccount = isBusinessAccount;
            BusinessCategoryName = businessCategoryName;
            Bio = bio;
            Website = website;
        }

        public override string ToString()
        {
            return $@"Profile Picture Uri: {this.ProfilePictureUri}
Handle: {this.Handle}
Post Count: {this.PostCount}
Follower Count: {this.FollowerCount}
Following Count: {this.FollowingCount}
Full Name: {this.FullName}
Is Business Account: {this.IsBusinessAccount}
Business Account Type: {this.BusinessCategoryName}
Bio: {this.Bio}
Website: {this.Website}";
        }
    }
}
