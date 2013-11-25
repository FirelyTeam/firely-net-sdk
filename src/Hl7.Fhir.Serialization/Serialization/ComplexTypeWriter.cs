using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Properties;
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
        private IFhirWriter _current;
        private ModelInspector _inspector;
        private bool _forResource = false;

        public ComplexTypeWriter(IFhirWriter writer, bool forResource = false)
        {
            _current = writer;
            _inspector = SerializationConfig.Inspector;
            _forResource = forResource;
        }

        internal void Serialize(ClassMapping mapping, object instance)
        {
            if (mapping == null) throw Error.ArgumentNull("mapping");

            _current.WriteStartComplexContent();

            if (_forResource)
                _current.EmitResourceTypeName(mapping.Name);

            // Emit members that need xml attributes first (to facilitate stream writer API)
            var withAttr = mapping.PropertyMappings.Where(pm => pm.SerializationHint == XmlSerializationHint.Attribute);
            foreach (var prop in mapping.PropertyMappings.Where(pm => pm.SerializationHint == XmlSerializationHint.Attribute))
                write(mapping, instance, prop);

            // Then emit the rest
            foreach (var prop in mapping.PropertyMappings.Where( pm => pm.SerializationHint != XmlSerializationHint.Attribute))
                write(mapping, instance, prop);

            _current.WriteEndComplexContent();
        }

        private void write(ClassMapping mapping, object instance, PropertyMapping prop)
        {
            var value = prop.GetValue(instance);
            var isEmptyArray = (value as IList) != null && ((IList)value).Count == 0;

            Message.Info("Handling member {0}.{1}", mapping.Name, prop.Name);

            if (value != null && !isEmptyArray)
            {
                string memberName = prop.Name;

                // For Choice properties, determine the actual name of the element using
                // by appending its type to the base property name (i.e. deceasedBoolean, deceasedDate)
                if (prop.HasChoices && !prop.HasAnyResourceWildcard)
                    memberName = determineElementMemberName(prop.Name, value.GetType());

                if (prop.IsCollection)
                    _current.WriteStartArray(memberName);
                else if (!prop.IsPrimitive)
                    _current.WriteStartMember(memberName);

                var writer = new DispatchingWriter(_current);
                writer.Serialize(prop, memberName, value);

                if (prop.IsCollection)
                    _current.WriteEndArray();
                else if (!prop.IsPrimitive)
                    _current.WriteEndMember();
            }
        }


        private string determineElementMemberName(string memberName, Type type)
        {
            var mapping = _inspector.ImportType(type);

            var suffix = mapping.Name;
            if (suffix == "ResourceReference") suffix = "Resource";

            return memberName + mapping.Name;
        }
    }
}
