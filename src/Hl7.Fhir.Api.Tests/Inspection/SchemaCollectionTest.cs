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
using System.Xml.Linq;
using System.Xml.Schema;
using Hl7.Fhir.Introspection.Source;

namespace Hl7.Fhir.Test.Inspection
{
    [TestClass]
#if PORTABLE45
	public class PortableSchemaCollectionTest
#else
    public class SchemaCollectionTest
#endif
    {
        [TestMethod]
        public void TestSchemaCompilation()
        {
            var schemas = SchemaCollection.ValidationSchemaSet;
            Assert.IsTrue(schemas.Count > 0);

            var patDoc = XDocument.Parse("<Patient xmlns='http://hl7.org/fhir' />");
            patDoc.Validate(SchemaCollection.ValidationSchemaSet, null);

            try
            {
                patDoc = XDocument.Parse("<Patient xmlns='http://hl7.org/fhir'><garbage/></Patient>");
                patDoc.Validate(SchemaCollection.ValidationSchemaSet, null);
                Assert.Fail();
            }
            catch
            {
                // perfect.
            }
        }
    }
}