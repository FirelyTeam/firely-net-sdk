/*  
* Copyright (c) 2017, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using System;
using System.Linq;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
    public interface IPositionProvider
    {
        int LineNumber { get; }
        int LinePosition { get; }
    }

}