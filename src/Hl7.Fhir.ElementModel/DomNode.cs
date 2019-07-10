/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public class DomNode<T> : IAnnotatable where T : DomNode<T>
    {
        public string Name { get; set; }

        protected List<T> ChildList = new List<T>();

        internal IEnumerable<T> ChildrenInternal(string name = null) =>
            name == null ? ChildList : ChildList.Where(c => c.Name == name);

        public T Parent { get; protected set; }

        public DomNodeList<T> this[string name] => new DomNodeList<T>(ChildrenInternal(name));

        public T this[int index] => ChildList[index];

        private readonly Lazy<List<object>> _annotations = new Lazy<List<object>>(() => new List<object>());
        protected List<object> AnnotationsInternal { get { return _annotations.Value; } }

        protected bool HasAnnotations =>
            _annotations.IsValueCreated == true && _annotations.Value.Any();

        public void AddAnnotation(object annotation)
        {
            AnnotationsInternal.Add(annotation);
        }

        public void RemoveAnnotations(Type type)
        {
            AnnotationsInternal.RemoveOfType(type);
        }
    }
}
