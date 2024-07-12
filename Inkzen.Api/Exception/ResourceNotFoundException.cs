using System;

namespace Inkzen.Api;

public class ResourceNotFoundException : Exception
{
    private ResourceNotFoundException() : base() { }
    private ResourceNotFoundException(string message) : base(message) { }
    private ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    public ResourceNotFoundException(int statusCode, DateTimeOffset timestamp, string message, string details) : base(message)
        => ErrorDetails = new(statusCode, timestamp, message, details);

    public ResourceNotFoundException(int statusCode, DateTimeOffset timestamp, string message, string details, Exception innerException) : base(message, innerException)
        => ErrorDetails = new(statusCode, timestamp, message, details);
    public ErrorDetails? ErrorDetails { get; }
}
