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
            var candidates = ConstraintSet.FromStructureDefinition(result);

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
            Assert.AreEqual(FHIRDefinedType.Period, effRes.First().CandidateTypes.Single().Code);

            var valueRes = res.WithChild("value");
            Assert.AreEqual(1, valueRes.Count);
            Assert.AreEqual(3, valueRes.First().Source.Current.Type.Count);
            Assert.AreEqual(3, valueRes.First().CandidateTypes.Count());
        }


        [TestMethod]
        public void TestDirectMultiChoiceNavigation()
        {
            StructureDefinition result = createTestSD();        // On Observation
            var pat = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);

            var candidates = ConstraintSet.FromStructureDefinition(result);
            candidates += ConstraintSet.FromStructureDefinition(pat);

            Assert.AreEqual(2, candidates.Count);

            var res = candidates.WithType(FHIRDefinedType.Patient);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(1, res.CandidateTypes.Length);

            res = candidates.WithType(FHIRDefinedType.Encounter);
            Assert.AreEqual(0, res.Count);

            res = candidates.WithType(FHIRDefinedType.Observation);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(1, res.CandidateTypes.Length);

            var resStatus = res.WithChild("status");
            resStatus = resStatus.WithType(FHIRDefinedType.Code);
            Assert.AreEqual(1, resStatus.Count);
            Assert.AreEqual(1, resStatus.CandidateTypes.Length);

            resStatus = resStatus.WithType(FHIRDefinedType.String);
            Assert.AreEqual(0, resStatus.Count);

            var resValue = res.WithChild("value");
            Assert.AreEqual(1, resValue.Count);
            Assert.AreEqual(3, resValue.CandidateTypes.Length);

            var resQValue = resValue.WithType(FHIRDefinedType.Quantity);
            Assert.AreEqual(1, resQValue.Count);
            Assert.AreEqual(2, resQValue.CandidateTypes.Length);

            var resSValue = resValue.WithType(FHIRDefinedType.String);
            Assert.AreEqual(1, resSValue.Count);
            Assert.AreEqual(1, resSValue.CandidateTypes.Length);

            resValue = resValue.WithType(FHIRDefinedType.Quantity, "http://example.org/StructureDefinition/WeightQuantity");
            Assert.AreEqual(1, resValue.Count);
            Assert.AreEqual(1, resValue.CandidateTypes.Length);

            resValue = resValue.WithType(FHIRDefinedType.Quantity, "http://example.org/StructureDefinition/XXX");
            Assert.AreEqual(0, resValue.Count);
        }

        [TestMethod]
        public void TestNavigateAcrossDefinitions()
        {
            var cs = ConstraintSet.FromStructureDefinition(createTestSD());
            var startCs = cs.WithChild("effective").WithChild("start", _source);

            Assert.AreEqual(1, startCs.Count);
            Assert.AreEqual("Period.start", startCs[0].Source.Path);

            //This won't work until we have a SnapshotGeneratorSource to ensure
            //our WithChild(,_source) will be able to create navigators 
            //(which require snapshots)
            //var valueCs = cs.WithChild("value");
            //Assert.AreEqual(1, valueCs.Count);
            //Assert.AreEqual(3, valueCs.CandidateTypeRefs().Count());

            //var systemCs = valueCs.WithChild("system", _source);
            //Assert.AreEqual(2, systemCs.Count);
            //Assert.IsTrue(systemCs.CandidateTypeRefs().All(tr => tr.Code == FHIRDefinedType.Quantity));

            var valueCs = cs.WithChild("component").WithChild("value");
            Assert.AreEqual(1, valueCs.Count);
            Assert.AreEqual(10, valueCs.CandidateTypes.Length);

            var systemCs = valueCs.WithChild("system", _source);
            Assert.AreEqual(1, systemCs.Count);
            Assert.AreEqual("Quantity.system",systemCs[0].Source.Path);
        }

        [TestMethod]
        public void TestSliceNavigation()
        {
            var cs = ConstraintSet.FromStructureDefinition(createSlicedSD());
            var telecom = cs.WithChild("telecom", includeSlices:true);
            Assert.AreEqual(8, telecom.Count);

            var system2 = telecom.WithChild("system", _source);   // all slices -except for the intro slice- slice system
            Assert.AreEqual(8, system2.Count);

            var periodSystem = telecom.WithChild("period").WithChild("start", _source);
            Assert.AreEqual(1, periodSystem.Count);    // but start is not sliced or constraint in the profile
            Assert.AreEqual("Period.start", periodSystem[0].Source.Path);
        }


        private StructureDefinition createSlicedSD()
        {
            var sd = _source.FindStructureDefinition("http://example.com/StructureDefinition/patient-telecom-reslice-ek");
            Assert.IsNotNull(sd);

            if(!(sd?.Snapshot?.Element?.Count >= 1))
                _testSG.Update(sd);

            return sd;
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
                .OfType(FHIRDefinedType.Quantity, "http://example.org/StructureDefinition/WeightQuantity")
                .OrType(FHIRDefinedType.Quantity, "http://example.org/StructureDefinition/HeightQuantity")
                .OrType(FHIRDefinedType.String));

            _testSG.Update(result);
            return result;
        }
    }
}
