using System;

namespace Instasharp.Exceptions
{
    /// <summary>
    /// Thrown if the content of an Instagram account cannot be obtained.
    /// </summary>
    [Serializable]
    public partial class ContentUnavailableException : InstasharpException
    {
        /// <summary>
        /// The username/URL of the account whose content cannot be retrieved.
        /// </summary>
        public string? UsernameOrUrl { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ContentUnavailableException"/> with the specified <paramref name="message"/>
        /// and <paramref name="usernameOrUrl"/>.
        /// </summary>
        public ContentUnavailableException(string message, string usernameOrUrl) : base(message) { UsernameOrUrl = usernameOrUrl; }
    }

    public partial class ContentUnavailableException
    {
        public static ContentUnavailableException PageUnavailable(string usernameOrUrl)
        {
            var message = $@"The content of the Instagram account '{usernameOrUrl}' could not be obtained.
This may be due to the user having blocked the account associated with the specified Session ID.
Alternatively, the account may have existed at one point, but has since been banned or deleted.
Additionally, if a URL is being used, ensure the path is that of an Instagram account.
If the issue persists, feel free to report this issue on the project's Github page.";

            return new ContentUnavailableException(message, usernameOrUrl);
        }
    }
}
