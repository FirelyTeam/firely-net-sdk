using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Validation;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System.Collections.Generic;

namespace Hl7.Fhir.Validation
{
    internal abstract class BaseBucket : IBucket
    {
        internal protected BaseBucket(ElementDefinition definition)
        {
            Name = definition.Path + (definition.Name != null ? $":{definition.Name}" : null);
            Cardinality = Cardinality.FromElementDefinition(definition);
            Path = definition.Path;
        }

        private string Path { get; set; }
        public string Name { get; private set; }
        public Cardinality Cardinality { get; private set; }
        public IList<ITypedElement> Members { get; private set; } = new List<ITypedElement>();

        public abstract bool Add(ITypedElement instance);
  
        public virtual OperationOutcome Validate(Validator validator, ITypedElement errorLocation)
        {
            var outcome = new OperationOutcome();

            if (!Cardinality.InRange(Members.Count))
            {
                OperationOutcome.IssueComponent issue = validator.Trace(outcome, $"Instance count for '{Name}' is {Members.Count}, which is not within the specified cardinality of {Cardinality.ToString()}",
                        Issue.CONTENT_INCORRECT_OCCURRENCE, errorLocation);
                if (issue != null)
                {
                    // the location in the structure definition (this will match to the discriminator when checking slicing)
                    // issue.LocationElement.Add(new FhirString(Path));
                    issue.SetAnnotation(new SlicePathAnnotation(Path));
                }
            }
            return outcome;
        }
    }
}
