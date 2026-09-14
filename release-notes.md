## Intro:

This release adds opt-in retention of XML comments in the POCO-based parser. There are no breaking changes.

**Serialization**
- The POCO-based XML parser can now retain the comments found in the source document. Set `DeserializerSettings.RetainComments` to `true` (the default remains `false`) and comments are attached to the parsed POCOs as `SourceComments` annotations, so that they survive a parse/serialize round-trip. See issue [#3561](https://github.com/FirelyTeam/firely-net-sdk/issues/3561).

  > **Behavioural note:** the XML serializer now writes any `SourceComments` annotation it encounters. Previously only POCOs produced by the legacy parser carried such annotations, so in practice nothing was written for POCOs coming from the new parser. If your code adds `SourceComments` annotations itself, those comments will now show up in serialized output.

**Performance**
- Parsing allocates considerably less. Most of what a parse used to allocate was transient garbage produced by model validation - which runs on every element in every deserialization mode except `SyntaxOnly` and `Ostrich` - rather than the POCO graph it returns. Validation now allocates about half of what it did, and the validation outcomes are unchanged. Measured on a Patient StructureDefinition (88 KB): 5.1 MB allocated and 3.4 ms per parse, down to 2.5 MB and 1.8 ms. With the validator off, the same parse went from 1.4 MB to 1.0 MB.

**Snapshot generation**
- The `SnapshotGenerator` now reports a specific issue (`PROFILE_ELEMENTDEF_INVALID_ELEMENT_ORDER`, code 10020) when a differential element is out of order, i.e. when it constrains a base element that precedes an element that was already matched. The spec requires `differential.element` and `snapshot.element` to follow the order of the base definition. Such elements could previously not be matched and were silently treated as new elements, which surfaced downstream as a confusing error. See issue [#3600](https://github.com/FirelyTeam/firely-net-sdk/issues/3600).

**Dependencies**
- Updated Fhir.Metrics, Microsoft.SourceLink.GitHub, MSTest.TestFramework and Verify.MSTest to their latest versions. NSubstitute was updated to 6.0.0 (test-only, not part of the shipped packages).
