namespace QueflityMVC.Application.UnitTests.Common;

internal static class GuidExtensions
{
    public static Guid GetDifferentGuid(this Guid guid)
    {
        return GetDifferentGuid([guid]);
    }
    
    public static Guid GetDifferentGuid(Guid[] guids)
    {
        Guid newGuid;
        do
        {
            newGuid = Guid.NewGuid();
        }while(guids.Contains(newGuid));
        return newGuid;
    }
}