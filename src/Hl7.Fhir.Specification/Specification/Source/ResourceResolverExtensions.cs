/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    public static class ResourceResolverExtensions
    {
        public static StructureDefinition FindExtensionDefinition(this IResourceResolver source, string uri, bool requireSnapshot = false)
        {
            var sd = source.ResolveByCanonicalUri(uri) as StructureDefinition;
            if (sd == null) return null;

            if (!sd.IsExtension)
                throw Error.Argument("uri", "Given uri exists as a StructureDefinition, but is not an extension");

            if (sd.Snapshot == null && requireSnapshot)
                return null;

            return sd;
        }

        public static StructureDefinition FindStructureDefinition(this IResourceResolver source, string uri, bool requireSnapshot = false)
        {
            var sd = source.ResolveByCanonicalUri(uri) as StructureDefinition;
            if (sd == null) return null;

            if (sd.Snapshot == null && requireSnapshot)
                return null;

            return sd;
        }

        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver source, string typename)
        {
            var url = ModelInfo.CanonicalUriForFhirCoreType(typename);
            return source.FindStructureDefinition(url);
        }

        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver source, FHIRDefinedType type)
        {
            return source.FindStructureDefinitionForCoreType(ModelInfo.FhirTypeToFhirTypeName(type));
        }

        /// <summary>
        /// Tries to locate a valueset using a combined algorithm: first, the uri is used to find a valueset by system.
        /// If that fails, the valueset is searched for by canonical url. Failing that, the function tries to locate the
        /// valueset by resource url.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static ValueSet FindValueSet(this IConformanceStore source, string uri)
        {
            var vs = source.FindValuesetBySystem(uri);

            if (vs == null)
                vs = source.ResolveByCanonicalUri(uri) as ValueSet;

            if (vs == null)
                vs = source.ResolveByUri(uri) as ValueSet;

            return vs;
        }
    }
}
