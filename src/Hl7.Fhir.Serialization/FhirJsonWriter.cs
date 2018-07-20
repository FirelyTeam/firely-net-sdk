/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Support.Model;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonWriterSettings
    {
        public bool IgnoreSourceJsonDetails;
        public bool AllowUntypedSource;
        public bool IncludeUntypedElements;
        public IExceptionSink Sink;
    }

    public static class FhirJsonWriterExtensions
    {
        public static void WriteTo(this IElementNavigator source, JsonWriter destination, FhirJsonWriterSettings settings = null) =>
            new FhirJsonWriter(settings).Write(source, destination);

        public static string ToJson(this IElementNavigator source, FhirJsonWriterSettings settings = null)
            => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings));

        public static byte[] ToJsonBytes(this IElementNavigator source, FhirJsonWriterSettings settings = null)
                => SerializationUtil.WriteJsonToBytes(writer => source.WriteTo(writer, settings));
    }

    public class FhirJsonWriter : IExceptionSource, IExceptionSink
    {
        public bool IgnoreSourceJsonDetails;
        public bool AllowUntypedSource;
        public bool IncludeUntypedMembers;

        public FhirJsonWriter(FhirJsonWriterSettings settings = null)
        {
            IgnoreSourceJsonDetails = settings?.IgnoreSourceJsonDetails ?? false;
            AllowUntypedSource = settings?.AllowUntypedSource ?? false;
            IncludeUntypedMembers = settings?.IncludeUntypedElements ?? false;
            Sink = settings?.Sink;
        }

        public void Write(IElementNavigator source, JsonWriter destination)
        {
            if (source is IExceptionSource)
            {
                using (source.Catch((o, a) => Sink.NotifyOrThrow(o, a)))
                {
                    write();
                }
            }
            else
                write();

            destination.Flush();

            void write()
            {
                var (root, _) = buildNode(source, isPrimitive: false, isResource: true, null);

                if (root == null)
                    root = new JObject();

                root.WriteTo(destination);
            }
        }

        public void Write(ISourceNavigator source, JsonWriter destination)
             => Write(source.AsElementNavigator(), destination);


        private (JToken first, JObject second) buildNode(IElementNavigator node, bool isPrimitive, bool isResource, object value)
        {
            JToken first = value != null ? buildValue(value) : null;
            JObject second = node.HasChildren() ? buildChildren(node) : null;

            var containedResourceType = isResource ? (node.Type ?? node.GetResourceType()) : null;
            if (containedResourceType != null && second != null)
                second.AddFirst(new JProperty(JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME, containedResourceType));

            // If this is a complex type with a value (should not occur)
            // serialize it like a primitive, otherwise, the first member
            // is just the normal content of the complex type (the children)
            if (!isPrimitive && first == null)
            {
                if (first == null)
                {
                    first = second;
                    second = null;
                }
            }

            return (first, second);

            JObject buildChildren(IElementNavigator n)
            {
                var objectWithChildren = new JObject();
                addChildren(n, objectWithChildren);

                if (objectWithChildren.Count == 0)
                    return null;
                else
                    return objectWithChildren;
            }
        }

        private void addChildren(IElementNavigator node, JObject parent)
        {
            foreach (var nameGroup in node.Children().GroupBy(n => n.Name))
            {
                var members = nameGroup.ToList();

                JsonSerializationDetails getSerializationDetails(IElementNavigator n)
                        => IgnoreSourceJsonDetails ? null : n.GetJsonSerializationDetails();

                // serialization info should be the same for each element in an
                // array - but do not explicitly check that
                if (!mustSerializeMember(members[0], out var generalInfo)) break;
                var generalJsonDetails = getSerializationDetails(members[0]);
                bool hasTypeInfo = generalInfo != null;

                // If we have type information, we know whather we need an array.
                // failing that, check whether this is a roundtrip and we have the information
                // about arrays in the serialization deails. Failing that, assume the default:
                // for unknown properties is to use an array - safest bet.
                var needsArray = generalInfo?.MayRepeat ?? generalJsonDetails?.IsArrayElement ?? true;
                var isResource = generalInfo?.IsContainedResource ?? members[0].GetResourceType() != null;
                var isPrimitive = members[0].Type != null ? Primitives.IsPrimitive(members[0].Type) :
                        members.Any(m => m.Value != null);

                var children = members.Select(m =>
                {
                    var details = getSerializationDetails(m);
                    object value = hasTypeInfo ? m.Value : getSerializationDetails(m)?.OriginalValue ?? m.Value;
                    return buildNode(m, isPrimitive, isResource, value);
                }).Where(c => !(c.first == null && c.second == null)).ToList();

                // Don't add empty nodes to the parent
                if (!children.Any()) return;

                var needsMainProperty = children.Any(c => c.first != null);
                var needsShadowProperty = children.Any(c => c.second != null);
                var propertyName = generalInfo?.IsChoiceElement == true ?
                        members[0].Name + members[0].Type.Capitalize() : members[0].Name;

                if (needsMainProperty)
                    parent.Add(new JProperty(propertyName,
                        needsArray ? new JArray(children.Select(c => c.first ?? JValue.CreateNull())) : children[0].first));

                if (needsShadowProperty)
                    parent.Add(new JProperty("_" + propertyName,
                        needsArray ? new JArray(children.Select(c => (JToken)c.second ?? JValue.CreateNull())) : (JToken)children[0].second));
            }
        }

        private JValue buildValue(object value)
        {          
            switch (value)
            {
                case bool b:
                case decimal d:
                case Int32 i32:
                case Int16 i16:
                case ulong ul:
                case long l:
                    return new JValue(value);
                case string s:
                    return new JValue(s.Trim());
                default:
                    return new JValue(PrimitiveTypeConverter.ConvertTo<string>(value));
            }
        }


        private bool mustSerializeMember(IElementNavigator source, out ElementSerializationInfo info)
        {
            info = source.GetSerializationInfo();

            if (info == null && !AllowUntypedSource)
                throw Error.NotSupported("The FhirJsonWriter does not work correctly with an untyped IElementNavigator source. Use the 'AllowUntypedSource' setting to override this behaviour and proceed anyway.");

            if (!AllowUntypedSource && info==null)
            {
                var message = $"Element '{source.Location}' is missing type information.";
                if (IncludeUntypedMembers)
                {
                    Notify(source, ExceptionNotification.Warning(
                        new MissingTypeInformationException(message)));
                    return true;
                }
                else
                {
                    Notify(source, ExceptionNotification.Error(
                        new MissingTypeInformationException(message)));
                    return false;
                }
            }

            return true;
        }

        public IExceptionSink Sink { get; set; }

        public void Notify(object source, ExceptionNotification args) => Sink.NotifyOrThrow(source, args);
    }
}
