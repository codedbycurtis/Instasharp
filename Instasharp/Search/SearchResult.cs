namespace Instasharp.Search
{
    /// <summary>
    /// Profile metadata returned from searches.
    /// </summary>
    public class SearchResult
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
        /// Is the account verified.
        /// </summary>
        public bool IsVerified { get; }

        /// <summary>
        /// The user's full name as displayed on their profile.
        /// </summary>
        public string? FullName { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Profile"/>.
        /// </summary>
        public bool IsPrivate { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="SearchResult"/>.
        /// </summary>
        public SearchResult(
            string profilePictureUri,
            string handle,
            bool isVerified,
            string? fullName,
            bool isPrivate)
        {
            ProfilePictureUri = profilePictureUri;
            Handle = handle;
            IsVerified = isVerified;
            FullName = fullName;
            IsPrivate = isPrivate;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $@"Profile Picture Uri: {this.ProfilePictureUri}
Handle: {this.Handle}
Is Verified: {this.IsVerified}
Full Name: {this.FullName}
Is Private: {this.IsPrivate}";
        }
    }
}
