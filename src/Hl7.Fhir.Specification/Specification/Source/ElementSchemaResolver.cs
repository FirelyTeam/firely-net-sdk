/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation.Schema;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Specification.Source
{
    internal class ElementSchemaResolver : IResourceResolver, ISchemaResolver
    {
        private readonly IResourceResolver _wrapped;
        private readonly IDictionary<Uri, ElementSchema> _cache = new Dictionary<Uri, ElementSchema>();

        public ElementSchemaResolver(IResourceResolver wrapped)
        {
            _wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }

        public ElementSchema GetSchema(Uri schemaUri)
        {
            if (_cache.TryGetValue(schemaUri, out ElementSchema schema))
            {
                return schema;
            }

            if (schemaUri.OriginalString.EndsWith("System.String"))
            {
                schema = new ElementSchema(schemaUri);
            }


            var sd = this.FindStructureDefinition(schemaUri.OriginalString);
            if (sd != null)
            {
                schema = new SchemaConverter(this).Convert(sd);
            }

            _cache.Add(schemaUri, schema);
            return schema;
        }

        public IEnumerable<ElementSchema> GetSchemas()
        {
            return _cache.Values;
        }

        public Resource ResolveByCanonicalUri(string uri) => _wrapped.ResolveByCanonicalUri(uri);

        public Resource ResolveByUri(string uri) => _wrapped.ResolveByUri(uri);
    }
}