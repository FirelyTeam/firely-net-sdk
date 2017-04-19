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
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.FhirPath
{
    public class PocoElementNavigator
    {
        static Hl7.Fhir.Introspection.ClassMapping GetMappingForType(Type elementType)
        {
            var inspector = Serialization.BaseFhirParser.Inspector;
            return inspector.ImportType(elementType);
        }

        // For Normal element properties representing a FHIR type
        internal PocoElementNavigator(string name, Base value)
        {
            _pocoElement = value ?? throw Error.ArgumentNull(nameof(value));
            PropMap = null;
            Name = name;
            _arrayIndex = 0;
        }

        // For Normal element properties representing a FHIR type
        internal PocoElementNavigator(Introspection.PropertyMapping map, Base value, int arrayIndex)
        {
            _pocoElement = value ?? throw Error.ArgumentNull(nameof(value));
            PropMap = map;
            Name = map.Name;
            _arrayIndex = arrayIndex;
        }

        // For properties representing primitive strings (id, url, div), as
        // rendered as attributes in the xml
        internal PocoElementNavigator(Introspection.PropertyMapping map, string value)
        {
            _string = value ?? throw Error.ArgumentNull(nameof(value));
            PropMap = map;
            Name = map.Name;
            _arrayIndex = 0;
        }

        private Base _pocoElement;
        private string _string;
        internal int _arrayIndex; // this is only for the ShortPath implementation eg Patient.Name[1].Family (its the 1 here)

        public string Name { get; private set; }
        public Introspection.PropertyMapping PropMap { get; private set; }

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
                        if ((_pocoElement as PositiveInt).Value.HasValue)
                            return (long)(_pocoElement as PositiveInt).Value.Value;
                        return null;
                    }
                    else if ((_pocoElement is UnsignedInt))
                    {
                        if ((_pocoElement as UnsignedInt).Value.HasValue)
                            return (long)(_pocoElement as UnsignedInt).Value.Value;
                        return null;
                    }
                    else if (_pocoElement is Primitive)
                        return ((Primitive)_pocoElement).ObjectValue;
                    else
                        return null;
                }
                catch (FormatException)
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

        internal Introspection.ClassMapping ClassMapping
        {
            get
            {
                if (_classmapping == null)
                    _classmapping = GetMappingForType(_pocoElement.GetType());
                if (_classmapping == null)
                    throw Error.NotSupported(String.Format("Unknown type '{0}' encountered", _pocoElement.GetType().Name));
                return _classmapping;
            }
        }
        private Introspection.ClassMapping _classmapping;

        private List<PocoElementNavigator> _children;
        private string _nameFilter;
        private List<PocoElementNavigator> _childrenFiltered;

        public IEnumerable<PocoElementNavigator> Children(string nameFilter = null)
        {
            lock (this)
            {
                // Cache children
                if (nameFilter == null)
                {
                    if (_children != null)
                        return _children;
                }
                else
                {
                    if (_childrenFiltered != null && nameFilter == _nameFilter)
                        return _childrenFiltered;
                    _nameFilter = nameFilter;
                    if (_children != null)
                    {
                        _childrenFiltered = _children.Where(c => c.Name == _nameFilter).ToList();
                        return _childrenFiltered;
                    }
                }

                // If this is a primitive, there are no children
                if (_pocoElement == null) return Enumerable.Empty<PocoElementNavigator>();

                List<PocoElementNavigator> list;
                List<Introspection.PropertyMapping> props = null;
                if (nameFilter != null)
                {
                    _childrenFiltered = new List<PocoElementNavigator>();
                    list = _childrenFiltered;
                    props = ClassMapping.PropertyMappings.Where(p => p.Name == _nameFilter).ToList();
                }
                else
                {
                    _children = new List<PocoElementNavigator>();
                    list = _children;
                    props = ClassMapping.PropertyMappings;
                }

                foreach (var item in props)
                {
                    // Don't expose "value" as a child, that's our ValueProvider.Value (if we're a primitive)
                    if (item.IsPrimitive && item.Name == "value")
                        continue;

                    var itemValue = item.GetValue(_pocoElement);

                    if (itemValue != null)
                    {
                        if (item.IsCollection)
                        {
                            int nIndex = 0;
                            foreach (var colItem in (itemValue as System.Collections.IList))
                            {
                                if (colItem != null)
                                {
                                    list.Add(new PocoElementNavigator(item, (Base)colItem, nIndex));
                                    nIndex++;
                                }
                            }
                        }
                        else
                        {
                            if (itemValue is string)
                                // The special case for the 'url' and 'id' properties, which are primitive strings
                                list.Add(new PocoElementNavigator(item, (string)itemValue));
                            else
                                list.Add(new PocoElementNavigator(item, (Base)itemValue, 0));
                        }
                    }
                }
                return list;
            }
        }     
    }
}