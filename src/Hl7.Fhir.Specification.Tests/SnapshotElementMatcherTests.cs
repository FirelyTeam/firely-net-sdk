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
    [TestClass]
    public class SnapshotElementMatcherTests
    {
        IResourceResolver _testResolver;

        [TestInitialize]
        public void Setup()
        {
            var dirSource = new DirectorySource("TestData/snapshot-test", includeSubdirectories: true);
            _testResolver = new CachedResolver(dirSource);
        }

        [TestMethod]
        public void TestElementMatcher_Simple()
        {
            var baseProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.identifier")
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient
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
        public void TestElementMatcher_Patient()
        {
            // Match core patient profile to itself
            // All elements should be merged

            var baseProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var userProfile = (StructureDefinition)baseProfile.DeepCopy();

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForSnapshot(userProfile);

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            matches = matchChildren(snapNav, diffNav);
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Extension_New()
        {
            // Slice Patient.extension, introduce a new extension

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

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Add new extension
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Extension_Override()
        {
            // Constrain existing extension slice

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

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
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
            // Define another extension slice
            userProfile.Differential.Element[2].Type[0].Profile = new string[] { "http://example.org/fhir/StructureDefinition/myOtherExtension" };

            var snapNav = ElementDefinitionNavigator.ForDifferential(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[1], ElementMatcher.MatchAction.Add, snapNav, diffNav);    // Add new extension slice
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

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.extension
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
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
        public void TestElementMatcher_Patient_Animal_Slice()
        {
            var baseProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var userProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient"),
                        new ElementDefinition("Patient.animal") { Slicing = new ElementDefinition.SlicingComponent() { Discriminator = new string[] { "species.coding.code" } } },
                        new ElementDefinition("Patient.animal") { Name = "dog" }
                    }
                }
            };

            var snapNav = ElementDefinitionNavigator.ForSnapshot(baseProfile);
            var diffNav = ElementDefinitionNavigator.ForDifferential(userProfile);

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
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
            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>()
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

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Extension slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Merge, snapNav, diffNav);    // Merge existing new slice
        }

        [TestMethod]
        public void TestElementMatcher_Patient_Animal_Slice_Add()
        {
            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>()
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

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
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
            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>()
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

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Slice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
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
        public void TestElementMatcher_Patient_Animal_Subslice()
        {
            var baseProfile = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>()
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
                    Element = new System.Collections.Generic.List<ElementDefinition>()
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

            // Merge: Patient
            var matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            assertMatch(matches, ElementMatcher.MatchAction.Merge, snapNav, diffNav);

            // Reslice: Patient.animal
            matches = ElementMatcher.Match(snapNav, diffNav);
            Assert.IsNotNull(matches);
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
            Assert.AreEqual(4, matches.Count);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToChild(diffNav.PathName));
            assertMatch(matches[0], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            Assert.IsTrue(snapNav.MoveToNext());
            assertMatch(matches[1], ElementMatcher.MatchAction.Slice, snapNav, diffNav);    // Child slice entry
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[2], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Add new child slice
            Assert.IsTrue(diffNav.MoveToNext());
            // Don't advance snapNav; new diff slice is merged with default base = snap slice entry
            assertMatch(matches[3], ElementMatcher.MatchAction.Add, snapNav, diffNav);      // Add new child slice
        }

        // Helper functions

        static List<ElementMatcher.MatchInfo> matchChildren(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            List<ElementMatcher.MatchInfo> matches = ElementMatcher.Match(snapNav, diffNav);
            // MatchPrinter.DumpMatches(matches, snapNav, diffNav);
            Assert.IsTrue(diffNav.MoveToFirstChild());
            Assert.IsTrue(snapNav.MoveToFirstChild());
            for (int i = 0; i < matches.Count; i++)
            {
                dumpMatch(matches[i], snapNav, diffNav);
                assertMatch(matches[i], ElementMatcher.MatchAction.Merge, snapNav, diffNav);

                snapNav.ReturnToBookmark(matches[i].BaseBookmark);
                diffNav.ReturnToBookmark(matches[i].DiffBookmark);
                Assert.AreEqual(snapNav.HasChildren, diffNav.HasChildren);
                if (snapNav.HasChildren)
                {
                    matchChildren(snapNav, diffNav);
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
            MatchPrinter.DumpMatches(matches, snapNav, diffNav);
            Assert.AreEqual(1, matches.Count);
            assertMatch(matches[0], action, snapNav, diffNav);
        }

        static void assertMatch(ElementMatcher.MatchInfo match, ElementMatcher.MatchAction action, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            Assert.IsNotNull(match);
            Assert.AreEqual(action, match.Action);
            Assert.AreEqual(snapNav.Bookmark(), match.BaseBookmark);
            Assert.AreEqual(diffNav.Bookmark(), match.DiffBookmark);
        }

        static void dumpMatch(ElementMatcher.MatchInfo match, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var snapBM = snapNav.Bookmark();
            var diffBM = snapNav.Bookmark();

            Assert.IsTrue(snapNav.ReturnToBookmark(match.BaseBookmark));
            Assert.IsTrue(diffNav.ReturnToBookmark(match.DiffBookmark));

            var bPos = snapNav.Path + $"[{snapNav.OrdinalPosition}]";
            var dPos = diffNav.Path + $"[{diffNav.OrdinalPosition}]";
            if (snapNav.Current != null && snapNav.Current.Name != null) bPos += $" '{snapNav.Current.Name}'";
            if (diffNav.Current != null && diffNav.Current.Name != null) dPos += $" '{diffNav.Current.Name}'";
            Debug.WriteLine($"B:{bPos} <--{match.Action.ToString()}--> D:{dPos}");

            Assert.IsTrue(snapNav.ReturnToBookmark(snapBM));
            Assert.IsTrue(snapNav.ReturnToBookmark(diffBM));

        }
    }
}
