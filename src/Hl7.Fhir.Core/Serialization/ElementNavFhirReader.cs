/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Hl7.Fhir.Utility;
using Hl7.Fhir.ElementModel;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This class wraps an IElementNavigator to implement IFhirReader. This is a temporary solution to use IElementNavigator
    /// with the POCO-parsers.
    /// </summary>
#pragma warning disable 612, 618
    internal struct ElementNavFhirReader : IFhirReader, IElementNavigator
#pragma warning restore 612,618
    {
        private IElementNavigator _current;

        public ElementNavFhirReader(IElementNavigator root)
        {
            _current = root;
        }

        public int LineNumber => getDetails(_current)?.LineNumber ?? -1;

        public int LinePosition => getDetails(_current)?.LinePosition ?? -1;

        private static XmlSerializationDetails getDetails(IElementNavigator node) =>
            (node as IAnnotated)?.Annotation<XmlSerializationDetails>();

        public object GetPrimitiveValue() => Value;

        public string GetResourceTypeName() => _current.GetResourceType() ??
            throw Error.Format($"Cannot retrieve type of resource for element '{Name}' from the underlying navigator.", this);

        private static XmlSerializationDetails getXmlDetails(IElementNavigator nav)
        {
            if (nav is IAnnotated ia)
                return ia.Annotation<XmlSerializationDetails>();
            else
                return null;
        }


        private static bool errorHandler(object sender, ExceptionRaisedEventArgs a)
        {
            if (a.Severity == ExceptionSeverity.Error)
                throw a.Exception;

            return false;
        }

#pragma warning disable 612, 618
        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            if (Value != null)
                yield return Tuple.Create("value", (IFhirReader)new ElementNavFhirReader(_current));

            using (var curr = _current.Catch(errorHandler))
            {
                foreach (var child in _current.Children())
                {
                    yield return Tuple.Create(child.Name, (IFhirReader)new ElementNavFhirReader(child));
                }
            }
        }
#pragma warning restore 612, 618

      

        #region IElementNavigator members
        public bool MoveToNext(string nameFilter = null) => _current.MoveToNext(nameFilter);

        public bool MoveToFirstChild(string nameFilter = null) => _current.MoveToFirstChild(nameFilter);

        public IElementNavigator Clone() => new ElementNavFhirReader(_current.Clone());

        public string Name => _current.Name;

        public string Type => _current.Type;

        public object Value => _current.Value;

        public string Location => _current.Location;
        #endregion
    }
}
