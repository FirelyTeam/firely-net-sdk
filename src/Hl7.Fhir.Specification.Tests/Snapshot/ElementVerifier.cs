using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    // Helper class to verify results
    class ElementVerifier
    {
        IList<ElementDefinition> _elements;
        ElementDefinition _current;
        SnapshotGeneratorSettings _settings;
        int _pos;

        public ElementVerifier(StructureDefinition sd, SnapshotGeneratorSettings settings)
        {
            Assert.IsNotNull(sd);
            Assert.IsTrue(sd.HasSnapshot);
            _settings = settings;
            _elements = sd.Snapshot.Element;
            _pos = 0;
            // var ann = sd.Annotation<OriginAnnotation>();
            // Debug.Print($"Assert structure: url = '{sd.Url}' - origin = '{ann.Origin}'");
            Debug.Print($"Assert structure: url = '{sd.Url}' - origin = '{sd.GetOrigin()}'");
        }

        public ElementVerifier(IList<ElementDefinition> elements, SnapshotGeneratorSettings settings)
        {
            _settings = settings;
            _elements = elements;
            _settings = settings;
            _pos = 0;
        }

        public ElementDefinition CurrentElement => _current;

        // Find first element with matching path
        // Continue at the final element position from the last call to this method (or 0)
        // Search matching element in forward direction
        // Optionally verify specified name / id / fixed value
        public void VerifyElement(string path, string name = null, string elementId = null, Element fixedValue = null)
        {
            var elements = _elements;
            while (_pos < _elements.Count)
            {
                var element = _current = elements[_pos++];
                if (element.Path == path)
                {
                    if (name != null)
                    {
                        Assert.AreEqual(name, element.SliceName, $"Invalid element name. Expected = '{name}', actual = '{element.SliceName}'.");
                    }
                    if (_settings.GenerateElementIds && elementId != null)
                    {
                        Assert.AreEqual(elementId, element.ElementId, $"Invalid elementId. Expected = '{elementId}', actual = '{element.ElementId}'.");
                    }
                    if (fixedValue != null)
                    {
                        Assert.IsTrue(element.Fixed != null && fixedValue.IsExactly(element.Fixed), $"Invalid fixed value. Expected = '{fixedValue}', actual = '{element.Fixed}'.");
                    }
                    return;
                }
            }
            Assert.Fail($"No matching element found for path '{path}'");
        }

        [DebuggerStepThrough]
        public void AssertSlicing(string discriminatorPath, ElementDefinition.SlicingRules? rules, bool? ordered)
        {
            AssertSlicing(ElementDefinition.DiscriminatorType.Value, discriminatorPath, rules, ordered);
        }

        [DebuggerStepThrough]
        public void AssertSlicing(ElementDefinition.DiscriminatorType discriminatorType, string discriminatorPath, ElementDefinition.SlicingRules? rules, bool? ordered)
        {
            AssertSlicing(new ElementDefinition.DiscriminatorComponent() { Path = discriminatorPath, Type = discriminatorType }, rules, ordered);
        }

        [DebuggerStepThrough]
        public void AssertSlicing(ElementDefinition.DiscriminatorComponent discriminator, ElementDefinition.SlicingRules? rules, bool? ordered)
        {
            AssertSlicing(new ElementDefinition.DiscriminatorComponent[] { discriminator }, rules, ordered);
        }

        public void AssertSlicing(IEnumerable<ElementDefinition.DiscriminatorComponent> discriminator, ElementDefinition.SlicingRules? rules, bool? ordered)
        {
            var slicing = CurrentElement.Slicing;
            Assert.IsNotNull(slicing);

            // Assert.Fail("TODO: Fix the below assertion");
            Assert.IsTrue(
                discriminator.SequenceEqual(slicing.Discriminator, DiscriminatorComparer.Default),
                $"Invalid discriminator for element with path '{CurrentElement.Path}' - Expected: '{string.Join(" | ", discriminator.Select(d => d.Type + " : " + d.Path))}' Actual: '{string.Join(" | ", slicing.Discriminator.Select(d => d.Type + " : " + d.Path))}' ");

            Assert.AreEqual(slicing.Rules, rules);
            Assert.AreEqual(slicing.Ordered, ordered);
        }
    }

    class DiscriminatorComparer : IEqualityComparer<ElementDefinition.DiscriminatorComponent>
    {
        public static readonly DiscriminatorComparer Default = new DiscriminatorComparer();

        public bool Equals(ElementDefinition.DiscriminatorComponent x, ElementDefinition.DiscriminatorComponent y)
        {
            if (x == null) { return y == null; }
            if (y == null) { return x == null; }
            return x.IsExactly(y);
        }

        public int GetHashCode(ElementDefinition.DiscriminatorComponent obj)
        {
            return 0; // Delegate to Equals
        }
    }
}
