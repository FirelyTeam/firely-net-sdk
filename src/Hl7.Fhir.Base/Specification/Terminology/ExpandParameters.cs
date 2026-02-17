#nullable enable

/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Terminology;

public class ExpandParameters : Parameters
{
    public const string URL_ATTRIBUTE = "url";
    public const string VALUE_SET_ATTRIBUTE = "valueSet";
    public const string VALUE_SET_VERSION_ATTRIBUTE = "valueSetVersion";
    public const string CONTEXT_ATTRIBUTE = "context";
    public const string CONTEXT_DIRECTION_ATTRIBUTE = "contextDirection";
    public const string FILTER_ATTRIBUTE = "filter";
    public const string DATE_ATTRIBUTE = "date";
    public const string OFFSET_ATTRIBUTE = "offset";
    public const string COUNT_ATTRIBUTE = "count";
    public const string INCLUDE_DESIGNATIONS_ATTRIBUTE = "includeDesignations";
    public const string DESIGNATION_ATTRIBUTE = "designation";
    public const string INCLUDE_DEFINITION_ATTRIBUTE = "includeDefinition";
    public const string ACTIVE_ONLY_ATTRIBUTE = "activeOnly";
    public const string EXCLUDE_NESTED_ATTRIBUTE = "excludeNested";
    public const string EXCLUDE_NOT_FOR_UI_ATTRIBUTE = "excludeNotForUI";
    public const string EXCLUDE_POST_COORDINATED_ATTRIBUTE = "excludePostCoordinated";
    public const string DISPLAY_LANGUAGE_ATTRIBUTE = "displayLanguage";
    public const string EXCLUDE_SYSTEM_ATTRIBUTE = "excludeSystem";
    public const string SYSTEM_VERSION_ATTRIBUTE = "systemVersion";
    public const string CHECK_SYSTEM_VERSION_ATTRIBUTE = "checkSystemVersion";
    public const string FORCE_SYSTEM_VERSION_ATTRIBUTE = "forceSystemVersion";

    public ExpandParameters()
    {
        // Nothing
    }

