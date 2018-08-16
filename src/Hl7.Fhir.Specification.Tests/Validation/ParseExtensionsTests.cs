using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hl7.Fhir.Validation
{

    [Trait("Category", "Validation")]
    public class ParseExtensionsTests
    {
        [Fact]
        public void TestParseQuantity()
        {
            var i = new Model.Quantity(3.14m, "kg", "http://mysystsem.org");
            var node = i.ToTypedElement();
            var p = node.ParseQuantity();
            Assert.True(p.IsExactly(i));
        }

        [Fact]
        public void TestParseCoding()
        {
            var i = new Model.Coding("http://example.org/fhir/system1", "code1", "Code1 in System1");
            var node = i.ToTypedElement();
            var p = node.ParseCoding();
            Assert.True(p.IsExactly(i));
        }

        [Fact]
        public void TestParseCodeableConcept()
        {
            var i = new CodeableConcept
            {
                Text = "Entered text"
            };
            i.Coding.Add(
                new Model.Coding("http://example.org/fhir/system1", "code1", "Code1 in System1"));
            i.Coding.Add(
                new Model.Coding("http://example.org/fhir/system2", "code2", "Code2 in System2"));

            var node = i.ToTypedElement();
            var p = node.ParseCodeableConcept();
            Assert.True(p.IsExactly(i));
        }

        [Fact]
        public void TestParseResourceReference()
        {
            var i = new Model.ResourceReference("http://example.org/fhir/Patient/1", "a patient");
            var node = i.ToTypedElement();
            var p = node.ParseResourceReference();
            Assert.True(p.IsExactly(i));
        }

        [Fact]
        public void TestParseBindableCode()
        {
            var ic = new Code("code");
            var node = ic.ToTypedElement();
            var c = node.ParseBindable() as Code;
            Assert.NotNull(c);
            Assert.True(ic.IsExactly(c));
        }

        [Fact]
        public void TestParseBindableCoding()
        {
            var ic = new Coding("system", "code");
            var node = ic.ToTypedElement();
            var c = node.ParseBindable() as Coding;
            Assert.NotNull(c);
            Assert.True(ic.IsExactly(c));
        }

        [Fact]
        public void TestParseBindableQuantity()
        {
            var iq = new Model.Quantity(4.0m, "kg", system: null);
            var node = iq.ToTypedElement();
            var c = node.ParseBindable() as Coding;
            Assert.NotNull(c);
            Assert.Equal(iq.Code, c.Code);
            Assert.Equal("http://unitsofmeasure.org", c.System);  // auto filled out by parsebinding()
        }

        [Fact]
        public void TestParseBindableString()
        {
            var ist = new Model.FhirString("Ewout");
            var node = ist.ToTypedElement();
            var c = node.ParseBindable() as Code;
            Assert.NotNull(c);
            Assert.Equal(ist.Value, c.Value);
        }

        [Fact]
        public void TestParseBindableUri()
        {
            var iu = new Model.FhirUri("http://somewhere.org");
            var node = iu.ToTypedElement();
            var c = node.ParseBindable() as Code;
            Assert.NotNull(c);
            Assert.Equal(iu.Value, c.Value);
        }

        [Fact]
        public void TestParseBindableExtension()
        {
            var ic = new Coding("system", "code");
            var ext = new Extension { Value = ic };
            var node = ext.ToTypedElement();
            var c = node.ParseBindable() as Coding;
            Assert.NotNull(c);
            Assert.True(ic.IsExactly(c));

            ext.Value = new HumanName();
            node = ext.ToTypedElement();
            c = node.ParseBindable() as Coding;
            Assert.Null(c);  // HumanName is not bindable

            ext.Value = null;
            node = ext.ToTypedElement();
            c = node.ParseBindable() as Coding;
            Assert.Null(c);  // nothing to bind to
        }

        [Fact]
        public void TestParseUnbindable()
        { 
            // Now, something non-bindable
            var x = new HumanName().WithGiven("Ewout");
            var node = x.ToTypedElement();
            var xe = node.ParseBindable();
            Assert.Null(xe);
        }
    }
}
