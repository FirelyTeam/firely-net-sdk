/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
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
        private readonly ITypedElement _wrapped;

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
        private readonly ITypedElement _current;
        private readonly ModelInspector _inspector;

        public ParserSettings Settings { get; private set; }

        public ComplexTypeReader(ModelInspector inspector, ITypedElement reader, ParserSettings settings)
        {
            _current = reader;
            _inspector = inspector;
            Settings = settings;
        }

        internal static void RaiseFormatError(string message, string location)
        {
            throw Error.Format("While building a POCO: " + message, location);
        }

        internal Base Deserialize(ClassMapping mapping, Base existing = null)
        {
            var mappingToUse = mapping;

            if (mappingToUse == null)
            {
                if (_current.InstanceType is null)
                    throw Error.Format("Underlying data source was not able to provide the actual instance type of the resource.");

                mappingToUse = (ClassMapping)_inspector.Provide(_current.InstanceType);

                if (mappingToUse == null)
                    RaiseFormatError($"Asked to deserialize unknown type '{_current.InstanceType}'", _current.Location);
            }

            if (existing == null)
            {
                var fac = new DefaultModelFactory();
                existing = (Base)fac.Create(mappingToUse.NativeType);
            }
            else
            {
                if (mappingToUse.NativeType != existing.GetType())
                    throw Error.Argument(nameof(existing), "Existing instance is of type {0}, but data indicates resource is a {1}".FormatWith(existing.GetType().Name, mappingToUse.NativeType.Name));
            }

            // The older code for read() assumes the primitive value member is represented as a separate child element named "value", 
            // while the newer ITypedElement represents this as a special Value property. We simulate the old behaviour here, by
            // explicitly adding the value property as a child and making it act like a typed node.
            var members = _current.Value != null ?
                new[] { new ValuePropertyTypedElement(_current) }.Union(_current.Children()) : _current.Children();

            read(mappingToUse, members, existing);
            return existing;
        }
        private void read(ClassMapping mapping, IEnumerable<ITypedElement> members, Base existing)
        {
            foreach (var memberData in members)
            {
                var memberName = memberData.Name;  // tuple: first is name of member
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

                    var reader = new DispatchingReader(_inspector, memberData, Settings, arrayMode: false);

                    // Since we're still using both ClassMappings and the newer IElementDefinitionSummary provider at the same time, 
                    // the member might be known in the one (POCO), but unknown in the provider. This is only in theory, since the
                    // provider should provide exactly the same information as the mappings. But better to get a clear exception
                    // when this happens.
                    value = reader.Deserialize(mappedProperty, memberName, value);

                    if (mappedProperty.RepresentsValueElement && mappedProperty.ImplementingType.IsEnum() && value is String)
                    {
                        if (!Settings.AllowUnrecognizedEnums)
                        {
                            if (EnumUtility.ParseLiteral((string)value, mappedProperty.ImplementingType) == null)
                                RaiseFormatError($"Literal '{value}' is not a valid value for enumeration '{mappedProperty.ImplementingType.Name}'", _current.Location);
                        }

                        ((PrimitiveType)existing).ObjectValue = value;
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
