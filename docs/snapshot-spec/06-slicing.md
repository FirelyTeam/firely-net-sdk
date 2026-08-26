# 6. Slicing

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 5,
> 2026-08-26: `startSlice`/`addSlice`/`findSliceAddPosition` deep-read). Java section pending (Phase 3).

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

## .NET behavior (Phase 2, deep-read 2026-08-26)

Slicing is split between the matcher and the generator: `ElementMatcher.constructSliceMatch`
(`ElementMatcher.cs:565-771`) *plans* the slice group — which diff element is the entry, which named slices
merge into base slices, which are new — and the generator *executes* the plan through two actions:
`Slice` → `startSlice` (`SnapshotGenerator.cs:834` → `:1787`) opens the group and processes the slicing
entry; `Add` → `addSlice` (`:831` → `:1933`) appends one new slice. A named diff slice that matches an
existing base slice is an ordinary `Merge`. Matching detail (discriminator-based matching of unnamed
slices, `sliceIsConstraining`, reslice matching) is in [chapter 4](04-element-matching.md); this section
covers generation.

Every slice match carries a **slice base**: a recursive clone of the unmerged base element with the
`slicing` component removed and `min` reset to 0 (`initSliceBase`, ch4). All new slices in a group
regenerate from that pristine clone, not from the merged slicing entry.

### Opening a slice group (`startSlice`, `:1787`)

