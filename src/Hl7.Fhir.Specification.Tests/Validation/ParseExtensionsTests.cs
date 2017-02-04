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
            var i = new Model.Quantity(3.14m, "kg");
            var nav = new PocoNavigator(i);
            var p = nav.ParseQuantity();
            Assert.True(p.IsExactly(i));
        }

        [Fact]
        public void TestParseCoding()
        {
            var i = new Model.Coding("http://example.org/fhir/system1", "code1", "Code1 in System1");
            var nav = new PocoNavigator(i);
            var p = nav.ParseCoding();
            Assert.True(p.IsExactly(i));
        }

        [Fact]
        public void TestParseCodeableConcept()
        {
            var i = new CodeableConcept();
            i.Text = "Entered text";
            i.Coding.Add(
                new Model.Coding("http://example.org/fhir/system1", "code1", "Code1 in System1"));
            i.Coding.Add(
                new Model.Coding("http://example.org/fhir/system2", "code2", "Code2 in System2"));

            var nav = new PocoNavigator(i);
            var p = nav.ParseCodeableConcept();
            Assert.True(p.IsExactly(i));
        }


        [Fact]
        public void TestParseBindable()
        {
            var ic = new Code("code");
            var nav = new PocoNavigator(ic);
            var c = nav.ParseBindable(FHIRAllTypes.Code) as Coding;
            Assert.NotNull(c);
            Assert.Equal(ic.Value, c.Code);
            Assert.Null(c.System);

            var iq = new Model.Quantity(4.0m, "kg");
            nav = new PocoNavigator(iq);
            c = nav.ParseBindable(FHIRAllTypes.Quantity) as Coding;
            Assert.NotNull(c);
            Assert.Equal(iq.Code, c.Code);
            Assert.Equal(iq.System, c.System);

            var ist = new Model.FhirString("Ewout");
            nav = new PocoNavigator(ist);
            c = nav.ParseBindable(FHIRAllTypes.String) as Coding;
            Assert.NotNull(c);
            Assert.Equal(ist.Value, c.Code);
            Assert.Null(c.System);

            var iu = new Model.FhirUri("http://somewhere.org");
            nav = new PocoNavigator(iu);
            c = nav.ParseBindable(FHIRAllTypes.Uri) as Coding;
            Assert.NotNull(c);
            Assert.Equal(iu.Value, c.Code);
            Assert.Null(c.System);

            // 'code','Coding','CodeableConcept','Quantity','Extension', 'string', 'uri'
        }
    }
}
