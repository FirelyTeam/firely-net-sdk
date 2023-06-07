using Hl7.Fhir.InterfaceApplier.CLI.Abstractions;
using Hl7.Fhir.InterfaceApplier.CLI.Configurations;
using Hl7.Fhir.InterfaceApplier.CLI.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(GetLogLevel())
    .WriteTo.Console()
    .CreateLogger();

// Set up the DI container
var services = new ServiceCollection();

services.AddOptions<ProjectManagerConfiguration>().Configure(GetProjectManagerConfiguration);

services.AddTransient<IProjectManager, ProjectManager>();
services.AddTransient<IInterfaceDiscoveryService, InterfaceDiscoveryService>();
services.AddTransient<IClassDiscoveryService, ClassDiscoveryService>();
services.AddTransient<IInterfaceApplierService, InterfaceApplierService>();
services.AddTransient<IInterfaceApplier, InterfaceApplier>();

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
serviceProvider.GetRequiredService<IInterfaceApplier>().Run();

Log.Information("*** Finished executing InterfaceApplier ***");
Log.CloseAndFlush();

#region Private Methods

void GetProjectManagerConfiguration(ProjectManagerConfiguration configuration)
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

LoggingLevelSwitch GetLogLevel()
{
    const string logLevelArgPrefix = "--LogLevel=";
    var sourcesDirectoryArg = args.FirstOrDefault(arg => arg.StartsWith(logLevelArgPrefix, StringComparison.InvariantCulture));

    var logLevel = sourcesDirectoryArg == null
        ? LogEventLevel.Information
        : Enum.Parse<LogEventLevel>(sourcesDirectoryArg[logLevelArgPrefix.Length..]);

    return new LoggingLevelSwitch(logLevel);
}

#endregion