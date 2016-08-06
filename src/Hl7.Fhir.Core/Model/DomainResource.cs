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

using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay("\\{\"{TypeName,nq}/{Id,nq}\" Identity={ResourceIdentity()}}")]
    [InvokeIValidatableObject]
    public abstract partial class DomainResource : IModifierExtendable
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>(base.Validate(validationContext));

            if (this.Contained != null)
            {
                if (!Contained.OfType<DomainResource>().All(dr => dr.Text == null))
                    result.Add(new ValidationResult("Resource has contained resources with narrative"));

                if (!Contained.OfType<DomainResource>().All(cr => cr.Contained == null || !cr.Contained.Any()))
                    result.Add(new ValidationResult("Resource has contained resources with nested contained resources"));
            }

            // and process all the invariants from the resource
            if (InvariantConstraints != null && InvariantConstraints.Count > 0)
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                // Need to serialize to XML until the object model processor exists
                // string tpXml = Fhir.Serialization.FhirSerializer.SerializeResourceToXml(this);
                // FhirPath.IFhirPathElement tree = FhirPath.InstanceTree.TreeConstructor.FromXml(tpXml);
                var tree = new FluentPath.ModelNavigator(this);
                OperationOutcome results = new OperationOutcome();
                foreach (var invariantRule in InvariantConstraints)
                {
                    ValidateInvariant(invariantRule, tree, results);
                }
                foreach (var item in results.Issue)
                {
                    if (item.Severity == OperationOutcome.IssueSeverity.Error
                        || item.Severity == OperationOutcome.IssueSeverity.Fatal)
                        result.Add(new ValidationResult(item.Details.Coding[0].Code + ": " + item.Details.Text));
                }

                sw.Stop();
                System.Diagnostics.Trace.WriteLine(String.Format("Validation of {0} execution took {1}", ResourceType.ToString(), sw.Elapsed.TotalSeconds));
            }

            return result;
        }
    }
}
