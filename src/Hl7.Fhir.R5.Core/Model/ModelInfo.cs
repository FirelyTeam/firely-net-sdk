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

#nullable enable

using System;

namespace Hl7.Fhir.Model
{
    public partial class ModelInfo
    {
        public static readonly Type[] OpenTypes =
        {
            typeof(Model.Address),
            typeof(Model.Age),
            typeof(Model.Annotation),
            typeof(Model.Attachment),
            typeof(Model.Base64Binary),
            typeof(Model.FhirBoolean),
            typeof(Model.Canonical),
            typeof(Model.Code),
            typeof(Model.CodeableConcept),
            typeof(Model.Coding),
            typeof(Model.ContactDetail),
            typeof(Model.ContactPoint),
            typeof(Model.Contributor),
            typeof(Model.Count),
            typeof(Model.DataRequirement),
            typeof(Model.Date),
            typeof(Model.FhirDateTime),
            typeof(Model.FhirDecimal),
            typeof(Model.Distance),
            typeof(Model.Dosage),
            typeof(Model.Duration),
            typeof(Model.Expression),
            typeof(Model.HumanName),
            typeof(Model.Id),
            typeof(Model.Identifier),
            typeof(Model.Instant),
            typeof(Model.Integer),
            typeof(Model.Markdown),
            typeof(Model.Meta),
            typeof(Model.Money),
            typeof(Model.Oid),
            typeof(Model.ParameterDefinition),
            typeof(Model.Period),
            typeof(Model.PositiveInt),
            typeof(Model.Quantity),
            typeof(Model.Range),
            typeof(Model.Ratio),
            typeof(Model.ResourceReference),
            typeof(Model.RelatedArtifact),
            typeof(Model.SampledData),
            typeof(Model.Signature),
            typeof(Model.FhirString),
            typeof(Model.Time),
            typeof(Model.Timing),
            typeof(Model.TriggerDefinition),
            typeof(Model.UnsignedInt),
            typeof(Model.FhirUri),
            typeof(Model.FhirUrl),
            typeof(Model.UsageContext),
            typeof(Model.Uuid)
        };

    }
}
#nullable restore