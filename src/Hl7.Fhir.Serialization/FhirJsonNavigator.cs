/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial class FhirJsonNavigator : ISourceNavigator, IAnnotated, IExceptionSource
    {
        internal FhirJsonNavigator(JObject current, string nodeName, FhirJsonNavigatorSettings settings = null)
        {
            _siblings = new[] { new JsonNavigatorNode(nodeName, null, current, this, false) };
            _index = 0;
            _nameIndex = 0;
            _parentPath = null;
            _settings = settings?.Clone();
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
                _settings = _settings,
                ExceptionHandler = this.ExceptionHandler,
            };

            return copy;
        }

        private JsonNavigatorNode[] _siblings;
        private int _index, _nameIndex;
        private string _parentPath;
        private FhirJsonNavigatorSettings _settings;

        public ExceptionNotificationHandler ExceptionHandler { get; set; }
        public bool AllowJsonComments => _settings?.AllowJsonComments ?? false;
        public bool PermissiveParsing => _settings?.PermissiveParsing ?? false;
#if NET_XSD_SCHEMA
        public bool ValidateFhirXhtml => _settings?.ValidateFhirXhtml ?? false;
#endif

        private void raiseFormatError(string message, JToken node)
        {
            var (lineNumber, linePosition) = getPosition(node);
            ExceptionHandler.NotifyOrThrow(this, ExceptionNotification.Error(Error.Format(message, lineNumber, linePosition)));
        }

        public string Name => Current.Name;

        public string Text => Current.Text;

        public string Location => _parentPath == null ? Name : $"{_parentPath}.{Current.Name}[{_nameIndex}]";

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
                    if(!AllowJsonComments && !PermissiveParsing) raiseFormatError("The 'fhir_comments' feature is disabled.", nodes[scan].PositionNode);
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
            if (type == typeof(FhirJsonNavigator))
                return new[] { this };
#pragma warning disable 612, 618
            else if (type == typeof(AdditionalStructuralRule) && !PermissiveParsing)
                return additionalTypeRules();
#pragma warning restore 612, 618
            else if (type == typeof(JsonSerializationDetails))
            {
                var (lineNumber, linePosition) = getPosition(Current.PositionNode);

                return new[]
                {
                    new JsonSerializationDetails()
                    {
                        OriginalValue = Current.JsonValue?.Value,
                        LineNumber = lineNumber,
                        LinePosition = linePosition,
                        ArrayIndex = Current.IsArrayElement ? _nameIndex : (int?)null
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

#pragma warning disable 612, 618
        private IEnumerable<AdditionalStructuralRule> additionalTypeRules()
        {
            yield return checkArrayUse;

#if NET_XSD_SCHEMA
            yield return checkXhtml;

            void checkXhtml(IElementNavigator nav, IExceptionSource ies)
            {
                if (nav.Type != "xhtml") return;

                if (ValidateFhirXhtml)
                    FhirXmlNavigator.ValidateXhtml((string)nav.Value, ies, nav);
            }
#endif

            void checkArrayUse(IElementNavigator nav, IExceptionSource ies)
            {
                var sdSummary = nav.GetElementDefinitionSummary();
                var serializationDetails = nav.GetJsonSerializationDetails();
                if (sdSummary == null || serializationDetails == null) return;

                if (sdSummary.IsCollection && serializationDetails.ArrayIndex == null)
                    ies.ExceptionHandler.NotifyOrThrow(nav, ExceptionNotification.Error(
                        new StructuralTypeException($"Since element '{nav.Name}' repeats, an array must be used here.")));

                if (!sdSummary.IsCollection && serializationDetails.ArrayIndex != null)
                {
                    // only report this once on the first of the group
                    if (serializationDetails.ArrayIndex == 0)
                    {
                        ies.ExceptionHandler.NotifyOrThrow(nav, ExceptionNotification.Error(
                            new StructuralTypeException($"Element '{nav.Name}' does not repeat, so an array must not be used here.")));
                    }
                }
            }
        }
#pragma warning restore 612, 618
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


