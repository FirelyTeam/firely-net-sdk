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
    public class TypedElement : ITypedElement, IAnnotated, IExceptionSource, IShortPath
    {
        public TypedElement(ISourceNode element, string type, IStructureDefinitionSummaryProvider provider, TypedNodeSettings settings = null)
        {
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            if (element == null) throw Error.ArgumentNull(nameof(element));

            Provider = provider;
            _settings = settings ?? new TypedNodeSettings();

            if (element is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);

            ShortPath = element.Name;
            Current = buildRootPosition(element, type, provider);
        }

        private NavigatorPosition buildRootPosition(ISourceNode element, string type, IStructureDefinitionSummaryProvider provider)
        {
            var rootType = type ?? element.ResourceType;
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


        private TypedElement(TypedElement parent, NavigatorPosition position, string prettyPath)
        {
            Current = position;
            ShortPath = prettyPath;
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

        public string InstanceType => Current.InstanceType;

        public object Value
        {
            get
            {
                string sourceText = Current.Node.Text;

                if (sourceText == null) return null;

                // If we don't have type information (no definition was found
                // for current node), all we can do is return the underlying string value
                if (!Current.IsTracking || Current.InstanceType == null) return sourceText;

                if (!Primitives.IsPrimitive(InstanceType))
                {
                    raiseTypeError($"Since type {InstanceType} is not a primitive, it cannot have a value", Current.Node);
                    return null;
                }

                // Finally, we have a (potentially) unparsed string + type info
                // parse this primitive into the desired type
                try
                {
                    return PrimitiveTypeConverter.FromSerializedValue(sourceText, InstanceType);
                }
                catch (FormatException fe)
                {
                    raiseTypeError($"Literal '{sourceText}' cannot be interpreted as a {InstanceType}: '{fe.Message}'.", Current.Node);
                    return sourceText;
                }
            }
        }

        private NavigatorPosition deriveInstanceType(ISourceNode current, IElementDefinitionSummary info)
        {
            string instanceType = null;

            if (info.IsResource)
            {
                instanceType = current.ResourceType;
                if (instanceType == null) raiseTypeError($"Element '{current.Name}' should contain a resource, but does not actually contain one", current);
            }
            else if (!info.IsResource && current.ResourceType != null)
            {
                raiseTypeError($"Element '{current.Name}' is not a contained resource, but seems to contain a resource of type '{current.ResourceType}'.", current);
                instanceType = current.ResourceType;
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

        private IEnumerable<TypedElement> enumerateElements(ElementDefinitionSummaryCache dis, ISourceNode parent, string name)
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
                 hit && !info.IsCollection ? $"{ShortPath}.{match.Name}" : $"{ShortPath}.{match.Name}[{_nameIndex}]";

                yield return new TypedElement(this, match, prettyPath);
            }
        }

        public IEnumerable<ITypedElement> Children(string nameFilter = null)
        {
            var firstChildDef = down(Current);

            if (Current.IsTracking && firstChildDef == ElementDefinitionSummaryCache.Empty)
            {
                // No type information available for the type representing the children....

                if (Current.InstanceType != null && _settings.ErrorMode == TypedNodeSettings.TypeErrorMode.Report)
                    raiseTypeError($"Encountered unknown type '{Current.InstanceType}'", Current.Node);

                // Don't go on with the (untyped) children, unless explicitly told to do so
                if (_settings.ErrorMode != TypedNodeSettings.TypeErrorMode.Passthrough)
                    return Enumerable.Empty<ITypedElement>();
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

        private IEnumerable<ITypedElement> runAdditionalRules(IEnumerable<ITypedElement> children)
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

        public string ShortPath { get; private set; }

        public IElementDefinitionSummary Definition => Current.SerializationInfo;

        public override string ToString() => $"{(Current.IsTracking ? ($"[{Current.InstanceType}] ") : "")}{Current.Node.ToString()}";

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(TypedElement) || type == typeof(ITypedElement) || type == typeof(IShortPath))
                return new[] { this };
            else
                return Current.Node.Annotations(type);

        }
    }

    [Obsolete("This class is used for internal purposes and is subject to change without notice. Don't use.")]
    public delegate object AdditionalStructuralRule(ITypedElement node, IExceptionSource ies, object state);
}
