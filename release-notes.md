## Intro:

This release adds `net10.0` to the frameworks the SDK targets, next to `net8.0` and
`netstandard2.1`. It also lets `FhirClient` send a resource directly as the body of an operation
request, extends the FHIRPath `htmlChecks()` function to string input, and makes the
`SnapshotGenerator` report differential elements that are out of order. There is one binary
breaking change: `ElementNavFhirExtensions.HtmlChecks(PocoNode)` now returns `bool?` instead of
`bool`. Code that calls it needs to be recompiled. See the FHIRPath notes below.

**Platform support**
- The SDK now also targets `net10.0`, next to `net8.0` and `netstandard2.1`. This is additive: .NET 8
  and .NET 9 applications keep resolving to the `net8.0` assets. On .NET 10, class mapping lookups
  by name through `ModelInspector.FindClassMapping(ReadOnlySpan<char>)` no longer allocate.
- `net8.0` remains supported until the next major version (SDK 7), even after Microsoft ends support
  for .NET 8 in November 2026.
- **Upcoming: `netstandard2.1` will be removed** in a later minor release, once Unity 6.8 - which
  replaces Mono with CoreCLR and .NET 10 - has shipped. Its only remaining audience is Unity, and
  Unity 6.8 can consume the `net10.0` assets. If you depend on the `netstandard2.1` assets for
  another platform, please let us know.

**FhirClient**
- Operations with exactly one resource input can now send that resource directly as the POST body,
  instead of wrapping it in a `Parameters` resource. `WholeSystemOperationAsync`,
  `TypeOperationAsync`, `InstanceOperationAsync` and `OperationAsync` have new overloads that take a
  `Resource`, and `TransactionBuilder` has matching overloads for its operation entries. The
  existing `Parameters` overloads are unchanged. See issue [#3599](https://github.com/FirelyTeam/firely-net-sdk/issues/3599).

**FHIRPath**
- `htmlChecks()` can now be invoked on a `string`. Its contents are validated as the content of a
  narrative `div`, following the current FHIRPath build ([FHIR-56303](https://jira.hl7.org/browse/FHIR-56303)).
  Invoked on anything other than `xhtml` or a `string`, it now returns empty instead of `false`. To
  support this, `ElementNavFhirExtensions.HtmlChecks(PocoNode)` returns `bool?` instead of `bool`,
  which is a binary breaking change for code that calls it directly. See issue [#3604](https://github.com/FirelyTeam/firely-net-sdk/issues/3604).

**Snapshot generation**
- The `SnapshotGenerator` now reports a specific issue (`PROFILE_ELEMENTDEF_INVALID_ELEMENT_ORDER`,
  code 10020) when a differential element is out of order, i.e. when it constrains a base element
  that precedes a base element already matched by an earlier differential element. The spec
  requires `differential.element` and `snapshot.element` to follow the order of the base
  definition. Such elements could previously not be matched and were silently treated as new
  elements, which surfaced downstream as a confusing error. The generator does not reorder the
  differential. See issue [#3600](https://github.com/FirelyTeam/firely-net-sdk/issues/3600).

**Dependencies**
- The `System.Reflection.Emit.Lightweight` and `System.Buffers` package dependencies were removed;
  both are part of every framework the SDK targets.
- Updated Microsoft.SourceLink.GitHub to 10.0.401. MSTest (4.4.1) and Verify.MSTest (33.1.5) were
  updated too, but are test-only and not part of the shipped packages.
