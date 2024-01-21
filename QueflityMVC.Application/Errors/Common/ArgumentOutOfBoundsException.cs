using QueflityMVC.Application.Common.ArgumentGuard;

namespace QueflityMVC.Application.Errors.Common
{
    public class ArgumentOutOfBoundsException<T> : ArgumentOutOfRangeException where T : struct,
          IComparable,
          IComparable<T>,
          IConvertible,
          IEquatable<T>,
          IFormattable
    {
        public ArgumentOutOfBoundsException(ArgumentGuardType argumentGuardType, T referencedValue, string? paramName) : base(paramName, GetErrorMessage(argumentGuardType, referencedValue))
        {
        }

        private static string? GetErrorMessage(ArgumentGuardType argumentGuardType, T referencedValue) => argumentGuardType switch
        {
            ArgumentGuardType.Equals => $"Argument must be equal to {referencedValue}",
            ArgumentGuardType.GreaterThan => $"Argument must be greater than {referencedValue}",
            ArgumentGuardType.GreaterThanOrEquals => $"Argument must be greater than or equal to {referencedValue}",
            ArgumentGuardType.LessThan => $"Argument must be less than {referencedValue}",
            ArgumentGuardType.LessThanOrEquals => $"Argument must be less than or equal to {referencedValue}",
            ArgumentGuardType.OtherThan => $"Argument must not equal to {referencedValue}",
            _ => throw new NotImplementedException()
        };
    }
}