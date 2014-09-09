/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using Fhir.XPath;

namespace Hl7.Fhir.Profiling
{
    public class SpecificationReader
    {
        int id = 0; // id to give to each element (id++) in order to identify them during debugging

        XmlNamespaceManager ns;

        List<Slicing> Slicings = new List<Slicing>();
        
        public string Value(XPathNavigator node, string xpath)
        {
            XPathNavigator n = node.SelectSingleNode(xpath, ns);
            if (n == null)
                throw new InvalidStructureException("Missing value in profile: {0}", xpath);
            return n.Value;
        }

        public string OptionalValue(XPathNavigator node, string xpath)
        {
            XPathNavigator n = node.SelectSingleNode(xpath, ns);
            return (n != null) ? n.Value : null;
        }

        public void ReadCardinality(Element element, XPathNavigator node)
        {
            Cardinality cardinality = new Cardinality();
            cardinality.Min = OptionalValue(node, "f:definition/f:min/@value");
            cardinality.Max = OptionalValue(node, "f:definition/f:max/@value");
            element.Cardinality = cardinality;
        }

        public Path ReadElementPath(XPathNavigator node)
        {
            string s = node.SelectSingleNode("f:path/@value", ns).Value;
            return new Path(s);
        }

        public void ReadFixedValue(Element element, XPathNavigator node)
        {
            string xpath;
            if (element.Multi)
            {
                xpath = string.Format("f:definition/*[starts-with(name(),'value')]/@value");
                string value = OptionalValue(node, xpath);
                element.FixedValue = value;
            }
            else
            {
                xpath = string.Format("f:definition/f:valueCoding/f:value/@value");
                string value = OptionalValue(node, xpath);
                element.FixedValue = value;
            }
        }

        public void ReadSliceValue(Element element, XPathNavigator node)
        {
            if (!element.Sliced)
                return;

            if (element.IsExtension) 
            {
                string xpath = string.Format("f:definition/f:type/f:profile/@value");
                string value = OptionalValue(node, xpath);
                element.SliceValue = value;
            }
            else if (element.Multi)
            {
                element.SliceValue = element.FixedValue;
            }
        }

        public void ReadReference(Element element, XPathNavigator node)
        {
            element.BindingUri = OptionalValue(node, "f:definition/f:binding/f:referenceResource/f:reference/@value");
        }

        public TypeRef ReadTypeRef(Element element, XPathNavigator node)
        {
            
            string code = Value(node, "f:code/@value");
            string profileUri = OptionalValue(node, "f:profile/@value");
            
            return new TypeRef(code, profileUri);
        }

        public void ReadTypeRefs(Element element, XPathNavigator node)
        {
            var iterator = node.Select("f:definition/f:type", ns);
            foreach(XPathNavigator n in iterator)
            {
                TypeRef typeref = ReadTypeRef(element, n);
                element.TypeRefs.Add(typeref);
            }
        }

        public void ReadElementRef(Element element, XPathNavigator node)
        {
            element.ElementRefPath = OptionalValue(node, "f:definition/f:nameReference/@value");
        }

        public void ReadConstraints(Element element, XPathNavigator node)
        {
            foreach (XPathNavigator nav in node.Select("f:definition/f:constraint", ns))
            {
                Constraint constraint = new Constraint();
                
                XPathNavigator xName = nav.SelectSingleNode("f:name/@value", ns);
                string key = OptionalValue(nav, "f:key/@value");
                constraint.Name = (xName != null) ? xName.Value : element.Path+", Key:"+key;

                constraint.XPath = nav.SelectSingleNode("f:xpath/@value", ns).Value;
                constraint.HumanReadable = OptionalValue(nav, "f:human/@value");
                element.Constraints.Add(constraint);
            }   
        }

        public bool IsSliceDefinitionNode(XPathNavigator node)
        {
            bool sliced = OptionalValue(node, "f:slicing") != null;
            return sliced;
        }

        private Slicing readSlicing(XPathNavigator node)
        {
            Slicing slicing = new Slicing();
            slicing.Discriminator = new Path(Value(node, "f:slicing/f:discriminator/@value"));
            slicing.Path = ReadElementPath(node);
            //slicing.Rule = (SlicingRules)Enum.Parse(typeof(SlicingRules), Value(node, "f:slicing/f:rules/@value"));
            //slicing.Ordered = (Value(node, "f:slicing/f:ordered/@value").ToLower() == "true");
            return slicing;
        }

