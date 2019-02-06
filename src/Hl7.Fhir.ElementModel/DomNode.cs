/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public class DomNode<T> : IAnnotatable where T:DomNode<T>
    {
        public string Name { get; set; }

        protected List<T> children = new List<T>();

        internal IEnumerable<T> ChildrenByName(string name = null) =>
            name == null ? children : children.Where(c => c.Name == name);

        public T Parent { get; protected set; }

        public DomNodeList<T> this[string name] => new DomNodeList<T>(ChildrenByName(name));

        public T this[int index] => children[index];

        public string Location
        {
            get
            {
                if (Parent != null)
                {
                    //TODO: Slow - but since we'll change the use of this property to informational 
                    //(i.e. for error messages), it may not be necessary to improve it.
                    var basePath = Parent.Location;
                    int myIndex = Parent.ChildrenByName(Name).ToList().IndexOf((T)this);
                    return $"{basePath}.{Name}[{myIndex}]";
                }
                else
                    return Name;
            }
        }

        private Lazy<List<object>> _annotations = new Lazy<List<object>>(() => new List<object>());
        protected List<object> annotations { get { return _annotations.Value; } }

        protected bool HasAnnotations => _annotations.IsValueCreated;

        public void AddAnnotation(object annotation)
        {
            annotations.Add(annotation);
        }

        public void RemoveAnnotations(Type type)
        {
            annotations.RemoveOfType(type);
        }
    }


    public class DomNodeList<T> : IEnumerable<T> where T : DomNode<T>
    {
        private IList<T> _wrapped;

        internal DomNodeList(IEnumerable<T> nodes)
        {
            _wrapped = nodes.ToList();
        }

        public T this[int index] => _wrapped[index];

        public DomNodeList<T> this[string name] =>
            new DomNodeList<T>(_wrapped.SelectMany(c => c.ChildrenByName(name)));

        public int Count => _wrapped.Count;

        public bool Contains(T item) => _wrapped.Contains(item);

        public IEnumerator<T> GetEnumerator() => _wrapped.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _wrapped.GetEnumerator();
    }

}
