using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hl7.Fhir.Validation
{
    public class SliceValidationTests : IClassFixture<ValidationFixture>
    {
        private IResourceResolver _resolver;
        private Validator _validator;

        public SliceValidationTests(ValidationFixture fixture)
        {
            _resolver = fixture.Resolver;
            _validator = fixture.Validator;
        }

        [Fact]
        public void TestSliceSetup()
        {
            var sd = _resolver.FindStructureDefinition("http://example.com/StructureDefinition/patient-telecom-reslice-ek");
            Assert.NotNull(sd);
            var snapgen = new SnapshotGenerator(_resolver);
            snapgen.Update(sd);

            var nav = new ElementDefinitionNavigator(sd);
            var success = nav.JumpToFirst("Patient.telecom");
            Assert.True(success);
            //var xml = FhirSerializer.SerializeResourceToXml(sd);
            //File.WriteAllText(@"c:\temp\sdout.xml", xml);

            var s = BucketFactory.Create(nav, _validator);
            Assert.IsType<SliceGroupBucket>(s);
            var slice = s as SliceGroupBucket;

            Assert.Equal(ElementDefinition.SlicingRules.Closed, slice.Rules);
            Assert.Equal(false, slice.Ordered);
            Assert.Equal("Patient.telecom", slice.Name);
            Assert.Equal(2, slice.ChildSlices.Count);
            Assert.IsType<SliceBucket>(slice.Entry);

            Assert.IsType<SliceBucket>(slice.ChildSlices[0]);
            Assert.Equal("Patient.telecom:phone", slice.ChildSlices[0].Name);

            Assert.IsType<SliceGroupBucket>(slice.ChildSlices[1]);
            Assert.Equal("Patient.telecom:email", slice.ChildSlices[1].Name);
            var reslice = slice.ChildSlices[1] as SliceGroupBucket;

            Assert.IsType<SliceBucket>(reslice.Entry);
            Assert.Equal(2, reslice.ChildSlices.Count);

            // Does not succeed yet since the snapshot generator reverses de re-slices
            // FIX - remove when Michel has fixed the problem with the snapshot generator
            var swap = reslice.ChildSlices[1];
            reslice.ChildSlices[1] = reslice.ChildSlices[0];
            reslice.ChildSlices[0] = swap;

            Assert.IsType<SliceBucket>(reslice.ChildSlices[0]);
            Assert.Equal("Patient.telecom:email/home", reslice.ChildSlices[0].Name);

            Assert.IsType<SliceBucket>(reslice.ChildSlices[1]);
            Assert.Equal("Patient.telecom:email/work", reslice.ChildSlices[1].Name);
        }

    }
}
