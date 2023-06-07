namespace Hl7.Fhir.InterfaceApplier.CLI.Configurations;

public class ProjectManagerConfiguration
{
    private IReadOnlyCollection<string>? _fhirVersions;

    #region Public Properties

    public string SourcesDirectory { get; set; } = string.Empty;

    public IReadOnlyCollection<string> FhirVersions
    {
        get => _fhirVersions ??= new List<string>();
        set => _fhirVersions = value;
    }

    #endregion
}