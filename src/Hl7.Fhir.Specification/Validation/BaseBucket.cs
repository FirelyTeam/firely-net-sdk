using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    internal abstract class BaseBucket : IBucket
    {
        internal protected BaseBucket(ElementDefinition definition, Validator validator, IElementNavigator errorLocation)
        {
            Name = definition.Path + definition.Name != null ? $"[{definition.Name}]" : null;
            Cardinality = Cardinality.FromElementDefinition(definition);

            Validator = validator;
            ErrorLocation = errorLocation;
        }

        protected Validator Validator;
        protected IElementNavigator ErrorLocation;

        public string Name { get; private set; }
        public Cardinality Cardinality { get; private set; }
        public IList<IElementNavigator> Members { get; private set; } = new List<IElementNavigator>();

        public virtual OperationOutcome Add(IElementNavigator instance)
        {
            var outcome = IsMember(instance);

            if (outcome.Success)
                Members.Add(instance);

            return outcome;
        }

        protected abstract OperationOutcome IsMember(IElementNavigator candidate);

        public virtual OperationOutcome Validate()
        {
            var outcome = new OperationOutcome();

            if (Cardinality.InRange(Members.Count))
                Validator.Trace(outcome, $"Instance count for {Name}' is {Members.Count}, which is not within the specified cardinality of {Cardinality.ToString()}",
                        Issue.CONTENT_INCORRECT_OCCURRENCE, ErrorLocation);

            return outcome;
        }
    }
}
