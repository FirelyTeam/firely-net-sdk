# 6. Slicing

> Status: **spec baseline + .NET + Java behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2
> packet 5, 2026-08-26: `startSlice`/`addSlice`/`findSliceAddPosition` deep-read; Phase 3 packet J-a,
> 2026-08-31: `ProfilePathProcessor` slicing paths + `SnapshotGenerationPreProcessor` propagation deep-read).

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
regenerate from that pristine clone, not from the merged slicing entry. The `min` reset is *permitted* by
§5.1.0.14 (an individual slice min may be 0, below the entry's min — "the only situation where this is
allowed"), but the spec never says 0 is the right **default** for a slice that states no `min` — inheriting
the entry's min would be equally consistent with the text. .NET chose 0 (rationale in code: a required
slice entry must still allow optional named slices); Java mostly agrees but carves out exceptions
(CLOSED-single-slice inherits the entry min, the `xtension.value[x]` hack — see the Java section).

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
  `rules = closed` being its precondition is likewise unchecked. (Java agrees — verified absence, packet J-a.)
- **`position` and `exists` discriminators** (R5) have no generator-side support or checks — irrelevant for
  named-slice generation, unsupported for unnamed-slice matching (ch4: `Invalid`).

## Java behavior (Phase 3 packet J-a, deep-read 2026-08-31)

Read at clone commit `b06c7ee`; line-verified. Full detail in the three extract files
(`java-ch06-simplepath-slicing-`, `java-ch06-slicedbase-and-PU-helpers-`,
`java-preprocessor-slicestuff-2026-08-31.md`, materials directory). `PPP` = `ProfilePathProcessor.java`,
`PU` = `ProfileUtilities.java`, `PRE` = `SnapshotGenerationPreProcessor.java`. Commit-pair caveat: sweep
oracle was 6.10.2 @ d06577dbc5c6.

Architecture: Java handles slicing in **two layers**. A preprocessor first *rewrites the differential*
(propagating slicing-entry trailing content into every named slice); the path processor then generates the
snapshot per path scope, with separate code paths for "base not sliced" (`processSimplePathDefault`,
`...WhereDiffsConstrainTypes`) and "base already sliced" (`processPathWithSlicedBase*`). Unlike .NET there
is no separate matcher/planner: matching and generation interleave in one recursive walk.

### Slice trailing-content propagation (the preprocessor — no .NET counterpart)

