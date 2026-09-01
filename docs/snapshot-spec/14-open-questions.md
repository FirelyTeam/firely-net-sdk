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
- **Java side (J-d, 2026-09-01):** the diamond **cannot arise for datatype profiles** — a datatype profile's
  root is never merged (the base row is the template, `PPP:775-778`; the doc-override discards non-Extension/
  Resource profiles, `PU:2643-2648`), so the element keeps the base's `0..1` and the profile root's `0..*` is
  invisible in the snapshot (the validator sees it through `type.profile`). For **extensions** the profile root
  *is* the template (`PPP:763-772`), so an extension definition's root `max` flows into the slice unless the
  diff states one, then capped to the slicer's max (`PPP:816-818`); its root `min` is overwritten to 0 for open
  slicings (`PPP:801-805`). New slices under an already-sliced base get the R5 root-cardinality rule applied
  one-directionally: `min` raised / `max` lowered toward the profile root, never loosened (`PPP:1398-1420`). So
  where .NET's answer is "type root wins, even loosening", Java's is "datatypes: base wins; extensions: root
  wins unless the slicing is open; sliced base: tighten only" —
  [DEV-038](13-deviation-register.md#dev-038--type-profile-scope-net-merges-any-type-profile-java-only-extensionresource-roots-and-only-when-the-base-has-no-children-ch7).
- **Status:** open (Java side 2026-09-01).

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
- **Java side (J-d, 2026-09-01): base wins.** Java opens a type profile's snapshot for children only when the
  base snapshot has **no children** at that element (`!baseWalksInto` `PPP:829`/`1423`, `baseHasChildren`
  `PPP:1080`/`1672`); if a
  parent profile already expanded the element, the walk continues over the base children and the type
  profile's child constraints are never merged — the profile is left to the validator via `type.profile`. When
  the profile snapshot *is* opened, its children become the base rows for the diff's child constraints
  (diff > profile), and the profile root is merged only for Extension/Resource types (OQ-001). So the two
  engines answer the question **oppositely** (.NET: type > base; Java: base > type, profile consulted only to
  fill a void), and Java's answer has the merit of never producing the "WRONG!" re-application .NET documents —
  at the cost of dropping the profile's constraints from the snapshot entirely
  ([DEV-038](13-deviation-register.md#dev-038--type-profile-scope-net-merges-any-type-profile-java-only-extensionresource-roots-and-only-when-the-base-has-no-children-ch7)).
  WGM framing: is a snapshot required to *close over* type profiles at all, or is `type.profile` a validator
  instruction (cf. OQ-021)?
- **Status:** open (Java side 2026-09-01).

## OQ-003 — Slicing non-repeating elements
May a profile slice an element with `max = 1` (outside the choice-type case)? .NET has a disabled reject
(`REJECT_SLICE_NONREPEATING_ELEMENT`, `SnapshotGenerator.cs:1794-1796`) with the disagreement preserved
inline (Ewout: no reason to reject, e.g. a derived profile can limit a sliced base element's cardinality to
0..1; Vadim: may break code generators).
- Packet 5 (2026-08-26): confirmed the reject is the *only* sliceability check that ever existed — with it
  compiled out, `startSlice` slices any element without issue (ch6). Remaining question is what the spec
  intends ("Slicing is only allowed when … on the first repetition of an element" [profiling §5.1.0.13]
  presupposes repetition without stating a rule for `max = 1`) and what Java does.
- **Java side answered (packet J-a, 2026-08-31):** Java **rejects** slicing a non-repeating element
  (`Attempt to a slice an element that does not repeat…`, `ProfilePathProcessor.java:309-312`) — with two
  carve-outs that mirror both sides of the .NET in-code debate: the intro itself capped to `max=1` ("the
  sum total of your slices is limited to 1" — exactly Ewout's derived-profile-limits-to-0..1 case) or type
  slicing. So the engines disagree only on the *unexcused* case, and Java's carve-outs suggest the WGM
  question should be "which exceptions, not whether".
- **Status:** open — both implementation sides answered.

## OQ-004 — contentReference + constraining children
After dereferencing a `contentReference` to constrain its children, which value-domain properties of the
referenced element meaningfully apply (`fixed`/`pattern`/`defaultValue`/`example`/`minValue`/`maxValue`/
`maxLength`/`binding`)? See DEV-009 / issue #3177 — .NET currently copies none, with reasoning in comments
only.
- **Java side (J-e, 2026-09-01):** copies none either — `replaceFromContentReference` (`PU:1870-1874`) moves
  only `type`. Agreement by omission; the question is purely spec-side (should eld-5's prohibition be
  restated as "these properties are undefined after dereferencing"?).
- **Status:** open, spec-side only (Java side 2026-09-01).

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
- **Java side answered (packet J-a, 2026-08-31):** Java enforces a *partial* lattice, but only when the
  differential restates the slicing entry over a sliced base (ch6): `ordered` may not change in either
  direction (stricter than §5.1.0.17's false→true allowance!), base discriminators must be an
  order-sensitive prefix of the diff's, and `ruleMatches` allows anything over OPEN but also tolerates
  **CLOSED→OPENATEND — a loosening §5.1.0.17 does not sanction**; the rules check is skipped entirely for
  choice elements. Appending a slice to a closed base slicing throws — except on `[x]` paths. Slice-min
  sums ARE checked post-generation (`PU:976-1036`) but the outcome depends on provenance: auto-added
  entries get silently rewritten, authored ones get a message that is an ERROR only `forPublication`.
  `@default` and openAtEnd *ordering* are unenforced in both engines. So neither engine implements
  §5.1.0.17 as written — Java approximates it with deviations in both directions.
- **Status:** open — both implementation sides answered.

## OQ-006 — sliceIsConstraining
What must a generator do with `sliceIsConstraining` when matching a derived profile's slices to base slices?
- Phase 2 correction (2026-08-24): the Phase-1 claim ".NET ignores it (`ElementMatcher.cs:602` TODO)" was
  **wrong** — `:600-623` is dead `#if false` code. Live code in `matchSlice` (`ElementMatcher.cs:816-838`)
  **enforces** it: a non-null value disagreeing with the actual name match → `MatchAction.Invalid`, issue
  emitted, element discarded; absent → STU3 fallback (match ⇒ constrain, no match ⇒ new slice).
- **Java side answered (J-c, 2026-09-01): Java never reads the property.** The string `sliceIsConstraining`
  does not occur anywhere in the generator package (`ProfileUtilities`/`ProfilePathProcessor`/
  `SnapshotGenerationPreProcessor` at `b06c7ee`); slices are matched to base slices by **name equality
  alone** in a lockstep walk (`PPP:1311-1362`), and a diff slice whose name matches no base slice is simply a
  new slice (`PPP:1370-1396`) — exactly .NET's *absent-flag* fallback, applied unconditionally. So `true`
  without a base match is silently a new slice in Java (an `Invalid` + drop in .NET), and `false` with a
  base match is silently a constraint in Java (`Invalid` + drop in .NET). Register entry:
  [DEV-036](13-deviation-register.md#dev-036--sliceisconstraining-net-enforces-java-ignores-ch4).
- Remaining question for the spec side: the property is Trial Use and its definition text ("If set to true,
  an ancestor profile SHALL have a slicing definition with this name") reads like a validation rule — is a
  *generator* expected to enforce it (and how: reject vs proceed), or is it a validator/renderer hint only?
  Feeds [OQ-014](#oq-014--inconsistent-error-taxonomy-for-author-errors).
- **Status:** open — both implementation sides answered (they disagree).

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
- **Java side (J-e, 2026-09-01): no — agreement.** `setIds(derived, false)` (`PU:886`) regenerates every id
  from path + enclosing slice names at the end of generation (`generateIds`, `PU:4310-4364`), overwriting
  author ids without a message — and, because `checkFirst` is false, it rewrites the **caller's differential
  ids too**. Both engines thus treat ids as derived data; the only inter-engine difference is cosmetic
  (Java maps `_` → `-` in id segments, `fixChars` `PU:4375`; .NET keeps the path characters). The remaining
  question is spec-side: is discarding author ids sanctioned given ids "may be used as the target of external
  references"?
- **Status:** open, spec-side only — both implementations answered (regenerate).

## OQ-010 — The "..." append convention
.NET supports prefixing `definition`/`comment`/`requirements` text with `"..."` in a differential to mean
*append to inherited text* (`ElementDefnMerger.cs:117-119`). Where is this specified, and does Java implement
the same convention for the same properties?
- Phase 1 finding (2026-08-21): **verified absent from the spec** — the convention appears nowhere on the
  R5 elementdefinition/profiling pages. It is pure tooling convention; origin and Java behavior pending
  (Phase 3), then this likely graduates to an RFC (document or drop the convention).
- **Java side answered (J-b, 2026-09-01):** Java implements the identical convention for
  `definition`/`comment`/`requirements` (`Utilities.appendDerivedTextToBase` = base + CRLF + diff minus the
  3-char marker — byte-identical to .NET, `mergeMarkdown` PU:3134); Java *additionally* attempts it on
  `label` but with swapped operands (broken — DEV-034(j)). Both engines agree the convention exists and how
  it composes; the spec still doesn't. Graduation to an RFC now justified (both-implementation precedent).
- **Status:** open for the spec question only — both implementation sides answered.

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
- **Java data point (J-b, 2026-09-01):** Java answers the question with a *third* posture — **warn-and-take**:
  illegal min/max/mustSupport/mustHaveValue diffs raise an ERROR `ValidationMessage` but the illegal value
  still lands in the snapshot (PU:2757-2892); the frozen rules are enforced by **silent omission** (isModifier
  outside extensions, defaultValue/meaningWhenMissing/representation never merged); `isSummary` alone is
  enforced by **hard throw** (generation aborts, PU:3042); binding gets the required-strength row plus an
  expansion-based subset check (ERRORs); type derivation is checked (throw/ERROR). So the two engines
  disagree not just on *what* to enforce but on *how* — silent-keep-base (.NET min/max) vs warn-take-diff
  (Java) vs silent-drop-diff (Java frozen props) vs abort (Java isSummary) — four postures, DEV-034(b)/(c).
  Prime WGM exhibit for the generator-contract question.
- **Status:** open (Phase 2 packet 1, 2026-08-24; Java side documented J-b 2026-09-01).

## OQ-012 — Partial overlay of fixed[x]/pattern[x] values
.NET merges a differential `fixed[x]`/`pattern[x]` (and `defaultValue[x]`, `minValue[x]`/`maxValue[x]`) by
*top-level property overlay* when the diff value's type equals or derives from the base value's type
(`mergeComplexAttribute`, `ElementDefnMerger.cs:760-793`): non-null diff properties win, everything else is
inherited from the base value. A base `patternCodeableConcept` with `text` + `coding` merged with a diff
supplying only `coding` yields a pattern combining diff `coding` with base `text` — a value **neither
profile stated**. Should a differential fixed/pattern replace the inherited value wholesale? Does Java
overlay or replace? (Spec is silent — ch5 "compatibility with an inherited base fixed/pattern is unstated".)
- **Java side answered (J-b, 2026-09-01):** Java **replaces wholesale** — `base.setFixed(derived.getFixed()
  .copy())`, no overlay of any kind (PU:2779-2796) — and then validates the value's type against the
  element's post-merge type list (`checkTypeOk` PU:3121-3126, ERROR message). So .NET's partial overlay is
  the outlier; .NET can synthesize values neither profile stated, Java cannot. (Java's *additional-base*
  pathway does have a recursive fixed-vs-pattern compatibility merge, PRE:431-453 — a different mechanism
  for a different input.) DEV-034(g).
- **Status:** open for the spec question; both implementation sides answered (2026-09-01).

## OQ-013 — Meaning of merging ElementDefinition.modifierExtension
.NET merges `modifierExtension` between base and differential elements like ordinary extensions, matched by
url (`ElementDefnMerger.cs:57-59`), with the question preserved in code: "Q: What does this mean? How should
consumers handle these?" — a modifier extension *inherited into* a snapshot element changes the meaning of a
definition the deriving author may never have seen. Is inheriting modifier extensions into snapshots even
sanctioned?
- **Java data point (J-b, 2026-09-01):** Java never merges ED-level `modifierExtension` at all —
  `updateFromDefinition` + `updateExtensionsFromDefinition` handle only `extension` (verified: zero
  `ModifierExtension` references in PU:2585-3217). A diff-supplied modifier extension on an
  ElementDefinition is silently dropped; whatever the base clone carried stays.
- **Status:** open (Phase 2 packet 1, 2026-08-24; Java side verified J-b 2026-09-01).

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
- **Phase-4 aggregate exhibit (packet 3, 2026-08-26):** across the suite's 21 `fail="true"` tests, Java
  satisfies the fail expectation on **21/21**, .NET on **8/21** — it silently generates on 13, and on two
  of those the output is *corrupt*, not merely unvalidated (duplicate element id + fabricated `base.min`
  on an out-of-order diff; phantom element + silently dropped `fixedString` on a `..` path) —
  [DEV-027](13-deviation-register.md#dev-027--malformed-differentials-produce-silently-corrupt-net-snapshots-ch2).
  The nine absent checks are catalogued in
  [DEV-028](13-deviation-register.md#dev-028--author-error-detection-catalogue-java-validates-net-emits-as-written-ch2ch6-ch9-ch12).
  Taxonomy-quality data point: where both sides *do* throw for the same author error (obs-1-2/obs-2-3/
  obs-3), Java's exception names the offending and allowed types while .NET reports "**Internal
  error** in snapshot generator" (`InvalidOperationException`). (Correction 2026-09-01: t37 does *not*
  belong in that clause — its Java failure is the driver's *sort* step, "Sort failed: counts differ; at
  least one of the paths in the differential is illegal", which names nothing; only without the sort step
  would `checkDifferential` throw the path-naming "must start with MedicationRequest" error.) The WGM
  question sharpens to: which author errors is a generator *required* to detect, and is "reject, repair,
  or propagate — but never corrupt" an acceptable floor?
- **Java rows (J-c, 2026-09-01; ch2/ch4 Java sections):** Java's taxonomy is narrower but not uniform
  either — **throw** (`FHIRException`/`DefinitionException`/`java.lang.Error`): every path-grammar failure
  (`checkDifferential`, `PU:1413-1461`), root `type` on a non-logical SD (`PU:1322`, a `java.lang.Error`),
  slicing a non-repeating element, slicing without an entry on a non-extension (`PPP:309-314`), existing
  slices out of order or a new slice before an existing one (`NAMED_ITEMS_ARE_OUT_OF_ORDER_IN_THE_SLICE`,
  `PPP:1373-1375`), extending a `closed` slicing (`PPP:1364-1369`), a sparse parent with neither type nor
  contentReference (`PPP:1094-1096`; the neighbouring multi-type throw at `PPP:1115-1117` is dead code — its
  guard iterates an empty list — so a multi-type sparse parent silently expands against `Element` and its
  children orphan instead), plus two internal-state throws that leak as author-facing errors ("This situation is
  not yet handled … please report issue to grahame@fhir.org", `PPP:382`; "Unable to find parent path …
  (internal code error)", `PU:1103`); **ERROR message, row dropped** (a throw only when
  `setThrowException(true)`): every differential row nothing matched — out-of-order siblings included
  (`PU:908-948`); **silent**: ordering violations whose row still gets matched later; unknown paths in
  `sortDifferential` sort to the front with the error recorded only in debug mode (`PU:3916-3918`);
  `sliceIsConstraining` ignored (OQ-006). Java has **no silent-New and no repair-with-issue** classes for
  constraint profiles; .NET has no throw class for path grammar. Neither implementation's taxonomy is
  derivable from the spec.
- **Java rows, type expansion + base resolution (J-d, 2026-09-01; ch7/ch3 Java sections):** **throw**:
  type profile incompatible with the element type (`Validation_VAL_Profile_WrongType2`, `PPP:716`); walking
  into an unsliced multi-type element (`_has_children__and_multiple_types…`) — but only in three of five
  code paths, the two commonest silently expand against `Element` (JI-17); no type and no contentReference on a
  walked-into element; unresolvable type SD; xver url with Bad/Invalid/Unknown status (`FHIRException`);
  `check-profile-version` mismatch (`FHIRException`); constraint SD whose `type` differs from its base's;
  missing `type`/`derivation`; **two `type.profile`s on a new slice under a sliced base → `java.lang.Error`
  "Not handled: multiple profiles"** (`PPP:1418-1420`) and a type on the first snapshot element → `java.lang.Error`
  (`PU:882-883`) — the spec's legal disjunction and a structural check both surface as JVM `Error`s, not
  exceptions; **ERROR message, continue**: profile/type inconsistency found by the final sweep (`PU:1067-1071`);
  **WARNING**: unknown profile in the final sweep; **log only** (`log.warn`/`log.debug`/`log.info`): unresolvable
  type profile at merge time under the default `allowUnknownProfile = ALL_TYPES`, failed `getProfileForDataType`
  lookup, profile-element reference into a mid-generation profile ("consult Grahame Grieve", `PPP:745`);
  **silent**: unresolvable datatype profile in the doc override (`msg=false`, `PU:2647`), stripped `#fragment`
  (`PU:4100-4102`), inherit-obligations target with a different `baseDefinition` (`PU:1210`). Contrast .NET's
  same classes: unresolvable/incompatible profile = issue + continue, several profiles = silent skip
  (`SnapshotGenerator.cs:1243-1249`) where Java throws an `Error` in one path and picks the core type in the
  others (`PPP:914`).
- **Java census (J-e, 2026-09-01; ch12 Java section):** live `throw new` sites over the three generator files
  (comment lines excluded): `DefinitionException` 67, `FHIRException` 68, **`java.lang.Error` 30** — the
  `Error`s reachable from author
  input include a slicing entry with children on a base that already has children (`PPP:382`, "please report
  issue to grahame@fhir.org"), two type profiles on a new slice (`PPP:1419`), a root `type` (`PU:883`/`1322`),
  a path outside the type (`PU:1021`), choice-group anomalies (`PU:1353` `"huh?"`, `PU:1362`). `Error`s bypass
  the `catch (Exception)` that nulls the half-built snapshot (`PU:1078-1084`). Message-mode gate: ERRORs throw
  only with `wantThrowExceptions`; two messages bypass even that (`PPP:351/355`). So Java's taxonomy has a
  fourth class .NET lacks — **JVM `Error` for author input** — and its report-vs-throw split is a constructor
  argument (`messages == null`), not a policy.
- **Status:** open (Phase 2 packets 2–3, 2026-08-24; Phase-4 evidence 2026-08-26; Java rows 2026-09-01).

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
- **Java side (J-c, 2026-09-01): Java clones the differential first** (`cloneDiff`, `PU:1483-1491` —
  in-code rationale: "we're sometimes going to hack the differential while processing it") and does all
  preprocessor injection/merging, slice-name generation and cursor bookkeeping on the clone. Write-back to
  the caller's rows is limited to derivation-tracking user data (`PU:913-920`) and one **opt-in** repair:
  clearing a root `type` that equals the base type (`wantFixDifferentialFirstElementType`, default
  false, `PU:1319-1320`). Two things still reach the caller's SD from *outside* the generator in the test
  driver: `setIds(source)` before generation (driver `:548`/`:600`) and the CLI's `autoFixSliceNames`
  (DEV-016). So the two implementations answer the question oppositely by default: .NET repairs in place,
  Java isolates and reports (orphans are ERRORs, ch4) — and Java's own opt-in flag shows the maintainers
  consider *repairing the input* a legitimate, if non-default, generator behavior.
- **Correction (J-e, 2026-09-01):** Java's write-back is **not** limited to user data. `generateSnapshot`
  ends with `setIds(derived, false)` (`PU:886`), and with `checkFirst = false` that regenerates the ids of
  the caller's **original differential** (the walk used a clone) and absolutizes its local contentReferences
  (`PU:4257-4260`, `4359-4363`). So Java also normalizes the input differential — ids and reference form —
  just not its constraint content. The test driver's own pre-generation `setIds` hides this in the shared
  suite. Both engines mutate the input; they differ in *what* (.NET: slice names, root sliceName; Java: ids,
  contentReference form, opt-in root type).
- **Status:** open (Phase 2 packet 2, 2026-08-24; Java side 2026-09-01, corrected J-e).

## OQ-016 — What does a differential-less StructureDefinition mean?
The spec never states whether a StructureDefinition without a differential means "snapshot = base snapshot"
(ch3 spec gap 3). .NET itself answers twice, differently: full generation synthesizes an empty differential
and proceeds — snapshot = rebased base snapshot + generator fill (`SnapshotGenerator.cs:362-369`); but root
resolution for *type profiles* rejects the same SD with an issue ("profile has no differential",
`SnapshotGenerator.cs:2391-2396`), so a differential-less SD cannot be used as a type profile whose root
merges into a referencing element. The in-code TODO ("Handle empty diff (=> return root element of base
profile)") shows the authors consider this a gap, not a design decision. What is the sanctioned meaning —
and does Java accept differential-less SDs in both roles?
- **Java side, main path (J-c, 2026-09-01; code-derived, not run):** accepted — `checkDifferential` sees no
  rows, the root-type check is skipped (`hasDifferential()` false), and the walk's diff limit is `-1`
  (`PPP:170`), so every base row is copied with the fill obligations: snapshot = base copy. Same answer as
  .NET's main path. Java's behavior in the *type-profile-root* role (.NET's refusing path) is ch7 material
  for packet J-d.
- **Java side, type-profile-root role (J-d, 2026-09-01):** also accepted. Java has no root-resolution
  cascade — a type profile's root is simply its snapshot's first element (`PPP:754`), and a snapshot-less
  profile is generated on demand first (`PPP:725-731`), which for a differential-less SD is the base copy
  above. No "has no differential" check exists anywhere in the Java path. So Java answers the question
  consistently in both roles ("snapshot = base"), .NET only in one — the .NET refusal is an implementation gap
  (its own TODO says so), not a defensible reading of the spec. The remaining open point is purely
  spec-side: should the spec *say* that a differential-less SD is legal and means "no constraints"?
- **Status:** open, spec-side only (Phase 2 packet 3, 2026-08-24; Java both roles 2026-09-01).

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
- **Java side (J-d, 2026-09-01): the extension, by element id, partially; the fragment not at all.**
  `elementdefinition-profile-element` drives template selection (`PPP:734-749`: the element with that **id**
  in the profile snapshot becomes the merge template — for Extension/Resource-typed bases), the type
  compatibility check (`PPP:715` → `PU:1650-1662`: the nominated element's types must intersect the diff types)
  and the final type sweep (`PU:1058-1066`); it is **not** used for the children walk-in, which opens the
  profile at its root (`PU:2673` in-code todo: "should we change down the profile_element if there's one?").
  The `url#fragment` form is **silently stripped** by `findProfile` (`PU:4100-4102`) and never read. So the
  two engines implement **disjoint** syntaxes: .NET fragment-only (id-vs-name confusion, DEV-018), Java
  extension-only (id-based, root-only expansion). The spec documents only the extension (R5 §5.1.0.16), which
  settles the syntax question in Java's favour; what remains open is the **generator obligation**: must the
  snapshot expand the profiled element's *children* from the nominated sub-tree (neither engine does today),
  or is "apply the profile starting at the nominated element" a validator-only instruction? No shared test
  covers either syntax — a test would settle the expected snapshot shape.
- **Status:** open, narrowed to generator obligation (Phase 2 packet 4, 2026-08-24; Java side 2026-09-01).

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
- Phase 4 packet 2 (2026-08-26, harness): **renamed-form data point** — `ts-case2` (renamed type
  slice) comes out *structurally identical* on both sides (ids/min/max/types/sliceNames all agree;
  only per-property noise), so for the renamed form .NET and Java implicitly agree. The **R5-form
  entry element** is where they explode apart — see DEV-020/OQ-020 (obs-2b): Java collapses the
  *entry's* type list to the sliced types (its version of an implicit constraint, applied to the
  entry rather than the slice), .NET keeps the full choice list on entry *and* (per this OQ) on a
  type-less slice.
- Phase 4 packet 3 (2026-08-26): the normalization split has a second, *identity-level* half —
  when a diff constrains **children under a renamed choice** (t16/t31), .NET synthesizes an explicit
  `value[x]:valueDecimal` type slice and hangs the subtree there, while Java/golden anchors the subtree
  directly on bare `value[x]` with no slice at all
  ([DEV-026](13-deviation-register.md#dev-026--renamed-choice-constraints-net-anchors-on-a-synthesized-type-slice-java-on-bare-valuex-ch6ch7)).
  So the full question is: given a renamed-form constraint, must a generator (a) synthesize a type
  slice (.NET), (b) fold constraints onto the unrenamed element (Java), and in either case (c) does an
  implicit type constraint apply? Golden blesses (b) — the representation the R5 text arguably
  *removed*.
- **Java side answered (packet J-a, 2026-08-31):** for (c), Java applies the implicit constraint in
  **both** syntactic forms — type-slice recognition infers the type from the path suffix *or the sliceName
  suffix* when the slice states no `type`, and stamps the inferred code onto the slice's differential
  (`diffsConstrainTypes` `PU:1806-1849`, `PPP:571-572`); canonical `<stem><Type>` slice names are enforced
  (auto-set when missing, error when wrong unless `autoFixSliceNames`). So a bare R5-form `value[x]:valueString`
  slice IS a single-type string slice in Java, while .NET leaves it the full choice list — the .NET R5-form
  behavior is now the outlier on (c).
- **Status:** open (spec question) — both implementation sides answered.

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
- **Phase-4 data point (2026-08-26, DEV-022):** the lists demonstrably do *not* agree. When copying
  datatype children into a snapshot, Java also filters **tooling** extensions .NET keeps — e.g. shipped
  `Identifier.type.binding` carries `tools/…/binding-definition`, `elementdefinition-bindingName` and
  `elementdefinition-isCommonBinding`; after expansion Java keeps only `bindingName`, .NET all three
  (~770 property diffs in the sweep). Java's presumed mechanism: the `EXT_SNAPSHOT_BEHAVIOR` policy +
  four static URL lists found in the Phase-3 orientation (deep-read pending).
- **List comparison (J-e, 2026-09-01; ch12 Java section):** Java's `NON_INHERITED_ED_URLS` (`PU:232-249`,
  15 urls; applied by `checkExtensions` on the copy-through path `PPP:1066`, at SD level `PU:1230`, and in
  specialization inheritance `PU:1276`) vs .NET's 18-entry blocklist (`SnapshotGeneratorExtensions.cs:137-156`):
  **agree on 8** (isCommonBinding, fmm, standards-status, category, security-category, wg, normative-version,
  summary); **Java-only 7** (tools/binding-definition, tools/no-binding — the sweep's ~770 diffs —,
  implements, explicit-type-name, obligation-profile core+tools, standards-status-reason); **.NET-only 9**
  (fmm-no-warnings, hierarchy, **interface**, applicable-version, codegen-super, replaces,
  resource-approvalDate/-effectivePeriod/-lastReviewDate) plus .NET's own marker. Java additionally has
  three *other* policy lists (non-overriding / overriding / default-inherited) and a per-extension
  `snapshot-behavior` declaration — i.e. Java already treats inheritance policy as **extension metadata**,
  which is what the candidate RFC would standardize. Neither list is documented anywhere.
- **Status:** open (Phase 2 packet 6, 2026-08-26; Java data point Phase 4 packet 3; list diff 2026-09-01).

## OQ-020 — What may a generator do to the slicing entry of a type slicing?
The headline Phase-4 finding (DEV-020, test `obs-2b`): given an author-written slicing entry on a choice
element (`rules=open`, no discriminator) plus one type slice with `min=1`, the two implementations produce
materially different sliced elements — Java **rewrites** the entry (injects `type:$this`, forces `closed`
against the explicit `open` ["type slicing is always CLOSED regardless of what the differential says",
`ProfilePathProcessor` L597/L1587], collapses the type list to the union of the slices' types, raises `min`
to the slice-min sum), while .NET **merges it as written** (open, no discriminator, full inherited choice
list, min 0). Golden files bless the Java behavior; the published spec text supports .NET on the type list
("type specific entries do not restrict allowed types") and is silent-to-contradictory on the rest
(§5.1.0.14 slice-min arithmetic is validation guidance, not a generator instruction — the
permission-vs-default distinction; no rule allows overriding an explicit `rules` value).
Decision needed, per property of the slicing entry: is the generator *required*, *permitted*, or
*forbidden* to (a) synthesize/complete the discriminator, (b) rewrite `rules`, (c) restrict the type list,
(d) recompute `min`? Sub-question: if the differential's slicing violates the shape rules (no discriminator
and no description), is normalize-and-repair (Java) or propagate-as-written (.NET) the sanctioned response?
(Feeds OQ-014's error-taxonomy theme; connects to OQ-018 — Java's type-list collapse on the *entry* is the
mirror image of .NET's implicit constraint on the renamed *slice*.)
- **Mechanism pinned (packet J-a, 2026-08-31):** the Java behavior is *rebuild-CLOSED-then-reopen-if-
  uncovered*, not conditional normalization — see DEV-020's pinned trigger conditions (the "always closed"
  comment is contradicted by the reopen at `PPP:646-667`; the entry-min raise is to literal 1; the type
  collapse rides on a `min>0` slice's `fixedType`). Adds a fifth per-property decision: (e) is the
  asymmetry between unsliced-base (reopen exists) and sliced-base (`PPP:1584-1588`, no reopen — stays
  CLOSED) type slicing intended? Also relevant: the community already decided *slicer properties do not
  copy into slices* (`APPLY_PROPERTIES_FROM_SLICER=false`, `PPP:42-58`, Zulip #IG-creation) — the one
  adjudicated data point on what a generator may do around the entry.
- **Status:** open (Phase 4 packet 2, 2026-08-26; mechanism pinned 2026-08-31) — prime WGM material,
  demo-able with obs-2b.

## OQ-021 — How much must a snapshot materialize?
The spec says a snapshot contains "all the elements" but never defines the element *set* — and the two
implementations disagree on ~450 elements across 23 tests
([DEV-025](13-deviation-register.md#dev-025--materialization-depth-of-unconstrained-content-java-normalizes-more-than-net-ch7ch8ch11)).
.NET's policy is minimal: materialize exactly the base's elements plus whatever subtrees the differential
constrains (ch11 §1). Java materializes more: the full child set of a sliced contentReference's *entry*
element (t21, comp-deep), slicing-entry child constraints copied into every named slice (org2a), and a
complex extension's complete nested slice structure even where the diff mentions one slice (pat-xver).
Both positions are defensible — the entry's constraints logically apply to the slices whether or not they
are physically copied — but consumers read snapshots *literally*: a renderer or code generator sees
different element sets, and cardinality/`ele-1`/binding data present in one snapshot is absent from the
other. Questions: (a) is there a normative minimum element set (is a slice missing its materialized
children still "a snapshot")? (b) when entry constraints are copied into slices, which properties copy —
and is that copy *required* so validators need no entry-fallback logic? (c) for recursive structures
(contentReference, extension-of-self), how deep must materialization go — per recursion level the diff
reaches (both engines), plus the entry level (Java only)? Related: sdf-3/8b define per-element fill
obligations but no set-level rule; §5.1.0.10 (R5 propagation text) touches (c) without answering it.
- **Sharpened by the min/mustSupport mining (2026-08-26; corrected J-a 2026-08-31):** for (b) Java's
  answer is concrete — the preprocessor merges entry-children into every named slice with **strict
  fill-if-absent for all 27 handled properties** (lists only when empty — no append; `mapping`/`condition`
  never propagate on a match, though *injected* elements keep them: a match-vs-inject asymmetry;
  `SnapshotGenerationPreProcessor`, DEV-025) — and this single mechanism accounts for **77% of all
  min/mustSupport divergences** in the sweep (101/131). The R5 text offers only "slices must be
  consistent with" the entry (profiling, slicing rules) — it neither mandates nor forbids the copy-down.
  The practical WGM question: *do slices inherit the slicing entry's children constraints, and must the
  snapshot materialize that inheritance?* Java says copy-down; .NET says nothing at all. Cautions for the
  WGM framing (J-a): the copy-down mechanism is demonstrably buggy
  ([DEV-033](13-deviation-register.md#dev-033--java-preprocessor-cross-slice-contamination--silent-constraint-loss-ch6)
  — the golden files bless contaminated output), and it sits in tension with the community's own
  `APPLY_PROPERTIES_FROM_SLICER=false` decision that slicer *properties* do NOT copy into slices (ch6) —
  entry children copy down, entry properties don't.
- **Type profiles as a materialization dimension (J-d, 2026-09-01):** the same question applies to
  `type.profile`. .NET *closes over* type profiles (root and children merged into the snapshot, ch7); Java
  does so only for Extension/Resource roots and only opens a profile's children where the base has none,
  otherwise leaving `type.profile` as an instruction to the validator
  ([DEV-038](13-deviation-register.md#dev-038--type-profile-scope-net-merges-any-type-profile-java-only-extensionresource-roots-and-only-when-the-base-has-no-children-ch7)).
  So Java materializes *more* for slicing-entry children and *less* for type profiles — neither engine has a
  single materialization principle. Question (d): must a snapshot incorporate the constraints of the profiles
  named in `type.profile`, or is the snapshot complete when it records the reference?
- **The one non-diff-driven expansion in Java (J-e, 2026-09-01):** apart from the preprocessor copy-down,
  Java's walk expands children only where the differential has rows — except the slicing-entry inline dump
  (`PPP:402-419`), which materializes a contentReference target's children under a sliced entry precisely
  when the entry has no diff child rows (DEV-025 flavor 1, comp-deep/t21). That is the only place either engine materializes structure the
  differential never mentions, and it exists for a mechanical reason (the slices need a base to be walked
  against), not a policy one.
- **Status:** open (Phase 4 packet 3, 2026-08-26; type-profile dimension 2026-09-01) — WGM material; demo-able
  with org2a (52-element gap), sd-comp-hist (45 property diffs), and t21.
