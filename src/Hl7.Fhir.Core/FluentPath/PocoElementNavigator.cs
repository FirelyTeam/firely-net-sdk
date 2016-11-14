/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.ElementModel;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.FluentPath
{
    internal class PocoElementNavigator
    {
        static Hl7.Fhir.Introspection.ClassMapping GetMappingForType(Type elementType)
        {
            var inspector = Serialization.BaseFhirParser.Inspector;
            //return inspector.FindClassMappingByType(elementType);
            return inspector.ImportType(elementType);
        }

        // For Normal element properties representing a FHIR type
        internal PocoElementNavigator(string name, Base value)
        {
            if (value == null) throw Error.ArgumentNull("value");

            _pocoElement = value;
            Name = name;
        }


        // For properties representing primitive strings (id, url, div), as
        // rendered as attributes in the xml
        internal PocoElementNavigator(string name, string value)
        {
            if (value == null) throw Error.ArgumentNull("value");

            _string = value;
            Name = name;
        }

        private Base _pocoElement;
        private string _string;

        public string Name { get; private set; }

        /// <summary>
        /// This is only needed for search data extraction (and debugging)
        /// to be able to read the values from the selected node (if a coding, so can get the value and system)
        /// </summary>
        public Base FhirValue
        {
            get
            {
                return _pocoElement;
            }
        }

        public object Value
        {
            get
            {
                if (_string != null)
                    return _string;

                try
                {
                    if (_pocoElement is FhirDateTime)
                        return ((FhirDateTime)_pocoElement).ToPartialDateTime();
                    else if (_pocoElement is Hl7.Fhir.Model.Time)
                        return ((Hl7.Fhir.Model.Time)_pocoElement).ToTime();
                    else if ((_pocoElement is Hl7.Fhir.Model.Date))
                        return (((Hl7.Fhir.Model.Date)_pocoElement).ToPartialDateTime());
                    else if (_pocoElement is Hl7.Fhir.Model.Instant)
                        return ((Hl7.Fhir.Model.Instant)_pocoElement).ToPartialDateTime();
                    else if ((_pocoElement is Integer))
                    {
                        if ((_pocoElement as Integer).Value.HasValue)
                            return (long)(_pocoElement as Integer).Value.Value;
                        return null;
                    }
                    else if ((_pocoElement is PositiveInt))
                    {
                        if ((_pocoElement as Integer).Value.HasValue)
                            return (long)(_pocoElement as PositiveInt).Value.Value;
                        return null;
                    }
                    else if (_pocoElement is Primitive)
                        return ((Primitive)_pocoElement).ObjectValue;
                    else
                        return null;
                }
                catch(FormatException)
                {
                    // If it fails, just return the unparsed shit
                    // Todo: add sentinel class!
                    return (_pocoElement as Primitive)?.ObjectValue;
                }

            }
        }

        private static string[] quantitySubtypes = { "SimpleQuantity", "Age", "Count", "Distance", "Duration", "Money" };

        public string TypeName
        {
            get
            {
#if DEBUGX
                Console.WriteLine("Read TypeName '{0}' for Element '{1}' (value '{2}')".FormatWith(_mapping.Name, Name, Value ?? "(nothing)"));
#endif
                if (_string != null)
                {
                    if (Name == "url")
                        return "uri";
                    else if (Name == "id")
                        return "id";
                    else if (Name == "div")
                        return "xhtml";
                    else
                        throw new NotSupportedException($"Don't know about primitive with name '{Name}'");
                }
                else
                {

                    var tn = _pocoElement.TypeName;
                    if (quantitySubtypes.Contains(tn)) tn = "Quantity";

                    return tn;
                }
            }
        }


        private List<PocoElementNavigator> _children;

        public IEnumerable<PocoElementNavigator> Children()
        {
            // Cache children
            if (_children != null) return _children;

            // If this is a primitive, there are no children
            if (_pocoElement == null) return Enumerable.Empty<PocoElementNavigator>();

            _children = new List<PocoElementNavigator>();

            var mapping = GetMappingForType(_pocoElement.GetType());

            if (mapping == null)
                throw Error.NotSupported(String.Format("Unknown type '{0}' encountered", _pocoElement.GetType().Name));

            foreach (var item in mapping.PropertyMappings)
            {
                // Don't expose "value" as a child, that's our ValueProvider.Value (if we're a primitive)
                if (item.IsPrimitive && item.Name == "value")
                    continue;

                var itemValue = item.GetValue(_pocoElement);

                if (itemValue != null)
                {
                    if (item.IsCollection)
                    {
                        foreach (var colItem in (itemValue as System.Collections.IList))
                        {
                            if (colItem != null)
                            {
                                _children.Add(new PocoElementNavigator(item.Name, (Base)colItem));
                            }
                        }
                    }
                    else
                    {
                        if(itemValue is string)
                            // The special case for the 'url' and 'id' properties, which are primitive strings
                            _children.Add(new PocoElementNavigator(item.Name, (string)itemValue));
                        else
                            _children.Add(new PocoElementNavigator(item.Name, (Base)itemValue));
                    }
                }
            }

            return _children;
        }     
    }
}