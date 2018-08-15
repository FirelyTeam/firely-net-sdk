/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
#pragma warning disable 612,618
    internal class ComplexTypeReader
    {
        private ISourceNode _current;
        private ModelInspector _inspector;

        public ParserSettings Settings { get; private set; }

        public ComplexTypeReader(ISourceNode reader, ParserSettings settings)
        {
            _current = reader;
            _inspector = BaseFhirParser.Inspector;
            Settings = settings;
        }

        internal Base Deserialize(Type elementType, Base existing = null)
        {
            var mapping = _inspector.FindClassMappingByType(elementType);

            if (mapping == null)
                throw Error.Format("Asked to deserialize unknown type '" + elementType.Name + "'", _current.Location);

            return Deserialize(mapping, existing);
        }
        
        internal Base Deserialize(ClassMapping mapping, Base existing=null)
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

            var members = _current.Text != null ?
                new[] { SourceNode.Valued("value", _current.Text) }.Union(_current.Children()) :
                _current.Children();

            read(mapping, members, existing);

            return existing;

        }


        private void read(ClassMapping mapping, IEnumerable<ISourceNode> members, Base existing)
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
                            throw Error.Format($"Element '{mappedProperty.Name}' must not repeat", memberData.Location);
                    }

                    var reader = new DispatchingReader(memberData, Settings, arrayMode: false);
                    value = reader.Deserialize(mappedProperty, memberName, value);

                    if (mappedProperty.RepresentsValueElement && mappedProperty.ImplementingType.IsEnum() && value is String)
                    {
                        if (!Settings.AllowUnrecognizedEnums)
                        {
                            if (EnumUtility.ParseLiteral((string)value, mappedProperty.ImplementingType) == null)
                                throw Error.Format("Literal '{0}' is not a valid value for enumeration '{1}'".FormatWith(value, mappedProperty.ImplementingType.Name), _current.Location);
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
                        throw Error.Format("Encountered unknown member '{0}' while de-serializing".FormatWith(memberName), memberData.Location);
                    else
                        Message.Info("Skipping unknown member " + memberName);
                }
            }

        }
    }
#pragma warning restore 612, 618
}
