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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Hl7.Fhir.Specification.Model
{

    public enum SlicingRules { OpenAtEnd };
    
    /// <summary>
    /// FHIR Profile. Basis for validation of FHIR Resources.
    /// Profile should be constructed from a ProfileBuilder
    /// </summary>
    public class SpecificationWorkspace
    {
        private List<Structure> _structures = new List<Structure>();
        private List<ValueSet> _valueSets = new List<ValueSet>();

        public bool Sealed { get; internal set; }

        internal SpecificationWorkspace()
        {
            Sealed = false;
        }

        private void AssertNotSealed()
        {
            if (Sealed) throw new InvalidOperationException("Profile is sealed");
        }

        internal void Add(ValueSet valueset)
        {
            AssertNotSealed();
            _valueSets.Add(valueset);

        }

        internal void Add(IEnumerable<ValueSet> valuesets)
        {
            AssertNotSealed();
            _valueSets.AddRange(valuesets);
        }

        internal void Add(IEnumerable<Structure> structures)
        {
            AssertNotSealed();
            _structures.AddRange(structures);
        }

        internal void Add(Structure structure)
        {
            AssertNotSealed();
            _structures.Add(structure);
        }

        public Structure GetStructureByName(string name)
        {
            return Structures.FirstOrDefault(s => s.Type == name);
        }

        public Structure GetStructureByUri(Uri uri)
        {
            return Structures.FirstOrDefault(s => UriHelper.Equal(s.Uri, uri));
        }

        public IEnumerable<Structure> StructuresWithName(string name)
        {
            return Structures.Where(s => s.Type == name);
        }

        public ValueSet GetValueSetByUri(Uri uri)
        {
            foreach(ValueSet valueset in ValueSets)
            {
                if (valueset.System == uri) return valueset;
                
            }
            return null;
        }

        public ValueSet GetValueSetByUri(string uri)
        {
            return GetValueSetByUri(new Uri(uri));
        }
       
        public Element GetElementByPath(Structure structure, string path)
        {
            return structure.Elements.FirstOrDefault(element => element.Path.ToString() == path);
        }

        public Structure StructureOf(Element element)
        {
            foreach (Structure structure in Structures)
            {
                if (structure.Elements.Contains(element))
                    return structure;
            }
            return null;
        }

        public Element ParentOf(Structure structure, Element element)
        {
            Path path = element.Path.Parent();
            Element parent = structure.Elements.Find(e => e.Path.Equals(path));
            return parent;
        }

        // todo: test resource boundary path following
        /// <summary>
        /// Follows the path into child elements and into other structures. The latter hasn't been tested yet
        /// </summary>
        /// <param name="origin">The element where the follow starts </param>
        /// <param name="path">The path to follow</param>
        /// <returns>The element (if found) at the end of the path, otherwise returns null</returns>
        public Element FollowPath(Element origin, Path path)
        {
            Segment segment = path.Segments.FirstOrDefault();

            if (segment == null)
            {
                // we have arrived at the matching path tail.
                return origin;
            }
            else
            {
                Element child = null;

                if (origin.Children.Count > 0)
                {
                    child = origin.Children.FirstOrDefault(e => segment.Match(e.Name));
                }
                else 
                {
                    TypeRef t = origin.TypeRefs.FirstOrDefault();
                    Structure structure = (t != null) ? t.Structure : null;
                    Element parent = (structure != null) ? structure.Root : null;

                    if (parent != null)
                    {
                        child = parent.Children.FirstOrDefault(e => segment.Match(e.Name));
                    }

                }
                
                if (child != null)
                {
                    Element target = FollowPath(child, path.ForChild());
                    return target;
                }

                return null;
            }
        }

        public Element ParentOf(Element element)
        {
            Structure structure = StructureOf(element);
            return ParentOf(structure, element);
        }

        public TypeRef FindTypeRef(TypeRef typeref)
        {
            return TypeRefs.FirstOrDefault(t => t.Equals(typeref));
        }

        public IEnumerable<Structure> Structures
        {
            get
            {
                return _structures;
            }
        }

        public IEnumerable<ValueSet> ValueSets
        {
            get
            {
                return _valueSets;
            }
        }

        public IEnumerable<Element> Elements
        {
            get
            {
                return Structures.SelectMany(s => s.Elements);
            }
        }

        public IEnumerable<Constraint> Constraints
        {
            get
            {
                return Elements.SelectMany(e => e.Constraints);
            }
        }

        public IEnumerable<TypeRef> TypeRefs
        {
            get
            {
                return Elements.SelectMany(e => e.TypeRefs);
            }
        }

        public IEnumerable<Uri> UnresolvedTypeRefUris()
        {
            return
                TypeRefs
                .Where(t => t.Unresolved)
                .Select(t => t.Uri)
                .Distinct();
        }

        public IEnumerable<Uri> ValueSetUris
        {
            get
            {
                return ValueSets.Select(v => v.System);
            }
        }

        public IEnumerable<Uri> BindingUris
        {
            get
            {
                return Elements
                    .Where(e => e.BindingUri != null)
                    .Distinct()
                    .Select(e => new Uri(e.BindingUri));
                    
            }
        }

        
    }
   
}
