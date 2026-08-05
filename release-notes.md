## Intro:

This release adds opt-in retention of XML comments in the POCO-based parser. There are no breaking changes.

**Serialization**
- The POCO-based XML parser can now retain the comments found in the source document. Set `DeserializerSettings.RetainComments` to `true` (the default remains `false`) and comments are attached to the parsed POCOs as `SourceComments` annotations, so that they survive a parse/serialize round-trip. See issue [#3561](https://github.com/FirelyTeam/firely-net-sdk/issues/3561).

  > **Behavioural note:** the XML serializer now writes any `SourceComments` annotation it encounters. Previously only POCOs produced by the legacy parser carried such annotations, so in practice nothing was written for POCOs coming from the new parser. If your code adds `SourceComments` annotations itself, those comments will now show up in serialized output.

**Dependencies**
- Updated Fhir.Metrics, Microsoft.SourceLink.GitHub, MSTest.TestFramework and Verify.MSTest to their latest versions. NSubstitute was updated to 6.0.0 (test-only, not part of the shipped packages).
