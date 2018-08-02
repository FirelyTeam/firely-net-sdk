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
    public class StructuralTypeException : Exception
    {
        public StructuralTypeException() { }
        public StructuralTypeException(string message) : base(message) { }
        public StructuralTypeException(string message, Exception inner) : base(message, inner) { }
    }

    public class TypedNavigator : IElementNavigator, IAnnotated, IExceptionSource
    {
        public TypedNavigator(ISourceNavigator element, string rootType, IStructureDefinitionSummaryProvider provider)
        {
            if (rootType == null) throw Error.ArgumentNull(nameof(rootType));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            if (element == null) throw Error.ArgumentNull(nameof(element));

            var elementType = provider.Provide(rootType);

            _current = NavigatorPosition.ForRoot(element, elementType, element.Name);
            _definition = _current.IsTracking ?
                ElementDefinitionSummaryCache.ForRoot(_current.SerializationInfo) : ElementDefinitionSummaryCache.Empty;
            _parentPath = null;
            _nameIndex = 0;

            Provider = provider;

            if (element is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        private void reportUnknownType(string typeName, ISourceNavigator position)
        {
            raiseTypeError($"Encountered unknown type '{typeName}'", position);
        }
        private void verifyElementTrackingStatus(NavigatorPosition current, ElementDefinitionSummaryCache definition)
        {
            // If we found a type, but we don't know about the specific child, complain
            if (definition != ElementDefinitionSummaryCache.Empty && !current.IsTracking)
                raiseTypeError($"Encountered unknown element '{current.Name}'", current.Node);
        }

        private TypedNavigator() { }   // for Clone()

        public IElementNavigator Clone()
        {
            return new TypedNavigator()
            {
                _current = new NavigatorPosition(this._current.Node.Clone(), this._current.SerializationInfo,
                                        this._current.Name, this._current.InstanceType),
                _definition = this._definition,
                _parentPath = this._parentPath,
                _nameIndex = this._nameIndex,
                ExceptionHandler = this.ExceptionHandler,
                Provider = this.Provider,
                LastOrder = this.LastOrder
            };
        }

        private void raiseTypeError(string message, ISourceNavigator current) =>
            ExceptionHandler.NotifyOrThrow(current, ExceptionNotification.Error(
                typeException(message, current)));

        private static StructuralTypeException typeException(string message, ISourceNavigator position)
        {
            if (position != null)
                message += $" (at {position.Path})";

            return new StructuralTypeException(message);
        }


        private NavigatorPosition _current;

        private int _nameIndex;
        private string _parentPath;

        private ElementDefinitionSummaryCache _definition;
        public IStructureDefinitionSummaryProvider Provider { get; private set; }

        public string Name => _current.Name;

        public string Type => _current.InstanceType;

        public (string name,int order)? LastOrder { get; private set; }

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

        private NavigatorPosition deriveInstanceType(ISourceNavigator current, IElementDefinitionSummary info)
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

                switch (tp)
                {
                    case IStructureDefinitionReference tr:
                        instanceType = tr.ReferredType;
                        break;
                    case IStructureDefinitionSummary ct:
                        instanceType = ct.TypeName;
                        break;
                    default:
                        throw Error.NotSupported($"Don't know how to derive type information from type {tp.GetType()}");
                }
            }

            return new NavigatorPosition(current, info, info?.ElementName ?? current.Name, instanceType);
        }

        private bool tryMoveToElement(ElementDefinitionSummaryCache dis, ISourceNavigator current, string name, out NavigatorPosition match)
        {
            var scan = current;

            // no name filter: a very common case, and we should handle it immediately before trying more
            // expensive methods.
            if (name == null)
            {
                var hit = dis.TryGetBySuffixedName(scan.Name, out var info);
                match = deriveInstanceType(scan, info);   // could be no info (null)
                return true;
            }

            var knownProperty = dis.TryGetValue(name, out var elementInfo);

            // Do a scan. There are two possibilities
            // a) This is a known property, and it's not a choice OR it's an unknown property 
            //          -> we can ask the underlying untyped navigator to move to it as quick as possible.
            // b) It's a known property and a choice
            //          -> we need to enumerate the elements to match on a prefix (since there will
            //          be a typename suffixed to it)
            var canUseQuickScan = !knownProperty || (knownProperty && !elementInfo.IsChoiceElement);

            if (canUseQuickScan)
            {
                // direct hit (scan matches), or move to it
                var success = scan.Name == name || scan.MoveToNext(name);
                match = deriveInstanceType(scan, elementInfo);
                return success;
            }

            // Else, use "slow" scan
            do
            {
                if (scan.Name.StartsWith(name))
                {
                    match = deriveInstanceType(scan, elementInfo);
                    return true;
                }
            }
            while (scan.MoveToNext());

            match = null;
            return false;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            // Since we don't want to report errors from the constructor, do this work now on
            // the initial call (_parentPath == null)
            if (_parentPath == null && _definition == ElementDefinitionSummaryCache.Empty)
                reportUnknownType(_current.InstanceType, _current.Node);

            var scan = _current.Node.Clone();

            // Move down the first child - note we don't use the nameFilter, 
            // the tryMoveToElement() which comes next will verify the filter (if any).
            if (!scan.MoveToFirstChild()) return false;

            var firstChildDef = down(_current);
            if (_current.IsTracking && firstChildDef == ElementDefinitionSummaryCache.Empty && _current.InstanceType != null)
                reportUnknownType(_current.InstanceType, _current.Node);

            if (!tryMoveToElement(firstChildDef, scan, nameFilter, out var match)) return false;
            verifyElementTrackingStatus(match, firstChildDef);

            // Found a match, so we can alter the current position of the navigator.
            // Modify _parentPath to be the current path before we do that
            LastOrder = null;
            _parentPath = Location;
            _nameIndex = 0;
            _current = match;
            _definition = firstChildDef;

            runAdditionalRules();

            return true;
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



        public bool MoveToNext(string nameFilter)
        {
            if (!_current.Node.MoveToNext()) return false;

            if (!tryMoveToElement(_definition, _current.Node, nameFilter, out var match)) return false;

            verifyElementTrackingStatus(match, _definition);

            // store the current name before proceeding to detect repeating
            // element names and count them
            var currentName = Name;
            if (_current.IsTracking && _current.SerializationInfo.Representation == XmlRepresentation.XmlElement)
                LastOrder = (Name, _current.SerializationInfo.Order);

            _current = match;

            if (currentName == Name)
                _nameIndex += 1;
            else
                _nameIndex = 0;

            runAdditionalRules();

            return true;
        }

        private void runAdditionalRules()
        {
#pragma warning disable 612,618
            var additionalRules = _current.Node.Annotations(typeof(AdditionalStructuralRule));

            foreach (var rule in additionalRules.Cast<AdditionalStructuralRule>())
                rule(this, this);
#pragma warning restore 612,618
        }

        public string Location
        {
            get
            {
                if (_parentPath == null)
                    return Name;
                else
                {
                    if (_current.IsTracking && _current.SerializationInfo.IsCollection == false)
                        return $"{_parentPath}.{Name}";
                    else
                        return $"{_parentPath}.{Name}[{_nameIndex}]";
                }
            }
        }

        public override string ToString() => $"{(_current.IsTracking ? ($"[{_current.InstanceType}] ") : "")}{_current.Node.ToString()}";

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(TypedNavigator))
                return new[] { this };
            else if (type == typeof(ElementDefinitionSummary) && _current.IsTracking)
                return new[] { new ElementDefinitionSummary(_current.SerializationInfo) };
            else
                return _current.Node.Annotations(type);
        }
    }

    [Obsolete("This class is used for internal purposes and is subject to change without notice. Don't use.")]
    public delegate void AdditionalStructuralRule(TypedNavigator node, IExceptionSource ies);
    
}
