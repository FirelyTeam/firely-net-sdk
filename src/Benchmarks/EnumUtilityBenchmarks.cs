using BenchmarkDotNet.Attributes;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;

namespace Firely.Sdk.Benchmarks
{
    [MemoryDiagnoser]
    public class EnumUtilityBenchmarks
    {
        private static readonly SearchParamType StringSearchParam = SearchParamType.String;
        private static readonly Enum StringSearchParamEnum = StringSearchParam;

        [Benchmark]
        public string EnumToString()
            => SearchParamType.String.ToString();

        [Benchmark]
        public string EnumGetName()
            => Enum.GetName(StringSearchParam);

        [Benchmark]
        public string EnumUtilityGetLiteral()
            => EnumUtility.GetLiteral(StringSearchParam);

        [Benchmark]
        public string EnumUtilityGetLiteralNonGeneric()
            => EnumUtility.GetLiteral(StringSearchParamEnum);

        [Benchmark]
        public SearchParamType EnumParse()
            => Enum.Parse<SearchParamType>("String");

        [Benchmark]
        public SearchParamType EnumParseIgnoreCase()
            => Enum.Parse<SearchParamType>("string", true);

        [Benchmark]
        public SearchParamType EnumUtilityParseLiteral()
            => EnumUtility.ParseLiteral<SearchParamType>("string").Value;

        [Benchmark]
        public SearchParamType EnumUtilityParseLiteralIgnoreCase()
            => EnumUtility.ParseLiteral<SearchParamType>("string", true).Value;

        [Benchmark]
        public Enum EnumUtilityNonGenericParseLiteral()
            => EnumUtility.ParseLiteral("string", typeof(SearchParamType));

        [Benchmark]
        public Enum EnumUtilityNonGenericParseLiteralIgnoreCase()
            => EnumUtility.ParseLiteral("string", typeof(SearchParamType), true);

        [Benchmark]
        public string? EnumUtilityGetSystem()
            => EnumUtility.GetSystem(StringSearchParam);

        [Benchmark]
        public string EnumUtilityNonGenericGetSystem()
            => EnumUtility.GetSystem(StringSearchParamEnum);
    }
}
