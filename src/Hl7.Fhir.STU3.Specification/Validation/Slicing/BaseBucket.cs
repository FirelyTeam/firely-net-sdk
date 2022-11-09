using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Collections.Generic;

namespace Hl7.Fhir.Validation
{
    internal abstract class BaseBucket : IBucket
    {
        protected internal BaseBucket(ElementDefinition definition, ValidationState state)
        {
            Name = definition.Path + (definition.SliceName != null ? $":{definition.SliceName}" : null);
            Cardinality = Cardinality.FromElementDefinition(definition);
            State = state;
        }

        public string Name { get; private set; }
        public Cardinality Cardinality { get; private set; }
        public IList<ITypedElement> Members { get; private set; } = new List<ITypedElement>();
        protected ValidationState State { get; }

        public abstract bool Add(ITypedElement instance);

        public virtual OperationOutcome Validate(Validator validator, ITypedElement errorLocation)
        {
            var outcome = new OperationOutcome();

            if (!Cardinality.InRange(Members.Count))
            {
                validator.Trace(outcome, $"Instance count for '{Name}' is {Members.Count}, which is not within the specified cardinality of {Cardinality.ToString()}",
                        Issue.CONTENT_INCORRECT_OCCURRENCE, errorLocation);
            }

            return outcome;
        }
    }
}
