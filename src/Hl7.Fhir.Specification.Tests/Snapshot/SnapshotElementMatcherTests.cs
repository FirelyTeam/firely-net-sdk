/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Navigation;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Tests
{
    // Unit tests for ElementMatcher

    [TestClass]
    public class SnapshotElementMatcherTests
    {
        IResourceResolver _testResolver;

        [TestInitialize]
        public void Setup()
        {
            var dirSource = new DirectorySource("TestData/snapshot-test", new DirectorySourceSettings { IncludeSubDirectories = true } );
            _testResolver = new CachedResolver(dirSource);
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Simple()
        {
            // Match element constraints on Patient and Patient.identifier to Patient core definition
            // Both element constraints should be merged

            var baseProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Merge: Patient.identifier
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Identity()
        {
            // Match core patient profile to itself
            // All element constraints should be merged

            var baseProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForSnapshot(userProfile);

            // Recursively match all constraints and verify that all matches return action Merged
            var matches = matchAndVerify(snapNav, diffNav);
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Extension_New()
        {
            // Slice core Patient.extension, introduce a new extension
            // Extension does not need a slicing introduction in differential

            var baseProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRDefinedType.Extension,
                                    Profile = new string[] { "http://example.org/fhir/StructureDefinition/myExtension" }
                                }
                            }
                        }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav);         // Extension slice entry (no diff match)
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);  // Add new extension
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Extension_Override()
        {
            // Constrain an existing Patient extension slice

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>() { new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Extension } },
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                Discriminator = new string[] { "url" }
                            }
                        },
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRDefinedType.Extension,
                                    Profile = new string[] { "http://example.org/fhir/StructureDefinition/myExtension" }
                                }
                            }
                        }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            // Constrain existing extension definition
            userProfile.Differential.Element[2].Min = 1;

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Override existing extension slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Extension_Add()
        {
            // Add another extension to an existing extension slice

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>() { new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Extension } },
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                Discriminator = new string[] { "url" },
                                Rules = ElementDefinition.SlicingRules.Closed
                            }
                        },
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRDefinedType.Extension,
                                    Profile = new string[] { "http://example.org/fhir/StructureDefinition/myExtension" }
                                }
                            }
                        }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            // Define another extension slice in diff
            userProfile.Differential.Element.RemoveAt(1); // Remove extension slicing entry (not required)
            userProfile.Differential.Element[1].Type[0].Profile = new string[] { "http://example.org/fhir/StructureDefinition/myOtherExtension" };

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            // [WMR 20170406] extension slice entry is inherited from base w/o diff constraints => no match
            // Expecting a single match for the additional complex extension element
            Assert.AreEqual(1, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Add, snapNav, diffNav);  // Add new extension slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Extension_Insert()
        {
            // Insert another extension into an existing extension slice

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>() { new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Extension } },
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                Discriminator = new string[] { "url" }
                            }
                        },
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRDefinedType.Extension,
                                    Profile = new string[] { "http://example.org/fhir/StructureDefinition/myExtension" }
                                }
                            }
                        }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            // Insert another extension slice before the existing extension
            var ext = (ElementDefinition)userProfile.Differential.Element[2].DeepCopy();
            ext.Type[0].Profile = new string[] { "http://example.org/fhir/StructureDefinition/myOtherExtension" };
            userProfile.Differential.Element.Insert(2, ext);

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(3, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Insert new extension slice
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[2], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge existing extension slice
        }

        [TestMethod]
        public void TestElementMatcher_ComplexExtension()
        {
            var baseProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Extension);
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension"),
                        new ElementDefinition("Extension.extension") { Name = "name" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("name")  },
                        new ElementDefinition("Extension.extension") { Name = "age" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Integer }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("age") },
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Extension root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Extension.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(3, matches.Count);  // extension slice entry + 2 complex extension elements
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav);             // Extension slice entry (no diff match)
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // First extension child element "name"
            Assert.IsTrue(diffNav.MoveToNext());
            assertMatch(matches[2], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Second extension child element "age"
            Assert.IsFalse(diffNav.MoveToNext());
        }

        [TestMethod]
        public void TestElementMatcher_ComplexExtension_Add()
        {
            // Add a child extension element to an existing complex extension definition

            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension"),
                        new ElementDefinition("Extension.extension") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] {"url" } } },
                        new ElementDefinition("Extension.extension") { Name = "name" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("name")  },
                        new ElementDefinition("Extension.extension") { Name = "age" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Integer }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("age") },
                    }
                }
            };
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension"),
                        new ElementDefinition("Extension.extension") { Name = "size" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Decimal }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("size")  },
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Extension root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Extension.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);

            // [WMR 20170406] extension slice entry is inherited from base w/o diff constraints => no match
            // Only expecting a single match for the additional complex extension element "size"
            Assert.AreEqual(1, matches.Count);  // add one additional complex extension element

            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Add new extension child element "size"
            Assert.IsFalse(diffNav.MoveToNext());
        }

        // [WMR 20180604] Disabled; no longer possible due to fix for issue #611
        // Does FHIR even allow this? Relevant discussion on Zulip:
        // https://chat.fhir.org/#narrow/stream/23-conformance/subject/Can.20a.20derived.20profile.20insert.20new.20named.20slices.3F
        // Grahame Grieve:
        //   "have you seen build\tests\resources\snapshot-generation-tests.xml ?
        //   it doesn't include that, so we can say with confidence that it's not tested behaviour
        //   certainly if the slice is ordered, you cannot insert
        //   if the slicing is not ordered, I don't see what the need for inseertion is"
        // => Derived profile is NOT allowed to *insert* named slices into an existing slice group
        [TestMethod]
        [Ignore]
        public void TestElementMatcher_ComplexExtension_Insert()
        {
            // Insert a child extension element into an existing complex extension definition

            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension"),
                        new ElementDefinition("Extension.extension") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] {"url" } } },
                        new ElementDefinition("Extension.extension") { Name = "name" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("name")  },
                        new ElementDefinition("Extension.extension") { Name = "age" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Integer }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("age") },
                    }
                }
            };
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension"),
                        new ElementDefinition("Extension.extension") { Name = "size" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Decimal }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("size")  },
                        new ElementDefinition("Extension.extension") { Name = "name" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("name")  },
                        new ElementDefinition("Extension.extension") { Name = "age" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Integer }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("age") },
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Extension root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Extension.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            // [WMR 20170406] extension slice entry is inherited from base w/o diff constraints => no match
            // Expecting three matches for three additional complex extension elements
            Assert.AreEqual(3, matches.Count);  // three additional complex extension elements
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Insert new extension child element "size"
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge extension child element "name"
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[2], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge extension child element "age"
            Assert.IsFalse(diffNav.MoveToNext());
        }

        [TestMethod]
        public void TestElementMatcher_ComplexExtension_ConstrainChild()
        {
            // Profile with constraint on a child element of a referenced complex extension

            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension"),
                        new ElementDefinition("Extension.extension") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] {"url" } } },
                        new ElementDefinition("Extension.extension") { Name = "parent" },

                        new ElementDefinition("Extension.extension.extension") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] {"url" } } },
                        new ElementDefinition("Extension.extension.extension") { Name = "child" },
                        new ElementDefinition("Extension.extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Coding }
                            }
                        },
                        new ElementDefinition("Extension.extension.extension.url") { Fixed = new FhirUri("child")  },

                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("parent")  },
                    }
                }
            };
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension"),
                        new ElementDefinition("Extension.extension") { Name = "parent" },
                        new ElementDefinition("Extension.extension.extension") { Name = "child" },
                        new ElementDefinition("Extension.extension.extension.valueCoding")
                        {
                            Min = 1,
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Coding }
                            }
                        }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Extension root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Extension.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            // [WMR 20170406] extension slice entry is inherited from base w/o diff constraints => no match
            // Expecting a single match for "parent"
            Assert.AreEqual(1, matches.Count);  // three additional complex extension elements
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            Assert.IsTrue(snapNav.MoveToNext());
            Assert.AreEqual("parent", snapNav.Current.Name);
            Assert.AreEqual("parent", diffNav.Current.Name);
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge extension child element "parent"
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToNext());
            Assert.AreEqual("child", snapNav.Current.Name);
            Assert.AreEqual("child", diffNav.Current.Name);
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge extension child element "child"
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(snapNav.MoveToFirstChild());
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.AreEqual("value[x]", snapNav.PathName);
            Assert.AreEqual("valueCoding", diffNav.PathName);
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge extension child element valueCoding
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Animal_Slice()
        {
            // Slice Patient.animal (named)

            var baseProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);    // Define new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Animal_Slice_Override()
        {
            // Constrain existing slice on Patient.animal (named)

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            userProfile.Differential.Element[2].Min = 1;

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge existing new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Animal_Slice_Override_NoEntry()
        {
            // Constrain existing slice on Patient.animal (named)
            // Similar to previous, but differential does NOT provide a slice entry
            // Not strictly necessary, as it is implied by the base

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            // Remove slice entry from diff
            userProfile.Differential.Element.RemoveAt(1); 
            userProfile.Differential.Element[1].Min = 1;

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(1, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            // diff has no slice entry; no match
            Assert.IsTrue(snapNav.MoveToNext());    // Skip base slice entry
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge existing new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Animal_Slice_Add()
        {
            // Add a new (named) slice to an existing slice on Patient.animal

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            userProfile.Differential.Element[2].Name = "cat";

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);    // Add new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Animal_Slice_Insert()
        {
            // Insert a new (named) slice into an existing slice on Patient.animal

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            var slice = (ElementDefinition)userProfile.Differential.Element[2].DeepCopy();
            slice.Name = "cat";
            userProfile.Differential.Element.Insert(2, slice);

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(3, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Insert new slice
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[2], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge existing slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Animal_Reslice()
        {
            // Reslice an existing (named) slice on Patient.animal

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" },
                        new ElementDefinition("Patient.animal") { Name = "cat" }
                    }
                }
            };
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog", Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] {  "breed" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog/schnautzer" },
                        new ElementDefinition("Patient.animal") { Name = "dog/dachshund" }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Reslice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(4, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Re-slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[2], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Add new slice to reslice
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[3], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Add new slice to reslice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Animal_Nested_Slice()
        {
            // Introduce a nested (named) slice within an existing (named) slice on Patient.animal

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" },
                        new ElementDefinition("Patient.animal.breed"),
                        new ElementDefinition("Patient.animal") { Name = "cat" },
                        new ElementDefinition("Patient.animal.breed")
                    }
                }
            };
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        // Is slice entry required? We're not reslicing animal...
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" },
                        new ElementDefinition("Patient.animal.breed") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "coding.code" } } },
                        new ElementDefinition("Patient.animal.breed") { Name="schnautzer" },
                        new ElementDefinition("Patient.animal.breed") { Name="dachshund" },
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // First slice
            Assert.AreEqual("dog", diffNav.Current.Name);

            // Nested slice: Patient.animal.breed
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(3, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // New nested slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav (not sliced)
            Assert.IsFalse(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // First new nested slice
            Assert.AreEqual("schnautzer", diffNav.Current.Name);
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav (not sliced)
            assertMatch(matches[2], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Second new nested slice
            Assert.AreEqual("dachshund", diffNav.Current.Name);
            Assert.IsFalse(diffNav.MoveToNext());
            // Don't advance snapNav (not sliced)
        }

        // [WMR 20170718] New: Match constraint on existing slice entry
        [TestMethod]
        public void TestElementMatcher_ConstraintOnExistingSliceEntry()
        {
            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "TEST" },
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            Name = "bsn"
                        }
                    }
                }
            };
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        // Constraint on inherited slice entry
                        new ElementDefinition("Patient.identifier")
                        {
                            Min = 1
                        },
                        // Constraint on inherited slice
                        new ElementDefinition("Patient.identifier")
                        {
                            Name = "bsn"
                        },
                        // Introduce new slice
                        new ElementDefinition("Patient.identifier")
                        {
                            Name = "ehrid"
                        }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice entry: Patient.identifier
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(3, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Constraint on slice entry
            var snapSliceEntryBookmark = snapNav.Bookmark();

            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Constraint on first slice
            Assert.AreEqual("bsn", diffNav.Current.Name);

            // New slice: Patient.identifier:ehrid
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsFalse(snapNav.MoveToNext());
            Assert.IsTrue(snapNav.ReturnToBookmark(snapSliceEntryBookmark));
            assertMatch(matches[2], ElementMatcher.MatchAction.Add, snapNav, diffNav);    // New slice
            Assert.AreEqual("ehrid", diffNav.Current.Name);
            Assert.IsFalse(diffNav.MoveToNext());
        }

        // ========== Helper functions ==========

        // Recursively match diffNav to snapNav and verify that all matches return the specified action (def. Merged)
        static List<ElementMatcher.MatchInfo> matchAndVerify(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, ElementMatcher.MatchAction action = ElementMatcher.MatchAction.Merge)
        {
            List<ElementMatcher.MatchInfo> matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            for (int i = 0; i < matches.Count; i++)
            {
                matches[i].DumpMatch(snapNav, diffNav);

                assertMatch(matches[i], action, snapNav, diffNav);

                snapNav.ReturnToBookmark(matches[i].BaseBookmark);
                diffNav.ReturnToBookmark(matches[i].DiffBookmark);
                Assert.AreEqual(snapNav.HasChildren, diffNav.HasChildren);
                if (snapNav.HasChildren)
                {
                    matchAndVerify(snapNav, diffNav);
                    snapNav.ReturnToBookmark(matches[i].BaseBookmark);
                    diffNav.ReturnToBookmark(matches[i].DiffBookmark);
                }

                bool notLastMatch = i < matches.Count - 1;
                Assert.AreEqual(notLastMatch, snapNav.MoveToNext());
                Assert.AreEqual(notLastMatch, diffNav.MoveToNext());
            }

            return matches;
        }

        static void assertMatch(List<ElementMatcher.MatchInfo> matches, ElementMatcher.MatchAction action, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(1, matches.Count);
            assertMatch(matches[0], action, snapNav, diffNav);
        }

        static void assertMatch(ElementMatcher.MatchInfo match, ElementMatcher.MatchAction action, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav = null)
        {
            assertMatch(match, action, snapNav.Bookmark(), diffNav != null ? diffNav.Bookmark() : Bookmark.Empty);
        }

        static void assertMatch(ElementMatcher.MatchInfo match, ElementMatcher.MatchAction action, Bookmark snap, Bookmark diff)
        {
            Assert.IsNotNull(match);
            Assert.AreEqual(action, match.Action);
            Assert.AreEqual(snap, match.BaseBookmark);
            Assert.AreEqual(diff, match.DiffBookmark);
        }
    }
}
