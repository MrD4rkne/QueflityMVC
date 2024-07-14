namespace QueflityMVC.Application.Results;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None) ||
            (!isSuccess && error == Error.None))
            throw new ArgumentException("Invalid error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }
}

public class Result<T> : Result
{
    private Result(bool isSuccess, Error error, T value) : base(isSuccess, error)
    {
        Value = value;
    }

    public T Value { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, Error.None, value);
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(false, error, default);
    }
}