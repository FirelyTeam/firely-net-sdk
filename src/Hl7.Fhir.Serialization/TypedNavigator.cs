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
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct TypedNavigator : IElementNavigator, IAnnotated, IPositionInfo, IExceptionSource
    {
        private TypedNavigator(NavigatorPosition current, SerializationInfoCache definition, IModelMetadataProvider provider)
        {
            _current = current;
            _definition = definition;
            _parentPath = null;
            _nameIndex = 0;

            Provider = provider;

            OnExceptionRaised = null;
            var oer = OnExceptionRaised;

            // Worries about dangling references...need to unsubscribe at some point...
            if (current is IExceptionSource ies)
                ies.OnExceptionRaised += (o, e) => oer?.Invoke(o, e);
        }

        internal static TypedNavigator ForRoot(IElementNavigator root, IModelMetadataProvider provider)
        {
            var rootType = root.Name;
            return ForElement(root, rootType, provider);
        }

        internal static TypedNavigator ForElement(IElementNavigator element, string type, IModelMetadataProvider provider)
        {
            var elementType = provider?.GetSerializationInfoForStructure(type);

            var current = NavigatorPosition.ForElement(element, elementType, element.Name);
            var definition = current.IsTracking ?
                SerializationInfoCache.ForRoot(current.SerializationInfo) : SerializationInfoCache.Empty;

            return new TypedNavigator(current, definition, provider);
        }

        public event EventHandler<ExceptionRaisedEventArgs> OnExceptionRaised;

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
                    OnExceptionRaised?.Invoke(this, new ExceptionRaisedEventArgs(fe));
                    return underlyingValue;
                }
            }
        }

        private static bool tryMatch(SerializationInfoCache dis, IElementNavigator current, string name, out NavigatorPosition match)
        {
            var scan = current.Clone();
            var found = dis.Find(current.Name, out var elementInfo, out var instanceType);
            var filter = found ? elementInfo.ElementName : current.Name;

            do
            {

                bool isMatch = name == null ||      // no name filter -> any match is ok
                    filter == name;    // else, filter on the actual name

                if (isMatch)
                {
                    //If the underlying nav has a ResourceType indicator....
                    if (scan is IAnnotated ia && ia.TryGetAnnotation<ResourceTypeIndicator>(out var rt))
                        instanceType = rt.ResourceType;

                    match = new NavigatorPosition(scan, elementInfo, filter, instanceType);
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

            if (!tryMatch(firstChildDef, _current.Node, nameFilter, out var match)) return false;

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
            if (!_current.IsTracking) return SerializationInfoCache.Empty;

            IComplexTypeSerializationInfo childType = null;

            // If this is a backbone element, the child type is the nested complex type
            if (_current.SerializationInfo.Type[0] is IComplexTypeSerializationInfo be)
                childType = be;
            else
                childType = Provider.GetSerializationInfoForStructure(this.Type);

            return SerializationInfoCache.ForType(childType);
        }



        public bool MoveToNext(string nameFilter)
        {
            if (!_current.Node.MoveToNext()) return false;

            if (!tryMatch(_definition, _current.Node, nameFilter, out var match)) return false;

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

        public int LineNumber => (_current.Node as PositionInfo)?.LineNumber ?? -1;

        public int LinePosition => (_current.Node as PositionInfo)?.LinePosition ?? -1;

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ElementSerializationInfo) && _current.IsTracking)
            {
                return new[] { new ElementSerializationInfo(_current.SerializationInfo) };
            }
            if (type == typeof(PositionInfo))
            {
                return new[] { new PositionInfo { LineNumber = this.LineNumber, LinePosition = this.LinePosition } };
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
