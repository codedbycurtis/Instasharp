using System;

namespace Instasharp.Exceptions
{
    /// <summary>
    /// Thrown when the <see cref="InstagramClient"/> fails to retrieve the requested metadata due to lacking authorization.
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
        public static ClientUnauthorizedException SessionIDRequiredOrInvalid()
        {
            var message = $@"Metadata retrieval failed; this is likely due to an unauthorized InstagramClient.
Try using a valid SessionID, if you are not.
If this does not work, and the issue persists, report it on the project's Github page.";

            return new ClientUnauthorizedException(message);
        }
    }
}
