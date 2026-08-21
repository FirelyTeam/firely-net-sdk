# 2. Differential preprocessing

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

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

## .NET behavior (Phase 2)
*(pending — `DifferentialTreeConstructor.cs`, `DifferentialComponent.MakeTree()`)*

## Java behavior (Phase 3)
*(pending — `SnapshotGenerationPreProcessor.java`)*
