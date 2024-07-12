using System;

namespace Inkzen.Api;

public class ErrorDetails
{
    public int? Status { get; private set; }
    public DateTimeOffset? Timestamp { get; private set; }
    public string? Message { get; private set; }
    public string? Details { get; private set; }


    public ErrorDetails(DateTimeOffset timestamp, string message, string details)
    {
        Timestamp = timestamp;
        Message = message;
        Details = details;
    }

    public ErrorDetails(int status, DateTimeOffset timestamp, string message, string details)
    {

        Status = status;
        Timestamp = timestamp;
        Message = message;
        Details = details;
    }

}
