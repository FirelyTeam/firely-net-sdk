/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.ElementModel
{
    public class DomNode<T> : IAnnotatable where T : DomNode<T>
    {
        public string Name { get; set; }

        private List<T> _childList = null;

        protected List<T> ChildList
        {
            get => LazyInitializer.EnsureInitialized(ref _childList, () => new());
            set => _childList = value;
        }

        internal IEnumerable<T> ChildrenInternal(string name = null) =>
            name == null ? ChildList : ChildList.Where(c => c.Name.MatchesPrefix(name));

        public T Parent { get; protected set; }

        public DomNodeList<T> this[string name] => new DomNodeList<T>(ChildrenInternal(name));

        public T this[int index] => ChildList[index];

        #region << Annotations >>
        private AnnotationList _annotations = null;
        protected AnnotationList AnnotationsInternal => LazyInitializer.EnsureInitialized(ref _annotations, () => new());

        protected bool HasAnnotations => _annotations is not null && !_annotations.IsEmpty;

        public void AddAnnotation(object annotation)
        {
            AnnotationsInternal.AddAnnotation(annotation);
        }

        public void RemoveAnnotations(Type type)
        {
            AnnotationsInternal.RemoveAnnotations(type);
        }
        #endregion
    }
}
