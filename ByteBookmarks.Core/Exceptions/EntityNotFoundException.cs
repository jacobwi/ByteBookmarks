#region

using System.Runtime.Serialization;

#endregion

namespace ByteBookmarks.Core.Exceptions;

public class EntityNotFoundException : Exception
{
    // Create multiple constructors to allow for different ways of creating the exception
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }

    public EntityNotFoundException(string name, object key, Exception innerException)
        : base($"Entity \"{name}\" ({key}) was not found.", innerException)
    {
    }

    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}