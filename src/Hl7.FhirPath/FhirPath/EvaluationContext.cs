using Hl7.Fhir.ElementModel;
using System;

namespace Hl7.FhirPath
{
    public class EvaluationContext
    {
        [Obsolete("Please use CreateDefault() instead of this member, which may cause raise conditions.")]
        public static readonly EvaluationContext Default = new EvaluationContext();

        public static EvaluationContext CreateDefault() => new EvaluationContext();

        public EvaluationContext()
        {
            // no defaults yet
        }

        public EvaluationContext(IElementNavigator container)
        {
            Container = container;
        }

        public IElementNavigator Container { get; set; }
    }


}