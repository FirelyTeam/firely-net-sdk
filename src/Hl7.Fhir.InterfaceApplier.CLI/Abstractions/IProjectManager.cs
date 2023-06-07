namespace Hl7.Fhir.InterfaceApplier.CLI.Abstractions;

public interface IProjectManager
{
    public string GetFhirBaseProjectDirectory();

    public string GetFhirConformanceProjectDirectory();

    public IReadOnlyCollection<string> GetFhirVersionProjectFilePaths();

    public (string buildDirectory, string projectDirectory) PublishProject(string projectFilePath);
}