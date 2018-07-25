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
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Rest;
using Hl7.FhirPath;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.Fhir.FhirPath;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay("\\{\"{TypeName,nq}/{Id,nq}\" Identity={ResourceIdentity()}}")]
    [InvokeIValidatableObject]
    public abstract partial class Resource 
    {
        /// <summary>
        /// This is the base URL of the FHIR server that this resource is hosted on
        /// </summary>
        [NotMapped]
        public Uri ResourceBase
        {
            get
            {
                var bd = this.Annotation<ResourceBaseData>();
                return bd != null ? bd.Base : null;
            }

            set
            {
                this.RemoveAnnotations<ResourceBaseData>();
                AddAnnotation(new ResourceBaseData { Base = value } );
            }
        }

        private class ResourceBaseData
        {
            public Uri Base;
        }

        /// <summary>
        /// The List of invariants to be validated for the resource
        /// </summary>
        [NotMapped]
        public List<ElementDefinition.ConstraintComponent> InvariantConstraints;

        public virtual void AddDefaultConstraints()
        {
            if (InvariantConstraints == null || InvariantConstraints.Count == 0)
                InvariantConstraints = new List<ElementDefinition.ConstraintComponent>();
        }

        /// <summary>
        /// Perform the Invariant based validation for this rule
        /// </summary>
        /// <param name="invariantRule"></param>
        /// <param name="model"></param>
        /// <param name="result">The OperationOutcome that will have the validation results appended</param>
        /// <param name="context">Describes the context in which a validation check is performed.</param>
        /// <returns></returns>
        public static bool ValidateInvariantRule(ValidationContext context, ElementDefinition.ConstraintComponent invariantRule, IElementNavigator model, OperationOutcome result)
        {
            string expression = invariantRule.GetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression");
            try
            {
                // No FhirPath extension
                if (string.IsNullOrEmpty(expression))
                {
                    result.Issue.Add(new OperationOutcome.IssueComponent()
                    {
                        Code = OperationOutcome.IssueType.Invariant,
                        Severity = OperationOutcome.IssueSeverity.Warning,
                        Details = new CodeableConcept(null, invariantRule.Key, "Unable to validate without a FhirPath expression"),
                        Diagnostics = expression
                    });
                    return true;
                }

                // Ensure the FHIR extensions are registered
                Hl7.Fhir.FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

                if (model.Predicate(expression, new EvaluationContext(model)))
                    return true;

                result.Issue.Add(new OperationOutcome.IssueComponent()
                {
                    Code = OperationOutcome.IssueType.Invariant,
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Details = new CodeableConcept(null, invariantRule.Key, invariantRule.Human),
                    Diagnostics = expression
                });
                return false;
            }
            catch (Exception ex)
            {
                result.Issue.Add(new OperationOutcome.IssueComponent()
                {
                    Code = OperationOutcome.IssueType.Invariant,
                    Severity = OperationOutcome.IssueSeverity.Fatal,
                    Details = new CodeableConcept(null, invariantRule.Key, "FATAL: Unable to process the invariant rule: " + invariantRule.Key + " " + expression),
                    Diagnostics = String.Format("FhirPath: {0}\r\nError: {1}", expression, ex.Message)
                });
                return false;
            }
        }

        /// <summary>
        /// Returns the entire URI of the location that this resource was retrieved from
        /// </summary>
        /// <remarks>
        /// It is not stored, but reconstructed from the components of the resource
        /// </remarks>
        /// <returns></returns>
        public ResourceIdentity ResourceIdentity(string baseUrl = null)
        {
            if (Id == null) return null;

            var result =  Hl7.Fhir.Rest.ResourceIdentity.Build(TypeName, Id, VersionId);

            if (!string.IsNullOrEmpty(baseUrl))
                return result.WithBase(baseUrl);

            if (ResourceBase != null)
                return result.WithBase(ResourceBase);
            else
                return result;
        }

        public ResourceIdentity ResourceIdentity(Uri baseUrl)
        {
            return ResourceIdentity(baseUrl.OriginalString);
        }

        /// <summary>
        /// This object is internally used for locking the resource in a multithreaded environment.
        /// </summary>
        /// <remarks>
        /// As a consumer of this API, please do not use this object.
        /// </remarks>
        [NotMapped]
        public readonly object SyncLock = new object();

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            // The ID field does not need to be an abolute URI,
            // this should be the ResourceIdentity.
            // if (Id != null && !new Uri(Id,UriKind.RelativeOrAbsolute).IsAbsoluteUri)
            //    result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Entry id must be an absolute URI"));

            if (Meta != null)
            {
                // if (!String.IsNullOrEmpty(this.Meta.VersionId) && !new Uri(Id,UriKind.RelativeOrAbsolute).IsAbsoluteUri)
                //     result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Entry selflink must be an absolute URI"));

                if (Meta.Tag != null && validationContext.ValidateRecursively())
                    DotNetAttributeValidation.TryValidate(Meta.Tag, result, true);
            }

            // and process all the invariants from the resource
            ValidateInvariants(validationContext, result);

            return result;
        }

        public void ValidateInvariants(List<ValidationResult> result)
             => ValidateInvariants(DotNetAttributeValidation.BuildContext(null), result);

        public void ValidateInvariants(ValidationContext context, List<ValidationResult> result)
        {
            OperationOutcome results = new OperationOutcome();
            ValidateInvariants(context, results);
            foreach (var item in results.Issue)
            {
                if (item.Severity == OperationOutcome.IssueSeverity.Error
                    || item.Severity == OperationOutcome.IssueSeverity.Fatal)
                    result.Add(new ValidationResult(item.Details.Coding[0].Code + ": " + item.Details.Text));
            }
        }

        public void ValidateInvariants(OperationOutcome result)
            => ValidateInvariants(DotNetAttributeValidation.BuildContext(new object()), result);

        public void ValidateInvariants(ValidationContext context, OperationOutcome result)
        {
            if (InvariantConstraints != null && InvariantConstraints.Count > 0)
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                // Need to serialize to XML until the object model processor exists
                // string tpXml = Fhir.Serialization.FhirSerializer.SerializeResourceToXml(this);
                // FhirPath.IFhirPathElement tree = FhirPath.InstanceTree.TreeConstructor.FromXml(tpXml);
                var tree = new PocoNavigator(this);
                foreach (var invariantRule in InvariantConstraints)
                {
                    ValidateInvariantRule(context,invariantRule, tree, result);
                }

                sw.Stop();
                // System.Diagnostics.Trace.WriteLine(String.Format("Validation of {0} execution took {1}", ResourceType.ToString(), sw.Elapsed.TotalSeconds));
            }
        }

        [NotMapped]
        public string VersionId
        {
            get 
            {
                if (HasVersionId)
                    return Meta.VersionId;
                else 
                    return null;
            }
            set
            {
                if (Meta == null) Meta = new Meta();
                Meta.VersionId = value;
            }
        }

        [NotMapped]
        public bool HasVersionId => Meta?.VersionId != null;
    }
}


