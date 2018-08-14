using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel
{
    internal class ElementNodeToElementNavAdapter : IElementNavigator, IAnnotated, IExceptionSource
    {
        private IList<ITypedElement> _siblings;
        private int _index;
        public ITypedElement Current =>  _siblings[_index]; 

        public ElementNodeToElementNavAdapter(ITypedElement sourceNode)
        {
            _siblings = new List<ITypedElement> { sourceNode };
            _index = 0;

            if (sourceNode is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private ElementNodeToElementNavAdapter() { }  // for clone

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Current.Name;

        public string Type => Current.InstanceType;

        public string Location => Current.Location;

        public object Value => Current.Value;

        public IElementNavigator Clone() =>
            new ElementNodeToElementNavAdapter
            {
                _siblings = this._siblings,
                _index = this._index,

                ExceptionHandler = this.ExceptionHandler
            };


        private int nextMatch(IList<ITypedElement> nodes, string namefilter = null, int startAfter = -1)
        {
            for (int scan = startAfter + 1; scan < nodes.Count; scan++)
            {
                if (namefilter == null || nodes[scan].Name == namefilter)
                    return scan;
            }

            return -1;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var children = Current.Children().ToList();

            if (!children.Any()) return false;

            var found = nextMatch(children, nameFilter);

            if (found == -1) return false;

            _siblings = children;
            _index = found;
            return true;
        }

        public bool MoveToNext(string nameFilter = null)
        {
            var found = nextMatch(_siblings, nameFilter, _index);

            if (found == -1) return false;

            _index = found;
            return true;
        }

        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            if (type == typeof(ElementNodeToElementNavAdapter))
                return new[] { this };
            else
                return Current.Annotations(type);
        }
    }



}