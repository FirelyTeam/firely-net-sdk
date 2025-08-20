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
    internal static Predicate<CodedException> IsRecoverableIssue => 
        FhirXmlException.RecoverableIssues.Concat(FhirJsonException.RecoverableIssues).Append(CodedValidationException.ID_LITERAL_INVALID_CODE).ToPredicate();

    internal static Predicate<CodedException> IsBackwardsCompatibilityIssue =>
        FhirXmlException.BackwardsCompatibilityAllowedIssues.Concat(FhirJsonException.BackwardsCompatibilityAllowedIssues).ToPredicate();
    
    internal static Predicate<CodedException> ToPredicate(this IEnumerable<string> ignoreList) => 
        ce => ignoreList.Contains(ce.ErrorCode);

    internal static Predicate<CodedException> And(this Predicate<CodedException> a, Predicate<CodedException>? b) =>
        b is not null ? ce => a(ce) && b(ce) : a;
    
    internal static Predicate<CodedException> Or(this Predicate<CodedException> a, Predicate<CodedException>? b) =>
        b is not null ? ce => a(ce) || b(ce) : a;
    
    internal static Predicate<CodedException> Negate(this Predicate<CodedException> a) => 
        ce => !a(ce);
    
}

#nullable restore