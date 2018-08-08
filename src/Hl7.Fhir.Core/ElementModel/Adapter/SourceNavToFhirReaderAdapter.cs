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

namespace Hl7.Fhir.ElementModel.Adapters
{
    /// <summary>
    /// This class wraps an ISourceNode to implement IFhirReader. This is a temporary solution to use ISourceNode
    /// with the POCO-parsers.
    /// </summary>
#pragma warning disable 612, 618
    internal struct SourceNavToFhirReaderAdapter : IFhirReader
#pragma warning restore 612,618
    {
        private ISourceNavigator _current;

        public SourceNavToFhirReaderAdapter(ISourceNavigator root)
        {
            _current = root;

            var dummy = root.Text;   // trigger format exceptions before we continue
        }

        public int LineNumber => getPositionInfo(_current)?.LineNumber ?? -1;

        public int LinePosition => getPositionInfo(_current)?.LinePosition ?? -1;

        private static IPositionInfo getPositionInfo(ISourceNavigator node) =>
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
                yield return Tuple.Create("value", (IFhirReader)new SourceNavToFhirReaderAdapter(_current));

            foreach (var child in _current.Children())
            {
                yield return Tuple.Create(child.Name, (IFhirReader)new SourceNavToFhirReaderAdapter(child));
            }
        }
#pragma warning restore 612, 618

        public string Name => _current.Name;

        public object Value => _current.Text;
    }
}
