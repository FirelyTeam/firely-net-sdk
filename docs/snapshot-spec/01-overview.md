# 1. Overview & terminology

> Status: **complete for Phases 1–3** (spec baseline 2026-08-21; .NET and Java architecture overviews and the
> reading guide added 2026-09-03 after all chapter deep-reads, the harness sweep and the WGM brief). The per-mechanism
> detail lives in chapters 2–12; this chapter only says what the two engines *are* and how the document set fits together.

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

## R6-build note (v6.0.0-ballot4, fetched 2026-08-18/21)

The R6 CI build changes the premise of three chapters, and every RFC in ch15 is verified against it: constraints
on a `contentReference` element apply to the literal path only, with a new `contentReferenceProfile` extension
to opt in to propagation ([FHIR-57266](https://jira.hl7.org/browse/FHIR-57266) /
[FHIR-57265](https://jira.hl7.org/browse/FHIR-57265) — reversing R5 §5.1.0.10, ch8); "contentReference only in
specializations" becomes invariant sdf-30; a datatype profile's root may carry a *binding*
([FHIR-48664](https://jira.hl7.org/browse/FHIR-48664), Applied — the interpretation table changed, and the
ticket asks both generators to "account for this change", ch7); and a derived profile on a complex extension
"is not establishing the 'url' value" (extensibility notes, ch7/RFC-015). Per-chapter "R6-build note" sections
carry the details.

## .NET behavior (Phase 2, deep-reads 2026-08-24 → 26; Hl7.Fhir.R5 6.2.1)

One engine, one pass, differential-driven. `SnapshotGenerator` (`src/Hl7.Fhir.Conformance/Specification/Snapshot/`)
resolves the base (ch3), rebases the base snapshot to the new type for specializations, then **walks the
differential's children and looks each one up in the base** with a forward-only cursor (`ElementMatcher`, ch4).
The consequences of that direction shape everything else: a differential row with no base match is silently a
**new element** for every derivation kind (`createNewElement`; the spec's constraint/specialization split is
not implemented), a misordered row is a new element too (t23a: duplicate id + fabricated `base`, DEV-027/035),
and nothing is ever expanded that the differential does not touch — .NET's materialization policy *is* its
recursion terminator ("base elements + differential-constrained subtrees", ch7/ch11, OQ-021).

Per matched element the order is: merge the element's **type profile** first — any single type profile that
differs from the base's implied profile, for any type, merged as if it were a differential ("type beats base",
`mergeTypeProfiles`, DEV-038, OQ-001/002) — then the differential row on top through `ElementDefnMerger`
(ch5: inherit-if-absent per primitive, overlay per complex property, union for most lists, diff-wins for every
†-frozen property; illegal min/max loosening silently keeps the base value). Children are expanded on demand
from the type's snapshot (`expandElement`, ch7), slicing is driven by the diff's slicing entries and slice
names (`startSlice`/`addSlice`, ch6; the renamed choice form gets a *synthesized* type slice, DEV-026; the
explicit slicing entry is merged as written, never normalized, DEV-020's .NET side), `contentReference`
targets are dereferenced into the *core* structure and the reference replaced by the target's `type` (ch8,
#3177), ids are always regenerated (`ElementIdGenerator`, ch10), and an `Extension.url.fixedUri` is
synthesized wherever missing (`fixExtensionUrl`, DEV-037). Recursion is guarded by a stack keyed on the
**canonical URL alone** (`SnapshotRecursionStack`, ch11 — hence `url == baseDefinition` is a hard failure,
DEV-029). Diagnostics are collected as an `OperationOutcome` (ch12: ~30 issue codes, two duplicated —
#3587); the generator's stated policy is "never throw, correctness belongs to the validator", so 13 of the 21
shared fail tests generate silently (DEV-028, Q5). Six settings (ch12); the two that matter for reproduction
are `ForceRegenerateSnapshots` (default false) and `GenerateSnapshotForExternalProfiles` (default true) — the
SDK's own manifest tests and our harness turn the first on, but every deviation in ch13 was re-checked to be
settings-independent where it mattered (DEV-029, 2026-09-03). Known .NET defects found by the study are filed
as firely-net-sdk #3583, #3587–#3591, #3597.

## Java behavior (Phase 3, deep-reads 2026-08-31 → 09-01; org.hl7.fhir.core @ b06c7ee, validator_cli 6.10.2)

Three cooperating classes in `org.hl7.fhir.r5/.../conformance/profile/`, base-driven. **`SnapshotGenerationPreProcessor`**
runs first over the differential (ch2): it collects "sliceStuff" — rows between a slicing entry and its first
named slice — and pre-merges it into every named slice (strict fill-if-absent for 27 properties, missing rows
injected as full copies), merges an `additionalBase` profile's differential in, and bails out of *all* slice
pre-processing with a warning when it meets a nested slicing it does not recognise (#2589, #2605). This single
mechanism accounts for 77% of the min/mustSupport differences in the shared suite and is where the golden files
encode a bug (DEV-033, #2584). **`ProfileUtilities.generateSnapshot`** then sorts nothing by itself (the JUnit
driver and the IG publisher call `sortDifferential` — a path-not-in-base there is swallowed unless `debug`,
JI-16), checks the differential (`checkDifferential`), and hands the walk to **`ProfilePathProcessor`**, which
**walks the base snapshot and queries the differential per base row** (`getDiffMatches`, ch4). A differential
row no base row pulled in is an *orphan*: ERROR + dropped for constraints, appended for specializations —
the spec's split implemented literally (DEV-035). Five step-in paths decide how an element's children come
into being (ch7): the type profile's root becomes the merge *template* only for `Extension`/`Resource`-typed
elements, and a type profile's children are used only when the base has none at that element — **base beats
type profile**, the inverse of .NET (DEV-038, Q1). Type slicings are rebuilt as `type:$this`/CLOSED and
reopened only if some type lacks a slice; a `min>0` slice raises the entry's min to a literal 1 and collapses
its type list (ch6, DEV-020, Q3). `updateFromDefinition` merges the diff row per property (ch5, DEV-034:
frozen-by-omission set, `isSummary` change = `java.lang.Error`, binding rebuilt with description loss,
restated constraint key dropped, mappings through `MappingAssistant` with a comma-append per identity — and
an identity-collision bug, #2603) after a *profile-doc override* for Extension/Resource/Logical type profiles
only. `contentReference` handling is path-dependent (three behaviors, ch8, DEV-023/025), local references are
absolutized at `setIds` time with a hard-coded core namespace (JI-20), ids are always regenerated and the
**caller's differential is mutated** (ch10, OQ-015), obligation profiles and `inherit-obligations` are folded
in only on the copy-through path — and crash on the diff-touched path (#2602). Recursion is guarded by a stack
of *derived* urls plus a `generatingSnapshot` flag on the SD and a first-element rule for the profile in
progress (ch11); a `java.lang.Error` (30 live sites) bypasses the `catch (Exception)` that would null a
half-built snapshot. Errors are a constructor argument: a message list collects, `null` throws
(`setThrowException`; ch12). **Configuration matters more than in .NET:** the golden files encode the JUnit
driver (`newSlicingProcessing=true`, `autoFixSliceNames=false`, no throw, `sortDifferential` where the manifest
says so); the `validator_cli -snapshot` task runs `newSlicingProcessing=false`, `autoFixSliceNames=true`,
throw-on-first-error — and never installs the xver manager (#2604). Four extension-inheritance policy lists
and a per-extension `snapshot-behavior` declaration exist (ch12, OQ-019/Q9) but the pack carries no metadata
yet. Java defects found by the study: hapifhir/org.hl7.fhir.core #2584–#2597, #2602–#2605.

## State of the study and how to read the document set (2026-09-03)

- **Where the two engines agree** is recorded in the chapters as one rule with two citations; **where they
  differ**, the [deviation register](13-deviation-register.md) holds one entry per mechanism (DEV-001…DEV-038;
  several settled as agreement after the sweep). **What the spec does not say** is one question each in
  [open questions](14-open-questions.md) (OQ-001…OQ-021, each with a JIRA/Zulip verdict as of 2026-09-02),
  and every question that has settled into a concrete text change is an RFC in [spec RFCs](15-spec-rfcs.md)
  (RFC-001…RFC-019).
- **Empirical footing:** the Phase-4 harness (project materials, `harness/`) runs both engines and the golden
  files over all 164 shared tests. The Java oracle reproduces the golden files on 143/143 generation tests and
  satisfies 21/21 fail tests, so every .NET-vs-golden difference is real signal; .NET is `equalsDeep`-equal to
  the golden file on 0/143 (141 differ, 2 threw — pervasive noise classes such as the datatype-root comment
  enrichment guarantee inequality even where semantics agree, so the *classified* diff report, not the raw
  count, is the deviation signal) and satisfies 8/21 fail tests. Extract files in the materials directory
  hold the classified diffs.
- **Prior art checked:** HL7 twice agreed to write these rules down and twice gave up
  ([FHIR-9079](https://jira.hl7.org/browse/FHIR-9079) 2016, [FHIR-13402](https://jira.hl7.org/browse/FHIR-13402)
  2018); the one live vehicle is [FHIR-31405](https://jira.hl7.org/browse/FHIR-31405). Chris Grenz's
  FHIR-Primer wiki (2017) and Gino Canessa's 2026 comparison are the only other write-ups found.
- **Decision venue:** the [WGM brief](wgm-brief-2026-09.md) (frozen 2026-09-03) carries the ten live
  decisions and the confirmations for the September 2026 WGM evening session; outcomes flow back into ch13–15.
- **Reading order for an implementer:** ch3 (what "base" is and rebasing) → ch4 (which rows meet which
  elements) → ch5 (what happens to each property) → ch6/ch7 (slicing, type and extension expansion — the two
  chapters where the engines diverge most) → ch8–ch11 → ch12; then the register for every place a choice must
  be made. **For the standards side:** ch15 first, then ch14, then the brief.
