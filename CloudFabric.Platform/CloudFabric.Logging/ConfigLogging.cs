using Loggly;
using Serilog;
using Serilog.Core;
using Loggly.Config;
using Serilog.Events;

namespace CloudFabric.Logging
{
    public class ConfigLogging
    {
        public Logger CreateLogger(string _environment, string _appName, string token)
        {
            var config = LogglyConfig.Instance;
            config.CustomerToken = token;
            config.ApplicationName = $"{_appName}-{_environment}";

            config.Transport.EndpointHostname = "logs-01.loggly.com";
            config.Transport.EndpointPort = 443;
            config.Transport.LogTransport = LogTransport.Https;

            var ct = new ApplicationNameTag();
            ct.Formatter = $"{_appName}-{_environment}";
            config.TagConfig.Tags.Add(ct);

            return new LoggerConfiguration()
                            .MinimumLevel.Verbose()
                            .MinimumLevel.Override("System", LogEventLevel.Warning)
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .Enrich.FromLogContext()
                            .WriteTo.Loggly(restrictedToMinimumLevel: LogEventLevel.Information)
                            .CreateLogger();
        }
    }
}
