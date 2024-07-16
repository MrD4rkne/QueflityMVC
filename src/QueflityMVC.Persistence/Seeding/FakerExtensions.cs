using Bogus;

namespace QueflityMVC.Persistence.Seeding;

internal static class FakerExtensions
{
    /// <summary>
    ///     1-based faker index instead of built-in 0-based
    /// </summary>
    /// <param name="faker"></param>
    /// <returns></returns>
    public static int GetPositiveIndexFaker(this Faker faker)
    {
        return faker.IndexFaker + 1;
    }
}