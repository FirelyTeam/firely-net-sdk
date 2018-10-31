/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Hl7.Fhir.Specification.Source
{
    public class SchemaCollection
    {

        private static Lazy<XmlSchemaSet> _validationSchemaSet = new Lazy<XmlSchemaSet>(compileValidationSchemas);

        public static XmlSchemaSet ValidationSchemaSet
        {
            get { return _validationSchemaSet.Value; }
        }


        private static string[] minimalSchemas = { "xml.xsd", "fhir-single.xsd", "fhir-xhtml.xsd","xmldsig-core-schema.xsd" };
        
        private static XmlSchemaSet compileValidationSchemas()
        {
            var resolver = ZipSource.CreateValidationSource();

            XmlSchemaSet schemas = new XmlSchemaSet();

            foreach(var schemaName in minimalSchemas)
            {
                using (var schema = resolver.LoadArtifactByName(schemaName))
                {
                    if(schema == null)
                        throw new FileNotFoundException("Cannot find manifest resources that represent the minimal set of schemas required for validation");

                    schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file
                }
            }

            schemas.Compile();

            return schemas;
        }
    }
}
