using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public class MappedModelClass
    {
        internal const string RESOURCENAME_SUFFIX = "Resource";

        public FhirModelConstruct ModelConstruct;

        public string Name { get; set; }

        public string Profile { get; set; }
        public Type ImplementingType { get; set; }

        public static MappedModelClass ForResource(Type t)
        {
            var result = new MappedModelClass();
            result.ModelConstruct = FhirModelConstruct.Resource;
            result.Name = getMappedResourceName(t);
            result.Profile = getProfile(t);
            result.ImplementingType = t;

            return result;
        }

        private static string getProfile(Type type)
        {
            var attr = (FhirResourceAttribute)Attribute.GetCustomAttribute(type, typeof(FhirResourceAttribute));

            return attr != null ? attr.Profile : null;
        }

        private static string getMappedResourceName(Type type)
        {
            var attr = (FhirResourceAttribute)Attribute.GetCustomAttribute(type, typeof(FhirResourceAttribute));

            // The abstract base-class has a Resource attribute too, calling it "resource"
            // avoid this becoming the name, since that class is abstract.
            if (attr != null && attr.Name != "Resource")
            {
                return attr.Name;
            }                
            else
            {
                var name = type.Name;
                if (name.EndsWith(RESOURCENAME_SUFFIX))
                    name = name.Substring(0, name.Length - RESOURCENAME_SUFFIX.Length);

                return name;
            }
        }

    }
}
