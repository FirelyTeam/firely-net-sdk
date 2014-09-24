/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Api.Properties;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.Serialization
{
    public class RepeatingElementReader
    {
        private IFhirReader _current;
        private ModelInspector _inspector;

        public RepeatingElementReader(IFhirReader reader)
        {
            _current = reader;
            _inspector = SerializationConfig.Inspector;
        }

        public object Deserialize(PropertyMapping prop, string memberName, object existing=null)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            if (existing != null && !(existing is IList) ) throw Error.Argument("existing", "Can only read repeating elements into a type implementing IList");

            IList result = existing as IList;

            bool overwriteMode;
            IEnumerable<IFhirReader> elements;

            if(_current.CurrentToken == TokenType.Array)        // Json has members that are arrays, if we encounter multiple, update the old values of the array
            {
                overwriteMode = result != null && result.Count > 0;
                elements = _current.GetArrayElements();
            }
            else if(_current.CurrentToken == TokenType.Object)  // Xml has repeating members, so this results in an "array" of just 1 member
            {
                //TODO: This makes   member : {x} in Json valid too,
                //even if json should have member : [{x}]
                overwriteMode = false;
                elements = new List<IFhirReader>() { _current };
            }
            else
                throw Error.Format("Expecting to be either at a repeating complex element or an array when parsing a repeating member.", _current);

            if (result == null) result = ReflectionHelper.CreateGenericList(prop.ElementType);

            var position = 0;
            foreach(var element in elements)
            {
                var reader = new DispatchingReader(element, arrayMode: true);

                if(overwriteMode)
                {
                    if (position >= result.Count)
                        throw Error.Format("The value and extension array are not well-aligned", _current);

                    // Arrays may contain null values as placeholders
                    if(element.CurrentToken != TokenType.Null)
                        result[position] = reader.Deserialize(prop, memberName, existing: result[position]);
                }
                else
                {                   
                    object item = null;
                    if (element.CurrentToken != TokenType.Null)
                        item = reader.Deserialize(prop, memberName);
                    else
                        item = null;  // Arrays may contain null values as placeholders
                    
                    result.Add(item);
                }

                position++;
            }

            return result;
        }
    }
}
