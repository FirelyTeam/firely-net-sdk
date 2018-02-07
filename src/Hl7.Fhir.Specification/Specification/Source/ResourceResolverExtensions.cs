/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    public static class ResourceResolverExtensions
    {
        public static StructureDefinition FindExtensionDefinition(this IResourceResolver resolver, string uri, bool requireSnapshot = false)
        {
            var sd = resolver.ResolveByCanonicalUri(uri) as StructureDefinition;
            if (sd == null) return null;

            if (!sd.IsExtension)
                throw Error.Argument(nameof(uri), "Given uri exists as a StructureDefinition, but is not an extension");

            if (sd.Snapshot == null && requireSnapshot)
                return null;

            return sd;
        }

        public static StructureDefinition FindStructureDefinition(this IResourceResolver resolver, string uri, bool requireSnapshot = false)
        {
            var sd = resolver.ResolveByCanonicalUri(uri) as StructureDefinition;
            if (sd == null) return null;

            if (sd.Snapshot == null && requireSnapshot)
                return null;

            return sd;
        }

        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver resolver, string typename)
        {
            var url = ModelInfo.CanonicalUriForFhirCoreType(typename);
            return resolver.FindStructureDefinition(url);
        }

        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver resolver, FHIRDefinedType type)
        {
            return resolver.FindStructureDefinitionForCoreType(ModelInfo.FhirTypeToFhirTypeName(type));
        }

        /// <summary>
        /// Tries to locate a valueset using a combined algorithm: first, the uri is used to find a valueset by system.
        /// If that fails, the valueset is searched for by canonical url. Failing that, the function tries to locate the
        /// valueset by resource url.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static ValueSet FindValueSet(this IResourceResolver source, string uri)
        {
            return source.ResolveByCanonicalUri(uri) as ValueSet;
        }


        public static IEnumerable<T> FindAll<T>(this IConformanceSource source) where T:Resource
        {
            var type = ModelInfo.GetFhirTypeNameForType(typeof(T));

            if (type != null)
            {
                var resourceType = EnumUtility.ParseLiteral<ResourceType>(type);
                // for some reason there is an issue with this StructureDefinition (needs fixing)
                var uris = source.ListResourceUris(resourceType).Where(u => u != "http://hl7.org/fhir/us/sdc/StructureDefinition/sdc-questionnaire");
                return uris.Select(u => source.ResolveByCanonicalUri(u) as T).Where(r => r != null);
            }
            else
                return null;
        }

        /// <summary>Resolve a <see cref="StructureDefinition"/> from a TypeRef.Code element, handle unknown/custom core types.</summary>
        /// <param name="resolver">An <see cref="IArtifactSource"/> reference.</param>
        /// <param name="typeCodeElement">A <see cref="ElementDefinition.TypeRefComponent.CodeElement"/> reference.</param>
        /// <returns>A <see cref="StructureDefinition"/> instance, or <c>null</c>.</returns>
        internal static StructureDefinition GetStructureDefinitionForTypeCode(this IResourceResolver resolver, Code<FHIRDefinedType> typeCodeElement)
        {
            StructureDefinition sd = null;
            var typeCode = typeCodeElement.Value;
            if (typeCode.HasValue)
            {
                sd = resolver.FindStructureDefinitionForCoreType(typeCode.Value);
            }
            else
            {
                // Unknown/custom core type; try to resolve from raw object value
                var typeName = typeCodeElement.ObjectValue as string;
                if (!string.IsNullOrEmpty(typeName))
                {
                    sd = resolver.FindStructureDefinitionForCoreType(typeName);
                }
            }
            return sd;
        }

        // [WMR 20170227] NEW
        // TODO:
        // - Analyze possible re-use by Validator
        // - Maybe move this method to another (public) class?

        /// <summary>
        /// Determines if the specified <see cref="StructureDefinition"/> is compatible with <paramref name="type"/>.
        /// Walks up the profile hierarchy by resolving base profiles from the current <see cref="IResourceResolver"/> instance.
        /// </summary>
        /// <returns><c>true</c> if the profile type is equal to or derived from the specified type, or <c>false</c> otherwise.</returns>
        /// <param name="resolver">A resource resolver instance.</param>
        /// <param name="type">A FHIR type.</param>
        /// <param name="profile">A StructureDefinition instance.</param>
        public static bool IsValidTypeProfile(this IResourceResolver resolver, FHIRDefinedType? type, StructureDefinition profile)
        {
            if (resolver == null) { throw new ArgumentNullException(nameof(resolver)); }
            return isValidTypeProfile(resolver, new HashSet<string>(), type, profile);
        }

        static bool isValidTypeProfile(this IResourceResolver resolver, HashSet<string> recursionStack, FHIRDefinedType? type, StructureDefinition profile)
        {
            // Recursively walk up the base profile hierarchy until we find a profile on baseType
            if (type == null) { return true; }
            if (profile == null) { return true; }

            // DSTU2: sd.ConstrainedType is empty for core definitions => resolve from sd.Name
            // STU3: sd.Type is always specified, including for core definitions
            var sdType = profile.ConstrainedType ?? ModelInfo.FhirTypeNameToFhirType(profile?.Name);
            if (sdType == null) { return false; }

            if (sdType == type) { return true; }
            if (profile.Base == null) { return false; }
            var sdBase = resolver.FindStructureDefinition(profile.Base);
            if (sdBase == null) { return false; }
            if (sdBase.Url == null) { return false; } // Shouldn't happen...

            // Detect/prevent endless recursion... e.g. X.Base = Y and Y.Base = X
            if (!recursionStack.Add(sdBase.Url))
            {
                throw Error.InvalidOperation(
                    $"Recursive profile dependency detected. Base profile hierarchy:\r\n{string.Join("\r\n", recursionStack)}"
                );
            }

            return isValidTypeProfile(resolver, recursionStack, type, sdBase);
        }

        // Helper method to retrieve debugger display strings for well-known implementations
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        internal static string DebuggerDisplayString(this IResourceResolver resolver)
        {
            if (resolver is DirectorySource ds) { return ds.DebuggerDisplay; }
            if (resolver is ZipSource zs) { return zs.DebuggerDisplay; }
            // if (resolver is WebResolver wr) { return wr.DebuggerDisplay; }
            if (resolver is MultiResolver mr) { return mr.DebuggerDisplay; }
            if (resolver is CachedResolver cr) { return cr.DebuggerDisplay; }
            if (resolver is SnapshotSource ss) { return ss.DebuggerDisplay; }

            return resolver.GetType().Name;
        }
    }
}
