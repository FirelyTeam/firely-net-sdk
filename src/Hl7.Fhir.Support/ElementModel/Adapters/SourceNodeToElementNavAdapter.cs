using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel.Adapters
{
    internal class SourceNodeToElementNavAdapter : IElementNavigator, IAnnotated, IExceptionSource
    {
        private IList<ISourceNode> _siblings;
        private int _index;

        public ISourceNode Current
        {
            get { return _siblings[_index]; }
        }

        public SourceNodeToElementNavAdapter(ISourceNode sourceNode)
        {
            _siblings = new List<ISourceNode> { sourceNode };
            _index = 0;

            if (sourceNode is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        public SourceNodeToElementNavAdapter() { }  // for clone

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Current.Name;

        public string Type => Current.GetResourceType();

        public object Value => Current.Text;

        public string Location => Current.Location;

        public IElementNavigator Clone() =>
            new SourceNodeToElementNavAdapter()
            {
                _siblings = this._siblings,
                _index = this._index,

                ExceptionHandler = this.ExceptionHandler,
            };

        private int nextMatch(IList<ISourceNode> nodes, string namefilter = null, int startAfter = -1)
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
            if (type == typeof(SourceNavToElementNavAdapter))
                return new[] { this };
            else if (Current is IAnnotated annotated)
                return annotated.Annotations(type);
            else
                return Enumerable.Empty<object>();
        }
    }
}