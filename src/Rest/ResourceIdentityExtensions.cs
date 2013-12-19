using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Rest
{
    public static class ResourceIdentityExtensions
    {
        // todo: dit is losgetrokken uit ResourceLocation. Maar nog niet af.

        public static string GetCollectionName(this Resource resource)
        {
            return (resource != null) 
                ? GetCollectionName(resource.GetType())
                : null;
        }

        public static string GetCollectionName(this Type type)
        {
            if (typeof(Resource).IsAssignableFrom(type))
                return ModelInfo.GetResourceNameForType(type);
            else
                throw new ArgumentException(String.Format(
                    "Cannot determine collection name, type {0} is not a resource type", type.Name));
        }

        public static string GetCollectionName(this ResourceType t)
        {
            return t.ToString().ToLower();
        }
    }
}