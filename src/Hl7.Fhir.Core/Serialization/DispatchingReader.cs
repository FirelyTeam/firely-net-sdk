/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.Serialization
{
#pragma warning disable 612,618
    internal class DispatchingReader
    {
        private readonly ITypedElement _current;
        private readonly ModelInspector _inspector;
        private readonly bool _arrayMode;
        private readonly bool _isPrimitive;

        public ParserSettings Settings { get; private set; }

        internal DispatchingReader(ITypedElement data, ParserSettings settings, bool arrayMode, bool isPrimitive = false)
        {
            _current = data;
            _inspector = BaseFhirParser.Inspector;
            _arrayMode = arrayMode;
            _isPrimitive = isPrimitive;

            Settings = settings;
        }
#pragma warning restore 612,618

        public object Deserialize(ITypedElement source, object existing=null)
        {
            if (source.Definition == null) throw Error.ArgumentNull(nameof(source.Definition));
            var prop = source.Definition;
            // ArrayMode avoid the dispatcher making nested calls into the RepeatingElementReader again
            // when reading array elements. FHIR does not support nested arrays, and this avoids an endlessly
            // nesting series of dispatcher calls
            if (!_arrayMode && source.Definition.IsCollection)
            {
                if (existing != null && !(existing is IList) ) throw Error.Argument(nameof(existing), "Can only read repeating elements into a type implementing IList");
                var reader = new RepeatingElementReader(_current, Settings);
                return reader.Deserialize(source, (IList)existing);
            }

            // If this is a primitive type, no classmappings and reflection is involved,
            // just parse the primitive from the input
            // NB: no choices for primitives!
            if(prop.IsPrimitive)
            {
                var reader = new PrimitiveValueReader(_current);
                return reader.Deserialize(prop.ImplementingValueType);
            }

            // A Choice property that contains a choice of any resource
            // (as used in Resource.contained)
            if(prop.IsResource)
            {
                var reader = new ResourceReader(_current, Settings);
                return reader.Deserialize(null);
            }

            if (_current.InstanceType is null)
                throw Error.Format("Underlying data source was not able to provide the actual instance type of the resource.");

            ClassMapping mapping;
            //Code added after first refactoring try. it needs to be sorted out when work is resumed on refactoring
            //ClassMapping mapping = prop.Choice == ChoiceType.DatatypeChoice
            //    ? getMappingForType(prop, memberName, _current.InstanceType)
            //    : _inspector.ImportType(prop.ImplementingType);
            
            // Handle other Choices having any datatype or a list of datatypes
            if (prop.IsChoiceElement)
            {
                // For Choice properties, determine the actual type of the element using
                // the suffix of the membername (i.e. deceasedBoolean, deceasedDate)
                // This function implements type substitution.
                
                mapping = getMappingForType(source);
            }   
            // Else use the actual return type of the property
            else
            {
                mapping = _inspector.ImportType(prop.ImplementingType);
            }

            if (existing != null && !(existing is Resource) && !(existing is Element) ) throw Error.Argument(nameof(existing), "Can only read complex elements into types that are Element or Resource");
            var cplxReader = new ComplexTypeReader(_current, Settings);
            return cplxReader.Deserialize(mapping, (Base)existing);
        }

        private ClassMapping getMappingForType(ITypedElement source)
        {
            ClassMapping result = null;

            // NB: this will return the latest type registered for that name, so supports type mapping/overriding
            // Maybe we should Import the types present on the choice, to make sure they are available. For now
            // assume the caller has Imported all types in the right (overriding) order.
            result = _inspector.FindClassMappingForFhirDataType(source.InstanceType);

            if (result == null)
                throw Error.Format("Encountered polymorph member {0}, which uses unknown datatype {1}".FormatWith(source.Name, source.InstanceType), _current.Location);

            return result;
        }

    }
}