- **Sliceability is not checked.** A reject of slices on non-repeating, non-choice elements exists but is
  compiled out (`REJECT_SLICE_NONREPEATING_ELEMENT`, `:26`, `:1797-1804`), with the design disagreement
  preserved inline — "Ewout: no reason to reject; e.g. derived profile can limit sliced base element
  cardinality to 0..1. Vadim: may introduce issues for code generators" (`:1794-1796`). So .NET slices
  `max = 1` elements without comment ([OQ-003](14-open-questions.md#oq-003--slicing-non-repeating-elements)).
- **Missing slicing entry:** for extension elements the entry is synthesized —
  `createExtensionSlicingEntry` (`:2147`) clones the base extension element and attaches
  `slicing = { discriminator: [value:url], ordered: false, rules: open }` (the universal convention,
  [profiling §5.1.0.18.1]). For non-extension elements a "missing slice entry" issue is emitted and the
  branch returns (`:1819-1826`) — though with the current matcher this appears unreachable: a `Slice`
  action always carries either a real diff slicing entry or an extension element (`ElementMatcher.cs:641`).
- **Merging the entry** (`:1853-1872`): a synthesized extension entry — and equally a *real* slicing entry
  that the differential put on an extension element — is merged **shallowly** via `mergeElementDefinition`
  (element id excluded, then regenerated). Children of an explicit extension slicing entry are therefore
  silently dropped (new row in
  [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors)). A non-extension
  slicing entry goes through the full recursive `mergeElement`, entry children included.
- **Named (re)slicing entries:** if the entry itself carries a `sliceName` that is a *sibling* of the slice
  at the match position (`IsSiblingSliceOf`, `:1839`), the generator first materializes a fresh copy of the
  slice base after the existing group (`addSliceBase`, or `DuplicateAfter` when no slice base is available)
  and merges the entry onto that copy (`:1839-1851`). This covers a differential that introduces a new
  named slice and immediately reslices it; the code comment traces it to DSTU2's `Composition.section`
  (named `section` in the core resource) — legacy, reachability in R4/R5 uncertain.

### Adding a slice (`addSlice`, `:1933`)

The comment block at `:1889-1931` is the closest thing to a written contract for slice ordering — three
worked examples with the rule: "diff (re)slicing constraints must be in same order as base"; diff can only
append new slices after all constraints on existing slices (see ch4's forward-only walk).

The method distinguishes two type-slice forms on a choice element:

- `isRenamed` — R4 form: diff path is a rename of the base path (`valueString` vs `value[x]`, `:1956`);
- `isImplicitTypeSlice` — R5 form: same `[x]` path plus a `sliceName` (`:1957`).

Steps, in order:

1. **Synthesize the type-slice slicing entry** (`:1960-1971`): in either form, if the base `[x]` element has
   no `slicing` yet it gets `{ discriminator: [type:$this], ordered: false, rules: open }`
   (`DiscriminatorComponent.ForTypeSlice()`, `ElementDefinition.cs:63`). The inline comment justifies
   `open`: "since in R4, we can just have a slice with constraints for one of the types". Together with the
   extension entry this is the second of exactly two generator-synthesized slicing entries (ch6 spec gap 3).
2. **Insert the pristine slice copy** (`addSliceBase`, `:2052`): a deep copy of the slice base element is
   inserted at the position computed by `findSliceAddPosition`, then its child elements are copied
   *unmerged* (`CopyChildren`, `:2067-2074`). `InsertAfter` rewrites the copy's path to parent-path +
   the base element's own name (`ElementDefinitionNavigator.cs:392,400`) — so a renamed diff slice enters
   the snapshot under its normalized `[x]` name from the start. A missing slice base or failed insert is an
   internal error → throws `InvalidOperation` (`:2057-2079`).
3. **Position** (`findSliceAddPosition`, `:2085`): two modes.
   *Renamed-choice mode* (diff has no `sliceName` and is a rename of the snap element): scan forward past
   the `[x]` element and everything that is a rename of it, append after the last (`:2093-2115`) — this is
   what lets multiple renamed constraints (`valueString`, `valueInteger`) accumulate.
   *Named mode*: if the diff slice name is a reslice (`A/1`), find base slice `A`, skip its existing
   reslices, insert after the last one — so reslices stay grouped under their base slice; otherwise scan to
   the last element with the same path name, i.e. new sibling slices append at the end of the group
   (`:2116-2141`).
4. **Renamed-form extras** (`:1981-1998`):
   - `applyImplicitChoiceTypeConstraint` (`:2022`) — issue #1074: when the renamed diff slice states **no
     explicit types**, the type is parsed from the rename (`ParseTypeFromRenamedElement`,
     `ElementDefinitionNavigationFunctions.cs:109`, with primitive-type uncapitalization) and the slice's
     type list is reduced to the matching base `TypeRefComponent`; a rename whose suffix matches no base
     type code yields an "invalid choice rename" issue and no constraint (`:2038-2041`).
   - a missing `sliceName` is auto-generated from the renamed path segment (`sliceName = "valueString"`,
     `:1988-1991`).
   - `NORMALIZE_RENAMED_TYPESLICE` is hard-`#define`d in generator, merger *and* test suite
     (`SnapshotGenerator.cs:37`, `ElementDefnMerger.cs:11`, `SnapshotGeneratorTest.cs:24`): the snapshot
     always keeps the normalized `[x]` path (the R5 form — .NET emits it for R4 targets too). The
     un-normalizing code at `:1993-1997` and the merger's renamed-path merge (`ElementDefnMerger.cs:76-92`)
     are dead; what remains live in the merger is a hard **throw** when paths mismatch on a non-choice base
     (`ElementDefnMerger.cs:71-74`).
   - **Asymmetry:** the R5 form gets none of this — an R5-form type slice without an explicit `type` keeps
     the base's full choice list
     ([OQ-018](14-open-questions.md#oq-018--implicit-type-constraint-only-for-the-renamed-form)).
5. `prepareSliceElements` (`:2008` → `:1644`) is event plumbing only — it raises `OnPrepareElement` with
   the slice-base clone as the base element and is a no-op without subscribers.
6. **Merge the differential** onto the fresh copy via the ordinary recursive `mergeElement` (`:2014`).

### Reslicing

Reslice structure lives entirely in the name: `/` is the only interpreted character
(`GetBaseSliceName`/`IsResliceOf`/`IsDirectResliceOf`/`IsSiblingSliceOf`,
`ElementDefinitionNavigationFunctions.cs:157-287`); the eld-16 name grammar is never validated. The matcher
re-targets the slice base of a reslice `A/1` to base slice `A` (ch4), a diff-internal reslice group (named
slice with its own slicing entry) recurses through `constructSliceMatch` (`ElementMatcher.cs:727-740`), and
`findSliceAddPosition` keeps reslices adjacent to their base slice (above). The snapshot thus interleaves
reslices into the slice sequence — matching the id algorithm's `slice/reslice` nesting (ch10).

### What .NET does *not* do (verified absences)

- **`slicing.rules`, `ordered`, `openAtEnd` are never read** — only written by the two synthesized entries
  (`:1968-1969`, `:2163-2164`). No enforcement of closed (a derived diff can append slices to a closed
  slicing without any issue), no openAtEnd ordering, no open→closed/false→true lattice check. Acknowledged
  as a file-header TODO: "Enforce/verify Slicing.Rule = Closed / OpenAtEnd" (`:22`;
  [OQ-005](14-open-questions.md#oq-005--enforcing-slicingrules--closed--openatend)).
- **Discriminators are never validated**: not for restricted-FHIRPath syntax, not for being backed by
  fixed/pattern/binding in the slices, not for the §5.1.0.17 "derived slicing SHALL include all the same
  discriminators" rule — the slicing component merges by ch5's overlay semantics (diff discriminator list
  replaces wholesale) with no compatibility check. Discriminators influence generation only as the matching
  key for *unnamed* slices (ch4: url for extensions — with a warning when the declared discriminator is not
  `value:url` — and `@type`/`@profile`).
- **Slice cardinality arithmetic** (§5.1.0.14 sums) is not checked.
- **`@default`** does not occur anywhere in the generator — the reserved name is an ordinary slice name;
  `rules = closed` being its precondition is likewise unchecked. (Java comparison pending, Phase 3.)
- **`position` and `exists` discriminators** (R5) have no generator-side support or checks — irrelevant for
  named-slice generation, unsupported for unnamed-slice matching (ch4: `Invalid`).

## Java behavior (Phase 3)
*(pending — `ProfilePathProcessor` slicing paths, `BaseTypeSlice`, `TypeSlice`)*

## Deviations
- [DEV-008](13-deviation-register.md#dev-008--extension-header-slicing-element-ch6) extension header slicing.

## Open questions
- [OQ-003](14-open-questions.md#oq-003--slicing-non-repeating-elements) slicing non-repeating elements
  (.NET side answered 2026-08-26 — accepted without issue).
- [OQ-005](14-open-questions.md#oq-005--enforcing-slicingrules--closed--openatend) enforcing closed/openAtEnd
  (.NET side answered 2026-08-26 — rules/ordered never read).
- [OQ-006](14-open-questions.md#oq-006--sliceisconstraining) sliceIsConstraining.
- [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors) error taxonomy — new
  row: explicit extension slicing-entry children silently dropped.
- [OQ-018](14-open-questions.md#oq-018--implicit-type-constraint-only-for-the-renamed-form) implicit type
  constraint applies to the renamed (R4) form only.
