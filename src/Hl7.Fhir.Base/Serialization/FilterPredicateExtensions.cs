#nullable enable

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization;

internal static class FilterPredicateExtensions
{
    internal static Predicate<CodedException> IsRecoverableIssue => 
        FromList(FhirXmlException.RecoverableIssues.Union(FhirJsonException.RecoverableIssues));

    internal static Predicate<CodedException> IsBackwardsCompatibilityIssue =>
        FromList(FhirXmlException.BackwardsCompatibilityAllowedIssues.Union(FhirJsonException.BackwardsCompatibilityAllowedIssues));

    internal static Predicate<CodedException> FromList(IEnumerable<string> ignoreList) => 
        ce => ignoreList.Contains(ce.ErrorCode);

    internal static Predicate<CodedException> And(this Predicate<CodedException> a, Predicate<CodedException>? b) =>
        b is not null ? ce => a(ce) && b(ce) : a;
    
    internal static Predicate<CodedException> Or(this Predicate<CodedException> a, Predicate<CodedException>? b) =>
        b is not null ? ce => a(ce) || b(ce) : a;
}

#nullable restore