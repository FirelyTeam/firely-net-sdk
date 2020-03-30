/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Validation.Impl;
using Hl7.Fhir.Validation.Schema;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Schema
{
    internal class SchemaConverter
    {
        public readonly ISchemaResolver Source;

        public SchemaConverter(ISchemaResolver source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public ElementSchema Convert(StructureDefinition definition)
        {
            var nav = ElementDefinitionNavigator.ForSnapshot(definition);
            return Convert(nav);
        }

        public ElementSchema Convert(ElementDefinitionNavigator nav)
        {
            bool hasContent = nav.MoveToFirstChild();

            var id = new Uri(nav.StructureDefinition.Url, UriKind.Absolute);

            if (!hasContent)
                return new ElementSchema(id);
            else
            {
                // Note how the root element (first element of an SD) is integrated within
                // the schema representing the SD as a whole by including just the members
                // of the schema generated from the first ElementDefinition.
                return new ElementSchema(id, harvest(nav).Members);
            }
        }

        private ElementSchema harvest(ElementDefinitionNavigator nav)
        {
            var schema = nav.Current.Convert(Source);

            if (nav.HasChildren)
            {
                var childNav = nav.ShallowCopy();   // make sure closure is to a clone, not the original argument
                var childAssertion = new Children(() => harvestChildren(childNav));
                return schema.With(childAssertion);
            }
            else
                return schema;
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
                if (!childSchema.IsEmpty)
                {
                    var schemaWithOrder = childSchema.With(new XmlOrder(xmlOrder, childNav.PathName));
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
