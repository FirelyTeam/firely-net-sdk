/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlWriterSettings
    {
        public bool SkipUnknownElements;

        public bool Pretty;

        public FhirXmlWriterSettings Clone() =>
            new FhirXmlWriterSettings
            {
                SkipUnknownElements = SkipUnknownElements,
                Pretty = Pretty
            };
    }
}
