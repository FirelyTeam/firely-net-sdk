/*
Copyright (c) 2011+, HL7, Inc
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

 * Redistributions of source code must retain the above copyright notice, this 
   list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, 
   this list of conditions and the following disclaimer in the documentation 
   and/or other materials provided with the distribution.
 * Neither the name of HL7 nor the names of its contributors may be used to 
   endorse or promote products derived from this software without specific 
   prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Publication
{
    public static class ElementDefnRenderingExtensions
    {
        public static string DescribeCardinality(this Profile.ElementDefinitionComponent defn)
        {
            if (defn.Max == null || defn.Max == "-1")
                return defn.Min.ToString() + "..*";
            else
                return defn.Min.ToString() + ".." + defn.Max;
        }

        public static string DescribeTypeCode(this Profile.ElementDefinitionComponent defn)
        {
            return String.Join(" | ", defn.Type.Select(tr => tr.Code));
        }

        public static string DescribeContext(this Profile.ProfileExtensionDefnComponent ext)
        {
            return String.Join(", ", ext.Context);
        }

        public static string RenderValue(this Element value)
        {
            if (value is FhirUri) return ((FhirUri)value).Value;
            if (value is FhirString) return ((FhirString)value).Value;
            if (value is Code) return ((Code)value).Value;

            return "(todo)";
        }
    }
}
