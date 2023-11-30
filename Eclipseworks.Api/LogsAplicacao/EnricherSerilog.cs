using Serilog;
using Serilog.Formatting.Json;
using System.Net;


namespace Eclipseworks.Api.LogsAplicacao;

public static class SerilogConfig
{
    public static WebApplicationBuilder ConfigurarSerilog(this WebApplicationBuilder builder)
    {

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            //.Enrich.With<EnricherSerilog>()
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName ?? "")
            .Enrich.WithProperty("ApplicationName", builder.Environment.ApplicationName ?? "")
            .Enrich.WithProperty("HostName", Dns.GetHostName())
            .Enrich.WithClientIp()
            .WriteTo.Console(new JsonFormatter(renderMessage: true, closingDelimiter: "\n"))
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception} {UserId}")

            .CreateBootstrapLogger();

        builder.Host.UseSerilog();
        return builder;
    }
}