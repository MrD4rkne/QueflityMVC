namespace QueflityMVC.Web.Common;

internal static class HelpersDependencyInjection
{
    internal static void AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<IMessageDateTimeFormatter, MessageDateTimeFormatter>();
    }
}