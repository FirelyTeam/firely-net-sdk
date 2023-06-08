using Hl7.Fhir.InterfaceApplier.CLI.Abstractions;
using Hl7.Fhir.InterfaceApplier.CLI.Configurations;
using Hl7.Fhir.InterfaceApplier.CLI.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

var loggerConfig = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(getLogLevel())
    .WriteTo.Console();

if (getLogToFile())
{
    loggerConfig.WriteTo.File("./logs/interface-applier-.log", rollingInterval: RollingInterval.Day);
}

Log.Logger = loggerConfig.CreateLogger();

// Set up the DI container
var services = new ServiceCollection();

services.AddOptions<ProjectManagerConfiguration>().Configure(getProjectManagerConfiguration);

services.AddTransient<IProjectManager, ProjectManager>();
services.AddTransient<IInterfaceDiscoveryService, InterfaceDiscoveryService>();
services.AddTransient<IClassDiscoveryService, ClassDiscoveryService>();
services.AddTransient<IInterfaceApplierService, InterfaceApplierService>();
services.AddTransient<IInterfaceApplierManager, InterfaceApplierManager>();

services.AddLogging(builder =>
{
    // Do not filter any log levels using the framework. The filtering is being done by Serilog
    builder.SetMinimumLevel(LogLevel.Trace);
    builder.ClearProviders();
    builder.AddSerilog();
});

// Build the service provider
var serviceProvider = services.BuildServiceProvider();

Log.Information("*** Executing InterfaceApplier ***");

// Set the interfaces on classes
serviceProvider.GetRequiredService<IInterfaceApplierManager>().Run();

Log.Information("*** Finished executing InterfaceApplier ***");
Log.CloseAndFlush();

#region Private Methods

void getProjectManagerConfiguration(ProjectManagerConfiguration configuration)
{
    // SolutionPath
    const string sourcesDirectoryArgPrefix = "--SourcesDirectory=";
    var sourcesDirectoryArg = args.FirstOrDefault(arg => arg.StartsWith(sourcesDirectoryArgPrefix, StringComparison.InvariantCulture));
    if (string.IsNullOrWhiteSpace(sourcesDirectoryArg))
    {
        throw new ArgumentException($"Missing argument '{sourcesDirectoryArgPrefix}'.");
    }

    configuration.SourcesDirectory = Path.GetFullPath(sourcesDirectoryArg[sourcesDirectoryArgPrefix.Length..]);

    // FhirVersions
    const string fhirVersionsArgPrefix = "--FhirVersions=";
    var fhirVersionsArg = args.FirstOrDefault(arg => arg.StartsWith(fhirVersionsArgPrefix, StringComparison.InvariantCulture));
    if (string.IsNullOrWhiteSpace(fhirVersionsArg))
    {
        throw new ArgumentException($"Missing argument '{fhirVersionsArg}'.");
    }

    configuration.FhirVersions = fhirVersionsArg[fhirVersionsArgPrefix.Length..].Split('|', StringSplitOptions.RemoveEmptyEntries)
        .Select(fhirVersion => fhirVersion.Trim())
        .ToList();
}

LoggingLevelSwitch getLogLevel()
{
    const string logLevelArgPrefix = "--LogLevel=";
    var logLevelArg = args.FirstOrDefault(arg => arg.StartsWith(logLevelArgPrefix, StringComparison.InvariantCulture));

    var logLevel = logLevelArg == null
        ? LogEventLevel.Information
        : Enum.Parse<LogEventLevel>(logLevelArg[logLevelArgPrefix.Length..]);

    return new LoggingLevelSwitch(logLevel);
}

bool getLogToFile()
{
    const string logToFileArgPrefix = "--LogToFile=";
    var logToFileArg = args.FirstOrDefault(arg => arg.StartsWith(logToFileArgPrefix, StringComparison.InvariantCulture));

    return logToFileArg != null && bool.Parse(logToFileArg[logToFileArgPrefix.Length..]);
}

#endregion