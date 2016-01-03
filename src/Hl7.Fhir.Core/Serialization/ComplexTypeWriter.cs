/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.Serialization
{
    internal class ComplexTypeWriter
    {
        private IFhirWriter _writer;
        private ModelInspector _inspector;
 

        internal enum SerializationMode
        {
            AllMembers,
            ValueElement,
            NonValueElements
        }

        public ComplexTypeWriter(IFhirWriter writer)
        {
            _writer = writer;
            _inspector = SerializationConfig.Inspector;
        }

        internal void Serialize(ClassMapping mapping, object instance, bool summary, SerializationMode mode = SerializationMode.AllMembers)
        {
            if (mapping == null) throw Error.ArgumentNull("mapping");

            _writer.WriteStartComplexContent();

            // Emit members that need xml /attributes/ first (to facilitate stream writer API)
            foreach (var prop in mapping.PropertyMappings.Where(pm => pm.SerializationHint == XmlSerializationHint.Attribute))
                if(!summary || prop.InSummary || instance is Bundle || prop.Name == "id") write(mapping, instance, summary, prop, mode);

            // Then emit the rest
            foreach (var prop in mapping.PropertyMappings.Where(pm => pm.SerializationHint != XmlSerializationHint.Attribute))
                if (!summary || prop.InSummary || instance is Bundle || prop.Name == "id") write(mapping, instance, summary, prop, mode);

            _writer.WriteEndComplexContent();
        }

        private void write(ClassMapping mapping, object instance, bool summary, PropertyMapping prop, SerializationMode mode)
        {
            // Check whether we are asked to just serialize the value element (Value members of primitive Fhir datatypes)
            // or only the other members (Extension, Id etc in primitive Fhir datatypes)
            // Default is all
            if (mode == SerializationMode.ValueElement && !prop.RepresentsValueElement) return;
            if (mode == SerializationMode.NonValueElements && prop.RepresentsValueElement) return;

            var value = prop.GetValue(instance);
            var isEmptyArray = (value as IList) != null && ((IList)value).Count == 0;

         //   Message.Info("Handling member {0}.{1}", mapping.Name, prop.Name);

            if (value != null && !isEmptyArray)
            {
                string memberName = prop.Name;

                // For Choice properties, determine the actual name of the element
                // by appending its type to the base property name (i.e. deceasedBoolean, deceasedDate)
                if (prop.Choice == ChoiceType.DatatypeChoice)
                {
                    memberName = determineElementMemberName(prop.Name, GetSerializationTypeForDataTypeChoiceElements(prop, value));
                }

                _writer.WriteStartProperty(memberName);
               
                var writer = new DispatchingWriter(_writer);

                // Now, if our writer does not use dual properties for primitive values + rest (xml),
                // or this is a complex property without value element, serialize data normally
                if(!_writer.HasValueElementSupport || !serializedIntoTwoProperties(prop,value))
                    writer.Serialize(prop, value, summary, SerializationMode.AllMembers);
                else
                {
                    // else split up between two properties, name and _name
                    writer.Serialize(prop,value, summary, SerializationMode.ValueElement);
                    _writer.WriteEndProperty();
                    _writer.WriteStartProperty("_" + memberName);
                    writer.Serialize(prop, value, summary, SerializationMode.NonValueElements);
                }

                _writer.WriteEndProperty();
            }
        }

        private Type GetSerializationTypeForDataTypeChoiceElements( PropertyMapping prop, object value)
        {
            Type serializationType = value.GetType();
            if (!prop.IsPrimitive && false)
            {
#if PORTABLE45
                Type baseType = serializationType.GetTypeInfo().BaseType;
                while (baseType != typeof(Element) && baseType != typeof(object))
                {
                    serializationType = baseType;
                    baseType = baseType.GetTypeInfo().BaseType;
                }
#else
                Type baseType = serializationType.BaseType;
                while (baseType != typeof(Element) && baseType != typeof(object))
                {
                    serializationType = baseType;
                    baseType = baseType.BaseType;
                }
#endif
            }

            return serializationType;
        }


        // If we have a normal complex property, for which the type has a primitive value member...
        private bool serializedIntoTwoProperties(PropertyMapping prop, object instance)
        {
            if (instance is IList)
                instance = ((IList)instance)[0];

            if (!prop.IsPrimitive && prop.Choice != ChoiceType.ResourceChoice)
            {
                var mapping = _inspector.ImportType(instance.GetType());
                return mapping.HasPrimitiveValueMember;
            }
            else
                return false;
        }

        private static string upperCamel(string p)
        {
            if (p == null) return p;

            var c = p[0];

            return Char.ToUpperInvariant(c) + p.Remove(0, 1);
        }

        private string determineElementMemberName(string memberName, Type type)
        {
            var mapping = _inspector.ImportType(type);

            var suffix = mapping.Name;
//            if (suffix == "Reference") suffix = "Resource";

            return memberName + upperCamel(suffix);
        }
    }
}
