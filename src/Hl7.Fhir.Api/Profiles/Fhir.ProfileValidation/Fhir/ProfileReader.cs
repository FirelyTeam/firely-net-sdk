/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace Fhir.Profiling
{
    public class ProfileReader
    {
        XmlNamespaceManager ns;

        public void ReadCardinality(Validation.Element element, XPathNavigator node)
        {
            Cardinality cardinality = new Cardinality();
            cardinality.Min = node.SelectSingleNode("f:definition/f:min/@value", ns).Value;
            cardinality.Max = node.SelectSingleNode("f:definition/f:max/@value", ns).Value;
            element.Cardinality = cardinality;
        }

        public void ReadPath(Validation.Element element, XPathNavigator node)
        {
            string s = node.SelectSingleNode("f:path/@value", ns).Value;
            s = Regex.Replace(s, @"\[x\]", "");
            List<string> parts = s.Split('.').ToList();
            element.Path = parts.ToList();
            element.Name = parts.Last();
        }
        public void ReadReference(Validation.Element element, XPathNavigator node)
        {
            XPathNavigator urinode = node.SelectSingleNode("f:definition/f:binding/f:referenceResource/f:reference/@value", ns);
            if (urinode != null)
            { 
                element.ReferenceUri = urinode.ToString();
            }
        }

        public void ReadStructure(Validation.Element element, XPathNavigator node)
        {
            XPathNavigator n = node.SelectSingleNode("f:definition/f:type/f:code/@value", ns);
            element.StructureName = (n != null) ? n.Value : null;
        }

        public void ReadConstraints(Validation.Element element, XPathNavigator node)
        {
            foreach (XPathNavigator nav in node.Select("f:definition/f:constraint", ns))
            {
                Constraint constraint = new Constraint();
                constraint.Name = nav.SelectSingleNode("f:name/@value", ns).Value;
                constraint.XPath = nav.SelectSingleNode("f:xpath/@value", ns).Value;
                element.Constraints.Add(constraint);
            }
        }

        public void BuildExpressions(Validation.Element element)
        {
            element.XPath = "./" + string.Join("/", element.Path.Skip(1).Select(p => "f:" + p));
            // todo: how to pre-compile expressions without adding 
        }

        public Validation.Element ReadElement(XPathNavigator node)
        {
            Validation.Element element = new Validation.Element();
            ReadPath(element, node);
            ReadReference(element, node);
            ReadStructure(element, node);
            ReadCardinality(element, node);
            ReadConstraints(element, node);
            BuildExpressions(element);
            return element;
        }

        public Validation.Structure ReadStructure(XPathNavigator node)
        {
            Validation.Structure structure = new Validation.Structure();
            structure.Name = node.SelectSingleNode("f:type/@value", ns).Value;
            XPathNodeIterator xElements = node.Select("f:element", ns);
            foreach (XPathNavigator xElement in xElements)
            {
                structure.Elements.Add(ReadElement(xElement));
            }
            structure.BuildHierarchy();
            return structure;
        }

        public Validation.Profile ReadProfile(XPathNavigator node)
        {
            Validation.Profile profile = new Validation.Profile();
            XPathNodeIterator xStructures = node.Select("f:Profile/f:structure", ns);
            foreach (XPathNavigator xStructure in xStructures)
            {
                profile.Add(ReadStructure(xStructure));
            }
            return profile;
        }

        public Validation.Profile Read(IXPathNavigable navigable)
        {
            XPathNavigator navigator = navigable.CreateNavigator();
            ns = FhirNamespace.GetManager(navigator);
            return ReadProfile(navigator);
        }

        public Validation.ValueSet ReadValueSet(XPathNavigator node)
        {
            Validation.ValueSet valueset = new Validation.ValueSet();
            
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

        public List<Validation.ValueSet> ReadValueSets(IXPathNavigable navigable)
        {
            List<Validation.ValueSet> valuesets = new List<Validation.ValueSet>();
            XPathNavigator navigator = navigable.CreateNavigator();
            ns = FhirNamespace.GetManager(navigator);

            foreach(XPathNavigator node in navigator.Select("atom:feed/atom:entry/atom:content/f:ValueSet", ns))
            {
                Validation.ValueSet set = ReadValueSet(node);
                if (set != null) valuesets.Add(set);
            }
            return valuesets;
        }
    }

}
