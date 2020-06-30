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
using System.Linq;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Model
{
    [FhirType("Parameters")]
    [InvokeIValidatableObject]
    [DataContract]
    public class FhirPatchParameters : Parameters
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

        public FhirPatchParameters ()
            : base()
        {
            PropertyChanged += (_, __) => _patchDocument = null;
        }

        public static implicit operator PatchDocument(FhirPatchParameters @this) => @this.Document;

        public override IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
        {
            var baseResult = base.Validate(validationContext);
            var result = new List<ValidationResult>();

            Action<Exception> errorReporter = (ex) => result.Add(new ValidationResult(ex.Message));
            _patchDocument = PatchDocumentReader.Read(this, errorReporter);

            return baseResult.Concat(result);
        }
    }
}
