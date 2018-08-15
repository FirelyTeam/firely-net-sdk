/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel.Adapters
{
    internal class TypedElementToElementNavAdapter : IElementNavigator, IAnnotated, IExceptionSource
    {
        private IList<ITypedElement> _siblings;
        private int _index;
        public ITypedElement Current =>  _siblings[_index]; 

        public TypedElementToElementNavAdapter(ITypedElement element)
        {
            _siblings = new List<ITypedElement> { element };
            _index = 0;

            if (element is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private TypedElementToElementNavAdapter() { }  // for clone

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Current.Name;

        public string Type => Current.InstanceType;

        public string Location => Current.Location;

        public object Value => Current.Value;

        public IElementNavigator Clone() =>
            new TypedElementToElementNavAdapter
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

        IEnumerable<object> IAnnotated.Annotations(Type type) => Current.Annotations(type);
    }
}