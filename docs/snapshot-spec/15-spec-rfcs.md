# 15. Spec RFCs — proposed changes to the FHIR specification

This register collects **proposed changes to the FHIR specification itself** (errata, clarifications,
additions) discovered while reverse-engineering the snapshot algorithm. The audience is the standardization
side of the community: entries are written to be posted on HL7 Confluence (and/or filed as JIRA change
requests) so the committees can run their ANSI processes over them — timely, since FHIR R6 normative is
being wrapped up.

**Scope note.** The spec deliberately describes the *shape* of differentials and snapshots, not the
generation algorithm — a separation worth preserving. RFCs here therefore propose: (a) fixes to outright
errata, (b) clarifications where the shape rules are ambiguous or contradictory, and (c) additions where a
missing shape rule forces implementations to invent semantics. The full algorithm description remains in
this document set (per [FHIR-13402](https://jira.hl7.org/browse/FHIR-13402)'s resolution, a Confluence-level
artifact, not spec text).

Relationship to [open questions](14-open-questions.md): an OQ is something *we* need answered; an RFC is
something *the spec* should say or fix. Settled OQs frequently graduate into RFCs.

**Status values:** `draft` · `verified` (checked against R6 ballot text — may already be fixed there) ·
`posted` (Confluence/JIRA reference recorded) · `accepted` · `rejected`.

> All entries below were found against the published **R5** pages (local copies, 2026-08-21) and have been
> **verified against the R6 CI build v6.0.0-ballot4** (hl7.fhir.r6.core#6.0.0-ballot4, build generated
> 2026-08-18, fetched 2026-08-21). The build changes daily — re-check verdicts shortly before posting.

---

## Errata (a)

### RFC-001 — profiling §5.1.0.14: contradictory normative strength on slice minimum sums
The same section states the sum of slice minimums "must be ≤ n" (bullet 3) and "SHOULD be ≤ m" (bullet 5) —
SHALL and SHOULD for what appears to be the same rule. Propose: pick one strength, state it once.
**Status:** verified — still present in R6 build v6.0.0-ballot4.

### RFC-002 — profiling: `position` discriminator defined twice with differing conditions
The R5 page defines the `position` discriminator type in two places with non-identical conditions.
Propose: single definition. **Status:** verified — still present in R6 build v6.0.0-ballot4.

### RFC-003 — elementdefinition #interpretation table: `base` row wrong for differentials
The row for `base` reads "required" in both Constraint Definition columns, contradicting practice and the
definitions page ("the base information must always be populated in element definitions in snap shots" —
i.e., a snapshot obligation the generator fulfills; differentials omit it). Propose: differential column =
prohibited (or optional/ignored), snapshot column = required.
**Status:** verified — **FIXED in R6 build v6.0.0-ballot4**: both constraint cells now read "expected" (a
new softened category). R5-only erratum; only worth filing if R5 technical corrections are still accepted,
otherwise close.

### RFC-004 — elementdefinition #interpretation table: leftover DSTU2 name `nameReference`
One row still uses the DSTU2 element name `nameReference` instead of `contentReference`.
**Status:** verified — still present in R6 build v6.0.0-ballot4.

### RFC-005 — structuredefinition §5.4.6.2 worked examples contain wrong canonical URLs
The Resource/CanonicalResource/Definition example rows carry `us/core` baseDefinition URLs that cannot be
right for core derivation examples, plus a doubled `"abstract": false` line. Dangerous because harnesses and
readers treat worked examples as ground truth.
**Status:** verified — partial in R6 build v6.0.0-ballot4: the doubled `"abstract": false` is fixed; all
three `us/core` URLs remain.

### RFC-006 — assorted editorial defects (R5 pages)
Broken link text `"&gt;Obligations` in profiling §5.1.0.22; unbalanced parenthesis in §5.1.0.7; typos
"discrinator", "mimimum". Bundle as one editorial ticket.
**Status:** verified — all four still present in R6 build v6.0.0-ballot4.

## Clarifications (b)

### RFC-007 — elementdefinition #interpretation table lacks rows for R5-new properties
`mustHaveValue`, `valueAlternatives`, `sliceIsConstraining`, `binding.additional` (and long-standing
omissions: `representation`, `orderMeaning`, `isModifierReason`) have no rows in the context table — their
legality in differentials vs snapshots must be reconstructed from scattered comments. Propose: complete the
table.
**Status:** verified — still present in R6 build v6.0.0-ballot4 (the table was reworked there — headers
renamed, a softened "expected/not expected" category added, the requirements/comments/alias row split into
three — but the missing rows are still missing).

### RFC-008 — replace-vs-append semantics unstated for most repeating properties
The ∆ (additive) footnote covers only `constraint`/`condition`/`mapping`; nothing states whether a
differential's `code`, `alias`, `example`, `type.profile`, `type.targetProfile` lists replace or extend the
base's. Every generator has had to invent this (and .NET/Java may disagree — see deviation register).
Propose: one sentence per property class in #interpretation. .NET's answers (Phase 2, ch5 table):
`code`/`alias`/`condition`/`example`/`valueAlternatives` union; `type` list replaces;
`type.profile`/`type.targetProfile`/`type.aggregation` replace wholesale; `mapping` unions on identity+map
(DEV-017). Java's answers (Phase 3 J-b): `alias`/`condition`/`valueAlternatives` union (agrees);
`example` unions on label+value; `type` list replaces (agrees); **`code` is never merged at all**
(frozen-by-omission — DEV-034(a)); `mapping` comma-appends per identity on R5+ (DEV-017). Agreement on the
big shapes (union for descriptive lists, replace for `type`), divergence on keys and on `code` — wording can
now be finalized from the two-implementation comparison. **Status:** draft — both implementations surveyed.

### RFC-009 — eld-14 vs additive constraints: restating an inherited constraint key
Inherited constraints "do not replace" and differential constraints add — but constraints "must be unique by
key" (eld-14). Behavior when a differential restates an inherited key is unspecified (error? merge?
duplicate?). .NET's answer (Phase 2): overlay-merge onto the inherited constraint, matched on `key` —
no duplicate, no error (`ElementDefnMerger.cs:487`). Java's answer (Phase 3 J-b): the restated diff
constraint is **silently dropped** — the inherited one survives untouched (`ProfileUtilities.java:3093`,
"constraints are cumulative. there is no replacing"). Two incompatible live answers (DEV-034(e)) — the RFC
should pick one. **Status:** draft — both implementations surveyed.

