/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
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

    public class ElementNode : IElementNode, IAnnotated, IAnnotatable
    {
        public IElementNode Parent { get; set;  }
        public IList<IElementNode> Children { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }

        public string Location
        {
            get
            {
                if (Parent != null)
                {
                    //TODO: Slow - but since we'll change the use of this property to informational 
                    //(i.e. for error messages), it may not be necessary to improve it.
                    var basePath = Parent.Location;
                    int myIndex = Parent.Children.Where(c => c.Name == Name).ToList().IndexOf(this);
                    return $"{basePath}.{Name}[{myIndex}]";
                }
                else
                    return Name;            
            }
        }

        private ElementNode(string name, object value, string type, params ElementNode[] children)
        {
            Name = name;
            Value = value;
            Type = type;
            Children = children;

            if(children != null)
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

        public IElementNode Clone()
        {
            var copy = new ElementNode(Name, Value, Type);

            if (_annotations.IsValueCreated)
                copy.annotations.AddRange(annotations);

            return copy;
        }


        private Lazy<List<object>> _annotations = new Lazy<List<object>>(() => new List<object>());
        private List<object> annotations { get { return _annotations.Value; } }

        public IEnumerable<object> Annotations(Type type)
        {
            return annotations.OfType(type);
        }

        public void AddAnnotation(object annotation)
        {
            annotations.Add(annotation);
        }

        public void RemoveAnnotations(Type type)
        {
            annotations.RemoveOfType(type);
        }
    }
}
