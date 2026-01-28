﻿/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Typed parameters utility class for the <see cref="ITerminologyServiceWithClosure.Closure"/> operation.
/// </summary>
public class ClosureParameters : Parameters
{
    public const string NAME_ATTRIBUTE = "name";
    public const string CONCEPT_ATTRIBUTE = "concept";
    public const string VERSION_ATTRIBUTE = "version";

    public ClosureParameters()
    {
        // Nothing
    }

    public ClosureParameters(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        Name = new FhirString(name);
    }

    public ClosureParameters(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// The name that defines the particular context for the subsumption based closure table.
    /// </summary>
    public FhirString? Name
    {
        get => this.GetSingleValue<FhirString>(NAME_ATTRIBUTE);
        set => this.SetSingleValue(NAME_ATTRIBUTE, value);
    }

    /// <summary>
    /// Concepts to add to the closure table.
    /// </summary>
    public IEnumerable<Coding>? Concept
    {
        get => this.GetMultipleValues<Coding>(CONCEPT_ATTRIBUTE);
        set => this.SetMultipleValues(CONCEPT_ATTRIBUTE, value);
    }

    /// <summary>
    /// A request to resynchronise - request to send all new entries since the nominated version was sent by the server.
    /// </summary>
    public FhirString? Version
    {
        get => this.GetSingleValue<FhirString>(VERSION_ATTRIBUTE);
        set => this.SetSingleValue(VERSION_ATTRIBUTE, value);
    }

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

    [Obsolete("This is just a DeepCopy of the current instance, use the instance or DeepCopy() instead", false)]
    public Parameters Build() => this.DeepCopy();
}