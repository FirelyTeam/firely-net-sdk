#nullable enable

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization;

internal static class FilterPredicateExtensions
{
    // Note that CodedValidationExceptions are coming from property validation, and so are by definition
    // recoverable, since the data was already safely in the POCO by that time.
    internal static Predicate<CodedException> IsRecoverableIssue =>
        ce => ce is CodedValidationException ||
              FhirXmlException.RecoverableIssues.Concat(FhirJsonException.RecoverableIssues).Contains(ce.ErrorCode);

    internal static Predicate<CodedException> IsInList(this IEnumerable<string> ignoreList) =>
        ce => ignoreList.Contains(ce.ErrorCode);

    internal static Predicate<CodedException> IsBackwardsCompatibilityIssue =>
        FhirXmlException.BackwardsCompatibilityAllowedIssues
            .Concat(FhirJsonException.BackwardsCompatibilityAllowedIssues)
            .IsInList();
    
    internal static Predicate<CodedException> And(this Predicate<CodedException> a, Predicate<CodedException>? b) =>
        b is not null ? ce => a(ce) && b(ce) : a;
    
    internal static Predicate<CodedException> Or(this Predicate<CodedException> a, Predicate<CodedException>? b) =>
        b is not null ? ce => a(ce) || b(ce) : a;
    
    internal static Predicate<CodedException> Negate(this Predicate<CodedException> a) => 
        ce => !a(ce);
}