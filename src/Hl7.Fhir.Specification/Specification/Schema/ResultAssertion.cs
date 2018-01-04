using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
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

    public class ResultAssertion : Assertion, IMergeableAssertion
    {
        public static readonly ResultAssertion Success = new ResultAssertion { Result = ValidationResult.Success };
        public static readonly ResultAssertion Failure = new ResultAssertion { Result = ValidationResult.Failure };
        public static readonly ResultAssertion Undecided = new ResultAssertion { Result = ValidationResult.Undecided };

        public ValidationResult Result;

        public bool IsSuccessful => Result == ValidationResult.Success;

        public override IEnumerable<Assertions> Collect() => Assertions.Empty.Collection;            

        public IMergeableAssertion Merge(IMergeableAssertion other)
        {
            if (other is ResultAssertion ra)
            {
                // If we currently are succesful, the new result fully depends on the other
                if (IsSuccessful) return other;

                // Otherwise, we are failing or undecided, which we need to
                // propagate
                return this;
            }
            else
                throw Error.InvalidOperation($"Internal logic failed: tried to merge a ResultAssertion with an {other.GetType().Name}");
        }

        public override JToken ToJson() => new JProperty("result", Result.ToString());
    }
}
