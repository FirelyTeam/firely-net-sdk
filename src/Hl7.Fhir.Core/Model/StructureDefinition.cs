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
using System.Collections.Generic;
using System.ComponentModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;

namespace Hl7.Fhir.Model
{
    // [WMR 20160803] Add common base interfaces
    public interface IElementList : IModifierExtendable, INotifyPropertyChanged, IValidatableObject, IDeepCopyable, IDeepComparable
    {
        List<ElementDefinition> Element { get; set; }
    }

    // [WMR 20161005] Added specific debugger display attribute that includes the canonical url
    [System.Diagnostics.DebuggerDisplay("\\{\"{TypeName,nq}/{Id,nq}\" Identity={ResourceIdentity()}} Url={Url}")]
    public partial class StructureDefinition
    {
        public partial class SnapshotComponent : IElementList {}

        public partial class DifferentialComponent : IElementList { }

        [NotMapped]
        public bool IsConstraint => Derivation == TypeDerivationRule.Constraint;

        [NotMapped]
        public bool IsExtension => Type == "Extension";

        [NotMapped]
        public bool HasSnapshot => Snapshot != null && Snapshot.Element != null && Snapshot.Element.Any();

        [NotMapped]
        public bool IsCoreDefinition => Type == Id && Url == ModelInfo.CanonicalUriForFhirCoreType(Type);

    }
}


