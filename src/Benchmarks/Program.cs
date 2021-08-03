using BenchmarkDotNet.Running;

namespace Firely.Sdk.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<ModelInspectorBenchmarks>();
        }
    }
}
