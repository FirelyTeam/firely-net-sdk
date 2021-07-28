using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using System.Linq;
using Xunit;

namespace Hl7.Fhir.Specification.Tests
{
    [Trait("Category", "Validation")]
    public class SliceValidationTests : IClassFixture<ValidationFixture>
    {
        private readonly IResourceResolver _resolver;
        private readonly Validator _validator;

        public SliceValidationTests(ValidationFixture fixture, Xunit.Abstractions.ITestOutputHelper _)
        {
            _resolver = new CachedResolver(new SnapshotSource(fixture.Resolver));
            _validator = fixture.Validator;
        }

        private IBucket createPatientReslices()
             => createSliceDefs("http://example.com/StructureDefinition/patient-telecom-reslice-ek", "Patient.telecom");

        private IBucket createSliceDefs(string url, string path)
        {
            // We don't want to rewrite the slice/bucket factories right now
#pragma warning disable CS0618 // Type or member is obsolete
            var sd = _resolver.FindStructureDefinition(url);
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.NotNull(sd);

            var nav = new ElementDefinitionNavigator(sd);
            var success = nav.JumpToFirst(path);
            Assert.True(success);

            //var xml = FhirSerializer.SerializeResourceToXml(sd);
            //File.WriteAllText(@"c:\temp\sdout.xml", xml);

            return BucketFactory.CreateRoot(nav, _resolver, _validator);
        }

        /*
         *  telecom [1..4]/openatend/ordered
	     *      telecom:phone [1..2]
	     *      telecom:email [0..1]/closed
		 *          telecom:email/home [0..*]
		 *          telecom:email/work [0..*]
         *  	telecom:other [0..3]/open
		 *          telecom:other/home [0..1]
		 *          telecom:other/work [0..1]
	    */

        [Fact]
        public void TestMixedTypeSlicingProfile()
        {
            var sliceDefs = createSliceDefs("http://example.com/fhir/StructureDefinition/profile-communication", "Communication.payload");

            sliceDefs.Should().BeOfType<SliceGroupBucket>().Which.ChildSlices.Length.Should().Be(3);

            var sliceGroup = sliceDefs as SliceGroupBucket;

            sliceGroup.ChildSlices[0].Should().Match<DiscriminatorBucket>(s =>
                    s.Cardinality.Max == "1" &&
                    s.Name == "Communication.payload:String"
                  );

            sliceGroup.ChildSlices[1].Should().Match<DiscriminatorBucket>(s =>
                    s.Cardinality.Max == "20" &&
                    s.Name == "Communication.payload:DocumentReference"
                  );

            sliceGroup.ChildSlices[2].Should().Match<DiscriminatorBucket>(s =>
                    s.Cardinality.Max == "11" &&
                    s.Name == "Communication.payload:Task"
                  );
        }

        [Fact]
        public void TestSliceSetup()
        {
            var s = createPatientReslices();
            Assert.IsType<SliceGroupBucket>(s);
            var slice = s as SliceGroupBucket;

            Assert.Equal(ElementDefinition.SlicingRules.OpenAtEnd, slice.Rules);
            Assert.True(slice.Ordered);
            Assert.Equal("Patient.telecom", slice.Name);
            Assert.Equal(3, slice.ChildSlices.Length);
            Assert.IsType<ElementBucket>(slice.Entry);

            // Slice one - "phone" - no discriminator
            Assert.IsType<ConstraintsBucket>(slice.ChildSlices[0]);
            Assert.Equal("Patient.telecom:phone", slice.ChildSlices[0].Name);

            // Slice two - "email" - 
            Assert.IsType<SliceGroupBucket>(slice.ChildSlices[1]);
            var email = slice.ChildSlices[1] as SliceGroupBucket;
            Assert.Equal("Patient.telecom:email", email.Name);
            Assert.Equal(ElementDefinition.SlicingRules.Closed, email.Rules);
            Assert.False(email.Ordered);

            Assert.IsType<ConstraintsBucket>(email.Entry);
            Assert.Equal(2, email.ChildSlices.Length);

            Assert.IsType<DiscriminatorBucket>(email.ChildSlices[0]);
            Assert.Equal("Patient.telecom:email/home", email.ChildSlices[0].Name);

            Assert.IsType<DiscriminatorBucket>(email.ChildSlices[1]);
            Assert.Equal("Patient.telecom:email/work", email.ChildSlices[1].Name);

            Assert.IsType<SliceGroupBucket>(slice.ChildSlices[2]);
            var other = slice.ChildSlices[2] as SliceGroupBucket;
            Assert.Equal("Patient.telecom:other", other.Name);
            Assert.Equal(ElementDefinition.SlicingRules.Open, other.Rules);
        }

        [Fact]
        public void TestDiscriminatedTelecomSliceUse()
        {
            var p = new Patient();

            // Incorrect "home" use for slice "phone"
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Use = ContactPoint.ContactPointUse.Home, Value = "e.kramer@furore.com" });

