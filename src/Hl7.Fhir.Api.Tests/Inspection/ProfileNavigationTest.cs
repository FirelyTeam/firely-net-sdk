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
using Hl7.Fhir.Api.Introspection;
using Hl7.Fhir.Api.Introspection.Source;
using Hl7.Fhir.Serialization;
using System.Reflection;
using System.Xml;
using Hl7.Fhir.Introspection;

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