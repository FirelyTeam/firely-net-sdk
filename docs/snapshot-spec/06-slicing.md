# 6. Slicing

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

## Scope
Everything slicing: the slicing entry (discriminators, `ordered`, `rules`), named slices, type slices,
extension slicing by `url`, reslicing, slice matching between a derived differential and an already-sliced
base, ordering, slice cardinality, and synthesizing a slicing entry when the differential omits one.

## Spec baseline (R5)

### Concept and structure

Slicing splits a repeating element (or a choice of types) "into a series of sub-lists, each with different
restrictions" [profiling §5.1.0.12]; slice names "are never exchanged" (serialization unaltered).
Structure [profiling §5.1.0.13, elementdefinition #slicing, defs]:

- "Slicing is only allowed when constraining an existing structure"; only "on the first repetition of an
  element" — that element is the **slicing entry**. sdf-20/23: no slicing/sliceName on the root element.
- The entry "contains the unconstrained definition of the element that is sliced, potentially including
  children"; it "is understood to be the set of constraints that apply to all slices and entries, whether
  they have a defined slice or not", except: slicing must be present, and its "min governs the number of
  total occurrences of the sliced element including the … open portion" (individual slices may differ).
- "All elements following the first repeat that containing a slicing SHALL have a sliceName"; the slice
  set is "any elements that come after this in the element sequence that have the same path, until a
  shorter path occurs".
- Slice-group adjacency: entries "must be adjacent … or, if there are any intervening elements, those
  elements must be 'compatible with' the group" (paths starting with the group's path).
- Slices are mutually exclusive: "an element in a resource instance will never match more than one element
  in a given slice group".
- sdf-28: a snapshot slicing entry must have a discriminator or a description.

### Discriminators [profiling §5.1.0.13]

"When a constraining structure designates one or more discriminators, it SHALL ensure that the possible
values for each slice are different and non-overlapping". Each discriminator = (type, restricted-FHIRPath
path — only element selections, `extension(url)`, `resolve()`, `ofType()`). Composite: "It is the
composite (combined) values of the discriminators that are unique, not each discriminator alone." Six
types in R5:

