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

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This class wraps an IElementNavigator to implement IFhirReader. This is a temporary solution to use IElementNavigator
    /// with the POCO-parsers.
    /// </summary>
#pragma warning disable 612,618
    internal class JsonNavFhirReader : IFhirReader
#pragma warning restore 612,618
    {
        private IElementNavigator _current;

        public JsonNavFhirReader(IElementNavigator root)
        {
            _current = root;
        }


        public object GetPrimitiveValue()
        {
            return _current.Value;
        }

        public string GetResourceTypeName()
        {
            if (_current.Type != null) return _current.Type;

            throw Error.Format("Cannot determine type of resource to create from json input data. " + 
                                $"Is there a member '{JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME}' on the root?", this);
        }


#pragma warning disable 612, 618
        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            if (_current.Value != null)
                yield return Tuple.Create("value", (IFhirReader)new JsonNavFhirReader(_current));

            var children = _current.Children();
            foreach (var child in _current.Children())
                yield return Tuple.Create(child.Name, (IFhirReader)new JsonNavFhirReader(child));
        }
#pragma warning restore 612, 618

        public int LineNumber => -1;

        public int LinePosition => -1;
    }
}
