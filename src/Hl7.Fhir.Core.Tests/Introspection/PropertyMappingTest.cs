/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.DSTU2;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
	public class ModelInspectorMembersTest
    {
        [TestMethod]
        public void TestPrimitiveDataTypeMapping()
        {
            var mapping = ClassMapping.Create(typeof(Base64Binary));
            Assert.AreEqual("base64Binary", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.DSTU2).Count); // id, extension & value
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.STU3).Count);
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.R4).Count);
            var idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.DSTU2, "id");
            Assert.IsNotNull(idProperty);
            idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.STU3, "id");
            Assert.IsNotNull(idProperty);
            idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.R4, "id");
            Assert.IsNotNull(idProperty);
            var valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.AreEqual("value", valueProp.Name);
            Assert.IsFalse(valueProp.IsCollection);     // don't see byte[] as a collection of byte in FHIR
            Assert.IsTrue(valueProp.RepresentsValueElement);

            mapping = ClassMapping.Create(typeof(Code<AddressUse>));
            Assert.AreEqual("codeOfT<Hl7.Fhir.Model.DSTU2.AddressUse>", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.DSTU2).Count); // id, extension & value
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.STU3).Count);
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.R4).Count);
            idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.DSTU2, "id");
            Assert.IsNotNull(idProperty);
            idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.STU3, "id");
            Assert.IsNotNull(idProperty);
            idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.R4, "id");
            Assert.IsNotNull(idProperty);
            valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.IsFalse(valueProp.IsCollection);
            Assert.IsTrue(valueProp.RepresentsValueElement);
            Assert.AreEqual(typeof(AddressUse),valueProp.ImplementingType);

            mapping = ClassMapping.Create(typeof(FhirUri));
            Assert.AreEqual("uri", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.DSTU2).Count); // id, extension & value
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.STU3).Count);
            Assert.AreEqual(3, mapping.GetPropertyMappings(Fhir.Model.Version.R4).Count);
            idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.DSTU2, "id");
            Assert.IsNotNull(idProperty);
            idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.STU3, "id");
            Assert.IsNotNull(idProperty);
            idProperty = mapping.FindMappedElementByName(Fhir.Model.Version.R4, "id");
            Assert.IsNotNull(idProperty);
            valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.IsFalse(valueProp.IsCollection); 
            Assert.IsTrue(valueProp.RepresentsValueElement);
            Assert.AreEqual(typeof(string),valueProp.ImplementingType);

            mapping = ClassMapping.Create(typeof(OperationOutcome.IssueComponent));
            Assert.AreEqual(8, mapping.GetPropertyMappings(Fhir.Model.Version.DSTU2).Count);  // id, extension, modifierExtension, severity, code, details, diagnostics, location
            Assert.AreEqual(9, mapping.GetPropertyMappings(Fhir.Model.Version.STU3).Count); // + expression
            Assert.AreEqual(9, mapping.GetPropertyMappings(Fhir.Model.Version.R4).Count); // + expression
            var expressionProperty = mapping.FindMappedElementByName(Fhir.Model.Version.DSTU2, "expression");
            Assert.IsNull(expressionProperty);
            expressionProperty = mapping.FindMappedElementByName(Fhir.Model.Version.STU3, "expression");
            Assert.IsNotNull(expressionProperty);
            expressionProperty = mapping.FindMappedElementByName(Fhir.Model.Version.R4, "expression");
            Assert.IsNotNull(expressionProperty);

            mapping = ClassMapping.Create(typeof(Meta));
            Assert.AreEqual(7, mapping.GetPropertyMappings(Fhir.Model.Version.DSTU2).Count);  // id, extension, versionId, lastUpdated, profile, security, tag
            Assert.AreEqual(7, mapping.GetPropertyMappings(Fhir.Model.Version.STU3).Count); // 
            Assert.AreEqual(8, mapping.GetPropertyMappings(Fhir.Model.Version.R4).Count); // + source
            var sourceProperty = mapping.FindMappedElementByName(Fhir.Model.Version.DSTU2, "source");
            Assert.IsNull(sourceProperty);
            sourceProperty = mapping.FindMappedElementByName(Fhir.Model.Version.STU3, "source");
            Assert.IsNull(sourceProperty);
            sourceProperty = mapping.FindMappedElementByName(Fhir.Model.Version.R4, "source");
            Assert.IsNotNull(expressionProperty);
        }
    }
}
