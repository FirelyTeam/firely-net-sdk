using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.FhirPath.Sprache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.FhirPath.Tests
{
    // Copied from Sprache.Test
    static class AssertParser
    {
        public static void SucceedsWithOne<T>(Parser<IEnumerable<T>> parser, string input, T expectedResult)
        {
            SucceedsWith(parser, input, t =>
            {
                Assert.AreEqual(1,t.Count());
                Assert.AreEqual(expectedResult, t.Single());
            });
        }

        public static void SucceedsWithMany<T>(Parser<IEnumerable<T>> parser, string input, IEnumerable<T> expectedResult)
        {
            SucceedsWith(parser, input, t => Assert.IsTrue(t.SequenceEqual(expectedResult)));
        }

        public static void SucceedsWithAll(Parser<IEnumerable<char>> parser, string input)
        {
            SucceedsWithMany(parser, input, input.ToCharArray());
        }

        public static void SucceedsWith<T>(Parser<T> parser, string input, Action<T> resultAssertion)
        {
            parser.TryParse(input)
                .IfFailure(f =>
                {
                    throw new Exception($"Parsing of \"{input}\" failed unexpectedly. {f}");
                })
                .IfSuccess(s =>
                {
                    resultAssertion(s.Value);
                    return s;
                });
        }

        public static void Fails<T>(Parser<T> parser, string input)
        {
            FailsWith(parser, input, f => { });
        }

        public static void FailsAt<T>(Parser<T> parser, string input, int position)
        {
            FailsWith(parser, input, f => Assert.AreEqual(position, f.Remainder.Position));
        }

        public static void FailsWith<T>(Parser<T> parser, string input, Action<IResult<T>> resultAssertion)
        {
            parser.TryParse(input)
                .IfSuccess(s =>
                {
                    // todo: Assert.Fail does not exist in XUnit
                    // Assert.Fail("Expected failure but succeeded with {0}.", s.Value);
                    Test.Fail($"Expected failure but succeeded with {s.Value}");
                    return s;
                })
                .IfFailure(f =>
                {
                    resultAssertion(f);
                    return f;
                });
        }

        // WMR Added

        public static void SucceedsMatch<T>(Parser<T> parser, string input)
        {
            SucceedsWith<T>(parser, input, result => Assert.AreEqual(input, (IEnumerable<char>)result));
        }

        public static void SucceedsMatch<T>(Parser<T> parser, string input, T match)
        {
            SucceedsWith<T>(parser, input, result => Assert.AreEqual(match, result));
        }

        public static void FailsMatch<T>(Parser<T> parser, string input)
        {
            FailsWith<T>(parser, input, result => { });
        }

        public static void FailsMatch<T>(Parser<T> parser, string input, T match)
        {
            SucceedsWith<T>(parser, input, result => Assert.AreNotEqual(match, result));
        }
    }

    // Internal methods copied from Sprache
    static class ResultHelper
    {
        public static IResult<U> IfSuccess<T, U>(this IResult<T> result, Func<IResult<T>, IResult<U>> next)
        {
            if (result == null) throw new ArgumentNullException("result");

            if (result.WasSuccessful)
                return next(result);

            return Result.Failure<U>(result.Remainder, result.Message, result.Expectations);
        }

        public static IResult<T> IfFailure<T>(this IResult<T> result, Func<IResult<T>, IResult<T>> next)
        {
            if (result == null) throw new ArgumentNullException("result");

            return result.WasSuccessful
                ? result
                : next(result);
        }
    }
}
