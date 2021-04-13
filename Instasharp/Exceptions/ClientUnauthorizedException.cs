using System;
using Instasharp.Net;

namespace Instasharp.Exceptions
{
    /// <summary>
    /// Thrown when the <see cref="InstagramWebClient"/> fails to retrieve the requested metadata due to lacking authorization.
    /// </summary>
    [Serializable]
    public partial class ClientUnauthorizedException : Exception
    {
        public ClientUnauthorizedException() { }
        public ClientUnauthorizedException(string message) : base(message) { }
        public ClientUnauthorizedException(string message, Exception inner) : base(message, inner) { }
        protected ClientUnauthorizedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public partial class ClientUnauthorizedException
    {
        public static ClientUnauthorizedException SessionIDRequired()
        {
            var message = $@"Metadata retrieval failed; this is likely due to an unauthorized InstagramWebClient.
Try using a valid SessionID, if you are not.
If this does not work, and the issue persists, report it on the project's Github page.";

            return new ClientUnauthorizedException(message);
        }
    }
}
