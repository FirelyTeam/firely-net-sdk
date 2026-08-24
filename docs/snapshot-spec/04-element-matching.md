# 4. Element matching

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 2,
> 2026-08-24: `ElementMatcher.cs` deep-read). Java section pending (Phase 3).

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

## Java behavior (Phase 3)
*(pending — `ProfilePathProcessor.java`)*

## Deviations
- [DEV-008](13-deviation-register.md#dev-008--extension-header-slicing-element-ch6) extension header keeps
  its slicing component on the slice base.

## Open questions
- [OQ-006](14-open-questions.md#oq-006--sliceisconstraining) sliceIsConstraining in matching (.NET side
  answered 2026-08-24 — enforced, not ignored).
- [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors) inconsistent error
  taxonomy: throw vs discard-with-issue vs silent New.
- [OQ-015](14-open-questions.md#oq-015--the-generator-mutates-its-input-differential) generator mutates the
  input differential (generated type-slice names).
