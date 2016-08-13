/*
  Copyright (c) 2011-2012, HL7, Inc
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.ElementModel;

namespace Hl7.Fhir.FluentPath
{
    public class ElementNavigator : IValueProvider, ITypeNameProvider
    {
        static Hl7.Fhir.Introspection.ClassMapping GetMappingForType(Type elementType)
        {
            var inspector = Serialization.BaseFhirParser.Inspector;
            return inspector.FindClassMappingByType(elementType);
        }

        // For Normal element properties representing a FHIR type
        internal ElementNavigator(string name, Base value)
        {
            if (value == null) throw new ArgumentNullException("value");

            _pocoElement = value;
            Name = name;
        }


        // For properties representing primitive strings (id, url, div), as
        // rendered as attributes in the xml
        internal ElementNavigator(string name, string value)
        {
            if (value == null) throw new ArgumentNullException("value");

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
                if (_string != null) return _string;

                if (_pocoElement is FhirDateTime)
                    return Hl7.FluentPath.PartialDateTime.FromDateTime(((FhirDateTime)_pocoElement).ToDateTimeOffset());
                else if (_pocoElement is Hl7.Fhir.Model.Time)
                    return Hl7.FluentPath.Time.Parse(((Hl7.Fhir.Model.Time)_pocoElement).Value);
                else if ((_pocoElement is Hl7.Fhir.Model.Date))
                    return Hl7.FluentPath.PartialDateTime.Parse(((Hl7.Fhir.Model.Date)_pocoElement).Value);
                else if (_pocoElement is Hl7.Fhir.Model.Instant)
                {
                    if (!((Hl7.Fhir.Model.Instant)_pocoElement).Value.HasValue)
                        return null;
                    return Hl7.FluentPath.PartialDateTime.Parse(((Hl7.Fhir.Model.Instant)_pocoElement).Value.Value.ToString("O"));
                }
                else if (_pocoElement is Primitive)
                    return ((Primitive)_pocoElement).ObjectValue;
                else
                    return null;
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
                    return "string";
                else
                {

                    var tn = _pocoElement.TypeName;
                    if (quantitySubtypes.Contains(tn)) tn = "Quantity";

                    return tn;
                }
            }
        }


        public ElementNavigator Next { get; private set; }

        private List<ElementNavigator> _children;

        public IEnumerable<ElementNavigator> Children()
        {
            // Cache children
            if (_children != null) return _children;

            // If this is a primitive, there are no children
            if (_pocoElement == null) return Enumerable.Empty<ElementNavigator>();

            _children = new List<ElementNavigator>();

            ElementNavigator previousChild = null;

            var mapping = GetMappingForType(_pocoElement.GetType());

#if !PORTABLE45
            if (mapping == null)
                System.Diagnostics.Trace.WriteLine(String.Format("Unknown type '{0}' encountered", _pocoElement.GetType().Name));
#endif
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
                                _children.Add(new ElementNavigator(item.Name, (Base)colItem));
                                if (previousChild != null)
                                    previousChild.Next = _children.Last();
                                previousChild = _children.Last();
                            }
                        }
                    }
                    else
                    {
                        if(itemValue is string)
                            _children.Add(new ElementNavigator(item.Name, (string)itemValue));
                        else
                            _children.Add(new ElementNavigator(item.Name, (Base)itemValue));

                        if (previousChild != null)
                            previousChild.Next = _children.Last();
                        previousChild = _children.Last();
                    }
                }
            }

            return _children;
        }     
    }
}