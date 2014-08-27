/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using Fhir.IO;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhir.Profiling.IO
{
    public class SpecificationLoader
    {
        private SpecificationBuilder builder;

        List<Slicing> Slicings = new List<Slicing>();

        internal Slicing GetSlicingForElement(Element element)
        {
            Slicing slicing = Slicings.FirstOrDefault(s => s.Path.Equals(element.Path));
            return slicing;
        }

        internal void InjectSlice(Element element)
        {
            Slicing slicing = GetSlicingForElement(element);
            if (slicing != null)
            {
               element.Discriminator = slicing.Discriminator;
               slicing.Count++;
               
            }
        }

        public SpecificationLoader(SpecificationBuilder builder)
        {
            this.builder = builder;
        }

        private void LoadBinding(Profile.ElementComponent source, Element target)
        {
            if (source.Definition.Binding != null)
            {
                var reference = source.Definition.Binding.Reference;

                if (reference is ResourceReference)
                {
                    // todo: dit deel is nog niet getest.
                    target.BindingUri = (reference as ResourceReference).Url.ToString();
                }
                else if (reference is FhirUri)
                {
                    target.BindingUri = (reference as FhirUri).Value;
                }
            }
            
        }

        private void LoadFixedValue(Profile.ElementComponent source, Element target)
        {
            target.FixedValue = (source.Definition.Value != null) 
                ? source.Definition.Value.ToString()
                : null;
        }

        private void LoadConstraints(Profile.ElementComponent source, Element target)
        {
            if (source.Definition.Constraint == null)
                return;

            foreach(Profile.ElementDefinitionConstraintComponent c in source.Definition.Constraint)
            {
                Constraint constraint = new Constraint();
                constraint.Name = c.Name;
                constraint.XPath = c.Xpath;
                constraint.HumanReadable = c.Human;
                target.Constraints.Add(constraint);
            }
        }

        private void LoadCardinality(Profile.ElementComponent source, Element target)
        {
            Cardinality cardinality = new Cardinality();
            cardinality.Min = source.Definition.Min.ToString();
            cardinality.Max = source.Definition.Max;
            target.Cardinality = cardinality;
        }

        private void LoadElementRef(Profile.ElementComponent source, Element target)
        {
            target.ElementRefPath = source.Definition.NameReference;
        }

        private TypeRef GetTypeRef(Profile.TypeRefComponent type)
        {
            TypeRef typeref = builder.CreateTypeRef(type.Code, type.Profile);
            return typeref;
        }

        private void LoadTypeRefs(Profile.ElementComponent source, Element target)
        {
            if (source.Definition.Type == null)
                return;

            foreach (var type in source.Definition.Type)
            {
                target.TypeRefs.Add(GetTypeRef(type));
            }
        }

       
        private void LoadSlicing(Profile.ElementComponent source, Element target)
        {
            InjectSlice(target);
        }

        private void LoadElement(Profile.ElementComponent source, Element target)
        {
            target.Path = new Path(source.Path);
            target.Name = target.Path.ElementName; //source.Name; 
            LoadBinding(source, target);
            LoadTypeRefs(source, target);
            LoadElementRef(source, target);
            LoadCardinality(source, target);
            LoadConstraints(source, target);
            LoadFixedValue(source, target);
            LoadSlicing(source, target); 
        }

        private Element GetElement(Profile.ElementComponent source)
        {
            Element target = new Element();
            LoadElement(source, target);
            return target;
        }

        private void LoadElements(Profile.ProfileStructureComponent source, Structure target)
        {
            foreach(var element in source.Element)
            {
                target.Elements.Add(GetElement(element));
            }
        }

        private Slicing ReadSlice(Profile.ElementComponent source)
        {
            Slicing slicing = new Slicing();
            slicing.Path = new Path(source.Path);
            slicing.Discriminator = new Path(source.Slicing.Discriminator);
            return slicing;
        }
        public void PreReadSlices(Profile.ProfileStructureComponent source)
        {
            foreach(Profile.ElementComponent e in source.Element)
            {
                if (e.Slicing != null)
                {
                    Slicing s = ReadSlice(e);
                    Slicings.Add(s);
                }
            }
        }

        private Structure GetStructure(Profile.ProfileStructureComponent source)
        {
            Structure target = new Structure();
            target.Type = source.Type;
            target.NameSpacePrefix = FhirNamespaceManager.Fhir;
            PreReadSlices(source);
            LoadElements(source, target);
            return target;
        }

        public IEnumerable<Structure> LoadStructures(Profile source)
        {
            foreach (var structure in source.Structure)
            {
                yield return GetStructure(structure);
            }
        }

        public ValueSet LoadValueSet(Hl7.Fhir.Model.ValueSet source)
        {
            ValueSet valueset = new ValueSet();
            // todo: This now only works with "defines". 

            if (source.Define != null)
            {
                foreach (var concept in source.Define.Concept)
                {
                    valueset.codes.Add(concept.Code);
                }
            }
            return valueset;
        }
             
    }
}
