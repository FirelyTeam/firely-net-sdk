# Plan: support for the `json-empty-behavior` tooling extension

## Background

The tools IG defines the extension
`http://hl7.org/fhir/tools/StructureDefinition/json-empty-behavior`, which can be placed on
`ElementDefinition` and `ElementDefinition.type`. It carries a `code` from
`http://hl7.org/fhir/tools/CodeSystem/json-empty-behavior`:

| code    | meaning                                                                                  |
|---------|------------------------------------------------------------------------------------------|
| absent  | When there are no items, the JSON property for the array must be missing                  |
| present | When there are no items, the JSON property for the array must be present and empty (`[]`) |
| either  | When there are no items, the JSON property may be present and empty, or absent            |

This lets profiles/logical models (e.g. models serialized with the FHIR JSON rules but with
non-FHIR wire-format requirements) override the standard FHIR JSON rule that "an array must not
be empty; if it is empty the property is omitted".

## Current SDK behavior (SDK 6.x, `develop`)

Serializers never emit empty arrays:

* `BaseFhirJsonSerializer` (POCO → JSON) iterates `Base.EnumerateElements()`, and the generated
  `EnumerateElements()` overrides skip empty lists (`if (_Telecom?.Any() is true) ...`), so an
  empty collection is invisible to the serializer. For primitive collections,
  `serializeFhirPrimitiveList` additionally returns immediately when `values.Count == 0`.
* `FhirJsonBuilder` (ITypedElement → JSON) skips name groups without children
  (`if (!children.Any()) continue;` in `addChildren`). Note that `ITypedElement`/`ISourceNode`
  cannot even *represent* "an empty collection is present".

Parsers reject or drop empty arrays:

* `BaseFhirJsonDeserializer.deserializeListInto` raises `FhirJsonException.ARRAYS_CANNOT_BE_EMPTY`
  (code `JSON121`, severity Error) when it encounters `[]`.
* `FhirJsonNode` (JSON → `ISourceNode`) silently produces zero children for `[]`, so the fact
  that the property was present is lost on round-trip.

There is no place in the serialization metadata (`IElementDefinitionSummary`, `PropertyMapping`,
`ClassMapping`) that carries this kind of per-element JSON serialization hint.

## Proposed design

### 1. Model the concept (Hl7.Fhir.Base)

* New public enum in `Hl7.Fhir.Specification`:

  ```csharp
  public enum JsonEmptyArrayBehavior
  {
      Absent,   // default; today's behavior
      Present,
      Either
  }
  ```

* Constants for the extension/CodeSystem URLs (e.g. in a `ToolingExtensions` constants class),
  plus a helper to read the extension:

  ```csharp
  public static JsonEmptyArrayBehavior? GetJsonEmptyBehavior(this IExtendable elementDefinitionOrType);
  ```

  The lookup should check the `ElementDefinition` itself first, and fall back to its
  `ElementDefinition.type` components (both are valid contexts for the extension).

### 2. Metadata plumbing

