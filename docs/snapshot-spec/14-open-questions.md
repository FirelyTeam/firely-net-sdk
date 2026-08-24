# 14. Open questions

Questions the published spec does not answer (or answers ambiguously), harvested from code archaeology, the
deviation register, and spec reading. Each will be swept against Zulip/JIRA history before being asked anew.
The subset that remains open by **2026-09-14** is frozen into the **WGM question brief** for the in-person
session at the HL7 WGM (~2026-09-21).

**Status values:** `open` · `answer-found` (cite Zulip/JIRA/spec) · `settled` (decision recorded) ·
`superseded`.

---

## OQ-001 — The cardinality diamond problem
A non-repeating element (`0..1`) is typed with a profiled datatype whose *root* element says `0..*`, and the
differential doesn't constrain `max`. Which cardinality does the snapshot element get?
- .NET today: the type profile's root wins → the element silently *becomes repeating*
  (`ElementDefnMerger.cs:689-695`, documented as unresolved).
- Related: the general priority question OQ-002.
- **Status:** open.

## OQ-002 — Priority: type-profile constraints vs base constraints
When an element's type carries a profile, and both the base element and that type profile constrain the same
property, who wins? `SnapshotGenerator.cs:999-1003` records "Ewout: not defined yet, under discussion; use
cases exist for both options" — .NET gives type profiles priority over base; merge order is: expand external
type profile → merge base snapshot constraints on top → merge differential on top (`SnapshotGenerator.cs:1383`).
Does Java use the same order? What *should* the order be?
- **Status:** open.

## OQ-003 — Slicing non-repeating elements
May a profile slice an element with `max = 1` (outside the choice-type case)? .NET has a disabled reject
(`REJECT_SLICE_NONREPEATING_ELEMENT`, `SnapshotGenerator.cs:1794-1796`) with the disagreement preserved
inline (Ewout: no reason to reject; Vadim: may break code generators).
- **Status:** open.

## OQ-004 — contentReference + constraining children
After dereferencing a `contentReference` to constrain its children, which value-domain properties of the
referenced element meaningfully apply (`fixed`/`pattern`/`defaultValue`/`example`/`minValue`/`maxValue`/
`maxLength`/`binding`)? See DEV-009 / issue #3177 — .NET currently copies none, with reasoning in comments
only.
- **Status:** open.

## OQ-005 — Enforcing slicing.rules = closed / openAtEnd
Should the *generator* enforce or validate `closed`/`openAtEnd` (e.g., reject a differential that appends a
slice to a closed slicing, or reorder for openAtEnd)? .NET does not (file-header TODO,
`SnapshotGenerator.cs:20-23`).
- **Status:** open.

## OQ-006 — sliceIsConstraining
What must a generator do with `sliceIsConstraining` when matching a derived profile's slices to base slices?
- Phase 2 correction (2026-08-24): the Phase-1 claim ".NET ignores it (`ElementMatcher.cs:602` TODO)" was
  **wrong** — `:600-623` is dead `#if false` code. Live code in `matchSlice` (`ElementMatcher.cs:816-838`)
  **enforces** it: a non-null value disagreeing with the actual name match → `MatchAction.Invalid`, issue
  emitted, element discarded; absent → STU3 fallback (match ⇒ constrain, no match ⇒ new slice).
- Remaining question: does Java enforce it the same way, and is *discard-with-issue* the sanctioned
  response (vs error, vs proceeding)? Feeds [OQ-014](#oq-014--inconsistent-error-taxonomy-for-author-errors).
- **Status:** open — .NET side answered.

## OQ-007 — Global StructureDefinition.mapping
Should the profile-level `mapping` declarations of base/type profiles be merged into the derived
StructureDefinition (so element-level `mapping.identity` references resolve)? .NET does not merge them
(file-header TODO).
- **Status:** open.

## OQ-008 — Verbosity of generated snapshots
profiling.html states tools "generate complete verbose snapshots; they do not support suppressing mappings or
constraints" — yet the `elementdefinition-suppress` extension exists and .NET honors it for mappings/examples
(`RespectSuppressExtension`). What is the sanctioned behavior?
- **Status:** open.

## OQ-009 — Element id stability
Are element ids in a generated snapshot always regenerated from path+sliceName, or can/should ids from the
base or differential survive? .NET force-regenerates (`ElementIdGenerator.Update(..., force: true)`) and had
to disable a "correct-looking" clear because it broke `Questionnaire.item.item`
(`SnapshotGenerator.cs:430-438`).
- **Status:** open.

## OQ-010 — The "..." append convention
.NET supports prefixing `definition`/`comment`/`requirements` text with `"..."` in a differential to mean
*append to inherited text* (`ElementDefnMerger.cs:117-119`). Where is this specified, and does Java implement
the same convention for the same properties?
- Phase 1 finding (2026-08-21): **verified absent from the spec** — the convention appears nowhere on the
  R5 elementdefinition/profiling pages. It is pure tooling convention; origin and Java behavior pending
  (Phase 3), then this likely graduates to an RFC (document or drop the convention).
- **Status:** open — spec side answered; Java side pending.

## OQ-011 — What must a generator enforce?
The .NET merger is diff-wins for nearly every property — including every rule the spec marks as frozen or
one-directional: `isModifier`/`isSummary`/`defaultValue[x]`/`meaningWhenMissing` (†-rules), `representation`
(frozen per elementdefinition-definitions prose — the interpretation table has no row for it, RFC-007),
`mustSupport` `true`→`false`, silent loosening of `binding.strength` against the §5.1.0.21 lattice. The
*only* most-restrictive enforcement is `min`/`max` — and there an illegally loosening differential is
**silently ignored** instead of reported (`ElementDefnMerger.cs:666,696`). Is a generator expected to
enforce, report, or ignore illegal differentials? Enforcement asymmetries like .NET's produce snapshots
whose provenance can't be reconstructed. (Related: RFC-012 — a normative generator contract would answer
this.)
- **Status:** open (Phase 2 packet 1, 2026-08-24).

