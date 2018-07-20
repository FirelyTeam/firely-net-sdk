/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
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
    public partial class FhirJsonNavigator : ISourceNavigator, IAnnotated, IExceptionSource, IExceptionSink
    {
        internal FhirJsonNavigator(JObject current, string nodeName, FhirJsonNavigatorSettings settings = null)
        {
            _siblings = new[] { new JsonNavigatorNode(nodeName, null, current, this, false) };
            _index = 0;
            _nameIndex = 0;
            _parentPath = null;

            Sink = settings?.Sink;
            AllowJsonCommments = settings?.AllowJsonComments ?? false;
        }

        internal JsonNavigatorNode Current => _siblings[_index];

        private FhirJsonNavigator() { }         // for Clone()

        public ISourceNavigator Clone()
        {
            var copy = new FhirJsonNavigator()
            {
                _siblings = this._siblings,
                _index = this._index,
                _nameIndex = this._nameIndex,
                _parentPath = this._parentPath,
                Sink = this.Sink,
                AllowJsonCommments = this.AllowJsonCommments
            };

            return copy;
        }

        private JsonNavigatorNode[] _siblings;
        private int _index, _nameIndex;
        private string _parentPath;

        public IExceptionSink Sink { get; set; }
        public bool AllowJsonCommments;

        private void raiseFormatError(string message, JToken node)
        {
            var (lineNumber, linePosition) = getPosition(node);
            Notify(this, ExceptionNotification.Error(Error.Format(message, lineNumber, linePosition)));
        }

        public void Notify(object source, ExceptionNotification args) => Sink.NotifyOrThrow(source, args);

        public string Name => Current.Name;

        public string Text => Current.Text;

        public string Path => _parentPath == null ? Name : $"{_parentPath}.{Current.Name}[{_nameIndex}]";

        private (int lineNumber, int linePosition) getPosition(JToken node)
        {
            if (node is IJsonLineInfo jli)
                return (jli.LineNumber, jli.LinePosition);
            else
                return (-1, -1);
        }

        private int nextMatch(JsonNavigatorNode[] nodes, string namefilter = null, int startAfter = -1)
        {
            for (int scan = startAfter + 1; scan < nodes.Length; scan++)
            {
                if (nodes[scan].Name == "fhir_comments")
                {
                    if(!AllowJsonCommments) raiseFormatError("The 'fhir_comments' feature is not valid in FHIR DSTU2 and later", nodes[scan].PositionNode);
                    continue;      // ignore pre-DSTU2 Json comments
                }
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

            _parentPath = Path;
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
                var (lineNumber, linePosition) = getPosition(Current.PositionNode);

                return new[]
                {
                    new JsonSerializationDetails()
                    {                        
                        OriginalValue = Current.JsonValue?.Value,
                        LineNumber = lineNumber,
                        LinePosition = linePosition,
                        IsArrayElement = Current.IsArrayElement
                    }
                };
            }
            else if (type == typeof(ResourceTypeIndicator))
            {
                return new[]
                {
                    new ResourceTypeIndicator
                    {
                        ResourceType = Current.ResourceType
                    }
                };
            }
            else
                return Enumerable.Empty<object>();
        }
    }

    
    internal static class JTokenExtensions
    {
        public static string GetResourceTypeFromObject(this JObject o)
        {
            var type = o[JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME];

            if (type is JValue typeValue && typeValue.Type == JTokenType.String)
                return (string)typeValue.Value;
            else
                return null;
        }
    }

}