            // Incorrect use of "use" for slice "other"
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Other, Use = ContactPoint.ContactPointUse.Home, Value = "http://nu.nl" });

            // Correct use of slice "other"
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Other, Value = "http://nu.nl" });

            // Correct "work" use for slice "phone", but out of order
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Use = ContactPoint.ContactPointUse.Work, Value = "ewout@di.nl" });
            DebugDumpOutputXml(p);

            var outcome = _validator.Validate(p, "http://example.com/StructureDefinition/patient-telecom-slice-ek");
            DebugDumpOutputXml(outcome);
            Assert.False(outcome.Success);
            Assert.Equal(3, outcome.Errors);
            Assert.Equal(0, outcome.Warnings);  // 11 terminology warnings, reset when terminology is working again
            var repr = outcome.ToString();
            Assert.Contains("matches slice 'Patient.telecom:phone', but this is out of order for group 'Patient.telecom'", repr);
            Assert.Contains("Value is not exactly equal to fixed value 'work'", repr);
            Assert.Contains("Instance count for 'Patient.telecom.use' is 1", repr);
        }

        [Fact]
        public void TestBucketAssignment()
        {
            var s = createPatientReslices() as SliceGroupBucket;

            var p = new Patient();
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Use = ContactPoint.ContactPointUse.Home, Value = "+31-6-39015765" });
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Email, Use = ContactPoint.ContactPointUse.Work, Value = "e.kramer@furore.com" });
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Other, Use = ContactPoint.ContactPointUse.Temp, Value = "skype://crap" });
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Other, Use = ContactPoint.ContactPointUse.Home, Value = "http://nu.nl" });
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Fax, Use = ContactPoint.ContactPointUse.Work, Value = "+31-20-6707070" });
            var pnode = new ScopedNode(p.ToTypedElement());

            var telecoms = pnode.Children("telecom").Cast<ScopedNode>();

            foreach (var telecom in telecoms)
                Assert.True(s.Add(telecom));

            var outcome = s.Validate(_validator, pnode);
            Assert.True(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            Assert.Equal("+31-6-39015765", s.ChildSlices[0].Members.Single().Children("value").Single().Value);

            var emailBucket = s.ChildSlices[1] as SliceGroupBucket;
            Assert.Equal("e.kramer@furore.com", emailBucket.Members.Single().Children("value").Single().Value);
            Assert.False(emailBucket.ChildSlices[0].Members.Any());
            Assert.Equal("e.kramer@furore.com", emailBucket.ChildSlices[1].Members.Single().Children("value").Single().Value);

            var otherBucket = s.ChildSlices[2] as SliceGroupBucket;
            Assert.Equal("http://nu.nl", otherBucket.ChildSlices[0].Members.Single().Children("value").Single().Value);
            Assert.False(otherBucket.ChildSlices[1].Members.Any());
            Assert.Equal("skype://crap", otherBucket.Members.First().Children("value").Single().Value); // in the open slice - find it on other bucket, not child

            Assert.Equal("+31-20-6707070", s.Members.Last().Children("value").Single().Value); // in the open-at-end slice
        }

        [Fact]
        public void TestTelecomReslicing()
        {
            var p = new Patient();

            // Incorrect "old" use for closed slice telecom:email
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Email, Use = ContactPoint.ContactPointUse.Home, Value = "e.kramer@furore.com" });
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Email, Use = ContactPoint.ContactPointUse.Old, Value = "ewout@di.nl" });

            // Too many for telecom:other/home
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Other, Use = ContactPoint.ContactPointUse.Home, Value = "http://nu.nl" });
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Other, Use = ContactPoint.ContactPointUse.Home, Value = "http://nos.nl" });

            // Out of order openAtEnd
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Fax, Use = ContactPoint.ContactPointUse.Work, Value = "+31-20-6707070" });

            // For the open slice in telecom:other
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Other, Use = ContactPoint.ContactPointUse.Temp, Value = "skype://crap" });  // open slice

            // Out of order (already have telecom:other)
            p.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Use = ContactPoint.ContactPointUse.Home, Value = "+31-6-39015765" });

            DebugDumpOutputXml(p);

            var outcome = _validator.Validate(p, "http://example.com/StructureDefinition/patient-telecom-reslice-ek");
            DebugDumpOutputXml(outcome);
            Assert.False(outcome.Success);
            Assert.Equal(7, outcome.Errors);
            Assert.Equal(0, outcome.Warnings);
            var repr = outcome.ToString();
            Assert.Contains("not within the specified cardinality of 1..5 (at Patient)", repr);
            Assert.Contains("which is not allowed for an open-at-end group (at Patient.telecom[5])", repr);
            Assert.Contains("a previous element already matched slice 'Patient.telecom:other' (at Patient.telecom[6])", repr);
            Assert.Contains("group at 'Patient.telecom:email' is closed. (at Patient.telecom[1])", repr);
        }

        [Fact]
        public void TestDirectTypeSliceCreation()
        {
            var s = createSliceDefs("http://example.org/fhir/StructureDefinition/obs-with-sliced-value", "Observation.value[x]") as SliceGroupBucket;

            Assert.Equal(3, s.ChildSlices.Length);
            assertTypeDiscriminator(s.ChildSlices[0], "string");
            assertTypeDiscriminator(s.ChildSlices[1], "boolean");
            assertTypeDiscriminator(s.ChildSlices[2], "Quantity", "CodeableConcept");

            void assertTypeDiscriminator(IBucket bucket, params string[] types)
            {
                Assert.True(bucket is DiscriminatorBucket);
                var db = bucket as DiscriminatorBucket;
                Assert.Single(db.Discriminators);
                Assert.True(db.Discriminators[0] is TypeDiscriminator);
                var td = db.Discriminators[0] as TypeDiscriminator;
                Assert.Equal(td.Types, types);
            }
        }


        [Fact]
        public void TestDirectTypeSliceValidation()
        {
            test("b:t", new FhirBoolean(true), true);
            test("b:f", new FhirBoolean(false), false, "not exactly equal to fixed value");  // fixed to true
            test("s:hi", new FhirString("hi!"), true);
            test("s:there", new FhirString("there"), false, "not exactly equal to fixed value");  // fixed to hi!
            test("c:10kg", new Quantity(10m, "kg"), true);
            test("c:cc", new CodeableConcept("http://nos.nl", "bla"), true);
            test("s:there", new FhirString("there"), false, "not exactly equal to fixed value");  // fixed to hi!
            test("fdt:f", FhirDateTime.Now(), false, "not one of the allowed choices");

            void test(string title, DataType v, bool success, string fragment = null)
            {
                var t = new Observation()
                {
                    Status = ObservationStatus.Final,
                    Code = new CodeableConcept("http://bla.ml", "blie"),
                    Value = v
                };

                var outcome = _validator.Validate(t, "http://example.org/fhir/StructureDefinition/obs-with-sliced-value");

                //if (!outcome.Success)
                //    output.WriteLine(outcome.ToString());

                if (success)
                    Assert.True(outcome.Success, title);
                else
                    Assert.False(outcome.Success, title);

                if (fragment != null)
                    Assert.Contains(fragment, outcome.ToString());
            }
        }

        [Fact]
        public void TestProfileSliceCreation()
        {
            _ = createSliceDefs("http://example.org/fhir/StructureDefinition/list-with-profile-slicing",
                    "List.entry") as SliceGroupBucket;
        }

        [Fact]
        public void TestValueSlicingWithPattern()
        {
            var s = createSliceDefs("http://validationtest.org/fhir/StructureDefinition/ValueDiscriminatorWithPattern",
                    "Practitioner.identifier") as SliceGroupBucket;
            Assert.NotNull(s);
            Assert.True(s is SliceGroupBucket sgb && sgb.ChildSlices.OfType<DiscriminatorBucket>().Count() == 3);
            var childSlices = s.ChildSlices.Cast<DiscriminatorBucket>().ToList();

            test("DL", "ID-TYPE-1", 0);  // should match slice1
            test("DL", "ID-TYPE-2", 1); // should match slice2
            test("DL", null, 2); // should match slice3
            test("DL", "ID-TYPE-OTHER", 2); // should match slice3
            test("XXX", "ID-TYPE-OTHER", -1); // should not match at all

            void test(string fhirCode, string localCode, int slice)
            {
                // STU3: http://hl7.org/fhir/v2/0203
                // R4: http://terminology.hl7.org/CodeSystem/v2-0203
                var data = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v2-0203", fhirCode);
                if (localCode != null)
                    data.Coding.Add(new Coding("http://local-codes.nl/identifier-types", localCode));

                var testee = new Identifier("http://nu.nl", "12345") { Type = data }.ToTypedElement();

                if (slice != -1)
                    Assert.True(childSlices[slice].Add(testee));
                else
                    Assert.True(childSlices.All(m => !m.Add(testee)));
            }
        }

        private void DebugDumpOutputXml(Base fragment)
        {
#if DUMP_OUTPUT
            // Commented out to not fill up the CI builds output log
            var doc = System.Xml.Linq.XDocument.Parse(new Serialization.FhirXmlSerializer().SerializeToString(fragment));
            output.WriteLine(doc.ToString(System.Xml.Linq.SaveOptions.None));
#endif
        }

        [Fact]
        public void SupportsExistsSlicing()
        {
            var p = new Patient();

            var iWithoutUse = new Identifier { System = "http://bla.nl" };
            p.Identifier.Add(iWithoutUse);

            var outcome = _validator.Validate(p, "http://validationtest.org/fhir/StructureDefinition/PatientExistsSlicing");
            Assert.False(outcome.Success);  // system should be 0..0 because we don't have a use

            iWithoutUse.Use = Identifier.IdentifierUse.Official;
            outcome = _validator.Validate(p, "http://validationtest.org/fhir/StructureDefinition/PatientExistsSlicing");
            Assert.True(outcome.Success);

            p.Identifier[0].System = null;
            outcome = _validator.Validate(p, "http://validationtest.org/fhir/StructureDefinition/PatientExistsSlicing");
            Assert.False(outcome.Success);  // system should be 1..1 now
        }
    }
}
