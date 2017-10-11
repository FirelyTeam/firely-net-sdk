/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using System;
using System.IO;
using Xunit;

namespace Hl7.Fhir.Support.Tests.Serialization
{
    public class NavigatorStreamXmlTests
    {
        [Fact]
        public void ScanThroughBundle()
        {
            var xmlBundle = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\profiles-types.xml");
            using (var stream = new XmlNavigatorStream(xmlBundle))
            {

                Assert.True(stream.IsBundle);
                Assert.Null(stream.Current);

                Assert.True(stream.MoveNext());
                Assert.True(stream.MoveNext());
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/dateTime", stream.Position);

                var nav = stream.Current;
                Assert.True(nav.MoveToFirstChild("name"));
                Assert.Equal("dateTime", nav.Value);

                var current = stream.Position;

                Assert.True(stream.MoveNext());
                Assert.True(stream.MoveNext());
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/string", stream.Position);

                stream.Seek(current);
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/dateTime", stream.Position);

                stream.Reset();
                Assert.True(stream.MoveNext());
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/date", stream.Position);

                while (stream.MoveNext()) ;     // read to end

                Assert.Equal("http://hl7.org/fhir/StructureDefinition/Age", stream.Position);

                // Reading past end does not change position
                Assert.False(stream.MoveNext());
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/Age", stream.Position);
            }
        }

        [Fact]
        public void ScanThroughSingle()
        {
            var xmlPatient = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\fp-test-patient.xml");
            using (var stream = new XmlNavigatorStream(xmlPatient))
            {
                Assert.False(stream.IsBundle);
                Assert.Equal("Patient", stream.ResourceType);
                Assert.Null(stream.Current);

                Assert.True(stream.MoveNext());
                Assert.Equal("http://example.org/Patient/pat1", stream.Position);
                var current = stream.Position;

                var nav = stream.Current;
                Assert.True(nav.MoveToFirstChild("gender"));
                Assert.Equal("male", nav.Value);

                stream.Reset();
                stream.Seek(current);
                Assert.Equal("http://example.org/Patient/pat1", stream.Position);

                Assert.False(stream.MoveNext());
                Assert.Equal("http://example.org/Patient/pat1", stream.Position);
            }
        }

        [Fact]
        public void ReadCrap()
        {
            // Try a random other xml file
            var xmlfile = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\source-test\books.xml");

            using (var stream = new XmlNavigatorStream(xmlfile))
            {
                Assert.Null(stream.ResourceType);
                Assert.False(stream.MoveNext());
            }
        }

    }
}
