/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using System.Net;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Specification.Source
{
    public class ElementSchemaResolver : IResourceResolver, ISchemaResolver
    {
        public readonly IResourceResolver Wrapped;

        public ElementSchemaResolver(IResourceResolver wrapped)
        {
            Wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }

        public ElementSchema GetSchema(Uri schemaUri)
        {
            var sd = this.FindStructureDefinition(schemaUri.OriginalString);

            if (sd == null) return null;

            return new SchemaConverter(this).Convert(sd);
        }

        public Resource ResolveByCanonicalUri(string uri) => Wrapped.ResolveByCanonicalUri(uri);

        public Resource ResolveByUri(string uri) => Wrapped.ResolveByCanonicalUri(uri);
    }
}
