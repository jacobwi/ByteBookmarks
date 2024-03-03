#region

using System.Runtime.Serialization;

#endregion

namespace ByteBookmarks.Core.Exceptions;

public class NotAuthorizedException : Exception
{
    public NotAuthorizedException()
    {
    }

    public NotAuthorizedException(string message)
        : base(message)
    {
    }

    public NotAuthorizedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected NotAuthorizedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}