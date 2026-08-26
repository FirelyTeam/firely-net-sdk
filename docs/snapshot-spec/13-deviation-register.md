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
- **Evidence:** issue #1253; tests `[Ignore]`d. **Status:** seeded — obs-2b analyzed as DEV-020
  (Phase 4, 2026-08-26); obs-2/obs-2a expected to be the same mechanism (verify from sweep output).

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

## DEV-018 — Complex type-profile references (`url#element`): expansion path broken (ch7)
- **Evidence:** `SnapshotGenerator.cs:1364` + `Navigation/ElementDefinitionNavigator.cs:281-303` — Phase 2
  deep-read 2026-08-24 (code-read only, no repro yet).
- **.NET:** the generator parses `type.profile` fragments (`ProfileReference`) and has a dedicated expansion
  path for a complex reference *with diff child constraints*, but the jump to the named element passes the
  **bare** fragment name to `JumpToNameReference`, which (a) parses it with `ProfileReference` again,
  classifies it as an (unknown) absolute canonical, and **throws `NotSupportedException`**; and (b) even for
  the `#name` form matches against the full element **id** (`Extension.extension:code`), which a short
  fragment name (`code`) never equals. So a differential that both uses `url#element` and constrains
  children under that element should crash the generator. The sibling *no-children* path works by design
  (nothing merged; sliceName-vs-fragment check only, `SnapshotGenerator.cs:1417-1486`), which is why the
  common patient-nationality-style profiles survive.
- **Suspected regression:** the id-matching + absolute-url guard in `JumpToNameReference` matches the
  absolute-contentReference work (#3177 era); the bare-name call site is 2016-era (WMR). The frozen STU3
  fork's variant (`Canonical`-based) silently no-matches instead of throwing — same outcome (issue
  `PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF` + subtree dropped), different failure mode.
- **Java:** TBD (Phase 3) — presumably honors `elementdefinition-profile-element` instead (OQ-017).
- **Status:** **confirmed and fixed** (2026-08-24): reproduced empirically, filed as
  [#3583](https://github.com/FirelyTeam/firely-net-sdk/issues/3583), fixed on branch
  `claude/elegant-leakey-7d15e9` (fragment resolved via SliceName lookup instead of
  `JumpToNameReference`; regression test in `SnapshotGeneratorTest.cs`). The fix is NOT in the
  6.2.1 NuGet the Phase-4 harness pins, so harness runs still reproduce the throw. The STU3 shim
  package (`Hl7.Fhir.STU3` `SnapshotGenerator.cs:1170`) has the identical bug, left unfixed.

## DEV-019 — `modifierExtension.url` never gets the fixedUri backfill (ch7)
- **Evidence:** `SnapshotGenerator.cs:1743-1746` — Phase 2 deep-read 2026-08-24.
- **.NET:** `fixExtensionUrl` only fires for elements whose path name is (case-insensitively) `extension`;
  `modifierExtension` never matches, so the `url.fixedUri` backfill never runs for modifier extensions.
  Narrow observable gap: in the normal full-expansion path the `url` child is copied from the extension
  definition's snapshot *with its fixed value already set* (the definition's own generation fixed it at root
  `Extension`, which does match), so the backfill only matters when that inheritance fails — unresolved
  extension profile, no child expansion, or an extension-definition snapshot lacking the fixed url.
- **Java:** TBD (Phase 3).
- **Status:** suspected (narrow; verify against Java + construct repro in Phase 4).

## DEV-020 — Type-slicing entry normalization: Java rewrites the sliced element, .NET merges it as written (ch6)
- **Evidence:** Phase-4 harness, 2026-08-26 — test `obs-2b` ("open type slicing + min on slice"),
  Java oracle EQUAL vs golden, so the golden file *is* current Java behavior.
  Versions: .NET = Hl7.Fhir.R5 6.2.1 | Java engine = 6.10.2 (d06577dbc5c6) | golden = fhir-test-cases 1.7.67.
