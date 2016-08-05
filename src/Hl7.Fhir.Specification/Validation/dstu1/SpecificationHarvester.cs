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
using Hl7.Fhir.XPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Specification.Model;

namespace Hl7.Fhir.Validation
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
                element.Slice = ++slicing.Count;
            }
        }

        private void HarvestBinding(Hl7.Fhir.Model.Profile.ElementDefinitionComponent source, Element target)
        {

            if (source.Binding != null)
            {
                var reference = source.Binding.Reference;

                if (reference is Hl7.Fhir.Model.ResourceReference)
                {
                    // todo: dit deel is nog niet getest.
                    target.BindingUri = (reference as Hl7.Fhir.Model.ResourceReference).Url.ToString();
                }
                else if (reference is Hl7.Fhir.Model.FhirUri)
                {
                    target.BindingUri = (reference as Hl7.Fhir.Model.FhirUri).Value;
                }
            }

        }

        private void HarvestFixedValue(Hl7.Fhir.Model.Profile.ElementDefinitionComponent source, Element target)
        {
            target.FixedValue = null;

            if (source.Fixed != null)
                target.FixedValue = source.Fixed;

            if (source.Pattern != null)
                target.PatternValue = source.Pattern;
        }

        private void HarvestConstraints(Hl7.Fhir.Model.Profile.ElementDefinitionComponent source, Element target)
        {
            if (source.Constraint == null)
                return;

            foreach (Hl7.Fhir.Model.Profile.ElementDefinitionConstraintComponent c in source.Constraint)
            {
                Constraint constraint = new Constraint();
                constraint.Name = c.Name ?? c.Key;
                constraint.XPath = c.Xpath;
                constraint.HumanReadable = c.Human;
                target.Constraints.Add(constraint);
            }
        }

        private void HarvestCardinality(Hl7.Fhir.Model.Profile.ElementDefinitionComponent source, Element target)
        {
            Cardinality cardinality = new Cardinality();
            cardinality.Min = source.Min.ToString();
            cardinality.Max = source.Max;
            target.Cardinality = cardinality;
        }

        private void HarvestElementRef(Hl7.Fhir.Model.Profile.ElementDefinitionComponent source, Element target)
        {
            target.ElementRefPath = source.NameReference;
        }

        private TypeRef HarvestTypeRef(Hl7.Fhir.Model.Profile.TypeRefComponent type)
        {
            TypeRef typeref = new TypeRef(type.Code, type.Profile);
                //builder.CreateTypeRef(type.Code, type.Profile);
            // todo: now the typerefs are duplicated. so resolving deduplication must be done else where.
            return typeref;
        }

        private void HarvestTypeRefs(Hl7.Fhir.Model.Profile.ElementDefinitionComponent source, Element target)
        {
            if (source.Type == null)
                return;

            foreach (var type in source.Type)
            {
                target.TypeRefs.Add(HarvestTypeRef(type));
            }
        }

        private void HarvestSlicing(Hl7.Fhir.Model.Profile.ElementComponent source, Element target)
        {
            InjectSlice(target);
        }

        private Representation TransformRepresentation(Hl7.Fhir.Model.Profile.ElementComponent source)
        {
            if (source.Representation == null)
                return Representation.Element;

            return (source.Representation.Contains(Hl7.Fhir.Model.Profile.PropertyRepresentation.XmlAttr))
                ? Representation.Attribute
                : Representation.Element;
        }

        private void HarvestElementDefinition(Hl7.Fhir.Model.Profile.ElementDefinitionComponent source, Element target)
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

        private void HarvestElement(Hl7.Fhir.Model.Profile.ElementComponent source, Element target)
        {
            target.Path = new Path(source.Path);
            target.Name = target.Path.ElementName; //source.Name; 
            target.Representation = TransformRepresentation(source);

            HarvestElementDefinition(source.Definition, target);
            HarvestSlicing(source, target); 
        }

        private Element HarvestElement(Hl7.Fhir.Model.Profile.ElementComponent source)
        {
            Element target = new Element();
            HarvestElement(source, target);
            return target;
        }

        private void HarvestElements(Hl7.Fhir.Model.Profile.ProfileStructureComponent source, Structure target)
        {
            if (source.Snapshot == null) throw Error.Argument("source", "Structure must have a differential representation");
            
            foreach(Hl7.Fhir.Model.Profile.ElementComponent component in source.Snapshot.Element)
            {
                if (component.Slicing == null)
                {
                    var element = HarvestElement(component);
                    target.Elements.Add(element);
                }
                /*if (!target.Root.HasElements)
                    target.Root.IsPrimitive == true; */
                // of moet structure zelf een IsPrimitive krijgen?
            }
        }

        private Slicing PrepareSlice(Hl7.Fhir.Model.Profile.ElementComponent source)
        {
            Slicing slicing = new Slicing();
            slicing.Path = new Path(source.Path);

            //TODO: Support multiple discriminators
            if (source.Slicing.Discriminator.Count() > 1)
                throw Error.NotImplemented("Multiple discriminators not yet implemented");
            
            //TODO: Support discriminator-less matching!
            slicing.Discriminator = new Path(source.Slicing.Discriminator.First());
            return slicing;
        }

        public void PrepareSlices(Hl7.Fhir.Model.Profile.ProfileStructureComponent source)
        {
            if (source.Snapshot == null) throw Error.Argument("source", "Structure must have a differential representation");
            foreach (Hl7.Fhir.Model.Profile.ElementComponent e in source.Snapshot.Element)
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

        public Structure HarvestStructure(Hl7.Fhir.Model.Profile.ProfileStructureComponent source, Uri uri)
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

        public Structure HarvestExtensionDefn(Hl7.Fhir.Model.Profile.ProfileExtensionDefnComponent source)
        {
            Structure target = new Structure();
            target.Name = source.Name;
            Element element = new Element();
            element.Name = source.Name;

            //TODO: Add support for complex extensions
            if (source.Element.Count > 0)
                throw new NotImplementedException("Complex extensions are not supported by the harvester");

            HarvestElementDefinition(source.Element[0].Definition, element);
            
            target.Elements.Add(element);
            return target;
        }

        public void HarvestProfileExtensions(Hl7.Fhir.Model.Profile source)
        {
            foreach(var defn in source.ExtensionDefn)
            {
                HarvestExtensionDefn(defn);
            }
        }

        public ValueSet HarvestValueSet(Hl7.Fhir.Model.ValueSet source, Uri system)
        {
            ValueSet valueset = new ValueSet(system, source);
            return valueset;
            /*
            // todo: This now only works with "defines". 
            valueset.System = system.ToString();

            if (source.Define != null)
            {
                foreach (var concept in source.Define.Concept)
                {
                    valueset.codes.Add(concept.Code);
                }
            }
            return valueset;
            */
        }
             
    }
}
