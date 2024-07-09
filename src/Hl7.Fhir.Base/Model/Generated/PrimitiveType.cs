// <auto-generated/>
// Contents of: hl7.fhir.r5.expansions#5.0.0, hl7.fhir.r5.core#5.0.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using SystemPrimitive = Hl7.Fhir.ElementModel.Types;

/*
  Copyright (c) 2011+, HL7, Inc.
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

namespace Hl7.Fhir.Model
{
  /// <summary>
  /// Parent type for DataTypes with a simple value
  /// </summary>
  /// <remarks>
  /// The base type for all re-useable types defined that have a simple property.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("PrimitiveType","http://hl7.org/fhir/StructureDefinition/PrimitiveType")]
  public abstract partial class PrimitiveType : Hl7.Fhir.Model.DataType
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "PrimitiveType"; } }

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as PrimitiveType;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if (ObjectValue != null) dest.ObjectValue = ObjectValue;
      return dest;
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other) => IsExactly(other);

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as PrimitiveType;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;

      var otherValue = otherT.ObjectValue;
      if (ObjectValue is byte[] bytes && otherValue is byte[] bytesOther)
        return Enumerable.SequenceEqual(bytes, bytesOther);
      else
        return Equals(ObjectValue, otherT.ObjectValue);

    }

  }

}

// end of file
