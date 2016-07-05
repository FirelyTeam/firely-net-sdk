/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.FhirPath
{
    [System.Diagnostics.DebuggerDisplay(@"\{Name = {Name} Child.Value = {Child.Value}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public class ChildNode
    {
        public ChildNode(string name, IFhirPathElement child)
        {
            Name = name;
            Child = child;
        }

        public string Name { get; private set; }
        public IFhirPathElement Child { get; private set; }

        private const string POLYMORPHICNAMESUFFIX = "[x]";

        public bool IsMatch(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw Error.ArgumentNull("name"); } // nameof(name)

            return Name == name | isPolymorphicMatch(Name, name);
        }

        private static bool isPolymorphicMatch(string childName, string name)
        {
            if (name.EndsWith(POLYMORPHICNAMESUFFIX))
            {
                var prefixLength = name.Length - POLYMORPHICNAMESUFFIX.Length;
                return String.Compare(childName, 0, name, 0, Math.Max(0, prefixLength)) == 0
                    && isValidTypeName(childName.Substring(prefixLength));
            }
            return false;
        }

        private static bool isValidTypeName(string name)
        {
            // EK: Only way is to use ModelInfo. If you don't want that dependency here, we should move the
            // FHIR extensions to a more "fhiry" place
            return ModelInfo.IsDataType(name) || ModelInfo.IsPrimitive(name.ToLower());
        }

    }

    public interface IFhirPathValue
    {
        object Value { get; }
    }

    public interface IFhirPathElement : IFhirPathValue
    {
        IFhirPathElement Parent { get; }

        IEnumerable<ChildNode> Children();
    }

    public static class FhirValueList
    {
        public static IEnumerable<IFhirPathValue> Create(params object[] values)
        {
            if (values != null)
            {
                return values.Select(value => value == null ? null : value is IFhirPathValue ? (IFhirPathValue)value : new TypedValue(value));
            }
            else
                return FhirValueList.Empty();
        }

        public static IEnumerable<IFhirPathValue> Empty()
        {
            return Enumerable.Empty<IFhirPathValue>();
        }
    }

}