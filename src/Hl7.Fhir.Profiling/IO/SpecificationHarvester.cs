/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using Fhir.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

namespace Fhir.Profiling.IO
{

    internal class SpecificationHarvester
    {

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

        private void HarvestBinding(Profile.ElementComponent source, Element target)
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

        private void HarvestFixedValue(Profile.ElementComponent source, Element target)
        {
            target.FixedValue = (source.Definition.Value != null) 
                ? source.Definition.Value.ToString()
                : null;
        }

        private void HarvestConstraints(Profile.ElementComponent source, Element target)
        {
            if (source.Definition.Constraint == null)
                return;

            foreach(Profile.ElementDefinitionConstraintComponent c in source.Definition.Constraint)
            {
                Constraint constraint = new Constraint();
                constraint.Name = c.Name ?? c.Key;
                constraint.XPath = c.Xpath;
                constraint.HumanReadable = c.Human;
                target.Constraints.Add(constraint);
            }
        }

        private void HarvestCardinality(Profile.ElementComponent source, Element target)
        {
            Cardinality cardinality = new Cardinality();
            cardinality.Min = source.Definition.Min.ToString();
            cardinality.Max = source.Definition.Max;
            target.Cardinality = cardinality;
        }

        private void HarvestElementRef(Profile.ElementComponent source, Element target)
        {
            target.ElementRefPath = source.Definition.NameReference;
        }

        private TypeRef HarvestTypeRef(Profile.TypeRefComponent type)
        {
            TypeRef typeref = new TypeRef(type.Code, type.Profile);
                //builder.CreateTypeRef(type.Code, type.Profile);
            // todo: now the typerefs are duplicated. so resolving deduplication must be done else where.
            return typeref;
        }

        private void HarvestTypeRefs(Profile.ElementComponent source, Element target)
        {
            if (source.Definition.Type == null)
                return;

            foreach (var type in source.Definition.Type)
            {
                target.TypeRefs.Add(HarvestTypeRef(type));
            }
        }
       
        private void HarvestSlicing(Profile.ElementComponent source, Element target)
        {
            InjectSlice(target);
        }

        private Representation TransformRepresentation(Profile.ElementComponent source)
        {
            if (source.Representation == null)
                return Representation.Element;

            return (source.Representation.Contains(Profile.PropertyRepresentation.XmlAttr))
                ? Representation.Attribute
                : Representation.Element;
        }

        private void HarvestElementDefinition(Profile.ElementComponent source, Element target)
        {
            if (source.Definition != null)
            {
                HarvestBinding(source, target);
                HarvestTypeRefs(source, target);
                HarvestElementRef(source, target);
                HarvestCardinality(source, target);
                HarvestConstraints(source, target);
                HarvestFixedValue(source, target);
            }
        }

        private void HarvestElement(Profile.ElementComponent source, Element target)
        {
            target.Path = new Path(source.Path);
            target.Name = target.Path.ElementName; //source.Name; 
            target.Representation = TransformRepresentation(source);

            HarvestElementDefinition(source, target);
            HarvestSlicing(source, target); 
        }

        private Element HarvestElement(Profile.ElementComponent source)
        {
            Element target = new Element();
            HarvestElement(source, target);
            return target;
        }

        private void HarvestElements(Profile.ProfileStructureComponent source, Structure target)
        {
            foreach(var element in source.Element)
            {
                target.Elements.Add(HarvestElement(element));
            }
        }

        private Slicing PrepareSlice(Profile.ElementComponent source)
        {
            Slicing slicing = new Slicing();
            slicing.Path = new Path(source.Path);
            slicing.Discriminator = new Path(source.Slicing.Discriminator);
            return slicing;
        }

        public void PrepareSlices(Profile.ProfileStructureComponent source)
        {
            foreach(Profile.ElementComponent e in source.Element)
            {
                if (e.Slicing != null)
                {
                    Slicing s = PrepareSlice(e);
                    Slicings.Add(s);
                }
            }
        }

        private Structure HarvestStructure(Profile.ProfileStructureComponent source)
        {
            Structure target = new Structure();
            target.Type = source.Type;
            target.NameSpacePrefix = FhirNamespaceManager.Fhir;
            PrepareSlices(source);
            HarvestElements(source, target);
            return target;
        }

        public IEnumerable<Structure> HarvestStructures(Profile source)
        {
            foreach (var structure in source.Structure)
            {
                yield return HarvestStructure(structure);
            }
        }

        public ValueSet HarvestValueSet(Hl7.Fhir.Model.ValueSet source)
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
