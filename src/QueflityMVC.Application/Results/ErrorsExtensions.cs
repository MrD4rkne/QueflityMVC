namespace QueflityMVC.Application.Results;

public static class ErrorsExtensions
{
    public static bool IsOfCode(this Error error, string code)
    {
        return error.Code == code;
    }
}