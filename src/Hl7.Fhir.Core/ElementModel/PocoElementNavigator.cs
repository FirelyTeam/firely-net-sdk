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
using Hl7.Fhir.Serialization;
using System.Collections;

namespace Hl7.Fhir.ElementModel
{
    public class PocoElementNavigator
    {
        private Base _parent;

        private int _arrayIndex; // this is only for the ShortPath implementation eg Patient.Name[1].Family (its the 1 here)
        private int _index;
        private IList<Introspection.PropertyMapping> _properties;
        private object _propValue;      // value of the property indexed by _index, may be an array
        private object _currentValue;    // current value, often equal to _propValue, except when _propValue is a list

        // For Normal element properties representing a FHIR type
        internal PocoElementNavigator(Base parent)
        {
            // The root is the special case, we start with a "collection" of children where the parent is the only element
            _parent = null;
            _index = 0;
            _arrayIndex = -1;
            _properties = new List<Introspection.PropertyMapping>()
            {
                new Introspection.PropertyMapping()
                {
                    Name = parent.TypeName,
                    IsCollection = false
                }
            };
            _propValue = parent;
            _currentValue = parent;
        }

        private PocoElementNavigator()
        {
            // only use for clone
        }

        internal PocoElementNavigator Clone()
        {
            var result = new PocoElementNavigator();

            result._parent = this._parent;
            result._arrayIndex = this._arrayIndex;
            result._index = this._index;
            result._properties = this._properties;
            result._propValue = this._propValue;
            result._currentValue = this._currentValue;

            return result;
        }


        private void enter(Base target)
        {
            //[20160620 EK] There's a bug here, when the target has no children, the internal
            //state of the navigator will have changed. This bug does not surface since all the
            //navigator extension methods will clone() before they more, but still, it should be
            //fixed by the time we have used the (generated) Children() method on the model.
            _parent = target;

            var type = target.GetType();
            var mapping = GetMappingForType(type) ??
                        throw Error.NotSupported($"Could not get POCO to FHIR mapping for type '{type.Name}'");

            _properties = mapping.PropertyMappings;

            // Reset everything, next() will initialize the values for the first "child"
            _index = -1;
            _arrayIndex = -1;
            _propValue = null;
            _currentValue = null;
        }

        private bool next(string name = null)
        {
            // If we are at a collection, fully enumerate its members first
            if (_arrayIndex != -1)
            {
                var list = (System.Collections.IList)_propValue;

                if(_arrayIndex + 1 < list.Count)
                {
                    _arrayIndex += 1;
                    _currentValue = list[_arrayIndex];
                    return true;

                    // _propValue and _index remain unchanged
                }
            }

            // If not a collection, or out of collection members, scan
            // for next property
            var scan = _index;

            while (scan+1 < _properties.Count)
            {
                scan += 1;
                var scanProp = _properties[scan];

                // Don't expose "value" as a child, that's our ValueProvider.Value (if we're a primitive) 
                if (isPrimitiveValueProp(scanProp)) continue;

                if (name == null || scanProp.Name == name)
                {
                    var tempValue = getNonEmptyValue(scanProp);

                    if (tempValue != null)
                    {
                        _index = scan;
                        _propValue = tempValue;
                        
                        if (!scanProp.IsCollection)
                        {
                            _arrayIndex = -1;
                            _currentValue = _propValue;
                            return true;
                        }
                        else
                        { 
                            _arrayIndex = 0;
                            _currentValue = ((IList)_propValue)[0];
                            return true;
                        }                          
                    }
                }
            }

            return false;

            object getNonEmptyValue(Introspection.PropertyMapping p)
            {
                var value = p.GetValue(_parent);

                if (value == null) return null;
                if (value is IList list && list.Count == 0) return null;

                return value;
            }

            bool isPrimitiveValueProp(Introspection.PropertyMapping p) => p.IsPrimitive && p.Name == "value";
        }

        public string Name => Prop.Name;

        internal Introspection.PropertyMapping Prop => _properties[_index];

        public bool AtCollection => Prop.IsCollection;

        public int ArrayIndex => AtCollection ? _arrayIndex : 0;

        /// <summary>
        /// This is only needed for search data extraction (and debugging)
        /// to be able to read the values from the selected node (if a coding, so can get the value and system)
        /// </summary>
        public Base FhirValue => _currentValue as Base;    // conversion will return null if on id, value, url (primitive attribute props in xml)

        public object Value
        {
            get
            {
                if (_currentValue is string)
                    return _currentValue;

                try
                {
                    switch (_currentValue)
                    {
                        case Hl7.Fhir.Model.Instant ins:
                            return ins.ToPartialDateTime();
                        case Hl7.Fhir.Model.Time time:
                            return time.ToTime();
                        case Hl7.Fhir.Model.Date dt:
                            return dt.ToPartialDateTime();
                        case FhirDateTime fdt:
                            return fdt.ToPartialDateTime();
                        case Hl7.Fhir.Model.Integer fint:
                            return (long)fint.Value;
                        case Hl7.Fhir.Model.PositiveInt pint:
                            return (long)pint.Value;
                        case Hl7.Fhir.Model.UnsignedInt unsint:
                            return (long)unsint.Value;
                        case Hl7.Fhir.Model.Base64Binary b64:
                            return b64.Value != null ? PrimitiveTypeConverter.ConvertTo<string>(b64.Value) : null;
                        case Primitive prim:
                            return prim.ObjectValue;
                        default:
                            return null;
                    }
                }
                catch (FormatException)
                {
                    // If it fails, just return the unparsed shit
                    // Todo: add sentinel class!
                    return (_currentValue as Primitive)?.ObjectValue;
                }
            }
        }

        private static string[] dstu2quantitySubtypes = { "SimpleQuantity", "Age", "Count", "Distance", "Duration", "Money" };
        private static string[] stu3quantitySubtypes = { "SimpleQuantity" };

        public string TypeName
        {
            get
            {
                if (_currentValue is string)
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
                    // _currentValue must now be of type Base....
                    var tn = FhirValue.TypeName;
                    if (dstu2quantitySubtypes.Contains(tn)) tn = "Quantity";

                    return tn;
                }
            }
        }

        private object lockObject = new object();

        static Hl7.Fhir.Introspection.ClassMapping GetMappingForType(Type elementType)
        {
            var inspector = Serialization.BaseFhirParser.Inspector;
            return inspector.ImportType(elementType);
        }

        public bool MoveToFirstChild(string name = null)
        {
            lock (lockObject)
            {
                // If this is a primitive, there are no children
                if (!(_currentValue is Base b)) return false;

                enter(b);
                return next(name);
            }
        }

        public bool MoveToNext(string name = null)
        {
            return next(name);
        }
    }
}