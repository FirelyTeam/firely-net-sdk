/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
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
        public TypedNavigator(IElementNavigator root, IModelMetadataProvider provider) : this(root, root.Name, provider)
        {
        }

        public TypedNavigator(IElementNavigator element, string type, IModelMetadataProvider provider)
        {
            if (type == null) throw Error.ArgumentNull(nameof(type));

            var elementType = provider?.GetSerializationInfoForStructure(type);

            var current = NavigatorPosition.ForElement(element, elementType, element.Name);
            var definition = current.IsTracking ?
                SerializationInfoCache.ForRoot(current.SerializationInfo) : SerializationInfoCache.Empty;

            _current = current;
            _definition = definition;
            _parentPath = null;
            _nameIndex = 0;

            Sink = null;
            Provider = provider;
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

        public bool Raise(object sender, ExceptionRaisedEventArgs args) => Sink.RaiseOrThrow(sender, args);

        private void raiseTypeError(string message, IElementNavigator current) =>
            Raise(current, ExceptionRaisedEventArgs.Error(
                typeException(message, current)));
                
        private static StructuralTypeException typeException(string message, IElementNavigator position)
        {
            if(position != null)
                message += $" (at {position.Location})";

            return new StructuralTypeException(message);
        }


        private NavigatorPosition _current;

        private int _nameIndex;
        private string _parentPath;

        private SerializationInfoCache _definition;
        public IModelMetadataProvider Provider { get; private set; }

        public string Name => _current.Name;

        public string Type => _current.InstanceType;

        public object Value
        {
            get
            {
                object underlyingValue = _current.Node.Value;

                if (underlyingValue == null) return null;

                // If the source already supplies parsed values, return
                // the source value
                if (!(underlyingValue is string)) return underlyingValue;

                // If we don't have type information (no definition was found
                // for current node), all we can do is return the underlying value
                if (!_current.IsTracking) return underlyingValue;

                // Finally, we have a (potentially) unparsed string + type info
                // parse this primitive into the desired type
                try
                {
                    return PrimitiveTypeConverter.FromSerializedValue((string)underlyingValue, Type);
                }
                catch (FormatException fe)
                {
                    raiseTypeError($"Literal '{(string)underlyingValue}' cannot be interpreted as a {Type}: '{fe.Message}'.", _current.Node);
                    return underlyingValue;
                }
            }
        }


        private NavigatorPosition deriveInstanceType(IElementNavigator current, IElementSerializationInfo info)
        {
            if (info == null) return new NavigatorPosition(current, null, current.Name, null);

            string instanceType = null;

            if (info.IsContainedResource)
            {
                instanceType = current.GetResourceType();
                if (instanceType == null) raiseTypeError("Element should contain a resource, but does not actually seem to contain one", current);
            }
            else if (!info.IsContainedResource && current.GetResourceType() != null)
            {
                raiseTypeError("Element is not a contained resource, but seems to contain a resource.", current);
            }
            else if (info.IsChoiceElement)
            {
                var suffix = current.Name.Substring(info.ElementName.Length);

                if (String.IsNullOrEmpty(suffix))
                    raiseTypeError($"Choice element '{current.Name}' is not suffixed with a type.", current);
                else
                {
                    instanceType = info.Type.Select(t => t.TypeName).FirstOrDefault(t => String.Compare(t, suffix, StringComparison.OrdinalIgnoreCase) == 0);

                    if (String.IsNullOrEmpty(instanceType))
                        raiseTypeError($"Choice element is suffixed with unexpected type '{suffix}'", current);
                }
            }
            else
            {
                instanceType = info.Type.Single().TypeName;
            }

            return new NavigatorPosition(current, info, info?.ElementName ?? current.Name, instanceType);
        }

        private bool tryMoveToElement(SerializationInfoCache dis, IElementNavigator current, string name, out NavigatorPosition match)
        {
            var scan = current;

            // no name filter: a very common case, and we should handle it immediately before trying more
            // expensive methods.
            if (name == null)
            {
                dis.TryGetBySuffixedName(scan.Name, out var info);
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
            var scan = _current.Node.Clone();

            if (!scan.MoveToFirstChild()) return false;
            var firstChildDef = down();

            if (!tryMoveToElement(firstChildDef, scan, nameFilter, out var match)) return false;

            if(!match.IsTracking)
            {
                raiseTypeError($"Encountered unknown element '{match.Name}'", match.Node);
            }

            // Found a match, so we can alter the current position of the navigator.
            // Modify _parentPath to be the current path before we do that
            _parentPath = Location;
            _nameIndex = 0;
            _current = match;
            _definition = firstChildDef;

            return true;
        }


        private SerializationInfoCache down()
        {
            if (!_current.IsTracking || _current.InstanceType == null) return SerializationInfoCache.Empty;

            // If this is a backbone element, the child type is the nested complex type
            if (_current.SerializationInfo.Type[0] is IComplexTypeSerializationInfo be)
                return SerializationInfoCache.ForType(be);
            else
            {
                var si = Provider.GetSerializationInfoForStructure(_current.InstanceType);

                if (si == null)
                {
                    raiseTypeError($"Encountered unknown type '{_current.InstanceType}'", _current.Node);
                    return SerializationInfoCache.Empty;
                }
                else
                    return SerializationInfoCache.ForType(si);
            }
        }


        public bool MoveToNext(string nameFilter)
        {
            if (!_current.Node.MoveToNext()) return false;

            if (!tryMoveToElement(_definition, _current.Node, nameFilter, out var match)) return false;

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

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ElementSerializationInfo))
            {
                return new[]
                {
                     _current.IsTracking ? new ElementSerializationInfo(_current.SerializationInfo) : ElementSerializationInfo.NO_SERIALIZATION_INFO
                };
            }
            else
            {
                if (_current.Node is IAnnotated ia)
                    return ia.Annotations(type);
                else
                    return Enumerable.Empty<object>();
            }
        }
    }
}
