using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Validation.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class StructureDefinitionSchemaFacade : IStructureDefinitionSchema
    {
        private readonly StructureDefinition _structureDef;
        private readonly List<ElementDefinition> _elementDefs;

        public StructureDefinitionSchemaFacade(List<ElementDefinition> elementDefs, StructureDefinition structureDef)
        {
            _elementDefs = elementDefs;
            _structureDef = structureDef;
            _lazyElements = new Lazy<ICollectionFacade<IStructureDefinitionElement>>(() => CreateElements());
        }

        private readonly Lazy<ICollectionFacade<IStructureDefinitionElement>> _lazyElements;
        public ICollectionFacade<IStructureDefinitionElement> Elements => _lazyElements.Value;

        public void Commit()
        {            
            var defs = GetElementDefinitions();
            _elementDefs.Clear();
            _elementDefs.AddRange(defs);
        }

        #region WRITE
        private IEnumerable<ElementDefinition> GetElementDefinitions()
        {
            return GetElementDefinitions(Elements);
        }

        private IEnumerable<ElementDefinition> GetElementDefinitions(ICollectionFacade<IStructureDefinitionElement> elements)
        {
            return elements.Cast<StructureDefinitionElementFacade>().SelectMany(element => GetElementDefinitions(element));
        }

        private IEnumerable<ElementDefinition> GetElementDefinitions(StructureDefinitionElementFacade element)
        {
            if (element.ElementDefinition.Slicing is object)
            {
                yield return element.ElementDefinition;
            }

            foreach(var sliceElementDef in GetElementDefinitions(element.Slices))
            {
                yield return sliceElementDef;
            }
        }

        private IEnumerable<ElementDefinition> GetElementDefinitions(ICollectionFacade<IStructureDefinitionSlice> slices)
        {
            return slices.Cast<StructureDefinitionSliceFacade>().SelectMany(slice => GetElementDefinitions(slice));
        }

        private IEnumerable<ElementDefinition> GetElementDefinitions(StructureDefinitionSliceFacade slice)
        {
            yield return slice.ElementDefinition;

            foreach (var elementDef in GetElementDefinitions(slice.Elements))
            {
                yield return elementDef;
            }
        }
        #endregion

        #region #READ
        private ICollectionFacade<IStructureDefinitionElement> CreateElements()
        {
            var navigator = new ElementDefinitionNavigator(_elementDefs, _structureDef);
            return new CollectionFacade<IStructureDefinitionElement, ElementDefinition>(elementDef => new StructureDefinitionElementFacade(elementDef), CreateElements(navigator));
        }

        private IEnumerable<IStructureDefinitionElement> CreateElements(ElementDefinitionNavigator navigator)
        {
            if (navigator.MoveToFirstChild())
            {
                do
                {
                    yield return CreateElement(navigator);
                }
                while (navigator.MoveToNext());

                navigator.MoveToParent();
            }
        }

        private IStructureDefinitionElement CreateElement(ElementDefinitionNavigator navigator)
        {
            var elementDef = navigator.Current;
            IEnumerable<IStructureDefinitionSlice> slices;

            if (navigator.Current.Slicing is null)
            {
                slices = new[] { CreateSlice(navigator) };
            }
            else
            {
                slices = CreateSlices(navigator);
            }

            return new StructureDefinitionElementFacade(elementDef, slices);
        }

        private IEnumerable<IStructureDefinitionSlice> CreateSlices(ElementDefinitionNavigator navigator)
        {
            while (navigator.MoveToNextSlice())
            {
                yield return CreateSlice(navigator);                
            }
        }

        private IStructureDefinitionSlice CreateSlice(ElementDefinitionNavigator navigator)
        {
            var elementDef = navigator.Current;
            var elements = CreateElements(navigator);
            return new StructureDefinitionSliceFacade(elementDef, elements);
        }
        #endregion
    }
}
