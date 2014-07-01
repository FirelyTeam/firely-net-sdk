/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Hl7.Fhir.Validation
{
    public class SchemaCollection
    {
#if !PORTABLE45
        private static Lazy<XmlSchemaSet> _validationSchemaSet = new Lazy<XmlSchemaSet>(compileXhtmlSchema, true);

        public static XmlSchemaSet ValidationSchemaSet
        {
            get { return _validationSchemaSet.Value; }
        }

        private static string[] minimalSchemas = { "fhir-atom-single.xsd", "fhir-single.xsd", "fhir-xhtml.xsd", "opensearch.xsd", "opensearchscore.xsd", "tombstone.xsd", "xml.xsd", "xmldsig-core-schema.xsd" };

        private static XmlSchemaSet compileXhtmlSchema()
        {
            var assembly = typeof(SchemaCollection).Assembly;
            var schemaNames = assembly.GetManifestResourceNames().Where( n => n.StartsWith("Hl7.Fhir.Schemas") && n.EndsWith(".xsd"));

            XmlSchemaSet schemas = new XmlSchemaSet();

            foreach(var schemaName in schemaNames)
            {
                Stream schema = assembly.GetManifestResourceStream(schemaName); 
                schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file
            }

            schemas.Compile();

            return schemas;
        }
#endif

    }
}
