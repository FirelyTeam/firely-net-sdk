using Hl7.Fhir.Model;
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

namespace Hl7.Fhir.Specification.Model
{

    public enum Representation { Element, Attribute };

    public class Element
    {
        internal int ID;
        public string Name;
        public Path Path { get; set; }
        public Segment TailSegment
        {
            get
            {
                return Path.Tail;
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
        public Hl7.Fhir.Model.Element FixedValue { get; set; }
        public Hl7.Fhir.Model.Element PatternValue { get; set; }
        public string SliceValue { get; set; }
        public Cardinality Cardinality;
        public List<Constraint> Constraints = new List<Constraint>();
        public bool IsPrimitive { get; set; }
        public Representation Representation { get; set; }
        //public string PrimitivePattern {get; set;} // RegExPattern to validate a primite against (only in case of IsPrimitive)
        public Func<string,bool> PrimitiveValidator;

        public string BindingUri;
        public ValueSet Binding;
        public string NameSpacePrefix { get; set; }
        public int Slice { get; set; }
        public Path Discriminator { get; set; }

        public bool IsValidPrimitive(string value)
        {
            if (PrimitiveValidator != null)
            {
                return PrimitiveValidator(value);
            }
            else return true;
        }

        public bool IsExtension
        {
            get
            {
                return HasTypeRef && TypeRefs.Exists(t => t.Code == "Extension");
            }
        }
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
    
        
        public string GetXPathFilter()
        {
            string xpath;

            if (TailSegment.Multi)
            {
                xpath = string.Format("*[starts-with(name(),'{0}')]", TailSegment.Name);
            }
            else if (Representation == Representation.Element)
            {
                xpath = string.Format("{0}:{1}", this.NameSpacePrefix, TailSegment.Name);
            }
            else 
            {
                xpath = string.Format("@{0}", TailSegment.Name);
            }

                 
            if (Sliced)
            {
                xpath += string.Format("[{0}]", DiscriminatorXPath());
            }
            return xpath;
        }

        public string DiscriminatorXPath()
        {
            //Segment segment = Discriminator.Tail;
            Element element = this;
            List<string> pieces = new List<string>();
            foreach(Segment segment in Discriminator.Segments)
            {
                element = element.FindChild(segment.Name);

                if (element == null)
                {
                    return "false"; 
                    // if the fixed value element can't be found, give back an all discriminating xpath.
                }

                string s;
                if (segment != Discriminator.Tail)
                {
                    s = string.Format("{0}:{1}", element.NameSpacePrefix, element.Name);
                }
                else
                {
                    if (element.Representation == Representation.Element)
                    {
                        s = string.Format("{0}:{1}/@value='{2}'", element.NameSpacePrefix, element.Name, this.SliceValue);
                    }
                    else
                    {
                        s = string.Format("@{0}='{1}'", element.Name, this.SliceValue);
                    }
                }
                pieces.Add(s);
            }
            return string.Join("/", pieces);
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

        public Element FindChild(string name)
        {
            Element element = Children.FirstOrDefault(c => c.NodeNameIsMatch(name));
            if (element == null)
            {
                foreach(TypeRef typref in this.TypeRefs.Where(t => t.Structure != null))
                {
                    element = typref.Structure.Root.FindChild(name);
                    if (element != null)
                        break;
                }
            }
            return element;
        }

        public Element FindChild(Path path)
        {
            Element element = this;
            foreach (Segment segment in path.Segments)
            {
                element = element.FindChild(segment.Name);
                if (element == null) break;
            }
            return element;
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
