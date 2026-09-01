# 2. Differential preprocessing

> Status: **spec baseline + .NET + Java behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2
> packet 3, 2026-08-24: `DifferentialTreeConstructor.cs` deep-read; Phase 3 packet J-c, 2026-09-01:
> `checkDifferential`/`cloneDiff`/`sortDifferential`/`closeDifferential`/`cleanUpDifferential` deep-read;
> slice-stuff propagation and additional-base merging were read in J-a/J-b).

## Scope
What must happen to a differential before merging: validity requirements on differentials (paths, order,
sparseness), reconstructing a full element *tree* from a sparse differential whose paths skip parents,
and each implementation's pre-processing pass.

## Spec baseline (R5)

### Sparseness — what a differential may omit

> "Nothing else is stated - all the rest of the structural information is implied (note that this means
> that a differential profile can be sparse and only mention the elements that are changed, without having
> to list the full structure. This rule includes the root element - it is not needed in a sparse
> differential)." [profiling §5.1.0.11]

sdf-8a confirms this at invariant level: the differential's **first element need only *start with* the
type** — a sparse differential may begin at a child path (e.g. `Patient.identifier`), and all subsequent
elements must be under the first element's root segment. Consequence: a generator must synthesize implied
ancestors (including the root) from the base. Note the interaction with the path rule "a.b.c.d cannot be
defined unless a.b.c is explicitly defined" [elementdefinition #path] — that governs *original
definitions*; sparse differentials are the sanctioned exception for *constraints*.

### Ordering — the only normative statement in the corpus

> "Elements specified in the differential (and all elements in the snapshot) must be ordered as such:
> - Elements from the baseDefinition appear before new elements in a StructureDefinition with derivation
>   'specialization'
> - Elements must be in the same order as the baseDefinition, and child elements appear in depth-first
>   order.
> - Unsliced descendants of sliced elements appear before slices" [structuredefinition §5.4.6]

Consequences: (a) a differential is *required* to already be in base order — no error behavior is
prescribed for violations (implementation choice: reject vs normalize, see ch12); (b) merging can be a
single ordered walk; (c) within a sliced element, the slicing entry's own children precede the slices.
Where new elements go in a *constraint* derivation is unaddressed (constraints cannot add elements at all
per profiling §5.1.0.7).

### Differential well-formedness invariants

| Key | Rule |
|---|---|
| sdf-8a | all elements start with the SD's `type` (first element may be a deeper path) |
| sdf-15a | a differential root element (path without `.`) has no `type` component (non-logical) |
| sdf-20 | no `slicing` on the root element |
| sdf-23 | no `sliceName` on the root element |
| sdf-9 | no `label`/`code`/`requirements` on the root element |
| sdf-14/17 | every differential element has an id; ids are unique |
| sdf-21 | no `defaultValue` in a constraint differential |

### Choice-type paths in differentials

> "when substituting [x] with a specific data type, always capitalize the first letter. Choice types are
> always camel-case. Ex: 'effectiveDateTime' is correct, 'effectivedateTime' is NOT correct."
> [structuredefinition §5.4.6]

In **R5** the sanctioned differential form for a type-specific constraint is the `[x]` path plus a type
slice (`Patient.deceased[x]` with sliceName `deceasedBoolean`) — see the R4 delta below and ch4.

### Spec gaps (preprocessing)

1. No procedure for synthesizing implied parents (the tree-reconstruction step both implementations have).
2. No error behavior for out-of-order differentials.
3. Whether preprocessing should normalize R4-era renamed choice paths into R5 type-slice form is a pure
   tooling decision.

## R4/R4B deltas

