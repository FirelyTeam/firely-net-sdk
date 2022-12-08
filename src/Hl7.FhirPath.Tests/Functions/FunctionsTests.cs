using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HL7.FhirPath.Tests
{
    [TestClass]
    public class FunctionsTests
    {
        public static IEnumerable<object[]> ExistenceFunctionTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function empty() : Boolean
                    ("{}.empty()", true, false),
                    ("(1).empty()", false, false),
                    ("(1 | 2 | 3).empty()", false, false),

                    // function exists([criteria : expression]) : Boolean
                    ("{}.exists()", false, false),
                    ("(1).exists()", true, false),
                    ("(1 | 2 | 3).exists()", true, false),
                    ("(1 | 2 | 3).exists($this > 0)", true, false),
                    ("(1 | 2 | 3).exists($this < 0)", false, false),


                    // function all(criteria : expression) : Boolean
                    ("{}.all(true)", true, false),
                    ("{}.all(false)", true, false),
                    ("(1 | 2 | 3).all($this > 0 )", true, false),

                    // function  allTrue() : Boolean
                    ("{}.allTrue()", true, false),
                    ("true.allTrue()", true, false),
                    ("false.allTrue()", false, false),
                    ("(true | true).allTrue()", true, false),
                    ("(true | false).allTrue()", false, false),
                    ("(false | true).allTrue()", false, false),
                    ("(false | false).allTrue()", false, false),

                    // function anyTrue() : Boolean
                    ("{}.anyTrue()", false, false),
                    ("true.anyTrue()", true, false),
                    ("false.anyTrue()", false, false),
                    ("(true | true).anyTrue()", true, false),
                    ("(true | false).anyTrue()", true, false),
                    ("(false | true).anyTrue()", true, false),
                    ("(false | false).anyTrue()", false, false),

                    // function allFalse() : Boolean
                    ("{}.allFalse()", true, false),
                    ("true.allFalse()", false, false),
                    ("false.allFalse()", true, false),
                    ("(true | true).allFalse()", false, false),
                    ("(true | false).allFalse()", false, false),
                    ("(false | true).allFalse()", false, false),
                    ("(false | false).allFalse()", true, false),

                    // function anyFalse() : Boolean
                    ("{}.anyFalse()", false, false),
                    ("true.anyFalse()", false, false),
                    ("false.anyFalse()", true, false),
                    ("(true | true).anyFalse()", false, false),
                    ("(true | false).anyFalse()", true, false),
                    ("(false | true).anyFalse()", true, false),
                    ("(false | false).anyFalse()", true, false),

                    // function subsetOf(other : collection) : Boolean
                    ("(1 | 2).subsetOf((1 | 2 | 3))", true, false),
                    ("(1 | 3).subsetOf((1 | 2 | 3))", true, false),
                    ("(4 | 5).subsetOf((1 | 2 | 3))", false, false),
                    ("{}.subsetOf(1)", true, false),
                    ("{}.subsetOf({})", true, false),
                    ("(1 | 2).subsetOf({})", false, false),

                    // function supersetOf(other : collection) : Boolean
                    ("(1 | 2 | 3).supersetOf((1 | 2))", true, false),
                    ("(1 | 2 | 3).supersetOf((1 | 3))", true, false),
                    ("(1 | 2 | 3).supersetOf(4 | 5)", false, false),
                    ("1.supersetOf({})", true, false),
                    ("{}.supersetOf(1)", false, false),
                    ("{}.supersetOf({})", true, false),

                    // function count() : Integer
                    ("{}.count() = 0", true, false),
                    ("34.count() = 1", true, false),
                    ("(1 | 2 | 3).count() = 3", true, false),
                    ("(1 | 1 | 1).count() = 1", true, false),
                    ("(1.combine(1.combine(1))).count() = 3", true, false),

                    // function distinct() : collection
                    ("{}.distinct().empty()", true, false),
                    ("(2).distinct() = 2", true, false),
                    ("(1 | 2).distinct() = (1 | 2)", true, false),
                    ("(1 | 2).distinct() = (2 | 1)", false, false),
                    ("(1 | 1).distinct() = 1", true, false),
                    ("(1.combine(1.combine(1))).distinct() = 1", true, false),

                    // function isDistinct() : Boolean
                    ("{}.isDistinct()", true, false),
                    ("(2).isDistinct()", true, false),
                    ("(1 | 2).isDistinct()", true, false),
                    ("(1.combine(1)).isDistinct()", false, false)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });



        public static IEnumerable<object[]> FilteringAndProjectionFunctionTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function where(criteria : expression) : collection
                    ("{}.where(true).empty()", true, false),
                    ("{}.where(false).empty()", true, false),
                    ("(1).where(false).empty()", true, false),
                    ("(1 | 2 | 3).where($this > 1) = (2 | 3)", true, false),
                    ("(1 | 2 | 3).where(true) = (1 | 2 | 3)", true, false),

                    // function select(projection: expression) : collection
                    ("{}.select(true).empty()", true, false),
                    ("(1 | 2).select($this + 1) = (2 | 3)", true, false),
                    ("('a' | 'b').select($this + 'x') = ('ax' | 'bx')", true, false),

                    // function repeat(projection: expression) : collection

                    // function Aggregate(aggregator : expression [, init : value]) : value
                    ("(1|2|3|4|5|6|7|8|9).aggregate($this+$total, 0) = 45", true, false),
                    ("(1|2|3|4|5|6|7|8|9).aggregate($this+$total, 2) = 47", true, false),
                    ("(1|2|3|4|5|6|7|8|9).aggregate(iif($total.empty(), $this, iif($this < $total, $this, $total))) = 1", true, false),
                    ("(1|2|3|4|5|6|7|8|9).aggregate(iif($total.empty(), $this, iif($this > $total, $this, $total))) = 9", true, false),

                    // function ofType(type : type specifier) : collection 
                    ("{}.ofType(String).empty()", true, false),
                    ("('a').ofType(String).count() = 1", true, false),
                    ("('a').ofType('String').count() = 1", false, true), // should throw an exception
                    ("(1 | 2).ofType(`Integer`).count() = 2", true, false),
                    ("(1 | 2).ofType(System.`Integer`).count() = 2", true, false),
                    ("(1 | 2).ofType(System.Integer).count() = 2", true, false),
                    ("(1 | 2).ofType(Integer).count() = 2", true, false)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> SubsettingFunctionTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function  [ index : Integer ] : collection
                    ("{}[0].empty()", true, false),
                    ("(1 | 2)[0] = 1", true, false),
                    ("(1 | 2)[1] = 2", true, false),
                    ("(1 | 2)[2].empty()", true, false),
                    ("(1 | 2 | 3)[2] = 3", true, false),

                    // function single() : collection
                    ("{}.single().empty()", true, false),
                    ("(1).single() = 1", true, false),
                    ("(1 | 2).single()", true, true), // should throw an error

                    // function first() : collection
                    ("{}.first().empty()", true, false),
                    ("1.first() = 1", true, false),
                    ("(1).first() = 1", true, false),
                    ("(1 | 2).first() = 1", true, false),

                    // function last() : collection
                    ("{}.last().empty()", true, false),
                    ("1.last() = 1", true, false),
                    ("(1).last() = 1", true, false),
                    ("(1 | 2).last() = 2", true, false),

                    // function tail() : collection
                    ("{}.tail().empty()", true, false),
                    ("1.tail().empty()", true, false),
                    ("(1).tail().empty()", true, false),
                    ("(1 | 2).tail() = 2", true, false),
                    ("(1 | 2 | 3).tail() = (2 | 3)", true, false),

                    // function skip(num : Integer) : collection
                    ("{}.skip(1).empty()", true, false),
                    ("{}.skip(0).empty()", true, false),
                    ("{}.skip(-1).empty()", true, false),
                    ("1.skip(1).empty()", true, false),
                    ("(1).skip(1).empty()", true, false),
                    ("(1 | 2).skip(1) = 2", true, false),
                    ("(1 | 2 | 3).skip(2) = 3", true, false),
                    ("(1 | 2 | 3).skip(3).empty()", true, false),
                    ("(1 | 2 | 3).skip(0) = (1 | 2 | 3)", true, false),
                    ("(1 | 2 | 3).skip(-1) = (1 | 2 | 3)", true, false),

                    // function take(num : Integer) : collection
                    ("{}.take(1).empty()", true, false),
                    ("{}.take(0).empty()", true, false),
                    ("{}.take(-1).empty()", true, false),
                    ("1.take(0).empty()", true, false),
                    ("(1).take(1) = 1", true, false),
                    ("(1 | 2).take(1) = 1", true, false),
                    ("(1 | 2).take(2) = (1 | 2)", true, false),
                    ("(1 | 2).take(3) = (1 | 2)", true, false),
                    ("(1 | 2).take(2) = (1 | 2)", true, false),
                    ("(1 | 2 | 3).take(2) = (1 | 2)", true, false),

                    // function intersect(other: collection) : collection
                    ("{}.intersect(1 | 2).empty()", true, false),
                    ("{}.intersect({}).empty()", true, false),
                    ("(1 | 2).intersect({}).empty()", true, false),
                    ("(1).intersect(1 | 2) = 1", true, false),
                    ("(1 | 2).intersect(1 | 2) = (1 | 2)", true, false),
                    ("(1 | 2 | 3).intersect(1 | 2) = (1 | 2)", true, false),
                    ("(1 | 2).intersect(1 | 2 | 3) = (1 | 2)", true, false),
                    ("(1 | 2 | 3).intersect(4 | 5).empty()", true, false),
                    ("(1 | 2 | 2 | 3).intersect(1 | 1 | 5) = 1", true, false),

                    // function exclude(other: collection) : collection
                    ("{}.exclude({}).empty()", true, false),
                    ("{}.exclude(1).empty()", true, false),
                    ("(1).exclude({}) = 1", true, false),
                    ("(1).exclude(1).empty()", true, false),
                    ("(1).exclude(2) = 1", true, false),
                    ("(1 | 2 | 3).exclude(2) = (1 | 3)", true, false),
                    ("(1 | 2 | 3).exclude(2 | 4) = 1 | 3", true, false),
                    ("(1.combine(2.combine(2.combine(3)))).exclude(2) = (1 | 3)", true, false), //  Duplicate items will not be eliminated by this function
                    ("(1 | 2 | 3).exclude(2 | 3) = 1", true, false),
                    ("(1 | 2 | 3).exclude(3 | 2) = 1", true, false)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> CombiningFunctionTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function union(other : collection)
                    ("{}.union({}).empty()", true, false),
                    ("{}.union(1) = 1", true, false),
                    ("{}.union(1 | 2) = (1 | 2)", true, false),
                    ("1.union({}) = 1", true, false),
                    ("(1 | 2).union({}) = (1 | 2)", true, false),
                    ("1.union(2) = (1 | 2)", true, false),
                    ("(1 | 2).union(3 | 4) = (1 | 2 | 3 | 4)", true, false),
                    ("(1.combine(1.combine(2.combine(3)))).union(2 | 3) = (1 | 2 | 3)", true, false),

                    // function combine(other : collection) : collection
                    ("{}.combine({}).empty()", true, false),
                    ("{}.combine(1) = 1", true, false),
                    ("{}.combine(1 | 2) = (1 | 2)", true, false),
                    ("1.combine({}) = 1", true, false),
                    ("(1 | 2).combine({}) = (1 | 2)", true, false),
                    ("1.combine(2) = (1 | 2)", true, false),
                    ("(1 | 2).combine(3 | 4) = (1 | 2 | 3 | 4)", true, false),
                    ("(1.combine(1.combine(2.combine(3)))).combine(2 | 3) = (1.combine(1.combine(2.combine(3.combine(2.combine(3))))))", true, false),
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> ConversionFunctionTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function iif(criterion: expression, true-result: collection [, otherwise-result: collection]) : collection
                    ("iif({}, true, false)", false, false),
                    ("iif({}, true).empty()", true, false),
                    ("iif(1 | 2 | 3, true, false)", false, true),
                    ("iif(false, true).empty()", true, false),
                    ("iif({ }, true, false)", false, false),
                    ("iif(true, true, false)", true, false),
                    ("iif({ } | true, true, false)", true, false),
                    ("iif(true, 1) = 1", true, false),
                    ("iif(false, 1).empty()", true, false),
                    ("iif(true, true, 1/0)", true, false),
                    ("iif(1=1, true, 1/0)", true, false),
                    ("iif(false, 1/0, true)", true, false),
                    ("iif(1=2, 1/0, 'a' = 'a')", true, false)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> StringManipulationFunctionTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function indexOf(substring : String) : Integer
                    ("{}.indexOf('a').empty()", true, false),
                    ("{}.indexOf('').empty()", true, false),
                    ("'abcdefg'.indexOf('bc') = 1", true, false),
                    ("'abcdefg'.indexOf('x') = -1", true, false),
                    ("'abcdefg'.indexOf('abcdefg') = 0", true, false),
                    ("('a' | 'b').indexOf('a')", true, true),  // should throw an error

                    // function substring(start : Integer [, length : Integer]) : String
                    ("{}.substring(0).empty()", true, false),
                    ("{}.substring(0, 1).empty()", true, false),
                    ("'abc'.substring({}).empty()", true, false),
                    ("'abc'.substring(1) = 'bc'", true, false),
                    ("'abc'.substring(1, {}) = 'bc'", true, false ), // this should be the same as the previous testcase
                    ("'abcdefg'.substring(3) = 'defg'", true, false),
                    ("'abcdefg'.substring(1, 2) = 'bc'", true, false),
                    ("'abcdefg'.substring(6, 2) = 'g'", true, false),
                    ("'abcdefg'.substring(7, 1).empty()", true, false),

                    // function startsWith(prefix : String) : Boolean
                    ("{}.startsWith('a').empty()", true, false),
                    ("'abc'.startsWith('')", true, false),
                    ("'abc'.startsWith('a')", true, false),
                    ("'abc'.startsWith('ab')", true, false),
                    ("'abc'.startsWith('abc')", true, false),
                    ("'abc'.startsWith('x')", false, false),

                    // function endsWith(suffix : String) : Boolean
                    ("{}.endsWith('a').empty()", true, false),
                    ("'abc'.endsWith('')", true, false),
                    ("'abc'.endsWith('c')", true, false),
                    ("'abc'.endsWith('bc')", true, false),
                    ("'abc'.endsWith('abc')", true, false),
                    ("'abc'.endsWith('x')", false, false),

                    // function contains(substring : String) : Boolean
                    ("{}.contains('a').empty()", true, false),
                    ("'abc'.contains('')", true, false),
                    ("'abc'.contains('b')", true, false),
                    ("'abc'.contains('bc')", true, false),
                    ("'abc'.contains('d')", false, false),

                    // function upper() : String
                    ("{}.upper().empty()", true, false),
                    ("'a'.upper() = 'A'", true, false),
                    ("'abcdefg'.upper() = 'ABCDEFG'", true, false),
                    ("'AbCdefg'.upper() = 'ABCDEFG'", true, false),
                    ("'AbC2e;~fg'.upper() = 'ABC2E;~FG'", true, false),

                    // function lower() : String
                    ("{}.lower().empty()", true, false),
                    ("'A'.lower() = 'a'", true, false),
                    ("'a'.lower() = 'a'", true, false),
                    ("'ABCDEFG'.lower() = 'abcdefg'", true, false),
                    ("'AbCdefg'.lower() = 'abcdefg'", true, false),
                    ("'AbC2e;~fg'.lower() = 'abc2e;~fg'", true, false),

                    // function replace(pattern : String, substitution : String) : String
                    ("{}.replace({}, {}).empty()", true, false),
                    ("''.replace({}, {}).empty()", true, false),
                    ("'abc'.replace('b', '') = 'ac'", true, false),
                    ("'abcbdbebf'.replace('b', '') = 'acdef'", true, false),
                    ("'abc'.replace('', 'x') = 'xaxbxcx'", true, false),
                    ("'abc'.replace('', 'x').replace('x', '') = 'abc'", true, false),
                    ("'abcdefg'.replace('cde', '123') = 'ab123fg'", true, false),
                    ("'abcdefg'.replace('cde', '') = 'abfg'", true, false),

                    // function matches(regex : String) : Boolean
                    ("{}.matches('').empty()", true, false),
                    ("{}.matches({}).empty()", true, false),
                    ("'abc'.matches({}).empty()", true, false),
                    ("'123'.matches('^[1-3]+$')", true, false),
                    ("'1234'.matches('^[1-3]+$')", false, false),
                    ("''.matches('^[1-3]+$')", false, false),
                    ("'1055 RW'.matches('^[1-9][0-9]{3} ?(?!SA|SD|SS)[A-Z]{2}$')", true, false),
                    ("'1055 SS'.matches('^[1-9][0-9]{3} ?(?!SA|SD|SS)[A-Z]{2}$')", false, false),
                    ("'1055RW'.matches('^[1-9][0-9]{3} ?(?!SA|SD|SS)[A-Z]{2}$')", true, false),

                    // function replaceMatches(regex : String, substitution: String) : String
                    ("{}.replaceMatches({}, {}).empty()", true, false),
                    (@"'11/30/1972'.replaceMatches('\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b','${day}-${month}-${year}') = '30-11-1972'", true, false),

                    // function length() : Integer
                    ("{}.length().empty()", true, false),
                    ("'a'.length() = 1", true, false),
                    ("'abcd'.length() = 4", true, false),

                    // function toChars() : collection
                    ("{}.toChars().empty()", true, false),
                    ("'a'.toChars() = ('a')", true, false),
                    ("'abc'.toChars() = ('a' | 'b' | 'c')", true, false)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });


        public static IEnumerable<object[]> MathFunctionTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function abs() : Integer | Decimal | Quantity
                    ("{}.abs().empty()", true, false),
                    ("(-5).abs() = 5", true, false),
                    ("(-5.5).abs() = 5.5", true, false),
                    ("(5.5 'mg').abs() = (5.5 'mg')", true, false),
                    ("(-5.5 'mg').abs() = (5.5 'mg')", true, false),

                    // function ceiling() : Integer
                    ("{}.ceiling().empty()", true, false),
                    ("1.ceiling() = 1", true, false),
                    ("1.1.ceiling() = 2", true, false),
                    ("(-1.1).ceiling() = -1", true, false),
                    ("(1 | 2).ceiling()", true, true), // should throw an error
                    ("'a'.ceiling()", true, true), // should throw an error

                    // function exp() : Decimal
                    ("{}.exp().empty()", true, false),
                    ("0.exp() = 1.0", true, false),
                    ("(-0.0).exp() = 1.0", true, false),
                    ("(1 | 2).exp()", true, true), // should throw an error
                    ("'a'.exp()", true, true), // should throw an error

                    // function floor() : Integer
                    ("{}.floor().empty()", true, false),
                    ("1.floor() = 1", true, false),
                    ("1.1.floor() = 1", true, false),
                    ("2.9.floor() = 2", true, false),
                    ("(-2.1).floor() = -3", true, false),
                    ("(1 | 2).floor()", true, true), // should throw an error
                    ("'a'.floor()", true, true), // should throw an error

                    // function ln() : Decimal
                    ("{}.ln().empty()", true, false),
                    ("1.ln() = 0.0", true, false),
                    ("1.0.ln() = 0.0", true, false),
                    ("(1 | 2).ln()", true, true), // should throw an error
                    ("'a'.ln()", true, true ),  // should throw an error

                    // function log(base : Decimal) : Decimal
                    ("{}.log(1).empty()", true, false),
                    ("{}.log({}).empty()", true, false),
                    ("1.log({}).empty()", true, false),
                    ("16.log(2) = 4.0", true, false),
                    ("100.0.log(10.0) = 2.0", true, false),
                    ("(1 | 2).log(2)", true, true), // should throw an error
                    ("'a'.log(2)", true, true), // should throw an error

                    // function power(exponent : Integer | Decimal) : Integer | Decimal
                    ("{}.power(1).empty()", true, false),
                    ("{}.power({}).empty()", true, false),
                    ("1.power({}).empty()", true, false),
                    ("2.power(3) = 8", true, false),
                    ("2.5.power(2) = 6.25", true, false),
                    ("(-1).power(0.5).empty()", true, false),
                    ("(1 | 2).power(2)", true, true), // should throw an eror
                    ("'a'.power(2)", true, true), // should throw an error

                    // function round([precision : Integer]) : Decimal
                    ("{}.round().empty()", true, false),
                    ("1.round() = 1", true, false),
                    ("1.5.round(0) = 2", true, false),
                    ("1.5.round() = 2", true, false),
                    ("3.14159.round(3) = 3.142", true, false),
                    ("1.0.round(-1)", true, true), // should throw an error
                    ("(1 | 2).round(2)", true, true), // should throw an error
                    ("'a'.round(2)", true, true), // should throw an error

                    // function sqrt() : Decimal
                    ("{}.sqrt().empty()", true, false),
                    ("(-1).sqrt().empty()", true, false),
                    ("81.sqrt() = 9.0", true, false),
                    ("9.0.sqrt() = 3.0", true, false),
                    ("(1 | 2).sqrt()", true, true), // should throw an error
                    ("'a'.sqrt()", true, true), // should throw an error

                    // function truncate() : Integer
                    ("{}.truncate().empty()", true, false),
                    ("101.truncate() = 101", true, false),
                    ("1.00000001.truncate() = 1", true, false),
                    ("(-1.56).truncate() = -1", true, false),
                    ("(1 | 2).truncate()", true, true), // should throw an error
                    ("'a'.truncate()", true, true) // should throw an error
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> UtilityFunctionTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function trace(name : String [, projection: Expression]) : collection
                    ("{}.trace('test').empty()", true, false),
                    ("(1 | 2).trace('test') = (1 | 2)", true, false),

                    // Current date and time functions
                    ("now().empty() = false", true, false),
                    ("timeOfDay().empty() = false", true, false),
                    ("today().empty() = false", true, false)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> BooleanOperatorTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    // function not(): Boolean
                    ("{}.not().empty() = true", true, false),
                    ("false.not() = true", true, false),
                    ("true.not() = false", true, false),
                    ("(0).toBoolean().not() = true", true, false),
                    ("(1).toBoolean().not() = false", true, false),
                    ("('true').toBoolean().not() = false", true, false),
                    ("('false').toBoolean().not() = true", true, false),
                    ("(1|2).not() = false", true, true)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> AllFunctionTestcases()
        {
            return
                Enumerable.Empty<object[]>()
                .Union(ExistenceFunctionTestcases())
                .Union(FilteringAndProjectionFunctionTestcases())
                .Union(SubsettingFunctionTestcases())
                .Union(CombiningFunctionTestcases())
                .Union(ConversionFunctionTestcases())
                .Union(StringManipulationFunctionTestcases())
                .Union(MathFunctionTestcases())
                .Union(UtilityFunctionTestcases())
                .Union(BooleanOperatorTestcases())
                ;
        }

        [DataTestMethod]
        [DynamicData(nameof(AllFunctionTestcases), DynamicDataSourceType.Method)]
        public void AssertTestcases(string expression, bool expected, bool invalid = false)
        {
            ITypedElement dummy = ElementNode.ForPrimitive(true);

            if (invalid)
            {
                Action act = () => dummy.IsBoolean(expression, expected);
                act.Should().Throw<Exception>();
            }
            else
            {
                dummy.IsBoolean(expression, expected).Should().BeTrue();
            }
        }

        [TestMethod]
        public void TraceTest()
        {
            ITypedElement dummy = ElementNode.ForPrimitive(true);
            var ctx = EvaluationContext.CreateDefault();
            ctx.Tracer = tracer;
            dummy.IsBoolean("(1 | 2).trace('test').empty()", true, ctx);

            static void tracer(string name, IEnumerable<ITypedElement> list)
            {
                name.Should().Be("test");
                list.Should().HaveCount(2);
            }
        }


        /// <summary>
        /// Check that scalar expressions are evaluated only once.
        /// </summary>
        [TestMethod]
        public void SingleScalarTest()
        {
            var iterations = 0;
            var symbols = new SymbolTable();

            symbols.Add("once", (object _) =>
            {
                iterations++;

                return ElementNode.CreateList(iterations);
            });

            var expression = new FhirPathCompiler(symbols).Compile("once()");
            var result = expression.Scalar(null, new EvaluationContext());

            Assert.AreEqual(result, 1);
        }

        /// <summary>
        /// Tests issue 1652 https://github.com/FirelyTeam/firely-net-sdk/issues/1652
        /// </summary>
        [TestMethod]
        public void ContextNestingLevelTest()
        {
            Coding c = new("http://nu.nl", "nl");
            var te = c.ToTypedElement(ModelInspector.Common);
            Assert.IsTrue(te.IsBoolean($"system.endsWith(code)", true));
            Assert.IsTrue(te.IsBoolean($"system.endsWith(%context.code)", true));
            Assert.IsTrue(te.IsBoolean($"system.endsWith('nl')", true));
            Assert.IsTrue(te.IsBoolean($"system.endsWith(code.toString())", true));
            Assert.IsTrue(te.IsBoolean($"system.endsWith('banana')", false));
        }

        /// <summary>
        /// Tests issue https://github.com/FirelyTeam/firely-net-sdk/issues/2018
        /// </summary>
        [TestMethod]
        public void TestFhirPathRepeatsStringLiteral()
        {
            var nav = ElementNode.ForPrimitive("a");

            var result = nav.Select("repeat('teststring')");
            result.Should().ContainSingle(ite => ((string)ite.Value) == "teststring");
        }
    }
}
