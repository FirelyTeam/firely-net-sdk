using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Schema
{
    public enum ValidationResult
    {
        Success,
        Failure,
        Undecided
    }

    public struct ResultAnnotation
    {
        public ValidationResult Result;
    }

    public class ValidationContext
    {
    }
}
