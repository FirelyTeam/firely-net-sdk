/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */



namespace Hl7.Fhir.Serialization
{
    public class FhirJsonWriterSettings
    {
        /// <summary>
        /// When encountering an unknown member, just skip it instead of reporting an error.
        /// </summary>
        public bool IgnoreUnknownElements;

        public bool Pretty;

        public FhirJsonWriterSettings Clone() =>
            new FhirJsonWriterSettings
            {
                IgnoreUnknownElements = IgnoreUnknownElements,
                Pretty = Pretty
            };
    }
}
