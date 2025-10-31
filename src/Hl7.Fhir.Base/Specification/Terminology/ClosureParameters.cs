#nullable enable

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Typed parameters utility class for the <see cref="ITerminologyServiceWithClosure.Closure"/> operation.
/// </summary>
public class ClosureParameters
{
    public ClosureParameters(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        Name = new FhirString(name);
    }

    /// <summary>
    /// The name that defines the particular context for the subsumption based closure table.
    /// </summary>
    public FhirString Name { get; }

    /// <summary>
    /// Concepts to add to the closure table.
    /// </summary>
    public IEnumerable<Coding>? Concept { get; private set; }

    /// <summary>
    /// A request to resynchronise - request to send all new entries since the nominated version was sent by the server.
    /// </summary>
    public FhirString? Version { get; private set; }

    #region Builder methods
    public ClosureParameters WithConcepts(IEnumerable<Coding>? codings)
    {
        Concept = codings;
        return this;
    }

    public ClosureParameters WithVersion(string? version)
    {
        Version = !string.IsNullOrWhiteSpace(version) ? new FhirString(version) : null;
        return this;
    }
    #endregion

    public Parameters Build()
    {
        var result = new Parameters();

        result.Add("name", Name);

        foreach (var concept in Concept ?? [])
        {
            result.Add("concept", concept);
        }

        if (Version is not null) result.Add("version", Version);

        return result;
    }
}