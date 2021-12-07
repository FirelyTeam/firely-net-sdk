using BenchmarkDotNet.Running;

namespace Firely.Sdk.Benchmarks
{
    public class Program
    {
        static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
