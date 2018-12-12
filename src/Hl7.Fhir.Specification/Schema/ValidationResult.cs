namespace Hl7.Fhir.Specification.Schema
{
    public enum ValidationResult
    {
        Success,
        Failure,
        Undecided
    }

    internal class ValidationOutcome
    {
        public static readonly ValidationOutcome Success = new ValidationOutcome(ValidationResult.Success);
        public static readonly ValidationOutcome Failure = new ValidationOutcome(ValidationResult.Failure);
        public static readonly ValidationOutcome Undecided = new ValidationOutcome(ValidationResult.Undecided);

        public readonly ValidationResult Result;
        //public readonly IAssertion[] Evidence;   // this is applicable for a GroupValidationOutcome

        public ValidationOutcome(ValidationResult result)
        {
            Result = result;
        }


        public bool IsSuccessful => Result == ValidationResult.Success;
    }
}
