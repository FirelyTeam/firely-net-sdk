using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public enum ValidationResult
    {
        Success,
        Failure,
        Undecided
    }

    public class ResultAssertion : IAssertion, IMergeable
    {
        public static readonly ResultAssertion Success = new ResultAssertion(ValidationResult.Success);
        public static readonly ResultAssertion Failure = new ResultAssertion(ValidationResult.Failure);
        public static readonly ResultAssertion Undecided = new ResultAssertion(ValidationResult.Undecided);

        public readonly ValidationResult Result;
        public readonly IAssertion[] Evidence;

        public ResultAssertion(ValidationResult result, params IAssertion[] evidence) : this(result, evidence.AsEnumerable())
        {
        }

        public ResultAssertion(ValidationResult result, IEnumerable<IAssertion> evidence)
        {
            Evidence = evidence?.ToArray() ?? throw new ArgumentNullException(nameof(evidence));
            Result = result;
        }

        public bool IsSuccessful => Result == ValidationResult.Success;

        public IMergeable Merge(IMergeable other)
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

        public JToken ToJson() => new JProperty("result", Result.ToString());
    }
}
