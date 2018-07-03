/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Support.Utility;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct TypedNavigator : IElementNavigator, IAnnotated, IPositionInfo, IExceptionSource, IExceptionSink
    {
        public TypedNavigator(IElementNavigator root, IModelMetadataProvider provider) : this(root, root.Name, provider)
        {
        }

        public TypedNavigator(IElementNavigator element, string type, IModelMetadataProvider provider)
        {
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

        public IExceptionSink Sink { get; set; }

        public bool Raise(object sender, ExceptionRaisedEventArgs args) => Sink.RaiseOrThrow(sender, args);

        private void raiseFormatError(string message, IElementNavigator current) =>
            Raise(current, ExceptionRaisedEventArgs.Error(Error.Format(message, current as IPositionInfo)));

        public IElementNavigator Clone() => this;        // the struct will be copied upon return

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
                    Raise(this, ExceptionRaisedEventArgs.Error(fe));
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
                instanceType = current.GetResourceTypeFromAnnotation();
                if (instanceType == null) raiseFormatError("Element is defined to contain a resource, but does not actually seem to contain a resource", current);
                //TODO: check validity of resource type
            }
            else if (!info.IsContainedResource && current.GetResourceTypeFromAnnotation() != null)
            {
                raiseFormatError("Element is not a contained resource, but it does contain a resource", current);
            }
            else if (info.IsChoiceElement)
            {
                var suffix = current.Name.Substring(info.ElementName.Length);

                if (String.IsNullOrEmpty(suffix))
                    raiseFormatError($"Choice element '{current.Name}' is not suffixed with a type.", current);
                else
                {
                    instanceType = info.Type.Select(t => t.TypeName).FirstOrDefault(t => String.Compare(t, suffix, StringComparison.OrdinalIgnoreCase) == 0);

                    if (String.IsNullOrEmpty(instanceType))
                        raiseFormatError($"Choice element is suffixed with unexpected type '{suffix}'", current);
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
            var scan = current.Clone();
            var isKnownElement = dis.TryGetValue(name, out var elementInfo);
            match = null;

            if (name == null || !isKnownElement || (isKnownElement && !elementInfo.IsChoiceElement))
            {
                var success = scan.MoveToNext(name);

                if (success)
                    match = deriveInstanceType(scan, elementInfo);

                return success;
            }

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
            if (!_current.Node.MoveToFirstChild()) return false;

            var firstChildDef = down();

            if (!tryMoveToElement(firstChildDef, _current.Node, nameFilter, out var match)) return false;

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
                return SerializationInfoCache.ForType(Provider.GetSerializationInfoForStructure(_current.InstanceType));
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

        int IPositionInfo.LineNumber => (_current as IXmlLineInfo)?.LineNumber ?? -1;

        int IPositionInfo.LinePosition => (_current as IXmlLineInfo)?.LinePosition ?? -1;


        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ElementSerializationInfo))
            {
                return new[]
                {
                     _current.IsTracking ? new ElementSerializationInfo(_current.SerializationInfo) : ElementSerializationInfo.NO_SERIALIZATION_INFO
                };
            }
            if (type == typeof(PositionInfo))
            {
                var t = this as IPositionInfo;
                return new[] { new PositionInfo { LineNumber = t.LineNumber, LinePosition = t.LinePosition } };
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
