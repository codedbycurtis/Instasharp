using System;

namespace Instasharp.Exceptions
{
    /// <summary>
    /// Parent class for all exceptions thrown by <see cref="Instasharp"/>.
    /// </summary>
    [Serializable]
    public abstract class InstasharpException : Exception
    {
        protected InstasharpException(string message) : base(message)
        {
        }
    }
}
