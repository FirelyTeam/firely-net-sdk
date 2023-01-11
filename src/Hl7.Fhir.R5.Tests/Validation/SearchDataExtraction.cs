/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Test.Validation
{
    public partial class ValidateSearchExtractionAllExamplesTest
    {
        // Verified examples that fail validations
        private readonly string[] _filesToBeSkipped = new string[]
        {      
            // this file has a Literal with value '-1.000000000000000000e245', which does not fit into a c# datatype
            "observation-decimal(decimal).xml",
            // resource Citation is not generated because of generator errors
            "citation-example(example).xml",
            // operation-resource.*.xml cannot be parsed because of `resource` is not a valid enum of ResourceType
            "operation-resource-convert.xml",
            "operation-resource-add.xml",
            "operation-resource-meta-delete.xml",
            "operation-resource-filter.xml",
            "operation-resource-meta-add.xml",
            "operation-resource-remove.xml",
            "operation-canonicalresource-current-canonical.xml",
            "operation-resource-graph.xml",
            "operation-resource-graphql.xml",
            "operation-resource-meta.xml",
            "operation-resource-validate.xml"


        };

    }
}
