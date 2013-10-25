using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    internal static class ReflectionHelper
    {
        internal static IDictionary<string,PropertyInfo> FindPublicProperties(Type t)
        {
            if(t == null) Error.ArgumentNull("t");

            var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var result = new Dictionary<string,PropertyInfo>();
            foreach (var prop in props)
                result.Add(prop.Name.ToUpperInvariant(), prop);

            return result;
        }
    }
}
