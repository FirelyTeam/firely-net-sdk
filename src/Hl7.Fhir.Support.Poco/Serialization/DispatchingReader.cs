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
using Hl7.Fhir.Utility;
using System.Collections;

namespace Hl7.Fhir.Serialization
{
#pragma warning disable 612,618
    internal class DispatchingReader
    {
        private readonly ITypedElement _current;
        private readonly ModelInspector _inspector;
        private readonly bool _arrayMode;

        public ParserSettings Settings { get; private set; }

        internal DispatchingReader(ModelInspector inspector, ITypedElement data, ParserSettings settings, bool arrayMode)
        {
            _current = data;
            _inspector = inspector;
            _arrayMode = arrayMode;

            Settings = settings;
        }
#pragma warning restore 612,618

        public object Deserialize(PropertyMapping prop, string memberName, object existing = null)
        {
            if (prop == null) throw Error.ArgumentNull(nameof(prop));

            // ArrayMode avoid the dispatcher making nested calls into the RepeatingElementReader again
            // when reading array elements. FHIR does not support nested arrays, and this avoids an endlessly
            // nesting series of dispatcher calls
            if (!_arrayMode && prop.IsCollection)
            {
                if (existing != null && !(existing is IList)) throw Error.Argument(nameof(existing), "Can only read repeating elements into a type implementing IList");
                var reader = new RepeatingElementReader(_inspector, _current, Settings);
                return reader.Deserialize(prop, memberName, (IList)existing);
            }

            // If this is a primitive type, no classmappings and reflection is involved,
            // just parse the primitive from the input
            // NB: no choices for primitives!
            if (prop.IsPrimitive)
            {
                var reader = new PrimitiveValueReader(_current);
                return reader.Deserialize(prop.ImplementingType);
            }

            // A Choice property that contains a choice of any resource
            // (as used in Resource.contained)
            if (prop.Choice == ChoiceType.ResourceChoice)
            {
                var reader = new ComplexTypeReader(_inspector, _current, Settings);
                return reader.Deserialize(null);
            }

            if (_current.InstanceType is null)
                ComplexTypeReader.RaiseFormatError(
                    "Underlying data source was not able to provide the actual instance type of the resource.", _current.Location);

            ClassMapping mapping = prop.Choice == ChoiceType.DatatypeChoice
                ? getMappingForType(memberName, _current.InstanceType)
                : _inspector.FindOrImportClassMapping(prop.ImplementingType);

            // Handle other Choices having any datatype or a list of datatypes

            if (existing != null && !(existing is Resource) && !(existing is Element)) throw Error.Argument(nameof(existing), "Can only read complex elements into types that are Element or Resource");
            var cplxReader = new ComplexTypeReader(_inspector, _current, Settings);
            return cplxReader.Deserialize(mapping, (Base)existing);
        }

        private ClassMapping getMappingForType(string memberName, string typeName)
        {
            // NB: this will return the latest type registered for that name, so supports type mapping/overriding
            // Maybe we should Import the types present on the choice, to make sure they are available. For now
            // assume the caller has Imported all types in the right (overriding) order.
            ClassMapping result = _inspector.FindClassMapping(typeName);

            if (result == null)
                ComplexTypeReader.RaiseFormatError(
                    $"Encountered polymorph member {memberName}, which uses unknown datatype {typeName}", _current.Location);

            return result;
        }

    }
}
