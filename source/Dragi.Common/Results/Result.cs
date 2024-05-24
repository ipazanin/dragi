namespace Dragi.Common.Results;

public class Result
{
    private readonly Exception? _exception;
    private readonly string? _message;
    private readonly IReadOnlyList<string>? _messages;

    public static Result Success { get; } = new Result();

    protected Result()
    {
    }

    public Result(Exception exception)
    {
        _exception = exception;
    }

    public Result(string message)
    {
        _message = message;
    }

    public Result(IReadOnlyList<string> messages)
    {
        _messages = messages;
    }

    public string FailReason
    {
        get
        {
            if (IsSuccess)
            {
                throw new UncheckedResultException("Result is in success state");
            }

            return _exception?.Message ?? _message ?? string.Join(", ", _messages!);
        }
    }

    public bool IsSuccess => _exception is null && _message is null && _messages is null;

    public bool IsFail => !IsSuccess;

    public static implicit operator Result(Exception exception)
    {
        return new Result(exception);
    }

    public static implicit operator Result(string message)
    {
        return new Result(message);
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue value)
    {
        _value = value;
    }

    public Result(Exception exception) : base(exception)
    {
    }

    public Result(string message) : base(message)
    {
    }

    public Result(IReadOnlyList<string> messages) : base(messages)
    {
    }

    public TValue Value
    {
        get
        {
            if (IsFail)
            {
                throw new UncheckedResultException("Result is in failed state");
            }

            return _value!;
        }
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        if (value is Result<TValue> result)
        {
            return result;
        }

        return new Result<TValue>(value);
    }

    public static implicit operator Result<TValue>(Exception exception)
    {
        return new Result<TValue>(exception);
    }

    public static implicit operator Result<TValue>(string message)
    {
        return new Result<TValue>(message);
    }
}
