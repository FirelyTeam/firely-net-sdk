/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Utility;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using System.Linq;

namespace Hl7.Fhir.ElementModel.Adapters
{
    /// <summary>
    /// This class wraps an ISourceNode to implement IFhirReader. This is a temporary solution to use ISourceNode
    /// with the POCO-parsers.
    /// </summary>
#pragma warning disable 612, 618
    internal struct ElementNavToFhirReaderAdapter : IFhirReader, IAnnotated
#pragma warning restore 612,618
    {
        private IElementNavigator _current;

        public ElementNavToFhirReaderAdapter(IElementNavigator root)
        {
            _current = root;

            var dummy = root.Value;   // trigger format exceptions before we continue
        }

        public int LineNumber => getPositionInfo(_current)?.LineNumber ?? -1;

        public int LinePosition => getPositionInfo(_current)?.LinePosition ?? -1;

        private static IPositionInfo getPositionInfo(IElementNavigator node) =>
            node is IAnnotated ia ?
                (IPositionInfo)ia.Annotation<XmlSerializationDetails>() ??
                    (IPositionInfo)ia.Annotation<JsonSerializationDetails>() : null;

        public object GetPrimitiveValue() => Value;

        public string GetResourceTypeName() => _current.GetResourceType() ??
            throw Error.Format($"Cannot retrieve type of resource for element '{Name}' from the underlying navigator.", this);

#pragma warning disable 612, 618
        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            if (Value != null)
                yield return Tuple.Create("value", (IFhirReader)new ElementNavToFhirReaderAdapter(_current));

            foreach (var child in _current.Children())
            {
                var newChild = new ElementNavToFhirReaderAdapter(child);
                yield return Tuple.Create(newChild.Name, (IFhirReader)newChild);
            }
        }
#pragma warning restore 612, 618

        public string Name
        {
            get
            {
                var typeInfo = _current.GetElementDefinitionSummary();

                return typeInfo?.IsChoiceElement == true ?
                    _current.Name + _current.Type.Capitalize() : _current.Name;
            }
        }

        public object Value => _current.Value == null ? null :
            PrimitiveTypeConverter.ConvertTo<string>(_current.Value);

        public IEnumerable<object> Annotations(Type type)
        {
            if (_current is IAnnotated annotated)
                return annotated.Annotations(type);
            else
                return Enumerable.Empty<object>();
        }
    }
}