| type | rule |
|---|---|
| value | "different values in the nominated element, as determined by the applicable fixed value, pattern, or required ValueSet binding" |
| exists | presence/absence; "SHALL be no more than two slices", one max=0 and one min≥1 |
| pattern | "same meaning as 'value' and is deprecated" |
| type | by type of the nominated element |
| profile | by conformance to a profile (expensive; resolve() → targetProfile) |
| position | by index; "only possible if all but the last slice have min=max cardinality" — the notes add "min > 0" *(defined twice, inconsistently — [RFC-002](15-spec-rfcs.md#rfc-002--profiling-position-discriminator-defined-twice-with-differing-conditions))* |

Slice definitions must *back* value/pattern discriminators: fixed[x], pattern[x], or a required binding to
an extensional value set. Discriminators are optional but their absence is discouraged.

### Slice cardinality [profiling §5.1.0.14]

For a sliced element `m..n`: each slice's max ≤ n; the sum of maxes may exceed n; "The sum of the minimum
cardinalities must be less or equal to n" *(bullet 3)* while bullet 5 says the sum "SHOULD be less than or
equal to m" *(contradiction — [RFC-001](15-spec-rfcs.md#rfc-001--profiling-51014-contradictory-normative-strength-on-slice-minimum-sums))*;
an individual slice min may be 0 (below m — "the only situation where this is allowed").

### Default slice [profiling §5.1.0.15]

`sliceName = @default` (reserved): rules for "all of the remaining content that is not in one of the
defined slices". Only allowed when `rules = closed`; must not fix discriminator values; may be resliced
(`@default/@default`).

### Reslicing and constraining inherited slicing [profiling §5.1.0.17]

- "ElementDefinition.slicing.rule can be constrained from open to closed"
- "ElementDefinition.slicing.ordered can be constrained from false to true"
- Child profiles "SHALL include all the same discriminators; MAY add additional discriminators"
- Constraint on a sliced element **without** sliceName → "adding constraints to all slices of X";
  **with** sliceName → that slice only (new name = new slice; matching name = further constraints)
- Reslice names: `parent/child` ("example/example1"), nestable indefinitely; slice names unique across the
  profile's slices; eld-16 grammar `^[a-zA-Z0-9\/\-_\[\]\@]+$`
- "it is possible for Profile C to make rules that are incompatible with profile B" — unsatisfiable
  profiles are acknowledged, tool behavior unprescribed
- `sliceIsConstraining` (Trial Use, eld-22): true ⇒ an ancestor SHALL have a slice of this name; false ⇒
  no ancestor may
- eld-25 (warning): no `ordered`/`openAtEnd` slicing unless `orderMeaning` is present on the element

### Extension slicing

> "Note that extensions are always sliced by the url element, though they may be resliced on additional
> elements where required." [profiling §5.1.0.13]

The standard discriminator is `value:url` [profiling §5.1.0.18.1]. Ordering within a sliced element:
"Unsliced descendants of sliced elements appear before slices" [structuredefinition §5.4.6].

### Spec gaps (slicing)

1. `openAtEnd` transitions absent from the constraint lattice ([RFC-011](15-spec-rfcs.md#rfc-011--openatend-transitions-never-addressed)).
2. Merging a differential onto an *already-sliced base* (slicing-component compatibility, distributing
   no-sliceName constraints over base slices, inheriting the parent's entry) — unstated.
3. Whether/when a generator synthesizes a missing slicing entry (the universal extension-slicing
   convention) — the convention is stated, the generator obligation is not.
4. Reslice *merge* semantics (how `parent/child` slices nest in the snapshot) — only the naming is given.
5. Whether slicing entry children + per-slice children must both appear fully expanded in the snapshot —
  unstated.

## R4/R4B deltas

- Discriminator `value` redefined in R5 to include pattern and required-binding matching; `pattern`
  deprecated-as-alias; **`position` is new in R5**; `exists` constraints (two slices, max=0/min≥1) are new
  in R5.
- R4's first slice-cardinality rule was garbled ("Each slice cannot have a greater cardinality than the
  maximum number of slices allowed" — sic); R5 fixed it and added the SHOULD-sum-of-mins rule.
- Reslicing rules, @default, adjacency, boundary rule: **verbatim identical**.
- `sliceIsConstraining` present in R4 4.0.1 already.

## R6-build note (v6.0.0-ballot4, fetched 2026-08-21)

R6 directly addresses two of the gaps above: it adds an **explicit list of slicing-entry constraints that
apply across all slices** (max, type+profiles, fixed/pattern, min/maxValue, maxLength, constraints,
required/extensible bindings incl. additional, mustHaveValue, valueAlternatives) — with `mustSupport`
explicitly NOT propagating to slices — and a new paragraph allowing discriminating elements to be asserted
in profiles other than the one defining the slicing. Partially resolves gap 2/5; fold into the merge
algorithm design and check whether R5-era implementations already behave this way (likely: this reads like
codified tooling practice).

## .NET behavior (Phase 2)
*(pending — `SnapshotGenerator.startSlice/addSlice` :1787/:1933, `createExtensionSlicingEntry` :2147,
`ElementMatcher.constructSliceMatch` :565 + discriminator-specific matchers, slice-base cloning
(`initSliceBase`), `applyImplicitChoiceTypeConstraint` :2022)*

## Java behavior (Phase 3)
*(pending — `ProfilePathProcessor` slicing paths, `BaseTypeSlice`, `TypeSlice`)*

## Deviations
- [DEV-008](13-deviation-register.md#dev-008--extension-header-slicing-element-ch6) extension header slicing.

## Open questions
- [OQ-003](14-open-questions.md#oq-003--slicing-non-repeating-elements) slicing non-repeating elements.
- [OQ-005](14-open-questions.md#oq-005--enforcing-slicingrules--closed--openatend) enforcing closed/openAtEnd.
- [OQ-006](14-open-questions.md#oq-006--sliceisconstraining) sliceIsConstraining.
