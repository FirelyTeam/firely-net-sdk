/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhir.Profiling
{
    public class Element
    {
        internal int ID;
        public string Name;
        public Path Path { get; set; }
        public Segment TailSegment
        {
            get
            {
                return Path.Last;
            }
        }
        public List<TypeRef> TypeRefs = new List<TypeRef>();
        public bool HasTypeRef
        {
            get
            {
                return TypeRefs.Count > 0;
            }
        }
        public string ElementRefPath { get; set; }
        public Element ElementRef { get; set; }
        public List<Element> Children = new List<Element>();
        public string FixedValue { get; set; }
        public Cardinality Cardinality;
        public List<Constraint> Constraints = new List<Constraint>();
        public bool IsPrimitive { get; set; }
        public bool IsAttribute { get; set; }
        public string PrimitivePattern {get; set;} // RegExPattern to validate a primite against (only in case of IsPrimitive)
        public string BindingUri;
        public ValueSet Binding;
        public string NameSpacePrefix { get; set; }
        public int Slice { get; set; }
        public Path Discriminator { get; set; }

        public bool Multi
        {
            get
            {
                return Path.Segments.Last().Multi;
            }
        }
        public bool IsRoot
        {
            get
            {
                return Path.Count == 1;
            }
        }
        public bool Sliced { get { return Discriminator != null; } }
    
        public string XPath
        {
            get
            {
                string xpath;

                if (TailSegment.Multi)
                {
                    xpath = string.Format("./*[starts-with(name(),'{0}')]", TailSegment.Name);
                }
                else
                {
                    xpath = string.Format("./{0}:{1}", this.NameSpacePrefix, TailSegment.Name);
                }

                if (Sliced)
                {
                    xpath += string.Format("[{0}:{1}/@value='{2}']", this.NameSpacePrefix, Discriminator, FixedValue);
                }

                return xpath;
            }
        }

        public bool NodeNameIsMatch(string name)
        {
            if (TailSegment.Multi)
            {
                return name.StartsWith(this.Name);
            }
            else
            {
                return name == Name;
            }
        }

        public bool HasChild(string name)
        {
            return this.Children.FirstOrDefault(c => c.NodeNameIsMatch(name)) != null;

        }
        
        public bool HasChildren
        {
            get
            {
                return Children.Count > 0;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Path, Cardinality);
        }
    }

    
}
