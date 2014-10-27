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
using System.Xml;
using System.Xml.XPath;

namespace Hl7.Fhir.Specification.Model
{
    public class Vector
    {
        public Structure Structure;
        public Vector Origin;
        public Element Element;
        public XPathNavigator Node;
        public XmlNamespaceManager NSM;

        private Vector()
        {

        }

        public static Vector Create(Structure structure, XPathNavigator node, XmlNamespaceManager nsm)
        {
            Vector vector = new Vector();
            vector.Structure = structure;
            vector.Element = (structure != null) ? structure.Root : null;
            vector.Node = node;
            vector.NSM = nsm;
            vector.Origin = vector; 
            return vector;
        }

        public Vector Clone()
        {
            Vector clone = new Vector();
            clone.Structure = this.Structure;
            clone.Element = this.Element;
            clone.Node = this.Node.CreateNavigator();
            clone.NSM = this.NSM;
            clone.Origin = this.Origin;
            return clone;
        }

        public Vector MoveTo(Element element)
        {
            Vector clone = this.Clone();
            clone.Element = element;
            return clone;
        }

        public Vector MoveTo(Element element, XPathNavigator node)
        {
            Vector clone = this.Clone();
            clone.Element = element;
            clone.Node = node;
            return clone;
        }
        
        public Vector MoveTo(Structure structure)
        {
            Vector clone = this.Clone();
            clone.Structure = structure;
            clone.Element = structure.Root;
            return clone;
        }

        public Vector MoveTo(XPathNavigator node)
        {
            Vector clone = this.Clone();
            clone.Node = node;
            return clone;
        }

        public bool Evaluate(Constraint constraint)
        {
            bool valid = (bool)Node.Evaluate(constraint.Expression);
            return valid;
        }
        
        public bool Exists(string xpath)
        {
            XPathNavigator node = Node.SelectSingleNode(xpath, NSM);
            return (node != null);
        }

        public int Count()
        {
            string xpath = Element.GetXPathFilter();
            XPathNodeIterator iterator = Node.Select(xpath, NSM);
            return iterator.Count;
        }

        public string GetValue(string xpath)
        {
            return Node.SelectSingleNode(xpath, NSM).ToString();
        }

        public string GetContent()
        {
            if (Element.Representation == Representation.Element)
            {
                return this.GetValue("@value"); 
            }
            else
            {
                string attr = string.Format("@{0}", Element.Name);
                return this.GetValue(attr);
            }

        }
        
        public string NodePath
        {
            get
            {
                XPathNavigator n = Node.CreateNavigator();
                string s = n.Name;
                while (!n.IsSamePosition(Origin.Node) && n.MoveToParent())
                {
                    if (!string.IsNullOrEmpty(n.Name))
                        s = n.Name + "." + s;
                }
                return s;
            }
        }
        
        public IEnumerable<Vector> ElementChildren
        {
            get
            {
                foreach (Element element in Element.Children)
                {
                    yield return this.MoveTo(element);
                }
            }
        }

        public IEnumerable<XPathNavigator> GetNodeAttributes()
        {
            XPathNavigator node = this.Node.CreateNavigator();
            bool ok = node.MoveToFirstAttribute();

            while (ok)
            {
                yield return node;
                ok = node.MoveToNextAttribute();
            }

        }

        public IEnumerable<Vector> NodeAttributes
        {
            get
            {
                foreach(XPathNavigator node in GetNodeAttributes())
                {
                    yield return this.MoveTo(node);
                }
            }
        }

        public bool NodeHasAttribute(string name)
        {
            return GetNodeAttributes().Any(n => n.Name == name);
        }

        public bool NodeHasValue()
        {
            return GetNodeAttributes().Any(n => n.Name == "value");
        }

        public IEnumerable<Vector> MatchingNodes
        {
            get
            {
                string xfilter = this.Element.GetXPathFilter();

                foreach (XPathNavigator node in Node.Select(xfilter, NSM))
                {
                    yield return this.MoveTo(node);
                }
            }
        }

        public IEnumerable<Vector> NodeChildren
        {
            get
            {
                foreach (XPathNavigator node in Node.SelectChildren(XPathNodeType.Element))
                {
                    yield return this.MoveTo(node);
                }

            }
        }
        
        public string ExtractTypeFromNode()
        {
            if (Node.Name.StartsWith(Element.Name))
            {
                return Node.Name.Remove(0, Element.Name.Length);
            }
            else throw new Exception("Data Type cannot be extracted from node on multivalued element");
        }
        
        public bool Match(TypeRef typeref)
        {
            if (Element.Multi)
            {
                string code = ExtractTypeFromNode();
                return (typeref.Code.ToLower() == code.ToLower());
            }
            else return true;
        }

        public IEnumerable<Vector> ElementStructures
        {
            get
            {
                foreach (TypeRef typeref in Element.TypeRefs)
                {
                    if (typeref.Structure != null && Match(typeref))
                    {
                        yield return this.MoveTo(typeref.Structure);
                    }
                }
            }
        }
        
        public bool ElementHasChild(string name, Representation representation)
        {
            Element element = Element.Children.FirstOrDefault(c => c.Name == name);
            if (element != null)
            {
                return (element.Representation == representation);
            }
            else
            {
                return false;
            }

        }

        public override string ToString()
        {
            return string.Format("{0} × {1}", Element, NodePath);
        }
    }

}
