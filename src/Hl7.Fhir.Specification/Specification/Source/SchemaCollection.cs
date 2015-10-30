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

namespace Hl7.Fhir.Specification.Source
{
#if !PORTABLE45
    public class SchemaCollection
    {

        private static Lazy<XmlSchemaSet> _validationSchemaSet = new Lazy<XmlSchemaSet>(compileValidationSchemas, true);

        public static XmlSchemaSet ValidationSchemaSet
        {
            get { return _validationSchemaSet.Value; }
        }


        private static string[] minimalSchemas = { "fhir-single.xsd", "fhir-xhtml.xsd", "xml.xsd", "xmldsig-core-schema.xsd" };
        
        private static XmlSchemaSet compileValidationSchemas()
        {
            var resolver = ZipArtifactSource.CreateValidationSource();

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
#endif
}
