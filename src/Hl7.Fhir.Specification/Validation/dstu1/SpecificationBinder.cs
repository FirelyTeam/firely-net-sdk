
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.XPath;

namespace Hl7.Fhir.Validation
{
    internal class SpecificationBinder
    {
        private SpecificationWorkspace specification;

        private SpecificationBinder(SpecificationWorkspace specification)
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

        private void _linkElements()
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
                Structure structure = specification.GetStructureByUri(typeref.Profile);
                typeref.Structure = structure;
            }
        }

        private void _linkElementRef(Structure structure, Element element)
        {
            if (element.ElementRef == null && element.ElementRefPath != null)
            {
                element.ElementRef = specification.GetElementByPath(structure, element.ElementRefPath);
            }
        }

        private void _linkElementRefs()
        {
            foreach (Structure structure in specification.Structures)
            {
                foreach (Element element in structure.Elements)
                {
                    _linkElementRef(structure, element);
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
      
        private void bind()
        {
            _linkBindings();
            _linkElements();
            _linkTypeRefs();
            _linkElementRefs();
            _compileConstraints();
            specification.Sealed = true;
        }

        /// <summary>
        /// Make the profile complete and usable by binding all internal structures and perform precompilation
        /// </summary>
        public static void Bind(SpecificationWorkspace specification)
        {
            SpecificationBinder binder = new SpecificationBinder(specification);
            binder.bind();
        }
    }
}
