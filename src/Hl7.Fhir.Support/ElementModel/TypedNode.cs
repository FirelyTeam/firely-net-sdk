/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Support.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public class TypedNode : IElementNode, IAnnotated, IExceptionSource
    {
        public TypedNode(ISourceNode element, IStructureDefinitionSummaryProvider provider)
                    : this(element, null, provider)
        {
            //
        }

        public TypedNode(ISourceNode element, string type, IStructureDefinitionSummaryProvider provider)
        {
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            if (element == null) throw Error.ArgumentNull(nameof(element));

            // All this stuff will break if there is an error, no type or the type cannot be provided,
            // need to figure out how to delay processing
            //using (sourceNav.Catch((o, a) => hasError = a.Exception is FormatException))
            //{
            var dummy = element.Text;         // trigger format exception for now.
            //}

            var rootType = type ?? element.GetResourceType() ??
                    throw Error.Argument(nameof(element), "Underlying navigator is not located on a resource, please supply a type argument");

            var elementType = provider.Provide(rootType);

            if (elementType == null)
                throw Error.Argument(nameof(element), $"Cannot locate type information for type '{rootType}'");

            _current = NavigatorPosition.ForRoot(element, elementType, element.Name);
            PrettyPath = _current.Name;

            Provider = provider;

            if (element is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private TypedNode(TypedNode parent, NavigatorPosition position, string prettyPath)
        {
            _current = position;
            PrettyPath = prettyPath;
            Provider = parent.Provider;
            ExceptionHandler = parent.ExceptionHandler;
        }


        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        private void reportUnknownType(string typeName, ISourceNode position)
        {
            raiseTypeError($"Encountered unknown type '{typeName}'", position);
        }
        private void verifyElementTrackingStatus(NavigatorPosition current, ElementDefinitionSummaryCache definition)
        {
            // If we found a type, but we don't know about the specific child, complain
            if (definition != ElementDefinitionSummaryCache.Empty && !current.IsTracking)
                raiseTypeError($"Encountered unknown element '{current.Name}'", current.Node);
        }

        private void raiseTypeError(string message, ISourceNode current) =>
            ExceptionHandler.NotifyOrThrow(current, ExceptionNotification.Error(
                typeException(message, current)));

        private static StructuralTypeException typeException(string message, ISourceNode position)
        {
            if (position != null)
                message += $" (at {position.Location})";

            return new StructuralTypeException(message);
        }

        private NavigatorPosition _current;

        public IStructureDefinitionSummaryProvider Provider { get; private set; }

        public string Name => _current.Name;

        public string Type => _current.InstanceType;

        public object Value
        {
            get
            {
                string sourceText = _current.Node.Text;

                if (sourceText == null) return null;

                // If we don't have type information (no definition was found
                // for current node), all we can do is return the underlying string value
                if (!_current.IsTracking || _current.InstanceType == null) return sourceText;

                if (!Primitives.IsPrimitive(Type))
                {
                    raiseTypeError($"Since type {Type} is not a primitive, it cannot have a value", _current.Node);
                    return null;
                }

                // Finally, we have a (potentially) unparsed string + type info
                // parse this primitive into the desired type
                try
                {
                    return PrimitiveTypeConverter.FromSerializedValue(sourceText, Type);
                }
                catch (FormatException fe)
                {
                    raiseTypeError($"Literal '{sourceText}' cannot be interpreted as a {Type}: '{fe.Message}'.", _current.Node);
                    return sourceText;
                }
            }
        }

        private NavigatorPosition deriveInstanceType(ISourceNode current, IElementDefinitionSummary info)
        {
            if (info == null) return new NavigatorPosition(current, null, current.Name, null);

            string instanceType = null;

            if (info.IsResource)
            {
                instanceType = current.GetResourceType();
                if (instanceType == null) raiseTypeError($"Element '{current.Name}' should contain a resource, but does not actually contain one", current);
            }
            else if (!info.IsResource && current.GetResourceType() != null)
            {
                raiseTypeError($"Element '{current.Name}' is not a contained resource, but seems to contain a resource of type '{current.GetResourceType()}'.", current);
                instanceType = current.GetResourceType();
            }
            else if (info.IsChoiceElement)
            {
                var suffix = current.Name.Substring(info.ElementName.Length);

                if (String.IsNullOrEmpty(suffix))
                {
                    raiseTypeError($"Choice element '{current.Name}' is not suffixed with a type.", current);
                    instanceType = null;
                }
                else
                {
                    instanceType = info.Type.OfType<IStructureDefinitionReference>().Select(t => t.ReferredType).FirstOrDefault(t => String.Compare(t, suffix, StringComparison.OrdinalIgnoreCase) == 0);

                    if (String.IsNullOrEmpty(instanceType))
                        raiseTypeError($"Choice element '{current.Name}' is suffixed with unexpected type '{suffix}'", current);
                }
            }
            else
            {
                var tp = info.Type.Single();
                instanceType = tp.GetTypeName();
            }

            return new NavigatorPosition(current, info, info?.ElementName ?? current.Name, instanceType);
        }

        private IEnumerable<TypedNode> enumerateElements(ElementDefinitionSummaryCache dis, ISourceNode parent, string name)
        {
            IEnumerable<ISourceNode> childSet = null;

            // no name filter: work on all the parent's children
            if (name == null)
                childSet = parent.Children();
            else
            { 
                var hit = dis.TryGetBySuffixedName(name, out var info);
                if (hit && info.IsChoiceElement)
                    childSet = parent.Children(name + "*");
                else
                    childSet = parent.Children(name);
            }

            string lastName = null;
            int _nameIndex = 0;

            foreach (var scan in childSet)
            {
                var hit = dis.TryGetBySuffixedName(scan.Name, out var info);
                var match = deriveInstanceType(scan, info);
                verifyElementTrackingStatus(match, dis);

                if (lastName == scan.Name)
                    _nameIndex += 1;
                else
                {
                    _nameIndex = 0;
                    lastName = scan.Name;
                }

                var prettyPath = 
                 hit && !info.IsCollection ? $"{PrettyPath}.{match.Name}" : $"{PrettyPath}.{match.Name}[{_nameIndex}]";

                yield return new TypedNode(this, match, prettyPath);
            }
        }

        public IEnumerable<IElementNode> Children(string nameFilter = null)
        {
            // Since we don't want to report errors from the constructor, do this work now on
            // the initial call (_prettyPath still on the root)
            if (!PrettyPath.Contains(".") && !_current.IsTracking)
                reportUnknownType(_current.InstanceType, _current.Node);

            var firstChildDef = down(_current);
            if (_current.IsTracking && firstChildDef == ElementDefinitionSummaryCache.Empty && _current.InstanceType != null)
                reportUnknownType(_current.InstanceType, _current.Node);

            return runAdditionalRules(enumerateElements(firstChildDef, _current.Node, nameFilter));
        }

        private ElementDefinitionSummaryCache down(NavigatorPosition current)
        {
            if (!current.IsTracking || current.InstanceType == null) return ElementDefinitionSummaryCache.Empty;

            // If this is a backbone element, the child type is the nested complex type
            if (current.SerializationInfo.Type[0] is IStructureDefinitionSummary be)
                return ElementDefinitionSummaryCache.ForType(be);
            else
            {
                var si = Provider.Provide(current.InstanceType);

                if (si == null)
                    return ElementDefinitionSummaryCache.Empty;
                else
                    return ElementDefinitionSummaryCache.ForType(si);
            }
        }

        private IEnumerable<IElementNode> runAdditionalRules(IEnumerable<IElementNode> children)
        {
#pragma warning disable 612,618
            var additionalRules = _current.Node.Annotations(typeof(AdditionalStructuralRule));
            var stateBag = new Dictionary<AdditionalStructuralRule, object>();
            foreach (var child in children)
            {
                foreach (var rule in additionalRules.Cast<AdditionalStructuralRule>())
                {
                    object state = null;
                    stateBag.TryGetValue(rule, out state);
                    state = rule(child, this, state);
                    if (state != null) stateBag[rule]=state;
                }

                yield return child;
            }
#pragma warning restore 612,618
        }

        public string Location => _current.Node.Location;

        public string PrettyPath { get; set; }

        public override string ToString() => $"{(_current.IsTracking ? ($"[{_current.InstanceType}] ") : "")}{_current.Node.ToString()}";

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(TypedNode))
                return new[] { this };
            else if (type == typeof(PrettyPath))
                return new[] { new PrettyPath { Path = PrettyPath } };
            else if (type == typeof(ElementDefinitionSummary) && _current.IsTracking)
                return new[] { new ElementDefinitionSummary(_current.SerializationInfo) };
            else
                return _current.Node.Annotations(type);

        }
    }

    [Obsolete("This class is used for internal purposes and is subject to change without notice. Don't use.")]
    public delegate object AdditionalStructuralRule(IElementNode node, IExceptionSource ies, object state);

    public class StructuralTypeException : Exception
    {
        public StructuralTypeException() { }
        public StructuralTypeException(string message) : base(message) { }
        public StructuralTypeException(string message, Exception inner) : base(message, inner) { }
    }


    /// <summary>
    /// Represents a dotted path into an instance, where indices on members are only included
    /// when the elements repeats.
    /// </summary>
    public class PrettyPath
    {
        public string Path;
    }

}
