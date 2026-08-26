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
- Packet 4: the entry point is the root-only type-profile merge — a bare `type.profile` reference merges the
  external root's constraints (incl. cardinality) onto the element even when the diff has no children
  (`SnapshotGenerator.cs:1464-1476`, ch7).
- Related: the general priority question OQ-002.
- **Status:** open.

## OQ-002 — Priority: type-profile constraints vs base constraints
When an element's type carries a profile, and both the base element and that type profile constrain the same
property, who wins? `SnapshotGenerator.cs:999-1003` records "Ewout: not defined yet, under discussion; use
cases exist for both options" — .NET gives type profiles priority over base; merge order is: expand external
type profile → merge base snapshot constraints on top → merge differential on top (`SnapshotGenerator.cs:1383`).
Does Java use the same order? What *should* the order be?
- Packet 4 (mechanism): "type beats base" is not a per-property policy but an artifact of merging the
  external profile's rebased **snapshot** onto the working element *as if it were a differential*
  (`mergeElement(snap, typeNav)`, `SnapshotGenerator.cs:1400-1413`) — a snapshot has a value for nearly
  every property, so it overrides wherever it differs. The in-code ISSUE at `:1405-1411` acknowledges a
  concrete wrongness: when the *base profile* had already constrained the type's children, the external type
  snapshot re-applies original type values over the base's overrides
  ("{Address Snap + Diff + Address Snap (WRONG!) + MyAddress Diff}"). So .NET's answer is not even
  consistently "type wins" — it degrades with derivation depth. Ch7.
- **Status:** open.

## OQ-003 — Slicing non-repeating elements
May a profile slice an element with `max = 1` (outside the choice-type case)? .NET has a disabled reject
(`REJECT_SLICE_NONREPEATING_ELEMENT`, `SnapshotGenerator.cs:1794-1796`) with the disagreement preserved
inline (Ewout: no reason to reject, e.g. a derived profile can limit a sliced base element's cardinality to
0..1; Vadim: may break code generators).
- Packet 5 (2026-08-26): confirmed the reject is the *only* sliceability check that ever existed — with it
  compiled out, `startSlice` slices any element without issue (ch6). Remaining question is what the spec
  intends ("Slicing is only allowed when … on the first repetition of an element" [profiling §5.1.0.13]
  presupposes repetition without stating a rule for `max = 1`) and what Java does.
- **Status:** open — .NET side answered.

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
- Packet 5 (2026-08-26): verified exhaustively — `slicing.rules` and `slicing.ordered` are **never read**
  anywhere in the generator/matcher/merger; the only writes are the two synthesized entries' defaults
  (`SnapshotGenerator.cs:1968-1969,2163-2164`). So the whole §5.1.0.17 constraint lattice (open→closed,
  ordered false→true, derived slicing SHALL repeat base discriminators) is unenforced, along with §5.1.0.14
  slice-cardinality sums and the `@default`-requires-closed precondition (`@default` is unknown to the
  generator, ch6). Extends the OQ: does the *generator* have any obligation here, or is this all
  validator territory?
- **Status:** open — .NET side answered.

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
- Packet 6 (2026-08-26, ch10): .NET side fully answered — with default settings ids are **always
  canonical**: base ids never inherited, custom differential ids merged (`:1052`) and then deliberately
  overwritten by force-regeneration (`:1063-1066`: "Ignore user-specified element id's in the
  differential"). Author ids survive only with `GenerateElementIds = false` (which disables generation
  entirely). Remaining question: is that sanctioned? The spec says ids "may be used as the target of
  external references" — regeneration breaks author-chosen ids external references may rely on; does Java
  preserve them?
- **Status:** open — .NET side answered.

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
  (`SnapshotGenerator.cs:604-618`);
- type-profile expansion (packet 4, ch7): several distinct type profiles on one element → external merge
  **silently skipped**, no issue (`SnapshotGenerator.cs:1243-1249`); unresolvable or type-incompatible
  external profile → **issue + continue** without merge (`:1306-1322`); external profile whose snapshot
  cannot be ensured → issue (from `ensureSnapshot`) + the element's **children silently dropped**
  (`:1329-1332` → `:1079-1082`); complex-reference jump failure → issue + children dropped (`:1364-1368`,
  though see DEV-018: the path throws before reaching that); cyclic `baseDefinition` chain in the
  compatibility walk → **throws** (`:2559-2564`);
