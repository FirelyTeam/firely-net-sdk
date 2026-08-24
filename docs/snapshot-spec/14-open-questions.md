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
.NET ignores it (`ElementMatcher.cs:602` TODO).
- **Status:** open.

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