        internal Slicing GetSlicingForElement(Element element)
        {
            Slicing slicing = Slicings.FirstOrDefault(s => s.Path.Equals(element.Path));
            return slicing;
        }

        public void PreReadSlices(Structure structure, XPathNodeIterator xNodes)
        {
            foreach (XPathNavigator node in xNodes)
            {
                if (IsSliceDefinitionNode(node))
                {
                    Slicing s = readSlicing(node);
                    Slicings.Add(s);
                }
            }
        }

        public void InjectSlice(Element element, XPathNavigator node)
        {
            Slicing slicing = GetSlicingForElement(element);
            if (slicing != null)
            {
                if (IsSliceDefinitionNode(node))
                {
                    // todo: SlicingRules op de slicing-element-null implementeren
                }
                else
                {
                    element.Discriminator = slicing.Discriminator;
                    element.Slice = ++slicing.Count;
                }
            }

        }

        public void ReadElementRepresentation(Element element, XPathNavigator node)
        {
            string rep = OptionalValue(node, "f:representation/@value");
            element.Representation = (rep == "xmlAttr") ? Representation.Attribute : Representation.Element;
        }

        public void ReadElementDefinition(Element element, XPathNavigator node)
        {

            element.Path = ReadElementPath(node);
            element.Name = element.Path.ElementName;
            ReadElementRepresentation(element, node);
            ReadReference(element, node);
            ReadTypeRefs(element, node);
            ReadElementRef(element, node);
            ReadCardinality(element, node);
            ReadConstraints(element, node);
            ReadFixedValue(element, node);
            InjectSlice(element, node);
            ReadSliceValue(element, node);
        }
     
        public Element ReadElement(Structure structure, XPathNavigator node)
        {
            Element element = new Element();
            element.ID = id++;
            
            ReadElementDefinition(element, node);
            return element;
        }

        public void ReadStructureElements(Structure structure, XPathNodeIterator xElements)
        {
            foreach (XPathNavigator xElement in xElements)
            {
                Element element = ReadElement(structure, xElement);
                structure.Elements.Add(element);
            }
        }

        public Structure ReadStructure(XPathNavigator node)
        {
            Structure structure = new Structure();
            structure.Type = node.SelectSingleNode("f:type/@value", ns).Value;
            structure.NameSpacePrefix = FhirNamespaceManager.Fhir;
            PreReadSlices(structure, node.Select("f:element", ns));
            ReadStructureElements(structure, node.Select("f:element", ns));
            return structure;
        }

        public List<Structure> ReadStructures(XPathNavigator node)
        {
            List<Structure> structures = new List<Structure>();
            foreach (XPathNavigator xStructure in node.Select("f:structure", ns))
            {
                Structure s = ReadStructure(xStructure);
                structures.Add(s);
            }
            return structures;
        }

        public List<Structure> ReadProfiles(IXPathNavigable root)
        {
            XPathNavigator navigator = root.CreateNavigator();
            ns = FhirNamespaceManager.CreateManager(navigator);
            List<Structure> structures = new List<Structure>();

            foreach (XPathNavigator node in navigator.Select("//f:Profile", ns))
            {
                structures.AddRange(ReadStructures(node));
            }
            return structures;
        }

        
        public ValueSet ReadValueSet(XPathNavigator node)
        {
            ValueSet valueset = new ValueSet();
            
            XPathNavigator systemnode = node.SelectSingleNode("f:define/f:system/@value", ns);

            if (systemnode == null) return null;
            valueset.System = systemnode.ToString();

            XPathNodeIterator iterator = node.Select("f:define/f:concept", ns);
            foreach(XPathNavigator n in iterator)
            {
                string s = n.SelectSingleNode("f:code/@value", ns).ToString();
                valueset.codes.Add(s);
            }
            return valueset;
        }

        public List<ValueSet> ReadValueSets(IXPathNavigable navigable)
        {
            List<ValueSet> valuesets = new List<ValueSet>();
            XPathNavigator navigator = navigable.CreateNavigator();
            ns = FhirNamespaceManager.CreateManager(navigator);

            foreach(XPathNavigator node in navigator.Select("atom:feed/atom:entry/atom:content/f:ValueSet", ns))
            {
                ValueSet set = ReadValueSet(node);
                if (set != null) valuesets.Add(set);
            }
            return valuesets;
        }   

    }

    

}
