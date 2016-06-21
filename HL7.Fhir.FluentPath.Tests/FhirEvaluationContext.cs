
using Hl7.Fhir.FluentPath.InstanceTree;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using Hl7.Fhir.Navigation;

namespace Hl7.Fhir.FluentPath
{

    public class FhirEvaluationContext : BaseEvaluationContext
    {
        IElementNavigator OriginalResource { get; }

        public FhirEvaluationContext()
        {
        }

        public FhirEvaluationContext(FhirClient client) : this(client, null)
        {
        }

        public FhirEvaluationContext(IElementNavigator originalResource) : this(null, originalResource)
        {
        }

        public FhirEvaluationContext(FhirClient client, IElementNavigator originalResource)
        {
            FhirClient = client;
            OriginalResource = originalResource;
        }


        FhirClient FhirClient { get; set; }

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
            if (FhirClient == null)
                throw Error.InvalidOperation($"The EvaluationContext does not have a FhirClient to use to resolve url '{url}'");

            try
            {
                var resource = FhirClient.Get(url);
                if (resource == null) return null;

                var xml = FhirSerializer.SerializeResourceToXml(resource);
                var tree = TreeConstructor.FromXml(xml);
                return new TreeNavigator(tree);
            }
            catch (Exception e)
            {
                throw e;
                //return null;
            }
        }
    }

}
