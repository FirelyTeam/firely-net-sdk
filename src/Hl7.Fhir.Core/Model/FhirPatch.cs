/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Patch;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Model
{
    [FhirType("Parameters")]
    [InvokeIValidatableObject]
    [DataContract]
    public class FhirPatch : Parameters
    {
        private PatchDocument _patchDocument;

        private PatchDocument Document
        {
            get 
            {
                if(_patchDocument == null)
                    _patchDocument = PatchDocumentReader.Read(this);
                return _patchDocument;
            }
        }

        public FhirPatch ()
            : base()
        {
            PropertyChanged += (_, __) => _patchDocument = null;
        }

        public static implicit operator PatchDocument(FhirPatch @this) => @this.Document;

        public override IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            try
            {
                var _ = Document;
            }
            catch(Exception ex)
            {
                var validationError = new ValidationResult(ex.Message);
                result.Add(validationError);
            }

            return result;
        }
    }
}
