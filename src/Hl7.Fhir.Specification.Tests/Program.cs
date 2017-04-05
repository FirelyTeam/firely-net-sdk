using System;

using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}