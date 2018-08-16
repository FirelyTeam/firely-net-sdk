/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


namespace Hl7.Fhir.Serialization
{
    public class SerializerSettings
    {
        public bool Pretty;

        public SerializerSettings Clone() =>
            new SerializerSettings
            {
                Pretty = Pretty
            };
    }
}
