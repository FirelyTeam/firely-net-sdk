using BenchmarkDotNet.Attributes;
using Hl7.Fhir.Model;
using System;

namespace Firely.Sdk.Benchmarks
{
    public class DateTimeBenchmarks
    {
        [GlobalSetup]
        public void BenchmarkSetup()
        {
            _dateTimeInstance = new FhirDateTime(DATETIME);
            _ = _dateTimeInstance.TryToSystemDateTime(out var _); // trigger initial compile of regex

            _dateInstance = new Date(DATE);
            _ = _dateInstance.TryToSystemDate(out var _); // trigger initial compile of regex
        }

        private const string DATETIME = "2023-07-11T13:00:00";
        private FhirDateTime _dateTimeInstance;

        [Benchmark]
        public DateTimeOffset DateTimeToDTO_Uncached()
        {
            // Clear the cache each invocation
            _dateTimeInstance.Value = DATETIME;
            _ = _dateTimeInstance.TryToDateTimeOffset(TimeSpan.Zero, out var result);
            _dateTimeInstance.Value = null;

            return result;
        }

        [Benchmark]
        public DateTimeOffset DateTimeToDTO_Cached()
        {
            _ = _dateTimeInstance.TryToDateTimeOffset(TimeSpan.Zero, out var result);
            return result;
        }

        private const string DATE = "2023-07-11";
        private Date _dateInstance;


        [Benchmark]
        public DateTimeOffset DateToDTO_Uncached()
        {
            // Clear the cache each invocation
            _dateInstance.Value = DATETIME;
            _ = _dateInstance.TryToDateTimeOffset(out var result);
            _dateInstance.Value = null;

            return result;
        }

        [Benchmark]
        public DateTimeOffset DateToDTO_Cached()
        {
            _ = _dateInstance.TryToDateTimeOffset(out var result);
            return result;
        }
    }
}