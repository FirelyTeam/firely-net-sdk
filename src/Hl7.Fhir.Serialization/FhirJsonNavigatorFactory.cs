/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Hl7.Fhir.Serialization
{
    [Obsolete("Please use the equivalent functions on the FhirJsonNavigator factory class")]
    public struct JsonDomFhirNavigator
    {
        [Obsolete("Use FhirJsonNavigator.Untyped() instead")]
        public static ISourceNavigator Create(JObject root, string rootName = null) => FhirJsonNavigator.Untyped(root, rootName);

        [Obsolete("Use FhirJsonNavigator.Untyped() instead")]
        public static ISourceNavigator Create(JsonReader reader, string rootName = null) => FhirJsonNavigator.Untyped(reader, rootName);

        [Obsolete("Use FhirJsonNavigator.Untyped() instead")]
        public static ISourceNavigator Create(string json, string rootName = null) => FhirJsonNavigator.Untyped(json, rootName);
    }

    public partial class FhirJsonNavigator
    {
        public static ISourceNavigator Untyped(JsonReader reader, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            return createUntyped(reader, rootName, settings);
        }

        public static ISourceNavigator Untyped(string json, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (json == null) throw Error.ArgumentNull(nameof(json));

            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                return createUntyped(reader, rootName, settings);
            }
        }

        public static ISourceNavigator Untyped(JObject root, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (root == null) throw Error.ArgumentNull(nameof(root));

            return createUntyped(root, rootName, settings);
        }

        public static IElementNavigator Typed(string json, ISerializationInfoProvider provider, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (json == null) throw Error.ArgumentNull(nameof(json));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                return createTyped(reader, null, rootName, provider, settings);
            }
        }

        public static IElementNavigator Typed(string json, string type, ISerializationInfoProvider provider, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (json == null) throw Error.ArgumentNull(nameof(json));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                return createTyped(reader, type, rootName, provider, settings);
            }
        }

        public static IElementNavigator Typed(JsonReader reader, ISerializationInfoProvider provider, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(reader, null, rootName, provider, settings);
        }

        public static IElementNavigator Typed(JsonReader reader, string type, ISerializationInfoProvider provider, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(reader, type, rootName, provider, settings);
        }

        public static IElementNavigator Typed(JObject root, ISerializationInfoProvider provider, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (root == null) throw Error.ArgumentNull(nameof(root));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(root, null, rootName, provider, settings);
        }

        public static IElementNavigator Typed(JObject root, string type, ISerializationInfoProvider provider, string rootName = null, FhirJsonNavigatorSettings settings = null)
        {
            if (root == null) throw Error.ArgumentNull(nameof(root));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(root, type, rootName, provider, settings);
        }

        public static IElementNavigator Typed(ISourceNavigator untypedNav, string type, ISerializationInfoProvider provider)
        {
            if (untypedNav == null) throw Error.ArgumentNull(nameof(untypedNav));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return untypedNav.AsElementNavigator(type, provider);
        }

        public static IElementNavigator Typed(ISourceNavigator untypedNav, ISerializationInfoProvider provider)
        {
            if (untypedNav == null) throw Error.ArgumentNull(nameof(untypedNav));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return untypedNav.AsElementNavigator(null, provider);
        }


        private static ISourceNavigator createUntyped(JsonReader reader, string rootName, FhirJsonNavigatorSettings settings)
        {
            try
            {
                JObject doc = null;
                doc = SerializationUtil.JObjectFromReader(reader);
                return createUntyped(doc, rootName, settings);
            }
            catch (JsonException jec)
            {
                throw Error.Format("Cannot parse json: " + jec.Message);
            }
        }

        public static ISourceNavigator createUntyped(JObject root, string rootName, FhirJsonNavigatorSettings settings)
        {
            var name = rootName ?? root.GetResourceTypeFromObject();

            if (name == null)
                throw Error.InvalidOperation("Root object has no type indication (resourceType) and therefore cannot be used to construct the navigator. Alternatively, specify a rootName using the parameter.");

            return new FhirJsonNavigator(root, name, settings);

        }

        private static IElementNavigator createTyped(JObject root, string type, string rootName, ISerializationInfoProvider provider, FhirJsonNavigatorSettings settings)
        {
            var untypedNav = createUntyped(root, rootName, settings);
            return untypedNav.AsElementNavigator(type, provider);
        }

        private static IElementNavigator createTyped(JsonReader reader, string type, string rootName, ISerializationInfoProvider provider, FhirJsonNavigatorSettings settings)
        {
            var untypedNav = createUntyped(reader, rootName, settings);
            return untypedNav.AsElementNavigator(type, provider);
        }
    }
}
