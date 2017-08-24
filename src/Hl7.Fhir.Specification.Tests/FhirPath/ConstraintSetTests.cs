using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.FhirPath;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests.FhirPath
{
    [TestClass]
    public class TypeCandidateTests
    {
        IResourceResolver _source;
        SnapshotGenerator _testSG;

        [TestInitialize]
        public void Setup()
        {
            var zSource = new ZipSource("specification.zip");
            var fSource = new DirectorySource(@"TestData\validation");
            _source = new CachedResolver(new MultiResolver(fSource,zSource));
            _testSG = new SnapshotGenerator(_source);
        }

        [TestMethod]
        public void TestDirectNavigation()
        {
            StructureDefinition result = createTestSD();
            var candidates = new ConstraintSet(result);

            var res = candidates.WithType(FHIRDefinedType.Observation);
            Assert.AreEqual(1, res.Count);

            var statusRes = res.WithChild("status");
            Assert.AreEqual(1, statusRes.Count);
            Assert.AreEqual("Observation.status", statusRes.First().Source.Current.Path);

            var refRes = res.WithChild("referenceRange");
            Assert.AreEqual(1, refRes.Count);
            Assert.AreEqual("Observation.referenceRange", refRes.First().Source.Current.Path);
            refRes = refRes.WithChild("age");
            Assert.AreEqual(1, refRes.Count);
            Assert.AreEqual("Observation.referenceRange.age", refRes.First().Source.Current.Path);

            var effRes = res.WithChild("effective");
            Assert.AreEqual(1, effRes.Count);
            Assert.AreEqual(FHIRDefinedType.Period, effRes.First().Source.Current.Type.Single().Code);
            Assert.AreEqual(FHIRDefinedType.Period, effRes.First().Types.Single().Code);

            var valueRes = res.WithChild("value");
            Assert.AreEqual(1, valueRes.Count);
            Assert.AreEqual(3, valueRes.First().Source.Current.Type.Count);
            Assert.AreEqual(3, valueRes.First().Types.Count());
        }


        [TestMethod]
        public void TestDirectMultiChoiceNavigation()
        {
            StructureDefinition result = createTestSD();
            var pat = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);

            var candidates = new ConstraintSet(result);
            candidates.AddConstraint(pat);

            Assert.AreEqual(2, candidates.Count);

            var res = candidates.WithType(FHIRDefinedType.Patient);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(1, res.CandidateTypes().Count());

            res = candidates.WithType(FHIRDefinedType.Encounter);
            Assert.AreEqual(0, res.Count);

            res = candidates.WithType(FHIRDefinedType.Observation);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(1, res.CandidateTypes().Count());

            var resStatus = res.WithChild("status");
            resStatus = resStatus.WithType(FHIRDefinedType.Code);
            Assert.AreEqual(1, resStatus.Count);
            Assert.AreEqual(1, resStatus.CandidateTypes().Count());

            resStatus = resStatus.WithType(FHIRDefinedType.String);
            Assert.AreEqual(0, resStatus.Count);

            var resValue = res.WithChild("value");
            Assert.AreEqual(1, resValue.Count);
            Assert.AreEqual(2, resValue.CandidateTypes().Count());
            Assert.AreEqual(3, resValue.CandidateTypeRefs().Count());

            var resQValue = resValue.WithType(FHIRDefinedType.Quantity);
            Assert.AreEqual(1, resQValue.Count);
            Assert.AreEqual(1, resQValue.CandidateTypes().Count());
            Assert.AreEqual(2, resQValue.CandidateTypeRefs().Count());

            var resSValue = resValue.WithType(FHIRDefinedType.String);
            Assert.AreEqual(1, resSValue.Count);
            Assert.AreEqual(1, resSValue.CandidateTypes().Count());
            Assert.AreEqual(1, resSValue.CandidateTypeRefs().Count());

            resValue = resValue.WithType(FHIRDefinedType.Quantity, "http://validationtest.org/fhir/StructureDefinition/WeightQuantity");
            Assert.AreEqual(1, resValue.Count);
            Assert.AreEqual(1, resValue.CandidateTypes().Count());
            Assert.AreEqual(1, resValue.CandidateTypeRefs().Count());

            resValue = resValue.WithType(FHIRDefinedType.Quantity, "http://validationtest.org/fhir/StructureDefinition/XXX");
            Assert.AreEqual(0, resValue.Count);
        }

        [TestMethod]
        public void TestSliceNavigation()
        {
            var sd = _source.FindStructureDefinition("http://example.com/StructureDefinition/patient-telecom-reslice-ek");
            Assert.IsNotNull(sd);
            _testSG.Update(sd);

        }


        private StructureDefinition createTestSD()
        {
            var result = TestProfileArtifactSource.CreateTestSD("http://validationtest.org/fhir/StructureDefinition/SubtlyConstrainedObservation", "SubtlyConstrainedObservation",
                    "Observation with some of the choice elements constrained to test navigation", FHIRDefinedType.Observation);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Observation").OfType(FHIRDefinedType.Observation));
            cons.Add(new ElementDefinition("Observation.effectivePeriod")
                .OfType(FHIRDefinedType.Period));

            cons.Add(new ElementDefinition("Observation.value[x]")
                .OfType(FHIRDefinedType.Quantity, "http://validationtest.org/fhir/StructureDefinition/WeightQuantity")
                .OrType(FHIRDefinedType.Quantity, "http://validationtest.org/fhir/StructureDefinition/HeightQuantity")
                .OrType(FHIRDefinedType.String));

            _testSG.Update(result);
            return result;
        }
    }
}
