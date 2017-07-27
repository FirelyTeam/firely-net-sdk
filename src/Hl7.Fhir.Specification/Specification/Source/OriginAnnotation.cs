/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using System;

namespace Hl7.Fhir.Specification.Source
{
    [Obsolete("OriginInformation has been renamed to OriginAnnotation")]
    public class OriginInformation
    {
        // Replaced by OriginAnnotation
    }

    public class OriginAnnotation
    {
        public string Origin { get; internal set; }
    }
}