`PRE.processSlices` (`:688-741`, invoked unconditionally on a clone of every differential, `PU:824-825`)
partitions the diff into `SliceInfo` records: everything between a slicing entry and its first named slice
is **sliceStuff** — "stuff for all slices" — and is merged into each named slice's diff range
(`mergeElements` `:743-810`, `merge` `:993-1075`) before generation begins. This is the mechanism behind
[DEV-025](13-deviation-register.md#dev-025--materialization-depth-of-unconstrained-content-java-normalizes-more-than-net-ch7ch8ch11)
flavor 2 (77% of the sweep's min/mustSupport diffs). Precise semantics:

- **Property merge is strict fill-if-absent for all 27 handled properties** (`base.hasX() && !focus.hasX()`;
  list properties are `addAll`ed only when the target list is *empty* — no union/append semantics anywhere,
  correcting the packet-3 "append for constraint/example" estimate). **`mapping` and `condition` never
  propagate** on a match. `merge` uses **no `.copy()`**: matched elements in different slices end up
  *sharing object instances* (Binding/TypeRefComponent/Constraint) with the sliceStuff originals.
- **Match-vs-inject asymmetry:** a sliceStuff element that matches nothing in a slice's range is *injected*
  as a **full copy** (id rewritten by string-replacing the slicer id with the slice id `PRE:799`, marked
  `SNAPSHOT_PREPROCESS_INJECTED`) — so injected elements keep `mapping` etc. while matched ones only
  receive the 27-property fill.
- **Extension slicing is exempt** (never forms a SliceInfo) only when literally named `extension` with
  `rules=open`, exactly one `value:url` discriminator **and an explicit `ordered=false`** (`PRE:1077-1086`):
  the `"modiferExtension"` typo (`:1078`) means modifierExtension slicings never qualify, and omitting
  `ordered` (the common case) also disqualifies — such slicings get propagation treatment.
- **Bail-out blast radius:** one non-extension slicing entry inside one slicing's trailing region (with
  named slices present) logs `UNSUPPORTED_SLICING_COMPLEXITY` and returns from `processSlices` entirely
  (`PRE:713-723`) — abandoning propagation for *every* slicing in the differential and skipping the final
  `markExtensions` pass. A warning, not an error; generation proceeds unpreprocessed.
- **Cross-slice contamination bug (confirmed):** matching is by `(path modulo [x], sliceName-or-null)` only
  (`elementsMatch` `PRE:812-822`) over the slice's *entire descendant range* — element ids and inner-slice
  ancestry are never consulted. Extension-slice `value[x]` children (same path, no sliceName) are therefore
  indistinguishable: sliceStuff content authored for extension slice E1's `value[x]` merges into a
  *different* extension slice E2's `value[x]` inside the named slice, and — having "matched" — is never
  injected where it was intended, silently dropping the authored constraints. Both effects are visible in
  the `on-questionnaire` golden file. → new
  [DEV-033](13-deviation-register.md#dev-033--java-preprocessor-cross-slice-contamination--silent-constraint-loss-ch6).

### Introducing slicing on an unsliced base (`processSimplePathDefault`, `PPP:307-448`)

- **Sliceability IS checked** (contrast .NET, OQ-003): slicing a non-repeating element throws
  `Attempt to a slice an element that does not repeat…` — unless the intro itself is capped to `max=1`
  ("the sum total of your slices is limited to 1", exactly Ewout's use case in the .NET debate) or it is
  type slicing (`PPP:309-312`; `unbounded` = max present and neither "0" nor "1", `PU:2517`).
- **A missing slicing entry is an error** for non-extension elements (`DIFFERENTIAL_DOES_NOT_HAVE_A_SLICE`,
  `PPP:313-314`). For `.extension`/`.modifierExtension` the standard entry is fabricated —
  `makeExtensionSlicing()` = `value:url`/unordered/open (`PU:2408-2414`), same convention as .NET — and the
  row is stamped `SNAPSHOT_auto_added_slicing` (`PPP:343-345`), which later flips the min-sum gate from
  "report" to "silently fix" (below).
- An authored entry is accepted "at face value"; later diff rows that *restate* slicing are compared
  (`slicingMatches`): mismatch → ERROR `ATTEMPT_TO_CHANGE_SLICING`, agreement → INFORMATION with a broken
  message (2 args for 4 placeholders, `PPP:355-357`). Neither throws.
- Each named slice then re-processes the same base scope, carrying the slicer row both as slicing context
  (drives the min-reset rules) and as `slicerElement` (drives the max-cap).

### Type slicing on an unsliced base (`...WhereDiffsConstrainTypes`, `PPP:493-672`) — the DEV-020 mechanism

Recognition (`diffsConstrainTypes`, `PU:1806-1849`): all diff matches on a `[x]` path whose tails extend
the choice stem; the type of each slice is taken from its single `type`, else **inferred from the path
suffix or the sliceName suffix** — so Java applies the implicit type constraint in *both* syntactic forms:
a type slice with no stated `type` gets the inferred code added to its differential
(`PPP:571-572`), where .NET only constrains the renamed form
([OQ-018](14-open-questions.md#oq-018--implicit-type-constraint-only-for-the-renamed-form) Java side answered).
Slice names are enforced to the canonical `<stem><Type>` form — auto-set when absent, error when wrong
unless `autoFixSliceNames` (which validator_cli sets to true).

Steps that produce the [DEV-020](13-deviation-register.md#dev-020--type-slicing-entry-normalization-java-rewrites-the-sliced-element-net-merges-it-as-written-ch6)
gradient, now fully pinned:

1. Without an authored intro ("shortcut"), a synthetic entry is inserted into the **live differential**
   (removed afterwards): *typed* (types = the sliced types) on R3, *untyped* on R4+ — but only when the
   caller opts into `newSlicingProcessing` (`PPP:504-529`, with a Zulip link in the comment). The library
   default is **false** (plain API callers get the old typed branch even on R5), while the HL7 test driver
   turns it on per test with default *true* (`SnapShotGenerationTests.java:130,597` — only `dk1` opts out
   in the R5 manifest), so **the golden files and the sweep oracle reflect the untyped branch**. Shape
   checks reject `ordered=true`, >1 discriminator, non-`type`/`$this` discriminators (`PPP:542-557`).
2. After processing the entry, its slicing is **rebuilt unconditionally** as `type:$this`/CLOSED/unordered —
   discarding whatever the differential said (`PPP:595-598`, comment: "type slicing is always closed; the
   differential might call it open, but that just means it's not constraining the slices it doesn't
   mention").
3. A slice with `min>0` must be the **last** diff match (else throw `INVALID_SLICING…`); when legal the
   entry's min is raised **to literal 1** (not the slice's min, not a sum — `PPP:611-616`) and the slice's
   type is latched as `fixedType`, which then **removes all other types from the entry** (`PPP:638-645`).
4. Coverage check (`PPP:646-667`): if any type still allowed on the entry has no matching type slice, the
   slicing is flipped back to **OPEN** — the "always closed" comment is overstated. Exception: when the
   base path contains `xtension.value` *and* the shortcut form was used, the unsliced types are instead
   **deleted from the entry** and it stays CLOSED (`PPP:657-663`).

So: obs-2 (slice states type only) → rebuild CLOSED, 12 types unsliced → reopened OPEN, 13 types kept;
obs-2a (entry itself constrained to CC) → nothing unsliced → stays CLOSED; obs-2b (slice min=1) → entry
min := 1, fixedType collapses the type list → nothing unsliced → CLOSED. Exactly the golden gradient.
Quirk: after each slice recursion, `typeList.size() > start + 1` (i.e. ≥ 3 rows) resets the slice's min
to 0 (`PPP:630-632`) — an apparent off-by-one, quoted verbatim in the extract.

### Named slices: min reset, max cap, and the disabled slicer inheritance

- **Slice min** (`PPP:801-810`, only when the diff slice states no `min`): under non-CLOSED slicing (and
  when the base row is not itself a slice — protects reslices) min is reset to **0** — *except* paths
  ending `xtension.value[x]` ("hack work around for problems with snapshots in official releases": published
  extension snapshots have value[x] slices whose min was never reset). Under CLOSED slicing min resets to 0
  only with **≥ 2 named slices** ("they share the min cardinality between them"); a *single* slice under
  CLOSED **inherits the entry's min**. .NET unconditionally resets to 0 via the pristine slice base (ch4) —
  the two agree only in the plain open-slicing case.
- **Slice max** is silently **capped to the slicer's max** when it exceeds it (`PPP:816-818`) — .NET has no
  such cap (the diamond problem, ch5).
- **`APPLY_PROPERTIES_FROM_SLICER = false`** (`PPP:42-58`): a fix making slices inherit the slicer's
  properties (the profiling text "It also contains the unconstrained definition of the element that is
  sliced" read as inheritance) exists but is disabled — "the community decided not to apply this fix in
  practice, and to change(/clarify) the text above" (Zulip: #IG-creation, "Slices not inheriting preferred
  bindings from root"). Both engines therefore regenerate slices from the *unmerged base element*, by
  different mechanisms (.NET: pristine slice-base clone; Java: `currentBase.copy()`). Direct OQ-001/OQ-020
  material: the community already adjudicated *entry-properties-do-not-copy-down* once — yet the
  preprocessor copies entry *children* down (DEV-025), a tension worth a WGM question.
- **`checkToSeeIfSlicingExists`** (`PPP:955-987`): a named slice arriving with no slicer row in the result
  gets one injected — `value:url`/OPEN for `.extension` paths, `type:$this`/CLOSED for named+typed slices
  landing on an unsliced `[x]` element — and any *other* path gets **no slicer and no diagnostic** (silent
  third outcome; sdf-28 violated without comment).

### Merging onto an already-sliced base (`processPathWithSlicedBaseDefault`, `PPP:1225-1482`)

The in-code contract (`PPP:1202-1207`): definition order must be maintained regardless of `ordered`; slice
names must match; new slices append at the end; "corallory [sic]: you can't re-slice existing slices. is
that ok?" (partially stale — see reslicing below).

- **Slicing-compatibility checks** run only when the diff *restates* the slicing entry (`PPP:1229-1239`):
  `ordered` may not change in either direction (`orderMatches`, `PU:2372`); the base's discriminator list
  must be an order-sensitive *prefix* of the diff's — appending discriminators is allowed, dropping or
  reordering is not (`discriminatorMatches`, `PU:2376-2389`); `ruleMatches` (`PU:2392-2395`) allows
  anything over base OPEN, only OPENATEND over base OPENATEND, and CLOSED **or OPENATEND** over base CLOSED
  (the last a nominal *loosening* that is tolerated). The rules check is **skipped entirely for choice
  elements** (`!currentBase.isChoice()`, `PPP:1238`). All three throw `DefinitionException` on mismatch.
  This is the §5.1.0.17 lattice, approximately — where .NET checks nothing (OQ-005).
- **The diff's slicing merges onto the base's** via `updateFromSlicing` (`PU:2351-2370`): `ordered` and
  `rules` overwrite when stated (this is how open→closed lands), discriminators union-append keyed by
  (type,path), never removed; `slicing.description` is not merged.
- **Base slices are matched strictly in order by sliceName** (`PPP:1321`); a matched slice's scope recurses
  with `trimDifferential := closed`; an unmatched base slice is copied through (slicing component stripped,
  children raw-copied). A diff slice whose name matches a base slice *later* than the current position
  falls to the append stage and throws `Named items are out of order in the slice`.
- **Closed enforcement:** remaining diff slices against a CLOSED base slicing throw `The base snapshot marks
  a slicing as closed, but the differential tries to extend it…` — **except when the sliced path ends in
  `[x]`** ("we're going to constrain a slice that actually implicitly exists", `PPP:1364-1369`).
- **New slices** are cloned from the base *slicer*, get **own min := 0** ("we're in a slice, so it's only a
  mandatory if it's explicitly marked so", `PPP:1390`) before the diff merges, and — uniquely on this path —
  pick up min/max from the root element of their **single** type profile (mandatory profile root raises the
  slice min, non-repeating profile root caps max, `PPP:1398-1417`); *multiple* type profiles on a new slice
  throw a hard `Error("Not handled: multiple profiles at …")` (`PPP:1419`).
- **Reslicing:** a diff slice id with `/` in its tail re-targets the template to the already-emitted derived
  parent slice (looked up by regenerated id, `PPP:1377-1384`) — one level only ("this is wrong if there's
  more than one reslice (todo: one thing at a time)"). Note the diff dive-straight-in case: when diff[0] is
  a named slice with no slicing entry, the *copied base slicer* is stamped `SNAPSHOT_auto_added_slicing`
  (`PPP:1250`) — even though the base's entry was authored — with min-sum consequences below.
- **Diff silent about a sliced element** (`PPP:1657-1721`): with inner diff matches the walk continues under
  the slicer; otherwise slicer + children + all named slices are bulk-raw-copied.

### Type slicing over an already-sliced base (`PPP:1494-1655`)

Same synthetic-slicer/version machinery and shape/name checks as the simple path; the emitted entry is again
forced `type:$this`/CLOSED/unordered (`PPP:1584-1588`) — but **this path has no reopen logic**: the
coverage check of `PPP:646-667` has no counterpart here, so a type slicing over a *sliced* base stays
CLOSED unconditionally (asymmetry with the unsliced-base path). Base type-slice ranges are collected by
`findBaseSlices` (keyed by each base slice's **first** type code) and matched by exact type-code equality;
diff slices matching a base slice merge into its range, unmatched ones generate from the slicer scope.
**Unhandled base slices are replayed against a fake empty differential** (`PPP:1635-1650`) — re-emitted
through the normal merge machinery rather than dropped, so the forced CLOSED never prunes base content.
Robustness: an empty `baseSlices` list crashes with `IndexOutOfBoundsException` at `PPP:1652`.

### The slicing-entry min-sum gate (`PU:976-1036`)

A post-generation sweep over the finished snapshot counts each slice group's min/max sums (counter flushed
when a shallower-or-equal-depth foreign path appears; groups extending to the snapshot's last element are
**never checked**). On flush, gated by `Base.max != "1"` (a proxy for "not type slicing" — comment and code
diverge): if the slice mins sum above the entry's min, then **if the entry is flagged
`SNAPSHOT_auto_added_slicing` its min is silently overwritten with the sum** (`PU:998-999`); otherwise a
message is raised — ERROR only `forPublication`, else INFORMATION, always `ignorableError`. Max-sum
overflow is always INFORMATION; a min-sum > max-sum yields a WARNING whose text prints the wrong values
(entry min/max instead of the sums). Net effect: **whether the differential restates the slicing entry
decides between silent repair and a report** — the DEV-020 C2 mechanism, now with corrected citations
(`PPP:343-345` extension entries, `PPP:1250` sliced-base dive-in; the old `au-med-k` attribution to
`:343-345` was wrong).

### The Base component of slices — agreement with .NET

`updateFromBase` (`PU:2004-2020`) copies `base.path/min/max` **verbatim from the sliced element's Base
component** for the entry and every slice row alike — `Base.min` is never reset to 0 (only the slice's *own*
min is zeroed). This matches .NET's deep-copy-without-reset (ch10), settling the slice-`Base.min` question
as **inter-engine agreement**; what neither engine has is spec text saying which is right (sdf-8b silence).

### What Java does *not* do (verified absences, this scope)

- `@default` slices: the reserved name appears nowhere in the generator — same as .NET (agreement).
- `position`/`exists` discriminators: no generator-side support (the preprocessor's additional-base
  machinery throws "Not supported yet" on them; the main path never interprets them).
- eld-16 slice-name grammar: not validated (reslice `/` convention interpreted, rest uninspected) — but
  unlike .NET, wrong *type-slice* names are errors and missing ones are synthesized.
- `openAtEnd`: accepted through the rules lattice, never enforced as an ordering constraint — matching
  .NET's non-enforcement (agreement in outcome, not in checking).

## Deviations
- [DEV-008](13-deviation-register.md#dev-008--extension-header-slicing-element-ch6) extension header slicing.
- [DEV-020](13-deviation-register.md#dev-020--type-slicing-entry-normalization-java-rewrites-the-sliced-element-net-merges-it-as-written-ch6)
  type-slicing entry normalization (obs-2 family).
- [DEV-024](13-deviation-register.md#dev-024--net-drops-reslice-subtrees-entirely--silent-constraint-loss-ch6) —
  Phase-4 sweep: **.NET drops reslice subtrees entirely**, including the differential's own constraints
  on them (reslicing-profile, slice23) — silent data loss, strongest register item of the sweep.
- [DEV-025](13-deviation-register.md#dev-025--materialization-depth-of-unconstrained-content-java-normalizes-more-than-net-ch7ch8ch11) —
  Java materializes slicing-entry child constraints into every named slice (org2a/on-questionnaire);
  .NET leaves slices bare (→ OQ-021).
- [DEV-026](13-deviation-register.md#dev-026--renamed-choice-constraints-net-anchors-on-a-synthesized-type-slice-java-on-bare-valuex-ch6ch7) —
  renamed-choice constraints: .NET synthesizes a `value[x]:valueTYPE` slice, Java anchors on bare
  `value[x]` (t16/t31).
- [DEV-028](13-deviation-register.md#dev-028--author-error-detection-catalogue-java-validates-net-emits-as-written-ch2ch6-ch9-ch12)
  groups (c) slice-name conventions, (g) non-repeating-element slicing, (j) slice-cardinality arithmetic
  (= DEV-007).
- [DEV-033](13-deviation-register.md#dev-033--java-preprocessor-cross-slice-contamination--silent-constraint-loss-ch6) —
  Java preprocessor bug (confirmed packet J-a): sliceStuff matching by (path, sliceName) only contaminates
  foreign extension-slice `value[x]` children and silently drops the intended constraints (on-questionnaire).

## Open questions
- [OQ-003](14-open-questions.md#oq-003--slicing-non-repeating-elements) slicing non-repeating elements
  (both sides answered — .NET accepts without issue; Java errors unless capped-to-1 or type slicing).
- [OQ-005](14-open-questions.md#oq-005--enforcing-slicingrules--closed--openatend) enforcing closed/openAtEnd
  (both sides answered — .NET never reads rules/ordered; Java enforces a partial lattice).
- [OQ-006](14-open-questions.md#oq-006--sliceisconstraining) sliceIsConstraining.
- [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors) error taxonomy — new
  row: explicit extension slicing-entry children silently dropped.
- [OQ-018](14-open-questions.md#oq-018--implicit-type-constraint-only-for-the-renamed-form) implicit type
  constraint applies to the renamed (R4) form only.
