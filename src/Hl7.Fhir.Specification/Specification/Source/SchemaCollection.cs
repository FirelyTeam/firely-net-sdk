/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

#nullable enable

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// A class that retrieves, compiles and caches an <see cref="XmlSchemaSet"/> necessary to validate FHIR XML.
    /// </summary>
    public class SchemaCollection
    {
        private static readonly string[] minimalSchemas = { "xml.xsd", "fhir-single.xsd", "fhir-xhtml.xsd", "xmldsig-core-schema.xsd" };

        /// <summary>
        /// Constructs a SchemaCollection which retrieves XML schemas from the given <see cref="IArtifactSource"/>.
        /// </summary>
        public SchemaCollection(IArtifactSource xsdSource)
        {
            _validationSchemaSet = new(() => compileValidationSchemas(xsdSource));
        }

        /// <summary>
        /// Constructs a SchemaCollection which retrieves XML schemas from the default source.
        /// </summary>
        /// <remarks>The default source is the source returned by <see cref="ZipSource.CreateValidationSource()"/>.</remarks>
        public SchemaCollection()
        {
            _validationSchemaSet = new(() => compileValidationSchemas(ZipSource.CreateValidationSource()));
        }

        private readonly Lazy<XmlSchemaSet> _validationSchemaSet;

        /// <summary>
        /// Return an XmlSchemaSet that contains the minimal set necessary to validate FHIR XML.
        /// </summary>
        public XmlSchemaSet MinimalSchemas => _validationSchemaSet.Value;

        /// <summary>
        /// Returns the schemas necessary to use XML validation on FHIR resources.
        /// </summary>
        /// <remarks>The schemas will be searched for at the default location, <see cref="DirectorySource.SpecificationDirectory"/>.</remarks>
        public static XmlSchemaSet ValidationSchemaSet => Default.MinimalSchemas;


        public static SchemaCollection Default = new();


        private static XmlSchemaSet compileValidationSchemas(IArtifactSource source)
        {
            var schemas = new XmlSchemaSet();

            foreach (var schemaName in minimalSchemas)
            {
                using var schema = source.LoadArtifactByName(schemaName);

                if (schema == null)
                    throw new FileNotFoundException("Cannot find manifest resources that represent the minimal set of schemas required for validation");

                schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file
            }

            schemas.Compile();

            return schemas;
        }
    }
}

#nullable restore