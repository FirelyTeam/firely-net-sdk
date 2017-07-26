using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Collections.Generic;

namespace Hl7.Fhir.Validation
{
    internal abstract class BaseBucket : IBucket
    {
        internal protected BaseBucket(ElementDefinition definition)
        {
            Name = definition.Path + (definition.Name != null ? $":{definition.Name}" : null);
            Cardinality = Cardinality.FromElementDefinition(definition);
        }

        public string Name { get; private set; }
        public Cardinality Cardinality { get; private set; }
        public IList<ScopedNavigator> Members { get; private set; } = new List<ScopedNavigator>();

        public abstract bool Add(ScopedNavigator instance);
  
        public virtual OperationOutcome Validate(Validator validator, IElementNavigator errorLocation)
        {
            var outcome = new OperationOutcome();

            if (!Cardinality.InRange(Members.Count))
                validator.Trace(outcome, $"Instance count for '{Name}' is {Members.Count}, which is not within the specified cardinality of {Cardinality.ToString()}",
                        Issue.CONTENT_INCORRECT_OCCURRENCE, errorLocation);

            return outcome;
        }
    }
}
