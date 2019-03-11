/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel.Adapters
{
#pragma warning disable 612, 618
    internal class SourceNodeToElementNavAdapter : IElementNavigator, IAnnotated, IExceptionSource
    {
        private IList<ISourceNode> _siblings;
        private int _index;

        public ISourceNode Current
        {
            get { return _siblings[_index]; }
        }

        public SourceNodeToElementNavAdapter(ISourceNode node)
        {
            if (node == null) throw Error.ArgumentNull(nameof(node));

            _siblings = new List<ISourceNode> { node };
            _index = 0;

            if (node is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private SourceNodeToElementNavAdapter() { }  // for clone

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Current.Name;

        public string Type => Current.GetResourceTypeIndicator();

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

        public IEnumerable<object> Annotations(Type type) => Current.Annotations(type);
    }
#pragma warning restore 612, 618
}