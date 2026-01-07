/* 
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Typed result utility class for the CodeSystem/$subsumes operation.
/// </summary>
public class SubsumesResult : Parameters
{
    public const string OUTCOME_ATTRIBUTE = "outcome";

    public SubsumesResult()
    {
        // Nothing
    }

    public SubsumesResult(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// The subsumption relationship between codeA and codeB.
    /// Possible values: equivalent | subsumes | subsumed-by | not-subsumed
    /// </summary>
    public Code<SubsumptionOutcome>? Outcome => this.GetSingleValue<Code<SubsumptionOutcome>>(OUTCOME_ATTRIBUTE);

    /// <summary>
    /// Gets the subsumption outcome as an enum value.
    /// </summary>
    public SubsumptionOutcome? OutcomeValue => Outcome?.Value;

    /// <summary>
    /// Creates a SubsumesResult with the given outcome.
    /// </summary>
    public static SubsumesResult ForOutcome(SubsumptionOutcome outcome)
    {
        var result = new SubsumesResult();
        result.Add(OUTCOME_ATTRIBUTE, new Code(outcome.GetLiteral()));
        return result;
    }

    /// <summary>
    /// Creates a SubsumesResult with the given outcome code.
    /// </summary>
    public static SubsumesResult ForOutcome(string outcome)
    {
        var result = new SubsumesResult();
        result.Add(OUTCOME_ATTRIBUTE, new Code(outcome));
        return result;
    }
}

/// <summary>
/// Subsumption outcomes for the $subsumes operation.
/// </summary>
public enum SubsumptionOutcome
{
    /// <summary>
    /// The two concepts are equivalent (have the same properties).
    /// </summary>
    [EnumLiteral("equivalent", "http://hl7.org/fhir/concept-subsumption-outcome")]
    Equivalent,

    /// <summary>
    /// Code A subsumes Code B (e.g. B has all the properties of A, and some of its own).
    /// </summary>
    [EnumLiteral("subsumes", "http://hl7.org/fhir/concept-subsumption-outcome")]
    Subsumes,

    /// <summary>
    /// Code A is subsumed by Code B (e.g. A has all the properties of B, and some of its own).
    /// </summary>
    [EnumLiteral("subsumed-by", "http://hl7.org/fhir/concept-subsumption-outcome")]
    SubsumedBy,

    /// <summary>
    /// Code A and Code B are not related by subsumption.
    /// </summary>
    [EnumLiteral("not-subsumed", "http://hl7.org/fhir/concept-subsumption-outcome")]
    NotSubsumed
}

