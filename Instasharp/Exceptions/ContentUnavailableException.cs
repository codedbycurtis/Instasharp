using System;

namespace Instasharp.Exceptions
{
    /// <summary>
    /// Thrown if the content of an Instagram account cannot be obtained.
    /// </summary>
    [Serializable]
    public partial class ContentUnavailableException : Exception
    {
        /// <summary>
        /// The username/URL of the account whose content cannot be retrieved.
        /// </summary>
        public string? UsernameOrUrl { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ContentUnavailableException"/> with the specified <paramref name="message"/> and <paramref name="usernameOrUrl"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="usernameOrUrl"></param>
        public ContentUnavailableException(string message, string usernameOrUrl) : base(message) { UsernameOrUrl = usernameOrUrl; }
        public ContentUnavailableException() { }
        public ContentUnavailableException(string message) : base(message) { }
        public ContentUnavailableException(string message, Exception inner) : base(message, inner) { }
        protected ContentUnavailableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public partial class ContentUnavailableException
    {
        public static ContentUnavailableException Unavailable(string usernameOrUrl)
        {
            var message = $@"The content of the Instagram account '{usernameOrUrl}' could not be obtained.
This may be due to the user having blocked the account associated with the specified SessionID.
Alternatively, the account may have existed at one point, but has since been banned or deleted.
If none of these theories are true, feel free to report this issue on the project's Github page.";

            return new ContentUnavailableException(message, usernameOrUrl);
        }
    }
}
