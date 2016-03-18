using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.FluentPath
{
    public class ChildNode
    {
        public ChildNode(string name, IFluentPathElement child)
        {
            Name = name;
            Child = child;
        }

        public string Name { get; private set; }
        public IFluentPathElement Child { get; private set; }

        // SEPARATION
        private const string POLYMORPHICNAMESUFFIX = "[x]";

        // In the most recent version of FluentPath, the FHIR-specific [x] has been removed, so this
        // match functionality becomes just a normal string match. Will keep code here until this
        // has finalized
        public bool IsMatch(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw Error.ArgumentNull("name"); } // nameof(name)

            //return Name == name | isPolymorphicMatch(Name, name);
            return Name == name;
        }

        //private static bool isPolymorphicMatch(string childName, string name)
        //{
        //    if (name.EndsWith(POLYMORPHICNAMESUFFIX))
        //    {
        //        var prefixLength = name.Length - POLYMORPHICNAMESUFFIX.Length;
        //        return String.Compare(childName, 0, name, 0, Math.Max(0, prefixLength)) == 0
        //        && isValidTypeName(childName.Substring(prefixLength));
        //    }
        //    return false;
        //}

        //private static bool isValidTypeName(string name)
        //{
        //    // EK: Only way is to use ModelInfo. If you don't want that dependency here, we should move the
        //    // FHIR extensions to a more "fhiry" place
        //    return ModelInfo.IsDataType(name) || ModelInfo.IsPrimitive(name.ToLower());
        //}

    }
}