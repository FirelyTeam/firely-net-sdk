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

namespace Hl7.Fhir.Serialization
{
#pragma warning disable 612,618
    internal class DispatchingReader
    {
        private readonly ISourceNode _current;
        private readonly ModelInspector _inspector;
        private readonly bool _arrayMode;

        public ParserSettings Settings { get; private set; }

        internal DispatchingReader(ISourceNode data, ParserSettings settings, bool arrayMode)
        {
            _current = data;
            _inspector = BaseFhirParser.Inspector;
            _arrayMode = arrayMode;

            Settings = settings;
        }
#pragma warning restore 612,618

        public object Deserialize(PropertyMapping prop, string memberName, object existing=null)
        {
            if (prop == null) throw Error.ArgumentNull(nameof(prop));

            // ArrayMode avoid the dispatcher making nested calls into the RepeatingElementReader again
            // when reading array elements. FHIR does not support nested arrays, and this avoids an endlessly
            // nesting series of dispatcher calls
            if (!_arrayMode && prop.IsCollection)
            {
                if (existing != null && !(existing is IList) ) throw Error.Argument(nameof(existing), "Can only read repeating elements into a type implementing IList");
                var reader = new RepeatingElementReader(_current, Settings);
                return reader.Deserialize(prop, memberName, (IList)existing);
            }

            // If this is a primitive type, no classmappings and reflection is involved,
            // just parse the primitive from the input
            // NB: no choices for primitives!
            if(prop.IsPrimitive)
            {
                var reader = new PrimitiveValueReader(_current);
                return reader.Deserialize(prop.ImplementingType);
            }

            // A Choice property that contains a choice of any resource
            // (as used in Resource.contained)
            if(prop.Choice == ChoiceType.ResourceChoice)
            {
                var reader = new ResourceReader(_current, Settings);
                return reader.Deserialize(null);
            }

            ClassMapping mapping;

            // Handle other Choices having any datatype or a list of datatypes
            if(prop.Choice == ChoiceType.DatatypeChoice)
            {
                // For Choice properties, determine the actual type of the element using
                // the suffix of the membername (i.e. deceasedBoolean, deceasedDate)
                // This function implements type substitution.
                mapping = determineElementPropertyType(prop, memberName);
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

        private ClassMapping determineElementPropertyType(PropertyMapping mappedProperty, string memberName)
        {
            ClassMapping result = null;

            var typeName = mappedProperty.GetChoiceSuffixFromName(memberName);

            if (String.IsNullOrEmpty(typeName))
                throw Error.Format("Encountered polymorph member {0}, but is does not specify the type used".FormatWith(memberName), _current.Location);

            // Exception: valueResource actually means the element is of type ResourceReference
            if (typeName == "Resource") typeName = "Reference";

            // NB: this will return the latest type registered for that name, so supports type mapping/overriding
            // Maybe we should Import the types present on the choice, to make sure they are available. For now
            // assume the caller has Imported all types in the right (overriding) order.
            result = _inspector.FindClassMappingForFhirDataType(typeName);

            if (result == null)
                throw Error.Format("Encountered polymorph member {0}, which uses unknown datatype {1}".FormatWith(memberName, typeName), _current.Location);

            return result;
        }

    }
}
