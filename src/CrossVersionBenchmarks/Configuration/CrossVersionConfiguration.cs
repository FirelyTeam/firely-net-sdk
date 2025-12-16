using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable

namespace Firely.Sdk.Benchmarks.Configuration;

public class CrossVersionConfigurationAttribute : Attribute, IConfigSource
{
    private record PackageVersion(string Version, string? Constant = null, bool Baseline = false);
    private readonly static string[] ALL_VERSIONS_BENCHMARK = [
        "4.2.0",
        "4.3.0",
        "5.1.0",
        "5.2.0",
        "5.3.0",
        "5.4.0",
        "5.5.0",
        "5.6.0",
        "5.7.0",
        "5.8.0",
        "5.9.0",
        "5.10.0",
        "5.11.0",
        "5.12.0",
        "6.0.0-alpha2",
        "6.0.0-alpha3-20250521.1",
        "6.0.0-beta1",
    ];

    public CrossVersionConfigurationAttribute(Type benchmarkType, bool DisplayGenColumns = false)
    {
        var attributes = benchmarkType.GetCustomAttributes(typeof(PackageVersionAttribute), false);
        var versions = attributes.OfType<PackageVersionAttribute>().DistinctBy(x=> x.PackageVersion).Select(x=> new PackageVersion(x.PackageVersion, x.Constant, x.Baseline)).ToList();
        
        if (versions.Count == 0)
            versions.AddRange(ALL_VERSIONS_BENCHMARK.Select(x => new PackageVersion(x)));
        
        Config = ManualConfig.CreateEmpty()
            .AddDiagnoser(new MemoryDiagnoser(new(DisplayGenColumns)))
            .HideColumns(BenchmarkDotNet.Columns.Column.Arguments, BenchmarkDotNet.Columns.Column.NuGetReferences)
            .AddJob([..buildJobsFromVersions(versions)]);
        
        if (benchmarkType.GetCustomAttribute(typeof(ProjectReferenceAttribute)) is not null)
            Config = Config.AddJob(Job.Default.WithId("Current Branch"));
    }

    private static IEnumerable<Job> buildJobsFromVersions(IEnumerable<PackageVersion> versions)
    {
        foreach (var major in versions.ToLookup(x => x.Version[0]))
        {
            List<Argument> args = GetArgsFor($"SDK{major.Key}");
            foreach (var version in major)
            {
                var job = Job.Default
                    .WithId(version.Version)
                    .WithArguments(version.Constant is null ? args : GetArgsFor(version.Constant))
                    // will upgrade the version defined in csproj to a specified version
                    .WithNuGet("Hl7.Fhir.R5", version.Version);

                if (version.Baseline)
                    yield return job.AsBaseline();
                else
                    yield return job;
            }
        }
    }

    static List<Argument> GetArgsFor(string constant)
    {
        return [new MsBuildArgument($"/p:DefineConstants={constant}")];
    }
    
    public IConfig Config { get; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class PackageVersionAttribute(string PackageVersion) : Attribute
{
    public string PackageVersion { get; } = PackageVersion;
    public string? Constant { get; set; } = null;
    public bool Baseline { get; set; } = false;
}
public class ProjectReferenceAttribute : Attribute;