/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Xml.Schema;

#nullable enable

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// A class that retrieves, compiles and caches an <see cref="XmlSchemaSet"/> necessary to validate FHIR XML.
    /// </summary>
    public class SchemaCollection
    {
        private const string FHIRSINGLE_XSD_RESOURCENAME = "xhtml.fhir-single.zip";

        /// <summary>
        /// Constructs a SchemaCollection which retrieves XML schemas from the given <see cref="IArtifactSource"/>.
        /// </summary>
        [Obsolete("The FHIR-supplied XSD schemas in SchemaCollection are now shipped as embedded resources, so you can no longer use a specific IArtifactSource to read from.")]
        public SchemaCollection(IArtifactSource _) : this()
        {
            // Nothing
        }

        /// <summary>
        /// Constructs a SchemaCollection which retrieves XML schemas from the default source.
        /// </summary>
        /// <remarks>The default source is the source returned by <see cref="CommonDirectorySource.SpecificationDirectory"/>.</remarks>
        public SchemaCollection()
        {
            _validationSchemaSet = new(SerializationUtil.BASEFHIRSCHEMAS, typeof(SchemaCollection).Assembly, FHIRSINGLE_XSD_RESOURCENAME);
        }

        private readonly IncludedXsdSchemaSet _validationSchemaSet;

        /// <summary>
        /// Return an XmlSchemaSet that contains the minimal set necessary to validate FHIR XML.
        /// </summary>
        public XmlSchemaSet MinimalSchemas => _validationSchemaSet.CompiledSchemas;

        /// <summary>
        /// Returns the schemas necessary to use XML validation on FHIR resources.
        /// </summary>
        public static XmlSchemaSet ValidationSchemaSet => Default.MinimalSchemas;

        /// <summary>
        /// A singleton instance of the <see cref="SchemaCollection"/>
        /// </summary>
        public static SchemaCollection Default = new();
    }
}

#nullable restore