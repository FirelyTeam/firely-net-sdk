
using Hl7.Fhir.FluentPath.InstanceTree;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using Hl7.Fhir.Navigation;

namespace Hl7.Fhir.FluentPath
{

    public class TestEvaluationContext : BaseEvaluationContext
    {
        IElementNavigator OriginalResource { get; }

        public TestEvaluationContext()
        {
        }

        public TestEvaluationContext(IElementNavigator originalResource)
        {
            OriginalResource = originalResource;
        }

       
        public override IEnumerable<IValueProvider> InvokeExternalFunction(string name, IEnumerable<IValueProvider> focus, IEnumerable<IEnumerable<IValueProvider>> parameters)
        {
            if(name == "resolve")
            {
                throw new NotImplementedException();        
            }
            else
                throw new NotSupportedException($"Function '{name}' is unknown");
        }


        public override IEnumerable<IValueProvider> ResolveValue(string name)
        {
            var baseValue = base.ResolveValue(name);
            if (baseValue != null) return baseValue;

            if (name == "resource")
                return FhirValueList.Create(OriginalResource);

            return null;
        }

        public virtual IElementNavigator ResolveResource(string url)
        {
            throw new NotImplementedException("ResolveResource cannot be tested unless you test it using a FhirClient - not available in FluentPath");
        }
    }

}
