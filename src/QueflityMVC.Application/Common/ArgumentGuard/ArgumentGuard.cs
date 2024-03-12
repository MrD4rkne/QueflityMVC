using QueflityMVC.Application.Errors.Common;

namespace QueflityMVC.Application.Common.ArgumentGuard;

public static class ArgumentGuard
{
    public static void MustBe<T>(this T param, ArgumentGuardType argGuardType, T referencedValue) where T : struct,
        IComparable,
        IComparable<T>,
        IConvertible,
        IEquatable<T>,
        IFormattable
    {
        ValidateArgument(param, referencedValue, argGuardType, null);
    }

    public static void ValidateArgument<T>(T inputValue, T referencedValue, ArgumentGuardType argGuardType,
        string? paramName) where T : struct,
        IComparable,
        IComparable<T>,
        IConvertible,
        IEquatable<T>,
        IFormattable
    {
        var comparision = inputValue.CompareTo(referencedValue);

        switch (argGuardType)
        {
            case ArgumentGuardType.GreaterThan:
                if (IsGreaterThan(comparision))
                    return;
                break;

            case ArgumentGuardType.GreaterThanOrEquals:
                if (IsGreaterThanOrEquals(comparision))
                    return;
                break;

            case ArgumentGuardType.LessThan:
                if (IsLessThan(comparision))
                    return;
                break;

            case ArgumentGuardType.LessThanOrEquals:
                if (IsLessThanOrEquals(comparision))
                    return;
                break;

            case ArgumentGuardType.Equals:
                if (IsEquals(comparision))
                    return;
                break;

            case ArgumentGuardType.OtherThan:
                if (OtherThan(comparision))
                    return;
                break;
        }

        // Argument did not match expectations
        throw new ArgumentOutOfBoundsException<T>(argGuardType, referencedValue, paramName);
    }

    private static bool IsGreaterThanOrEquals(int comparision)
    {
        var isGreaterThanOrEquals = comparision >= 0;
        return isGreaterThanOrEquals;
    }

    private static bool IsGreaterThan(int comparision)
    {
        var isGreaterThanOrEquals = comparision > 0;
        return isGreaterThanOrEquals;
    }

    private static bool IsLessThan(int comparision)
    {
        var isLessThan = comparision < 0;
        return isLessThan;
    }

    private static bool IsLessThanOrEquals(int comparision)
    {
        var isLessThanOrEquals = comparision <= 0;
        return isLessThanOrEquals;
    }

    private static bool IsEquals(int comparision)
    {
        var isEquals = comparision == 0;
        return isEquals;
    }

    private static bool OtherThan(int comparision)
    {
        var otherThan = comparision != 0;
        return otherThan;
    }
}