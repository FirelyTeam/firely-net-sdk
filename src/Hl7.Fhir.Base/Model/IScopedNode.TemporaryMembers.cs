using Hl7.Fhir.Specification;
using System;

namespace Hl7.Fhir.Model;

#nullable enable

public partial interface IScopedNode
{
    [Obsolete]
    new IElementDefinitionSummary? Definition => throw new NotImplementedException();

    // [Obsolete] new string? InstanceType => throw new NotImplementedException(); Commented this out to avoid compilation error
}