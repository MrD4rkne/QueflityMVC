using Microsoft.Extensions.Options;
using QueflityMVC.Infrastructure.Emails;

namespace QueflityMVC.Web.Setup;

internal class ConfigureSmtp(IOptions<SmtpOptions> smtpOptions) : IConfigureOptions<SmtpConfig>
{
    public void Configure(SmtpConfig options)
    {
        options.Host = smtpOptions.Value.Host;
        options.Port = smtpOptions.Value.Port;
        options.Username = smtpOptions.Value.Username;
        options.Password = smtpOptions.Value.Password;
        options.Email = smtpOptions.Value.Email;
    }
}