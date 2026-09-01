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
| 2 | .NET implementation semantics, with code citations | **done** (2026-08-26 — packet 1: ch5 property-merge table from `ElementDefnMerger.cs`, +DEV-017, OQ-011..013; packet 2: ch4 matching from `ElementMatcher.cs`, +OQ-014/015, OQ-006 corrected, DEV-008 enriched; packet 3: ch2 preprocessing + ch3 base resolution, +OQ-016; packet 4: ch7 type-profile & extension expansion from `mergeTypeProfiles`/`expandElement`, +DEV-018/019, +OQ-017, OQ-001/002/014 enriched; packet 5: ch6 slicing from `startSlice`/`addSlice`/`findSliceAddPosition`, +OQ-018, OQ-003/005 .NET sides answered, OQ-014 row added; packet 6: ch8 contentReference (no R5-propagation; reference dropped on expansion), ch9 logical models/interfaces, ch10 ids/Base (OQ-009 .NET answered), ch11 recursion (guarded vs unguarded channels), ch12 issue catalog + 6 settings + events, +OQ-019) |
| 3 | Java implementation semantics + deviation register | **in progress** (packet 1 done 2026-08-26: orientation maps of ProfileUtilities / ProfilePathProcessor+state objects / SnapshotGenerationPreProcessor / SnapShotGenerationTests driver — 4 extract files in the project materials directory, line citations spot-verified against commit b06c7ee; fhir-test-cases pin 1.7.67 = our clone, no test drift. **Packet J-a done 2026-08-31: ch6 Java section filled** — ProfilePathProcessor slicing paths + preprocessor sliceStuff propagation deep-read, 3 extract files; DEV-020 trigger conditions pinned [rebuild-CLOSED-then-reopen mechanism, entry-min raise = literal 1, two citation corrections], DEV-025 property table corrected [strict fill-if-absent ×27, no append], **new DEV-033: preprocessor cross-slice contamination confirmed** [golden files bless the bug], OQ-003/005/018 Java sides answered, slice Base.min settled as inter-engine agreement [ch10]. **Packet J-b done 2026-09-01: ch5 Java section filled** — `updateFromDefinition` PU:2585-3128 + `MappingAssistant` + preprocessor additional-base merge table PRE:399-531 deep-read; **new DEV-034: per-property merge divergence catalogue** [frozen-by-omission set, isSummary hard-abort, isModifier gate, restated-constraint-key drop, binding rebuild w/ description loss, extension policy lists + duplicate-append, label append broken]; DEV-001 settled as agreement, DEV-002 three-way contrast pinned [Java stamps base url, .NET derived url], DEV-017 pinned as R5+-only; OQ-010/012/013 Java sides answered, OQ-011 four-postures exhibit; trimDifferential resolved as near-dead [only non-false value = `closed` at PPP:1246]; RFC-008/009/010 both-implementation data recorded; 4 new verified upstream candidates JI-12/13 + upgraded JI-9/10. **Packet J-c done 2026-09-01: ch4 + ch2 Java sections filled** — `getDiffMatches`/`hasInnerDiffMatches` + `ProfilePathProcessor` dispatch, `checkDifferential`/`cloneDiff`/`sortDifferential`/`closeDifferential`/`cleanUpDifferential` deep-read; **new DEV-035: unmatched/out-of-order rows — Java drops with ERROR (constraint) or appends (specialization), .NET silent New** [t23 = t23a + `sort` flag; `Fix_t23` = manual sort; DEV-012 settled]; **new DEV-036: `sliceIsConstraining` enforced by .NET, never read by Java** [OQ-006 both sides answered]; OQ-014 Java taxonomy rows + t37 correction; OQ-015 Java side = clone + opt-in root-type repair; RFC-012 data point; no new ready-to-file Java bugs, JI-16 held as design question. **Packet J-d done 2026-09-01: ch7 + ch3 Java sections filled** — `ProfilePathProcessor` template selection + all five step-in paths, `updateFromDefinition` profile-doc override + `allowUnknownProfile`, final type sweep, `findProfile`, xver, obligation profiles, `generateSnapshot` preamble/epilogue, `cloneSnapshot`; **new DEV-037: `Extension.url` fixedUri is a .NET-only synthesis** [Java inherits or expects it authored; golden files bless fixed-less extension snapshots]; **new DEV-038: type-profile scope inverted** [.NET merges any type profile, type beats base; Java merges only Extension/Resource roots as templates and opens a profile's children only where the base has none — base beats type]; OQ-001/002/016/017/021 Java sides (OQ-016 Java consistent in both roles; OQ-017 disjoint syntaxes: .NET fragment-only, Java profile-element-only by id), OQ-014 J-d rows [two `java.lang.Error`s for author-legal input]; DEV-018/019/032 Java sides; JI-11 + JI-17 verified and filed upstream as hapifhir#2597/#2596, JI-18/19 held) |
| 4 | Empirical cross-check (shared test cases, Java-oracle harness) | **in progress** (packet 1 done 2026-08-26: three-way harness built, oracle validated on milestone set. Packet 2 done 2026-08-26: batch harness [one shared-context JVM + one .NET process + package parity], **full 164-test sweep: Java oracle EQUAL vs golden on 143/143 gen+sort tests, 21/21 fail tests JUnit-pass** — every .NET diff is now real signal; noise-classified report over ~52k property diffs [10 classes]; headline classification DEV-020/OQ-020 — see materials `extracts/harness-sweep-2026-08-26.md`. **Packet 3 done 2026-08-26 (sweep mining):** all 556 ELEMENT-SET elements grouped [zero ungrouped], all 131 min/mustSupport diffs classified [zero per-property merge differences — ch5 verified clean], all 21 fail tests dossiered [Java 21/21 vs .NET 8/21, two silently *corrupt* .NET outputs]; register grown DEV-021..032 + OQ-021, DEV-003/005 settled as agreement, DEV-007 confirmed, DEV-014 split; Java preprocessor slice-propagation mechanism located [`SnapshotGenerationPreProcessor`, 77% of min/MS diffs]; extracts `element-set-`, `failtests-`, `min-mustsupport-2026-08-26.md`) |
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
