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
        public TypedNode(ISourceNode element, string type, IStructureDefinitionSummaryProvider provider, TypedNodeSettings settings = null)
        {
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            if (element == null) throw Error.ArgumentNull(nameof(element));

            Provider = provider;
            _settings = settings ?? new TypedNodeSettings();

            if (element is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);

            PrettyPath = element.Name;
            Current = buildRootPosition(element, type, provider);
        }

        private NavigatorPosition buildRootPosition(ISourceNode element, string type, IStructureDefinitionSummaryProvider provider)
        {
            var rootType = type ?? element.GetResourceType();
            if (rootType == null)
            {
                if (_settings.ErrorMode == TypedNodeSettings.TypeErrorMode.Report)
                    throw Error.Argument(nameof(type), $"Cannot determine the type of the root element at '{element.Location}', " +
                        $"please supply a type argument.");
                else
                    return NavigatorPosition.ForRoot(element, null, element.Name);
            }

            var elementType = provider.Provide(rootType);

            if (elementType == null)
            {
                if (_settings.ErrorMode == TypedNodeSettings.TypeErrorMode.Report)
                    throw Error.Argument(nameof(type), $"Cannot locate type information for type '{rootType}'");
                else
                    return NavigatorPosition.ForRoot(element, null, element.Name);
            }

            return NavigatorPosition.ForRoot(element, elementType, element.Name);
        }


        private TypedNode(TypedNode parent, NavigatorPosition position, string prettyPath)
        {
            Current = position;
            PrettyPath = prettyPath;
            Provider = parent.Provider;
            ExceptionHandler = parent.ExceptionHandler;
            _settings = parent._settings;
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        private void raiseTypeError(string message, object source, bool warning = false)
        {
            var exc = new StructuralTypeException(message);
            var notification = warning ?
                ExceptionNotification.Warning(exc) :
                ExceptionNotification.Error(exc);

            ExceptionHandler.NotifyOrThrow(source, notification);
        }

        private readonly NavigatorPosition Current;

        public readonly IStructureDefinitionSummaryProvider Provider;

        private readonly TypedNodeSettings _settings;

        public string Name => Current.Name;

        public string Type => Current.InstanceType;

        public object Value
        {
            get
            {
                string sourceText = Current.Node.Text;

                if (sourceText == null) return null;

                // If we don't have type information (no definition was found
                // for current node), all we can do is return the underlying string value
                if (!Current.IsTracking || Current.InstanceType == null) return sourceText;

                if (!Primitives.IsPrimitive(Type))
                {
                    raiseTypeError($"Since type {Type} is not a primitive, it cannot have a value", Current.Node);
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
                    raiseTypeError($"Literal '{sourceText}' cannot be interpreted as a {Type}: '{fe.Message}'.", Current.Node);
                    return sourceText;
                }
            }
        }

        private NavigatorPosition deriveInstanceType(ISourceNode current, IElementDefinitionSummary info)
        {
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
                NavigatorPosition match = null;

                if (info == null)
                    match = new NavigatorPosition(scan, null, scan.Name, null);
                else
                    match = deriveInstanceType(scan, info);

                // If we found a type, but we don't know about the specific child, complain
                if (dis != ElementDefinitionSummaryCache.Empty && !match.IsTracking)
                {
                    raiseTypeError($"Encountered unknown element '{scan.Name}'", this,
                            warning: _settings.ErrorMode != TypedNodeSettings.TypeErrorMode.Report);

                    // don't include member, unless we are explicitly told to let it pass
                    if (_settings.ErrorMode != TypedNodeSettings.TypeErrorMode.Passthrough)
                        continue;
                }

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
            var firstChildDef = down(Current);

            if (Current.IsTracking && firstChildDef == ElementDefinitionSummaryCache.Empty)
            {
                // No type information available for the type representing the children....

                if (Current.InstanceType != null && _settings.ErrorMode == TypedNodeSettings.TypeErrorMode.Report)
                    raiseTypeError($"Encountered unknown type '{Current.InstanceType}'", Current.Node);

                // Don't go on with the (untyped) children, unless explicitly told to do so
                if (_settings.ErrorMode != TypedNodeSettings.TypeErrorMode.Passthrough)
                    return Enumerable.Empty<IElementNode>();
                else
                    // Ok, pass through the untyped members, but since there is no type information, 
                    // don't bother to run the additional rules
                    return enumerateElements(firstChildDef, Current.Node, nameFilter);
            }
            else
                return runAdditionalRules(enumerateElements(firstChildDef, Current.Node, nameFilter));
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
            var additionalRules = Current.Node.Annotations(typeof(AdditionalStructuralRule));
            var stateBag = new Dictionary<AdditionalStructuralRule, object>();
            foreach (var child in children)
            {
                foreach (var rule in additionalRules.Cast<AdditionalStructuralRule>())
                {
                    object state = null;
                    stateBag.TryGetValue(rule, out state);
                    state = rule(child, this, state);
                    if (state != null) stateBag[rule] = state;
                }

                yield return child;
            }
#pragma warning restore 612,618
        }

        public string Location => Current.Node.Location;

        public string PrettyPath { get; set; }

        public override string ToString() => $"{(Current.IsTracking ? ($"[{Current.InstanceType}] ") : "")}{Current.Node.ToString()}";

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(TypedNode))
                return new[] { this };
            else if (type == typeof(PrettyPath))
                return new[] { new PrettyPath { Path = PrettyPath } };
            else if (type == typeof(ElementDefinitionSummary) && Current.IsTracking)
                return new[] { new ElementDefinitionSummary(Current.SerializationInfo) };
            else
                return Current.Node.Annotations(type);

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
