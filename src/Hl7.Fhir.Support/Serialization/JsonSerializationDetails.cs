/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Serialization
{
    public class JsonSerializationDetails : IPositionInfo
    {
        public const string RESOURCETYPE_MEMBER_NAME = "resourceType";

        public object RawValue;

        public int LineNumber { get; internal set; }

        public int LinePosition { get; internal set; }

    }
}