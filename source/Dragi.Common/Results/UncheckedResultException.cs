namespace Dragi.Common.Results;

public class UncheckedResultException : Exception
{
    public UncheckedResultException()
    {
    }

    public UncheckedResultException(string? message) : base(message)
    {
    }

    public UncheckedResultException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
