/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Navigation;
using System.Collections.Generic;
using System.Diagnostics;
using static Hl7.Fhir.Model.ElementDefinition.DiscriminatorComponent;
using Hl7.Fhir.Utility;
using System.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    // Unit tests for ElementMatcher

    [TestClass, TestCategory("Snapshot")]
    public class SnapshotElementMatcherTests
    {
        IAsyncResourceResolver _testResolver;

        [TestInitialize]
        public void Setup()
        {
            var dirSource = new DirectorySource("TestData/snapshot-test", new DirectorySourceSettings { IncludeSubDirectories = true } );
            _testResolver = new CachedResolver(dirSource);
        }

        static List<ElementMatcher.MatchInfo> Match(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            Debug.WriteLine($"Match base '{snapNav.Path}' to diff '{diffNav.Path}':");
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            return matches;
        }

        [TestMethod]
        public async T.Task TestElementMatcher_Patient_Simple()
        {
            // Match element constraints on Patient and Patient.identifier to Patient core definition
            // Both element constraints should be merged

            var baseProfile = await _testResolver.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient);
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
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Merge: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);
        }

        [TestMethod]
        public async T.Task TestElementMatcher_Patient_Identity()
        {
            // Match core patient profile to itself
            // All element constraints should be merged

            var baseProfile = await _testResolver.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient);
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForSnapshot(userProfile);

            // Recursively match all constraints and verify that all matches return action Merged
            _ = matchAndVerify(snapNav, diffNav);
        }

        [TestMethod]
        public async T.Task TestElementMatcher_Patient_Extension_New()
        {
            // Slice core Patient.extension, introduce a new extension
            // Extension does not need a slicing introduction in differential

            var baseProfile = await _testResolver.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient);
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
                                    Code = FHIRAllTypes.Extension.GetLiteral(),
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
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
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
                            Type = new List<ElementDefinition.TypeRefComponent>() { new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Extension.GetLiteral() } },
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                Discriminator = ForValueSlice("url").ToList()
                            }
                        },
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Extension.GetLiteral(),
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
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
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
                            Type = new List<ElementDefinition.TypeRefComponent>() { new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Extension.GetLiteral() } },
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                Discriminator = ForValueSlice("url").ToList(),
                                Rules = ElementDefinition.SlicingRules.Closed
                            }
                        },
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Extension.GetLiteral(),
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
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
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
                            Type = new List<ElementDefinition.TypeRefComponent>() { new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Extension.GetLiteral() } },
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                Discriminator = ForValueSlice("url").ToList()
                            }
                        },
                        new ElementDefinition("Patient.extension")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Extension.GetLiteral(),
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
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
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
        public async T.Task TestElementMatcher_ComplexExtension()
        {
            var baseProfile = await _testResolver.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Extension);
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension"),
                        new ElementDefinition("Extension.extension") {
                            SliceName = "name",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.String.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("name")  },
                        new ElementDefinition("Extension.extension") {
                            SliceName = "age",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Integer.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("age") },
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Extension root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Extension.extension
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            // DSTU2
            // [WMR 20170411] In DSTU2, The core Extension profile does NOT define a slicing component on "Extension.extension"
            // Current profile slices the extension element, so the matcher returns a virtual match (w/o diff element)
            // Assert.AreEqual(3, matches.Count);  // extension slice entry + 2 complex extension elements

            // STU3
            // [WMR 20170411] In STU3, The core Extension profile defines url slicing component on "Extension.extension"
            // Diff does not further constrain the inherited slice entry, so no match
            Assert.AreEqual(2, matches.Count);  // 2 complex extension elements

            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            // assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav);          // Extension slice entry (no diff match)
            assertMatch(matches[0], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // First extension child element "name"
            Assert.IsTrue(diffNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Second extension child element "age"
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
                        new ElementDefinition("Extension.extension")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() {
                                Discriminator = ForValueSlice("url").ToList()
                            }
                        },
                        new ElementDefinition("Extension.extension") {
                            SliceName = "name",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.String.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("name")  },
                        new ElementDefinition("Extension.extension") {
                            SliceName = "age",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Integer.GetLiteral() }
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
                        new ElementDefinition("Extension.extension")
                        {
                            SliceName = "size",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Decimal.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("size")  },
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Extension root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Extension.extension
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            // [WMR 20170406] extension slice entry is inherited from base w/o diff constraints => no match
            // Only expecting a single match for the additional complex extension element "size"
            Assert.AreEqual(1, matches.Count);  // add one additional complex extension element

            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Add new extension child element "size"
            Assert.IsFalse(diffNav.MoveToNext());
        }

