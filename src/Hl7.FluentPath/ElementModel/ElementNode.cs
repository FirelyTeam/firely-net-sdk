/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Furore.Support;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.ElementModel
{

    public class ElementNode : IElementNode
    {
        public IElementNode Parent { get; private set; }

        public string Name { get; private set; }

        public string Path
        {
            get
            {
                var myIndex = Parent != null ? Parent.Children.Where(c=> c.Name == Name).ToList().IndexOf(this) : -1;
                var root = Parent != null ? Parent.Path + "." : "";
                root += Name;

                if(myIndex >= 0)
                    root += "[{0}]".FormatWith(myIndex);

                return root;
            }

        }

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

            foreach (var c in children) c.Parent = this;
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
