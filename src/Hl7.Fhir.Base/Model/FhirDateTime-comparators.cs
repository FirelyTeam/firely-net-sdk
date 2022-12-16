/*
  Copyright (c) 2011-2012, HL7, Inc
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
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model
{

    public partial class FhirDateTime
    {
        public static bool operator >(FhirDateTime a, FhirDateTime b)
        {
            var aValue = a?.Value;
            var bValue = b?.Value;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return P.DateTime.Parse(a.Value) > P.DateTime.Parse(b.Value);
        }

        public static bool operator >=(FhirDateTime a, FhirDateTime b)
        {
            var aValue = a?.Value;
            var bValue = b?.Value;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return P.DateTime.Parse(a.Value) >= P.DateTime.Parse(b.Value);
        }

        public static bool operator <(FhirDateTime a, FhirDateTime b)
        {
            var aValue = a?.Value;
            var bValue = b?.Value;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return P.DateTime.Parse(a.Value) < P.DateTime.Parse(b.Value);
        }

        public static bool operator <=(FhirDateTime a, FhirDateTime b)
        {
            var aValue = a?.Value;
            var bValue = b?.Value;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return P.DateTime.Parse(a.Value) <= P.DateTime.Parse(b.Value);
        }

        /// <summary>
        /// If you use this operator, you should check that a modifierExtension isn't changing the meaning
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(FhirDateTime a, FhirDateTime b) => Equals(a, b);

        /// <summary>
        /// If you use this operator, you should check that a modifierExtension isn't changing the meaning
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(FhirDateTime a, FhirDateTime b) => !Equals(a, b);

        public override bool Equals(object obj)
        {
            if (obj is FhirDateTime other)
            {
                var otherValue = other?.Value;

                if (Value == null) return otherValue == null;
                if (otherValue == null) return false;

                if (this.Value == otherValue) return true; // Default reference/string comparison works in most cases

                var left = P.DateTime.Parse(Value);
                var right = P.DateTime.Parse(otherValue);

                return left == right;
            }
            else
                return false;
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