#if false
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
                        new ElementDefinition("Extension.extension") {
                            Slicing = new ElementDefinition.SlicingComponent() {
                                Discriminator = ForValueSlice("url").ToList() 
                            } 
                        },
                        new ElementDefinition("Extension.extension") { SliceName = "name" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.String.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("name")  },
                        new ElementDefinition("Extension.extension") { SliceName = "age" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Integer.GetLiteral() }
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
                        new ElementDefinition("Extension.extension") { SliceName = "size" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Decimal.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("size")  },
                        new ElementDefinition("Extension.extension") { SliceName = "name" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.String.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("name")  },
                        new ElementDefinition("Extension.extension") { SliceName = "age" },
                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Integer.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.url") { Fixed = new FhirUri("age") },
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Extension root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Extension.extension
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

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
#endif

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
                        new ElementDefinition("Extension.extension")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() {
                                Discriminator = ForValueSlice("url").ToList()
                            }
                        },
                        new ElementDefinition("Extension.extension") {
                            SliceName = "parent",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Extension.extension.extension")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() {
                                Discriminator = ForValueSlice("url").ToList()
                            }
                        },
                        new ElementDefinition("Extension.extension.extension") {
                            SliceName = "child",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Extension.extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Coding.GetLiteral() }
                            }
                        },
                        new ElementDefinition("Extension.extension.extension.url") { Fixed = new FhirUri("child")  },

                        new ElementDefinition("Extension.extension.value[x]")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.String.GetLiteral() }
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
                        new ElementDefinition("Extension.extension") {
                            SliceName = "parent",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = true
                        },
                        new ElementDefinition("Extension.extension.extension") {
                            SliceName = "child",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = true
                        },
                        new ElementDefinition("Extension.extension.extension.valueCoding")
                        {
                            Min = 1,
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Coding.GetLiteral() }
                            }
                        }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Extension root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Extension.extension
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            // [WMR 20170406] extension slice entry is inherited from base w/o diff constraints => no match
            // Expecting a single match for "parent"
            Assert.AreEqual(1, matches.Count);  // three additional complex extension elements
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            Assert.IsTrue(snapNav.MoveToNext());
            Assert.AreEqual("parent", snapNav.Current.SliceName);
            Assert.AreEqual("parent", diffNav.Current.SliceName);
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge extension child element "parent"
            matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToNext());
            Assert.AreEqual("child", snapNav.Current.SliceName);
            Assert.AreEqual("child", diffNav.Current.SliceName);
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge extension child element "child"
            matches = Match(snapNav, diffNav);
            Assert.IsTrue(snapNav.MoveToFirstChild());
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.AreEqual("value[x]", snapNav.PathName);
            Assert.AreEqual("valueCoding", diffNav.PathName);

            // STU3: renamed element implies constraint to a single type (0...1)
            //assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge extension child element valueCoding
            // R4: renamed element only applies to specified type (0...*)
            // Add as separate constraint; do not merge with base [x] element definition
            assertMatch(matches[0], ElementMatcher.MatchAction.Add, snapNav, diffNav);
        }

        [TestMethod]
        public async T.Task TestElementMatcher_Patient_Identifier_Slice()
        {
            // Slice Patient.identifier (named)

            var baseProfile = await _testResolver.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient);
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            SliceName = "ssn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);    // Define new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Identifier_Slice_Override()
        {
            // Constrain existing slice on Patient.identifier (named)

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "ssn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            userProfile.Differential.Element[2].Min = 1;
            // [WMR 20181211] R4: also specify SliceIsConstraining flag
            //userProfile.Differential.Element[2].SliceIsConstraining = true;

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge existing new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Identifier_Slice_Override_NoEntry()
        {
            // Constrain existing slice on Patient.identifier (named)
            // Similar to previous, but differential does NOT provide a slice entry
            // Not strictly necessary, as it is implied by the base

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "ssn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            // Remove slice entry from diff
            userProfile.Differential.Element.RemoveAt(1);
            userProfile.Differential.Element[1].Min = 1;
            // [WMR 20181211] R4: also specify SliceIsConstraining flag
            //userProfile.Differential.Element[1].SliceIsConstraining = true;

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            Assert.AreEqual(1, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            // diff has no slice entry; no match
            Assert.IsTrue(snapNav.MoveToNext());    // Skip base slice entry
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge existing new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Identifier_Slice_Add()
        {
            // Add a new (named) slice to an existing slice on Patient.identifier

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "ssn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        }
                    }
                }
            };
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            userProfile.Differential.Element[2].SliceName = "his";
            // [WMR 20181211] R4: Cloned SliceIsConstraining flag value = false

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);    // Add new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Identifier_Slice_Insert()
        {
            // Insert a new (named) slice into an existing slice on Patient.identifier

            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "ssn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        }
                    }
                }
            };

            var userProfile = (StructureDefinition)baseProfile.DeepCopy();
            // [WMR 20181211] R4: update "ssn" slice in derived profile, flag as a constraining slice
            var ssnSlice = userProfile.Differential.Element[2];
            Assert.AreEqual("ssn", ssnSlice.SliceName);
            //ssnSlice.SliceIsConstraining = true;
            // [WMR 20181211] R4: add "his" slice to derived profile and flag as a new named slice
            // Note: insert new slice *before* existing slice
            // Actually illegal according to FHIR, but our implementation can handle it
            var newSlice = (ElementDefinition)ssnSlice.DeepCopy();
            newSlice.SliceName = "his";
            //newSlice.SliceIsConstraining = false;
            userProfile.Differential.Element.Insert(2, newSlice);

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

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
        public void TestElementMatcher_Patient_Identifier_Reslice()
        {
            // Reslice an existing (named) slice on Patient.animal

            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "his",
                            Short = "HIS"
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "ssn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
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
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "his",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = true,
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "his/acme",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "his/firely",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            SliceName = "ssn"
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = true
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            SliceName = "new"
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Reslice: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            Assert.AreEqual(6, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            var snapSliceBaseBookmark = snapNav.Bookmark();
            var sliceBase = CreateSliceBase(snapNav.Current); // slice intro

            ElementDefinition CreateSliceBase(ElementDefinition elem)
            {
                var result = (ElementDefinition)elem.DeepCopy();
                result.Min = 0;
                result.Slicing = null;
                return result;
            }


            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            var hisSliceBase = CreateSliceBase(snapNav.Current); // named slice "his" in base profile
            assertMatch(matches[1], ElementMatcher.MatchAction.Slice, snapNav, diffNav, hisSliceBase);    // Re-slice entry

            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[2], ElementMatcher.MatchAction.Add, snapNav, diffNav, hisSliceBase);      // Add new slice to reslice

            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[3], ElementMatcher.MatchAction.Add, snapNav, diffNav, hisSliceBase);      // Add new slice to reslice

            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            var ssnSliceBase = CreateSliceBase(snapNav.Current); // named slice "ssn" in base profile
            assertMatch(matches[4], ElementMatcher.MatchAction.Merge, snapNav, diffNav, ssnSliceBase);    // Second slice

            // [WMR 20190813] NEW - TODO
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.ReturnToBookmark(snapSliceBaseBookmark));
            assertMatch(matches[5], ElementMatcher.MatchAction.Add, snapNav, diffNav, sliceBase);      // Add new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Identifier_Nested_Slice()
        {
            // Introduce a nested (named) slice within an existing (named) slice on Patient.identifier

            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "ssn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Patient.identifier.use"),
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "his",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Patient.identifier.use")
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
                        // Is slice entry required? We're not reslicing identifier...
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier") {
                            SliceName = "ssn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = true
                        },
                        new ElementDefinition("Patient.identifier.use")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" }
                        },
                        new ElementDefinition("Patient.identifier.use") {
                            SliceName ="official",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                        new ElementDefinition("Patient.identifier.use") {
                            SliceName ="secondary",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        },
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // First slice
            Assert.AreEqual("ssn", diffNav.Current.SliceName);

            // Nested slice: Patient.identifier.use
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            Assert.AreEqual(3, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // New nested slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav (not sliced)
            Assert.IsFalse(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // First new nested slice
            Assert.AreEqual("official", diffNav.Current.SliceName);
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav (not sliced)
            assertMatch(matches[2], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Second new nested slice
            Assert.AreEqual("secondary", diffNav.Current.SliceName);
            Assert.IsFalse(diffNav.MoveToNext());
            // Don't advance snapNav (not sliced)
        }

        // [WMR 20170718] New: Match constraint on existing slice entry
        [TestMethod]
        public void TestElementMatcher_ConstraintOnExistingSliceEntry()
        {
            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" },
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            SliceName = "bsn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
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
                            SliceName = "bsn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = true,
                        },
                        // Introduce new slice
                        new ElementDefinition("Patient.identifier")
                        {
                            SliceName = "ehrid",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
                        }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice entry: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            Assert.AreEqual(3, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Constraint on slice entry
            var snapSliceEntryBookmark = snapNav.Bookmark();

            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Constraint on first slice
            Assert.AreEqual("bsn", diffNav.Current.SliceName);

            // New slice: Patient.identifier:ehrid
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsFalse(snapNav.MoveToNext());
            Assert.IsTrue(snapNav.ReturnToBookmark(snapSliceEntryBookmark));
            assertMatch(matches[2], ElementMatcher.MatchAction.Add, snapNav, diffNav);    // New slice
            Assert.AreEqual("ehrid", diffNav.Current.SliceName);
            Assert.IsFalse(diffNav.MoveToNext());
        }

        // [WMR 20170927] New: match layered constraints on choice types

        [TestMethod]
        public void TestElementMatcher_ChoiceType1()
        {
            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.value[x]")
                    }
                }
            };

            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.value[x]")
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Observation root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Merge: Observation.value[x]
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            // Verify: B:value[x] <-- merge --> D:value[x]
            Assert.AreEqual(1, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);
        }

        [TestMethod]
        public void TestElementMatcher_ChoiceType2()
        {
            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.valueString")
                    }
                }
            };

            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.valueString")
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Observation root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Merge: Observation.valueString
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            // Verify: B:valueString <-- merge --> D:valueString
            Assert.AreEqual(1, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);
        }

        [TestMethod]
        public void TestElementMatcher_ChoiceType3()
        {
            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.value[x]")
                    }
                }
            };

            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.valueString")
                    }
                }
            };

            // 2. Verify: match value[x] in derived profile to valueString in base profile

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Observation root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Merge: Observation.value[x]
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            // Verify: B:value[x] <-- merge --> D:valueString
            Assert.AreEqual(1, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());

            // STU3: renamed element implies constraint to a single type (0...1)
            //assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);
            // R4: renamed element only applies to specified type (0...*)
            // Add as separate constraint; do not merge with base [x] element definition
            assertMatch(matches[0], ElementMatcher.MatchAction.Add, snapNav, diffNav);
        }

        // [WMR 20190211] For STU3
        [TestMethod, Ignore]
        public void TestElementMatcher_ChoiceType4()
        {
            // Base profile renames value[x] to valueString
            // Derived profile refers to value[x] - WRONG! SHALL refer to valueString
            // Expected behavior: add new constraint for value[x] next to existing valueString constraint
            // This actually creates a profile that is invalid in STU3, but the validator will handle that
            // STU3: only rename choice type elements if constrained to a single type

            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.valueString")
                    }
                }
            };

            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        // STU3: INVALID!
                        // If the inherited element is already renamed, then derived profile MUST use new name
                        new ElementDefinition("Observation.value[x]")
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Observation root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Add: Observation.value[x]
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            // Verify: B:Observation <-- new --> D:value[x]
            Assert.AreEqual(1, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            var match = matches[0];
            assertMatch(match, ElementMatcher.MatchAction.New, snapNav, diffNav);
            // Verify: matcher should emit a warning
            Assert.IsNotNull(match.Issue);
            Debug.Print(match.Issue.Details?.Text);
            Assert.IsTrue(int.TryParse(match.Issue.Details?.Coding?[0]?.Code, out int code));
            Assert.AreEqual(SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_CHOICETYPE_NAME.Code, code);
        }

        // [WMR 20190211] For R4
        [TestMethod]
        public void TestElementMatcher_ChoiceType5()
        {
            // Base profile renames value[x] to valueString
            // Derived profile refers to value[x]
            // R4: allow combinations of constraints on both value[x] and on renamed type slices

            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.value[x]"),
                        new ElementDefinition("Observation.valueString")
                    }
                }
            };

            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation"),
                        new ElementDefinition("Observation.value[x]"),      // Match to base:value[x]
                        new ElementDefinition("Observation.valueString"),   // Match to base:valueString
                        new ElementDefinition("Observation.valueBoolean")   // Match to base:value[x]
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Observation root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Merge: Observation.value[x]
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            Assert.AreEqual(3, matches.Count);

            // Verify: B:Observation.value[x] <-- merge --> D:value[x]
            Assert.IsTrue(snapNav.MoveToFirstChild());
            Assert.IsTrue(diffNav.MoveToFirstChild());
            var match = matches[0];
            assertMatch(match, ElementMatcher.MatchAction.Merge, snapNav, diffNav);
            Assert.IsNull(match.Issue); // Valid in R4
            var bmSnapValueX = snapNav.Bookmark();

            // Verify: B:Observation.valueString <-- merge --> D:valueString
            match = matches[1];
            Assert.IsTrue(snapNav.MoveToNext());
            Assert.IsTrue(diffNav.MoveToNext());
            assertMatch(match, ElementMatcher.MatchAction.Merge, snapNav, diffNav);
            Assert.IsNull(match.Issue); // Valid in R4

            // Verify: B:Observation.value[x] <-- merge --> D:valueBoolean
            match = matches[2];
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.ReturnToBookmark(bmSnapValueX));
            assertMatch(match, ElementMatcher.MatchAction.Add, snapNav, diffNav);
            Assert.IsNull(match.Issue); // Valid in R4

        }

        // [WMR 20181211] R4: NEW
        // Test handling of invalid ElementDefinition.SliceIsConstraining flag values
        // If SliceIsConstraining flag is explicitly specified (not null),
        // then verify slice name in derived profile against slice names in base profile
        // Otherwise, if SliceIsConstraining flag is unspecified,
        // then fall back to original (STU3) matching behavior:
        // - found matching slice in base profile => implies derived profile constrains existing slice
        // - no matching slice exists in base profile => implies derived profile introduces a new slice
        [TestMethod]
        public void TestElementMatcher_SliceIsConstraining()
        {
            var baseProfile = new StructureDefinition()
            {
                Snapshot = new StructureDefinition.SnapshotComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent() { Description = "DUMMY" },
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            SliceName = "bsn",
                            // [WMR 20181211] R4: NEW
                            //SliceIsConstraining = false
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
                            SliceName = "bsn",
                            // [WMR 20181211] R4: NEW
                            // INVALID - conflicts with existing "bsn" slice in base profile
                            SliceIsConstraining = false,
                        },
                        // Introduce new slice
                        new ElementDefinition("Patient.identifier")
                        {
                            SliceName = "his",
                            // [WMR 20181211] R4: NEW
                            // INVALID - no matching "his" slice in base profile
                            SliceIsConstraining = true
                        }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Observation root
            var matches = Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice entry: Patient.identifier
            matches = Match(snapNav, diffNav);
            Assert.IsNotNull(matches);

            Assert.AreEqual(3, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Constraint on slice entry
            var snapSliceEntryBookmark = snapNav.Bookmark();

            // New slice: Patient.identifier:ssn - INVALID (conflicting slice in base profile)
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            var match = matches[1];
            assertMatch(match, ElementMatcher.MatchAction.Invalid, snapNav, diffNav);  
            Assert.AreEqual("bsn", diffNav.Current.SliceName);
            Assert.AreEqual("bsn", snapNav.Current.SliceName);
            // Verify generated outcome issue
            Assert.IsNotNull(match.Issue);
            Debug.Print(match.Issue.Details?.Text);
            Assert.IsTrue(int.TryParse(match.Issue.Details?.Coding?[0]?.Code, out int code));
            Assert.AreEqual(SnapshotGenerator.PROFILE_ELEMENTDEF_SLICENAME_CONFLICT.Code, code);

            // Slice constraint: Patient.identifier:his - INVALID (no matching slice in base profile)
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsFalse(snapNav.MoveToNext());
            Assert.IsTrue(snapNav.ReturnToBookmark(snapSliceEntryBookmark));
            match = matches[2];
            assertMatch(match, ElementMatcher.MatchAction.Invalid, snapNav, diffNav);
            Assert.AreEqual("his", diffNav.Current.SliceName);
            Assert.IsNull(snapNav.Current.SliceName);
            // Verify generated outcome issue
            Assert.IsNotNull(match.Issue);
            Debug.Print(match.Issue.Details?.Text);
            Assert.IsTrue(int.TryParse(match.Issue.Details?.Coding?[0]?.Code, out code));
            Assert.AreEqual(SnapshotGenerator.PROFILE_ELEMENTDEF_SLICENAME_NOMATCH.Code, code);

            // No more matches
            Assert.IsFalse(diffNav.MoveToNext());
        }

        // [WMR 20190902] #1090 SnapshotGenerator should support logical models
        [TestMethod]
        public async T.Task TestElementMatcher_LogicalModel()
        {
            const string rootPath = "MyModel";
            var SimpleLogicalModel = new StructureDefinition()
            {
                Url = "http://example.org/fhir/StructureDefinition/SimpleLogicalModel",
                Name = "SimpleLogicalModel",
                Kind = StructureDefinition.StructureDefinitionKind.Logical,
                // Last segment equals root element name
                Type = "http://example.org/fhir/StructureDefinition/" + rootPath,
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Element),
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition(rootPath)
                        {
                            //Min = 0,
                            //Max = "*",
                            //Type = new List<ElementDefinition.TypeRefComponent>()
                            //{
                            //    new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Element.GetLiteral() }
                            //}
                        },
                        new ElementDefinition(rootPath + ".target")
                        {
                            //Min = 0,
                            Max = "1",
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Reference.GetLiteral(),
                                    TargetProfile = new string[] { ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Person) }
                                }
                            }
                        },
                        new ElementDefinition(rootPath + ".value[x]")
                        {
                            //Min = 0,
                            //Max = "*",
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.String.GetLiteral(),
                                },
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Boolean.GetLiteral(),
                                }
                            }

                        }
                    }
                }
            };

            var baseProfile = await _testResolver.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Element);
            Assert.IsNotNull(baseProfile);
            Assert.IsTrue(baseProfile.HasSnapshot); // Rely on default snapshot
            baseProfile.Snapshot.Rebase(rootPath);  // Explicitly rebase before matching!


            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(SimpleLogicalModel);

            // Merge: MyModel (root element)
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);

            // New: MyModel.target
            Assert.IsTrue(diffNav.MoveToFirstChild());
            assertMatch(matches[0], ElementMatcher.MatchAction.New, snapNav, diffNav);

            // New: MyModel.value[x]
            Assert.IsTrue(diffNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.New, snapNav, diffNav);

            Assert.IsFalse(diffNav.MoveToNext());
        }


        // ========== Helper functions ==========

        // Recursively match diffNav to snapNav and verify that all matches return the specified action (def. Merged)
        static List<ElementMatcher.MatchInfo> matchAndVerify(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, ElementMatcher.MatchAction action = ElementMatcher.MatchAction.Merge)
        {
            List<ElementMatcher.MatchInfo> matches = Match(snapNav, diffNav);
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
            //matches.DumpMatches(snapNav, diffNav);
            Assert.AreEqual(1, matches.Count);
            assertMatch(matches[0], action, snapNav, diffNav);
        }

        static void assertMatch(ElementMatcher.MatchInfo match, ElementMatcher.MatchAction action, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav = null, ElementDefinition sliceBase = null)
        {
            assertMatch(match, action, snapNav.Bookmark(), diffNav != null ? diffNav.Bookmark() : Bookmark.Empty, sliceBase);
        }

        static void assertMatch(ElementMatcher.MatchInfo match, ElementMatcher.MatchAction action, Bookmark snap, Bookmark diff, ElementDefinition sliceBase)
        {
            Assert.IsNotNull(match);
            Assert.AreEqual(action, match.Action);
            Assert.AreEqual(snap, match.BaseBookmark);
            Assert.AreEqual(diff, match.DiffBookmark);
            Assert.IsTrue(sliceBase is null || sliceBase.IsExactly(match.SliceBase.Current));
        }
    }
}
