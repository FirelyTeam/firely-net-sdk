/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
    public class VersionAwarePocoProvTest
    {
        [TestMethod]
        public void TestResourceNameResolving()
        {
            var inspector = new ModelInspector(Specification.FhirRelease.STU3);

            inspector.ImportType(typeof(Way));
            inspector.ImportType(typeof(Way2));

            var way = inspector.FindClassMapping("Way");
            Assert.IsNotNull(way);
            Assert.AreEqual(way.NativeType, typeof(Way));

            var way2 = inspector.FindClassMapping("Way2");
            Assert.IsNotNull(way2);
            Assert.AreEqual(way2.NativeType, typeof(Way2));

            var noway = inspector.FindClassMapping("nonexistent");
            Assert.IsNull(noway);
        }


        [TestMethod]
        public void TestAssemblyInspection()
        {
            var inspector = new ModelInspector(Specification.FhirRelease.STU3);

            // Inspect the HL7.Fhir.Model common assembly
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // Check for presence of some basic ingredients
            Assert.IsNotNull(inspector.FindClassMapping("Meta"));
            Assert.IsNotNull(inspector.FindClassMapping(typeof(Code)));
            Assert.IsNotNull(inspector.FindClassMapping("boolean"));

            // Should also have found the abstract classes
            Assert.IsNotNull(inspector.FindClassMapping("Element"));
            Assert.IsNotNull(inspector.FindClassMapping(typeof(Resource)));

            // The open generic Code<> should not be there
            var codeOfT = inspector.FindClassMapping(typeof(Code<>));
            Assert.IsNull(codeOfT);
        }

    }

    [FhirEnumeration("SomeEnum")]
    public enum SomeEnum { Member, AnotherMember }

    public class ActResource
    {
        [FhirEnumeration("SomeOtherEnum")]
        public enum SomeOtherEnum { Member, AnotherMember }
    }
}
