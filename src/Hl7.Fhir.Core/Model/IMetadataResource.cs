namespace Hl7.Fhir.Model
{
    public interface IMetadataResource
    {
        string Url { get; set; }
        FhirUri UrlElement { get; set; }

        string Name { get; set; }
        FhirString NameElement { get; set; }

        string Publisher { get; set; }
        FhirString PublisherElement { get; set; }

        string Description { get; set; }

        bool? Experimental { get; set; }
        FhirBoolean ExperimentalElement { get; set; }

        string Date { get; set; }
        FhirDateTime DateElement { get; set; }
    }
}
