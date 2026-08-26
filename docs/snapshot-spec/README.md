# The Snapshot Generation Specification (reverse-engineered)

This document set is a reverse-engineered specification of FHIR **snapshot generation** — the algorithm
that computes `StructureDefinition.snapshot` from a `StructureDefinition.differential` and the snapshot of
its base definition. It is written as the foundation for a future reimplementation of the SDK's
`SnapshotGenerator`, and as a record of where the two mainstream implementations — this SDK (.NET) and the
Java reference implementation (`hapifhir/org.hl7.fhir.core`) — agree, disagree, and go beyond the published
FHIR specification.

**Reference semantics: FHIR R5**, with R4/R4B deltas noted explicitly per chapter. In addition, the **R6 CI
build** (build.fhir.org) is tracked as a forward-looking source: it captures the community's current
thinking since R5, it is where several of our documented gaps are already being addressed, and it is the
verification target for every spec-change proposal in [chapter 15](15-spec-rfcs.md) — timely, since FHIR R6
normative is being wrapped up. Chapters carry "R6-build note" sections where the build diverges from R5;
each is stamped with the build version it was checked against (the CI build changes daily). The SDK's
legacy STU3 fork is out of scope.

**Two audiences.** The FHIR spec deliberately describes the *shape* of differentials and snapshots, not the
generation algorithm — a good separation this document preserves. The algorithm chapters (1–12) serve
*implementers*; everything we find that the shape spec gets wrong, leaves ambiguous, or omits is written up
in [chapter 15](15-spec-rfcs.md) for the *standardization* side (HL7 Confluence / JIRA), where it can feed
the FHIR R6 normative wrap-up.

## Status

| Phase | Content | Status |
|---|---|---|
| 1 | Chapter skeleton + spec-derived baseline (what hl7.org actually says) | **done** (2026-08-21; R5 v5.0.0 baseline + R4 v4.0.1 deltas; R6-build v6.0.0-ballot4 deltas folded in where noted) |
| 2 | .NET implementation semantics, with code citations | **in progress** (2026-08-24 — packet 1: ch5 property-merge table from `ElementDefnMerger.cs`, +DEV-017, OQ-011..013; packet 2: ch4 matching from `ElementMatcher.cs`, +OQ-014/015, OQ-006 corrected, DEV-008 enriched; packet 3: ch2 preprocessing + ch3 base resolution, +OQ-016; packet 4: ch7 type-profile & extension expansion from `mergeTypeProfiles`/`expandElement`, +DEV-018/019, +OQ-017, OQ-001/002/014 enriched; packet 5 (2026-08-26): ch6 slicing from `startSlice`/`addSlice`/`findSliceAddPosition`, +OQ-018, OQ-003/005 .NET sides answered, OQ-014 row added) |
| 3 | Java implementation semantics + deviation register | seeded |
| 4 | Empirical cross-check (shared test cases, Java-oracle harness) | pending |
| 5 | Adjudication of deviations (Zulip/JIRA/WGM); spec RFCs | seeded (ch15) |

## Chapters

1. [Overview & terminology](01-overview.md)
2. [Differential preprocessing](02-differential-preprocessing.md)
3. [Base resolution, rebasing and the root element](03-base-resolution.md)
4. [Element matching](04-element-matching.md)
5. [Per-property merge semantics](05-property-merge.md)
6. [Slicing](06-slicing.md)
7. [Type-profile and extension expansion](07-type-and-extension-expansion.md)
8. [contentReference handling](08-contentreference.md)
9. [Logical models & interfaces](09-logical-models-interfaces.md)
10. [Element ids & the Base component](10-ids-and-base.md)
11. [Recursion & circularity](11-recursion.md)
12. [Error handling & configuration](12-errors-and-settings.md)
13. [Deviation register](13-deviation-register.md) — .NET ↔ Java differences, one entry per suspected/confirmed deviation
14. [Open questions](14-open-questions.md) — the questions the spec does not answer; feeds the WGM/Zulip consultation
15. [Spec RFCs](15-spec-rfcs.md) — proposed FHIR-spec changes (errata/clarifications/additions) for the
    standardization committees; timely for R6 normative

## Methodology

- **Every documented rule must cite its evidence**: spec text (page + section number), implementation code
  (`file:line`, both implementations where possible), or a shared test case (fhir-test-cases id). Rules with
  only single-implementation evidence are marked as such.
- Chapter sections are layered by provenance and labeled accordingly:
  *Spec baseline* (what hl7.org mandates) → *.NET behavior* → *Java behavior* → *deviations* → *open questions*.
  Implementation-derived rules are never silently presented as spec rules.
- Deviations get an entry in the [register](13-deviation-register.md) with a reproducing input where feasible.
  When comparing against Java, version-normalization artifacts (Java serves all FHIR versions from one
  R5-based codebase via conversion) are distinguished from true algorithm differences.

## Sources

- FHIR R5/R4 spec pages: profiling, conformance-rules, elementdefinition(+definitions), structuredefinition,
  extensibility, defining-extensions (local copies + extracts in the project materials directory, not
  committed).
- FHIR R6 CI build (build.fhir.org): the same pages, snapshotted as **v6.0.0-ballot4** (build generated
  2026-08-18, fetched 2026-08-21) — R6 findings in this document refer to that snapshot unless stated
  otherwise.
- .NET: `src/Hl7.Fhir.Conformance/Specification/Snapshot/` (+ `Hl7.Fhir.Shims.Base` infrastructure).
- Java: `org.hl7.fhir.r5/.../conformance/profile/` (ProfileUtilities, ProfilePathProcessor,
  SnapshotGenerationPreProcessor) and its manifest-driven test driver.
- Shared test cases: `FHIR/fhir-test-cases` `r5|r4b/snapshot-generation` (166 R5 manifest tests; 64 vendored
  in this repo under `src/Hl7.Fhir.Specification.Shared.Tests/TestData/snapshot-test/Type Slicing/`).
- History: GitHub issues (this repo), chat.fhir.org (#conformance is the primary venue; #implementers also
  carries relevant threads — too large to scan, but both are covered by full-text search across all public
  streams via the Zulip API), jira.hl7.org.
- Prior art: [FHIR-13402](https://jira.hl7.org/browse/FHIR-13402) "Clarify snapshot generation rules"
  (2017, closed 2023 *Not Persuasive* — HL7 acknowledges the rules are undocumented and that any future
  write-up would live in Confluence rather than the spec), and Chris Grenz's
  [FHIR-Primer wiki](https://github.com/chrisgrenz/FHIR-Primer/wiki) (DSTU2/STU3-era, non-normative):
  *Snapshots — Determining Refines* (a 9-rule refinement-lineage system) and
  *Aggregating Profile Differentials* (a 5-category per-property merge taxonomy), both cited by FHIR-13402.
