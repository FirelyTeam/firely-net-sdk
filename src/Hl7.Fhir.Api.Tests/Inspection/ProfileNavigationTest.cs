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

            Assert.IsTrue(nav.HasChildren);
            Assert.IsFalse(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(1, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(2, nav.OrdinalPosition);
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(3, nav.OrdinalPosition);
            Assert.IsFalse(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToParent());
            Assert.AreEqual(1, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(4, nav.OrdinalPosition);
            Assert.IsFalse(nav.HasChildren);
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.AreEqual(4, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.IsFalse(nav.MoveToNext());
            Assert.AreEqual(6, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToParent());
            Assert.AreEqual(0, nav.OrdinalPosition);
            Assert.IsFalse(nav.MoveToParent());
        }

        [TestMethod]
        public void TestChildNavigationNamed()
        {
            var nav = createTestNav();

            Assert.IsTrue(nav.JumpTo("A.B"));
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(4, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToNext("B"));
            Assert.AreEqual(5, nav.OrdinalPosition);
            Assert.IsFalse(nav.MoveToNext("B"));

            nav.MoveToRoot();
            Assert.IsTrue(nav.JumpTo("A.B.C2"));
            Assert.IsFalse(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToParent());          
            Assert.IsTrue(nav.MoveToChild("C2"));
            Assert.IsTrue(nav.MoveToParent());
            Assert.IsFalse(nav.MoveToNext("X"));
            Assert.IsTrue(nav.MoveToNext("D"));
            Assert.AreEqual(6, nav.OrdinalPosition);
        }

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

        [TestMethod, Ignore]
        public void TestFindSliceParent()
        {
            // Find an element that is a slice (and so it has multiple siblings with the same name)
        }

        [TestMethod, Ignore]
        public void TestFindSlicedChildren()
        {
            // Find sliced children of an element (children will have the same name)
        }
    }
}