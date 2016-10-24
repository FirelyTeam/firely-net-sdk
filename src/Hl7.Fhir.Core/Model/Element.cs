using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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
    [KnownType(typeof(Coding))]
    [KnownType(typeof(Extension))]
    [KnownType(typeof(Narrative))]
    [KnownType(typeof(Address))]
    [KnownType(typeof(Annotation))]
    [KnownType(typeof(Attachment))]
    [KnownType(typeof(BackboneElement))]
    [KnownType(typeof(CodeableConcept))]
    [KnownType(typeof(ContactPoint))]
    [KnownType(typeof(ElementDefinition))]
    [KnownType(typeof(ElementDefinition.SlicingComponent))]
    [KnownType(typeof(ElementDefinition.BaseComponent))]
    [KnownType(typeof(ElementDefinition.TypeRefComponent))]
    [KnownType(typeof(ElementDefinition.ConstraintComponent))]
    [KnownType(typeof(ElementDefinition.BindingComponent))]
    [KnownType(typeof(DataElement.MappingComponent))]
    [KnownType(typeof(HumanName))]
    [KnownType(typeof(Identifier))]
    [KnownType(typeof(Meta))]
    [KnownType(typeof(Period))]
    [KnownType(typeof(Quantity))]
    [KnownType(typeof(Range))]
    [KnownType(typeof(Ratio))]
    [KnownType(typeof(ResourceReference))]
    [KnownType(typeof(SampledData))]
    [KnownType(typeof(Signature))]
    [KnownType(typeof(Timing))]
    [KnownType(typeof(Timing.RepeatComponent))]
    public abstract partial class Element : IExtendable
    {
    }
    
}
