/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

// [WMR 20170411] HACK - suppress infinite recursion
// TODO: Properly handle recursive type declarations
// Don't throw exception but emit OperationOutcome issue(s) and continue
#define HACK_STU3_RECURSION

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
    [TestClass, TestCategory("Snapshot")]
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
        }

        // [WMR 20190723] Unit tests for type slicing, using Grahame's example profiles
        // Source: https://github.com/hapifhir/org.hl7.fhir.core/tree/master/org.hl7.fhir.r5/src/test/resources/snapshot-generation

        StructureDefinition Load(DirectorySource dirSource, IResourceResolver source, string name)
        {
            var summaries = dirSource.ListSummaries(ResourceType.StructureDefinition);
            var summary = summaries.FirstOrDefault(s => s.GetConformanceName() == name);
            if (!(summary is null))
            {
                return source.FindStructureDefinition(summary.ResourceUri);
            }
            return null;
        }

        StructureDefinition LoadInput(string name) => Load(_inputDirSource, _inputResolver, name);

        StructureDefinition LoadExpected(string name) => Load(_expectedDirSource, _expectedResolver, name);

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

        void TestTypeSlicingExample(string id)
        {
            var input = LoadInput(id);
            Assert.IsNotNull(input);

            var expected = LoadExpected(id);
            Assert.IsNotNull(expected);
            Assert.IsTrue(expected.HasSnapshot);
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

        [TestMethod]
        public void TestTypeSlicingExample1() => TestTypeSlicingExample("t1");
        [TestMethod]
        public void TestTypeSlicingExample2() => TestTypeSlicingExample("t2");
        [TestMethod]
        public void TestTypeSlicingExample3() => TestTypeSlicingExample("t3");
        [TestMethod]
        public void TestTypeSlicingExample4() => TestTypeSlicingExample("t4");
        [TestMethod]
        public void TestTypeSlicingExample5() => TestTypeSlicingExample("t5");
        [TestMethod]
        public void TestTypeSlicingExample6() => TestTypeSlicingExample("t6");
        [TestMethod]
        public void TestTypeSlicingExample7() => TestTypeSlicingExample("t7");
        [TestMethod]
        public void TestTypeSlicingExample8() => TestTypeSlicingExample("t8");
        [TestMethod]
        public void TestTypeSlicingExample9() => TestTypeSlicingExample("t9");
        [TestMethod]
        public void TestTypeSlicingExample10() => TestTypeSlicingExample("t10");
        [TestMethod]
        public void TestTypeSlicingExample11() => TestTypeSlicingExample("t11");
        [TestMethod]
        public void TestTypeSlicingExample12() => TestTypeSlicingExample("t12");
        [TestMethod]
        public void TestTypeSlicingExample12a() => TestTypeSlicingExample("t12a");
        [TestMethod]
        public void TestTypeSlicingExample13() => TestTypeSlicingExample("t13");
        [TestMethod]
        public void TestTypeSlicingExample14() => TestTypeSlicingExample("t14");
        [TestMethod]
        public void TestTypeSlicingExample15() => TestTypeSlicingExample("t15");
        [TestMethod]
        public void TestTypeSlicingExample16() => TestTypeSlicingExample("t16");
        [TestMethod]
        public void TestTypeSlicingExample17() => TestTypeSlicingExample("t17");
        [TestMethod]
        public void TestTypeSlicingExample18() => TestTypeSlicingExample("t18");
        [TestMethod]
        public void TestTypeSlicingExample19() => TestTypeSlicingExample("t19");
        [TestMethod]
        public void TestTypeSlicingExample20() => TestTypeSlicingExample("t20");
        [TestMethod]
        public void TestTypeSlicingExample21() => TestTypeSlicingExample("t21");
        [TestMethod]
        public void TestTypeSlicingExample22() => TestTypeSlicingExample("t22");
        [TestMethod]
        public void TestTypeSlicingExample23() => TestTypeSlicingExample("t23");
        [TestMethod]
        public void TestTypeSlicingExample24a() => TestTypeSlicingExample("t24a");
        [TestMethod]
        public void TestTypeSlicingExample24b() => TestTypeSlicingExample("t24b");
        //[TestMethod]
        //public void TestTypeSlicingExample25() => TestTypeSlicingExample("t25");
        [TestMethod]
        public void TestTypeSlicingExample26() => TestTypeSlicingExample("t26");
        [TestMethod]
        public void TestTypeSlicingExample27() => TestTypeSlicingExample("t27");
        [TestMethod]
        public void TestTypeSlicingExample28() => TestTypeSlicingExample("t28");
        [TestMethod]
        public void TestTypeSlicingExample29() => TestTypeSlicingExample("t29");
        //[TestMethod]
        //public void TestTypeSlicingExample30a() => TestTypeSlicingExample("t30a");
        [TestMethod]
        public void TestTypeSlicingExample30b() => TestTypeSlicingExample("t30b");
        [TestMethod]
        public void TestTypeSlicingExample31() => TestTypeSlicingExample("t31");
        [TestMethod]
        public void TestTypeSlicingExample32() => TestTypeSlicingExample("t32");
        [TestMethod]
        public void TestTypeSlicingExample33() => TestTypeSlicingExample("t33");
        [TestMethod]
        public void TestTypeSlicingExample34() => TestTypeSlicingExample("t34");
        [TestMethod]
        public void TestTypeSlicingExample35() => TestTypeSlicingExample("t35");
        [TestMethod]
        public void TestTypeSlicingExample36() => TestTypeSlicingExample("t36");
        [TestMethod]
        public void TestTypeSlicingExample37() => TestTypeSlicingExample("t37");
        [TestMethod]
        public void TestTypeSlicingExample38() => TestTypeSlicingExample("t38");
        [TestMethod]
        public void TestTypeSlicingExample39() => TestTypeSlicingExample("t39");
        [TestMethod]
        public void TestTypeSlicingExample40() => TestTypeSlicingExample("t40");
        [TestMethod]
        public void TestTypeSlicingExample41() => TestTypeSlicingExample("t41");
        [TestMethod]
        public void TestTypeSlicingExample42() => TestTypeSlicingExample("t42");
        [TestMethod]
        public void TestTypeSlicingExample43() => TestTypeSlicingExample("t43");
        [TestMethod]
        public void TestTypeSlicingExample44() => TestTypeSlicingExample("t44");


    }

}
