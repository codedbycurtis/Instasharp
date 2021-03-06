using System;

namespace Instasharp.Profiles
{
    /// <summary>
    /// Publicly-available metadata of an Instagram profile.
    /// </summary>
    public class Profile : IProfile
    {
        ///<inheritdoc />
        public string ProfilePictureUri { get; }

        ///<inheritdoc />
        public string Handle { get; }

        ///<inheritdoc />
        public bool IsVerified { get; }

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

        ///<inheritdoc />
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

        ///<inheritdoc />
        public bool IsPrivate { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Profile"/>.
        /// </summary>
        public Profile(
            string profilePictureUri,
            string handle,
            bool isVerified,
            double postCount,
            double followerCount,
            double followingCount,
            string? fullName,
            bool isBusinessAccount,
            string? businessCategoryName,
            string? bio,
            string? website,
            bool isPrivate)
        {
            this.ProfilePictureUri = profilePictureUri;
            this.Handle = handle;
            this.IsVerified = isVerified;
            this.PostCount = postCount;
            this.FollowerCount = followerCount;
            this.FollowingCount = followingCount;
            this.FullName = fullName;
            this.IsBusinessAccount = isBusinessAccount;
            this.BusinessCategoryName = businessCategoryName;
            this.Bio = bio;
            this.Website = website;
            this.IsPrivate = isPrivate;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $@"Profile Picture Uri: {this.ProfilePictureUri}
Handle: {this.Handle}
Is Verified: {this.IsVerified}
Post Count: {this.PostCount}
Follower Count: {this.FollowerCount}
Following Count: {this.FollowingCount}
Full Name: {this.FullName}
Is Business Account: {this.IsBusinessAccount}
Business Account Type: {this.BusinessCategoryName}
Bio: {this.Bio}
Website: {this.Website}
Is Private: {this.IsPrivate}";
        }
    }
}
