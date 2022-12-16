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
            "observation-decimal(decimal).xml"
        };
    }
}
