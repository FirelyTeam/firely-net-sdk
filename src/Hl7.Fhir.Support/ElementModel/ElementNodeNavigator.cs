using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel
{
    public struct ElementNodeNavigator : IElementNavigator, IAnnotated
    {
        private IList<IElementNode> _siblings;
        private int _index;

        public IElementNode Current
        {
            get { return _siblings[_index]; }
        }

        public ElementNodeNavigator(IElementNode wrapped)
        {
            _siblings = new List<IElementNode> { wrapped };
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

        
        private int nextMatch(IList<IElementNode> nodes, string namefilter=null, int startAfter=-1)
        {
            for(int scan=startAfter+1; scan < nodes.Count; scan++)
            {
                if (namefilter == null || nodes[scan].Name == namefilter)
                    return scan;
            }

            return -1;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var children = Current.Children;

            if (!children.Any()) return false;

            var found = nextMatch(children, nameFilter);

            if (found == -1) return false;

            _siblings = children;
            _index = 0;
            return true;
        }

        public bool MoveToNext(string nameFilter = null)
        {
            var found = nextMatch(_siblings, nameFilter, _index);

            if (found == -1) return false;

            _index = found;
            return true;
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