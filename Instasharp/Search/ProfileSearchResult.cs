using System.Threading.Tasks;
using System.Collections.Generic;
using Instasharp.Internal.Extensions;

namespace Instasharp.Search
{
    /// <summary>
    /// Profile metadata returned from searches.
    /// </summary>
    public partial class ProfileSearchResult
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

        /// <summary>
        /// Initializes a new instance of <see cref="ProfileSearchResult"/>.
        /// </summary>
        public ProfileSearchResult(
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

    /// <summary>
    /// Useful methods for <see cref="ProfileSearchResult"/> collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Enumerates through the contents of an <see cref="IAsyncEnumerable{T}"/> and collates them into an <see cref="IReadOnlyList{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyList{T}"/> of <see cref="ProfileSearchResult"/>.</returns>
        public static async ValueTask<IReadOnlyList<T>> CollateAsync<T>(
            this IAsyncEnumerable<T> source) where T : ProfileSearchResult => await source.ToListAsync();
    }
}