- slicing (packet 5, ch6): an explicit slicing entry on an *extension* element is merged shallowly
  (`mergeElementDefinition`, `SnapshotGenerator.cs:1855-1866`) — any children the differential put on that
  entry are **silently dropped**, no issue; internal slice-insertion failures in `addSliceBase` **throw**
  (`:2057-2079`);
- structure-level (packet 6, ch12): SD without `url`, constraint SD without `baseDefinition`, missing
  `type` on a non-logical SD → **throw** (`SnapshotGenerator.cs:171-174,372-375,490-493`); the same missing
  `type` on a *logical* SD → **warning + repair** (root parsed from first diff element, ch9); an element
  with no Base match anywhere up the base chain → **silently no Base component**
  (`SnapshotBaseComponentGenerator.cs:123-124`, ch10), leaving sdf-8b unmet without an issue.
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

## OQ-017 — Starting expansion below the root: extension vs fragment syntax
R5 documents the `elementdefinition-profile-element` extension (on `type.profile`) nominating an element
**id** in the target SD as "an instruction to a validator to apply the profile starting at the nominated
element" [profiling §5.1.0.16] — for a generator, the rebase point (ch7 spec baseline). .NET's generator
**never reads that extension** (no occurrence anywhere in generator code, verified 2026-08-24); its only
below-root mechanism is the older `url#name` **fragment** syntax (`ProfileReference`), and the expansion
path for *that* appears broken (DEV-018 — bare-name jump throws; id-vs-name mismatch). Questions:
- Must a generator support the extension, the fragment syntax, or both? Is the fragment syntax sanctioned
  at all in `type.profile` (canonical fragments normally address contained resources)?
- Does the fragment name an element **id** or a **slice name**? (.NET itself is torn: the expansion path
  passes the bare fragment name into an id-matching jump — the DEV-018 mismatch
  (`SnapshotGenerator.cs:1364`) — while the no-expansion path compares the fragment to the **sliceName**,
  `:1480-1485`.)
- Does Java's generator honor the extension (it originated there), the fragment, or both?
- **Status:** open (Phase 2 packet 4, 2026-08-24).

## OQ-018 — Implicit type constraint only for the renamed form
Does a type slice *imply* a type constraint, or must the constraint be stated? The two syntactic forms mean
the same thing — R4's renamed path (`valueString`) and R5's `value[x]` + `sliceName` — but .NET treats them
differently when the slice states **no explicit `type`**:
- renamed form: the type is parsed from the rename and the slice's type list is reduced to that single type
  (`applyImplicitChoiceTypeConstraint`, `SnapshotGenerator.cs:2022-2049`, gated on `isRenamed` at `:1981`;
  added for issue #1074);
- R5 form: no implicit constraint — the slice inherits the base's **full choice list**, so
  `value[x]:valueString` without a `type` is a "string slice" whose snapshot still allows every type.
Code-derived (all vendored R5-form tests state explicit types); verify empirically in the Phase-4 harness,
and check Java. Spec side: R5 [elementdefinition #typesx] says a type-specific element "constrains the use
of a particular type" — arguably that *is* an implied type constraint, which would make the R5-form
behavior wrong; prime WGM material since it decides what a bare type slice means.
- **Status:** open (Phase 2 packet 5, 2026-08-26).

## OQ-019 — Which extensions are non-inheritable?
A derived profile's snapshot inherits everything from the base — including metadata extensions that are
plainly *about the base*, not the derivation: maturity level (`structuredefinition-fmm`),
`-standards-status`, `-normative-version`, `-wg`, `-interface`, `resource-approvalDate`, etc. .NET strips a
**hard-coded blocklist of 17 core extension urls** from all inherited snapshot content
(`SnapshotGeneratorExtensions.cs:137-156`, applied at `SnapshotGenerator.cs:523,1578`; ch12) — without it,
every derived snapshot would claim its base's maturity/WG/normative status. The spec never mentions
extension-inheritance policy anywhere. Questions: is stripping sanctioned at all (snapshots are supposed to
be *complete*)? Which extensions? Does Java maintain an equivalent list, and do the lists agree? (Related:
the `elementdefinition-suppress` mechanism, OQ-008, is the *author-controlled* variant of the same
concern; this is generator-hardcoded.) Candidate RFC: the spec (or extensions pack) should mark extensions
as inheritable/non-inheritable.
- **Status:** open (Phase 2 packet 6, 2026-08-26).
