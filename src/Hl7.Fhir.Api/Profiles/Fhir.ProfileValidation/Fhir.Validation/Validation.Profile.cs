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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Fhir.Profiling
{
    public struct Cardinality
    {
        public string Min;
        public string Max;
        public bool InRange(int x)
        {
            int min = Convert.ToInt16(Min);
            if (x < min)
                return false;

            if (Max == "*")
                return true;

            int max = Convert.ToInt16(Max);
            if (x > max)
                return false;

            return true;
        }
        public XPathExpression MinExpression;
        public XPathExpression MaxExpression;
        public override string ToString()
        {
            return Min + ".." + Max;
        }
    }

    public struct Constraint
    {
        public string Name;
        public string XPath;
    }

    public class Validation
    {

        public class ValueSet
        {
            public string System { get; set; }
            public List<string> codes = new List<string>();
            public bool Contains(string code)
            {
                return codes.Exists(c => c == code);
            }
        }

        public class Element
        {
            public string Name;

            private string ElementPathToXPath(string path)
            {
                path = Regex.Replace(path, @"\[x\]", "");
                string[] parts = path.Split('.').Select(p => "f:" + p).ToArray();
                return "../" + string.Join("/", parts);

            }
            public List<string> Path { get; set; }
            public Structure Structure;
            public string StructureName;
            public List<Element> Children = new List<Element>();
            public string XPath;
            public Cardinality Cardinality;
            public List<Constraint> Constraints = new List<Constraint>();
            public string ReferenceUri;
            public ValueSet Reference;
            public override string ToString()
            {
                return string.Format("{0} ({1})", string.Join(".", Path), Cardinality);
            }
        }

        public class Structure
        {
            public string Name { get; set; }
            public List<Element> Elements = new List<Element>();

            public Element Root
            {
                get
                {
                    return Elements.FirstOrDefault(e => e.Path.Count == 1);
                }
            }
            public Element FindParent(Element element)
            {
                List<string> p = element.Path.ToList();
                p.RemoveAt(p.Count - 1);

                Element parent = Elements.Find(e => e.Path.SequenceEqual(p));
                return parent;
            }
            public void BuildHierarchy()
            {
                foreach (Element e in Elements)
                {
                    Element parent = FindParent(e);
                    if (parent != null)
                    {
                        parent.Children.Add(e);
                    }
                }
            }
            public override string ToString()
            {
                return string.Format("{0} ({1} elements)", Name, Elements.Count);
            }
        }

        public class Profile
        {
            public List<ValueSet> ValueSets = new List<ValueSet>();
            public List<Structure> Structures = new List<Structure>();
            
            public void Add(Structure structure)
            {
                Structures.Add(structure);
            }
            public void Add(List<Structure> structures)
            {
                addReferencesTo(structures);
                this.Structures.AddRange(structures);
            }
            public void Add(IEnumerable<ValueSet> valuesets)
            {
                this.ValueSets.AddRange(valuesets);
            }
            public void Add(Profile profile)
            {
                Add(profile.Structures);
                Add(profile.ValueSets);
            }

            private void addReferencesTo(List<Structure> structures)
            {
                foreach (Structure s in structures)
                foreach (Element e in s.Elements)
                {
                    e.Structure = this.GetStructureByName(e.StructureName);
                    if (e.ReferenceUri != null)
                    { 
                        e.Reference = this.GetValueSetByUri(e.ReferenceUri);
                    }
                }
            }

            public Structure GetStructureByName(string name)
            {
                foreach (Structure s in Structures)
                {
                    if (s.Name == name)
                        return s;
                }
                return null;
            }
            public ValueSet GetValueSetByUri(string uri)
            {
                foreach(ValueSet valueset in ValueSets)
                {
                    if (valueset.System == uri)
                        return valueset;
                }
                return null;
            }

        }

    }

}
