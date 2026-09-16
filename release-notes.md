## Intro:

This release is mostly about the cost of parsing and serializing: allocations on the hot path of
(de)serialization are down substantially, and the serializers no longer emit empty objects or
elements. It also collects a series of `SnapshotGenerator` fixes and a concurrency fix in
terminology validation. There are no breaking API changes, but two `SnapshotGenerator`
`OperationOutcome` issue codes were renumbered to remove duplicates (10012 -> 10018,
10014 -> 10019), which affects code that switches on those numbers. See the behavioural
notes below.

**Serialization**
- The serializers no longer write empty objects (`{}`) or empty elements (`<x/>`). A structure is
  opened only once it is known to have content, so an empty POCO - or one emptied out by a
  `SerializationFilter`, as happens when summarizing - no longer produces output our own
  deserializer rejects with `OBJECTS_CANNOT_BE_EMPTY` / `ELEMENT_CANNOT_BE_EMPTY`. Summarized
  resources now round-trip. See issue [#3557](https://github.com/FirelyTeam/firely-net-sdk/issues/3557).
- Both deserializers now expose a `NOOVERFLOW` preset, next to `STRICT`. `DeserializationMode.NoOverflow`
  was the one mode without a preset, reachable only by constructing the settings by hand. Thanks to
  @sujeito-operator for the contribution. See issue [#3532](https://github.com/FirelyTeam/firely-net-sdk/issues/3532).
- `new FhirUri((Uri)null)` no longer throws a `NullReferenceException`; `FhirUrl(Uri)` and
  `Canonical(Uri)` had the same problem. They now null-propagate, like the other primitive
  datatype constructors. See issue [#3578](https://github.com/FirelyTeam/firely-net-sdk/issues/3578).

**Performance**
- Parsing allocates considerably less. Most of what a parse used to allocate was transient garbage
  produced by model validation - which runs on every element in every deserialization mode except
  `SyntaxOnly` and `Ostrich` - rather than the POCO graph it returns. Validation now allocates about
  half of what it did, and the validation outcomes are unchanged. Measured on a Patient
  StructureDefinition (88 KB): 5.1 MB allocated and 3.4 ms per parse, down to 2.5 MB and 1.8 ms.
  With the validator off, the same parse went from 1.4 MB to 1.0 MB.
- The `ClassMapping` hot path no longer allocates per access. Two patterns - a `LazyInitializer`
  factory allocated on every call, including the warm path, and a full locked snapshot of the
  property mapping collection - sat on the per-element path of every (de)serialization and are now
  gone.
- Serialization caches encoded JSON property names, and class mapping lookups are allocation-free
  on .NET 10 and later (`ClassMappingCollection.FindByName(ReadOnlySpan<char>)`), with a fallback
  on earlier frameworks.

**Snapshot generation**
- Fixed a `NotSupportedException` ("Can only jump to local nameReferences") when a differential
  element carried a complex type profile reference of the form `canonicalUrl#elementName` *and*
  had child constraints. See issue [#3583](https://github.com/FirelyTeam/firely-net-sdk/issues/3583).
- Logical models deriving directly from `Base` - which is only a resolvable `StructureDefinition`
  from R5 onwards - are now handled gracefully. Before R5, `Base` is treated as an empty root and a
  warning is emitted instead of the generator failing. Element matching and child handling for
  anonymous and untyped logical model elements were improved along with it (VONK-10132).
- Two `OperationOutcome` issue codes were each used for two unrelated conditions. The duplicates
  have been resolved: `PROFILE_ELEMENTDEF_SLICENAME_NOMATCH` moves from 10012 to **10018**, and
  `PROFILE_STRUCTURE_TYPE_MISSING` moves from 10014 to **10019**. Code that switches on these
  numeric codes needs updating. See issue [#3587](https://github.com/FirelyTeam/firely-net-sdk/issues/3587).

**Terminology**
- `LocalTerminologyService` (multi-coding `CodeableConcept` validation) and `ValueSetExpander`
  (grouping value sets) fanned out with `Task.WhenAll`, so a single logical operation could call
  back into the caller-supplied `IAsyncResourceResolver` from several threads at once. A resolver
  that is not safe for concurrent use - a scoped EF Core-backed resolver, for instance - failed
  with "A second operation was started on this context instance". Both sites now resolve
  sequentially. See issue [#3582](https://github.com/FirelyTeam/firely-net-sdk/issues/3582).

**Other**
- Dynamic type inference in `NewPocoBuilder` now uses the FHIR definition, where available, to
  decide whether an element is primitive or complex, rather than relying on type-name heuristics.
  This fixes misclassification of complex types in logical models identified by a canonical url.
- `MatchesPrefix` no longer compares a `ReadOnlySpan<char>` against `null` (CA2265). This is not a
  behaviour change - the comparison was correct - but it blocked building against `net10.0` under
  `TreatWarningsAsErrors`. See issue [#3580](https://github.com/FirelyTeam/firely-net-sdk/issues/3580).

**Snapshot generation**
- The `SnapshotGenerator` now reports a specific issue (`PROFILE_ELEMENTDEF_INVALID_ELEMENT_ORDER`, code 10020) when a differential element is out of order, i.e. when it constrains a base element that precedes a base element already matched by an earlier differential element. The spec requires `differential.element` and `snapshot.element` to follow the order of the base definition. Such elements could previously not be matched and were silently treated as new elements, which surfaced downstream as a confusing error. See issue [#3600](https://github.com/FirelyTeam/firely-net-sdk/issues/3600).

**Dependencies**
- Updated Microsoft.SourceLink.GitHub to 10.0.400. NSubstitute (6.2.0) and Verify.MSTest (32.0.0)
  were updated too, but are test-only and not part of the shipped packages.
