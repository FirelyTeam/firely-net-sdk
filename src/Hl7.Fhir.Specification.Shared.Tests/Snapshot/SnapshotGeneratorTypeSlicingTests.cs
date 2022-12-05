/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// [WMR 20190828] OBSOLETE, instead use SnapshotGeneratorManifestTests
#if false

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Snapshot;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.IO;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Specification.Tests
{
    // [WMR 20190807] OBSOLETE - See SnapshotGeneratorManifestTests


    [TestClass, TestCategory("Snapshot"), Ignore]
#if PORTABLE45
	public class PortableSnapshotGeneratorTypeSlicingTest
#else
    public class SnapshotGeneratorTypeSlicingTest
#endif
    {
        static readonly FhirXmlSerializer serializer = new FhirXmlSerializer(new SerializerSettings() { Pretty = true, AppendNewLine = true });

        SnapshotGenerator _generator;
        DirectorySource _inputDirSource, _expectedDirSource;
        TimingSource _inputSource, _expectedSource;
        IResourceResolver _inputResolver, _expectedResolver;

        ElementDefinitionNavigator _DomainResourceNavigator;
        ElementDefinitionNavigator _ExtensionNavigator;

        readonly SnapshotGeneratorSettings _settings = new SnapshotGeneratorSettings()
        {
            // Throw on unresolved profile references; must include in TestData folder
            GenerateSnapshotForExternalProfiles = true,
            ForceRegenerateSnapshots = true,
            GenerateExtensionsOnConstraints = false,
            GenerateAnnotationsOnConstraints = false,
            GenerateElementIds = true // STU3
        };

        static (DirectorySource dirSource, TimingSource timingSource, IResourceResolver resolver) InitializeSource(string mask)
        {
            var dirSource = new DirectorySource("TestData/snapshot-test/Type Slicing",
                new DirectorySourceSettings
                {
                    IncludeSubDirectories = false,
                    Includes = new string[] { mask }
                }
            );

            var timingSource = new TimingSource(dirSource);
            var resolver = new CachedResolver(
                new MultiResolver(
                    new ZipSource("specification.zip"),
                    timingSource));

            return (dirSource, timingSource, resolver);
        }

        [TestInitialize]
        public void Setup()
        {
            FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

            (_inputDirSource, _inputSource, _inputResolver) = InitializeSource("*-input.xml");
            (_expectedDirSource, _expectedSource, _expectedResolver) = InitializeSource("*-expected.xml");

            _generator = new SnapshotGenerator(_inputResolver, _settings);

            _DomainResourceNavigator = ElementDefinitionNavigator.ForSnapshot(_inputResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.DomainResource));
            _ExtensionNavigator = ElementDefinitionNavigator.ForSnapshot(_inputResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Extension));
        }

        // [WMR 20190723] Unit tests for type slicing, using Grahame's example profiles
        // Source: https://github.com/hapifhir/org.hl7.fhir.core/tree/master/org.hl7.fhir.r5/src/test/resources/snapshot-generation

        StructureDefinition Load(DirectorySource dirSource, string name)
        {
            var summaries = dirSource.ListSummaries(ResourceType.StructureDefinition);
            var summary = summaries.FirstOrDefault(s => s.GetConformanceName() == name);
            if (!(summary is null))
            {
                return dirSource.LoadBySummary<StructureDefinition>(summary);
            }
            return null;
        }

        StructureDefinition LoadInput(string name) => Load(_inputDirSource, name);

        StructureDefinition LoadExpected(string name) => Load(_expectedDirSource, name);

        bool compareElementValues(string elementId, string propertyName, string expected, string actual)
        {
            var result = StringComparer.Ordinal.Equals(expected, actual);
            Debug.WriteLineIf(!result, $"MISMATCH {elementId}::{propertyName} - expected='{expected}', actual='{actual}'");
            return result;
        }

        bool compareElementValues(string elementId, string propertyName, Base expected, Base actual)
        {
            var result = expected is null ? actual is null : expected.IsExactly(actual);
            Debug.WriteLineIf(!result, $"MISMATCH {elementId}::{propertyName} - expected='{expected}', actual='{actual}'");
            return result;
        }

        bool compareElementValues<T, S>(string elementId, string propertyName, T expected, T actual)
            where T : List<S>
            where S : Base
        {
            var result = expected is null ? actual is null : expected.Count == actual.Count;

            if (result)
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    result &= compareElementValues(elementId, propertyName, expected[i], actual[i]);
                }
            }

            return result;
        }

        bool compareElements(ElementDefinition expected, ElementDefinition actual)
        {
            if (expected is null) { return actual is null; }
            else if (actual is null) { return false; }

            bool result = true;

            var elemId = expected.ElementId;

            //result &= compareElementValues<List<Code<ElementDefinition.PropertyRepresentation>>, Code<ElementDefinition.PropertyRepresentation>>(elemId, nameof(ElementDefinition.Representation), expected.RepresentationElement, actual.RepresentationElement);
            result &= compareElementValues(elemId, nameof(ElementDefinition.SliceName), expected.SliceNameElement, actual.SliceNameElement);
            result &= compareElementValues(elemId, nameof(ElementDefinition.SliceIsConstraining), expected.SliceIsConstrainingElement, actual.SliceIsConstrainingElement);
            //result &= compareElementValues(elemId, nameof(ElementDefinition.Label), expected.LabelElement, actual.LabelElement);
            //result &= compareElementValues<List<Coding>, Coding>(elemId, nameof(ElementDefinition.Code), expected.Code, actual.Code);

            // [WMR 20190723] HACK
            // .NET FHIR API - .extension elements inherit slicing component from Element.extension
            // Grahame       - .extension elements do NOT inherit slicing component

            if (!ElementDefinitionNavigator.IsExtensionPath(expected.Path))
            {
                result &= compareElementValues(elemId, nameof(ElementDefinition.Slicing), expected.Slicing, actual.Slicing);
            }

            result &= compareElementValues(elemId, nameof(ElementDefinition.Short), expected.ShortElement, actual.ShortElement);
            result &= compareElementValues(elemId, nameof(ElementDefinition.Min), expected.MinElement, actual.MinElement);
            result &= compareElementValues(elemId, nameof(ElementDefinition.Max), expected.MaxElement, actual.MaxElement);
            result &= compareElementValues(elemId, nameof(ElementDefinition.Binding), expected.Binding, actual.Binding);
            // ... compare other properties ...

            return result;
        }

        bool compareElementLists(List<ElementDefinition> expected, List<ElementDefinition> actual)
        {
            //Assert.AreEqual(expected.Count, actual.Count);
            if (expected.Count != actual.Count)
            {
                Debug.WriteLine($"MISMATCH : expected element count = {expected.Count}, actual = {actual.Count}");
                return false;
            }

            bool result = true;
            for (int i = 0; i < expected.Count; i++)
            {
                var exp = expected[i];
                var act = actual[i];
                var match = StringComparer.Ordinal.Equals(exp.Path, act.Path);
                if (match)
                {
                    result &= compareElements(exp, act);
                }
                else
                {
                    Debug.WriteLine($"MISMATCH Path: expected='{exp.Path}', actual='{act.Path}'");
                    result = false;
                }
            }
            return result;

        }

        void TestTypeSlicingExample(
            string id,
            Action<StructureDefinition> fixExpected = null,
            Action<StructureDefinition> fixInput = null)
        {
            var input = LoadInput(id);
            Assert.IsNotNull(input);
            fixInput?.Invoke(input);

            var expected = LoadExpected(id);
            Assert.IsNotNull(expected);
            Assert.IsTrue(expected.HasSnapshot);
            fixExpected?.Invoke(expected);
            var expectedElems = expected.Snapshot.Element;

            var expanded = _generator.Generate(input);
            Assert.IsNotNull(expanded);
            Assert.IsNull(_generator.Outcome);

            // TODO: Compare expanded & expected
            var result = compareElementLists(expectedElems, expanded);

            // On failure, serialize to XML & dump
            if (!result)
            {
                var clone = (StructureDefinition)input.DeepCopy();
                clone.Snapshot = new StructureDefinition.SnapshotComponent() { Element = expanded };
                var xml = serializer.SerializeToString(clone);

                var filePath = Path.Combine(Path.GetTempPath(), id + "_actual.xml");
                Debug.WriteLine($"FAILED: {id} {input.GetOrigin()}, save output to '{filePath}'");
                File.WriteAllText(filePath, xml);
                //Debug.WriteLine(xml);
            }

            Assert.IsTrue(result);
        }

        // [WMR 20190807] TODO: Add code to fix invalid constraints in input/expected

        [TestMethod] public void TestTypeSlicingExample_t1() => TestTypeSlicingExample("t1");
        [TestMethod] public void TestTypeSlicingExample_t2() => TestTypeSlicingExample("t2");
        [TestMethod] public void TestTypeSlicingExample_t3() => TestTypeSlicingExample("t3");
        [TestMethod] public void TestTypeSlicingExample_t4() => TestTypeSlicingExample("t4");
        [TestMethod] public void TestTypeSlicingExample_t4a() => TestTypeSlicingExample("t4a");
        [TestMethod] public void TestTypeSlicingExample_t5() => TestTypeSlicingExample("t5");
        [TestMethod] public void TestTypeSlicingExample_t6() => TestTypeSlicingExample("t6");
        [TestMethod] public void TestTypeSlicingExample_t7() => TestTypeSlicingExample("t7");
        [TestMethod] public void TestTypeSlicingExample_t8() => TestTypeSlicingExample("t8");
        [TestMethod] public void TestTypeSlicingExample_t9() => TestTypeSlicingExample("t9");
        [TestMethod] public void TestTypeSlicingExample_t10() => TestTypeSlicingExample("t10");

        [TestMethod] public void TestTypeSlicingExample_t11() => TestTypeSlicingExample("t11", FixExpected11_12);

        [TestMethod] public void TestTypeSlicingExample_t12() => TestTypeSlicingExample("t12", FixExpected11_12);

        [TestMethod] public void TestTypeSlicingExample_t12a() => TestTypeSlicingExample("t12a", FixExpected_12a);
        [TestMethod] public void TestTypeSlicingExample_t14() => TestTypeSlicingExample("t14");
        [TestMethod] public void TestTypeSlicingExample_t15() => TestTypeSlicingExample("t15");
        [TestMethod] public void TestTypeSlicingExample_t16() => TestTypeSlicingExample("t16");
        [TestMethod] public void TestTypeSlicingExample_t17() => TestTypeSlicingExample("t17");
        [TestMethod] public void TestTypeSlicingExample_t18() => TestTypeSlicingExample("t18");
        [TestMethod] public void TestTypeSlicingExample_t19() => TestTypeSlicingExample("t19");
        //[TestMethod] public void TestTypeSlicingExample_t20() => TestTypeSlicingExample("t20");
        [TestMethod] public void TestTypeSlicingExample_t21() => TestTypeSlicingExample("t21");
        [TestMethod] public void TestTypeSlicingExample_t22() => TestTypeSlicingExample("t22");
        [TestMethod] public void TestTypeSlicingExample_t23() => TestTypeSlicingExample("t23");
        [TestMethod] public void TestTypeSlicingExample_t23a() => TestTypeSlicingExample("t23a");
        [TestMethod] public void TestTypeSlicingExample_t24a() => TestTypeSlicingExample("t24a");
        [TestMethod] public void TestTypeSlicingExample_t24b() => TestTypeSlicingExample("t24b");
        //[TestMethod] public void TestTypeSlicingExample_t25() => TestTypeSlicingExample("t25");
        [TestMethod] public void TestTypeSlicingExample_t26() => TestTypeSlicingExample("t26");
        [TestMethod] public void TestTypeSlicingExample_t27() => TestTypeSlicingExample("t27");
        [TestMethod] public void TestTypeSlicingExample_t28() => TestTypeSlicingExample("t28");
        [TestMethod] public void TestTypeSlicingExample_t29() => TestTypeSlicingExample("t29");
        //[TestMethod] public void TestTypeSlicingExample_t30a() => TestTypeSlicingExample("t30a");
        [TestMethod] public void TestTypeSlicingExample_t30b() => TestTypeSlicingExample("t30b");
        [TestMethod] public void TestTypeSlicingExample_t31() => TestTypeSlicingExample("t31");
        [TestMethod] public void TestTypeSlicingExample_t32() => TestTypeSlicingExample("t32");
        [TestMethod] public void TestTypeSlicingExample_t33() => TestTypeSlicingExample("t33");
        [TestMethod] public void TestTypeSlicingExample_t34() => TestTypeSlicingExample("t34");
        [TestMethod] public void TestTypeSlicingExample_t35() => TestTypeSlicingExample("t35");
        [TestMethod] public void TestTypeSlicingExample_t36() => TestTypeSlicingExample("t36");
        //[TestMethod] public void TestTypeSlicingExample_t37() => TestTypeSlicingExample("t37", null, FixInput37);
        [TestMethod] public void TestTypeSlicingExample_t38() => TestTypeSlicingExample("t38");
        //[TestMethod] public void TestTypeSlicingExample_t39() => TestTypeSlicingExample("t39");
        [TestMethod] public void TestTypeSlicingExample_t40() => TestTypeSlicingExample("t40");
        [TestMethod] public void TestTypeSlicingExample_t41() => TestTypeSlicingExample("t41");
        [TestMethod] public void TestTypeSlicingExample_t42() => TestTypeSlicingExample("t42");
        [TestMethod] public void TestTypeSlicingExample_t43() => TestTypeSlicingExample("t43");
        [TestMethod] public void TestTypeSlicingExample_t44() => TestTypeSlicingExample("t44");
        [TestMethod] public void TestTypeSlicingExample_t45() => TestTypeSlicingExample("t45");
        [TestMethod] public void TestTypeSlicingExample_samply1() => TestTypeSlicingExample("samply1");
        //[TestMethod] public void TestTypeSlicingExample_au1() => TestTypeSlicingExample("au1");
        [TestMethod] public void TestTypeSlicingExample_au2() => TestTypeSlicingExample("au2");
        [TestMethod] public void TestTypeSlicingExample_au3() => TestTypeSlicingExample("au3");
        [TestMethod] public void TestTypeSlicingExample_dv1() => TestTypeSlicingExample("dv1");

        // Private helpers

        void FixExpected11_12(StructureDefinition sd)
        {
            var elems = sd.Snapshot.Element;
            var navDR = _DomainResourceNavigator;

            // FIX: Patient.extension inhers from DomainResource.extension
            var elem = FindElementByPath(elems, "Patient.extension");
            FixExtensionBase(elem, navDR);

            // FIX: Patient.extension:name1 inherits from target  extension definition "patient-birthTime"
            elem = FindElementByPath(elems, "Patient.extension", "name1");
            elem.Max = "1";

            // FIX: Patient.extension:name2 inherits from target  extension definition "patient-mothersMaidenName"
            elem = FindElementByPath(elems, "Patient.extension", "name2");
            elem.Max = "1";
        }

        void FixExpected_12a(StructureDefinition sd)
        {
            var elems = sd.Snapshot.Element;
            var navDR = _DomainResourceNavigator;

            // FIX: Patient.extension inhers from DomainResource.extension
            var elem = FindElementByPath(elems, "Patient.extension");
            FixExtensionBase(elem, navDR);

            // FIX: Patient.extension:name1 inherits from target  extension definition "patient-birthTime"
            elem = FindElementByPath(elems, "Patient.extension", "name1");
            elem.Max = "1";
        }

        [TestMethod] public void TestTypeSlicingExample13() => TestTypeSlicingExample("t13", FixExpected13);

        void FixExpected13(StructureDefinition sd)
        {
            var elems = sd.Snapshot.Element;
            var navDR = _DomainResourceNavigator;

            // FIX: Patient.extension inhers from DomainResource.extension
            var elem = FindElementByPath(elems, "Patient.extension");
            FixExtensionBase(elem, navDR);

            // FIX: Patient.extension:t inherits from target  extension definition "patient-birthTime"
            elem = FindElementByPath(elems, "Patient.extension", "t");
            elem.Max = "1";

            // FIX: Patient.extension.extension inhers from DomainResource.extension
            elem = FindElementById(elems, "Patient.extension:complex.extension:code.extension");
            FixExtensionBase(elem, navDR);
            elem = FindElementById(elems, "Patient.extension:complex.extension:period.extension");
            FixExtensionBase(elem, navDR);

            // FIX: Patient.extension.extension.extension inhers from DomainResource.extension
            elem = FindElementByPath(elems, "Patient.extension.extension.extension");
            FixExtensionBase(elem, navDR);

            // FIX: Add missing element: Patient.extension:complex.extension:code.value[x]
            Assert.IsTrue(navDR.JumpToFirst("DomainResource.extension"));
            elem = FindElementById(elems, "Patient.extension:complex.extension:code.url");
            var idx = elems.IndexOf(elem);
            elem = new ElementDefinition();
            navDR.Current.CopyTo(elem);
            elem.Path = "Patient.extension.extension.value[x]";
            elem.ElementId = "Patient.extension:complex.extension:code.value[x]";
            elems.Insert(idx + 1, elem);

            // FIX: Add missing element: Patient.extension:complex.extension:period.value[x]
            elem = FindElementById(elems, "Patient.extension:complex.extension:period.url");
            idx = elems.IndexOf(elem);
            elem = new ElementDefinition();
            navDR.Current.CopyTo(elem);
            elem.Path = "Patient.extension.extension.value[x]";
            elem.ElementId = "Patient.extension:complex.extension:period.value[x]";
            elems.Insert(idx + 1, elem);
        }

        void FixInput37(StructureDefinition sd)
        {
            var elems = sd.Differential.Element;
            var navDR = _DomainResourceNavigator;

            // FIX: Fix typo in path
            var elem = FindElementByPath(elems, "MedicationRequiest.dosageInstruction.timing.event");
            elem.Path = "MedicationRequest.dosageInstruction.timing.event";
        }

        void FixExtensionBase(ElementDefinition elem, ElementDefinitionNavigator navDR)
        {
            // FIX: extension element inhers from DomainResource.extension
            Assert.IsTrue(navDR.JumpToFirst("DomainResource.extension"));
            elem.Short = navDR.Current.Short;
            elem.Definition = navDR.Current.Definition;
        }

        static ElementDefinition FindElementById(List<ElementDefinition> elems, string id)
        {
            var elem = elems.FirstOrDefault(e => e.ElementId == id);
            Assert.IsNotNull(elem);
            return elem;
        }

        static ElementDefinition FindElementByPath(List<ElementDefinition> elems, string path)
        {
            var elem = elems.FirstOrDefault(e => e.Path == path && e.SliceName is null);
            Assert.IsNotNull(elem);
            return elem;
        }

        static ElementDefinition FindElementByPath(List<ElementDefinition> elems, string path, string sliceName)
        {
            var elem = elems.FirstOrDefault(e => e.Path == path && e.SliceName == sliceName);
            Assert.IsNotNull(elem);
            return elem;
        }


    }

}

#endif