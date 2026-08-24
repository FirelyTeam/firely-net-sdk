# 13. Deviation register

One entry per suspected or confirmed difference between the .NET SDK and the Java reference implementation
(or between an implementation and the spec). Entries are *seeded* from already-documented evidence
(GitHub issues, disabled tests, runtime fixture patches) and will be verified and detailed in Phases 3–4.

**Status values:** `seeded` (evidence exists, not yet analyzed) · `confirmed` (reproduced, both behaviors
documented) · `version-artifact` (difference caused by Java's cross-version normalization, not the algorithm)
· `settled` (resolution agreed; record it) · `withdrawn`.

**Fields per entry:** area/chapter · .NET behavior · Java behavior · spec basis · reproducing input ·
status/resolution.

---

## DEV-001 — Type list: replace vs merge (ch5)
- **Evidence:** issue #827; `ElementDefnMerger.cs:222` (`mergeElementTypes`).
- **.NET:** a differential `type` list *replaces* the inherited list (derived profiles can remove inherited
  types); surviving types are then item-merged by type code so base extensions on `type.code` (the
  json/xml/rdf "compiler magic") are preserved. Within a type, `profile`/`targetProfile` lists **replace
  wholesale** when the diff has any (`mergeCanonicals`, `ElementDefnMerger.cs:387` — explicit R4-era
  decision: differentials may remove profiles; corrected 2026-08-24, Phase 2 deep-read), `aggregation`
  replaces wholesale.
- **Java:** TBD (Phase 3).
- **Spec basis:** TBD — believed silent on list-merge semantics.
- **Status:** seeded.

## DEV-002 — Constraint.source population (ch5)
- **Evidence:** issue #1052; `ElementDefnMerger.cs:484,507,514`, `SnapshotGenerator.cs:2460`.
- **.NET:** initializes `constraint.source` from the differential's base URL when absent.
- **Java:** TBD.
- **Status:** seeded.

## DEV-003 — obs-1-2: shared-suite divergence (ch TBD)
- **Evidence:** issue #1252; manifest test `obs-1-2` `[Ignore]`d in `SnapshotGeneratorManifestTests.cs`.
- **Status:** seeded — analyze in Phase 4 (re-run, diff against Java oracle).

## DEV-004 — obs-2 / obs-2a / obs-2b: shared-suite divergence
- **Evidence:** issue #1253; tests `[Ignore]`d. **Status:** seeded.

## DEV-005 — obs-2-3 / obs-3: shared-suite divergence
- **Evidence:** issue #1254; tests `[Ignore]`d. **Status:** seeded.

## DEV-006 — obs-4: shared-suite divergence
- **Evidence:** issue #1255; test `[Ignore]`d. **Status:** seeded.

## DEV-007 — obs-5: shared-suite divergence
- **Evidence:** issue #1256; test `[Ignore]`d. **Status:** seeded.

## DEV-008 — Extension header slicing element (ch6)
- **Evidence:** issue #2466; `ElementMatcher.cs:651-652`.
- **.NET** (Phase 2, 2026-08-24): named slices normally get a slice base with the `slicing` component
  removed and `min` reset to 0 (`initSliceBase`, `ElementMatcher.cs:545-560`) — but for *extension header*
  elements the slicing component is deliberately **kept** on the slice base (`initSliceBase(snapNav,
  false)`), so the synthesized/inherited extension slicing entry survives into the snapshot.
- **Java:** TBD (Phase 3). **Spec basis:** none found for either the slicing-removal or the min-reset rule.
- **Status:** seeded.

## DEV-009 — contentReference expansion details (ch8)
- **Evidence:** issue #3177; `SnapshotGenerator.cs:664-683`.
- **.NET:** on constraining children of a content-referenced element, nulls `contentReference` and copies the
  referenced element's children; deliberately does *not* copy `defaultValue`/`fixed`/`pattern`/`example`/
  `minValue`/`maxValue`/`maxLength`/`binding` (reasoning left in code comments, dated 2025).
- **Java:** TBD. **Spec basis:** TBD. **Status:** seeded.

## DEV-010 — Runtime-patched HL7 fixtures (`FixInput`)
- **Evidence:** `SnapshotGeneratorManifestTests.cs:134` — `Fix_t4a`, `Fix_t13`, `Fix_t15`, `Fix_t16`,
  `Fix_t23`, `Fix_t29`, `Fix_au3` mutate the checked-in `-input`/`-expected` files (ids, slice names, element
  order, `valueSetReference`→`valueSet`, inserting missing `value[x]` type-slice intros).
- Each patch documents a point where we considered the Java-produced fixture wrong → one deviation each,
  to be split out after Phase 3 analysis. **Status:** seeded.

## DEV-011 — Runtime-patched manifest rules (`FixManifest`)
- **Evidence:** `SnapshotGeneratorManifestTests.cs:684` — rewrites FHIRPath rules for `t13`, `t15`, `t16`,
  `t22`, `t24b`; injects a missing `t24a` entry. Same treatment as DEV-010. **Status:** seeded.

## DEV-012 — t26: input equals expected
- **Evidence:** `[Ignore]` note "input==expected" on `t26`. Determine what the test intends upstream.
- **Status:** seeded.

## DEV-013 — t14 under R5: manifest/spec version mismatch
- **Evidence:** `#if R5` ignore: "manifest.xml is not representing R5 5.0.0-snapshot3".
- **Status:** seeded — possibly version-artifact.

## DEV-014 — t37, t43 pass with "FAILS!" annotations
- **Evidence:** enabled tests carrying `// FAILS! TODO` / `// FAILS - FIXED` comments documenting divergence
  from Java-produced expected output. **Status:** seeded.

## DEV-015 — 102 upstream manifest tests never integrated (coverage gap)
- **Evidence:** upstream `fhir-test-cases/r5/snapshot-generation/manifest.xml` has 166 test ids vs 64
  vendored (diff captured 2026-08-21; only `au1` was removed upstream). Not a behavioral deviation per se,
  but every one of the 102 is an unverified area. Id clusters: `mi-use-*` (interfaces/multiple inheritance),
  `logical*`/`lm-*`/`xt-logical`, `ext-recursion-*`, `reslicing-profile*`, `profile-mapping-1..4`,
  `obs-6/badfixed/badpattern/ms-*/rebind/unit/perf`, real-world IGs (`zib-BodyHeight`, `uk-core-composition`,
  `ILCorePractitioner`, `ihe1/2`, `telus-oo`, `sushi1-3`, …).
- **Status:** seeded — Phase 4 sweeps these through both implementations.

## DEV-016 — Java oracle caveat: autoFixSliceNames
- **Evidence:** `ValidationEngine.java:1010` — the validator CLI runs `ProfileUtilities` with
  `setAutoFixSliceNames(true)`.
- Any harness comparison must account for this flag; raw `ProfileUtilities` default may differ from CLI
  behavior. **Status:** seeded (harness design note).

## DEV-017 — Mapping matched on identity+map vs R5 replace-by-identity (ch5)
- **Evidence:** `ElementDefnMerger.cs:193,918` (`matchMappings`) — Phase 2 deep-read 2026-08-24.
- **.NET:** diff mappings match inherited ones on the **(identity, map) pair**; a diff mapping restating an
  inherited `identity` with a different `map` is *appended*, yielding two mappings with the same identity
  (also violating eld-27, a warning).
- **Java:** TBD (Phase 3).
- **Spec basis:** R5 profiling §5.1.0.9 (new in R5): "providing a new mapping with the same identity … means
  that the new mapping replaces a mapping with the same identity in the element being profiled" — .NET does
  not implement the R5 rule (which is correct-for-R4, where mappings were additive-only; .NET runs the same
  merger for all versions).
- **Status:** seeded (spec-noncompliance under R5; compare Java in Phase 3).
