
using Hl7.Fhir.FluentPath.InstanceTree;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath
{

    public class FhirEvaluationContext : BaseEvaluationContext
    {
        public FhirEvaluationContext()
        {
        }

        public FhirEvaluationContext(FhirClient client) : this(client, null)
        {
        }

        public FhirEvaluationContext(IFluentPathElement originalResource) : this(null, originalResource)
        {
        }

        public FhirEvaluationContext(FhirClient client, IFluentPathElement originalResource)
        {
            FhirClient = client;
            OriginalResource = originalResource;
        }


        FhirClient FhirClient { get; set; }

        public override void InvokeExternalFunction(string name, IList<IEnumerable<IFluentPathValue>> parameters)
        {
            if(name == "resolve")
            {
                
            }
            else
                throw new NotSupportedException($"Function '{name}' is unknown");
        }


        public override IFluentPathValue ResolveConstant(string name)
        {
            string value = null;

            if (name.StartsWith("ext-"))
                value = "http://hl7.org/fhir/StructureDefinition/" + name.Substring(4);
            else if (name.StartsWith("vs-"))
                value = "http://hl7.org/fhir/ValueSet/" + name.Substring(3);
            else if (name == "sct")
                value = "http://snomed.info/sct";
            else if (name == "loinc")
                value = "http://loinc.org";
            else if (name == "ucum")
                value = "http://unitsofmeasure.org";

            return value != null ? new ConstantValue(value) : null;
        }

        public virtual IFluentPathElement ResolveResource(string url)
        {
            if (FhirClient == null)
                throw Error.InvalidOperation($"The EvaluationContext does not have a FhirClient to use to resolve url '{url}'");

            try
            {
                var resource = FhirClient.Get(url);
                if (resource == null) return null;

                var xml = FhirSerializer.SerializeResourceToXml(resource);
                return TreeConstructor.FromXml(xml);
            }
            catch (Exception e)
            {
                throw e;
                //return null;
            }
        }
    }

}
