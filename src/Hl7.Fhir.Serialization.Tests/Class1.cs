using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Hl7.Fhir.Serialization.Tests
{


    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MyTestClass
    {
        private static ReadOnlySpan<byte> Utf8Bom => new byte[] { 0xEF, 0xBB, 0xBF };



        [TestMethod]
        public void DeserializeWithStreams()
        {
            //using var stream = new FileStream(@"C:\Users\Marco\Downloads\profiles-resources.json", FileMode.Open);

            using var stream = new FileStream(@"C:\Users\Marco\Downloads\small_patient.json", FileMode.Open, FileAccess.Read);
            var buffer = new byte[1_024];
            var span = buffer.AsSpan();
            // Read past the UTF-8 BOM bytes if a BOM exists.
            if (span.StartsWith(Utf8Bom))
            {
                span = span.Slice(Utf8Bom.Length);
            }

            // read the first block
            int bytesRead = stream.Read(buffer);

            Utf8JsonReader reader = new(buffer.AsSpan(0, bytesRead), bytesRead == 0, default);

            FhirJsonPocoDeserializerSettings settings = new() { OnRead = Foo };
            FhirJsonPocoDeserializer parser = new(typeof(Bundle).Assembly, settings);
            var resource = parser.DeserializeResource(ref reader);

            resource.Should().NotBeNull();

            void Foo(ref Utf8JsonReader reader)
            {
                string str = Encoding.UTF8.GetString(buffer);
                var contentLength = buffer.Length;
                if (reader.BytesConsumed < buffer.Length)
                {
                    ReadOnlySpan<byte> leftover = buffer.AsSpan((int)reader.BytesConsumed);
                    str = Encoding.UTF8.GetString(leftover);
                    if (leftover.Length == buffer.Length)
                    {
                        Array.Resize(ref buffer, buffer.Length * 2);

                    }

                    leftover.CopyTo(buffer);
                    bytesRead = stream.Read(buffer.AsSpan(leftover.Length));
                    contentLength = bytesRead == 0 ? 0 : bytesRead + leftover.Length;
                }
                else
                {
                    bytesRead = stream.Read(buffer);
                    contentLength = bytesRead == 0 ? 0 : bytesRead;
                }
                var span = contentLength < buffer.Length ? new ReadOnlySpan<byte>(buffer, 0, contentLength) : buffer.AsSpan();
                str = Encoding.UTF8.GetString(span);

                Debug.WriteLine(str);
                Debug.WriteLine("-------------------");
                reader = new(span, bytesRead == 0, reader.CurrentState);
            }

        }

        [TestMethod]
        public void DeserializeWithStreams2()
        {
            using var stream = new FileStream(@"C:\Users\Marco\Downloads\small_patient.json", FileMode.Open, FileAccess.Read);
            FhirJsonPocoDeserializer parser = new(typeof(Bundle).Assembly);
            var resource = parser.DeserializeResource(stream, 32000);
            resource.Should().NotBeNull();
        }


    }
}
