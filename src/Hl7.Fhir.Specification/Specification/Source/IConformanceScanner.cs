/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source
{
    [Obsolete("Replaced by IArtifactScanner.")]
    internal interface IConformanceScanner
    {
        Resource Retrieve(ConformanceScanInformation entry);
        List<ConformanceScanInformation> List();
    }
}
