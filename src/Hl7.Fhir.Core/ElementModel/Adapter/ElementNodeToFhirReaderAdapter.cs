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
    internal class ElementNodeToFhirReaderAdapter : IFhirReader, IAnnotated
#pragma warning restore 612,618
    {
        public readonly ITypedElement Current;

        public ElementNodeToFhirReaderAdapter(ITypedElement root)
        {
            Current = root;

            var dummy = root.Value;   // trigger format exceptions before we continue
        }

        public int LineNumber => getPositionInfo(Current)?.LineNumber ?? -1;

        public int LinePosition => getPositionInfo(Current)?.LinePosition ?? -1;

        private static IPositionInfo getPositionInfo(ITypedElement node) =>
            node is IAnnotated ia ?
                (IPositionInfo)ia.Annotation<XmlSerializationDetails>() ??
                    (IPositionInfo)ia.Annotation<JsonSerializationDetails>() : null;

        public object GetPrimitiveValue() => Value;

        public string GetResourceTypeName() => Current.InstanceType ?? 
            Current.Annotation<IResourceTypeSupplier>()?.ResourceType ??
            throw Error.Format($"Cannot retrieve type of resource for element '{Name}' from the underlying navigator.", this);

#pragma warning disable 612, 618
        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            if (Value != null)
                yield return Tuple.Create("value", (IFhirReader)new ElementNodeToFhirReaderAdapter(Current));

            foreach (var child in Current.Children())
            {
                var newChild = new ElementNodeToFhirReaderAdapter(child);
                yield return Tuple.Create(newChild.Name, (IFhirReader)newChild);
            }
        }
#pragma warning restore 612, 618

        public string Name
        {
            get
            {
                var typeInfo = Current.Definition;

                return typeInfo?.IsChoiceElement == true ?
                    Current.Name + Current.InstanceType.Capitalize() : Current.Name;
            }
        }

        public object Value => Current.Value == null ? null :
            PrimitiveTypeConverter.ConvertTo<string>(Current.Value);

        IEnumerable<object> IAnnotated.Annotations(Type type) => Current.Annotations(type);
    }
}
