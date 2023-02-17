#nullable enable

/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Hl7.Fhir.Utility
{
    /// <summary>
    /// Internal class that holds a set of compiled Xsd schema.
    /// </summary>
    public class IncludedXsdSchemaSet
    {
        private readonly (Assembly a, string k)[] _resourceRefs;

        /// <summary>
        /// Create a schema set given a set of embedded resource keys to reach the schemas from.
        /// </summary>
        public IncludedXsdSchemaSet(Assembly resourceAssembly, params string[] resourceKeys) : this(null, resourceAssembly, resourceKeys)
        {
            // Nothing
        }

        /// <summary>
        /// Create a schema set given an existing set, and and a set of embedded resource keys to reach the schemas from.
        /// </summary>
        public IncludedXsdSchemaSet(IncludedXsdSchemaSet? other, Assembly resourceAssembly, params string[] resourceKeys)
        {
            var additionalRefs = resourceKeys.Select(rk => (resourceAssembly, rk));
            _resourceRefs = other is not null ?
                other._resourceRefs.Concat(additionalRefs).ToArray() : additionalRefs.ToArray();
            _compiledSchemas = new Lazy<XmlSchemaSet>(compileSchemas, true);
        }

        private readonly Lazy<XmlSchemaSet> _compiledSchemas;

        /// <summary>
        /// Return the cached and compiled set of Xsd schemas.
        /// </summary>
        public XmlSchemaSet CompiledSchemas => _compiledSchemas.Value;

        private XmlSchemaSet compileSchemas()
        {
            XmlSchemaSet schemas = new();

            foreach (var resourceRef in _resourceRefs)
            {
                using var schemaStream = readResource(resourceRef.a, resourceRef.k);
                schemas.Add(null, XmlReader.Create(schemaStream));   // null = use schema namespace as specified in schema file
            }

            schemas.Compile();
            return schemas;
        }

        private static Stream readResource(Assembly assembly, string resourceName)
        {
            var stream = assembly.GetManifestResourceStream(resourceName) ??
                throw new Exception($"Resource {resourceName} not found in {assembly.FullName}.  Valid resources are: {String.Join(", ", assembly.GetManifestResourceNames())}.");

            if (resourceName.EndsWith(".zip"))
            {
                var archive = new ZipArchive(stream, ZipArchiveMode.Read, leaveOpen: false);
                if (archive.Entries.Count > 1)
                    throw new InvalidOperationException("Expected only a single XSD in the schema zip embedded resource, not these: " +
                        string.Join(",", archive.Entries.Select(e => e.Name)));

                return archive.Entries.First().Open();
            }
            else
                return stream;
        }
    }
}

#nullable restore