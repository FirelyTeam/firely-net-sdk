using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Firely.Sdk.Benchmarks.Configuration;

public class CrossVersionConfigurationAttribute : Attribute, IConfigSource
{
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
    
    public CrossVersionConfigurationAttribute(string? Baseline = null, bool DisplayGenColumns = false, bool AddProjectReference = false, params string[] Versions)
    {
        if (Versions.Length == 0)
            
            Versions = ALL_VERSIONS_BENCHMARK;
        Config = ManualConfig.CreateEmpty()
            .AddDiagnoser(new MemoryDiagnoser(new(DisplayGenColumns)))
            .HideColumns(BenchmarkDotNet.Columns.Column.Arguments, BenchmarkDotNet.Columns.Column.NuGetReferences)
            .AddJob([..buildJobsFromVersions(Baseline, AddProjectReference, Versions.Distinct())]);
    }

    private static IEnumerable<Job> buildJobsFromVersions(string? baseline, bool addProjectReference, IEnumerable<string> versions)
    {
        foreach (var major in versions.ToLookup(x => x[0]))
        {
            List<Argument> args = [new MsBuildArgument($"/p:DefineConstants=SDK{major.Key}")];
            foreach (var version in major)
            {
                var job = Job.Default
                    .WithId(version)
                    .WithArguments(args)
                    // will upgrade the version defined in csproj to a specified version
                    .WithNuGet("Hl7.Fhir.R4", version);
                
                if (version == baseline)
                    yield return job.AsBaseline();
                else
                    yield return job;
            }
        }
        
        // if(addProjectReference)
        //     yield return Job.Default.WithId("Current Branch")
        //         .WithArguments([new MsBuildArgument($"/p:DefineConstants=PROJECT")]);
    }

    public IConfig Config { get; }
}