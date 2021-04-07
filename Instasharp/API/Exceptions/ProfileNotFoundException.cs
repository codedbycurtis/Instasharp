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
        /// The username of the non-existent account.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ProfileNotFoundException"/>.
        /// </summary>
        public ProfileNotFoundException(string message, string username) : base(message) { Username = username; }
    }

    public partial class ProfileNotFoundException
    {
        public static ProfileNotFoundException InvalidUsername(string username)
        {
            var message = $@"A profile with the username '{username}' could not be found.
If the username is valid, then Instagram's servers are probably down.
If Instagram is working correctly, and this issue persists, Instagram have likely changed their HTTP response, so feel free to report it on the project's Github page.";

            return new ProfileNotFoundException(message, username);
        }
    }
}
