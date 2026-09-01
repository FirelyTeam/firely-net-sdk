# 4. Element matching

> Status: **spec baseline + .NET + Java behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2
> packet 2, 2026-08-24: `ElementMatcher.cs` deep-read; Phase 3 packet J-c, 2026-09-01: `getDiffMatches` +
> `ProfilePathProcessor` dispatch deep-read).

## Scope
Pairing each differential element with its base-snapshot counterpart, one tree level at a time: exact path
matching, choice-type matching, slice matching, the decision space per pair (merge in place / add as slice /
open a slice group / introduce a new element / remove), and ordering requirements. Matching detail lives
here; slicing *semantics* (discriminators, reslicing, closed/openAtEnd) live in [chapter 6](06-slicing.md).

## Spec baseline (R5)

**There is no element-matching algorithm in the spec.** What exists is raw material the algorithm must be
consistent with:

### Identity: path, id, sliceName

> "The path element is the most important property of the element definition. It both names the element,
> and locates the element within a hierarchy … Within the FHIR specification, there is only one original
> definition for each path." [elementdefinition #path]

- Constraint SDs "are not allowed to define or include ElementDefinitions with a path not defined within
  the base type definition from which they derive" [elementdefinition #path] — an unmatched differential
  path is an **error**, never a new element (contrast specializations/logical models).
