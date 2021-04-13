using System;

namespace Instasharp.Exceptions
{
    /// <summary>
    /// Thrown if a profile with the specified username could not be found.
    /// </summary>
    [Serializable]
    public partial class ProfileNotFoundException : Exception
    {
        /// <summary>
        /// The username/URL of the non-existent account.
        /// </summary>
        public string? UsernameOrUrl { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ProfileNotFoundException"/> with the specified <paramref name="message"/> and <paramref name="usernameOrUrl"/>.
        /// </summary>
        public ProfileNotFoundException(string message, string usernameOrUrl) : base(message) { UsernameOrUrl = usernameOrUrl; }
        public ProfileNotFoundException() { }
        public ProfileNotFoundException(string message) : base(message) { }
        public ProfileNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ProfileNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public partial class ProfileNotFoundException
    {
        public static ProfileNotFoundException InvalidUsernameOrUrl(string usernameOrUrl)
        {
            var message = $@"A profile with the username/URL '{usernameOrUrl}' could not be found.
If the username/URL is valid, then Instagram's servers are probably down.
If Instagram is working correctly, and this issue persists, Instagram have likely changed their HTTP response, so feel free to report it on the project's Github page.";

            return new ProfileNotFoundException(message, usernameOrUrl);
        }
    }
}
