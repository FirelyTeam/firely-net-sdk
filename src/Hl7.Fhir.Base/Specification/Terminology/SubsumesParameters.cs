/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Specification.Terminology;

public class SubsumesParameters : Parameters
{
    public SubsumesParameters()
    {
        // Nothing
    }

    public SubsumesParameters(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    public const string CODE_A_ATTRIBUTE = "codeA";
    public const string CODE_B_ATTRIBUTE = "codeB";
    public const string SYSTEM_ATTRIBUTE = "system";
    public const string VERSION_ATTRIBUTE = "version";
    public const string CODING_A_ATTRIBUTE = "codingA";
    public const string CODING_B_ATTRIBUTE = "codingB";

    /// <summary>
    /// The "A" code that is to be tested. If a code is provided, a system must be provided.
    /// </summary>
    public Code? CodeA
    {
        get => this.GetSingleValue<Code>(CODE_A_ATTRIBUTE);
        set => this.SetSingleValue(CODE_A_ATTRIBUTE, value);
    }

    /// <summary>
    /// The "B" code that is to be tested. If a code is provided, a system must be provided.
    /// </summary>
    public Code? CodeB
    {
        get => this.GetSingleValue<Code>(CODE_B_ATTRIBUTE);
        set => this.SetSingleValue(CODE_B_ATTRIBUTE, value);
    }

    /// <summary>
    /// The code system in which subsumption testing is to be performed.
    /// This must be provided unless the operation is invoked on a code system instance.
    /// </summary>
    public FhirUri? System
    {
        get => this.GetSingleValue<FhirUri>(SYSTEM_ATTRIBUTE);
        set => this.SetSingleValue(SYSTEM_ATTRIBUTE, value);
    }

    /// <summary>
    /// The version of the code system, if one was provided in the source data.
    /// </summary>
    public FhirString? Version
    {
        get => this.GetSingleValue<FhirString>(VERSION_ATTRIBUTE);
        set => this.SetSingleValue(VERSION_ATTRIBUTE, value);
    }

    /// <summary>
    /// The "A" Coding that is to be tested.
    /// </summary>
    public Coding? CodingA
    {
        get => this.GetSingleValue<Coding>(CODING_A_ATTRIBUTE);
        set => this.SetSingleValue(CODING_A_ATTRIBUTE, value);
    }

    /// <summary>
    /// The "B" Coding that is to be tested.
    /// </summary>
    public Coding? CodingB
    {
        get => this.GetSingleValue<Coding>(CODING_B_ATTRIBUTE);
        set => this.SetSingleValue(CODING_B_ATTRIBUTE, value);
    }

    #region Build methods
    public SubsumesParameters WithCode(string codeA, string codeB, string? system = null, string? version = null)
    {
        if (!string.IsNullOrWhiteSpace(codeA)) CodeA = new Code(codeA);
        if (!string.IsNullOrWhiteSpace(codeB)) CodeB = new Code(codeB);
        if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
        if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);

        return this;
    }

    public SubsumesParameters WithCoding(Coding codingA, Coding codingB, string? system = null, string? version = null)
    {
        CodingA = codingA;
        CodingB = codingB;
        if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
        if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);

        return this;
    }
    #endregion

    [Obsolete("This is just a DeepCopy of the current instance, use the instance or DeepCopy() instead", false)]
    public Parameters Build() => this.DeepCopy();
}