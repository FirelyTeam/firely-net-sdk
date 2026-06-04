using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace Firely.Sdk.Benchmarks.Configuration;

public class Sdk5To6Config : ManualConfig
{
    public Sdk5To6Config()
    {
        var job = Job.Default;
        AddJob(job.WithId("5.11.7").AsBaseline());
        AddJob(job.WithId("6.0.0-alpha2").WithArguments([new MsBuildArgument("/p:DefineConstants=SDK6_ALPHA2")]));
        AddJob(job.WithId("6.0.0-alpha3-20250521.1").WithArguments([new MsBuildArgument("/p:DefineConstants=SDK6_ALPHA3")]));
        HideColumns(BenchmarkDotNet.Columns.Column.Arguments);
    }
}