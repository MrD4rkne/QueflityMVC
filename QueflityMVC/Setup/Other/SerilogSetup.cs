using Serilog;

namespace QueflityMVC.Web.Setup.Other
{
    public static class SerilogSetup
    {
        private const string LOG_FILE = "Logs/log-.log";

        public static void SetupLogger()
        {
            using var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(LOG_FILE, rollingInterval: RollingInterval.Day)
    .CreateLogger();

            Log.Logger = log;
            Log.Information("Logger has been configured.");
        }
    }
}