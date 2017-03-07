/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;

namespace Hl7.Fhir.ElementModel
{

    public class ElementNode : IElementNode
    {
        public IElementNode Parent { get; set;  }
        public IList<IElementNode> Children { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }

        public string Location => this.BuildPath();

        private ElementNode(string name, object value, string type, params ElementNode[] children)
        {
            Name = name;
            Value = value;
            Type = type;
            Children = children;

            foreach (var c in children) c.Parent = this;
        }

        public static ElementNode Valued(string name, object value, params ElementNode[] children)
        {
            return new ElementNode(name, value, null, children);
        }

        public static ElementNode Valued(string name, object value, string type, params ElementNode[] children)
        {
            return new ElementNode(name, value, type, children);
        }

        public static ElementNode Node(string name, params ElementNode[] children)
        {
            return new ElementNode(name, null, null, children);
        }

        public static ElementNode Node(string name, string type, params ElementNode[] children)
        {
            return new ElementNode(name, null, type, children);
        }
    }
}
