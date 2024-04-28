namespace QueflityMVC.Application.Results;

public static class ErrorsExtensions
{
    public static bool IsOfCode(this Error error, string code)
    {
        return error.Code == code;
    }

    public static T Match<T>(
        this Result result,
        Func<T> onSuccess,
        Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }
}