using BenchmarkDotNet.Attributes;
using Hl7.Fhir.Model;
using System;

namespace Firely.Sdk.Benchmarks
{
    /// <summary>
    /// Reads of a <see cref="Base64Binary"/>. It keeps either the base64 literal or the bytes, and moves the data
    /// from one to the other when the other one is read.
    /// </summary>
    public class Base64BinaryBenchmarks
    {
        [Params(64, 16 * 1024)]
        public int Bytes { get; set; }

        private string _literal;
        private Base64Binary _withBytes;
        private Base64Binary _withLiteral;
        private Base64Binary _moving;
        private Base64Binary _empty;
        private byte[] _bytes;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            _bytes = new byte[Bytes];
            new Random(42).NextBytes(_bytes);
            _literal = Convert.ToBase64String(_bytes);
            _empty = new Base64Binary();

            _withBytes = new Base64Binary { JsonValue = _literal };
            _ = _withBytes.Value;

            _withLiteral = new Base64Binary { JsonValue = _literal };
            _moving = new Base64Binary { JsonValue = _literal };
        }

        /// <summary>Value, when the bytes are already there.</summary>
        [Benchmark]
        public byte[] Value_Cached() => _withBytes.Value;

        /// <summary>JsonValue, when only the literal is there: what a serializer reads after deserialization.</summary>
        [Benchmark]
        public object JsonValue_Literal() => _withLiteral.JsonValue;

        /// <summary>Value and then JsonValue: decodes the literal, and encodes the bytes again.</summary>
        [Benchmark]
        public object Value_Then_JsonValue()
        {
            _ = _moving.Value;
            return _moving.JsonValue;
        }

        /// <summary>A new instance with a literal, read once: what first use after deserialization costs.</summary>
        [Benchmark]
        public byte[] Create_And_Value() => new Base64Binary { JsonValue = _literal }.Value;

        /// <summary>A new instance with bytes, serialized once: encodes the bytes.</summary>
        [Benchmark]
        public object Create_From_Bytes_And_JsonValue() => new Base64Binary(_bytes).JsonValue;

        /// <summary>Value of an instance without a value, as for an element that only has extensions.</summary>
        [Benchmark]
        public byte[] Value_Empty() => _empty.Value;

        /// <summary>JsonValue of an instance without a value.</summary>
        [Benchmark]
        public object JsonValue_Empty() => _empty.JsonValue;
    }
}
