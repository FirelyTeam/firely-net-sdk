using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Schema.Tags
{
    public enum ValidationResult
    {
        Success,
        Failure,
        Undecided
    }

    public class ResultTag : SchemaTag
    {
        public static readonly ResultTag Success = new ResultTag { Result = ValidationResult.Success };
        public static readonly ResultTag Failure = new ResultTag { Result = ValidationResult.Failure };
        public static readonly ResultTag Undecided = new ResultTag { Result = ValidationResult.Undecided };

        public ValidationResult Result;

        public bool IsSuccessful => Result == ValidationResult.Success;

        public override SchemaTag Merge(SchemaTag other)
        {
            // If we currently are succesful, the new result fully depends on the other
            if (IsSuccessful) return other;

            // Otherwise, we are failing or undecided, which we need to
            // propagate
            return this;
        }
    }
}