- **Reproducing input:** `obs-2b-input.xml` — differential on `Observation.value[x]` (base 0..1,
  13-type choice) with an explicit slicing entry stating only `rules="open"` (no discriminator,
  no description), plus one slice `valueCodeableConcept` with `min=1`, `type=CodeableConcept`,
  and a required binding.
- **Snapshot slicing entry** (`Observation.value[x]`), four properties diverge:

  | property | .NET 6.2.1 | Java 6.10.2 / golden |
  |---|---|---|
  | `slicing.discriminator` | none (as written) | **`type:$this` injected** |
  | `slicing.rules` | `open` (as written) | **`closed`** — overrides the differential's explicit `open` |
  | `type` | full 13-type choice list | **collapsed to `[CodeableConcept]`** (the union of the slices' types) |
  | `min` | 0 (inherited from base) | **1** (raised to the sum of the slice minimums) |

  The slice element itself (`value[x]:valueCodeableConcept`) is identical on both sides
  (min=1, max=1, CodeableConcept, binding).
- **.NET:** the explicit slicing entry is matched and merged like any other element — inherit-if-absent
  per property. Nothing is synthesized or normalized; the author's `open` and the base's `min`/type
  list survive verbatim (ch6: `startSlice` merges an explicit entry; discriminator synthesis only
  happens for entries .NET *invents*, `SnapshotGenerator.cs:1968-1969,2163-2164`).
- **Java:** type slicing on a choice element is normalized **conditionally** — the obs-2 family
  (identical entry `rules=open`, varying constraints) maps the gradient in the golden files:

  | test | differential delta | golden entry outcome |
  |---|---|---|
  | obs-2 | slice = type CC only | 13 types kept, `open` kept, only `type:$this` injected |
  | obs-2a | *entry itself* also constrains `type` to CC | types [CC] (authored), rules forced **closed** |
  | obs-2b | slice = type CC + **min=1** (+binding) | types **collapsed** to [CC], **closed**, min **0→1** |

  So the discriminator is always injected; `closed` is forced once the entry's effective type set
  equals the sliced types; and a slice `min=1` triggers both the type collapse and the entry-min
  raise. `ProfilePathProcessor` L597/L1587 carry the comment that type slicing is always CLOSED
  "regardless of what the differential says" (overstated relative to obs-2's kept `open`); slice-min
  arithmetic cf. PPP L802-810; `type:$this` stamping cf. `checkToSeeIfSlicingExists` PPP:955-987.
  Exact trigger conditions to be pinned in Phase 3 packet J-a.
- **Spec basis:** the published text favors .NET on the type list — R4 *and* R5 choice-constraint rules
  state "type specific entries **do not restrict allowed types**" and "the original element SHALL always
  be represented in a snapshot" — yet Java/golden collapse the list, and forcing `closed` against an
  explicit `open` has no spec basis at all. On `min`, §5.1.0.14 gives slice-cardinality *validation*
  arithmetic ("the sum of the minimum cardinalities of the slices SHOULD be ≤ m", with its known
  bullet-3/bullet-5 contradiction) but never instructs a generator to rewrite the entry's declared
  cardinality (the permission-vs-default distinction, ch6). Input-validity wrinkle: the differential's
  slicing has neither discriminator nor description (violates the slicing shape rules), so Java can be
  read as *repairing* invalid input while .NET *propagates* it.
- **Consequences:** downstream tools reading the .NET snapshot see an open 13-type element with min 0;
  reading the Java snapshot they see a mandatory single-type element under closed slicing. Validation
  outcomes differ materially. This is the headline exhibit for the slicing-entry WGM question
  (OQ-020; connects to OQ-018 on implicit type constraints and to the slice-`Base.min` question).
- **Status:** confirmed (both behaviors reproduced 2026-08-26; spec question open → OQ-020).

