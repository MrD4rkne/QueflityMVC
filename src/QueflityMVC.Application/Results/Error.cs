using System.Text;

namespace QueflityMVC.Application.Results;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(Code);
        if (!string.IsNullOrEmpty(Description))
        {
            sb.Append(": ");
            sb.Append(Description);
        }
        return sb.ToString();
    }
}