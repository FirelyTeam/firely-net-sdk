/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.ElementModel
{
    public class PocoElementNavigator
    {
        private Base _parent;

        private int? _arrayIndex; // this is only for the ShortPath implementation eg Patient.Name[1].Family (its the 1 here)
        private int _index;
        private IList<ElementValue> _children;

        // For Normal element properties representing a FHIR type
        internal PocoElementNavigator(Base parent)
        {
            // The root is the special case, we start with a "collection" of children where the parent is the only element
            _parent = null;
            _index = 0;
            _arrayIndex = null;
            _children = new List<ElementValue>() { new ElementValue(parent.TypeName, false, parent) };
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
            result._children = this._children;

            return result;
        }


        private bool enter(Base target)
        {
            var children = target.NamedChildren.ToList();
            if (!children.Any()) return false;

            _parent = target;
            _children = children;

            // Reset everything, next() will initialize the values for the first "child"
            _index = -1;
            _arrayIndex = null;

            return true;
        }


        private bool next(string name = null)
        {
            // If not a collection, or out of collection members, scan
            // for next property
            var scan = _index;

            while (scan + 1 < _children.Count)
            {
                var oldElementName = scan >= 0 ? _children[scan].ElementName : null;

                scan += 1;
                var scanProp = _children[scan];

                if (oldElementName != scanProp.ElementName)
                    _arrayIndex = null;

                if (name == null || scanProp.ElementName == name)
                {
                    _index = scan;

                    if (!scanProp.IsCollectionMember)
                        _arrayIndex = null;
                    else
                        _arrayIndex = _arrayIndex == null ? 0 : _arrayIndex + 1;

                    return true;
                }
            }

            return false;
        }

        internal ElementValue Current => _children[_index];

        public string Name => Current.ElementName;

        public bool AtCollection => Current.IsCollectionMember;

        public int ArrayIndex => AtCollection ? _arrayIndex.Value : 0;

        /// <summary>
        /// This is only needed for search data extraction (and debugging)
        /// to be able to read the values from the selected node (if a coding, so can get the value and system)
        /// </summary>
        public Base FhirValue => Current.Value as Base;    // conversion will return null if on id, value, url (primitive attribute props in xml)

        public object Value
        {
            get
            {
                if (Current.Value is string)
                    return Current.Value;

                try
                {
                    switch (Current.Value)
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
                    return (Current.Value as Primitive)?.ObjectValue;
                }
            }
        }

        private static string[] dstu2quantitySubtypes = { "SimpleQuantity", "Age", "Count", "Distance", "Duration", "Money" };
        private static string[] stu3quantitySubtypes = { "SimpleQuantity" };

        public string TypeName
        {
            get
            {
                if (Current.Value is string)
                {
                    if (Name == "url")
                        return "uri";
                    else if (Name == "id")
                        return "id";
                    else if (Name == "div")
                        return "xhtml";
                    else if (Name == "fhir_comments")
                        return "string";
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
                if (!(Current.Value is Base b)) return false;

                if (enter(b))
                    return next(name);
                else
                    return false;
            }
        }

        public bool MoveToNext(string name = null)
        {
            return next(name);
        }
    }
}