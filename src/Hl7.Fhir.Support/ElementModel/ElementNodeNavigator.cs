using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel
{
    public struct ElementNodeNavigator : IElementNavigator, IAnnotated
    {
        private IElementNode[] _siblings;
        private int _index;

        public IElementNode Current
        {
            get { return _siblings[_index]; }
        }

        public ElementNodeNavigator(IElementNode wrapped)
        {
            _siblings = new[] { wrapped };
            _index = 0;
        }

        public string Name
        {
            get
            {
                return Current.Name;
            }
        }

        public string Type
        {
            get
            {
                return Current.Type;
            }
        }

        public string Location
        {
            get
            {
                return Current.Location;
            }
        }


        public object Value
        {
            get
            {
                return Current.Value;
            }
        }

        public IElementNavigator Clone()
        {
            var r = new ElementNodeNavigator();

            r._siblings = this._siblings;
            r._index = this._index;

            return r;
        }

        public bool MoveToFirstChild()
        {
            if (Current.Children != null && Current.Children.Any())
            {
                _siblings = Current.Children.ToArray();
                _index = 0;
                return true;
            }

            return false;
        }

        public bool MoveToNext()
        {
            if (_siblings.Length > _index + 1)
            {
                _index += 1;
                return true;
            }

            return false;
        }

        public IEnumerable<object> Annotations(Type type)
        {
            if (Current is IAnnotated annotated)
                return annotated.Annotations(type);
            else
                return Enumerable.Empty<object>();
        }
    }


    public static class ElementNodeNavigatorFactory
    {
        public static IElementNavigator ToNavigator(this IElementNode node)
        {
            return new ElementNodeNavigator(node);
        }

    }

}