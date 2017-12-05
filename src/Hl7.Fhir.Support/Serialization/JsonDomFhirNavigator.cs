/*  
* Copyright (c) 2017, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct JsonDomFhirNavigator : IElementNavigator, IAnnotated, IPositionInfo
    {
        internal JsonDomFhirNavigator(string root, JObject current)
        {
            _siblings = new[] { new JsonNavigatorNode(root, current) };
            _index = 0;
            _nameIndex = 0;
            _parentPath = null;
        }


        internal JsonNavigatorNode Current => _siblings[_index];

        public IElementNavigator Clone()
        {
            var copy = new JsonDomFhirNavigator()
            {
                _siblings = this._siblings,
                _index = this._index,
                _nameIndex = this._nameIndex,
                _parentPath = this._parentPath
            };

            return copy;
        }

        private JsonNavigatorNode[] _siblings;
        private int _index, _nameIndex;
        private string _parentPath;

        public string Name => Current.Name;

        public string Type => Current.Type;

        public object Value => Current.Value;

        public string Location
        {
            get
            {
                if (_parentPath == null)
                    return Current.Name;
                else
                    return $"{_parentPath}.{Current.Name}[{_nameIndex}]";
            }
        }

        public int LineNumber => Current.LineNumber;

        public int LinePosition => Current.LinePosition;

        private int nextMatch(JsonNavigatorNode[] nodes, string namefilter = null, int startAfter = -1)
        {
            for (int scan = startAfter + 1; scan < nodes.Length; scan++)
            {
                if (namefilter == null || nodes[scan].Name == namefilter || nodes[scan].Name == "_" + namefilter)
                    return scan;
            }

            return -1;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var children = Current.GetChildren().ToArray(); 

            if (children.Length == 0) return false;

            var found = nextMatch(children, nameFilter);
            if (found == -1) return false;

            _parentPath = Location;
            _siblings = children;
            _index = found;
            _nameIndex = 0;

            return true;
        }

        public bool MoveToNext(string nameFilter = null)
        {
            var found = nextMatch(_siblings, nameFilter, _index);

            if (found == -1) return false;

            var currentName = Name;
            _index = found;

            if (currentName == Name)
                _nameIndex += 1;
            else
                _nameIndex = 0;

             return true;
        }
       
        public override string ToString()
        {
            return Current.ToString();
        }

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(JsonSerializationDetails))
            {
                return new[]
                {
                    new JsonSerializationDetails()
                    {
                        RawValue = Current.JsonValue?.Value,
                        LineNumber = Current.LineNumber,
                        LinePosition = Current.LinePosition
                    }
                };
            }
            else
                return null;
        }
    }

    
    internal static class JTokenExtensions
    {
        public static string GetCoreTypeFromObject(this JObject o)
        {
            var type = o[JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME];

            if (type is JValue typeValue && typeValue.Type == JTokenType.String)
                return (string)typeValue.Value;
            else
                return null;
        }
    }

}