- Element ids are deterministic from path + sliceNames (`pathpart:slicename/reslicename` per token,
  [elementdefinition #id]; see ch10) — so id-based and path+sliceName-based matching are formally
  equivalent when ids follow the algorithm.
- `sliceName` is "required for slices, else prohibited" in constraint differentials
  [elementdefinition #interpretation], and unique within the structure per sliced element (eld-16 grammar).
- `sliceIsConstraining` (Trial Use) *implies* the cross-profile matching rule: "If set to true, an ancestor
  profile SHALL have a slicing definition with this name" [elementdefinition-definitions] — i.e. derived
  slices match ancestor slices **by sliceName**. That is the closest the spec comes to stating a matching
  rule.

### Ordering as a matching aid

Differential elements "must be in the same order as the baseDefinition, children depth-first; unsliced
descendants of sliced elements appear before slices" [structuredefinition §5.4.6] — so a conforming
differential can be matched in a single forward walk of the base. Slice-group adjacency: entries "must be
adjacent … or, if there are any intervening elements, those elements must be 'compatible with' the group"
(paths starting with the group's path) [profiling §5.1.0.13]. The slice-set boundary: "The set of slices is
any elements that come after this in the element sequence that have the same path, until a shorter path
occurs" [elementdefinition-definitions, slicing].

### Choice-type (`value[x]`) matching

R5 rules [elementdefinition #typesx], verbatim core:

> "- Constraints limiting the acceptable list of types must be applied to the original '[x]' element …
> - The inclusion of a type specific element (such as 'Patient.deceased[x]:deceasedBoolean') SHALL NOT be
>   interpreted as constraining allowed types, but instead, it constrains the use of a particular type
> - the original element SHALL always be represented in a snapshot; the type specific variants are only
>   represented when needed"

So in R5, a type-specific constraint is a **type slice**: path stays `…[x]`, sliceName is the rendered
type name, id is `…[x]:valueBoolean`-style. Renamed paths (`Patient.deceasedBoolean`) are neither
sanctioned nor prohibited in R5 (spec gap; see
[RFC-013](15-spec-rfcs.md#rfc-013--sanction-or-prohibit-renamed-choice-type-paths-in-snapshots)).

### The decision space (implied, not specified)

From the rules above, a matcher confronts per differential child: same path & no slicing → constrain in
place; sliceName present → match base slice by name, else new slice; slicing component present → the
slicing entry; renamed/type-sliced choice → the `[x]` element; unmatched path → error (constraints) or new
element (specializations/logical models). None of this taxonomy is spec text — it is what both
implementations independently derived (Phases 2–3 document each).

### Spec gaps (matching)

1. No matching algorithm; no statement on resolving ambiguity (e.g. duplicate paths without sliceNames).
2. Matching semantics for a differential that constrains "all slices of X" (no sliceName on a sliced
   element, sanctioned by [profiling §5.1.0.17]) — how it distributes over base slices is unstated.
3. Whether matching may rely on ids when they deviate from the deterministic algorithm — unstated.

## R4/R4B deltas

- **Headline (same as ch2):** R4 prescribed *renamed paths* for single-type choice constraints; R5 requires
  the `[x]`-path type-slice form. R4 id-algorithm example even used `path = Patient.deceasedBoolean`
  [R4 ed §2.30.0.3]. Matchers must accept both, per version.
- `sliceIsConstraining` exists in R4 4.0.1 already (verified) — no delta.
- Slice-group adjacency, boundary rule, ordering: identical.

## .NET behavior (Phase 2, deep-read 2026-08-24)

Matching is isolated in `ElementMatcher` (`ElementMatcher.cs`, internal static class). Per tree level,
`Match(snapNav, diffNav)` (`:84`) pairs the *children* of the current base-snapshot element with the
children of the current differential element and returns a list of `MatchInfo` records: an action, a base
bookmark, a diff bookmark, an optional cloned *slice base* element, and an optional issue. The
`SnapshotGenerator` then executes the actions (`SnapshotGenerator.cs:825-859`) and recurses into children.
Two compile-time switches are active: `MULTIPLE_SLICE_PROFILES` and `GENERATE_MISSING_TYPE_SLICE_NAMES`
(`:10`, `:14`).

### Walk mechanics: one forward pass, ordering-dependent

The diff children are consumed strictly in document order; for each, `matchBase` (`:134`) matches the base
child by **last path segment**: current-position name equality, else a *forward-only* `MoveToNext(name)`
scan of the remaining base siblings. If that fails, the diff name is tested as a renamed choice element
against the level's choice-element names (`listChoiceElements`, `:1087`): `IsRenamedChoiceTypeElement` is a
**pure prefix test** — `otherName` matches `name[x]` when it starts with the stem and is longer
(`ElementDefinitionNavigationFunctions.cs:94`); the suffix is *not* validated to be a type name. No match at
all → the element is **New**.

Because the base cursor never moves backwards, .NET operationalizes the spec's ordering rule
[structuredefinition §5.4.6]: a differential that revisits an earlier sibling path does not re-match it —
the constraint silently degrades to a New element. The same holds inside slice groups ("diff cannot
re-order existing slices or insert new slices", `:708`; forward-only `MoveToNextSlice`/`MoveToSliceBase`,
`ElementNavigatorSlicingExtensions.cs:83,221`).

### The action space

| Action | Produced when | Consumed as (`SnapshotGenerator.cs`) |
|---|---|---|
| `Merge` | same-name match; renamed choice matched to base `[x]`, renamed base element or named type slice; named diff slice matching the base slice at the cursor; diff constraint on an inherited slice entry; unnamed extension/type slice matching by url/type | `mergeElement` (`:828`) |
| `Add` | named or unnamed slice with no matching base slice; renamed choice element with no renamed/named-slice counterpart in base | `addSlice` from `SliceBase` (`:831`) |
| `Slice` | diff carries a `slicing` component (slice entry), or *implicitly* for extension elements when the base is not yet sliced (`DiffBookmark = Bookmark.Empty`; the generator synthesizes the entry, `createExtensionSlicingEntry` `:1713`) | `startSlice` (`:834`) |
| `New` | no base sibling matches the diff path — legitimate for specializations/logical models; for constraint profiles this is the *silent* fallback (no issue emitted, `createNewElement` `:887`) | `createNewElement` (`:839`) |
| `Invalid` | `sliceIsConstraining` conflicting with the name match; unnamed slice under an unsupported discriminator; type slice without a type | issue collected, element **discarded** (`:841-843`) |
| `Remove` | base type slices made redundant because the diff slice intro constrains the type list (MS 20220712) | second pass after all other actions, `removeElement` (`:848-859`) |

### Named-slice matching and `sliceIsConstraining` (`matchSlice`, `:804-853`)

A named diff slice matches when the base cursor's slice name is equal (ordinal). If
`sliceIsConstraining` is present it is **enforced** (`:816-838`): `true` without a matching base slice →
`Invalid` + "no match" issue; `false` with a matching base slice → `Invalid` + "conflict" issue; the
element is dropped. Absent → STU3 fallback: match ⇒ constrain, no match ⇒ new slice (`Add`). (A disabled
duplicate of this check sits in dead `#if false` code at `:600-623` — earlier project notes cited that block
and wrongly concluded .NET ignores the property; corrected in [OQ-006](14-open-questions.md#oq-006--sliceisconstraining).)
Reslices match through `IsSliceBase`: a diff slice `A/B` matches base slice `A`
(`ElementNavigatorSlicingExtensions.cs:209`).

### Unnamed slices (`:855-887`)

Allowed only for extensions and type-discriminated slices; everything else → `Invalid` ("slices must be
named"). Extensions match on the extension profile url (`type[0].profile`), regardless of the declared
discriminator — a non-`url` discriminator on an extension slicing only produces an issue, matching proceeds
on url anyway (`matchExtensionSlice`, `:892-918`). A `@type` discriminator matches on the **full type-code
sequence** (`SequenceEqual` — order-sensitive, `matchSliceByTypeCode` `:922`); `@profile` (alone or with
`@type`) additionally compares the profile lists, where a profile reference of the form `url#name` is
matched against the base slice *name* instead of the url (complex-extension convention,
`SliceByTypeProfileEqualityComparer` `:945-976`).

### Choice-type (`value[x]`) matching (`constructChoiceTypeMatch`, `:336-472`)

Both the R5 form (path `value[x]` + sliceName) and the R4 renamed form (`valueString`) are accepted, and
multiple type-specific constraints may follow each other; all are consumed in one go. Each is matched
first to a *renamed base element*, then to a *named type slice* in the base (`value[x]:valueString`), else
added as a new type slice whose base is a clone of the `[x]` element. Two notable behaviors:

- **Slice-name generation mutates the differential:** a type-slice constraint without `sliceName` gets one
  derived from its single type (`valueString`), written **into the caller's differential component**
  (`GENERATE_MISSING_TYPE_SLICE_NAMES`, `:14`, `:318-334`), with an informational issue. The in-code comment
  itself asks "Q: Are we allowed to update the diff itself...?" ([OQ-015](14-open-questions.md#oq-015--the-generator-mutates-its-input-differential)).
- **Type widening throws:** if the diff's type-code set on the `[x]` element is not a subset of the base's,
  the generator throws `InvalidOperation` (`typeIsSubSetOf`, `:406-409`) — the only author error in the
  matcher that escalates to an exception, against the file's own stated policy that the generator "should
  never throw" and leave correctness to the validator (`:158-164`); see
  [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors).
- A diff constraining a choice element by its **original `[x]` name when the base has renamed it** is not
  matched; it becomes New with a warning issue (`constructNew`, `:517-535`).

### Slice bases (`initSliceBase`, `:545-560`)

Slice matches carry a `SliceBase`: a recursive clone of the *unmerged* base element taken before any diff
constraints land, so every named slice regenerates from pristine base content. On that clone, .NET applies
two rules for named slices: the `slicing` component is removed, and **`min` is reset to 0** — rationale in
code: a required slice entry (`min=1`) must still allow optional named slices. Spec basis for the `min`
reset: none found (candidate for the ch6 Phase-2/3 pass). Exception ([#2466], DEV-008): for *extension
header* elements the slicing component is **kept** on the slice base (`initSliceBase(snapNav, false)`,
`:652`). For reslices, the slice base is re-targeted to the matching base named slice (`:716-725`).

### Removed type slices (`Remove`; `checkForRemovedTypes`, `:245-290`)

When a diff constrains the type list on a *sliced* choice element's slice intro, base type slices whose
types no longer appear are matched with action `Remove` and deleted from the snapshot in a second pass —
the matcher-level counterpart of ch5's type-list replace semantics (DEV-001).

## Java behavior (Phase 3 packet J-c, deep-read 2026-09-01)

Code: `ProfileUtilities.java` (PU) `getDiffMatches` `:2444-2489`, `hasInnerDiffMatches` `:2420-2442`,
scope helpers `:2491-2515`, `:2339-2349`; `ProfilePathProcessor.java` (PPP) main loop `:191-235` and
dispatch `:283-305`, `:1196-1223`, `:1723-1736`; unmatched-row handling PU `:842-867`, `:908-948`. All at
commit `b06c7ee`. Full detail with verbatim code in the materials extract
`java-ch04-matching-and-ch02-preprocessing-2026-09-01.md`; what each branch then *does* is ch6/ch7 material.

### Inverted walk: Java walks the base and queries the differential

Where .NET iterates the differential's children and looks each up in the base (forward-only cursor), Java
iterates the **base snapshot** rows in scope and, for each, asks the differential what it says
(`getDiffMatches(differential, currentBasePath, diffCursor, diffLimit, …)`, PPP:204). The query scans the
**entire remaining diff scope** `[diffCursor, diffLimit]` (inclusive) and returns *every* row whose path has
the same depth and matches segment-by-segment (PU:2449-2457) — so a slicing entry and all its slices are
collected in one list regardless of adjacency, and the dispatch below decides what the *set* means. Every
base row in scope is visited exactly once; diff rows are consumed only when a base row pulls them in.
Cursor discipline is admittedly loose: a July-2025 patch advances `diffCursor` by `diffMatches.size()`
whenever a branch consumed matches without moving it (PPP:216-229, comment: "some of the code paths above
don't… pretty difficult… Since this *works*, I'm going with this"), and a self-check of the invariant is
commented out (`checkDiffAssignedAndCursor`, PPP:246-263).

### Path matching rules (`getDiffMatches` + `isSameBase`, PU:2444-2489)

Per segment: string equality, or `isSameBase` — either side ends in `[x]` and the other **starts with its
stem** (PU:2487-2489). Consequences, contrasted with .NET's leaf-only prefix test:

| rule | Java | .NET |
|---|---|---|
| renamed diff row vs `[x]` base (`valueQuantity` ↔ `value[x]`) | matches; the bare stem `value` matches too; suffix not validated as a type | matches when strictly longer than the stem (`ElementDefinitionNavigationFunctions.cs:94`); suffix not validated |
| `[x]` diff row vs **renamed base** (R4-style base snapshot) | **matches** (`isSameBase` is symmetric) | **no match** → New + warning (`constructNew`, `:517-535`) — code-derived deviation, no shared test |
| where the tolerance applies | every path segment, including ancestors | leaf segment only |
| duplicate unnamed rows of one path | both returned; dispatch throws `DIFFERENTIAL_DOES_NOT_HAVE_A_SLICE` unless extension (PPP:313-314) | `Invalid` — dropped with issue |

A commented-out block (PU:2465-2473) once warned `"unknown element '…' (or it is out of order) … (looking
for '…')"`; it was disabled because it misfired on inherited elements, with the note "Might be better done
when we're sorting the profile?" — Java has **no in-walk ordering diagnostic** (see ch2 for
`sortDifferential`, the tooling-side answer).

`hasInnerDiffMatches` (PU:2420-2442) answers "does the diff say anything *below* this base row?" — a child
path, or (for `[x]` rows) any row extending the stem. Its notorious fall-through arm ("not sure why we get
here, but returning false … makes a bunch of tests fail", PU:2438) is simply the `i == start` case where
the window begins at the row itself: skip it, look at what follows. `allowSlices=false` (PPP:377, :1260)
makes a same-path named-slice row terminate the search (rows after it belong to the slice, not the entry);
`allowSlices=true` (PPP:1077, :1658) looks past such rows.

### The dispatch (Java's action space) and its .NET analogue

Unsliced base (`processSimplePath`, PPP:283-305), tested in order:

| `diffMatches` | Java branch | ≈ .NET action |
|---|---|---|
| empty | copy the base row; if `hasInnerDiffMatches` recurse into base children, or step into the type when the base has none (PPP:1061-1130) | base child not in diff → copied; .NET's stand-in parent (ch2) yields the same outcome |
| exactly one, and `oneMatchingElementInDifferential` (PPP:1723-1736): not a renamed row of a `[x]` base (`isImplicitSlicing`), no `slicing` component, not a *named extension slice* | plain **merge** (`processSimplePathWithOneMatchingElementInDifferential`, `updateFromDefinition` at PPP:813) — cursor **jumps** to just after the matched row (PPP:827) | `Merge` |
| `diffsConstrainTypes` (PU:1806-1849; the `size < 2` guard is commented out, so one renamed/typed row suffices) | type slicing (ch6, DEV-020) | `constructChoiceTypeMatch` |
| otherwise | the diff **slices** the base (`processSimplePathDefault`, PPP:307-448): throws for a non-repeating base unless sliced-to-one/type slicing (PPP:309-312) and for a missing slicing entry on a non-extension (PPP:313-314); extension without entry → synthesized `value:url` entry + `SNAPSHOT_auto_added_slicing` (PPP:343-345); each remaining row is then processed as a slice over the same base scope (PPP:430-444) | `Slice` (incl. .NET's implicit extension entry), then `Add` per slice |

Sliced base (`processPathWithSlicedBase`, PPP:1196-1223): empty → copy entry and all slices;
`diffsConstrainTypes` → type slicing over slices (ch6); otherwise `processPathWithSlicedBaseDefault`
(PPP:1225-1482) — slicing-component compatibility throws (PPP:1229-1240), entry merge, then a **lockstep
slice walk** (PPP:1311-1362): base slices in base order, each compared only with the diff row at `diffpos`
by **sliceName equality**; match → recurse into the slice, `diffpos++`; no match → base slice copied
unchanged. Leftover diff rows (PPP:1364-1396): base slicing `closed` → throw (unless `[x]` path); a leftover
whose name equals *any* base slice → throw `NAMED_ITEMS_ARE_OUT_OF_ORDER_IN_THE_SLICE`; else a **new slice**
(template = entry, or the parent slice for `a/b` reslices), `min=0`, merged. So existing slices must appear
in base order and new slices last (the in-code rules at PPP:1203-1207); .NET's forward-only slice cursor
turns an out-of-order existing name into an `Add` instead (code-derived, `matchSlice` `:804-853`).

`sliceIsConstraining` is **never read** anywhere in the Java generator package (grep of PU/PPP/PRE at
`b06c7ee`: zero hits) — Java matches slices by name alone; .NET enforces the flag
([DEV-036](13-deviation-register.md#dev-036--sliceisconstraining-net-enforces-java-ignores-ch4),
[OQ-006](14-open-questions.md#oq-006--sliceisconstraining) Java side answered).

### What happens to a differential row nothing matched — the constraint/specialization split

Java has **no `New` action inside the walk.** A diff row that no base row pulled in is left unmarked
(`SNAPSHOT_GENERATED_IN_SNAPSHOT` absent), and its fate depends on the derivation:

- **Specialization** (PU:842-867, second pass): the row is looked up by exact path among the snapshot's
  trailing rows (`getElementInCurrentContext`, PU:1189-1199); found → merged; not found → **appended** after
  the parent's last child (`findLastChildForParent`, PU:1099-1109 — throws an "internal code error" if the
  parent is absent), and if the diff walks into it with a single type, the type's children are inherited
  (`addInheritedElementsForSpecialization`; multiple types → throw, PU:856-861).
- **Constraint** (every derivation, PU:908-948): each unmarked row is an **orphan** — an ERROR per row
  ("No match found for `<id>` in the generated snapshot: check that the path and definitions are legal in
  the differential (including order)", PU:925) plus one profile-level ERROR (PU:935-947); the row is
  **dropped** from the snapshot. Errors are messages unless `wantThrowExceptions` (default false; then
  `DefinitionException`, PU:1221-1226).

This is the spec's split implemented literally — constraint SDs may not introduce paths [elementdefinition
#path]; specializations list "elements from the baseDefinition … before new elements" [structuredefinition
§5.4.6] — where .NET's universal, issue-less `New` (`createNewElement`, `SnapshotGenerator.cs:887`) applies
neither half. Out-of-order siblings surface the same way: the one-match cursor jump (PPP:827) skips every
row between the cursor and the match for good, so in t23a `males.telecom` (later in the diff, earlier in
the base) is matched and `males.gender` becomes the orphan Java reports — the mechanism behind
[DEV-027](13-deviation-register.md#dev-027--malformed-differentials-produce-silently-corrupt-net-snapshots-ch2)'s
Java side. Whole picture:
[DEV-035](13-deviation-register.md#dev-035--unmatched-and-out-of-order-differential-rows-java-drops-with-error-or-appends-by-derivation-net-silently-creates-new-elements-ch4).

### Removed type slices — inverse trigger

.NET's `Remove` deletes base *type slices* whose types the diff's slicing entry no longer lists. Java's
post-walk sweep (PU:869-881) does the inverse: on a multi-type row, each *type* whose type slice
(`findTypeSlice`, PU:1131-1139) is **prohibited** (`max=0`) is removed from the type list. Same intent
(keep slices and type list consistent), opposite direction of inference; neither is spec text.

## Deviations
- [DEV-008](13-deviation-register.md#dev-008--extension-header-slicing-element-ch6) extension header keeps
  its slicing component on the slice base.
- [DEV-027](13-deviation-register.md#dev-027--malformed-differentials-produce-silently-corrupt-net-snapshots-ch2)
  out-of-order differential: Java orphan ERROR (cursor-jump mechanism above) vs .NET corrupt output.
- [DEV-035](13-deviation-register.md#dev-035--unmatched-and-out-of-order-differential-rows-java-drops-with-error-or-appends-by-derivation-net-silently-creates-new-elements-ch4)
  unmatched rows: Java drops + ERROR (constraint) / appends (specialization) vs .NET silent `New`; includes
  the `[x]`-vs-renamed-base matching asymmetry and the out-of-order-existing-slice behavior.
- [DEV-036](13-deviation-register.md#dev-036--sliceisconstraining-net-enforces-java-ignores-ch4)
  `sliceIsConstraining`: .NET enforces, Java never reads it.

## Open questions
- [OQ-006](14-open-questions.md#oq-006--sliceisconstraining) sliceIsConstraining in matching (.NET side
  answered 2026-08-24 — enforced; Java side answered 2026-09-01 — ignored).
- [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors) inconsistent error
  taxonomy: throw vs discard-with-issue vs silent New — Java rows added 2026-09-01 (orphan drop+ERROR,
  matcher throws).
- [OQ-015](14-open-questions.md#oq-015--the-generator-mutates-its-input-differential) generator mutates the
  input differential (generated type-slice names) — Java side answered 2026-09-01 (works on a clone, ch2).
