/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.FluentPath.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.ElementModel
{

    public class ElementNode : IElementNode
    {
        public IElementNode Parent { get; private set; }

        public string Name { get; private set; }

        public string Path { get; private set; }

        public string TypeName { get; private set; }

        public object Value { get; private set; }


        public IList<IElementNode> Children { get; private set; }

        public IElementNavigator ToNavigator()
        {
            return new ElementNodeNavigator(this);
        }

        private ElementNode(string name, object value, string typeName, params ElementNode[] children)
        {
            Name = name;
            Value = value;
            TypeName = typeName;
            Children = children;

            int childIndex = 0;
            foreach (var c in children)
            {
                c.Parent = this;
                c.Path = Path + ".{0}[{1}]".FormatWith(c.Name, childIndex);
                childIndex++;
            }
        }

        public static ElementNode Valued(string name, object value, params ElementNode[] children)
        {
            return new ElementNode(name, value, null, children);
        }

        public static ElementNode Valued(string name, object value, string typeName, params ElementNode[] children)
        {
            return new ElementNode(name, value, typeName, children);
        }

        public static ElementNode Node(string name, params ElementNode[] children)
        {
            return new ElementNode(name, null, null, children);
        }

        public static ElementNode Node(string name, string typeName, params ElementNode[] children)
        {
            return new ElementNode(name, null, typeName, children);
        }
    }
}
