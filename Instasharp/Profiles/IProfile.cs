namespace Instasharp.Profiles
{
    /// <summary>
    /// Properties shared by all profile types.
    /// </summary>
    public interface IProfile
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
        /// Is the account private.
        /// </summary>
        public bool IsPrivate { get; }
    }
}