    public ExpandParameters(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// A canonical reference to a value set.
    /// </summary>
    public FhirUri? Url
    {
        get => this.GetSingleValue<FhirUri>(URL_ATTRIBUTE);
        set => this.SetSingleValue(URL_ATTRIBUTE, value);
    }

    /// <summary>
    /// The value set is provided directly as part of the request.
    /// </summary>
    public Resource? ValueSet
    {
        get => this.GetSingleResource(VALUE_SET_ATTRIBUTE);
        set => this.SetSingleResource(VALUE_SET_ATTRIBUTE, value);
    }

    /// <summary>
    /// The identifier that is used to identify a specific version of the value set to be used when generating the expansion.
    /// </summary>
    public FhirString? ValueSetVersion
    {
        get => this.GetSingleValue<FhirString>(VALUE_SET_VERSION_ATTRIBUTE);
        set => this.SetSingleValue(VALUE_SET_VERSION_ATTRIBUTE, value);
    }

    /// <summary>
    /// The context of the value set, so that the server can resolve this to a value set to expand.
    /// </summary>
    public FhirUri? Context
    {
        get => this.GetSingleValue<FhirUri>(CONTEXT_ATTRIBUTE);
        set => this.SetSingleValue(CONTEXT_ATTRIBUTE, value);
    }

    /// <summary>
    /// If a context is provided, a context direction may also be provided.
    /// </summary>
    public ContextDirection? ContextDirection
    {
        get
        {
            var code = this.GetSingleValue<Code>(CONTEXT_DIRECTION_ATTRIBUTE);
            return code != null ? EnumUtility.ParseLiteral<ContextDirection>(code.Value) : null;
        }
        set => this.SetSingleValue(CONTEXT_DIRECTION_ATTRIBUTE, value.HasValue ? new Code(value.GetLiteral()) : null);
    }

    /// <summary>
    /// A text filter that is applied to restrict the codes that are returned.
    /// </summary>
    public FhirString? Filter
    {
        get => this.GetSingleValue<FhirString>(FILTER_ATTRIBUTE);
        set => this.SetSingleValue(FILTER_ATTRIBUTE, value);
    }

    /// <summary>
    /// The date for which the expansion should be generated.
    /// </summary>
    public FhirDateTime? Date
    {
        get => this.GetSingleValue<FhirDateTime>(DATE_ATTRIBUTE);
        set => this.SetSingleValue(DATE_ATTRIBUTE, value);
    }

    /// <summary>
    /// Where to start if a subset is desired (default = 0)
    /// </summary>
    public Integer? Offset
    {
        get => this.GetSingleValue<Integer>(OFFSET_ATTRIBUTE);
        set => this.SetSingleValue(OFFSET_ATTRIBUTE, value);
    }

    /// <summary>
    /// How many codes should be provided in a partial page view
    /// </summary>
    public Integer? Count
    {
        get => this.GetSingleValue<Integer>(COUNT_ATTRIBUTE);
        set => this.SetSingleValue(COUNT_ATTRIBUTE, value);
    }

    /// <summary>
    /// Controls whether concept designations are to be included or excluded in value set expansions.
    /// </summary>
    public FhirBoolean? IncludeDesignations
    {
        get => this.GetSingleValue<FhirBoolean>(INCLUDE_DESIGNATIONS_ATTRIBUTE);
        set => this.SetSingleValue(INCLUDE_DESIGNATIONS_ATTRIBUTE, value);
    }

    /// <summary>
    /// A token that specifies a system+code that is either a use or a language.
    /// </summary>
    public IEnumerable<FhirString>? Designation
    {
        get => this.GetMultipleValues<FhirString>(DESIGNATION_ATTRIBUTE);
        set => this.SetMultipleValues(DESIGNATION_ATTRIBUTE, value);
    }

    /// <summary>
    /// Controls whether the value set definition is included or excluded in value set expansions.
    /// </summary>
    public FhirBoolean? IncludeDefinition
    {
        get => this.GetSingleValue<FhirBoolean>(INCLUDE_DEFINITION_ATTRIBUTE);
        set => this.SetSingleValue(INCLUDE_DEFINITION_ATTRIBUTE, value);
    }

    /// <summary>
    /// Controls whether inactive concepts are included or excluded in value set expansions.
    /// </summary>
    public FhirBoolean? ActiveOnly
    {
        get => this.GetSingleValue<FhirBoolean>(ACTIVE_ONLY_ATTRIBUTE);
        set => this.SetSingleValue(ACTIVE_ONLY_ATTRIBUTE, value);
    }

    /// <summary>
    /// Controls whether or not the value set expansion nests codes or not (i.e. ValueSet.expansion.contains.contains).
    /// </summary>
    public FhirBoolean? ExcludeNested
    {
        get => this.GetSingleValue<FhirBoolean>(EXCLUDE_NESTED_ATTRIBUTE);
        set => this.SetSingleValue(EXCLUDE_NESTED_ATTRIBUTE, value);
    }

    /// <summary>
    /// Controls whether or not the value set expansion is assembled for a user interface use or not.
    /// </summary>
    public FhirBoolean? ExcludeNotForUI
    {
        get => this.GetSingleValue<FhirBoolean>(EXCLUDE_NOT_FOR_UI_ATTRIBUTE);
        set => this.SetSingleValue(EXCLUDE_NOT_FOR_UI_ATTRIBUTE, value);
    }

    /// <summary>
    /// Controls whether or not the value set expansion includes post coordinated codes.
    /// </summary>
    public FhirBoolean? ExcludePostCoordinated
    {
        get => this.GetSingleValue<FhirBoolean>(EXCLUDE_POST_COORDINATED_ATTRIBUTE);
        set => this.SetSingleValue(EXCLUDE_POST_COORDINATED_ATTRIBUTE, value);
    }

    /// <summary>
    /// Specifies the language to be used for description in the expansions i.e. the language to be used for ValueSet.expansion.contains.display
    /// </summary>
    public Code? DisplayLanguage
    {
        get => this.GetSingleValue<Code>(DISPLAY_LANGUAGE_ATTRIBUTE);
        set => this.SetSingleValue(DISPLAY_LANGUAGE_ATTRIBUTE, value);
    }

    /// <summary>
    /// Code system, or a particular version of a code system to be excluded from the value set expansion.
    /// </summary>
    /// <remarks> The format is the same as a canonical URL: [system]|[version] - e.g. http://loinc.org|2.56.</remarks>
    public IEnumerable<Canonical>? ExcludeSystem
    {
        get => this.GetMultipleValues<Canonical>(EXCLUDE_SYSTEM_ATTRIBUTE);
        set => this.SetMultipleValues(EXCLUDE_SYSTEM_ATTRIBUTE, value);
    }

    /// <summary>
    /// Specifies a version to use for a system, if the value set does not specify which one to use.
    /// </summary>
    /// <remarks>The format is the same as a canonical URL: [system]|[version] - e.g. http://loinc.org|2.56.</remarks>
    public IEnumerable<Canonical>? SystemVersion
    {
        get => this.GetMultipleValues<Canonical>(SYSTEM_VERSION_ATTRIBUTE);
        set => this.SetMultipleValues(SYSTEM_VERSION_ATTRIBUTE, value);
    }

    /// <summary>
    /// Specifies a version to use for a system. If a value set specifies a different version, an error is returned instead of the expansion.
    /// </summary>
    /// <remarks>The format is the same as a canonical URL: [system]|[version] - e.g. http://loinc.org|2.56.</remarks>
    public IEnumerable<Canonical>? CheckSystemVersion
    {
        get => this.GetMultipleValues<Canonical>(CHECK_SYSTEM_VERSION_ATTRIBUTE);
        set => this.SetMultipleValues(CHECK_SYSTEM_VERSION_ATTRIBUTE, value);
    }

    /// <summary>
    /// Specifies a version to use for a system. This parameter overrides any specified version in the value set (and any it depends on).
    /// </summary>
    /// <remarks>The format is the same as a canonical URL: [system]|[version] - e.g. http://loinc.org|2.56.</remarks>
    public IEnumerable<Canonical>? ForceSystemVersion
    {
        get => this.GetMultipleValues<Canonical>(FORCE_SYSTEM_VERSION_ATTRIBUTE);
        set => this.SetMultipleValues(FORCE_SYSTEM_VERSION_ATTRIBUTE, value);
    }

    #region Build methods

    public ExpandParameters WithValueSet(string? url = null, Resource? valueSet = null, string? valueSetVersion = null, string? context = null,
        ContextDirection? contextDirection = null)
    {
        if (!string.IsNullOrWhiteSpace(url)) Url = new FhirUri(url);
        ValueSet = valueSet;
        if (!string.IsNullOrWhiteSpace(valueSetVersion)) ValueSetVersion = new FhirString(valueSetVersion);
        if (!string.IsNullOrWhiteSpace(context)) Context = new FhirUri(context);
        ContextDirection = contextDirection;
        return this;
    }

    public ExpandParameters WithFilter(string filter)
    {
        if (!string.IsNullOrWhiteSpace(filter)) Filter = new FhirString(filter);
        return this;
    }

    public ExpandParameters WithPaging(int? offset = null, int? count = null)
    {
        if (offset.HasValue) Offset = new Integer(offset);
        if (count.HasValue) Count = new Integer(count);
        return this;
    }

    public ExpandParameters WithDesignation(bool? includeDesignation = null, string[]? designations = null)
    {
        if (includeDesignation.HasValue) IncludeDesignations = new FhirBoolean(includeDesignation);
        Designation = designations?.Select(d => new FhirString(d));
        return this;
    }

    #endregion
    
    
    protected internal override Base DeepCopyInternal() => new ExpandParameters(this);

    [Obsolete("This is just a DeepCopy of the current instance, use the instance or DeepCopy() instead", false)]
    public Parameters Build() => this.DeepCopy();
}
