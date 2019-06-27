/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    internal class ValuePropertyTypedElement : ITypedElement
    {
        private ITypedElement _wrapped;

        public ValuePropertyTypedElement(ITypedElement primitiveElement)
        {
            _wrapped = primitiveElement;
        }

        public string Name => "value";

        public string InstanceType => _wrapped.InstanceType;

        public object Value => _wrapped.Value;

        public string Location => _wrapped.Location;

        public IElementDefinitionSummary Definition => _wrapped.Definition;

        public IEnumerable<ITypedElement> Children(string name = null) => _wrapped.Children(name);
    }

#pragma warning disable 612,618
    internal class ComplexTypeReader
    {
        private ITypedElement _current;
        private ModelInspector _inspector;

        public ParserSettings Settings { get; private set; }

        public ComplexTypeReader(ITypedElement reader, ParserSettings settings)
        {
            _current = reader;
            _inspector = BaseFhirParser.Inspector;
            Settings = settings;
        }

        internal Base Deserialize(Base existing = null)
        {
            if (_current.InstanceType is null)
                throw Error.Format("Underlying data source was not able to provide the actual instance type of the resource.");

            var mapping = _inspector.FindClassMappingByType(_current.InstanceType);

            if (mapping == null)
                RaiseFormatError($"Asked to deserialize unknown type '{_current.InstanceType}'", _current.Location);

            return Deserialize(mapping, existing);
        }

        internal static void RaiseFormatError(string message, string location)
        {
            throw Error.Format("While building a POCO: " + message, location);
        }

        internal Base Deserialize(ClassMapping mapping, Base existing = null)
        {
            if (mapping == null) throw Error.ArgumentNull(nameof(mapping));

            if (existing == null)
            {
                var fac = new DefaultModelFactory();
                existing = (Base)fac.Create(mapping.NativeType);
            }
            else
            {
                if (mapping.NativeType != existing.GetType())
                    throw Error.Argument(nameof(existing), "Existing instance is of type {0}, but data indicates resource is a {1}".FormatWith(existing.GetType().Name, mapping.NativeType.Name));
            }

            // The older code for read() assumes the primitive value member is represented as a separate child element named "value", 
            // while the newer ITypedElement represents this as a special Value property. We simulate the old behaviour here, by
            // explicitly adding the value property as a child and making it act like a typed node.
            var members = _current.Value != null ?
                new[] { new ValuePropertyTypedElement(_current) }.Union(_current.Children()) : _current.Children();

            try
            {
                read(mapping, members, existing);
            }
            catch (StructuralTypeException ste)
            {
                throw Error.Format(ste.Message);
            }

            return existing;

        }

        //this should be refactored into read(ITypedElement parent, Base existing)

        private void read(ClassMapping mapping, IEnumerable<ITypedElement> members, Base existing)
        {
            foreach (var memberData in members)
            {
                var memberName = memberData.Name;  // tuple: first is name of member

                // Find a property on the instance that matches the element found in the data
                // NB: This function knows how to handle suffixed names (e.g. xxxxBoolean) (for choice types).
                var mappedProperty = mapping.FindMappedElementByName(memberName);

                if (mappedProperty != null)
                {
                    //   Message.Info("Handling member {0}.{1}", mapping.Name, memberName);

                    object value = null;

                    // For primitive members we can save time by not calling the getter
                    if (!mappedProperty.IsPrimitive)
                    {
                        value = mappedProperty.GetValue(existing);

                        if (value != null && !mappedProperty.IsCollection)
                            RaiseFormatError($"Element '{mappedProperty.Name}' must not repeat", memberData.Location);
                    }

                    var reader = new DispatchingReader(memberData, Settings, arrayMode: false);

                    // Since we're still using both ClassMappings and the newer IElementDefinitionSummary provider at the same time, 
                    // the member might be known in the one (POCO), but unknown in the provider. This is only in theory, since the
                    // provider should provide exactly the same information as the mappings. But better to get a clear exception
                    // when this happens.
                    value = reader.Deserialize(mappedProperty, memberName, value);

                    if (mappedProperty.RepresentsValueElement && mappedProperty.ElementType.IsEnum() && value is String)
                    {
                        if (!Settings.AllowUnrecognizedEnums)
                        {
                            if (EnumUtility.ParseLiteral((string)value, mappedProperty.ElementType) == null)
                                RaiseFormatError($"Literal '{value}' is not a valid value for enumeration '{mappedProperty.ElementType.Name}'", _current.Location);
                        }

                        ((Primitive)existing).ObjectValue = value;
                        //var prop = ReflectionHelper.FindPublicProperty(mapping.NativeType, "RawValue");
                        //prop.SetValue(existing, value, null);
                        //mappedProperty.SetValue(existing, null);                           
                    }
                    else
                    {
                        mappedProperty.SetValue(existing, value);
                    }
                }
                else
                {
                    if (Settings.AcceptUnknownMembers == false)
                        RaiseFormatError($"Encountered unknown member '{memberName}' while de-serializing", memberData.Location);
                    else
                        Message.Info("Skipping unknown member " + memberName);
                }
            }

        }
    }
#pragma warning restore 612, 618
}
