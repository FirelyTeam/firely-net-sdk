/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation.Impl;
using Hl7.Fhir.Validation.Schema;
using Hl7.FhirPath.Sprache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    internal class SchemaConverter
    {
        private readonly IElementDefinitionAssertionFactory _assertionFactory;
        public readonly ISchemaResolver Source;

        public SchemaConverter(ISchemaResolver source, IElementDefinitionAssertionFactory assertionFactory)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            _assertionFactory = assertionFactory;
        }

        public IElementSchema Convert(StructureDefinition definition)
        {
            var nav = ElementDefinitionNavigator.ForSnapshot(definition);
            return Convert(nav);
        }

        public IElementSchema Convert(ElementDefinitionNavigator nav)
        {
            bool hasContent = nav.MoveToFirstChild();

            var id = new Uri(nav.StructureDefinition.Url, UriKind.Absolute);

            if (!hasContent)
                return _assertionFactory.CreateElementSchemaAssertion(id);
            else
            {
                // Note how the root element (first element of an SD) is integrated within
                // the schema representing the SD as a whole by including just the members
                // of the schema generated from the first ElementDefinition.
                return _assertionFactory.CreateElementSchemaAssertion(id, harvest(nav).Members);
            }
        }

        private IElementSchema harvest(ElementDefinitionNavigator nav)
        {
            var schema = nav.Current.Convert(Source, _assertionFactory);

            if (nav.HasChildren)
            {
                var childNav = nav.ShallowCopy();   // make sure closure is to a clone, not the original argument
                var childAssertion = _assertionFactory.CreateChildren(() => harvestChildren(childNav));
                schema = schema.With(_assertionFactory, childAssertion);
            }

            var result = SkipSlicingComponents(nav);
            if (result != null)
                schema = schema.With(_assertionFactory, result);
            return schema;
        }

        private static ElementDefinitionNavigator walkToCondition(ElementDefinitionNavigator root, string discriminator, IResourceResolver resolver)
        {
            var walker = new StructureDefinitionWalker(root, resolver);
            var conditions = walker.Walk(discriminator);

            // Well, we could check whether the conditions are Equal, since that's what really matters - they should not differ.
            if (conditions.Count > 1)
                throw new IncorrectElementDefinitionException($"The discriminator path '{discriminator}' at {root.CanonicalPath()} leads to multiple ElementDefinitions, which is not allowed.");

            return conditions.Single().Current;
        }


        private IAssertion CreateSliceAssertion(ElementDefinitionNavigator root, IEnumerable<Bookmark> slices)
        {
            var bm = root.Bookmark();

            var slicing = root.Current.Slicing;
            var discriminatorPath = slicing.Discriminator.Select(d => d.Path).FirstOrDefault();

            var sliceList = new List<SliceAssertion.Slice>();

            foreach (var slice in slices)
            {
                root.ReturnToBookmark(slice);

                var conditionNav = walkToCondition(root, discriminatorPath, Source as IResourceResolver);

                var condition = new PathSelectorAssertion(discriminatorPath, conditionNav.Current.ValueSlicingConditions(Source, _assertionFactory));

                sliceList.Add(new SliceAssertion.Slice(root.Current.SliceName, condition, harvest(root)));
            }
            var sliceAssertion = new SliceAssertion(slicing.Ordered ?? false, sliceList);


            root.ReturnToBookmark(bm);
            return _assertionFactory.CreateElementSchemaAssertion(new Uri($"#{root.Path}", UriKind.Relative), new[] { sliceAssertion });

        }

        private IAssertion SkipSlicingComponents(ElementDefinitionNavigator root)
        {
            IAssertion result = null;
            // 20200520: for now only value slicing
            if (root.Current.Slicing?.Discriminator.All(d => d.Type == ElementDefinition.DiscriminatorType.Value) == true &&
                !root.Current.Path.Contains(".extension"))
            {
                var slices = root.FindMemberSlices(true);

                result = CreateSliceAssertion(root, slices);

                skipSlicingElements(root);

            }
            return result;
        }

        private static void skipSlicingElements(ElementDefinitionNavigator root)
        {
            var curPath = root.Path;
            while (root.MoveToNext() && curPath == root.Path)
            {
            }
        }

        private IReadOnlyDictionary<string, IAssertion> harvestChildren(ElementDefinitionNavigator childNav)
        {
            var children = new Dictionary<string, IAssertion>();

            childNav.MoveToFirstChild();
            var xmlOrder = 0;

            do
            {
                xmlOrder += 10;
                var childSchema = harvest(childNav);

                // Don't add empty schemas (i.e. empty ElementDefs in a differential)
                if (!childSchema.IsEmpty())
                {
                    var schemaWithOrder = childSchema.With(_assertionFactory, new XmlOrder(xmlOrder, childNav.PathName));
                    children.Add(childNav.PathName, schemaWithOrder);
                }
            }
            while (childNav.MoveToNext());
#if NET40
            return children.ToReadOnlyDictionary();
#else
            return children;
#endif
        }
    }
}
