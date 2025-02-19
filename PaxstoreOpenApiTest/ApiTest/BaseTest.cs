using NUnit.Framework;
using Serilog;

namespace Paxstore.Test
{
    public class BaseTest
    {

        [SetUp]
        public void Setup() {
            var logOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}][{ThreadId}] {Message:lj}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code, outputTemplate: logOutputTemplate).CreateLogger();
        }

    }
}
