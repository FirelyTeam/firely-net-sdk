# 2. Differential preprocessing

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 3,
> 2026-08-24: `DifferentialTreeConstructor.cs` deep-read). Java section pending (Phase 3).

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

## Java behavior (Phase 3)
*(deep-read pending — `SnapshotGenerationPreProcessor.java`; first empirically-grounded findings from
Phase 4 packet 3, 2026-08-26:)*

- **Slice-content propagation** (the preprocessor's headline job, located via min/mustSupport mining):
  before generation, Java collects "sliceStuff" — the differential elements between a slicing entry and
  its first named slice — and merges it into **each named slice's differential** (`processSlices` →
  `mergeElements` → `merge`, ~:688-810, :993-1075; invoked from `ProfileUtilities.java:825`).
  Fill-if-absent for min/max/mustSupport/fixed/pattern/type/binding, *append* for constraint/example;
  missing elements are injected with rewritten ids (`SNAPSHOT_PREPROCESS_INJECTED`). Extension slicing
  is excluded as a slicer; entry-level extension slices ride along as sliceStuff. .NET has no
  counterpart mechanism — see
  [DEV-025](13-deviation-register.md#dev-025--materialization-depth-of-unconstrained-content-java-normalizes-more-than-net-ch7ch8ch11)
  and [OQ-021](14-open-questions.md#oq-021--how-much-must-a-snapshot-materialize).
- **additionalBase merging** (`process:137-152`): a second base's differential merged in
  ([DEV-032](13-deviation-register.md#dev-032--java-only-merge-inputs-additionalbase-and-obligation-profiles-ch3)).
- **Suspected defect:** `elementsMatch` (:812-822) matches on leaf path + leaf sliceName only, ignoring
  ancestor slices — cross-slice constraint contamination observed (on-questionnaire, DEV-025's M1q
  note); Phase-3 verification target.

## Deviations
- [DEV-027](13-deviation-register.md#dev-027--malformed-differentials-produce-silently-corrupt-net-snapshots-ch2) —
  Phase-4 fail-test evidence: an out-of-order differential (t23a) and a `..` path (obs-unit) pass through
  preprocessing and yield silently corrupt .NET snapshots (duplicate element id + fabricated `base.min`;
  phantom element + dropped constraint) where Java rejects both.
- [DEV-028](13-deviation-register.md#dev-028--author-error-detection-catalogue-java-validates-net-emits-as-written-ch2ch6-ch9-ch12) —
  the full author-error detection catalogue (root type/slicing invariants group (f) is preprocessing
  territory).

## Open questions
- [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors) preprocessing throws
  on malformed differentials.
- [OQ-015](14-open-questions.md#oq-015--the-generator-mutates-its-input-differential) shared element
  instances make generator repairs visible to the caller.
- [OQ-016](14-open-questions.md#oq-016--what-does-a-differential-less-structuredefinition-mean)
  differential-less StructureDefinitions.
