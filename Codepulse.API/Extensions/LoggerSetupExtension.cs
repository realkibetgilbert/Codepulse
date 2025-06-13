using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Codepulse.API.Extensions
{
    public static class LoggerSetupExtension
    {
        public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
{
    builder.Host.UseSerilog();

    // Detect if running in Docker
    var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

    var loggerConfig = new LoggerConfiguration()
        .MinimumLevel.Warning()
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning);

    if (isDocker)
    {
        
        loggerConfig.WriteTo.Console();
    }
    else
    {
        
        var logPath = builder.Configuration["Log_Path"] ?? "C:\\Temp\\CODEPULSELOGS.Logs-.log";
        loggerConfig.WriteTo.File(logPath, rollingInterval: RollingInterval.Day);
    }

    Log.Logger = loggerConfig.CreateLogger();

    return builder;
}
    }
}
