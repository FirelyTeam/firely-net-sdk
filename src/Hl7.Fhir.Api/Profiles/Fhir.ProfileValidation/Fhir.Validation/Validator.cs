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

namespace Fhir.Profiling
{
    public class Validator
    {
        private XmlNamespaceManager ns;
        private Validation.Profile Profile = new Validation.Profile();
        public Report Report = new Report();

        public Validator(Validation.Profile profile)
        {
            this.Profile = profile;
        }

        public bool Exists(XPathNavigator nav, string xpath)
        {
            XPathNavigator node = nav.SelectSingleNode(xpath, ns);
            return (node != null);
        }

        public Validation.ValueSet FindValueSet(string uri)
        {
            foreach(Validation.ValueSet valueset in Profile.ValueSets)
            {
                if (valueset.System == uri)
                    return valueset;
            }
            return null;
        }

        public bool ValidateCoding(string name, string code)
        {
            Validation.ValueSet valueset = FindValueSet(name);
            if (valueset != null)
            {
                return valueset.Contains(code);
            }
            else return false;
        }

        public void ValidateCode(Validation.Element element, XPathNavigator node)
        {
            
            string code = node.SelectSingleNode("@value").ToString();
            bool exists = element.Reference.Contains(code);
            Report.Add("Code", element.Name + " - " + element.Reference.System, exists);
        }

        public void ValidateCardinality(Validation.Element element, XPathNavigator node)
        {
            string xpath = "./f:" + element.Name;
            string expression;
            if (element.Cardinality.Min != "0")
            {
                expression = string.Format("boolean(count({0}) < {1})", xpath, element.Cardinality.Min);
                bool valid = !(bool)node.Evaluate(expression, ns);
                Report.Add("cardinality", element.Name, valid);
            }
            else // if min fails, no need to test max 

                if (element.Cardinality.Max != "*")
                {
                    expression = string.Format("boolean(count({0}) > {1})", xpath, element.Cardinality.Max);
                    bool valid = !(bool)node.Evaluate(expression, ns);
                    Report.Add("cardinality", element.Name, valid);
                }
                else
                {
                    // 0..*
                    Report.Add("cardinality", element.Name, true);
                }
        }

        public int Count(Validation.Element element, XPathNavigator root)
        {
            string xpath = string.Format("count({0})", element.XPath);
            int count = Convert.ToInt32(root.Evaluate(xpath, ns));
            return count;
        }

        public void ValidateConstraint(Constraint constraint, XPathNavigator node)
        {
            bool valid = (bool)node.Evaluate(constraint.XPath, ns);
            Report.Add("constraint", constraint.Name, valid);
        }

        public void ValidateConstraints(Validation.Element element, XPathNavigator node)
        {

            foreach (Constraint constraint in element.Constraints)
            {
                ValidateConstraint(constraint, node);
            }
        }

        public void ValidateStructure(Validation.Element element, XPathNavigator node)
        {
            if (element.Structure != null)
            {
                ValidateStructure(element.Structure, node);
            }
        }

        public void ValidateElement(Validation.Element element, XPathNavigator node)
        {
            if (element.Reference != null)
            {
                ValidateCode(element, node);
                return;
            }
            ValidateConstraints(element, node);
            ValidateStructure(element, node);
            foreach (Validation.Element childElement in element.Children)
            {
                ValidateCardinality(childElement, node);

                foreach (XPathNavigator childNode in Matches(childElement, node))
                {
                    ValidateElement(childElement, childNode);
                }
            }
        }

        public XPathNodeIterator Matches(Validation.Element element, XPathNavigator node)
        {
            string xpath = string.Format("./f:{0}", element.Name);
            XPathNodeIterator nodes = node.Select(xpath, ns);
            return nodes;
        }

        public void ValidateStructure(Validation.Structure structure, XPathNavigator root)
        {
            Report.Add(structure.Name, "Start", true);
            ValidateElement(structure.Root, root);
            Report.Add(structure.Name, "End", true);
        }

        public void Validate(IXPathNavigable navigable)
        {
            Report.Clear();
            XPathNavigator root = navigable.CreateNavigator();
            ns = FhirNamespace.GetManager(root);

            string name = root.Name;
            Validation.Structure structure = Profile.GetStructureByName(name);
            ValidateStructure(structure, root);
        }
    }
}
