using System;

namespace Instasharp.Exceptions
{
    /// <summary>
    /// Thrown when the <see cref="InstagramClient"/> is unauthorized and cannot retrieve the requested metadata.
    /// </summary>
    [Serializable]
    public partial class ClientUnauthorizedException : InstasharpException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ClientUnauthorizedException"/> with the specified <paramref name="message"/>.
        /// </summary>
        public ClientUnauthorizedException(string message) : base(message) { }
    }

    public partial class ClientUnauthorizedException
    {
        public static ClientUnauthorizedException InvalidSessionId()
        {
            var message = $@"Metadata retrieval failed - this is likely due to an unauthorized client.
Try using a valid Session ID.
If this does not work, and the issue persists, report it on the project's Github page.";

            return new ClientUnauthorizedException(message);
        }
    }
}
