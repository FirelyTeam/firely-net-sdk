using BenchmarkDotNet.Attributes;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;

#nullable enable

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
        public string? EnumGetName()
            => Enum.GetName(StringSearchParam);

        [Benchmark]
        public string EnumUtilityGetLiteral()
            => StringSearchParam.GetLiteral();

        [Benchmark]
        public string EnumUtilityGetLiteralNonGeneric()
            => StringSearchParamEnum.GetLiteral();

        [Benchmark]
        public SearchParamType EnumParse()
            => Enum.Parse<SearchParamType>("String");

        [Benchmark]
        public SearchParamType EnumParseIgnoreCase()
            => Enum.Parse<SearchParamType>("string", true);

        [Benchmark]
        public SearchParamType EnumUtilityParseLiteral()
            => EnumUtility.ParseLiteral<SearchParamType>("string")!.Value;

        [Benchmark]
        public Enum? EnumUtilityParseLiteralNonGeneric()
            => EnumUtility.ParseLiteral("string", typeof(SearchParamType));

        [Benchmark]
        public SearchParamType EnumUtilityParseLiteralIgnoreCase()
            => EnumUtility.ParseLiteral<SearchParamType>("string", true)!.Value;

        [Benchmark]
        public Enum? EnumUtilityParseLiteralIgnoreCaseNonGeneric()
            => EnumUtility.ParseLiteral("string", typeof(SearchParamType), true);

        [Benchmark]
        public string? EnumUtilityGetSystem()
            => StringSearchParam.GetSystem();

        [Benchmark]
        public string? EnumUtilityGetSystemNonGeneric()
            => StringSearchParamEnum.GetSystem();
    }
}
#nullable restore