## OQ-012 — Partial overlay of fixed[x]/pattern[x] values
.NET merges a differential `fixed[x]`/`pattern[x]` (and `defaultValue[x]`, `minValue[x]`/`maxValue[x]`) by
*top-level property overlay* when the diff value's type equals or derives from the base value's type
(`mergeComplexAttribute`, `ElementDefnMerger.cs:760-793`): non-null diff properties win, everything else is
inherited from the base value. A base `patternCodeableConcept` with `text` + `coding` merged with a diff
supplying only `coding` yields a pattern combining diff `coding` with base `text` — a value **neither
profile stated**. Should a differential fixed/pattern replace the inherited value wholesale? Does Java
overlay or replace? (Spec is silent — ch5 "compatibility with an inherited base fixed/pattern is unstated".)
- **Status:** open (Phase 2 packet 1, 2026-08-24).

## OQ-013 — Meaning of merging ElementDefinition.modifierExtension
.NET merges `modifierExtension` between base and differential elements like ordinary extensions, matched by
url (`ElementDefnMerger.cs:57-59`), with the question preserved in code: "Q: What does this mean? How should
consumers handle these?" — a modifier extension *inherited into* a snapshot element changes the meaning of a
definition the deriving author may never have seen. Is inheriting modifier extensions into snapshots even
sanctioned?
- **Status:** open (Phase 2 packet 1, 2026-08-24).

## OQ-014 — Inconsistent error taxonomy for author errors
What should a generator do with an *illegal differential*? .NET's matcher answers differently per error:
- illegal choice-type **widening** → throws `InvalidOperation` (`ElementMatcher.cs:406-409`) — contradicting
  the in-code policy that the generator "should never throw" and leave correctness to the validator
  (`ElementMatcher.cs:158-164`);
- slice-name / `sliceIsConstraining` conflicts, unnamed slices under unsupported discriminators → element
  **discarded** with an issue (`MatchAction.Invalid`, `SnapshotGenerator.cs:841-843`);
- an unmatched differential path (illegal in a constraint profile per [elementdefinition #path]) →
  **silently** added as a New element (`createNewElement` emits no issue, `SnapshotGenerator.cs:887`);
- non-choice type widening isn't checked at all (ch5: `mergeElementTypes` replaces the list wholesale);
  min/max loosening is silently ignored (OQ-011);
- preprocessing (packet 3): an element without a path, or a root element that is not first, →
  **throws** (`DifferentialTreeConstructor.cs:62-78`); out-of-order differentials → debug-build-only
  warnings, silently degrading in matching; an illegal root `sliceName` → **repaired with an issue**
  (`SnapshotGenerator.cs:604-618`).
One taxonomy — throw / drop-with-issue / repair-with-issue / silent repair / silent accept — chosen
different ways per error class. The matcher-side sibling of [OQ-011](#oq-011--what-must-a-generator-enforce).
- **Status:** open (Phase 2 packets 2–3, 2026-08-24).

## OQ-015 — The generator mutates its input differential
With `GENERATE_MISSING_TYPE_SLICE_NAMES` active, a type-slice constraint lacking a `sliceName` gets one
generated and written **into the caller's differential component**, not just the snapshot
(`ElementMatcher.cs:13-14,318-334`; the in-code comment asks "Q: Are we allowed to update the diff
itself...?"). Is a generator permitted to repair/normalize the differential it was handed? Related:
Java's CLI oracle runs with `autoFixSliceNames(true)` (DEV-016) — same repair, but behind a flag.
- Packet 3 addition: the mechanism is structural — `MakeTree` returns a new *list* but shares the element
  *instances* with the caller's differential (`DifferentialTreeConstructor.cs:48-51`), so any generator
  repair (type-slice names, root-sliceName clearing at `SnapshotGenerator.cs:604-618`) lands in the caller's
  StructureDefinition, except when the touched element is a generator-synthesized stand-in parent.
- **Status:** open (Phase 2 packet 2, 2026-08-24).

## OQ-016 — What does a differential-less StructureDefinition mean?
The spec never states whether a StructureDefinition without a differential means "snapshot = base snapshot"
(ch3 spec gap 3). .NET itself answers twice, differently: full generation synthesizes an empty differential
and proceeds — snapshot = rebased base snapshot + generator fill (`SnapshotGenerator.cs:362-369`); but root
resolution for *type profiles* rejects the same SD with an issue ("profile has no differential",
`SnapshotGenerator.cs:2391-2396`), so a differential-less SD cannot be used as a type profile whose root
merges into a referencing element. The in-code TODO ("Handle empty diff (=> return root element of base
profile)") shows the authors consider this a gap, not a design decision. What is the sanctioned meaning —
and does Java accept differential-less SDs in both roles?
- **Status:** open (Phase 2 packet 3, 2026-08-24).
