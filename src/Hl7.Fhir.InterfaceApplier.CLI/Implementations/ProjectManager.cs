using Hl7.Fhir.InterfaceApplier.CLI.Abstractions;
using Hl7.Fhir.InterfaceApplier.CLI.Configurations;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Hl7.Fhir.InterfaceApplier.CLI.Implementations;

public class ProjectManager : IProjectManager
{
    #region Private Fields

    private readonly ProjectManagerConfiguration _configuration;

    #endregion

    #region Constructors

    public ProjectManager(IOptions<ProjectManagerConfiguration> options)
    {
        _configuration = options.Value;
    }

    #endregion

    #region Public Methods

    public string GetFhirBaseProjectDirectory() => getProjectFilePath("Hl7.Fhir.Base.csproj");

    public string GetFhirConformanceProjectDirectory() => getProjectFilePath("Hl7.Fhir.Conformance.csproj");

    public IReadOnlyCollection<string> GetFhirVersionProjectFilePaths()
        => Directory.GetFiles(_configuration.SourcesDirectory, "Hl7.Fhir.*.csproj", SearchOption.AllDirectories)
            .Where(isFhirVersionProject)
            .Select(Path.GetFullPath)
            .ToList();

    public (string buildDirectory, string projectDirectory) PublishProject(string projectFilePath)
    {
        const string buildConfiguration = "Debug";
        const string relativeOutputPath = $"bin/{buildConfiguration}/InterfaceApplier";
        var projectDirectory = Path.GetDirectoryName(projectFilePath);
        var buildDirectory = Path.GetFullPath(Path.Join(projectDirectory, relativeOutputPath));

        if (projectDirectory == null)
        {
            throw new InvalidOperationException(
                $"The directory where '{projectFilePath}' is located could not be found");
        }

        if (Directory.Exists(buildDirectory))
        {
            Directory.Delete(buildDirectory, true);
        }

        var process = new Process();
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.WorkingDirectory = projectDirectory;
        process.StartInfo.Arguments = $"publish --configuration {buildConfiguration}" +
                                      $"        --framework net6.0" +
                                      $"        --self-contained true " +
                                      $"        --output {relativeOutputPath}";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();

        return (buildDirectory, projectDirectory);
    }

    #endregion

    #region Private Methods

    private string getProjectFilePath(string projectName)
        => Directory.GetFiles(_configuration.SourcesDirectory, projectName, SearchOption.AllDirectories)
               .Select(Path.GetFullPath)
               .Select(Path.GetDirectoryName)
               .SingleOrDefault()
           ?? throw new InvalidOperationException(
               $"The project file for project with name={projectName} could not be found");

    private bool isFhirVersionProject(string projectFilePath)
        => _configuration.FhirVersions
            .Any(fhirVersion => projectFilePath.EndsWith($"Hl7.Fhir.{fhirVersion}.csproj"));

    #endregion
}