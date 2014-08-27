using Fhir.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhir.Profiling
{
    public class SpecificationSealer
    {
        private Specification specification;

        private SpecificationSealer(Specification specification)
        {
            this.specification = specification;
        }

        private void _linkBindings()
        {
            foreach (Element element in specification.Elements)
            {
                if (element.BindingUri != null)
                    element.Binding = specification.GetValueSetByUri(element.BindingUri);
            }
        }

        private bool _tryLinkToParent(Structure structure, Element element)
        {
            Element parent = specification.ParentOf(structure, element);
            if (parent != null)
            {
                parent.Children.Add(element);
                return true;
            }
            return false;
        }

        private void _linkElements(Structure structure)
        {
            foreach (Element e in structure.Elements)
            {
                _tryLinkToParent(structure, e);
            }
        }

        public void _linkElements()
        {
            foreach (Structure structure in specification.Structures)
            {
                _linkElements(structure);
            }
        }
        
        private IEnumerable<TypeRef> unlinkedTypeRefs
        {
            get
            {
                return specification.Elements.SelectMany(e => e.TypeRefs).Where(r => r.Structure == null);
            }
        }

        private void _linkTypeRefs()
        {
            foreach (TypeRef typeref in unlinkedTypeRefs)
            {
                typeref.Structure = specification.GetStructureByName(typeref.Code);
            }
        }

        private void _linkElementRefs()
        {
            foreach (Structure structure in specification.Structures)
            {
                foreach (Element element in structure.Elements)
                {
                    if (element.ElementRef == null && element.ElementRefPath != null)
                        element.ElementRef = specification.GetElementByPath(structure, element.ElementRefPath);
                }
            }
        }

        private void _compileConstraints()
        {
            foreach (Constraint c in specification.Constraints)
            {
                if (!c.Compiled)
                    ConstraintCompiler.Compile(c);
            }
        }

        public void _addNameSpace(Element element)
        {
            if (element.HasTypeRef)
            {
                TypeRef typeref = element.TypeRefs.FirstOrDefault(t => t.Structure != null);
                if (typeref != null)
                {
                    element.NameSpacePrefix = typeref.Structure.NameSpacePrefix;
                }
            }

            if (element.NameSpacePrefix == null)
                element.NameSpacePrefix = FhirNamespaceManager.Fhir;
        }

        public void _addNameSpaces()
        {
            foreach (Element element in specification.Elements.Where(e => e.NameSpacePrefix == null))
            {
                _addNameSpace(element);
            }
        }

        private void seal()
        {
            _linkBindings();
            _linkElements();
            _linkTypeRefs();
            _linkElementRefs();
            _compileConstraints();
            _addNameSpaces();
            specification.Sealed = true;
        }

        /// <summary>
        /// Make the profile complete and usable by linking all internal structures and perform precompilation
        /// </summary>
        public static void Seal(Specification specification)
        {
            SpecificationSealer sealer = new SpecificationSealer(specification);
            sealer.seal();
        }
    }
}
