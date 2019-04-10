using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;

namespace Hl7.FhirPath
{
    public class EvaluationContext
    {
        public static EvaluationContext CreateDefault() => new EvaluationContext();

        public EvaluationContext()
        {
            // no defaults yet
        }

        public EvaluationContext(ITypedElement container)
        {
            Container = container;
        }

        public ITypedElement Container { get; set; }

        public Action<string, IEnumerable<ITypedElement>> Tracer { get; set; }

        #region Obsolete members
        [Obsolete("Please use CreateDefault() instead of this member, which may cause raise conditions.")]
        public static readonly EvaluationContext Default = new EvaluationContext();

        [Obsolete("Use EvaluationContext(ITypedElement container) instead. Obsolete since 2018-10-17")]
        public EvaluationContext(IElementNavigator container)
        {
            Container = container.ToTypedElement();
        }

        #endregion
    }
}