namespace Hl7.Fhir.Model
{
    public interface IResource
    {
        string TypeName { get; }
        ResourceType ResourceType { get; }
        Id IdElement { get; set; }
        string Id { get; set; }
        Meta Meta { get; set; }
        FhirUri ImplicitRulesElement { get; set; }
        string ImplicitRules { get; set; }
        Code LanguageElement { get; set; }
        string Language { get; set; }
    }
}
