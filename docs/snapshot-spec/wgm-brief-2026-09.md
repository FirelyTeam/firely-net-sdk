# Snapshot generation — open questions for the HL7 WGM (September 2026)

**Status: v2 (2026-09-03) — review/freeze pass over the 2026-09-02 draft (coherence, evidence tiers, executive
summary); freeze target 2026-09-14.** Prepared by Ewout Kramer (Firely) from a reverse-engineering study of the two
mainstream snapshot generators. This brief is self-contained: it can be read and used without the underlying
document set.

## Executive summary — what the evening session has to decide

Two implementations generate different snapshots from the same profile because the spec defines the *shape*
of a snapshot and not the algorithm. Everything below is either a **live decision** (no HL7 ruling exists and
the engines disagree), a **confirmation** (a ruling exists in JIRA resolution text and at least one engine
ignores it), or **if time permits**. Each live item lists our recommended answer and the input that demonstrates
it; the numbered sections give the full evidence. Tier legend used throughout: *resolution* = HL7 vote on
record; *comment* = discussion on a ticket; *Zulip* = a chat agreement, however senior the participants.

**Live decisions (ten; ordered by architectural weight):**

| # | decision | our recommended answer | demo |
|---|---|---|---|
| Q1 | Does a snapshot close over `type.profile` (merge the datatype profile's root + children), and who wins when base and type profile both constrain a property? Sharpest form: FHIR-48664 now allows a *binding* on a datatype-profile root — must it reach the snapshot? | Close over the profile's children and non-cardinality root properties, once, in the order base ≺ type profile ≺ differential; cardinality stays a *bound* (FHIR-19756). Or decide "never merge" — but decide. Ask for a shared test case. | MyAddress / `Patient.address` (code-derived) |
| Q2(b) | Do a slicing entry's **child** rows copy down into every named slice (Java since 2025; FHIR-50267 says entry *properties* don't)? | No implicit copy-down; state one principle for properties and children; element set = base + differential-constrained subtrees, expanded one level below every touched element. | org2a, t21 |
| Q3(b) | May a generator rewrite an authored `slicing.rules` (open → closed) on a type slicing? (c)/(d) — type-list collapse and min raise — are already ruled out in resolution text; confirm. | Forbidden; the snapshot must not contradict what the author wrote. (a) discriminator synthesis permitted. | obs-2b |
| Q4(b) | Renamed-form differential (`Observation.valueString`): synthesize a type slice (.NET), fold onto bare `value[x]` (Java, golden), or reject — and which id does the snapshot carry? No decision-tier source on either side. | Fold when the element is single-typed; synthesize only when several types remain; state the id. (a) a type-less type slice *is* single-typed — one sentence. | ts-case2, t16, t31 |
| Q5 | The generator contract: a normative floor ("never silently corrupt or silently drop"), which author errors a generator SHALL detect, and the posture for one-directional violations. Note: every HL7 enforcement decision so far placed the check in the *validator*. | Floor: yes. Required checks: path grammar, new paths in constraint profiles, ordering, min/max loosening, †-frozen changes. Posture: keep the base value **and** emit an error. No duplicate ids, no fabricated `base`. | t23a, obs-unit, the 21 fail tests |
| Q6 | Is `Extension.url.fixedUri` part of the snapshot contract (generator supplies it) or an authoring obligation? | Promote §2.1.5.1.4 to conformance language: authors SHALL fix it; a generator MAY supply a missing value and SHALL NOT alter an inherited one (RFC-015). | ext-recursion-2, au2 |
| Q7(b)(d) | After a generator expands a contentReference's children, does the reference survive on the element (Java sliced-base) or is it replaced by the target's `type` (.NET, Java step-in)? Are the eld-5 properties undefined after dereferencing? | Replaced by `type`; eld-5 properties undefined — one sentence each. (a) R6 literal-path direction is decided (FHIR-57266): confirm. (c) absolutization: confirm the 2022 Zulip rule and pick the canonical. | t21, comp-deep, eob tests |
| Q8 | Both below-root syntaxes (`url#id` fragment, `profile-element` extension) are sanctioned, both by element id. Is the fragment superseded? Must the generator expand children from the nominated element? | Extension canonical, fragment deprecated-but-accepted (by id). Expand from the nominated element iff Q1 says "close over". Needs a shared test. | none — needs a test |
| Q9 | Non-inheritable extensions: FHIR-28441 decided the mechanism *and* the 17-url list (Java omits nine). Who stamps the metadata on the extensions pack, and when? | Ask FHIR-I to stamp `structuredefinition-inheritance-control` on the pack in the next release, starting from the 28441 classification (decision text) and using the Java/.NET/Grahame-2023 lists only for post-2020 extensions; interim: both engines adopt the 28441 class-5 list verbatim; rule on `explicit-type-name`. | ca-patient, ILCorePractitioner |
| Q11(b) | Does the 2019 "error, not repair" agreement on wrong type-slice names still stand (both engines' defaults now repair silently)? | Reaffirm and write it down: repair only with a message; canonical-form normalizations (ids, `[x]` paths, absolutization) stay permitted. | t29a, t43a |

**Confirm only (decided in JIRA resolution text; the room need only nod, then the engines fix):** a type root's
cardinality is a *bound* on the referencing element, never merged or loosened (Q1 sub-decision; FHIR-19756,
FHIR-36738); the `[x]` entry keeps its type list and its declared `min` (Q3 c/d; FHIR-12259, FHIR-31054); a type
slice is single-typed (Q4a; FHIR-12259 item 4); R6 contentReference semantics are literal-path with opt-in
propagation (Q7a; FHIR-57266/57265); both below-root syntaxes address an element **id** (Q8a; FHIR-13973,
FHIR-49079); inheritance policy is per-extension metadata with an adopted classification (Q9; FHIR-28441); ids
are derived data (Q11a; FHIR-9843, FHIR-12182); slicing a non-repeating base element is only supported by type
(Q12; FHIR-28619); an
ED mapping with the same identity *replaces* the parent's (Q13; FHIR-34434 — neither engine does it) and
`elementdefinition-suppress` deletes at snapshot time (Q13; FHIR-31406).

**If time permits:** Q10 (differential-less SDs — one sentence), Q12 (lattice / `sliceIsConstraining` as
generator duties — decide together with Q5), Q13 merge-rule table (one direction per row), Q14 (Java-only merge
inputs: additionalBase, obligations), Q15 (self-typed extension slices), Q16 (what a new element inherits from
its type). Tier 3 holds design questions for the Java maintainers and the already-filed list — do not rehash.

**After the session (where outcomes go):** Q5 and Q13 merge-rule outcomes → a comment by Ewout on
[FHIR-31405](https://jira.hl7.org/browse/FHIR-31405) (open, FHIR-I is waiting for Firely's input there);
Q1–Q3 outcomes → Ewout's live thread #conformance "Issue 13402 - Clarify snapshotting rules" (Appendix B);
agreed spec sentences → the RFC list in our document set → Confluence, the home FHIR-13402 named for these rules;
any new shared test cases → FHIR/fhir-test-cases `r5/snapshot-generation`. Nothing is posted from this brief
automatically; it is Ewout's material.

## How to use this brief

- **Purpose.** Firely is re-specifying (and later re-implementing) FHIR snapshot generation. The published
  spec deliberately describes the *shape* of differentials and snapshots, not the generation algorithm
  ([FHIR-13402](https://jira.hl7.org/browse/FHIR-13402), closed 2023 *Not Persuasive*: the rules are
  acknowledged as undocumented; any write-up would live in Confluence). Where the shape rules run out, the
  two implementations — the Java reference implementation (`org.hl7.fhir.core`) and the Firely .NET SDK —
  have each invented semantics, and in the places listed below they invented **different** ones. Each
  question asks for a decision, not an opinion. HL7 has twice agreed the rules should be written down and
  twice given up for lack of a volunteer: [FHIR-9079](https://jira.hl7.org/browse/FHIR-9079) (Persuasive
  11-0-0 in 2016, "Ewout and Chris are going to merge their docu"; reverted 2022 — "test cases and reference
  implementations … will have to do") and FHIR-13402 (Persuasive 9-0-0 in 2018, "create a whole section";
  reverted 2023). This brief and the document set behind it are that abandoned work item. The one live JIRA
  vehicle is [FHIR-31405](https://jira.hl7.org/browse/FHIR-31405) ("Clarify expected behavior of
  ElementDefinition properties in differential", *Waiting for Input* since 2021 — FHIR-I asked Firely for the
  merge rules; Ewout's 2023 comment is the last activity): WGM outcomes on Q5/Q13 belong there, not in new
  tickets.
- **Tiers.** Tier 1 = architectural forks that decide the shape of every non-trivial snapshot; Tier 2 = spec
  clarifications where one sentence would settle a divergence; Tier 3 = Java design questions for the
  maintainers and a list of things already filed (so the session does not rehash them).
- **Per question:** context → reproducing input → .NET behavior → Java behavior → spec basis → decision needed
  (options, with our recommendation where we have one). Questions are ordered by importance to a fresh
  implementation.
- **Evidence provenance.** Every behavior below was observed with: .NET = Firely SDK `Hl7.Fhir.R5` 6.2.1;
  Java = `org.hl7.fhir.r5` engine from validator_cli 6.10.2 (git d06577dbc5c6, built 2026-08-13), code
  citations against `org.hl7.fhir.core` master commit `b06c7ee` (2026-08-21); golden files =
  [FHIR/fhir-test-cases](https://github.com/FHIR/fhir-test-cases/tree/master/r5/snapshot-generation)
  1.7.67 (commit 9f495e8), R5 core 5.0.0. Java reproduced its own golden files on **143/143** generation
  tests and satisfied all **21/21** `fail="true"` tests, so every Java behavior quoted here *is* the behavior
  the golden files encode.
- **Java configuration caveat (important for live reproduction).** The golden files encode the **JUnit
  test-driver configuration** of `ProfileUtilities`: `newSlicingProcessing = true` (the manifest default),
  `autoFixSliceNames = false`, `setThrowException(false)`, `sortDifferential` applied only where the manifest
  says `sort="true"`. The `validator_cli -snapshot` task uses a *different* configuration
  (`autoFixSliceNames = true`, `newSlicingProcessing = false`, throw on first error). The two configurations
  diverge on exactly the type-slicing territory of Q3/Q4. If a behavior is reproduced live with validator_cli
  and does not match this brief, check the configuration first.
- **Independent corroboration.** Gino Canessa published a separate Java-vs-.NET comparison on 2026-09-01
  ([ginoc.io/202609-snapshot](https://ginoc.io/202609-snapshot/snapshot-generation-comparison.html)), pinned
  to *different* revisions (Java master 4f52ba6 / 6.10.4-SNAPSHOT, .NET main 4bd9dd8 = v6.2.0-10, fhir-test-cases
  1.7.68), with nine executed cases. It independently reproduces the reslice loss (Q5 FYI list, #3589), the
  t23 duplicate (Q5), the type-slicing entry normalization (Q3: obs-1 discriminator, obs-4 collapse), the
  obs-5 validation-boundary split and the fixed/pattern type check (Q5), the contentReference shape difference
  (Q7) and the base-reuse settings sensitivity — so none of those is a version artifact of our pins.
- **Code-citation legend.** Java: `PU` = `ProfileUtilities.java`, `PPP` = `ProfilePathProcessor.java`,
  `PRE` = `SnapshotGenerationPreProcessor.java`, all in
  `org.hl7.fhir.r5/src/main/java/org/hl7/fhir/r5/conformance/profile/`, line numbers at commit `b06c7ee`.
  .NET: `SnapshotGenerator.cs`, `ElementMatcher.cs`, `ElementDefnMerger.cs` in
  `src/Hl7.Fhir.Conformance/Specification/Snapshot/` of `FirelyTeam/firely-net-sdk` (6.2.1).
  Spec: R5 pages `profiling.html`, `elementdefinition.html`, `structuredefinition.html`, `extensibility.html`
  under `https://hl7.org/fhir/R5/`; "R6 build" = build.fhir.org snapshot v6.0.0-ballot4 (2026-08-18).
- **Evidence tiers and verification.** JIRA quotes are tagged by where they sit on the ticket: *resolution text*
  (an HL7 vote on record — the "Resolution Description" field), a *comment* (discussion, however senior), or a
  *retracted/auto-approved* ticket (no vote). Sixteen load-bearing tickets were re-read via the JIRA REST API on
  2026-09-02/03 and every quote attributed to resolution text below was found verbatim in that field
  (FHIR-3623, 8969, 13973, 14958, 19756, 28441, 28619, 31054, 34434, 48664, 49079) or in a description the
  resolution adopts wholesale (FHIR-12259 "Make change as proposed", FHIR-50267 "Do this"); the quotes tagged as
  comments (FHIR-8969 Lloyd, 14091 Lloyd, 19756 Chris Grenz, 50267 Lloyd + Grahame) were confirmed to be
  comments; FHIR-15900 turned out to be an *auto-approved* tooling ticket. The remaining ~65 tickets were read
  once during the sweep. Zulip permalinks: ten verified in a browser on 2026-09-03 (Q1 ×2, Q3 ×2, Q4, Q5, Q7, Q9,
  Q13, Ewout's thread) plus two on 2026-09-02 — both the `#narrow/stream/…` and `#narrow/channel/…` forms resolve
  to the cited topic and message; the rest were taken from the API and are unverified.
- **Internal cross-references** (`OQ-nnn` = open question, `DEV-nnn` = deviation register entry, `RFC-nnn` =
  spec-change proposal) point into the Firely study document set; they are for our own bookkeeping and can
  be ignored during the session.

---

## Tier 1 — architectural forks

### Q1. Does a snapshot close over `type.profile`? (type profile vs base: who wins, and is the profile merged at all?)
*OQ-001, OQ-002, OQ-021(d), DEV-038*

**Context.** An element declares `type.profile = MyAddress` (a datatype profile with root `1..1`, `line 1..*`,
an invariant on `city`). Nothing in the spec says whether the generated snapshot must *incorporate*
MyAddress's constraints into the element and its children, or whether `type.profile` is simply recorded for
the validator to apply. Both implementations answered — oppositely — and the answer determines the element
set, cardinalities and invariants of every snapshot that uses a datatype profile.

**Reproducing input.** Community MWE (Volker Wegert, #Simplifier.net / #shorthand, Oct–Nov 2025): `MyAddress`
= Address + invariant `demo-1`; `MyPatient` with `address only MyAddress`; `MySecondPatient` derived from it.
SUSHI `--snapshot` (Java engine) yields `Patient.address` carrying only `ele-1`; the Simplifier-regenerated
(.NET) snapshot carries `demo-1` as well. For the children question, no shared test exercises a datatype
`type.profile` with child constraints — a test would be the best outcome of this question (code-derived
example: `Patient.address.type.profile = MyAddress` plus a differential row on `Patient.address.line`).

**.NET behavior.** Always merges a single type profile that differs from the base's implied profile, for
**any** type. With child constraints in the differential: the profile's rebased snapshot is merged onto the
element *as if it were a differential* (so it overrides nearly every property — "type beats base",
`SnapshotGenerator.cs:1400-1413`), then the differential is merged on top. Without child constraints: only the
profile **root** is merged onto the element (`:1464-1476`) — which is how the *cardinality diamond* arises: a
`0..1` element typed with a profile whose root says `0..*` silently becomes repeating
(`ElementDefnMerger.cs:689-695`, documented in code as unresolved). The code also documents a known wrongness:
when a parent profile had already constrained the type's children, the type snapshot re-applies original type
values over the parent's overrides (`:1405-1411`, "Address Snap + Diff + Address Snap (WRONG!) + MyAddress
Diff").

**Java behavior.** Three narrower mechanisms. (1) The profile root becomes the merge template only when the
element's type is `Extension` or `Resource` (`PPP:763-772`); for **datatype profiles the root is never merged**
— cardinality, invariants, binding of MyAddress's root are invisible in the snapshot, only `type.profile`
records the url (`PU:2643-2648` discards non-Extension/Resource profiles in the descriptive-text override).
(2) The profile's children are used **only if the base snapshot has no children at that element**
(`!baseWalksInto`, `PPP:829/1423`; `baseHasChildren`, `PPP:1080/1672`); if a parent profile already
expanded `address`, Java walks the base children and MyAddress's child constraints are ignored — **base beats
type profile**, the inverse of .NET. (3) A new slice under an already-sliced base picks up the profile root's
`min`/`max` one-directionally (raise min / lower max, `PPP:1398-1420`; two profiles → `java.lang.Error "Not
handled: multiple profiles"`). For extensions the root *is* the template: root `max` flows into the slice
(capped to the slicer's max), root `min` is overwritten to 0 for open slicings (`PPP:801-805`).

Outcome for the example: .NET emits `address` carrying MyAddress's root cardinality + invariants and
MyAddress's children over the base's; Java emits the base `address` row (+ `type.profile`), and MyAddress's
children only if `Patient.address` had no children in the base snapshot — otherwise MyAddress's constraints
reach the snapshot **nowhere**.

**Spec basis.** None on merge scope or priority. The only related rule is R5's root-cardinality rule
(structuredefinition §5.4.6.1: a type's root `0..1` caps the referencing element's max; R4 called the root
cardinality irrelevant) — Java honors it partially (sliced-base pick-up, extension templates), .NET as a side
effect of the root merge (including the loosening direction the rule does not sanction).

**Decision needed.**
- (A) A snapshot SHALL close over `type.profile`: the profile's root and children are merged, with a defined
  priority order (proposal: base ≺ type profile ≺ differential, i.e. .NET's order but applied *once*, never
  re-applying the type snapshot over a parent profile's overrides).
- (B) A snapshot SHALL NOT merge type profiles: `type.profile` is a validator instruction; the snapshot
  reflects base + differential only (Java's datatype behavior, generalized).
- (C) Java's actual hybrid (Extension/Resource roots merged, datatypes not, children only into a void) — the
  golden files encode this. The root half has a stated principle ("the features of the type remain on the
  type"; the override "wasn't intended to work for types" — Grahame, below); the *children gate* has none and
  was never discussed.
- Sub-decision for the cardinality diamond — **decided in resolution text, confirm only:** a type root's
  cardinality is a bound the referencing element must respect (FHIR-19756 resolution; FHIR-36738 "Root
  elements" section). That the generator "should not pull the root element's cardinality into the snapshot"
  is a *comment* on 19756 (Chris Grenz's WGM note), not resolution text — the room may weigh it differently.
  What is new: FHIR-48664 (2025) lets a datatype-profile root carry a *binding* and asks both generators to
  "account for this change" — under (B) that binding never reaches the snapshot; under (A) it does. That is
  the sharpest live form of this question.
- **Recommendation:** (A) restricted to the profile's *children and non-cardinality root properties*
  (cardinality stays a bound per FHIR-19756 — never merged, never loosened), or (B) — but a decision. (C)'s
  children gate is not implementable from a principle. Ask for a shared test case either way.

**Decisions on record (JIRA).** The cardinality half is decided: [FHIR-19756](https://jira.hl7.org/browse/FHIR-19756)
(2018, Persuasive 5-0-0): "the cardinality on a type places constraints on references to that type" —
referencing profiles "must fall within the cardinality bounds of the type"; Chris Grenz's WGM note on the
ticket: the generator "should not pull the root element's cardinality into the snapshot".
[FHIR-36738](https://jira.hl7.org/browse/FHIR-36738) (2022, 16-0-2) added the "Root elements" section (a
`0..1` root → referencing element max ≤ 1). So .NET's diamond behavior is wrong twice over (it merges, and it
loosens), and Java's "never merge datatype roots" satisfies 19756. But
[FHIR-48664](https://jira.hl7.org/browse/FHIR-48664) (2024–25, Applied, 11-0-0) now **allows a binding on a
datatype profile's root** and says "Will ask the Java and Firely snapshot generator authors to account for
this change" — Java's gate cannot surface such a binding in the snapshot, .NET surfaces it only via the merge
19756 forbids; neither engine has reconciled it. On the wider closure question: FHIR-12179 (2016, 8-0-3) wants
the build snapshot to inherit "all applicable constraints from all applied base and type profiles"; Grahame's
San Diego 2017 note on the retracted FHIR-13839: "snapshot generators should be sure to generate the snap shot
completely"; Ewout's 2016 proposal on FHIR-9791 (type-profile descriptive text ignored except single-profile /
new-slice cases) was never landed. No ruling on precedence when both constrain the same property.

**Prior discussion (Zulip, see Appendix B).** This is the oldest open question in the set and *already
scheduled for this WGM*: Ward Weistra re-raised it verbatim in Feb 2026 (#conformance "Inheritance from parent
profile or datatype profile?") and Grahame proposed "an evening meeting in Rotterdam". Positions on record:
Java stopped merging datatype-profile roots in Dec 2024 ("wasn't intended to work for types" — Grahame;
"the features of the type remain on the type" — Grahame/Lloyd, Nov 2025; "a type has no meaning of its own"
— Lloyd), and on the derived-host case Grahame said "I don't actually know in that case". Grahame 2016:
inlining types into the snapshot is optional, "I object strenuously to making it always required"; Michel
Rutten 2016: a snapshot omitting external-profile information "will be incomplete and unreliable". On the
diamond: 2018 consensus (Lloyd, Grahame, Chris Grenz, Michel) that a type root may **never loosen** the
referencing element — Lloyd: the root *bounds* it (GF#19756); Chris: the generator should ignore root
cardinality entirely; Michel: .NET overrides without verifying and leaves verification to a validator step.
So the .NET loosening has been a known gap since 2018. Firely customers (Nictiz, PS-CA) depend on the
Simplifier behavior (type-profile descriptive text wins); Grahame's 2023 package scan found hundreds of
profiles applying datatype profiles to elements — the population any decision affects.

### Q2. How much must a snapshot materialize? (slicing-entry copy-down, contentReference entries, nested extensions)
*OQ-021, DEV-025, DEV-033, DEV-021*

**Context.** The spec says a snapshot contains "all the elements" but never defines the element *set*. The two
implementations differ on ~450 elements across 23 shared tests. .NET's policy is minimal: the base's elements
plus whatever subtrees the differential constrains. Java materializes more, through three mechanisms, and
the golden files bless Java's element sets.

**Reproducing inputs** (all in fhir-test-cases `r5/snapshot-generation`):
- [`org2a`](https://github.com/FHIR/fhir-test-cases/blob/master/r5/snapshot-generation/org2a-input.json) —
  differential constrains children of the **slicing entry** `Organization.identifier` (e.g. `identifier.use`);
  Java copies the modified entry children into **every named slice** (`identifier:NPI.{id,use,type,…}`, 16
  elements); .NET leaves the named slices as bare elements (52-element gap).
- [`t21`](https://github.com/FHIR/fhir-test-cases/blob/master/r5/snapshot-generation/t21-input.xml) /
  `comp-deep` — a sliced **contentReference** element (`Composition.section`); Java expands the target's full
  child set under the *unsliced slicing entry* too (70 elements in t21, repeated per recursion level in
  comp-deep); .NET expands only under the named slices.
- `pat-xver-extension` — a complex extension referenced by one slice; Java inlines the extension's complete
  nested slice structure (`species`/`breed`/`genderStatus`, each with `{id,extension,url,value[x]}`); .NET emits
  only the mentioned slice, childless.
- `sd-comp-hist` (45 property diffs), `on-questionnaire` — the property-level shadow of the copy-down.

**.NET behavior.** Expand only where the differential constrains (its recursion terminator as well as its
materialization policy). No copy-down of entry children into slices; no entry-level expansion of
contentReference targets; extension children only where the differential mentions them.

**Java behavior.** (1) A **preprocessor** (`SnapshotGenerationPreProcessor`, invoked from `PU:825`) collects
"sliceStuff" — differential rows between a slicing entry and its first named slice — and pre-merges it into
each named slice's differential: strict fill-if-absent for 27 properties (lists copied only when the target
list is empty; `mapping`/`condition` never propagate on a match); missing elements are **injected** as full
copies. This single mechanism accounts for 77% of all min/mustSupport divergences in the shared suite
(101/131). (2) The slicing-entry "inline dump" (`PPP:402-419`): when a contentReference element is sliced and
the entry has no differential children, the target's children are copied under the entry — the only place
either engine materializes structure the differential never mentions. (3) Extension structure comes with the
extension definition's snapshot as the template (`PPP:763-772`).

**Two cautions for treating the golden files as normative here.**
- The copy-down mechanism is **demonstrably buggy**: it matches entry rows to slice rows by (path modulo `[x]`,
  sliceName-or-null) only, so `value[x]` rows of *different* extension slices are indistinguishable — authored
  `mustSupport`/binding data lands on the wrong extension's `value[x]` and is silently lost from the right one
  (`on-questionnaire-expected.xml` exhibits both effects; filed as
  [org.hl7.fhir.core#2584](https://github.com/hapifhir/org.hl7.fhir.core/issues/2584)).
- The community decided in April 2025 that slicer **properties** do *not* copy into slices
  (`APPLY_PROPERTIES_FROM_SLICER = false`, `PPP:42-58`, Zulip #IG-creation "Slices not inheriting preferred
  bindings from root", FHIR-50267) — while the same discussion's outcome (FHIR-50391) is what made entry
  **children** copy. Two halves of one 2025 outcome, never stated as a single principle: a validator must
  apply the entry's *properties* to every slice itself, but finds the entry's *children* pre-materialized.

**Spec basis.** "all the elements" (structuredefinition); sdf-3/sdf-8b define per-element fill obligations
(definition/min/max/base) but no set-level rule; profiling says slices "must be consistent with" the entry —
neither mandating nor forbidding a copy-down; R5 §5.1.0.10 (recursive elements) touches contentReference
depth without answering it.

**Decision needed.**
- (a) Is there a normative minimum element set? Is a named slice without its materialized children still "a
  snapshot"?
- (b) Do slices inherit the slicing entry's *children* constraints, and must the snapshot **materialize** that
  inheritance (so validators/renderers need no entry-fallback logic)? If yes: which properties copy, and
  fill-if-absent or override?
- (c) For recursive structures (contentReference, extension-of-self): how deep — per recursion level the
  differential reaches (both engines), plus the entry level (Java only)?
- **Recommendation:** decide (b) explicitly, in either direction, and record it next to the
  `APPLY_PROPERTIES_FROM_SLICER` decision so properties and children follow one principle. Our preference for
  a re-implementation: *no* implicit copy-down (validators apply entry constraints to slices anyway; the
  copy-down has produced silent data loss), with the element set defined as "base elements + differential-
  constrained subtrees, fully expanded one level below every differential-touched element".

**Decisions on record (JIRA) — the only algorithm rule HL7 has ever adopted, and it is ambiguous exactly
here.** [FHIR-50267](https://jira.hl7.org/browse/FHIR-50267) (Lloyd, 2025-04-28, Persuasive 8-0-1, *Resolved –
change required*; text not yet in the R6 build we captured) adds a "Slicing Snapshot Generation" sub-topic: a
slice in a derived profile has "essentially two 'base' elements"; its snapshot base is the **same-named slice
in the parent profile's snapshot**, else the parent's slicing element; "constraints of the base (slicing)
elements are *not* included in the snapshots" of the slices — yet they apply (a slicer binding "holds for all
slices"; a slicer mustSupport means "all slices are automatically mustSupport"). *Tiers:* the quoted rule is
Lloyd's proposed text adopted verbatim by the resolution ("Do this"); Lloyd's "This isn't a change, it's
documenting what the snapshot generation behavior has long been" and Grahame's "too late to make this a SHALL …
settle for a validation warning" are both *comments* on the ticket, not resolution text. Three days earlier [FHIR-50391](https://jira.hl7.org/browse/FHIR-50391)
(Grahame, *Applied*) said the opposite about mustSupport ("can not be assumed to apply to all slices … for
legacy reasons") and lists which slicer constraints do bind all slices (max, type, fixed/pattern, min/maxValue,
maxLength, constraints, required/extensible bindings, mustHaveValue, valueAlternatives). **Neither ticket says
whether "constraints" covers the entry's child rows** — the examples are the entry's own properties, which both
engines already refuse to copy. So Java's child copy-down is either out of scope of the 2025 decision or a
reference-implementation violation of it blessed by golden files. Older: [FHIR-8286](https://jira.hl7.org/browse/FHIR-8286)
(Ewout, 2015, 9-0-0): the slicing entry's "unconstrained definition includes the children, and update the
tooling to populate this" — sanctions materializing entry children (Java's contentReference inline dump);
FHIR-8975 (2015): an unnamed constraint on a sliced element in a derived profile "is … merely adding to all
slices"; FHIR-7783 (2015, Not Persuasive): incomplete snapshots were once "a consequence we chose".

**Prior discussion (Zulip, see Appendix B).** The copy-down is **recent and dated**: it is the outcome of
FHIR-50391 (Grahame, from the 314-message #IG-creation thread of April 2025), confirmed by Grahame in June
2025 ("Recent Snapshot generation changes.", Eric Haas noticing `Organization.identifier:NPI.id` etc. — the
org2a fixture). The same April thread concluded that slicer *properties* (binding, mustSupport) do **not**
propagate, with proposed spec text that the slicer's constraints "apply whether or not shown in the
snapshot" (FHIR-50267). So the "tension" we recorded is two halves of one 2025 outcome — entry children
materialize, entry properties don't — that nobody stated as a single principle; Lloyd still said in Dec 2025
and Jun 2026 that tools "can't" propagate slicer constraints. Boundaries on record: an inherited slice does
not receive a derived profile's allSlices changes ("otherwise we'd have multiple inheritance", Grahame
2026-08); slice `min` must not be inherited from the slicer (Lloyd 2026-01, Java bug core#2282). The depth
rule has an informal answer (Lloyd 2021: "the depth equals that in the differential", backbone exception;
Grahame 2016: reference-or-inline-all) but no spec text. The 2017 "Slicer vs Slice" thread is this question's
first run: Michel stated Firely's deliberate no-three-way-merge policy; Lloyd: downstream applications
"shouldn't have to do any merging at all".

### Q3. What may a generator do to the slicing entry of a type slicing?
*OQ-020, DEV-020*

**Context.** The headline empirical finding. Given an author-written slicing entry on a choice element plus
one type slice, the two implementations produce materially different sliced elements, and the golden files
bless the rewriting one.

**Reproducing input.**
[`obs-2b`](https://github.com/FHIR/fhir-test-cases/blob/master/r5/snapshot-generation/obs-2b-input.xml):
differential on `Observation.value[x]` (base `0..1`, 13-type choice) with an explicit slicing entry stating
only `rules="open"` (no discriminator, no description), plus one slice `valueCodeableConcept` with `min=1`,
`type=CodeableConcept` and a required binding.

**Result on the slicing entry `Observation.value[x]`** (the slice element itself is identical on both sides):

| property | .NET 6.2.1 | Java 6.10.2 = golden |
|---|---|---|
| `slicing.discriminator` | none (as written) | **`type:$this` injected** |
| `slicing.rules` | `open` (as written) | **`closed`** — overrides the authored `open` |
| `type` | full 13-type choice list | **collapsed to `[CodeableConcept]`** |
| `min` | 0 (inherited) | **1** |

The obs-2 family maps Java's gradient: `obs-2` (slice types CC only) keeps 13 types and `open`, only injects
`type:$this`; `obs-2a` (entry itself constrains type to CC) → closed; `obs-2b` (slice `min=1`) → collapsed +
closed + min 1.

**.NET behavior.** The explicit entry is matched and merged like any element — inherit-if-absent per property;
nothing is synthesized or normalized (discriminators are synthesized only for entries .NET *invents*).

**Java behavior (mechanism).** After processing the entry, its slicing is **rebuilt unconditionally** as
`type:$this` / CLOSED / unordered (`PPP:595-598`, comment: "type slicing is always CLOSED regardless of what
the differential says"), then a coverage check (`PPP:646-667`) flips it back to OPEN iff some type still
allowed on the entry has no type slice — which is what keeps `obs-2` open. A slice with `min>0` must be the
last differential match (else throw), raises the entry `min` to **literal 1** (not a sum, `PPP:611-616`) and
latches a `fixedType` that strips all other types from the entry (`PPP:638-645`). Over an *already-sliced*
base there is no reopen logic at all (`PPP:1584-1588`) — stays CLOSED. Separately, an auto-added slicing
entry on a repeating element gets `min := sum of slice mins` (`PU:983-1005`; an authored entry gets a
warning instead), and an extension `value[x]` slice keeps an unstated `min=1` where other slices are reset
to 0 (`PPP:801-805`, "hack work around for problems with snapshots in official releases").

**Spec basis.** The published text favors .NET on the type list: R4 and R5 both say type-specific entries
"do not restrict allowed types" and "the original element SHALL always be represented in a snapshot".
Forcing `closed` against an explicit `open` has no basis anywhere. On `min`, profiling §5.1.0.14 gives slice
cardinality arithmetic as *validation* guidance (with its own SHALL/SHOULD contradiction) — it never
instructs a generator to rewrite the entry's declared cardinality. Input-validity wrinkle: the differential's
slicing has neither discriminator nor description (violates the slicing shape rules), so Java can be read as
*repairing* invalid input while .NET *propagates* it.

**Decision needed** — per property of the slicing entry, is a generator *required / permitted / forbidden* to:
(a) synthesize or complete the discriminator; (b) rewrite `rules`; (c) restrict the type list; (d) recompute
`min`; (e) treat unsliced-base and sliced-base type slicings differently (Java does)? And if the differential's
slicing violates the shape rules: normalize-and-repair (Java) or propagate-as-written (.NET)?
**Recommendation:** (a) permitted (a type slicing without a discriminator is unambiguous); (b)(c)(d)
**forbidden** — the snapshot must not contradict what the author wrote, and the spec text already says the
type list is not restricted; validators compute (d). If the committee prefers Java's normalization, the
golden files need re-blessing after the `min := 1` (not sum) and sliced-base asymmetries are fixed.

**Decisions on record (JIRA) — (c) and (d) are decided against the Java behavior.**
[FHIR-12259](https://jira.hl7.org/browse/FHIR-12259) (2016, Persuasive 14-0-18) adopted Chris Grenz's rules:
the `[x]` element "must remain in the profile snapshot as-is" and a type-specific path "shall not be
interpreted as constraining allowed types" (resolution text); FHIR-8969 (2015) resolution lets a slicing entry
carry any constraint allowed when "merely profiling the element", and Lloyd's *comment* on it names the
exception "constraining to a single type for a type slice" as nonsensical. So the entry type-list collapse is
unsanctioned by resolution text, with the sharpest wording comment-sourced. [FHIR-31054](https://jira.hl7.org/browse/FHIR-31054) (2021, 30-0-0,
Firely-reported): the slice-min sum "SHOULD be less than or equal to m" — "Will add a warning to the
validator" — a validator SHOULD, not a generator recompute; so the entry-min raise is unsanctioned. (b):
FHIR-3623/17821 let only the *author* tighten open→closed; nothing lets a generator rewrite an explicit
`rules`. (a): FHIR-31400 (2021, 20-0-4) moved eld-1 to the snapshot so a derived differential may *omit* the
discriminator — implying the generator carries the base slicing forward; synthesis itself is not addressed.

**Prior discussion (Zulip, see Appendix B).** The auto-close is contested on record: Grahame 2024 ("type
slicing is always closed in practice") vs Lloyd 2020 ("It's wrong to auto-change 'open' slices to
'closed'"); Alexander Henket 2025 reports the auto-close breaking a downstream profile in Java. The only
generator-side debate of `rules` is the 2019–2020 #conformance thread "Type[x], Slices, open/closed", where
Grahame's model — a differential's `rules` is "an interpretation hint" for the generator, closed stays closed
— was restated by Ward, objected to by Ewout ("turns from an aspect of the slice to an interpretation hint")
and Chris Grenz ("a diff of open over a base of closed is an error"), and never resolved. Grahame 2023: a
derived profile may not loosen `closed`/`openAtEnd` ("correct"), stated nowhere in the spec.

Per sub-question, the record is: **(a) discriminator injection — permitted.** Grahame 2019-12: omitting it is
allowed ("no. but it's allowed"); Michel records the Redmond DevDays 2019 agreement to "always emit a type
slicing entry to the snapshot" (so this one is settled — the WGM need only confirm). **(b) forcing `closed` —
Java-implementer decision, objected to every time it surfaced, and its author has called the result wrong.**
Nov 2020 (#IG-creation "Validation issue with partial type slicing"): an `effective[x]` diff with one optional
Period slice came out `closed`; Grahame: "this is an issue in the snapshot generator" — but auto-closing exists
"because if it doesn't, every single extension will allow any type" (rescuing R4 extensions written in the
STU3 shorthand); Lloyd: "methodologically wrong to force all type slices to be closed"; after "several days of
work" Grahame pushed fhir-test-cases `acceaea7` — the probable origin of the rebuild-CLOSED-then-reopen
mechanism. In 2020 Java threw "Type slicing with slicing.rules != closed" outright; Patrick Werner: "it's not a
stated constraint … Simplifier/.net … without a problem"; Grahame: "it's not a stated constraint. But how does it
make sense?". Grahame's own 2024 example ("something goes awry") shows `open`→`closed` surprising its author.
**(c) type-list collapse — the renamed-form variant was declared a bug** (Sept 2022 cholesterol snapshot,
"a very unpleasant discovery": the JUnit suite ran with a different slice-processing parameter than
publication — a caution that shipped core snapshots and fhir-test-cases are not the same oracle); the obs-2b
`min>0` collapse was never discussed. **(d) entry-min raise — never discussed.** **(e) sliced-base stays
closed — live exhibit July 2026** (#tooling "Type choice elements issue in instances of derived profiles":
derived profile constrains one of two base type slices, instances of the other type now fail; unresolved).

### Q4. What does a bare type slice mean, and how is the legacy renamed form represented?
*OQ-018, DEV-026*

**Context.** R4 prescribed renamed paths (`Observation.valueString`); R5 requires `value[x]` plus a type slice
(`value[x]:valueString`). Two questions the spec leaves open: does a type slice that states **no explicit
`type`** imply the type constraint? And when a differential still uses the renamed form, which snapshot
representation must the generator produce?

**Reproducing inputs.** `ts-case2` (renamed form; both engines agree structurally);
[`t16`](https://github.com/FHIR/fhir-test-cases/blob/master/r5/snapshot-generation/t16-input.xml) and `t31`
(children constrained under a renamed choice `…extension:latitude.valueDecimal.extension:…`); the implicit
constraint case is code-derived (every shared R5-form test states an explicit `type`).

**.NET behavior.** Renamed form: type parsed from the rename, slice type list reduced to that type
(`applyImplicitChoiceTypeConstraint`, `SnapshotGenerator.cs:2022-2049`), and an explicit type slice
`value[x]:valueDecimal` is **synthesized** to hang the subtree on (t16: bare `value[x]` kept as well).
R5 form without explicit `type`: **no implicit constraint** — the slice inherits the full choice list, so
`value[x]:valueString` is a "string slice" whose snapshot allows every type.

**Java behavior.** Applies the implicit constraint in **both** forms: the type is inferred from the path suffix
*or the sliceName suffix* and stamped on the slice (`diffsConstrainTypes`, `PU:1806-1849`; `PPP:571-572`);
canonical `<stem><Type>` slice names are enforced (auto-set when missing, error when wrong unless
`autoFixSliceNames`). For the renamed form with child constraints (t16/t31) Java anchors the subtree
**directly under bare `value[x]`** — no type slice synthesized; golden blesses this.

**Spec basis.** R5 elementdefinition (#typesx): a type-specific element "constrains the use of a particular
type" — arguably an implied constraint, which would make .NET's R5-form behavior wrong. The spec says nothing
about which snapshot representation a renamed-form differential must yield — and R5 arguably *removed* the
bare-`value[x]` representation the golden files bless (RFC-013).

**Decision needed.** (a) Does `value[x]:valueString` without `type` constrain the type? (b) Given a renamed-form
differential, must a generator synthesize a type slice (.NET), fold onto the unrenamed element (Java), or
reject? (c) Is the `<stem><Type>` slice-name convention normative (eld-16 territory)?
**Recommendation:** (a) yes — one sentence in elementdefinition.html; (b) **no decision-tier source on either
side** — Grahame's 2019 single-type rule (Zulip, below) supports the bare-`value[x]` fold when the element is
single-typed (t16: the base `value[x]` is already `decimal`-only; t31: the diff states the type), while the
"a differential id must reappear in the snapshot" principle comes only from the reporter's description on
FHIR-15900 (2018, an *auto-approved* tooling ticket, no WG vote, below), which would support synthesizing the
slice when the differential used the renamed id. Our proposal: fold when single-typed (no synthesized slice),
synthesize only when several types remain — but the committee must pick, and say which id the snapshot
carries; (c) SHOULD, validated by the generator with a message, not a repair.

**Decisions on record (JIRA).** [FHIR-12259](https://jira.hl7.org/browse/FHIR-12259) (2016): choice elements
are implicitly type-sliced with ids `Patient.deceased[x]:deceasedBoolean`; constraints on a named type slice
"apply only to instances of that type" (so a type slice *is* single-typed — Java's inferred type is right,
.NET's full-list R5-form slice is not); either path form is legal. FHIR-6066/6093 (2015): constraining a
sub-type "does not imply the other sub-types are to be omitted"; renamed snapshot paths lose the choice
identity (this is where `base.path/min/max` came from). [FHIR-15900](https://jira.hl7.org/browse/FHIR-15900)
(2018, resolution "Auto-approved" — a tooling fix, no WG vote): a differential `occurrenceDateTime` with a
snapshot `occurrence[x]` — "an id in the differential that's not in the snapshot — that should not occur" is
the *reporter's* description — which, for what it is worth, cuts against Java's bare-`value[x]` anchoring for
(b) when the differential used the renamed id. FHIR-10034: the type need not be restated with the
shorthand (eld-6/7 relaxed). FHIR-18264 (2018, Not Persuasive): re-slicing a type slice "cannot occur"
(Chris Grenz dissents).

**Prior discussion (Zulip, see Appendix B) — (a) and (c) are answered, (b) has a Java rationale.** The R4
decision (#conformance "Slicing a non-repeating element", 154 messages, mid-2019; FHIR-12259 → FHIR-33233):
the *entry's* allowed types are never narrowed by a type slice — Lloyd: a `valueQuantity` row "constraints the
valueQuantity slice. It doesn't prevent the other types", "a very clear outcome of our meeting"; Chris Moesel
2022: "as of R4, slicing *never* implicitly limits the allowed types". But the *slice itself* is by
definition single-typed — Chris Grenz: "Type slices implicitly exist"; Grahame: "the path
Extension.valueQuantity.value establishes the implicit type slice". So Java stamping the inferred type on a
type-less R5-form slice matches the community position and .NET's full-choice-list slice is the outlier.
Slice names: Lloyd asked for an error on non-canonical names and Java switched from silently rewriting to
throwing on 2019-07-25 (t29a/t43a). For (b): Grahame 2019 — "Either you have a single type, or you can only
talk about element properties" (children under `[x]` require a single type); Michel 2017 — a single-type
constraint "does not introduce an actual slice entry" (the original .NET design, which DEV-026's synthesized
slice now contradicts). The long id form `value[x]:valueCoding` is required (Lloyd 2021, FHIR-33233); renaming
is tolerated in differentials ("you can continue to get away with it") and SUSHI 3.0 dropped it.

### Q5. The generator contract: what must a generator detect, and what may it never do?
*OQ-011, OQ-014, DEV-027, DEV-028, DEV-034(b)(c), DEV-035, RFC-012*

**Context.** The spec has shape rules (constraint profiles cannot add paths; differential order follows the
base; min/max may only tighten; †-frozen properties) but states **no consequence for violations** and no
generator obligation. The two implementations chose opposite ends of "reject vs propagate", and neither is
uniform.

**Reproducing inputs.** The 21 `fail="true"` tests in the shared suite. Java satisfies the fail expectation on
**21/21**; .NET on **8/21** — it silently generates on 13, and on two of those the output is *corrupt*:
- [`t23a`](https://github.com/FHIR/fhir-test-cases/blob/master/r5/snapshot-generation/t23a-input.xml)
  (out-of-order differential): Java drops the skipped row with ERROR "No match found … (including order)";
  .NET emits a **duplicate** `Patient.contact:males.telecom` (duplicate element id) with a **fabricated
  `base.min`** taken from the differential. No diagnostic. (`t23` is the identical differential with
  `sort="true"`, repaired by the driver's `sortDifferential` — a tooling-side normalization .NET lacks.)
- [`obs-unit`](https://github.com/FHIR/fhir-test-cases/blob/master/r5/snapshot-generation/obs-unit-input.json)
  (`Observation...unit`, a `..` path): Java throws "Invalid path … name portion missing"; .NET emits a phantom
  element `Observation.` and **silently drops the author's `fixedString`**.

**Absent checks in .NET, present in Java** (each silently accepted by .NET): type-slice naming convention
(t29a/t43a); fixed/pattern value type vs element type (`obs-badfixed`/`obs-badpattern`); type/targetProfile
derivation (`ihe2`, `mi-use-distinct`); root-element invariants sdf-15a/20 (`ext-recursion-1`, `ext-ccuk`);
slicing a non-repeating element (`simplifier-1`); mustSupport `true→false` (`obs-ms-bad`); SD `type` vs base
type coherence (`t29b`).

**Four enforcement postures for the same class of illegal input** (min/max loosening, mustSupport
`true→false`, †-frozen properties):

| posture | who | example |
|---|---|---|
| silent-keep-base | .NET | loosening `min`/`max` silently ignored (`ElementDefnMerger.cs:666,696`) |
| warn-and-take-diff | Java | illegal min/max/mustSupport → ERROR message **and** the illegal value lands in the snapshot (`PU:2757-2892`) |
| silent-drop-diff | Java | `isModifier` (outside extensions), `defaultValue`, `meaningWhenMissing`, `representation`, `code`, `sliceIsConstraining` never merged (`PU:2906` comment) |
| abort | Java | any change to `isSummary` when the base has a value → `throw new Error` (`PU:3039-3048`) |

.NET's `ElementDefnMerger` is diff-wins for nearly everything, including every †-frozen property.

**Unmatched differential rows.** Java walks the *base* and queries the differential per base row; a row no base
row pulled in is an **orphan**: ERROR + dropped for constraint profiles, appended for specializations
(`PU:842-867`, `908-948`) — the spec's own constraint/specialization split, implemented literally. .NET walks the
*differential*; a row with no base match is silently a **New** element for every derivation
(`SnapshotGenerator.cs:887`).

**Error taxonomy.** Neither side's mapping of error → (throw / message+drop / repair / silent) is derivable from
the spec. Java additionally surfaces author-legal input as JVM `java.lang.Error` in 30 live sites (two
`type.profile`s on a new slice `PPP:1419`; a slicing entry with children on a base that already has children
`PPP:382` "please report issue to grahame@fhir.org"; root `type`), which bypass the `catch (Exception)` that
nulls a half-built snapshot (`PU:1078-1084`). .NET reports several shared throws as "Internal error in snapshot
generator". Java's report-vs-throw split is a constructor argument (`messages == null`), not a policy.

**Spec basis.** elementdefinition #path (constraints cannot define new paths); structuredefinition §5.4.6
(ordering); the † and one-directional rules in the interpretation table — all without stated consequences.

**Decision needed.**
- (a) A minimal normative contract (RFC-012): a generator SHALL NOT produce a snapshot that contradicts the
  differential or the base without a diagnostic — i.e. "reject, repair-with-message, or propagate — but never
  silently corrupt or silently drop". Acceptable as a floor?
- (b) Which author errors is a generator **required** to detect (path grammar? order? new paths in constraint
  profiles? min/max loosening? †-frozen changes?), versus validator territory?
- (c) For one-directional rules violated by the differential: keep base (most restrictive), take diff with
  error, or refuse?
- **Recommendation:** (a) yes; (b) required: path grammar, new paths in constraint profiles, ordering, min/max
  loosening, †-frozen changes — everything checkable from base + differential alone; (c) take the *base*
  value **and** emit an error (never silently either way). Also: state that a generator SHALL NOT emit
  duplicate element ids or fabricate `base` (sdf-8b integrity). Note what this asks: every HL7 enforcement
  decision on record has placed the check in the *validator* as a warning (JIRA paragraph below) — (b) asks
  the committee to assign the generator a detection duty for the first time, on the argument that these
  checks need only base + differential and that the validator cannot detect what the generator has already
  silently normalized away.

**Decisions on record (JIRA) — every enforcement decision lands in the validator, as a warning.** FHIR-7800/
7802 (2015): illegal cardinality / binding-strength loosening was being published in snapshots — tooling
"fixed so that they check and generate warnings"; FHIR-17469 (2018): closed-slicing arithmetic fixed in the
*validator*; FHIR-31054 (2021, resolution text): slice-min sum → "Will add a warning to the validator";
FHIR-13461 (2017, *retracted* → org.hl7.fhir.core#518, so discussion-tier only): openAtEnd misuse → build
warnings; Grahame on FHIR-50267 (*comment*, not resolution): "too late to make this a SHALL … settle for a
validation warning"; FHIR-37692 (2022, 18-0-0): constraint keys unique per SD, enforced "with the tooling". There is **no HL7 precedent for a generator throwing or repairing** — Java's throw census
and .NET's silent-keep-base are both without a mandate. FHIR-20405 (2019): a reporter noted "snapshot
generation just ignores the illegal setting in differential" (isModifier false over true) — no ruling.
FHIR-31405 (open) is where an answer should be posted.

**Prior discussion (Zulip, see Appendix B).** The normative-contract question already has an on-record
disagreement: Marten Smits 2022 — snapshot generation "I thought was 'normative' by now, or at least, it
should be"; Lloyd — "Snapshot generation is a 'process'. It doesn't get to be 'normative'". Lloyd 2026: the
shared test cases "define the rules for snapshot generation and validation"; formal rules in the spec were
considered but "they get super ugly". Grahame 2020: "there is a set answer, represented in the snap shot
tests"; GF#9079 (2016) asked for the algorithm to be written down (Ewout: "possible, but a daunting task").
Grahame's 2022 property-by-property list of what profiles may change ("fixed — profiles can introduce but
not change"; "sliceName / sliceIsConstraining — can be changed"; "constraint / alias / code — can add") is the
closest public statement of the one-directional rules and was never turned into spec text.

**The stated policy is .NET's; the practice is Java's.** Grahame 2022 (#conformance "Modifier Extension
mismatch", the one explicit where-in-the-stack debate): the generator misses many things because "it's task is
to generate the snapshot, not validate the profile" — the check went to the *validator*; Grahame 2024: an
illegal type restatement is an error "though that doesn't mean that the snapshot generator should fail". Yet
Java throws in at least seven Zulip-attested places, several by explicit decision: wrong type-slice names
(2019-07, at Lloyd's request), `rules != closed` on type slicing (2020), discriminator equality with the base
(2022, defended: "the purpose of the diff is to repeat enough information to connect the dots"; Chris Moesel
objected with spec text), mustSupport `false` over `true` (a hard `DefinitionException` in validator 5.6.35,
2022 — now warn-and-take: undocumented drift), `isSummary` (`java.lang.Error`, 2021→2025, "you can't change
isSummary in a profile"), named slices out of order (2024: Grahame — "sushi is clearly not wrong here, against
the spec" but "the existing java code doesn't work that way"), path-not-in-base from the sort step (2017→2022;
a 2021 user report of profiles validating clean on Simplifier and aborting in Java). Michel 2017 stated the .NET
side: slicing-entry constraints "SHOULD be enforced! This is the responsibility of the validator". Both engines
profess "don't validate"; both breach it, in different places.

---

## Tier 2 — spec clarifications (one sentence each would settle a divergence)

### Q6. `Extension.url` fixed value in snapshots
*DEV-037, RFC-015*

**Context.** defining-extensions §2.1.5.1.4 lists, as authoring *guidance*, that an extension definition's
`Extension.url` has "value = canonical URL (fixed)" (no SHALL); extensibility §2.1.5.0.1 states the url rule
for *instances* (absolute canonical; in-line parts of complex extensions relative). Nothing says whether the
fixed value must be in the differential or may be filled into the snapshot by a generator, which property
carries it (`fixedUri` vs `pattern[x]`), or what a nested part's `url` fixes. The R6 build does settle one
sub-question: a derived profile on a complex extension "is not establishing the 'url' value", so a derived
extension profile inheriting its base's fixed url is correct.

**De-facto convention.** hl7.fhir.uv.extensions 5.2.0: **632/632** definitions fix `Extension.url` to the
canonical in both differential and snapshot; **272/272** nested in-line parts fix a relative local name (270 =
slice name, 2 another authored name; zero `patternUri`). The fixed value is universally expected — but
because it is authored everywhere, the pack is neutral between the two engines.

**Reproducing inputs.**
[`ext-recursion-2`](https://github.com/FHIR/fhir-test-cases/blob/master/r5/snapshot-generation/ext-recursion-2-input.xml)
and `au2` (definition roots: input **and expected** carry no `fixedUri`); eight profile extension slices with
`type.profile` set and no fixed url in the expected files (`ca-patient` `…extension:myExtension.url`,
`telus-oo`, …);
[`ext-sort-issue`](https://github.com/FHIR/fhir-test-cases/blob/master/r5/snapshot-generation/ext-sort-issue-input.xml)
(the one expected `fixedUri` is inherited from the base extension — correct per the R6 text).

**.NET behavior.** After every element's child merge, an `extension`-named element whose `url` child has no
`fixed[x]` gets `fixedUri` = the SD canonical (extension root) or the type-profile url / slice name (profile
extension slice; nested parts get the relative name) — `SnapshotGenerator.cs:1743-1785`. Never for
`modifierExtension`.

**Java behavior.** Never writes a fixed url (`setFixed(` occurs only in the legacy xver template synthesizer,
`PU:4715`). Extension definitions carry the fixed url only if their differential states it (FSH / IG Publisher
authoring does); constraint profiles inherit whatever the extension's snapshot has. Golden files bless
fixed-less extension snapshots.

**Consequence.** The same extension definition snapshotted by the two engines differs on `Extension.url`
whenever the author omitted `fixedUri`; validation of instances against the .NET snapshot is stricter.

**Decision needed.** Is the fixed url part of the snapshot *contract* — should a conforming snapshot of an
extension definition carry `Extension.url.fixedUri` (canonical at the root; local name for in-line parts;
referenced canonical for by-reference parts) regardless of whether the author stated it?
(A) .NET: the generator SHALL synthesize a missing fixed url. (B) Java: only authored or inherited fixed
urls appear, and §2.1.5.1.4's guidance becomes a SHALL on authors.
**Recommendation:** promote §2.1.5.1.4 to conformance language either way ("an extension definition SHALL
fix `Extension.url` (as `fixedUri`) to its canonical; in-line nested parts SHALL fix their local name;
by-reference parts SHALL fix the referenced canonical; a generator MAY supply missing values and SHALL NOT
alter inherited ones") — that is RFC-015. The pack practice favours "it belongs in the snapshot"; the Java
goldens (`ext-recursion-2`, `au2`) are the only artifacts that force the A/B choice.

**Prior discussion (Zulip).** Nothing asks whether a *generator* should add the fixed url. The convention is
treated as an authoring obligation enforced by the Java validator (Elliot Silver 2025: `patternUri` → "The
value of Extension.url is not fixed to the extension URL"; publisher complains when not `fixedUri`) — which
explains why Java never synthesizes it and why no golden test exercises synthesis. The `fixedUri` type was
settled in 2019 (Michel ↔ Grahame ↔ Lloyd, "Extension.url actually requires a fixedUri value"). SUSHI 2.8.0
"keeps parent's fixedUri on Extension.url of child extensions" — consistent with the R6 text.

### Q7. contentReference: three versions, three behaviors — and what a generator does with it
*ch8, DEV-023, OQ-004*

**Context.** R4 says nothing about constraints on recursive elements; R5 profiling §5.1.0.10 says constraints
on `Questionnaire.item` propagate to `item.item`, `item.item.item`, …; the R6 build **flips again**:
constraints apply to the literal path only, with a new `contentReferenceProfile` extension to opt in to
propagation. R6 also promotes "contentReference only in specializations" to an invariant (sdf-30).

**.NET behavior.** Dereferences into the *core* structure (not the profile) when children are constrained,
drops the `contentReference` and restores the target's `type` on the expanded element (issue #3177), copies
none of the eld-5 value-domain properties. R5 §5.1.0.10 propagation is **not implemented** — .NET is closest
to the R6 literal-path semantics and never matched R5's text.

**Java behavior.** Three path-dependent behaviors for a referencing element: the two step-in paths do what
.NET does (reference nulled, type restored, `replaceFromContentReference` `PU:1870-1874`); the sliced-base
path keeps the reference and clears `type` (`PPP:1477-1479`, and only on the last-emitted element of that
path, so sibling slices can differ); the slicing-entry inline dump keeps the reference *and* materializes the
target's children (Q2). All local `#Observation.referenceRange` references in a constraint profile's snapshot
are rewritten to absolute form at id-generation time with a **hard-coded core namespace**
(`http://hl7.org/fhir/StructureDefinition/<type>#…`, `PU:4359-4363`; SD url only for logical models) — .NET
leaves the base's local form except inside merged subtrees. §5.1.0.10 propagation is implemented by **neither**
engine.

**Spec basis.** eld-5 (no type/fixed/pattern/binding/… on a reference), "cannot be changed and always reference
the non-constrained definition", §5.1.0.10 (R5), R6 literal-path + `contentReferenceProfile`.

**Decision needed.** (a) Confirm R6 direction (literal path, opt-in propagation) — and is R5's automatic
propagation then an erratum nobody implemented? (b) After expanding children, does the `contentReference`
survive on the expanded element (Java sliced-base) or is it replaced by the target's `type` (.NET, Java
step-in)? (c) Local vs absolute form in a constraint profile's snapshot: is absolutization required, and to
which canonical (the core type's, or the base profile's)? (d) Are the eld-5 properties "undefined after
dereferencing" (both engines copy none)?
**Recommendation:** (a) R6 direction — decided in resolution text (FHIR-57266, Applied), so **confirm only**,
and document it as a version-specific behavior; (b) replaced by `type` (a snapshot element with both children
and a reference is contradictory under eld-5); (c) **answered on Zulip only, never in JIRA** — Grahame 2022:
"the relative content references must be replaced with absolute content references when a snapshot is
generated" (Java's global rule; .NET's merged-children-only rewrite is the deviation, fixed for reading in
firely-net-sdk #2039) — ask the room to confirm, and ask *which* canonical for elements copied out of a base
profile; (d) yes, state it.

**Decisions on record (JIRA).** [FHIR-14958](https://jira.hl7.org/browse/FHIR-14958) (2018, 7-0-0): a
contentReference "bring[s] across all the rules … including bindings, invariants etc.", only in
specializations, "cannot be changed and always reference the non-constrained definition" (Michel's comment:
resolving from the *referencing* profile would recursively inherit constraints — wrong). FHIR-13139 (2017):
tooling fixed so children of a contentReference element can be constrained. FHIR-39350 (2022, Not
Persuasive): contentReference stays legal on non-backbone elements (bindings by reference). The R6 flip has
ticket numbers: [FHIR-57266](https://jira.hl7.org/browse/FHIR-57266) (2026-05, Applied, 6-0-0): constraints
"only apply to the literally stated path"; [FHIR-57265](https://jira.hl7.org/browse/FHIR-57265) (Resolved –
change required): `contentReferenceProfile` 0..1, "a profile on the element identified by the ContentReference
that also applies to this element". Nothing anywhere on (b) survival or (d) eld-5 after dereferencing.

**Prior discussion (Zulip, see Appendix B).** Three eras, three answers. 2021 (#IG-creation "Clarification
on contentReference", Grahame agreeing with Ewout): profile constraints on `Questionnaire.item` do **not**
apply to `item.item` unless the profile refers to itself via the profile-element extension; SUSHI implemented
that. 2023: Chris Moesel notes R5 §5.1.0.10 (FHIR-39753) contradicts the 2021 agreement; Grahame reads it as
"an unprofiled nested level is profiled by the root, an explicit profile overrides". 2026 (#implementers
"Recursive schemas validation"): Gino wants non-recursive by default with an opt-in; Lloyd: making recursion
the default now "could be breaking … treat recursive as the exception"; outcome FHIR-57265 (`contentReferenceProfile`
extension) + FHIR-57266 (reword §5.1.0.10) = the R6 flip. On (b): Ewout 2025-06-18 stated the unwritten
rule "you EITHER have a contentReference or children, but not both" and announced the .NET change (#3177);
in the same Consent case the Java snapshot *lacked* the reference on an unexpanded `provision.provision`
while .NET kept it — a field data point for the path-dependence. Michel 2018 posed the two resolution
options (same profile → cascade; core definition → per-level) that the three eras have oscillated between.

### Q8. Starting a type profile below its root: `elementdefinition-profile-element` vs `url#fragment`
*OQ-017, DEV-018*

**Context.** R5 profiling §5.1.0.16 documents the `elementdefinition-profile-element` extension (on
`type.profile`) nominating an element **id** in the target as "an instruction to a validator to apply the
profile starting at the nominated element". The older `url#name` fragment syntax also circulates.

**.NET behavior.** Never reads the extension. Supports only the fragment syntax, and the expansion path for it
was broken (id-vs-sliceName confusion; fixed in [#3583](https://github.com/FirelyTeam/firely-net-sdk/issues/3583)
by resolving the fragment as a slice name).

**Java behavior.** Reads the extension, by element **id**, for template selection (`PPP:734-749`, Extension/
Resource-typed bases), the type-compatibility check (`PU:1650-1662`) and the final type sweep — but **not** for
the children walk-in, which opens the profile at its root (`PU:2673` todo: "should we change down the
profile_element if there's one?"). The `#fragment` form is **silently stripped** by `findProfile`
(`PU:4100-4102`). Disjoint syntaxes; no shared test covers either.

**Decision needed.** (a) Both forms are HL7-sanctioned and both name an element **id** (FHIR-13973 for the
fragment, FHIR-49079 for the extension — see below): is the `url#id` fragment now *superseded* by the extension
(so generators may reject or warn on it), or must both be supported? Either way the fragment names an id, not a
slice name. (b) Generator obligation: must the snapshot expand the profiled element's children from the
nominated sub-tree (neither does today), or is this validator-only? (c) A shared test would settle the expected
snapshot shape.
**Recommendation:** (a) declare the extension canonical and the fragment deprecated-but-accepted (by id), since
the fragment was never documented (FHIR-13386); (b) if Q1 answers (A), then yes, from the nominated element.

**Decisions on record (JIRA) — both syntaxes are sanctioned, and both address an element id.**
[FHIR-13973](https://jira.hl7.org/browse/FHIR-13973) (2017, 11-0-0) blessed the fragment form as "(as yet
undocumented) functionality": `type.profile` may point into a StructureDefinition "by appending a # and the
**id** of the element"; FHIR-13386 (2017): "the feature is currently undocumented. We need an example" —
assigned, never documented. [FHIR-49079](https://jira.hl7.org/browse/FHIR-49079) (2025, 10-0-0) redefined the
extension: "Provides the snapshot.element.id of the element … to use as the starting point for validation" —
by id, framed as a validator instruction. Consequences: .NET's sliceName comparison of the fragment is wrong
(it is an id), and Java's silent strip of `#fragment` drops an HL7-sanctioned form. Only the retracted
FHIR-13839 note ("generators should be sure to generate the snap shot completely") speaks to the generator
obligation. So the question sharpens to: is the fragment form superseded by the extension, and must children
be expanded from the nominated sub-tree?

**Prior discussion (Zulip).** Grahame 2021 gave the worked example (`Composition.section` with
`type.profile` = the profile itself + `profile-element = "Composition.section"`: "each section must have a
section, recursively"); semantics: the type must still match, the decision is based on the nominated element,
constraints on its subpaths apply ("yes"); Ewout: "Quite elegant." Lloyd 2021 restricted the intent to
backbone elements (SUSHI followed). FHIR-39384 (2022, High): "Extension: profile-element doesn't work";
2024–25 reports: works for sub-element slices, not for slices on the nominated element itself. The
`url#fragment` syntax has **zero** presence on Zulip — but see the 2017 JIRA blessing above.

### Q9. Which extensions are non-inheritable in snapshots?
*OQ-019, DEV-022*

**Context.** A derived profile's snapshot inherits everything from the base — including metadata extensions
that are about the *base* (maturity, standards status, work group, normative version). Both engines strip a
hard-coded url list; the lists do not agree; the spec never mentions inheritance policy.

**Evidence.** Java `NON_INHERITED_ED_URLS` (`PU:232-249`, 15 urls) vs .NET's blocklist (18 urls incl. its own
marker, `SnapshotGeneratorExtensions.cs:137-156`): **agree on 8** (isCommonBinding, fmm, standards-status,
category, security-category, wg, normative-version, summary); **Java-only 7** (tools/binding-definition,
tools/no-binding — ~770 property diffs in the shared suite —, implements, explicit-type-name,
obligation-profile core+tools, standards-status-reason); **.NET-only 9** (fmm-no-warnings, hierarchy,
**interface**, applicable-version, codegen-super, replaces, resource-approvalDate/-effectivePeriod/
-lastReviewDate). Java additionally has three more policy lists (non-overriding / overriding /
default-inherited) and a per-extension `snapshot-behavior` declaration — i.e. it already treats inheritance
policy as **extension metadata**.

**Decisions on record (JIRA) — mechanism *and* classification are resolution-tier.**
[FHIR-28441](https://jira.hl7.org/browse/FHIR-28441) (Ewout, 2020, Persuasive 11-0-0) defines the
`snapshot-behavior` extension "that indicates what rules a snapshot generator must follow", with five classes
(add-to / override-subset / override-free / must-equal-propagate / **does not propagate**). The resolution
text says "The rules will be those listed in the comment below" — so Lloyd's classification of ~45 core
extensions is *incorporated by reference into the decision*, not an opinion. Its class-5 list of 17 urls **is
.NET's blocklist verbatim** (fmm, fmm-no-warnings, hierarchy, interface, normative-version, applicable-version,
category, codegen-super, security-category, standards-status, summary, wg, replaces,
resource-approvalDate/-effectivePeriod/-lastReviewDate, isCommonBinding); Java's list carries only 8 of the
17 and **omits nine** (the nine ".NET-only" urls above) — a deviation from decision text, not from a
preference. The record conflicts on `explicit-type-name`: class 4 "always propagate" in the 28441 decision
vs "should not inherit" in the *comments* closing FHIR-27535 as its duplicate — Java follows the comment-tier
source. What is missing is not a decision but uptake: no extension definition carries the metadata.

**Prior discussion (Zulip) — this is decided but stalled.** Ewout proposed in 2020 that "the author of the
extension has to indicate whether this extension propagates" (#tooling "Forge added extension
explicit-type-name"); Grahame agreed ("an extension on the extension definition … a list in the short
term"); FHIR-28441 was resolved with `structuredefinition-inheritance-control` (extensions pack) and the
tools IG has `snapshot-behavior` — but as of 2026-08-19 (Chris Moesel) "there wasn't a single extension that
actually used them"; Grahame 2026-08-18: "it's something we should do. I think that the snapshot generator in
the validator follows them". Ward 2025-12: .NET's list was copied from Java's (firely-net-sdk PR #2886).
Grahame posted his working per-context lists in Feb 2023 (SD / ED / Type / Binding — a third list to
reconcile). Field reports keep arriving (CRMI `artifact-author` inherited, Aug 2026).

**Decision needed.** Not whether — *who applies the existing metadata to the core extensions, and when*, so
that both generators can retire their lists. Interim: agree one list (union of both minus tooling-only urls).
**Recommendation:** ask FHIR-I to stamp `structuredefinition-inheritance-control` on the extensions pack in
the next pack release, **starting from the FHIR-28441 classification itself** (it is decision text — the 17
class-5 urls are already decided), and using the Java, .NET and Grahame-2023 lists only to classify the
extensions created after 2020 that 28441 could not cover. Interim, until the pack carries the metadata: both
engines adopt the 28441 class-5 list verbatim (i.e. Java adds the nine it omits), and `explicit-type-name`
gets an explicit ruling (28441 says propagate; the 27535 comments say not).

### Q10. What does a StructureDefinition without a differential mean?
*OQ-016*

**Context.** Never stated. Java accepts it consistently in both roles (snapshot = base copy; as a type profile,
its root is its snapshot's first element). .NET accepts it for generation (synthesizes an empty differential)
but *refuses* it as a type profile ("profile has no differential", `SnapshotGenerator.cs:2391-2396`, with a
TODO acknowledging the gap).
**Decision needed.** State that a differential-less constraint SD is legal and means "no constraints
beyond the base" (snapshot = base snapshot with rebased paths/ids + fill obligations).
**Recommendation:** yes, one sentence in structuredefinition.html.

### Q11. Element ids and input mutation: may a generator discard author ids and normalize the differential?
*OQ-009, OQ-015*

**Context.** elementdefinition says ids "may be used as the target of external references"; the element-id
algorithm is fully specified. **Both engines regenerate every id** from path + slice names, overwriting
author-chosen ids without a message (.NET `SnapshotGenerator.cs:1063-1066` "Ignore user-specified element
id's"; Java `setIds(derived, false)` `PU:886`). Cosmetic difference: Java maps `_`→`-` in id segments
(`fixChars`, `PU:4375`). **Both also mutate the caller's differential**, differently: .NET writes generated
type-slice names and a repaired root `sliceName` into the shared element instances; Java regenerates the
*differential's* ids and absolutizes its local contentReferences at `setIds` time (`PU:4257-4260`,
`4359-4363`), plus an opt-in root-type repair. Java's CLI runs with `autoFixSliceNames(true)`.
**Decisions on record (JIRA).** Ids are derived data: FHIR-9843 (2016) — id = path + slice name, "must be
present and distinct within the profile", usable as `#` anchors; FHIR-12182 (2016 WGM) — `pathpart:slicename/
reslicename`; FHIR-20465 (2019) — differential and snapshot ids must agree ("the snapshot needs to be
corrected"); FHIR-14091 (2017, comment): "If the ids weren't correct, the publishing process will 'fix' them.
This is expected behavior." Nothing on repairing *constraint content* of the input.

**Prior discussion (Zulip) — (a) is answered, (b) was agreed the other way in 2019 (a Zulip agreement, evidenced
by t29/t43 becoming fail tests — no JIRA resolution).** Ids: Grahame 2019
"ids are derivative"; Michel: .NET generates ids "but does not use/depend on them", Grahame: "Same as java";
Firely is removing the `GenerateElementIds=false` option (Ward 2024). Nobody has ever argued for preserving
author ids. Slice-name repair: in July 2019 (#conformance "Slicing a non-repeating element") Chris Grenz
objected to Java silently rewriting a wrong type-slice name in the snapshot while leaving the differential
("identifier inconsistencies between snapshot and differential"); Lloyd: "raise an error if the differential
slice name is wrong"; Grahame the same day: "The generation now blows up if the slice names are wrong in the
differential" (t29/t43 became fail tests). .NET's default in-place repair and Java's later `autoFixSliceNames`
CLI flag both contradict that decision.

**Decision needed.** Confirm (a) ids are derived data — one sentence in elementdefinition.html. For (b):
does the 2019 "error, not repair" Zulip agreement still stand, and if so should it be written down (and Java's
CLI default `autoFixSliceNames=true` revisited)? Which normalizations *are* permitted silently (id
regeneration, `[x]` path normalization, contentReference absolutization)?
**Recommendation:** (a) yes; (b) reaffirm 2019 — repair only with a message, never silently; canonical-form
normalizations permitted.

### Q12. Slicing shape rules: generator or validator?
*OQ-003, OQ-005, OQ-006, DEV-036*

Three rules the spec states about slicing, and whether the *generator* has any obligation:
- **Slicing a non-repeating element** (profiling §5.1.0.13 presupposes repetition). **Answered as
  "not allowed":** FHIR-28619 "Allow slicing of a non-repeating element to define a choice" (Rob Hausam,
  2020) closed *Resolved – No Change*; Grahame 2020: "I don't really see the use case"; Chris Moesel 2022/23:
  "not a legal use of slicing" (though "Forge seems to allow it"); Grahame posted Java's live error in March
  2026. Dissent on record: Lloyd ("I am in favor of allowing"), Rob Hausam, Chris Grenz ("honestly thought we
  decided to allow"), Firely (.NET's reject is compiled out, `REJECT_SLICE_NONREPEATING_ELEMENT`; it slices
  anything). Java rejects (`PPP:309-312`) with two **undiscussed** carve-outs: the slices' total capped to 1,
  or type slicing. Remaining question: are those carve-outs sanctioned — a derived profile limiting a sliced
  base element to `0..1` must remain legal, and IPS Allergy slices `code 1..1` by pattern with a happy
  validator (Lloyd 2021). FHIR-28619's resolution (17-0-0) concerns a *base* `max=1` only: "for
  non-repeating elements, slicing is *only* presently supported by type"; the derived-profile cap is not
  addressed.
- **`slicing.rules` / `ordered` lattice** (§5.1.0.17: open→closed, ordered false→true, derived slicing repeats
  base discriminators; `openAtEnd` never addressed — RFC-011). .NET never reads `rules`/`ordered`. Java
  enforces a partial lattice only when the differential restates the entry over a sliced base: `ordered` may
  not change in *either* direction (stricter than the spec), base discriminators must be an ordered prefix,
  CLOSED→OPENATEND is *tolerated* (a loosening the spec does not sanction), the rules check is skipped for
  choice elements; appending a slice to a closed base slicing throws except on `[x]` paths. Neither engine
  implements §5.1.0.17 as written. Prior discussion: Grahame 2023 confirms no loosening ("correct"); Lloyd
  2017: the validator "doesn't distinguish between openAtEnd and closed" — `openAtEnd` ordering has been
  unenforced everywhere since (RFC-011); general generator enforcement never discussed.
- **`sliceIsConstraining`** (Trial Use; "an ancestor profile SHALL have a slicing definition with this
  name"). .NET enforces it during slice matching (disagreement → element discarded with an issue); Java never
  reads the property and additionally drops an authored value from its output. Origin ticket
  [FHIR-13545](https://jira.hl7.org/browse/FHIR-13545) (San Diego 2017): purpose "Allows detection of a
  situation where an ancestor profile adds or removes slicing" — detection language, i.e. validation; no
  generator obligation stated. Zulip mentions the property substantively once (Grahame 2022: "can be changed
  by profiles").
**Decision needed.** For each: generator SHALL enforce / MAY enforce / validator-only. And for `sliceIsConstraining`:
is it a generator input at all?
**Recommendation:** generator SHALL check the lattice and `sliceIsConstraining` (both are decidable from base +
differential) and report, never repair — this is the same first-ever generator detection duty Q5(b) asks for,
against the same validator-warning precedent noted there, so decide it once under Q5 and apply it here. On
the non-repeating rule: ask the committee to sanction Java's two carve-outs explicitly (slice total capped to 1;
type slicing) — note that the first goes *beyond* FHIR-28619's resolution, which covers a base `max=1` and
only sanctions type slicing there; the derived-profile cap case has never been ruled on.

### Q13. Per-property merge semantics the spec should state (confirmations for RFCs)
*OQ-010, OQ-012, OQ-013, DEV-034, RFC-008/009/010*

The interpretation table's footnotes state obligations but no merge rules; the two engines disagree on the
following. Each needs one sentence; we ask only for confirmation of a direction.

| property | .NET | Java | proposed rule |
|---|---|---|---|
| `"..."` prefix on `definition`/`comment`/`requirements` = append to inherited text | supported (byte-identical algorithm) | supported (same; also attempted on `label` but broken, filed #2592) | **document the convention** or deprecate it (OQ-010; never discussed on Zulip; FHIR-8182 (2015, Not Persuasive) acknowledged it as applying to "elements with a 'string' data type in StructureDefinition" — broader than either engine's three properties — and declined to document it) |
| `fixed[x]`/`pattern[x]` when the base has one | partial *overlay* (diff properties win, rest inherited → a value neither profile stated) | wholesale replace + type check | Grahame 2022: "fixed — profiles can introduce but not change" (so a changed fixed value is illegal outright; pattern line garbled) — state it; if change is legal, replace wholesale (OQ-012) |
| `elementdefinition-suppress` (remove inherited mappings/examples) vs "complete verbose snapshots" | honored behind a setting | always on, incl. `$all` wildcard and SD-level mappings | **answered**: FHIR-I 2022 agreed removal via tooling extension (Lloyd: "remove entirely"); golden `address-no-examples` blesses snapshot-time deletion (FHIR-56831 documents `$all`); the profiling.html sentence is stale (OQ-008) |
| restated inherited `constraint.key` (eld-14 uniqueness vs ∆-additive) | overlay onto the inherited constraint | diff constraint **silently dropped** | pick one — proposal: replace by key, with message (RFC-009); FHIR-37692 (2022, 18-0-0): keys "SHALL be … unique within a StructureDefinition", tooling-enforced — supports replace-by-key |
| `binding` | per-sub-element overlay, lattice not enforced | rebuild; inherited `description` + binding extensions **dropped**; `required` lattice enforced with ERRORs | per-sub-element overlay, lattice enforced (RFC-010) |
| `code` | union | never merged | union (§5.1.0.8 says add/removable) (RFC-008) |
| `example` | union by label | union by label+value | state the key |
| ED-level `modifierExtension` | merged by url like extensions | never merged | is inheriting a modifier extension into a snapshot element sanctioned at all? (OQ-013) |
| `isModifier`/`isSummary`/`defaultValue`/`meaningWhenMissing`/`representation` (†-frozen) | diff wins | frozen by omission; `isSummary` aborts | frozen; violation = error (feeds Q5) |
| `mapping` | union on identity+map | R5+: comma-append per identity (`MappingAssistant`); SD-level identity reconciliation Java-only | R5 text says replace-by-identity — decided in [FHIR-34434](https://jira.hl7.org/browse/FHIR-34434) (2021, 11-0-0): a differential mapping with the same identity "*replaces* the mapping element(s) in the parent"; **neither engine implements it** — confirm, then both fix |

**Answered (no question needed):** base profiles' **SD-level `mapping` declarations** propagate into the
derived SD at snapshot time — Lloyd 2024: "Mappings definitely inherit. They always have"; Grahame fixed the
Java duplication 2024-08-23 and added suppress support for `StructureDefinition.mapping`; Ward's summary
"Snapshot generation should always propagate mapping definitions" confirmed by Lloyd; tests in fhir-test-cases
(PR #188, `r5/snapshot-generation`). .NET does not merge them (OQ-007) — a .NET to-do, not a WGM item.
Also on record: Grahame 2019 on ED-level `modifierExtension`: "we have no concrete examples", support "by
specific request" (OQ-013).

### Q14. Java-only merge inputs: must a conformant generator honor them?
*DEV-032*

`structuredefinition-additionalBase` (Java's preprocessor merges a second base profile's differential in,
with its own differential×differential merge table — `PRE:399-531`), `inherit-obligations` / obligation
profiles (Java sets `mustSupport=true` where any obligation element has it, `PU:2529-2582`),
`imposeProfile`/`compliesWithProfile` (read by neither for snapshot shaping; Java uses `imposeProfile` only
in a targetProfile derivation check, `PU:3333`), `structuredefinition-interface` (no generator handling in
either beyond .NET skipping interfaces in base resolution). Shared tests exercising them (`multi-profile`,
`profile-patient-op3`, `mi-use-*`) are Java-only passes. **Question:** which of these extensions carry
*generator-affecting* semantics a conformant generator must implement, and where is that stated (the
extensions pack defines them; the core spec never mentions their snapshot effect)?

### Q15. Recursive extension definitions and the recursion guard
*DEV-029*

`ext-recursion-1` (fail test): an extension whose differential **root** carries `type.profile = its own url`.
Java rejects structurally ("Type on first differential element!", sdf-15a); .NET accepts silently (root types
never validated). `ext-recursion-2` (gen test): a *slice* typed with its own profile url — Java generates
(the slice has no children, so the profile is never entered); .NET throws `Recursive profile dependency
detected` because it eagerly ensures the external profile's snapshot. `logical-goo`: `url == baseDefinition`
with a different SD registered under that canonical — Java uses the registered base; .NET throws (guard keyed
purely by canonical). *Caveat: the .NET throws were observed under harness settings
(`ForceRegenerateSnapshots` + `GenerateSnapshotForExternalProfiles`); default-settings behavior is
unverified.* **Question:** is a self-typed extension slice (ext-recursion-2) legal, and what must a generator
produce for it — the profile's own first element (Java's in-progress-profile rule, `PPP:714-724`) or a refusal?

### Q16. What does a specialization's new element inherit from its type?
*DEV-021*

For a diff-only new element (specializations, logical models), .NET seeds the snapshot element from a **deep
copy of the type's snapshot root** (comment, alias, binding incl. extensions, the datatype's own invariants
with `constraint.source` back-filled) and merges the differential on top — with a regeneration cascade
(`string`'s root comment ends up on `Practitioner.gender` via `code`; 310 occurrences in the suite). Java's
specialization path (`addInheritedElementsForSpecialization`, `PU:1263-1283`) copies the type's snapshot
children, appends the type root's constraints and adds policy-filtered extensions — but not the root's
comment/alias/binding. Consequence: **.NET regeneration of the core package does not reproduce the published
core snapshots.** The spec says nothing about what a new element inherits from its type declaration (sdf-3/8b
only oblige definition/min/max/base). **Question:** should the spec state the inherited property set (proposal:
nothing but `type`, `min`/`max` from the differential, and sdf-3 fill), so core regeneration is reproducible?

---

## Tier 3 — for the Java maintainers (design questions, not bugs) and already-filed items

### Design questions to ask Grahame Grieve directly
- **JI-15 — additional-base bounds merge.** `SnapshotGenerationPreProcessor` picks the *looser* value for `min`
  (`PRE:421`, lower of the two) and `maxLength` (`PRE:466`, larger wins) while `max`/`minValue`/`maxValue` pick the
  stricter. Under "both bases must hold" semantics `min` should take the higher and `maxLength` the lower. Was
  intersection ever the contract?
- **JI-16 — `sortDifferential` swallows "path not found in base".** The comparer records the message
  (`PU:3790-3795`) but copies it to the caller's `errors` list only when `debug` (`PU:3916-3918`); an unknown path
  gets base index 0 and is silently sorted to the front of its sibling group. Deliberate (the same rationale
  that disabled the out-of-order warning in `getDiffMatches`, `PU:2465-2473`)?
- **JI-18 — obligation-profile mustSupport fold only for untouched elements.** `updateFromObligationProfiles`
  (`PU:2529-2582`) has one caller — the empty-diff copy-through path (`PPP:1068`); a differential row that merely
  changes `short` on an obligated element drops the inherited `mustSupport=true`. Intentional ("author took over
  the element") or a bug?
- **JI-19 — xver template selection is order-dependent.** `PPP:700` tests the raw `getXver()` field where other
  sites use the lazy `makeXVer()`; a caller that never calls `setXver` gets the xver branch skipped for the *first*
  xver-typed extension slice per `ProfileUtilities` instance. Do production callers (ValidationEngine, IG
  Publisher) always call `setXver`?
- **JI-14 — `MappingAssistant.merge()` renames applied to the diff's mappings, not the inherited ones**
  (`MappingAssistant.java:173-177`) — looks inverted; needs a two-SD repro, but worth a question.
- **Golden files as normative.** Given #2584 (copy-down contamination blessed by `on-questionnaire-expected`)
  and the `min := 1` (not sum) entry rewrite, would the maintainers accept a review pass over the
  `snapshot-generation` expected files once Q2/Q3 are decided?

### Already filed — FYI, do not rehash
- **Java (`hapifhir/org.hl7.fhir.core`), filed 2026-09-01:** #2584 (preprocessor cross-slice contamination),
  #2585 (IndexOutOfBounds in type slicing over a sliced base), #2586 (off-by-one in type-slice min reset),
  #2587 (slice-cardinality sum check never runs for trailing slice groups), #2588 (`"modiferExtension"` typo
  excludes modifierExtension slicings from preprocessing), #2589 (unsupported-slicing bail-out abandons all
  slicings), #2590 (`mergeAdditionalBinding` `any` no-op), #2591 (additional-base pattern×pattern operand bug),
  #2592 (`"..."` label append swapped operands), #2593 (obligation additional-binding inverted guard), #2594
  (message-arg mismatch + NPE risk, bundled), #2596 (dead multi-type guards — two of five walk paths silently
  expand a multi-type element against `Element`), #2597 (`AllowUnknownProfile` doc vs default).
- **.NET (`FirelyTeam/firely-net-sdk`):** #3583 (complex `url#element` type-profile expansion — fixed),
  #3587 (duplicate issue codes 10012/10014), #3588 (fake hl7.org canonical for the constrained-by-diff marker
  extension), #3589 (**reslice subtrees dropped entirely — silent loss of authored min/pattern constraints**,
  `reslicing-profile`/`slice23`), #3590 (t23a duplicate element + fabricated `base.min`), #3591 (obs-unit
  phantom element + dropped `fixedString`).

---

## Appendix A — one-line index (for the session agenda)

| # | question | tier | demo input |
|---|---|---|---|
| Q1 | does a snapshot close over `type.profile`; type vs base priority; cardinality diamond | 1 | code-derived (`Patient.address` / MyAddress) |
| Q2 | materialization: entry-children copy-down, contentReference entries, nested extensions | 1 | org2a, t21, comp-deep, pat-xver-extension |
| Q3 | rewriting the type-slicing entry (discriminator, rules, types, min) | 1 | obs-2 / obs-2a / obs-2b |
| Q4 | bare type slice = type constraint?; renamed-form representation | 1 | ts-case2, t16, t31 |
| Q5 | generator contract: required checks, enforcement posture, never-corrupt floor | 1 | t23a, obs-unit, the 21 fail tests |
| Q6 | `Extension.url` fixedUri convention | 2 | ext-recursion-2, ext-sort-issue |
| Q7 | contentReference: R4/R5/R6 semantics, survival, absolutization | 2 | Composition.section tests, eob tests |
| Q8 | profile-element extension vs `url#fragment` | 2 | none (needs a test) |
| Q9 | non-inheritable extensions: apply the existing inheritance-control metadata | 2 | ca-patient, ILCorePractitioner |
| Q10 | differential-less SD | 2 | none |
| Q11 | id regeneration (settled); does the 2019 "error, not repair" decision stand | 2 | t29a, t43a |
| Q12 | slicing shape rules: generator or validator; max=1 carve-outs | 2 | simplifier-1 |
| Q13 | per-property merge rules to state | 2 | code-derived |
| Q14 | additionalBase / obligations / imposeProfile / interfaces | 2 | multi-profile, profile-patient-op3, mi-use-* |
| Q15 | recursive extension definitions | 2 | ext-recursion-1/2, logical-goo |
| Q16 | what a new element inherits from its type | 2 | ILCorePractitioner (`Practitioner.gender`) |

## Appendix B — prior discussion found (Zulip / JIRA sweep)

*Sweeps completed 2026-09-02: chat.fhir.org, all public streams, ~200 searches across three clusters, ~70
threads read in full; jira.hl7.org, ~110 JQL queries, 83 tickets read in full. Verdict summary — **answered in
JIRA resolution text (no longer asked, only confirmed):** slicing a non-repeating element (Q12, FHIR-28619),
ED-level mapping replace-by-identity and suppress (Q13, FHIR-34434/31406), id regeneration (Q11a), root
cardinality as a bound (Q1 sub-decision, FHIR-19756/36738), the entry type-list collapse and entry-min raise
(Q3 c/d, FHIR-12259/31054), the implicit type constraint (Q4a, FHIR-12259), both below-root syntaxes by id
(Q8a, FHIR-13973/49079), non-inheritable extensions as extension metadata incl. the classification (Q9,
FHIR-28441), the R6 contentReference direction (Q7a, FHIR-57266). **Agreed on Zulip only (no HL7 ruling — so
still listed as live in the executive summary; ask the room to confirm or reopen):** contentReference
absolutization (Q7c, Grahame 2022), discriminator
synthesis on type slicings (Q3a, Redmond DevDays 2019 per Michel), slice-name error-not-repair (Q11b, July
2019), SD-level mapping inheritance (Q13, Lloyd 2024). **Discussed, unresolved (the live questions):** Q1, Q2,
Q3 b/e, Q4b, Q5, Q6, Q7 b/d, Q8b, Q10, Q13 merge rules, Q14–Q16. **Never discussed anywhere:** differential-less SDs (Q10), the `"..."` convention beyond a 2015
decline (Q13), `sliceIsConstraining` generator semantics (Q12), DEV-038's children gate (Q1), contentReference
recursion depth (Q7).*

- **Q1 / Q2 / Q4 — #conformance "snapshot depth" (Chris Grenz ↔ Grahame Grieve, June–Sept 2016)**
  ([permalink](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/snapshot.20depth/near/153846250)).
  Grahame states the only materialization principle found so far: an element with no children in the
  snapshot *references* its type ("see the type for further details"); an element with children *inlines*
  the type and "it's all or nothing … all of the type" — never a partial inline that leaves consumers to
  "figure out the intent in overlap/gap". BackboneElement children are always inlined ("you have to walk
  into a backbone element, because it's abstract"). Lloyd McKenzie: the snapshot "definitely needs to
  contain" the infrastructural children; hiding is a rendering concern. Asked whether the rule extends to
  `value[x]` variants (does one `valueBoolean` row force a `valueDateTime` element?), Grahame: "that's a good
  question. I'm not really sure." Reading for our questions: the 2016 principle is *reference-or-inline-all*
  per element, which both engines follow at the child level (both copy the full child set when they walk
  in); it says nothing about slice copy-down (Q2b), about *which* type's children are inlined when a
  `type.profile` is present (Q1), or about type slices (Q4) — those remain open. Status: **discussed-
  unresolved** for Q1/Q4; partial principle for Q2(a).

Full sweep records (queries, hit counts, every thread with permalink and gist) are in the study materials:
`extracts/zulip-sweep-2026-09-02-{a,b,c}.md` and `extracts/jira-sweep-2026-09-02.md`. Key permalinks per
question:

- **Q1** — #implementers "cardinality of root elements" (2018, the diamond; Lloyd/Grahame/Chris Grenz/Michel;
  GF#19756) [near/154024442](https://chat.fhir.org/#narrow/channel/179166-implementers/topic/cardinality.20of.20root.20elements/near/154024442);
  #IG-creation "Broken snapshot generation" (2023, "we do it because of extensions")
  [near/360386646](https://chat.fhir.org/#narrow/channel/179252-IG-creation/topic/Broken.20snapshot.20generation/near/360386646);
  #conformance "Snapshot Generation Question" (Nov 2024–May 2025, Java stops merging datatype-profile roots;
  "I don't actually know in that case")
  [near/485011469](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/Snapshot.20Generation.20Question/near/485011469);
  #conformance "Inheritance from parent profile or datatype profile?" (Feb 2026, Ward; Rotterdam evening)
  [near/574760523](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/Inheritance.20from.20parent.20profile.20or.20datatype.20profile.3F/near/574760523);
  #conformance "Where are the invariants on datatype roots?" (2025, "the features of the type remain on the
  type") [near/529291761](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/Where.20are.20the.20invariants.20on.20datatype.20roots.3F/near/529291761);
  #Simplifier.net "constraints missing in derived type" (2025, the MyAddress `demo-1` exhibit)
  [near/546855162](https://chat.fhir.org/#narrow/channel/413744-Simplifier.2Enet/topic/constraints.20missing.20in.20derived.20type/near/546855162);
  #conformance "Snapshot Generation" (Oct 2016, Grahame: inlining types is optional, "I object strenuously")
  [near/153853742](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/Snapshot.20Generation/near/153853742);
  #conformance "type profiles (#9791)" (2016 FHIR-I consensus that .NET implemented)
  [near/153831902](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/type.20profiles.20.28.239791.29/near/153831902).
- **Q2** — #IG-creation "Slices not inheriting preferred bindings from root" (Apr 2025, 314 msgs; FHIR-50267/
  50286/50390/50391) [near/511518779](https://chat.fhir.org/#narrow/channel/179252-IG-creation/topic/Slices.20not.20inheriting.20preferred.20bindings.20from.20root/near/511518779);
  #conformance "Recent Snapshot generation changes." (Jun 2025, copy-down = FHIR-50391)
  [near/526158294](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/Recent.20Snapshot.20generation.20changes.2E/near/526158294);
  #conformance "Slicer vs Slice" (2017, first run; Michel's no-three-way-merge policy)
  [near/153880168](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/Slicer.20vs.20Slice/near/153880168);
  #conformance "Snapshots: include extensions and datatype profiles?" (2021, Lloyd's depth rule)
  [near/264161646](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/Snapshots.3A.20include.20extensions.20and.20datatype.20profiles.3F/near/264161646);
  #IG-creation "allSlices issues when using inherited profile" (2026, "multiple inheritance" boundary)
  [near/614638534](https://chat.fhir.org/#narrow/channel/179252-IG-creation/topic/allSlices.20issues.20when.20using.20inherited.20profile/near/614638534);
  #FHIR-Validator "Problem with https://validator.fhir.org/" (2026, slice min not inherited, core#2282)
  [near/570137752](https://chat.fhir.org/#narrow/channel/291844-FHIR-Validator/topic/Problem.20with.20https.3A.2F.2Fvalidator.2Efhir.2Eorg.2F/near/570137752).
- **Q3 / Q12** — #conformance "Type[x], Slices, open/closed" (2019–20, `rules` as interpretation hint)
  [near/182253089](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Type.5Bx.5D.2C.20Slices.2C.20open.2Fclosed/near/182253089);
  #conformance "Type slicing in Profiles" (2024, "always closed in practice")
  [near/483615991](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Type.20slicing.20in.20Profiles/near/483615991);
  #IG-creation "Validation issue with partial type slicing" (2020, Lloyd: wrong to auto-close)
  [near/215972826](https://chat.fhir.org/#narrow/stream/179252-IG-creation/topic/Validation.20issue.20with.20partial.20type.20slicing/near/215972826);
  #conformance "Slicing rules question" (2023, no loosening)
  [near/371299107](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Slicing.20rules.20question/near/371299107);
  #implementers "Slicing non-repeating elements to define a choice" (2017–21, FHIR-28619)
  [near/202788891](https://chat.fhir.org/#narrow/stream/179166-implementers/topic/Slicing.20non-repeating.20elements.20to.20define.20a.20choice/near/202788891);
  #FHIR-Validator "Validation without snapshot" (2026, live Java error)
  [near/580899914](https://chat.fhir.org/#narrow/stream/291844-FHIR-Validator/topic/Validation.20without.20snapshot/near/580899914).
- **Q3 (cluster B)** — #IG-creation "Validation issue with partial type slicing" (Nov 2020: "the snapshot is
  wrong", "every single extension will allow any type", commit `acceaea7`)
  [near/215807285](https://chat.fhir.org/#narrow/stream/179252-IG-creation/topic/Validation.20issue.20with.20partial.20type.20slicing/near/215807285);
  #hapi "snapshot generator throwing Exceptions" (2020, "not a stated constraint")
  [near/195333774](https://chat.fhir.org/#narrow/stream/179167-hapi/topic/snapshot.20generator.20throwing.20Exceptions/near/195333774);
  #tooling "validator error url must have one match" (2025, Henket; "type slicing is always closed")
  [near/536543648](https://chat.fhir.org/#narrow/stream/179239-tooling/topic/validator.20error.20url.20must.20have.20one.20match/near/536543648);
  #conformance "Choice Type Renaming" (2022, cholesterol collapse = bug; JUnit vs publication parameter)
  [near/299624346](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Choice.20Type.20Renaming/near/299624346);
  #tooling "Type choice elements issue in instances of derived profiles" (Jul 2026, sliced-base exhibit)
  [near/612905754](https://chat.fhir.org/#narrow/stream/179239-tooling/topic/Type.20choice.20elements.20issue.20in.20instances.20of.20derived.20profiles/near/612905754);
  Redmond DevDays "always emit the entry" (Michel 2019-08)
  [near/173665164](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Slicing.20a.20non-repeating.20element/near/173665164).
- **Q4** — #conformance "Slicing a non-repeating element" (2019, 154 msgs: entry types never narrowed; "Type
  slices implicitly exist"; "Either you have a single type, or you can only talk about element properties";
  slice-name throw) [near/170899260](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Slicing.20a.20non-repeating.20element/near/170899260),
  [near/171687504](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Slicing.20a.20non-repeating.20element/near/171687504);
  #conformance "Element id for constrained choice type elements" (2017, Michel: implicit slice "does not
  introduce an actual slice entry")
  [near/153912077](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Element.20id.20for.20constrained.20choice.20type.20elements/near/153912077);
  #conformance "Choice Type Renaming" (2021–22, long id form required; SUSHI drops renaming)
  [near/297952166](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Choice.20Type.20Renaming/near/297952166);
  #shorthand "Error w/ type slices missing discriminator" (2022, "slicing never implicitly limits the allowed
  types") [near/286076884](https://chat.fhir.org/#narrow/stream/215610-shorthand/topic/Error.20w.2F.20type.20slices.20missing.20discriminator/near/286076884).
- **Q5 (cluster B)** — #conformance "Modifier Extension mismatch" (2022, "not validate the profile")
  [near/274604861](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Modifier.20Extension.20mismatch/near/274604861);
  #conformance "constraints on Types, and profiles" (2024, "doesn't mean that the snapshot generator should fail")
  [near/435801435](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/constraints.20on.20Types.2C.20and.20profiles/near/435801435);
  #implementers "Java: Snapshot-generation: Slicing rules on differential" (2022–23, discriminator equality throw)
  [near/299370917](https://chat.fhir.org/#narrow/stream/179166-implementers/topic/Java.3A.20Snapshot-generation.3A.20.20Slicing.20rules.20on.20differential/near/299370917);
  #implementers "ReSlicing Validation Error: Named items are out of order" (2024, code limitation admitted)
  [near/449788833](https://chat.fhir.org/#narrow/stream/179166-implementers/topic/ReSlicing.20Validation.20Error.3A.20Named.20items.20are.20out.20of.20order/near/449788833);
  #tooling "HL7 Validator: StructDef.differential.MustSupport=false" (2022, hard throw)
  [near/273172859](https://chat.fhir.org/#narrow/stream/179239-tooling/topic/.E2.9C.94.20HL7.20Validator.3A.20StructDef.2Edifferential.2EMustSupport.3Dfalse/near/273172859);
  #implementers "sum sign not allowed?" (2025, isSummary `java.lang.Error`)
  [near/524199848](https://chat.fhir.org/#narrow/stream/179166-implementers/topic/sum.20sign.20not.20allowed.3F/near/524199848);
  #conformance "How to differentiate extensions in StructureDefinitions?" (2026, Lloyd: the tests define the
  rules) [near/570433270](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/How.20to.20differentiate.20extensions.20in.20StructureDefinitions.3F/near/570433270);
  #fhir/infrastructure-wg "Status Report" (2023, FHIR-13402 "Not happening for R5")
  [near/328143250](https://chat.fhir.org/#narrow/stream/179280-fhir.2Finfrastructure-wg/topic/Status.20Report/near/328143250).
- **Q5** — #fhir/infrastructure-wg "Constraining out element properties in a differential" (2022, Lloyd:
  snapshot generation "doesn't get to be 'normative'")
  [near/273651748](https://chat.fhir.org/#narrow/stream/179280-fhir.2Finfrastructure-wg/topic/Constraining.20out.20element.20properties.20in.20a.20differential/near/273651748);
  #conformance "default values for mustSupport in R4B/R5" (2022, Grahame's what-may-change list)
  [near/315910873](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/default.20values.20for.20mustSupport.20in.20R4B.2FR5/near/315910873);
  #conformance "Array values in differentials" (2020, "represented in the snap shot tests")
  [near/207625254](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Array.20values.20in.20differentials/near/207625254);
  #committers "Task 9079" (2016) [near/153860752](https://chat.fhir.org/#narrow/stream/179165-committers/topic/Task.209079/near/153860752).
- **Q6** — #conformance "Extension.url - fixedString or fixedUri?" (2019)
  [near/176082903](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Extension.2Eurl.20-.20fixedString.20or.20fixedUri.3F/near/176082903);
  #IG-creation "fixed vs pattern values for simple data types" (2025, validator requires fixedUri)
  [near/504198628](https://chat.fhir.org/#narrow/stream/179252-IG-creation/topic/fixed.20vs.20pattern.20values.20for.20simple.20data.20types/near/504198628).
- **Q7** — #IG-creation "Clarification on contentReference" (2020–2025, the whole history incl. Ewout's
  "EITHER a contentReference or children")
  [near/225077241](https://chat.fhir.org/#narrow/stream/179252-IG-creation/topic/Clarification.20on.20contentReference/near/225077241),
  [near/524681048](https://chat.fhir.org/#narrow/stream/179252-IG-creation/topic/Clarification.20on.20contentReference/near/524681048);
  #implementers "Recursive schemas validation" (2026, FHIR-57265/57266)
  [near/580017127](https://chat.fhir.org/#narrow/stream/179166-implementers/topic/Recursive.20schemas.20validation/near/580017127);
  #conformance "STU3 Qustionnaire snapshot generation" (2022, absolute-form rule)
  [near/278523804](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/STU3.20Qustionnaire.20snapshot.20generation/near/278523804);
  #conformance "contentReference definition" (2018, Michel's two options)
  [near/153930284](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/contentReference.20definition/near/153930284).
- **Q8** — "Clarification on contentReference" above (Grahame's 2021 worked example, near/225079400);
  #conformance "profile-element Extension" (2021, Lloyd: backbone elements)
  [near/238315419](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/profile-element.20Extension/near/238315419);
  FHIR-39384.
- **Q9** — #tooling "Forge added extension explicit-type-name" (2020–2026, the whole inheritance-control
  history) [near/205217073](https://chat.fhir.org/#narrow/stream/179239-tooling/topic/Forge.20added.20extension.20explicit-type-name/near/205217073),
  Grahame's 2023 lists [near/328203575](https://chat.fhir.org/#narrow/stream/179239-tooling/topic/Forge.20added.20extension.20explicit-type-name/near/328203575),
  Ward on PR #2886 [near/562720344](https://chat.fhir.org/#narrow/stream/179239-tooling/topic/Forge.20added.20extension.20explicit-type-name/near/562720344).
- **Q11** — #conformance "Slicing a non-repeating element" (July 2019; ids derivative; "generation now blows
  up if the slice names are wrong")
  [near/171687483](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Slicing.20a.20non-repeating.20element/near/171687483),
  [near/171730052](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Slicing.20a.20non-repeating.20element/near/171730052);
  #shorthand "Adding extensions" (2024, `GenerateElementIds` to be removed)
  [near/482640122](https://chat.fhir.org/#narrow/stream/215610-shorthand/topic/Adding.20extensions/near/482640122).
- **Q13** — #conformance "Inheritance of StructureDefinition.mappings" (2024–26, SD.mapping propagates;
  `$all` suppress) [near/462578892](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Inheritance.20of.20StructureDefinition.2Emappings/near/462578892),
  [near/592817665](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Inheritance.20of.20StructureDefinition.2Emappings/near/592817665);
  #conformance "Modifier extension on ElementDefinition?" (2019)
  [near/162849209](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Modifier.20extension.20on.20ElementDefinition.3F/near/162849209).
- **Not found on Zulip:** OQ-006 generator semantics, OQ-010 `"..."` convention, OQ-016 differential-less
  SDs, DEV-038's children gate, contentReference recursion depth.
- **JIRA (jira.hl7.org, ~110 queries, 83 tickets read in full; per-OQ verdict map in
  `extracts/jira-sweep-2026-09-02.md`).** Decisions on record: [FHIR-50267](https://jira.hl7.org/browse/FHIR-50267)
  slicing snapshot generation (Q2); [FHIR-50391](https://jira.hl7.org/browse/FHIR-50391) mustSupport/obligations
  in repeating elements (Q2, contradicts 50267 on mustSupport); [FHIR-12259](https://jira.hl7.org/browse/FHIR-12259)
  implicit type slicing (Q3/Q4); [FHIR-8969](https://jira.hl7.org/browse/FHIR-8969), [FHIR-31054](https://jira.hl7.org/browse/FHIR-31054),
  [FHIR-31400](https://jira.hl7.org/browse/FHIR-31400), [FHIR-3623](https://jira.hl7.org/browse/FHIR-3623),
  [FHIR-17821](https://jira.hl7.org/browse/FHIR-17821) (Q3/Q12 slicing rules); [FHIR-19756](https://jira.hl7.org/browse/FHIR-19756),
  [FHIR-36738](https://jira.hl7.org/browse/FHIR-36738), [FHIR-48664](https://jira.hl7.org/browse/FHIR-48664),
  [FHIR-12179](https://jira.hl7.org/browse/FHIR-12179), FHIR-9791, FHIR-13839 (Q1); [FHIR-8286](https://jira.hl7.org/browse/FHIR-8286),
  FHIR-8975, FHIR-7783 (Q2); [FHIR-28441](https://jira.hl7.org/browse/FHIR-28441), FHIR-27535 (Q9);
  [FHIR-14958](https://jira.hl7.org/browse/FHIR-14958), FHIR-13139, FHIR-39350, [FHIR-57266](https://jira.hl7.org/browse/FHIR-57266),
  [FHIR-57265](https://jira.hl7.org/browse/FHIR-57265) (Q7); [FHIR-13973](https://jira.hl7.org/browse/FHIR-13973),
  FHIR-13386, [FHIR-49079](https://jira.hl7.org/browse/FHIR-49079) (Q8); FHIR-9843, FHIR-12182, FHIR-20465, FHIR-14091
  (Q11); [FHIR-28619](https://jira.hl7.org/browse/FHIR-28619), [FHIR-13545](https://jira.hl7.org/browse/FHIR-13545)
  (Q12); [FHIR-31406](https://jira.hl7.org/browse/FHIR-31406), FHIR-20385, FHIR-6125, FHIR-40543 (open) (suppress);
  [FHIR-34434](https://jira.hl7.org/browse/FHIR-34434), FHIR-37692, FHIR-8182, FHIR-7801, FHIR-14272 (Q13);
  FHIR-7800/7802, FHIR-17469, FHIR-13461, FHIR-20405 (Q5); [FHIR-31405](https://jira.hl7.org/browse/FHIR-31405)
  (open — the landing place for Q5/Q13 outcomes); FHIR-9079 + FHIR-13402 (the twice-abandoned work item).
  Not found in JIRA: OQ-007 SD-level mapping merge, OQ-013, OQ-016.
- **Ewout's own thread** — #conformance "Issue 13402 - Clarify snapshotting rules" (2026-09-01; Grahame "sure
  we can look at that"; Gino's comparison posted there)
  [near/620626646](https://chat.fhir.org/#narrow/channel/179177-conformance/topic/Issue.2013402.20-.20Clarify.20snapshotting.20rules/near/620626646).
