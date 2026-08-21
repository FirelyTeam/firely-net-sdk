# 1. Overview & terminology

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

## Scope
What snapshot generation is: inputs (a `StructureDefinition` with a differential, a resolvable base
definition, and resolvable type/extension profiles), output (the fully expanded `snapshot` element list),
and the derivation kinds (`constraint` vs `specialization`). Terminology used throughout the document.
Non-goals: validation of instances, differential *generation* (the reverse operation).

Out of scope for the whole document: the SDK's legacy STU3 fork (`src/Hl7.Fhir.STU3/Specification/Snapshot/`,
a drifted copy of the R4+ engine; noted here, not analyzed).

## Spec baseline (R5)

### The two artifacts

> "A snapshot view is expressed in a standalone form that can be used and interpreted without considering
> the base StructureDefinition" — "A differential view is expressed relative to the base
> StructureDefinition - a statement of differences that it applies" [structuredefinition §5.4.5]

> "Differential statements describe only the differences that they make relative to the structure
> definition they constrain" [profiling §5.1.0.11]

Both components are `0..1` with `element : ElementDefinition [1..*]`; at least one must be present
(sdf-6). A structure is "a flat list of elements. The element.path provides the overall structure"
[structuredefinition §5.4.6]; "children are never implied, and the path statements are always in order"
[profiling §5.1.0.9].

### The generator's entire mandate

The spec's complete statement of what snapshot generation *is*:

> "In order to properly understand a differential structure, it must be applied to the structure
> definition on which it is based. In order to save tools from needing to support this operation (which is
> computationally intensive - and impossible if the base structure is not available), a StructureDefinition
> can also carry a 'snapshot' - a fully calculated form of the structure that is not dependent on any other
> structure. The FHIR project provides tools for the common platforms that can populate a snapshot from a
> differential (note that the tools generate complete verbose snapshots; they do not support suppressing
> mappings or constraints)." [profiling §5.1.0.11]

> "Differentials in constraints need only specify elements that they are making rules about. Other
> elements can be inferred as defined in the base resource" [structuredefinition §5.4.6]

Operational systems "should always have the snapshot view populated"; the differential "serves the
authoring process, while the snapshot serves the implementation tooling" [profiling §5.1.0.11]. **No
algorithm is specified anywhere** — HL7 formally acknowledged this in
[FHIR-13402](https://jira.hl7.org/browse/FHIR-13402) ("Clarify snapshot generation rules", 2017–2023,
closed *Not Persuasive*: any future write-up "will be done in Confluence, not as a formal part of the
spec").

### Derivation kinds

Controlling metadata: `kind` (1..1: `primitive-type | complex-type | resource | logical`), `abstract`
(1..1), `type` (1..1 uri), `baseDefinition` (0..1 canonical), `derivation` (0..1:
`specialization | constraint`). Invariants: non-abstract requires baseDefinition (sdf-4); baseDefinition
requires derivation (sdf-27).

> "The type this structure describes. If the derivation kind is 'specialization' then this is the master
> definition for a type … Otherwise the structure definition is a constraint on the stated type (and in
> this case, the type cannot be an abstract type). References are URLs that are relative to
> http://hl7.org/fhir/StructureDefinition … Absolute URLs are only allowed in logical models, where they
> are required" [structuredefinition §5.4.5, `type`]

- **specialization** — a new type; base elements' paths are rooted at the base type, the new snapshot's at
  the new type (path *rebasing* is implied by sdf-11 but never described — see ch3). Only the FHIR spec
  itself defines base types/resources; implementers define constraints, extensions, logical models
  [structuredefinition §5.4.6.2].
- **constraint** (a *profile*) — "A set of constraints on a resource represented as a structure definition
  with derivation = constraint" [profiling §5.1.0.1]. `type` equals the base's type and cannot be abstract.

§5.4.6.2 gives 9 worked url/kind/type/abstract/baseDefinition/derivation patterns (base datatype,
constrained datatype, base resource, profile, base Extension, defined extension, profile-of-extension,
abstract resource, interface + logical model). **Caution:** the printed examples contain published typos
(`us/core` in the Resource/CanonicalResource/Definition baseDefinition urls; a doubled `"abstract": false`)
— see [RFC-005](15-spec-rfcs.md#rfc-005--structuredefinition-5462-worked-examples-contain-wrong-canonical-urls).

### General limits on profiles [profiling §5.1.0.7]

- "Profiles cannot break the rules established in the base specification"
- "Profiles cannot specify default values or meanings for elements" (logical models may — see ch9)
- "Profiles cannot change the name of elements defined in the base specification, or add new elements"
- "It must be safe to process a resource without knowing the profile"

### Terminology used in this document

- **base (definition)** — the SD referenced by `baseDefinition`, whose *snapshot* is the merge input.
- **slicing entry** — the first repeat of a sliced element, carrying `slicing`; **slice group** — entry +
  named slices; **reslice** — a slice of a slice (`name/subname`).
- **type slice** — a slice of a choice (`[x]`) element constraining one concrete type.
- **rebasing** — rewriting a snapshot fragment's paths to a new root (type-profile/extension expansion,
  specialization, logical models).
- **expansion** — materializing an element's children from its type's (or type profile's) snapshot.

## R4/R4B deltas

- Definitions, sparse-differential rules, and ordering rules are **verbatim identical** R4→R5.
- R5 added the *interface* and *logical-model-on-Base* derivation patterns to §5.4.6.2 (see ch9).
- R4B could not be diffed locally (no R4B pages in the corpus); treat R4B attribution of any R5 delta as
  unverified.

## .NET behavior (Phase 2)
*(pending)*

## Java behavior (Phase 3)
*(pending)*
