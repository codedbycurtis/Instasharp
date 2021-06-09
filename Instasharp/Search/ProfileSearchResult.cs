using System.Threading.Tasks;
using System.Collections.Generic;
using Instasharp.Profiles;
using Instasharp.Internal.Extensions;

namespace Instasharp.Search
{
    /// <summary>
    /// Profile metadata returned from searches.
    /// </summary>
    public class ProfileSearchResult : IProfile
    {
        ///<inheritdoc />
        public string ProfilePictureUri { get; }

        ///<inheritdoc />
        public string Handle { get; }

        ///<inheritdoc />
        public bool IsVerified { get; }

        ///<inheritdoc />
        public string? FullName { get; }

        ///<inheritdoc />
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
        /// Enumerates through the contents of an <see cref="IAsyncEnumerable{T}"/> and collects them into an <see cref="IReadOnlyList{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyList{T}"/> of <see cref="ProfileSearchResult"/>.</returns>
        public static async Task<IReadOnlyList<T>> CollectAsync<T>(
            this IAsyncEnumerable<T> source) where T : ProfileSearchResult => await source.ToListAsync();

        /// <summary>
        /// Enumerates through the contents of an <see cref="IAsyncEnumerable{T}"/> and collects the first <paramref name="count"/> number of results into an <see cref="IReadOnlyList{T}"/>.
        /// </summary>
        /// <param name="count">The number of search results to return.</param>
        /// <returns>An <see cref="IReadOnlyList{T}"/> of <see cref="ProfileSearchResult"/>.</returns>
        public static async Task<IReadOnlyList<T>> CollectAsync<T>(
            this IAsyncEnumerable<T> source, int count) where T : ProfileSearchResult => await source.ToListAsync(count);
    }
}
