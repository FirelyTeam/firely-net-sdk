/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Support.Model;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonWriterSettings
    {
        public bool AllowUntypedElements;
        public bool IncludeUntypedElements;
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

    public class FhirJsonWriter : IExceptionSource
    {
        public FhirJsonWriter(FhirJsonWriterSettings settings = null)
        {
            AllowUntypedElements = settings?.AllowUntypedElements ?? false;
            IncludeUntypedElements = settings?.IncludeUntypedElements ?? false;
        }

        public bool AllowUntypedElements;
        public bool IncludeUntypedElements;
        public ExceptionNotificationHandler ExceptionHandler { get; set; }


        public void Write(IElementNavigator source, JsonWriter destination)
        {
            //Re-enable when the PocoNavigator is also fed through the TypedNavigator
            //if(!source.InPipeline(typeof(TypedNavigator)))
            //    throw Error.NotSupported($"The {nameof(FhirXmlWriter)} requires a {nameof(TypedNavigator)} to be present in the pipeline.");
            writeInternal(source, destination);
        }

        private void writeInternal(IElementNavigator source, JsonWriter destination)
        {
            if (source is IExceptionSource)
            {
                using (source.Catch((o, a) => ExceptionHandler.NotifyOrThrow(o, a)))
                {
                    write();
                }
            }
            else
                write();

            destination.Flush();

            void write()
            {
                var (root, _) = buildNode(source);

                if (root == null)
                    root = new JObject();

                root.WriteTo(destination);
            }
        }


        public void Write(ISourceNavigator source, JsonWriter destination)
        {
            bool hasJsonSource = source.InPipeline(typeof(FhirJsonNavigator));

            // We can only work with an untyped source if we're doing a roundtrip,
            // so we have all serialization details available.
            if (hasJsonSource)
                Write(source.ToElementNavigator(), destination);
            else
                throw Error.NotSupported($"The {nameof(FhirJsonWriter)} will only work correctly on an untyped " +
                    $"source if the source is a {nameof(FhirJsonNavigator)}.");
        }


        private (JToken first, JObject second) buildNode(IElementNavigator node)
        {
            var details = node.GetJsonSerializationDetails();
            object value = details != null ? node.Value : details?.OriginalValue ?? node.Value;
            var isPrimitive = node.Type != null ? Primitives.IsPrimitive(node.Type) : value != null;

            JToken first = value != null ? buildValue(value) : null;
            JObject second = buildChildren(node);

            var edSummary = node.GetElementDefinitionSummary();

            var isResource = edSummary?.IsResource ?? node.GetResourceType() != null;
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

        internal bool MustSerializeMember(IElementNavigator source, out ElementDefinitionSummary info)
        {
            info = source.GetElementDefinitionSummary();

            if (info == null && !AllowUntypedElements)
            {
                var message = $"Element '{source.Location}' is missing type information.";
                if (IncludeUntypedElements)
                {
                    ExceptionHandler.NotifyOrThrow(source, ExceptionNotification.Warning(
                        new MissingTypeInformationException(message)));
                    return true;
                }
                else
                {
                    ExceptionHandler.NotifyOrThrow(source, ExceptionNotification.Error(
                        new MissingTypeInformationException(message)));
                    return false;
                }
            }

            return true;
        }

        private void addChildren(IElementNavigator node, JObject parent)
        {
            foreach (var nameGroup in node.Children().GroupBy(n => n.Name))
            {
                var members = nameGroup.ToList();

                // serialization info should be the same for each element in an
                // array - but do not explicitly check that
                if (!MustSerializeMember(members[0], out var generalInfo)) break;
                bool hasTypeInfo = generalInfo != null;

                // If we have type information, we know whather we need an array.
                // failing that, check whether this is a roundtrip and we have the information
                // about arrays in the serialization deails. Failing that, assume the default:
                // for unknown properties is to use an array - safest bet.
                var generalJsonDetails = members[0].GetJsonSerializationDetails();
                var needsArray = generalInfo?.IsCollection ?? generalJsonDetails?.IsArrayElement ?? true;

                var children = members.Select(m => buildNode(m))
                            .Where(c => !(c.first == null && c.second == null)).ToList();

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
    }
}
