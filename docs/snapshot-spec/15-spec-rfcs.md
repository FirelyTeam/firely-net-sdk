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
tighten/loosen rule. **Status:** draft.

## Additions (c)

### RFC-012 — minimum normative statement of snapshot-generation obligations
Not the algorithm — but the spec could state the generator's *contract*: what a conforming snapshot must
contain relative to base + differential (e.g., the "omission ≠ removal" rule of profiling §5.1.0.9
generalized; the sdf-3/8b fill obligations; ordering per §5.4.6). Revives the spirit of FHIR-13402 in
shape-only form, suitable for R6. **Status:** draft — depends on adjudication outcomes.

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
they are the most time-sensitive entries in this register.
**Status:** draft — verify each is still present in the current build immediately before filing.
