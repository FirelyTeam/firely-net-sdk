/*
* Copyright (c) 2014, Firely (info@fire.ly) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.XPath
{
    internal class NavigatorState : IEqualityComparer<NavigatorState>, IEquatable<NavigatorState>
    {
        public NavigatorState(JProperty pos)
        {
            Element = pos;
        }

        private NavigatorState()
        {
        }

        public JProperty Element { get; private set; }
        public int? ChildPos { get; set; }
        public int? AttributePos { get; set; }
        public int? NamespacePos { get; set; }

        // Transient variable containing cached list of children,
        // so we save time recompiling these when navigating back and forth
        private List<JProperty> _children;

        public IEnumerable<JProperty> Children
        {
            get
            {
                if (_children == null) _children = Element.ElementChildren().ToList();
                return _children;
            }
        }
 
        public NavigatorState Copy()
        {
            var result = new NavigatorState();
            result.Element = Element;
            result.ChildPos = ChildPos;
            result.AttributePos = AttributePos;
            result.NamespacePos = NamespacePos;

            return result;
        }

        public override string ToString()
        {
            if (Element == null) return ("[Uninitialized]");

            var result = new StringBuilder();

            result.Append(Element.Name);
            if(ChildPos != null) result.AppendFormat("[At Child {0}]", ChildPos.Value);
            if (AttributePos != null) result.AppendFormat("[At Attr {0}]", AttributePos.Value);
            if (NamespacePos != null) result.AppendFormat("[At Nspc {0}]", NamespacePos.Value);
            return result.ToString();
        }

        public bool Equals(NavigatorState other)
        {
            return Equals(this, other);
        }

        public bool Equals(NavigatorState x, NavigatorState y)
        {
            return x.Element.Name == y.Element.Name && x.ChildPos == y.ChildPos &&
                    x.AttributePos == y.AttributePos && x.NamespacePos == y.NamespacePos;
        }

        public int GetHashCode(NavigatorState obj)
        {
            if(obj == null) return 0;

            return obj.Element.Name.GetHashCode() ^
                obj.ChildPos ?? 0 ^
                obj.AttributePos ?? 0 ^
                obj.NamespacePos ?? 0;
        }
    }
}