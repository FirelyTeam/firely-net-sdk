/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    internal class SerializationUtil
    {
        public const string FHIRNS = "http://hl7.org/fhir";
        public const string RESTPARAM_FORMAT = "_format";
       
        public const string SEARCH_PARAM_ID = "_id";
        public const string SEARCH_PARAM_COUNT = "_count";
        public const string SEARCH_PARAM_INCLUDE = "_include";
        public const string HISTORY_PARAM_SINCE = "_since";
        public const string SEARCH_PARAM_SORT = "_sort";

        public const string HISTORY_PARAM_COUNT = SEARCH_PARAM_COUNT;


        public static ResourceEntry CreateResourceEntryFromId(Uri id)
        {
            // Figure out the resource type from the id
            ResourceIdentity rid = new ResourceIdentity(id);
            
            if (rid.Collection != null)
            {
                var inspector = SerializationConfig.Inspector;
                var classMapping = inspector.FindClassMappingForResource(rid.Collection);
                return ResourceEntry.Create(classMapping.NativeType);                            
            }
            else
                throw Error.Format("BundleEntry's id '{0}' does not specify the type of resource: cannot determine Resource type in parser.", null, id.ToString());

        }

        public static bool UriHasValue(Uri u)
        {
            return u != null && !String.IsNullOrEmpty(u.ToString());
        }


        private static string xValue(XObject elem)
        {
            if (elem == null) return null;

            if (elem is XElement)
                return (elem as XElement).Value;
            if (elem is XAttribute)
                return (elem as XAttribute).Value;

            return null;
        }

        public static string StringValueOrNull(XObject elem)
        {
            string value = xValue(elem);

            return String.IsNullOrEmpty(value) ? null : value;
        }

        public static int? IntValueOrNull(XObject elem)
        {
            string value = xValue(elem);

            return String.IsNullOrEmpty(value) ? (int?)null : Int32.Parse(value);
        }

        public static Uri UriValueOrNull(XObject elem)
        {
            string value = StringValueOrNull(elem);

            return String.IsNullOrEmpty(value) ? null : new Uri(value, UriKind.RelativeOrAbsolute);
        }

        public static Uri UriValueOrNull(JToken attr)
        {
            if (attr == null) return null;

            var value = attr.Value<string>();

            return String.IsNullOrEmpty(value) ? null : new Uri(value, UriKind.RelativeOrAbsolute);
        }

        public static DateTimeOffset? InstantOrNull(XObject elem)
        {
            string value = StringValueOrNull(elem);

            return String.IsNullOrEmpty(value) ? (DateTimeOffset?)null : 
                PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(value);
        }
    }
}