### RFC-010 — binding merge granularity unstated
Whether a differential `binding` replaces the base binding wholesale, merges per sub-element
(strength/valueSet/description), only tightens strength, and whether `additional` bindings append — none of
it is stated on the ElementDefinition page.
.NET's answer (Phase 2): per-sub-element overlay — strength/description replace (lattice not enforced),
valueSet overlays, `additional` unions by full value; binding dropped entirely when no bindable type remains
(`ElementDefnMerger.cs:358,186`).
Java's answer (Phase 3 J-b): rebuild-with-sub-element-overlay — strength/valueSet diff-wins, but inherited
`description` and binding extensions are **dropped** unless the diff restates them; the `required` lattice
row is enforced (ERROR), including an expansion-based value-set subset check; `additional` merged by
(valueSet, purpose) (`ProfileUtilities.java:3001-3027`; DEV-034(f)). So both merge per sub-element — the RFC
can state that as common practice — but they disagree on inherited-description survival and enforcement.
**Status:** draft — R6 build makes partial progress: new profiling section "Additional Bindings" says
additional bindings are constrainable only via a matching `key` (new `binding.additional.key` element,
eld-31), and keyless base additionalBindings cannot be constrained. Main-binding merge granularity remains
unstated, and the binding-strength lattice was NOT updated for R6's new `descriptive` strength (fresh gap).

### RFC-011 — `openAtEnd` transitions never addressed
The slicing lattice covers open→closed and ordered false→true; `openAtEnd` is not covered by any
tighten/loosen rule. JIRA history (sweep 2026-09-02): FHIR-3623 (2014) is the lattice's origin, FHIR-17821
(2018) reworded it to the two bullets and the openAtEnd→closed question was asked on the ticket and not
answered; FHIR-5581 only added the "discouraged" note; Lloyd 2017 (Zulip): the validator "doesn't distinguish
between openAtEnd and closed". Java tolerates CLOSED→OPENATEND, which the lattice's spirit forbids (ch6).
**Status:** draft — history verified, no prior decision.

