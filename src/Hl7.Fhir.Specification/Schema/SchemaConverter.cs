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
using Hl7.Fhir.Validation;
using Hl7.Fhir.Validation.Impl;
using Hl7.Fhir.Validation.Schema;
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
            bool isInlineChildren = !nav.Current.IsRootElement();

            if (nav.HasChildren)
            {
                var childNav = nav.ShallowCopy();   // make sure closure is to a clone, not the original argument

                bool allowAdditionalChildren = (isInlineChildren && nav.Current.IsResourcePlaceholder()) ||
                                     (!isInlineChildren && nav.StructureDefinition.Abstract == true);

                var childAssertion = _assertionFactory.CreateChildren(() => harvestChildren(childNav), allowAdditionalChildren);
                schema = schema.With(_assertionFactory, childAssertion);
            }

            if (nav.IsSlicing)
            {
                var sliceAssertion = harvestSlicing(nav);
                schema = schema.With(_assertionFactory, sliceAssertion);
            }

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

            var sliceList = new List<SliceAssertion.Slice>();

            foreach (var slice in slices)
            {
                root.ReturnToBookmark(slice);

                IAssertion condition = new AllAssertion(slicing.Discriminator.Select(d => createCondition(root, d.Path)));
                sliceList.Add(_assertionFactory.CreateSlice(root.Current.SliceName ?? root.Current.ElementId, condition, harvest(root)));
            }

            var defaultSlice = createDefaultSlice(slicing);
            var sliceAssertion = _assertionFactory.CreateSliceAssertion(slicing.Ordered ?? false, defaultSlice, sliceList);

            root.ReturnToBookmark(bm);
            return _assertionFactory.CreateElementSchemaAssertion(new Uri($"#{root.Path}", UriKind.Relative), new[] { sliceAssertion });
        }

        private IAssertion createDefaultSlice(ElementDefinition.SlicingComponent slicing)
            => slicing.Rules == ElementDefinition.SlicingRules.Closed ?
                ResultAssertion.CreateFailure(
                            new IssueAssertion(1026, "Element does not match any slice and the group is closed.", IssueSeverity.Error))
                : ResultAssertion.Success;

        private IAssertion createCondition(ElementDefinitionNavigator root, string path)
        {
            var conditionNav = walkToCondition(root, path, Source as IResourceResolver);
            return new PathSelectorAssertion(path, conditionNav.Current.ValueSlicingConditions(Source, _assertionFactory));
        }


        private IAssertion harvestSlicing(ElementDefinitionNavigator root)
        {
            IAssertion result = null;
            // 20200520: for now only value slicing
            if (root.Current.Slicing?.Discriminator.All(d => d.Type == ElementDefinition.DiscriminatorType.Value) == true)
            {
                var slices = root.FindMemberSlices(true);

                result = CreateSliceAssertion(root, slices);

                skipSlicingElements(root);

            }
            return result;
        }

        private static void skipSlicingElements(ElementDefinitionNavigator root)
        {
            var pathName = root.PathName;
            while (root.MoveToNext(pathName)) { }
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