- Ordering rules and sparse-differential rules: **verbatim identical** R4→R5.
- **Headline:** R4 *prescribed renamed paths* for single-type choice constraints ("the name of the element
  is changed to include the type instead of '[x]'" [R4 sd §5.3.5]); R5 **deleted** that rule — the `[x]`
  path + type slice is the R5 form. A dual-version generator must accept renamed paths in R4 differentials
  and match them to the base `[x]` element (see ch4, [RFC-013](15-spec-rfcs.md#rfc-013--sanction-or-prohibit-renamed-choice-type-paths-in-snapshots)).

## .NET behavior (Phase 2, deep-read 2026-08-24)

### Tree reconstruction (`DifferentialTreeConstructor.MakeTree`, `DifferentialTreeConstructor.cs:43`)

A single forward pass over the differential's element list, indexed, comparing each element's path with its
predecessor's. Four cases: root path → must be at index 0, else **throws** ("Differential has multiple
roots", `:69-78`); sibling of the previous element or direct child of it → nothing to do; a path whose
parent is an ancestor-or-self of the previous element → nothing to do (going back *up* the hierarchy;
assumed to be a slice — an unnamed one only rates a debug-build warning, `:113`); anything else → a missing
parent, so a **stand-in parent** is inserted (`new ElementDefinition { Path = parentPath }` — no id, no
slicing, no constraints; the class doc promises stand-ins "should not have any influence on the final
snapshot form", `:20-26`) and the pass re-examines the same index until the chain to an already-seen
ancestor is complete. An element with a missing/empty path **throws** (`:62-65`). Both throws are more rows
for [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors)'s error-taxonomy
question.

Two properties worth pinning:

- **Ordering is assumed, never verified.** The algorithm relies on the spec's document-order rule
  [structuredefinition §5.4.6]; an out-of-order differential is neither detected nor normalized (duplicate
  sibling constraints get a debug-build-only warning, `:84`). Misordered input flows into matching, where it
  silently degrades (ch4).
- **The result list is new, but the *elements* are shared** with the caller's differential — deliberately
  not cloned since 2019 to avoid copying internal annotations (`:48-51`). Consequently later generator
  mutations of differential elements (generated type-slice names, ch4/[OQ-015](14-open-questions.md#oq-015--the-generator-mutates-its-input-differential);
  the root sliceName fix below) reach the caller's StructureDefinition — except where the mutated element is
  a generator-owned stand-in.

### Other preprocessing steps (in `SnapshotGenerator.generate`)

- **Missing differential** is synthesized as an empty one before anything else (`SnapshotGenerator.cs:362-369`),
  so a differential-less SD generates snapshot = rebased base snapshot + generator fill obligations. (But
  see [OQ-016](14-open-questions.md#oq-016--what-does-a-differential-less-structuredefinition-mean) — root
  resolution for *type profiles* refuses the same input.)
- **Root sliceName repair** (`FIX_SLICENAMES_ON_ROOT_ELEMENTS`, active): a differential root element
  carrying a `sliceName` (illegal, sdf-23; the core R4 `SimpleQuantity` fixture famously did this) has it
  cleared, with an issue emitted unless the SD *is* core SimpleQuantity (`SnapshotGenerator.cs:556-559,
  604-618`). When the root came from the caller's differential this mutates the caller's SD; same theme as
  the runtime fixture patches (DEV-010).
- **No normalization of R4-era renamed choice paths** happens in preprocessing — renamed paths are handled
  during matching (ch4) and normalized to type-slice form in the snapshot (`NORMALIZE_RENAMED_TYPESLICE`,
  ch5/ch6).

## Java behavior (Phase 3; J-c deep-read 2026-09-01, slice propagation J-a 2026-08-31)

Code: `ProfileUtilities.java` (PU) `generateSnapshot` preamble `:770-839`, `checkDifferential` `:1413-1461`,
`checkDifferentialBaseType` `:1317-1330`, `cloneDiff` `:1483-1491`, `sortDifferential` `:3815-3869` (+ holder/
comparer `:3672-3812`, `:3888-4039`), `closeDifferential` `:3424-3508`, `cleanUpDifferential` `:4570-4645`;
`ProfilePathProcessor.java` (PPP) `:1061-1130`. Commit `b06c7ee`. Verbatim detail in the materials extract
`java-ch04-matching-and-ch02-preprocessing-2026-09-01.md`.

### Order of operations in `generateSnapshot` (PU:770-839)

1. circular-reference check on the snapshot stack → throw (PU:774-776; ch11);
2. **path validation** of the caller's differential rows (`checkDifferential`, PU:791);
3. **root-type check** (`checkDifferentialBaseType`, PU:792);
4. inherited SD-level extensions + obligation profiles collected (PU:808-810);
5. bookkeeping user data cleared on the original rows (PU:820-821);
6. **the differential is cloned** (`cloneDiff`, PU:824) — "we're sometimes going to hack the differential
   while processing it";
7. the **preprocessor** runs on the clone (`SnapshotGenerationPreProcessor.process`, PU:825);
8. for specializations the base snapshot is cloned with ids re-rooted to the derived type (PU:828-832);
9. the walk (`ProfilePathProcessor.processPaths`, PU:839; ch4).

### Path validation (`checkDifferential`, PU:1413-1461) — no .NET counterpart

Every row, all failures `FHIRException` throws: missing `path` element or value; path must equal the SD
`type` or start with `type + "."` (the sdf-8a check — this is what rejects t37's `MedicationRequiest…` typo
when the driver does not sort first; note the "may equal the type" allowance is applied to every row because
the `first` flag is never cleared, PU:1414); per segment: empty (obs-unit's `..`), >64 characters, Unicode
whitespace, any of `, : ; ' " / | ? ! @ # $ % ^ & * ( ) { }`, any character outside `' '..'z'`, and `[`/`]`
anywhere except as a trailing `[x]`. .NET validates none of this (DEV-027 obs-unit; DEV-028 group (f)).

### Root type (`checkDifferentialBaseType`, PU:1317-1325) — sdf-15a

A root row with `type` → `throw new Error(TYPE_ON_FIRST_DIFFERENTIAL_ELEMENT)` unless the SD is LOGICAL.
An opt-in repair (`wantFixDifferentialFirstElementType`, **default false**, PU:444/497) instead **clears the
type on the caller's original row** when it equals the base's type (PU:1319-1320) — the one write to the
caller's differential that happens *before* cloning. .NET: never checked (DEV-028 f).

### Caller isolation (`cloneDiff`, PU:1483-1491) — OQ-015 Java side

Each row is `copy()`d into a fresh differential; each clone points back via `SNAPSHOT_diff_source`. All
in-generation mutation — preprocessor injections and merges, generated slice names, cursor bookkeeping —
lands on clones. Written back to the originals: only `SNAPSHOT_DERIVATION_EQUALS`/`_POINTER` user data
(PU:913-920), the bookkeeping clear (PU:820-821), and the opt-in root-type clear above. Two caveats:
`Base.setCopyUserData(true)` is switched on for the whole run so clones carry user data (PU:779-780), and
the *test driver* runs `setIds(source, false)` on the original before generation (driver `:548`, `:600`) —
Java differentials may acquire ids outside `generateSnapshot` (ch10). Contrast .NET's deliberate sharing of
element instances with the caller (`DifferentialTreeConstructor.cs:48-51`).

### No tree reconstruction — sparse parents are handled inside the walk

Java never builds a differential tree and never inserts stand-in rows (the preprocessor injects rows only
for slice propagation, below). A base row the diff does not mention but whose *children* it does is handled
by the empty-match branch of the walk (PPP:1061-1130): the base row is copied, and `hasInnerDiffMatches`
(PU:2420-2442, ch4) decides whether to recurse — into the base's own children when it has them
(PPP:1080-1092), otherwise by "implicitly stepping into" the row's type (PPP:1093-1130; ch7) — throwing when
that is impossible: no type and no contentReference (`_HAS_NO_CHILDREN__AND_NO_TYPES_IN_PROFILE_`,
PPP:1094-1096), or several non-Reference types with non-extension child rows
(`_HAS_CHILDREN__AND_MULTIPLE_TYPES__IN_PROFILE_`, PPP:1098-1117). Outcome-equivalent to .NET's stand-in
parent (an empty stand-in merged over the base copy is a no-op merge) and the same "expand only where the
diff constrains children" policy (ch7/ch11); the difference is that .NET turns an unexpandable sparse chain
into stand-ins plus a `New` leaf (DEV-027 obs-unit, DEV-035), where Java throws.

### Slice-content propagation (the preprocessor's headline job — J-a deep-read, ch6)

Before the walk, the preprocessor collects the differential rows between a slicing entry and its first
named slice ("sliceStuff") and merges them into **each named slice's differential** with strict
fill-if-absent semantics, injecting missing rows (`SNAPSHOT_PREPROCESS_INJECTED`, exempt from orphan
reporting PU:913-914). .NET has no counterpart. Detail and the confirmed contamination defect:
[DEV-025](13-deviation-register.md#dev-025--materialization-depth-of-unconstrained-content-java-normalizes-more-than-net-ch7ch8ch11),
[DEV-033](13-deviation-register.md#dev-033--java-preprocessor-cross-slice-contamination--silent-constraint-loss-ch6)
(the `elementsMatch` leaf-only matching suspected in Phase 4 is **confirmed** — golden files bless it; filed
upstream as hapifhir#2584), [OQ-021](14-open-questions.md#oq-021--how-much-must-a-snapshot-materialize).
The preprocessor also merges **additionalBase** differentials
([DEV-032](13-deviation-register.md#dev-032--java-only-merge-inputs-additionalbase-and-obligation-profiles-ch3), ch5 table).

### Ordering: no in-generation normalization or diagnostic; `sortDifferential` is tooling

Inside `generateSnapshot` Java neither sorts nor warns about order (the in-walk warning is commented out,
PU:2465-2473: "Might be better done when we're sorting the profile?"). An out-of-order row is detected only
if it ends up **unmatched** (cursor-jump mechanism, ch4) — then it is dropped with an ERROR (PU:925, "…check
that the path and definitions are legal in the differential (including order)"); a misordered row that still
finds its base row later is accepted silently.

`sortDifferential(base, diff, name, errors, errorIfChanges)` (PU:3815-3869) is a **public utility, never
called by `generateSnapshot`** — but the shared test suite's driver applies it before generation to every
`sort="true"` test (28 of 166 R5 manifest tests) and to **every base in a derivation chain** (driver
`:610`, `:699`), and the harness oracle replicates that. So the golden files describe *sorted-differential*
behavior. Mechanics: rows are placed in a holder tree with **placeholders** for a missing root and for
skipped intermediate levels (PU:3831-3840, :3892-3899); siblings are sorted by the index of their
counterpart in the base snapshot (`ElementDefinitionComparer.find`, PU:3755-3797 — `[x]`-tolerant both
ways, follows `contentReference`), child scopes by the snapshot of the child's type via `getComparer`
(PU:3930-4002: profile-typed `Resource`, extension SDs, renamed `[x]` children, Reference-only unions;
mixed polymorphic children with content → throw); `Collections.sort` is stable, so a slicing entry and its
slices keep their authored relative order; placeholders are dropped on write-back (PU:4033-4039). Quirks:
a path not found in the base gets index 0 (sorts to the *front*) and the recorded error surfaces **only in
debug mode** (PU:3916-3918); a row outside the root prefix ends the top-level loop, so it and everything
after it are dropped → "Sort failed: counts differ; at least one of the paths in the differential is
illegal" (PU:3867-3868) — which is how t37 actually fails in Java (the sort step, not `checkDifferential`).

Evidence: **t23 and t23a are the same differential** (`males.gender` listed before `males.telecom`); t23 is
`sort="true"` and passes, t23a is not and yields the orphan ERROR
([DEV-027](13-deviation-register.md#dev-027--malformed-differentials-produce-silently-corrupt-net-snapshots-ch2)).
.NET has no sorting at all: the vendored driver deserializes the `sort` attribute but never reads it
(`SnapshotGeneratorManifestTests.cs:916-917`), and `Fix_t23` (`:236-252`) **hand-swaps** the two rows in the
checked-in fixture instead (DEV-010); t26 (sort-only, already in order) is correctly `[Ignore]`d as
"input==expected" (DEV-012 settled).

### Two more public utilities, outside generation (no callers in the checked-out cone)

- **`closeDifferential(base, derived)`** (PU:3424-3438): for every base row that is an immediate child of
  the root (except `.id`) and is not mentioned in the differential, appends `{path, max="0"}`; recurses only
  into *sliced* base rows the differential mentions (`closeChildren`, PU:3440-3460), matching by path alone
  (slice names ignored); then sorts, discarding sort errors. Tooling for "closed" profiles (prohibit
  everything not mentioned); deeper unmentioned content under unsliced parents is not closed.
- **`cleanUpDifferential(sd)`** (PU:4570-4619): legacy repair for differentials that repeat a path without
  a slicing entry — inserts an `open` entry before the first occurrence, names the rows `slice-N` (or from
  `SNAPSHOT_slice_name` user data, which nothing in the cone sets), and picks a **hard-coded discriminator
  by path** (`determineSlicing`, PU:4622-4645): `.extension`→`value:url`, `DiagnosticReport.result`→
  `value:reference.code`, `Observation.related`→`value:target.reference.code` (STU3 element),
  `Bundle.entry`→`value:resource.@profile` (DSTU2 syntax); anything else → `Error("No slicing for …")`.
  DSTU2-conversion era. Java's *in-generation* counterpart to .NET's implicit extension slicing entry is
  not this but `makeExtensionSlicing` (PPP:343-345) and `checkToSeeIfSlicingExists` (PPP:955-987) — ch6.

## Deviations
- [DEV-027](13-deviation-register.md#dev-027--malformed-differentials-produce-silently-corrupt-net-snapshots-ch2) —
  Phase-4 fail-test evidence: an out-of-order differential (t23a) and a `..` path (obs-unit) pass through
  preprocessing and yield silently corrupt .NET snapshots (duplicate element id + fabricated `base.min`;
  phantom element + dropped constraint) where Java rejects both — Java mechanisms pinned 2026-09-01
  (orphan ERROR via the matching cursor jump; `checkDifferential` empty-segment throw).
- [DEV-028](13-deviation-register.md#dev-028--author-error-detection-catalogue-java-validates-net-emits-as-written-ch2ch6-ch9-ch12) —
  the full author-error detection catalogue (root type/slicing invariants group (f) is preprocessing
  territory; `checkDifferential`'s path grammar is Java-only).
- [DEV-035](13-deviation-register.md#dev-035--unmatched-and-out-of-order-differential-rows-java-drops-with-error-or-appends-by-derivation-net-silently-creates-new-elements-ch4) —
  ordering/unmatched-row handling incl. `sortDifferential` as Java's tooling-side normalization channel vs
  .NET's hand-patched fixture.
- [DEV-010](13-deviation-register.md#dev-010--runtime-patched-hl7-fixtures-fixinput) — `Fix_t23` is a manual
  stand-in for `sortDifferential`; [DEV-012](13-deviation-register.md#dev-012--t26-input-equals-expected) settled.

## Open questions
- [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors) preprocessing throws
  on malformed differentials (both sides now catalogued).
- [OQ-015](14-open-questions.md#oq-015--the-generator-mutates-its-input-differential) shared element
  instances make generator repairs visible to the caller (.NET); Java clones (answered 2026-09-01).
- [OQ-016](14-open-questions.md#oq-016--what-does-a-differential-less-structuredefinition-mean)
  differential-less StructureDefinitions.
