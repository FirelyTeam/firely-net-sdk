using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel.Adapters
{

#pragma warning disable 612, 618
    /// <summary>
    /// An abstract class on which to build adapter components that adapt a node-based
    /// representation of FHIR data to a navigator based representation.
    /// </summary>
    /// <remarks>This is a highly specialized class used by the API to create backwards-compatibility
    /// adapters to enable working side-by-side with node-based and navigator-based representations
    /// of data. Should not normally be used for other purposes.</remarks>
    public abstract class BaseNodeToNavAdapter : IExceptionSource, IAnnotated, IElementNavigator
    {
        private IList<ITypedElement> _siblings;
        private int _index;
        protected ITypedElement Current => _siblings[_index];

        protected void Initialize(ITypedElement root)
        {
            if (root is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);

            _siblings = new List<ITypedElement> { root };
            _index = 0;
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public object Value => Current.Value;

        public string Type => Current.InstanceType;

        public string Name => Current.Name;

        public string Location => Current.Location;

        public IEnumerable<object> Annotations(Type type) => Current.Annotations(type);

        protected abstract BaseNodeToNavAdapter NewClone();

        public IElementNavigator Clone()
        {
            var clone = NewClone();
            clone._siblings = _siblings;
            clone._index = _index;
            clone.ExceptionHandler = ExceptionHandler;

            return clone;
        }

        protected abstract IList<ITypedElement> GetChildren();

        public virtual bool MoveToFirstChild(string nameFilter = null)
        {
            var children = GetChildren();
            if (!children.Any()) return false;

            var found = nextMatch(children, nameFilter);
            if (found == -1) return false;

            _siblings = children;
            _index = found;

            return true;
        }

        public virtual bool MoveToNext(string nameFilter = null)
        {
            var found = nextMatch(_siblings, nameFilter, _index);

            if (found == -1) return false;

            _index = found;
            return true;
        }
    
        private int nextMatch(IList<ITypedElement> nodes, string namefilter = null, int startAfter = -1)
        {
            for (int scan = startAfter + 1; scan < nodes.Count; scan++)
            {
                if (namefilter == null || nodes[scan].Name == namefilter)
                    return scan;
            }

            return -1;
        }
    }
#pragma warning restore 612, 618
}