* Add `JsonEmptyArrayBehavior EmptyArrayBehavior { get; }` to `IElementDefinitionSummary`,
  next to the existing `DefaultTypeName` (which follows the same "surfaced from an
  ElementDefinition extension" pattern). Implementations to update:
  * `ElementDefinitionSummary` (POCO copy of the interface) — new property + copy constructor.
  * `PropertyMapping` (`Hl7.Fhir.Base/Introspection`) — settable, defaults to `Absent`; can be
    set via object initializer for dynamically built mappings (custom resources built from
    StructureDefinitions).
  * `ElementDefinitionSerializationInfo` in both `StructureDefinitionSummaryProvider`s
    (`Hl7.Fhir.Conformance` and `Hl7.Fhir.STU3`) — read the extension from the underlying
    `ElementDefinition`, exactly like `DefaultTypeName` reads `elementdefinition-defaulttype`.
  * `PrimitiveElement` — returns `Absent`.

  Note: adding a member to a public interface is source-breaking for external implementers, so
  this must land in a major release; alternatively (for a minor release) introduce a small
  optional interface (e.g. `IJsonSerializationHints`) that serializers test for. Recommendation:
  add to `IElementDefinitionSummary` in the next major, as was done for `IsModifier`.

* Add an optional named argument to `FhirElementAttribute`
  (e.g. `public JsonEmptyArrayBehavior EmptyArrayBehavior { get; set; }`) and propagate it into
  `PropertyMapping` during reflection, so hand-written or code-generated POCOs for custom
  models can declare the behavior.

### 3. Serializer changes

* `BaseFhirJsonSerializer` (POCO):
  * Because `EnumerateElements()` skips empty lists, handle `Present` as a post-step per object:
    after enumerating the members, consult the `ClassMapping` for properties whose
    `EmptyArrayBehavior == Present` that were not written, and emit `"name": []` for them.
    (Alternative: change the code generator's `EnumerateElements()` to also yield empty lists
    for such elements — more invasive, requires poco-generation changes; not preferred.)
  * `serializeFhirPrimitiveList`: when `values.Count == 0` and behavior is `Present`, write the
    empty array instead of returning.
  * `Either` requires no serializer change (keep omitting).
* `FhirJsonBuilder` (ITypedElement):
  * In `addChildren`, after processing the child name-groups, walk the element definitions of
    the node's type (`node.Definition` / `IStructureDefinitionSummary.GetElements()`) and emit
    `[]` for every collection element with behavior `Present` that produced no children.
  * This only works when type information is available (typed serialization); untyped
    round-trip serialization cannot know about the extension — document this limitation.

### 4. Parser/deserializer changes

* `BaseFhirJsonDeserializer.deserializeListInto`: when an empty array is encountered, look up
  `EmptyArrayBehavior` on the `PropertyMapping`; suppress `JSON121`
  (`ARRAYS_CANNOT_BE_EMPTY`) when the behavior is `Present` or `Either`. The resulting POCO
  gets an instantiated empty list, which — combined with the serializer change — makes
  `"prop": []` round-trip correctly for `Present` elements.
* Optional (stretch): when the deserializer finishes an object and a property with behavior
  `Present` was never seen, raise a new (warning-level) coded error. This is a wire-format
  constraint that cannot be validated post-parse (empty list and missing property are
  indistinguishable in the POCO), so the parser is the only place it can be detected.
* `FhirJsonNode`: already tolerates `[]` (yields no children). No change needed for `Either`;
  for `Present` round-tripping through `ISourceNode`, the loss of "property was present" is a
  known limitation (see above).
* Users on older behavior are unaffected: for elements without the extension everything works
  exactly as today.

### 5. Out of scope / follow-ups

* **Validation**: enforcement of `absent`/`present` against raw JSON belongs to the parsers
  (above). The Firely validator (separate repo, firely-validator-api) operates on parsed
  instances and cannot see the difference; no change there.
* **Code generation**: if core specs or tooling IGs used by the SDK's POCO generator start
  carrying this extension, the generator must emit `EmptyArrayBehavior = ...` on
  `[FhirElement]`. Follow-up in the codegen pipeline.
* XML serialization is unaffected (the extension is JSON-specific).

### 6. Tests

* Unit tests in `Hl7.Fhir.Support.Poco.Tests`:
  * Deserializing `[]` with behavior `Present`/`Either` produces no `JSON121`; with
    `Absent`/no extension it still errors.
  * Serializing a POCO with an instantiated empty list and behavior `Present` emits `[]`;
    with `Either`/`Absent` it omits the property.
  * Round-trip: `{"prop": []}` → POCO → `{"prop": []}` for `Present`.
* `FhirJsonBuilder` tests with a custom StructureDefinition carrying the extension, driven
  through `StructureDefinitionSummaryProvider`.

### Suggested implementation order

1. Enum + extension-URL constants + `GetJsonEmptyBehavior()` helper (+ tests).
2. Metadata: `IElementDefinitionSummary` & implementations, `FhirElementAttribute`,
   `PropertyMapping` (+ tests).
3. Deserializer: suppress `JSON121` based on metadata (+ tests).
4. Serializers: POCO serializer, then `FhirJsonBuilder` (+ round-trip tests).
5. Docs on https://docs.fire.ly (serialization pages) describing the supported behavior and
   the untyped/ISourceNode limitation.
