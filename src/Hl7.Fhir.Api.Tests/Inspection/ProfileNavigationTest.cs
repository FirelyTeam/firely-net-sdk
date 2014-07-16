/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Diagnostics;
using System.IO;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Serialization;
using System.Reflection;
using System.Xml;

namespace Hl7.Fhir.Test.Inspection
{
    [TestClass]
#if PORTABLE45
	public class PortableProfileNavigationTest
#else
    public class ProfileNavigationTest
#endif
    {
        public Profile testProfile;

        [TestInitialize]
        public void SetupProfile()
        {
            var str = this.GetType().Assembly.GetManifestResourceStream("Hl7.Fhir.Test.profile.profile.xml");
            testProfile = (Profile)FhirParser.ParseResource(XmlReader.Create(str));
        }

        [TestMethod]
        public void TestFindChild()
        {
            var profStruct = testProfile.Structure[0];

            var child = profStruct.FindChild("Profile.structure.searchParam.documentation");
            Assert.IsNotNull(child);
            Assert.AreEqual("documentation", child.GetNameFromPath());
            Assert.AreEqual("Profile.structure.searchParam.documentation", child.Path);

            child = profStruct.FindChild("Profile.doesntexist");
            Assert.IsNull(child);

            child = profStruct.FindChild("Profile.extensionDefn.definition.max");
            Assert.IsNotNull(child);
            Assert.AreEqual("max", child.GetNameFromPath());
            Assert.AreEqual("Profile.structure.element.definition.max", child.Path);

            child = profStruct.FindChild("Profile.extensionDefn.definition");
            Assert.IsNotNull(child);
            Assert.AreEqual(child, profStruct.FindChild("Profile.structure.element.definition"));

            child = profStruct.FindChild("Profile");
            Assert.IsNotNull(child);
            Assert.AreEqual(child, profStruct.Element[0]);
        }

        [TestMethod]
        public void TestListChildren()
        {
            var profStruct = testProfile.Structure[0];

            var children = profStruct.GetChildren("Profile.structure.searchParam");
            Assert.AreEqual(5+2, children.Count());   // first 2 are extension elements
            Assert.AreEqual("name", children.Skip(2).First().GetNameFromPath());

            children = profStruct.GetChildren("Profile.crap");
            Assert.AreEqual(0, children.Count());

            children = profStruct.GetChildren("Profile.query", includeGrandchildren: true);
            Assert.AreEqual(3 + 2 + 5 + 2, children.Count());
        }

        [TestMethod]
        public void TestChildNavigation()
        {
            var nav = createTestNav();

            Assert.IsTrue(nav.HasChildren());
            Assert.IsFalse(nav.MoveToNext());

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(0, nav.OrdinalPosition);    // A
            Assert.AreEqual("A", nav.CurrentPath());

            Assert.IsTrue(nav.MoveToFirstChild());      // A.B
            Assert.AreEqual(1, nav.OrdinalPosition);
            Assert.AreEqual("A.B", nav.CurrentPath());

            Assert.IsTrue(nav.MoveToFirstChild());      // A.B.C1
            Assert.AreEqual(2, nav.OrdinalPosition);
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.AreEqual("A.B.C1", nav.CurrentPath());

            Assert.IsTrue(nav.MoveToNext());       // A.B.C2 
            Assert.AreEqual(3, nav.OrdinalPosition);
            Assert.AreEqual("A.B.C2", nav.CurrentPath());

            Assert.IsFalse(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToParent());
            Assert.AreEqual(1, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(4, nav.OrdinalPosition);
            Assert.IsFalse(nav.HasChildren());
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.AreEqual(4, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.IsFalse(nav.MoveToNext());            
            Assert.AreEqual(6, nav.OrdinalPosition);  // A.D
            Assert.AreEqual("A.D", nav.CurrentPath());
            
            Assert.IsTrue(nav.MoveToPrevious());        // A.B
            Assert.AreEqual(5, nav.OrdinalPosition);
            Assert.AreEqual("A.B", nav.CurrentPath());

            Assert.IsTrue(nav.MoveToParent());
            Assert.AreEqual(0, nav.OrdinalPosition);

            Assert.IsTrue(nav.MoveToParent());  // root
            Assert.IsFalse(nav.MoveToParent()); // beyond root
        }

        [TestMethod]
        public void TestChildNavigationNamed()
        {
            var nav = createTestNav();
            nav.MoveToFirstChild();     // A

            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(4, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToNext("B"));
            Assert.AreEqual(5, nav.OrdinalPosition);
            Assert.IsFalse(nav.MoveToNext("B"));

            nav.Reset();
            nav.MoveToFirstChild();     // A

            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsTrue(nav.MoveToChild("C2"));
            Assert.IsFalse(nav.MoveToNext());
            Assert.AreEqual(3, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToParent());          
            Assert.IsTrue(nav.MoveToChild("C2"));
            Assert.IsTrue(nav.MoveToParent());
            Assert.IsFalse(nav.MoveToNext("X"));
            Assert.IsTrue(nav.MoveToNext("D"));
            Assert.AreEqual(6, nav.OrdinalPosition);

            Assert.IsFalse(nav.MoveToPrevious("X"));
            Assert.IsTrue(nav.MoveToPrevious("B"));
            Assert.AreEqual(5, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToPrevious("B"));
            Assert.IsTrue(nav.MoveToPrevious("B"));
            Assert.AreEqual(1, nav.OrdinalPosition);
        }

      
        //[TestMethod]
        //public void TestBasicAlterations()
        //{
        //    var nav = createTestNav();

        //    var newCNode = new Profile.ElementComponent() { Path = "X.C" };
        //    var newNode = new Profile.ElementComponent() { Path = "X.Y.E" };
        //    var newChildNode = new Profile.ElementComponent() { Path = "X.Y.F" };
        //    var newChildNodeC3 = new Profile.ElementComponent() { Path = "X.C3" };

        //    Assert.IsTrue(nav.JumpTo("A.D"));
        //    nav.InsertBefore(newCNode);
        //    Assert.AreEqual("A.D", nav.CurrentPath());
        //    Assert.IsTrue(nav.MoveToPrevious());
        //    Assert.AreEqual("A.C", nav.CurrentPath());
        //    Assert.IsTrue(nav.MoveToNext());
        //    nav.InsertAfter(newNode);
        //    Assert.AreEqual("A.D", nav.CurrentPath());
        //    Assert.IsTrue(nav.MoveToNext());
        //    Assert.AreEqual("A.E", nav.CurrentPath());
        //    nav.AppendChild(newChildNode);
        //    Assert.AreEqual("A.E", nav.CurrentPath());
        //    Assert.IsTrue(nav.HasChildren());
        //    Assert.IsTrue(nav.MoveToFirstChild());
        //    Assert.AreEqual("A.E.F", nav.CurrentPath());
            
        //    Assert.IsTrue(nav.JumpTo("A.B"));
        //    nav.AppendChild(newChildNodeC3);
        //    Assert.IsTrue(nav.MoveToFirstChild());
        //    Assert.IsTrue(nav.MoveToNext());
        //    Assert.IsTrue(nav.MoveToNext());
        //    Assert.AreEqual("A.B.C3", nav.CurrentPath());
        //}


        [TestMethod]
        public void TestDeletions()
        {
            var nav = createTestNav();

            nav.MoveToFirstChild();     // A
            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsFalse(nav.Delete(), "still has children");

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.MoveToNext());            // A.B.C2
            Assert.IsTrue(nav.Delete());
            Assert.AreEqual("A.B.C1", nav.CurrentPath(), "Did not move back to sibling");
            Assert.IsTrue(nav.Delete());
            Assert.AreEqual(1, nav.OrdinalPosition, "Did not move back to parent");

            Assert.IsTrue(nav.MoveToNext());

            Assert.IsTrue(nav.Delete());  // 2nd A.B
            Assert.IsTrue(nav.Delete());  // 3rd A.B
            Assert.AreEqual("A.D", nav.CurrentPath());

            Assert.IsTrue(nav.Delete());  // A.D
            Assert.AreEqual("A.B", nav.CurrentPath());

            Assert.IsTrue(nav.Delete());
            Assert.AreEqual(1, nav.Count);  // only A left

            Assert.IsTrue(nav.Delete());
            Assert.AreEqual(0, nav.Count);
            Assert.IsNull(nav.Current);

            Assert.IsFalse(nav.Delete());
        }

        //[TestMethod]
        //public void TestModificationResilience()
        //{
        //    var nav = createTestNav();
        //    nav.JumpTo("A.B.C1");

        //    var nav2 = nav.Clone();

        //    // Delete children in nav
        //    Assert.IsTrue(nav.Delete());
        //    Assert.IsTrue(nav.Delete());

        //    // Should still be there in nav2
        //    nav2.JumpTo("A.B");
        //    Assert.IsTrue(nav2.HasChildren());
        //}


        [TestMethod]
        public void TestBookmarks()
        {
            var nav = createTestNav();

            Assert.IsTrue(nav.MoveToFirstChild());  // A
            Assert.IsTrue(nav.MoveToFirstChild());  // A.B
            var bm1 = nav.Bookmark();
            var pos1 = nav.OrdinalPosition;

            Assert.IsTrue(nav.MoveToNext());        // 2nd A.B
            var bm2 = nav.Bookmark();
            var pos2 = nav.OrdinalPosition;

            Assert.AreNotEqual(pos1, pos2);
            Assert.AreNotEqual(bm1.data, bm2.data);

            nav.ReturnToBookmark(bm1);
            Assert.AreEqual(pos1, nav.OrdinalPosition);
            nav.ReturnToBookmark(bm2);
            Assert.AreEqual(pos2, nav.OrdinalPosition);

        }

        //[TestMethod]
        //public void TestAbsoluteMoves()
        //{
        //    var nav = createTestNav();

        //    Assert.IsTrue(nav.JumpTo("A.B.C2"));
        //    Assert.AreEqual(3, nav.OrdinalPosition);

        //    Assert.IsFalse(nav.JumpTo("A.B.C1.E"));
        //    Assert.AreEqual(3, nav.OrdinalPosition);

        //    Assert.IsTrue(nav.Approach("A.B.C1.E"));
        //    Assert.AreEqual(2, nav.OrdinalPosition);

        //    Assert.IsTrue(nav.Approach("A.B.X"));
        //    Assert.AreEqual(1, nav.OrdinalPosition);

        //    Assert.IsTrue(nav.JumpTo("A.B"));
        //    Assert.IsTrue(nav.MoveToNext("B"));
        //    nav.AppendChild(new Profile.ElementComponent() { Path = "A.B.C2" });
        //    Assert.IsTrue(nav.MoveToChild("C2"));
        //    nav.AppendChild(new Profile.ElementComponent() { Path = "A.B.C2.E" });

        //    Assert.IsTrue(nav.Approach("A.B.C2"));
        //    Assert.AreEqual(3, nav.OrdinalPosition);

        //    Assert.IsTrue(nav.Approach("A.B.C2.E"));
        //    Assert.AreEqual(6, nav.OrdinalPosition);

        //}

     //   private static ElementNavigator createTestNav()
        //{
        //    var struc = new Profile.ProfileStructureComponent();
        //    struc.Element = new System.Collections.Generic.List<Profile.ElementComponent>();

        //    var nav = new ElementNavigator(struc);

        //    nav.AppendChild(new Profile.ElementComponent() { Path = "A" });
        //    nav.MoveToFirstChild();     // To A
        //    nav.AppendChild(new Profile.ElementComponent() { Path = "A.B" });
        //    nav.MoveToFirstChild();     // To A.B
        //    nav.AppendChild(new Profile.ElementComponent() { Path = "A.B.C1" });
        //    nav.AppendChild(new Profile.ElementComponent() { Path = "A.B.C2" });
        //    nav.InsertAfter(new Profile.ElementComponent() { Path = "A.B" });
        //    nav.InsertAfter(new Profile.ElementComponent() { Path = "A.B" });
        //    nav.InsertAfter(new Profile.ElementComponent() { Path = "A.D" });

        //    nav.Reset();

        //    return nav;
        //}


        private static ElementNavigator createTestNav()
        {
            var struc = new Profile.ProfileStructureComponent();
            struc.Element = new System.Collections.Generic.List<Profile.ElementComponent>();

            struc.Element.Add(new Profile.ElementComponent() { Path = "A" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B.C1" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B.C2" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.D" });

            var nav = new ElementNavigator(struc);
            return nav;
        }
    }
}