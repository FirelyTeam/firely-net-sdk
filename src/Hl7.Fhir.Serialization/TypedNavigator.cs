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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public class StructuralTypeException : Exception
    {
        public StructuralTypeException() { }
        public StructuralTypeException(string message) : base(message) { }
        public StructuralTypeException(string message, Exception inner) : base(message, inner) { }
    }

    public class TypedNavigator : IElementNavigator, IAnnotated, IExceptionSource, IExceptionSink
    {
        public TypedNavigator(ISourceNavigator root, ISerializationInfoProvider provider) : this(root, root.Name, provider)
        {
        }

        public TypedNavigator(ISourceNavigator element, string type, ISerializationInfoProvider provider)
        {
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            if (element == null) throw Error.ArgumentNull(nameof(element));

            var elementType = provider.Provide(type);

            _current = NavigatorPosition.ForElement(element, elementType, element.Name);
            _definition = _current.IsTracking ?
                SerializationInfoCache.ForRoot(_current.SerializationInfo) : SerializationInfoCache.Empty;
            _parentPath = null;
            _nameIndex = 0;

            Sink = null;
            Provider = provider;
        }

        private void reportUnknownType(string typeName, ISourceNavigator position)
        {
            raiseTypeError($"Encountered unknown type '{typeName}'", position);
        }
        private void verifyElementTrackingStatus(NavigatorPosition current, SerializationInfoCache definition)
        {
            // If we found a type, but we don't know about the specific child, complain
            if (definition != SerializationInfoCache.Empty && !current.IsTracking)
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
                Sink = this.Sink,
                Provider = this.Provider
            };
        }

        public IExceptionSink Sink { get; set; }

        public void Notify(object sender, ExceptionNotification args) => Sink.NotifyOrThrow(sender, args);

        private void raiseTypeError(string message, ISourceNavigator current) =>
            Notify(current, ExceptionNotification.Error(
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

        private SerializationInfoCache _definition;
        public ISerializationInfoProvider Provider { get; private set; }

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


        private NavigatorPosition deriveInstanceType(ISourceNavigator current, IElementSerializationInfo info)
        {
            if (info == null) return new NavigatorPosition(current, null, current.Name, null);

            string instanceType = null;

            if (info.IsContainedResource)
            {
                instanceType = current.GetResourceType();
                if (instanceType == null) raiseTypeError($"Element '{current.Name}' should contain a resource, but does not actually seem to contain one", current);
            }
            else if (!info.IsContainedResource && current.GetResourceType() != null)
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
                    instanceType = info.Type.OfType<ITypeReference>().Select(t => t.ReferredType).FirstOrDefault(t => String.Compare(t, suffix, StringComparison.OrdinalIgnoreCase) == 0);

                    if (String.IsNullOrEmpty(instanceType))
                        raiseTypeError($"Choice element '{current.Name}' is suffixed with unexpected type '{suffix}'", current);
                }
            }
            else
            {
                var tp = info.Type.Single();

                switch (tp)
                {
                    case ITypeReference tr:
                        instanceType = tr.ReferredType;
                        break;
                    case IComplexTypeSerializationInfo ct:
                        instanceType = ct.TypeName;
                        break;
                    default:
                        throw Error.NotSupported($"Don't know how to derive type information from type {tp.GetType()}");
                }
            }

            return new NavigatorPosition(current, info, info?.ElementName ?? current.Name, instanceType);
        }

        private bool tryMoveToElement(SerializationInfoCache dis, ISourceNavigator current, string name, out NavigatorPosition match)
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
            if (_parentPath == null && _definition == SerializationInfoCache.Empty)
                reportUnknownType(_current.InstanceType, _current.Node);

            var scan = _current.Node.Clone();

            // Move down the first child - note we don't use the nameFilter, 
            // the tryMoveToElement() which comes next will verify the filter (if any).
            if (!scan.MoveToFirstChild()) return false;


            var firstChildDef = down(_current);
            if (_current.IsTracking && firstChildDef == SerializationInfoCache.Empty && _current.InstanceType != null)
                reportUnknownType(_current.InstanceType, _current.Node);

            if (!tryMoveToElement(firstChildDef, scan, nameFilter, out var match)) return false;
            verifyElementTrackingStatus(match, firstChildDef);

            // Found a match, so we can alter the current position of the navigator.
            // Modify _parentPath to be the current path before we do that
            _parentPath = Location;
            _nameIndex = 0;
            _current = match;
            _definition = firstChildDef;

            return true;
        }

        private SerializationInfoCache down(NavigatorPosition current)
        {
            if (!current.IsTracking || current.InstanceType == null) return SerializationInfoCache.Empty;

            // If this is a backbone element, the child type is the nested complex type
            if (current.SerializationInfo.Type[0] is IComplexTypeSerializationInfo be)
                return SerializationInfoCache.ForType(be);
            else
            {
                var si = Provider.Provide(current.InstanceType);

                if (si == null)
                    return SerializationInfoCache.Empty;
                else
                    return SerializationInfoCache.ForType(si);
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

            _current = match;

            if (currentName == Name)
                _nameIndex += 1;
            else
                _nameIndex = 0;

            return true;
        }

        public string Location
        {
            get
            {
                if (_parentPath == null)
                    return Name;
                else
                {
                    if (_current.IsTracking && _current.SerializationInfo.MayRepeat == false)
                        return $"{_parentPath}.{Name}";
                    else
                        return $"{_parentPath}.{Name}[{_nameIndex}]";
                }
            }
        }

        public override string ToString() => $"{(_current.IsTracking ? ($"[{_current.InstanceType}] ") : "")}{_current.Node.ToString()}";

        private static readonly PipelineComponent _componentLabel = PipelineComponent.Create<TypedNavigator>();

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(PipelineComponent))
                return (new[] { _componentLabel }).Union(_current.Node.Annotations(typeof(PipelineComponent)));
            else if (type == typeof(ElementSerializationInfo) && _current.IsTracking)
                return new[] { new ElementSerializationInfo(_current.SerializationInfo) };
            else
                return _current.Node.Annotations(type);
        }
    }
}
