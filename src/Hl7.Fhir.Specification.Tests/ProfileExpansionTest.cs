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
using Hl7.Fhir.Serialization;
using System.Collections.Generic;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Expansion;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Test.Inspection
{
    [TestClass]
#if PORTABLE45
	public class PortableProfileExpansionTest
#else
    public class ProfileExpansionTest
#endif
    {
        private IArtifactSource _source;

        [TestInitialize]
        public void Setup()
        {
            _source = new CachedArtifactSource(ArtifactResolver.CreateDefault());
        }


        [TestMethod]
        public void ExpandProfile()
        {
            var expander = new ProfileExpander(_source);
            
            // This file will be found local to the test DLL, based on its it, not its url
            var diff = (Profile)_source.ReadResourceArtifact(new Uri("http://test.nu/Profile/example-lipid-profile-differential"));
            Assert.IsNotNull(diff);

            var snapshot = expander.Expand(diff);
            Assert.IsNotNull(diff);

            var xml = FhirSerializer.SerializeResourceToXml(snapshot);
            File.WriteAllText("c:\\temp\\expanded.xml", xml);
        }

        [TestMethod]
        public void MakeDifferentialTree()
        {
            var struc = new Profile.ProfileStructureComponent();
            struc.Element = new List<Profile.ElementComponent>();

            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B.C1" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B.C1" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B.C2" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.B.C1.D" });
            struc.Element.Add(new Profile.ElementComponent() { Path = "A.D.F" });

            var tree = new DifferentialTreeConstructor(struc).MakeTree();
            Assert.IsNotNull(tree);

            var nav = new ElementNavigator(tree);
            Assert.AreEqual(10, nav.Count);

            Assert.IsTrue(nav.MoveToChild("A"));
            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsTrue(nav.MoveToChild("C1"));
            Assert.IsTrue(nav.MoveToNext("C1"));
            Assert.IsTrue(nav.MoveToNext("C2"));

            Assert.IsTrue(nav.MoveToParent());  // 1st A.B
            Assert.IsTrue(nav.MoveToNext() && nav.Path == "A.B");  // (now) 2nd A.B
            Assert.IsTrue(nav.MoveToChild("C1"));
            Assert.IsTrue(nav.MoveToChild("D"));

            Assert.IsTrue(nav.MoveToParent());  // A.B.C1
            Assert.IsTrue(nav.MoveToParent());  // A.B (2nd)
            Assert.IsTrue(nav.MoveToNext() && nav.Path == "A.D");
            Assert.IsTrue(nav.MoveToChild("F"));
        }



        [TestMethod]
        public void LocateStructure()
        {
            var locator = new StructureLoader(ArtifactResolver.CreateDefault());
            var profileUri = new Uri("http://hl7.org/fhir/Profile/Profile");

            var prof = locator.LocateStructure(profileUri, new Code("Profile"));
            Assert.IsNotNull(prof);
            Assert.AreEqual("Profile", prof.Type);
            //Assert.AreEqual(profileUri.ToString(), prof.GetProfileLocation());

            // Try to locate a structure that cannot be found in the given profile
            prof = locator.LocateStructure(profileUri, new Code("Patient"));
            Assert.IsNull(prof);

            profileUri = new Uri("http://take.from.disk/Profile/example-lipid-profile");
            var profileUriWithFrag = new Uri(profileUri.ToString() + "#lipidResultMessage");
            prof = locator.LocateStructure(profileUriWithFrag, new Code("MessageHeader"));
            Assert.IsNotNull(prof);
            Assert.AreEqual("MessageHeader", prof.Type);
            Assert.AreEqual("lipidResultMessage", prof.Name);
            //Assert.AreEqual(profileUri.ToString(), prof.GetProfileLocation());

            // Try to locate a structure that cannot be found in the profile by name
            profileUriWithFrag = new Uri(profileUri.ToString() + "#XXX");
            prof = locator.LocateStructure(profileUriWithFrag, new Code("Profile"));
            Assert.IsNull(prof);
        }


        [TestMethod]
        public void TestExpandChild()
        {
            var loader = new StructureLoader(ArtifactResolver.CreateDefault());
            var profStruct = loader.LocateStructure(new Uri("http://hl7.org/fhir/Profile/Profile"), new Code("Profile"));

            var nav = new ElementNavigator(profStruct);
            
            nav.JumpToFirst("Profile.telecom");
            Assert.IsTrue(nav.ExpandElement(loader));
            Assert.IsTrue(nav.MoveToChild("period"));

            nav.JumpToFirst("Profile.extensionDefn.definition");
            Assert.IsTrue(nav.ExpandElement(loader));
            Assert.IsTrue(nav.MoveToChild("max"));
        }

    }
}