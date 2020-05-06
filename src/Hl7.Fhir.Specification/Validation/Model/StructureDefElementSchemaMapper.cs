using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Validation.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class StructureDefElementSchemaMapper : IAssignMapper<List<ElementDefinition>, UniStructureDefElementSchema>
    {
        private readonly StructureDefinition _structureDef;

        public StructureDefElementSchemaMapper(StructureDefinition structureDef)
        {
            _structureDef = structureDef;
        }

        public UniStructureDefElementSchema Map(MappingContext context, List<ElementDefinition> source)
        {
            if (source is null)
            {
                return null;
            }

            var navigator = new ElementDefinitionNavigator(source, _structureDef);
            return new UniStructureDefElementSchema { Elements = CreateElements(context, navigator).ToList() };
        }

        public List<ElementDefinition> Map(MappingContext context, UniStructureDefElementSchema source)
        {
            if (source?.Elements is null)
            {
                return null;
            }

            return GetElementDefinitions(context, source.Elements).ToList();
        }

        #region WRITE
        private IEnumerable<ElementDefinition> GetElementDefinitions(MappingContext context, IEnumerable<UniStructureDefElement> elements)
        {
            return elements.SelectMany(element => GetElementDefinitions(context, element));
        }

        private IEnumerable<ElementDefinition> GetElementDefinitions(MappingContext context, UniStructureDefElement element)
        {
            if (element.Slicing is object)
            {
                yield return element.Map(context, StructureDefElementMapper.Current);
            }

            foreach (var sliceElementDef in GetElementDefinitions(context, element.Buckets))
            {
                yield return sliceElementDef;
            }
        }

        private IEnumerable<ElementDefinition> GetElementDefinitions(MappingContext context, IEnumerable<UniStructureDefElementBucket> buckets)
        {
            return buckets.SelectMany(bucket => GetElementDefinitions(context, bucket));
        }

        private IEnumerable<ElementDefinition> GetElementDefinitions(MappingContext context, UniStructureDefElementBucket bucket)
        {
            yield return bucket.Map(context, StructureDefBucketMapper.Current);

            foreach (var elementDef in GetElementDefinitions(context, bucket.Elements))
            {
                yield return elementDef;
            }
        }
        #endregion

        #region #READ
        private IEnumerable<UniStructureDefElement> CreateElements(MappingContext context, ElementDefinitionNavigator navigator)
        {
            if (navigator.MoveToFirstChild())
            {
                do
                {
                    yield return CreateElement(context, navigator);
                }
                while (navigator.MoveToNext());

                navigator.MoveToParent();
            }
        }

        private UniStructureDefElement CreateElement(MappingContext context, ElementDefinitionNavigator navigator)
        {
            var elementDef = navigator.Current;
            var target = elementDef.Map(context, StructureDefElementMapper.Current);            

            if (navigator.Current.Slicing is null)
            {
                target.Buckets = new List<UniStructureDefElementBucket> { CreateBucket(context, navigator) };
            }
            else
            {
                target.Buckets = CreateBuckets(context, navigator).ToList();
            }

            return target;
        }

        private IEnumerable<UniStructureDefElementBucket> CreateBuckets(MappingContext context, ElementDefinitionNavigator navigator)
        {
            while (navigator.MoveToNextSlice())
            {
                yield return CreateBucket(context, navigator);
            }
        }

        private UniStructureDefElementBucket CreateBucket(MappingContext context, ElementDefinitionNavigator navigator)
        {
            var elementDef = navigator.Current;
            var target = elementDef.Map(context, StructureDefBucketMapper.Current);
            target.Elements = CreateElements(context, navigator).ToList();
            return target;
        }
        #endregion
    }
}