### RFC-016 — contentReference form in generated snapshots (local vs absolute)
`ElementDefinition.contentReference` is defined as a local `#id` reference into the same structure; nothing
says what form it takes once an element is copied into another structure's snapshot. Grahame Grieve ruled on
Zulip in 2022 (#conformance "STU3 Qustionnaire snapshot generation") that "the relative content references must
be replaced with absolute content references when a snapshot is generated", and Ewout asked then that the
element definition say so — it still does not (R5 and R6 build unchanged). Java absolutizes globally at
id-generation time with a hard-coded core namespace; .NET only inside merged subtrees (DEV-023 flavor 1).
Proposal: one sentence in the `contentReference` definition and structuredefinition §5.4.6: "In a snapshot,
contentReference SHALL be absolute (`<canonical of the structure the element was copied from>#<id>`)". Also
state whether the reference survives on an element whose children were expanded (both engines' step-in paths
replace it by the target's `type`; Ewout 2025: "you EITHER have a contentReference or children, but not both").
**Status:** draft (2026-09-02) — Zulip ruling on record, spec text absent.

### RFC-017 — "tools generate complete verbose snapshots" is stale
profiling.html states that tools "generate complete verbose snapshots; they do not support suppressing
mappings or constraints", yet FHIR-31406 (2021) defined `elementdefinition-suppress` so that "the element
property should be removed from the corresponding snapshot.element during snapshot generation", FHIR-20385
(applied 2023) lets profiles remove code/comment/requirements/alias/example/mapping made irrelevant, and the
shared golden test `address-no-examples` deletes inherited examples via the (undocumented, FHIR-56831) `$all`
label. Proposal: replace the sentence with a pointer to the suppress mechanism and the FHIR-20385 text; document
`$all`. **Status:** draft (2026-09-02; OQ-008 answer-found).

### RFC-018 — snapshot element ids are derived data
Both generators regenerate every element id from path + slice names (OQ-009), the community has said so since
2016 (FHIR-9843, FHIR-12182: id is derived; FHIR-14091: "the publishing process will 'fix' them. This is
expected behavior"; Grahame 2019: "ids are derivative"), but elementdefinition.html still presents the id as an
author-assigned identifier that "may be used as the target of external references". Proposal: state that
snapshot (and differential) element ids are computed by the documented algorithm and that generators regenerate
them; external references must use the computed form. **Status:** draft (2026-09-02).

### RFC-019 — FHIR-50267 vs FHIR-50391 contradict each other on mustSupport across slices
Resolved three days apart in April 2025 by the same work group: FHIR-50267 ("Slicing Snapshot Generation" text,
*Resolved – change required*, not yet in the R6 build captured 2026-08-18) says a slicer `mustSupport` means
"all slices are automatically mustSupport"; FHIR-50391 (*Applied*) says mustSupport "can not be assumed to
apply to all slices … for legacy reasons". Whichever lands, the text should also say whether the slicing
element's **child rows** (not only its own properties) are "constraints of the base (slicing) element" that are
*not* included in slice snapshots — that is exactly where the two generators diverge (DEV-025, WGM brief Q2).
**Status:** draft (2026-09-02) — raise before citing either ticket.

## Additions (c)

### RFC-012 — minimum normative statement of snapshot-generation obligations
Not the algorithm — but the spec could state the generator's *contract*: what a conforming snapshot must
contain relative to base + differential (e.g., the "omission ≠ removal" rule of profiling §5.1.0.9
generalized; the sdf-3/8b fill obligations; ordering per §5.4.6). Revives the spirit of FHIR-13402 in
shape-only form, suitable for R6. **Status:** draft — depends on adjudication outcomes.
- Data point (J-c, 2026-09-01): the two implementations already disagree on the *contract* for a
  differential row that names no base element in a constraint profile — the spec forbids such rows
  [elementdefinition #path] but says nothing about what a snapshot must contain when one is present. Java
  drops it and reports an error; .NET materializes it as a new element without a diagnostic
  ([DEV-035](13-deviation-register.md#dev-035--unmatched-and-out-of-order-differential-rows-java-drops-with-error-or-appends-by-derivation-net-silently-creates-new-elements-ch4)).
  A one-line contract ("a snapshot SHALL NOT contain elements not present in the base for derivation
  = constraint; generators SHALL report such differential elements") would settle it. Likewise the
  §5.4.6 ordering rule has no stated consequence for violations — the same two implementations
  respectively drop-and-report and silently duplicate (DEV-027).

### RFC-013 — sanction (or prohibit) renamed choice-type paths in snapshots
The spec says type-specific elements are slices of `[x]` and "the original element SHALL always be
represented in a snapshot", but renamed paths (`Patient.deceasedBoolean`) in differentials/snapshots are
neither sanctioned nor prohibited — while both major implementations accept and normalize them.
**Status:** draft — depends on Phase 2/3 findings.

### RFC-014 — R6-build errata batch (report against the ballot)
Eight fresh errata candidates found in the R6 CI build itself (v6.0.0-ballot4), recorded in the extract
`R5-R6build-deltas.md` (materials dir): a ViewDefinition example contradicting its own SHALL, broken ♉
footnote glyphs in the reworked interpretation table, stale example numbering, the `binding.additional.purpose`
changes-list claiming an `open` code the rendered enum lacks, and others. These target the R6 ballot, so
they are the most time-sensitive entries in this register. Added 2026-09-02 (RFC-015 research): R6-build
`extensibility.html` Notes, the reworked relative-url bullet — a stray `'` after
`.../StructureDefinition/Extension` inside the `<code>` element.
**Status:** draft — verify each is still present in the current build immediately before filing.

### RFC-015 — `Extension.url` fixed-value convention in snapshots
defining-extensions §2.1.5.1.4 lists, as authoring *guidance*, that an extension definition's
`Extension.url` has "value = canonical URL (fixed)"; extensibility §2.1.5.0.1 states the url rule for
*instances* (absolute canonical, except in-line parts of complex extensions, which are relative). Neither
says whether the fixed value lives in the differential or may be filled into the snapshot by a generator,
which property carries it (`fixedUri` — tooling-settled, no spec text — vs `pattern[x]`), or what value a
nested part's `Extension.extension:x.url` fixes. Two live behaviors: .NET (`fixExtensionUrl`) backfills
a missing `fixedUri` — canonical on a definition root, type-profile url or else slice name on an
extension slice; Java never writes one and only inherits an authored/base value, and its golden files
bless unfixed definition roots (`ext-recursion-2`, `au2`) and unfixed profile slices (`ca-patient`,
`telus-oo`, …) ([DEV-037](13-deviation-register.md#dev-037--extensionurl-fixeduri-net-synthesizes-java-inherits-only-ch7)).
De-facto convention: hl7.fhir.uv.extensions 5.2.0 fixes `Extension.url` to the canonical in every
differential and snapshot (632/632) and every nested in-line part to its local name (272/272 relative,
zero `patternUri`; 270 of them equal the slice name, 2 use another authored local name) — so the fixed
value is universally *expected*, but being authored it does not discriminate the two engines. The R6 build
settles one sub-question: a derived profile on a complex extension "is not establishing the 'url' value"
(extensibility Notes, new bullet), so a derived extension profile inheriting its base's `fixedUri`
(`ext-sort-issue`) is correct for both engines.
**Proposal** (defining-extensions §2.1.5.1.4, promote guidance to conformance language): "An extension
definition SHALL fix `Extension.url` (as `fixedUri`) to its canonical URL; an in-line nested extension of
a complex extension SHALL fix its `url` to its local name (normally the slice name); a nested extension
defined by reference SHALL fix its `url` to the referenced extension's canonical URL. A snapshot generator
MAY supply these fixed values when the differential omits them and SHALL NOT alter an inherited one."
Option A (.NET) makes the MAY a SHALL; option B (Java) drops the MAY and leaves authoring tools
responsible. Either way the snapshot contract becomes stated, and validation of instances against a
snapshot no longer depends on which generator produced it. WGM brief Q6.
**Status:** draft — verified against R6 build 6.0.0-ballot4 (page build 2026-08-18, checked 2026-09-02);
research extract `rfc-015-extension-url-fixed-2026-09-02.md` (materials dir) has the census numbers.
