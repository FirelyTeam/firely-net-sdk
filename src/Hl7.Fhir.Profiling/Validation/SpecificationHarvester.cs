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
using Hl7.Fhir.Model;
using Hl7.Fhir.XPath;

namespace Hl7.Fhir.Profiling
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

        private void HarvestBinding(Profile.ElementDefinitionComponent source, Element target)
        {

            if (source.Binding != null)
            {
                var reference = source.Binding.Reference;

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

        private void HarvestFixedValue(Profile.ElementDefinitionComponent source, Element target)
        {
            target.FixedValue = (source.Value != null) 
                ? source.Value.ToString()
                : null;
        }

        private void HarvestConstraints(Profile.ElementDefinitionComponent source, Element target)
        {
            if (source.Constraint == null)
                return;

            foreach(Profile.ElementDefinitionConstraintComponent c in source.Constraint)
            {
                Constraint constraint = new Constraint();
                constraint.Name = c.Name ?? c.Key;
                constraint.XPath = c.Xpath;
                constraint.HumanReadable = c.Human;
                target.Constraints.Add(constraint);
            }
        }

        private void HarvestCardinality(Profile.ElementDefinitionComponent source, Element target)
        {
            Cardinality cardinality = new Cardinality();
            cardinality.Min = source.Min.ToString();
            cardinality.Max = source.Max;
            target.Cardinality = cardinality;
        }

        private void HarvestElementRef(Profile.ElementDefinitionComponent source, Element target)
        {
            target.ElementRefPath = source.NameReference;
        }

        private TypeRef HarvestTypeRef(Profile.TypeRefComponent type)
        {
            TypeRef typeref = new TypeRef(type.Code, type.Profile);
                //builder.CreateTypeRef(type.Code, type.Profile);
            // todo: now the typerefs are duplicated. so resolving deduplication must be done else where.
            return typeref;
        }

        private void HarvestTypeRefs(Profile.ElementDefinitionComponent source, Element target)
        {
            if (source.Type == null)
                return;

            foreach (var type in source.Type)
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

        private void HarvestElementDefinition(Profile.ElementDefinitionComponent source, Element target)
        {
            if (source != null)
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

            HarvestElementDefinition(source.Definition, target);
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

        private void fixUris(Structure structure, Uri uri)
        {
            UriHelper.SetStructureIdentification(structure, uri);
            List<TypeRef> typerefs = structure.Elements.SelectMany(e => e.TypeRefs).ToList();
            foreach (TypeRef t in typerefs)
            {
                UriHelper.SetTypeRefIdentification(structure, t);
            }
        }

      
        public Structure HarvestStructure(Profile.ProfileStructureComponent source, Uri uri)
        {
            Structure target = new Structure();
            target.Name = source.Name;
            target.Type = source.Type;
            target.NameSpacePrefix = FhirNamespaceManager.Fhir;
            PrepareSlices(source);
            HarvestElements(source, target);
            fixUris(target, uri);
            return target;
        }

        public Structure HarvestExtensionDefn(Profile.ProfileExtensionDefnComponent source)
        {
            Structure target = new Structure();
            target.Name = source.Code;
            Element element = new Element();
            element.Name = source.Code;
            HarvestElementDefinition(source.Definition, element);
            
            target.Elements.Add(element);
            return target;
        }


       


        public void HarvestProfileExtensions(Profile source)
        {
            foreach(var defn in source.ExtensionDefn)
            {
                HarvestExtensionDefn(defn);
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
