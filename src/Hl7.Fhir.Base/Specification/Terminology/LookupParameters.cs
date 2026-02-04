/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Specification.Terminology;

public class LookupParameters : Parameters
{
    public const string CODE_ATTRIBUTE = "code";
    public const string SYSTEM_ATTRIBUTE = "system";
    public const string VERSION_ATTRIBUTE = "version";
    public const string CODING_ATTRIBUTE = "coding";
    public const string DATE_ATTRIBUTE = "date";
    public const string DISPLAY_LANGUAGE_ATTRIBUTE = "displayLanguage";
    public const string PROPERTY_ATTRIBUTE = "property";

    public LookupParameters()
    {
        // Nothing
    }

    public LookupParameters(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// The code that is to be located. If a code is provided, a system must be provided.
    /// </summary>
    public Code? Code
    {
        get => this.GetSingleValue<Code>(CODE_ATTRIBUTE);
        set => this.SetSingleValue(CODE_ATTRIBUTE, value);
    }

    /// <summary>
    /// The system for the code that is to be located.
    /// </summary>
    public FhirUri? System
    {
        get => this.GetSingleValue<FhirUri>(SYSTEM_ATTRIBUTE);
        set => this.SetSingleValue(SYSTEM_ATTRIBUTE, value);
    }

    /// <summary>
    /// The version of the system, if one was provided in the source data.
    /// </summary>
    public FhirString? Version
    {
        get => this.GetSingleValue<FhirString>(VERSION_ATTRIBUTE);
        set => this.SetSingleValue(VERSION_ATTRIBUTE, value);
    }

    /// <summary>
    /// A coding to look up.
    /// </summary>
    public Coding? Coding
    {
        get => this.GetSingleValue<Coding>(CODING_ATTRIBUTE);
        set => this.SetSingleValue(CODING_ATTRIBUTE, value);
    }

    /// <summary>
    /// The date for which the information should be returned.
    /// </summary>
    public FhirDateTime? Date
    {
        get => this.GetSingleValue<FhirDateTime>(DATE_ATTRIBUTE);
        set => this.SetSingleValue(DATE_ATTRIBUTE, value);
    }

    /// <summary>
    /// The requested language for display.
    /// </summary>
    public Code? DisplayLanguage
    {
        get => this.GetSingleValue<Code>(DISPLAY_LANGUAGE_ATTRIBUTE);
        set => this.SetSingleValue(DISPLAY_LANGUAGE_ATTRIBUTE, value);
    }

    /// <summary>
    /// A property that the client wishes to be returned in the output.
    /// </summary>
    /// <remarks>If no properties are specified, the server chooses what to return.</remarks>
    public IEnumerable<Code>? Property
    {
        get => this.GetMultipleValues<Code>(PROPERTY_ATTRIBUTE);
        set => this.SetMultipleValues(PROPERTY_ATTRIBUTE, value);
    }

    #region Builder methods
    public LookupParameters WithCode(string? code = null, string? system = null, string? version = null, string? displayLanguage = null)
    {
        if (!string.IsNullOrWhiteSpace(code)) Code = new Code(code);
        if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
        if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);
        if (!string.IsNullOrWhiteSpace(displayLanguage)) DisplayLanguage = new Code(displayLanguage);
        return this;
    }

    public LookupParameters WithDate(FhirDateTime? date)
    {
        Date = date;
        return this;
    }

    public LookupParameters WithProperties(string[]? properties)
    {
        Property = properties?.Select(p => new Code(p));
        return this;
    }
    #endregion

    [Obsolete("This is just a DeepCopy of the current instance, use the instance or DeepCopy() instead", false)]
    public Parameters Build() => (Parameters)this.DeepCopyInternal();
}
