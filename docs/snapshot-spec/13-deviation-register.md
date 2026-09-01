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
- **Java:** **agrees on the headline** — the diff type list replaces the inherited list wholesale
  (`ProfileUtilities.java:3053-3080` @ b06c7ee, J-b deep-read 2026-09-01). Differences in the details:
  Java first *validates* each diff type against the base (`checkTypeDerivation` PU:3262 — unknown code →
  DefinitionException throw; targetProfile not tracing to a base targetProfile → ERROR message,
  specialization exempt; .NET validates nothing), then takes the diff item **verbatim** except for a
  whitelisted copy-down from the matched base type (`type-must-support` if new, `pattern` + obligation
  extensions always, PU:3292-3293) — so Java does NOT preserve general base extensions on `type.code`
  (the json/xml/rdf compiler magic .NET keeps; on core-shipped snapshots those are already present in the
  base snapshot element, masking the difference).
- **Spec basis:** silent on list-merge semantics ([RFC-008](15-spec-rfcs.md)); "profile may restrict the
  types of a choice element" [profiling §5.1.0.9] supports replace-with-subset.
- **Status:** settled as agreement-on-outcome for the list semantics (both replace); residual delta =
  validation + extension fidelity (folded into DEV-028 group (e) and DEV-022 respectively).

## DEV-002 — Constraint.source population (ch5)
- **Evidence:** issue #1052; `ElementDefnMerger.cs:484,507,514`, `SnapshotGenerator.cs:2460`;
  `ProfileUtilities.java:3084-3098` @ b06c7ee (J-b deep-read 2026-09-01).
- **.NET:** *after* the constraint merge, stamps `source` on **every** constraint still lacking one — using
  the url of the SD whose constraints are being merged (main path: the **derived** profile's url) — and only
  when the diff declares ≥1 constraint on that element. Inherited base constraints that were never stamped
  thereby get attributed to the *derived* profile.
- **Java:** *before* appending diff constraints, stamps `source` on inherited constraints lacking one — with
  the **base SD's** url (`srcSD` at the PPP call sites = the base structure definition) — unconditionally
  (also marks them `SNAPSHOT_IS_DERIVED`). Diff-added constraints are **never stamped** (left as authored).
- **Net:** opposite attribution for unstamped inherited constraints (base url vs derived url), and Java
  leaves new constraints unattributed where .NET stamps them. Java's attribution matches the spec's
  "reference to the original source of the constraint, for traceability".
- **Spec basis:** `constraint.source` definition [elementdefinition-definitions]; no stamping algorithm given.
- **Status:** confirmed both sides (code-derived; corpus rarely exposes it because core snapshots ship
  pre-stamped).

## DEV-003 — obs-1-2: shared-suite divergence (ch4)
- **Evidence:** issue #1252; manifest test `obs-1-2` `[Ignore]`d in `SnapshotGeneratorManifestTests.cs`.
- **Status:** **settled as agreement-on-outcome** (Phase 4 packet 3, 2026-08-26): both sides THROW for
  the same author error (profiling a type already profiled out of the choice). Residual divergence is
  error taxonomy only — Java's author-facing `DefinitionException` ("invalid constrained type Quantity
  from CodeableConcept") vs .NET's `InvalidOperationException` "**Internal error** in snapshot generator
  (ElementMatcher.constructChoiceTypeMatch)" (OQ-014 row). Details: fail-test extract 2026-08-26.

## DEV-004 — obs-2 / obs-2a / obs-2b: shared-suite divergence
- **Evidence:** issue #1253; tests `[Ignore]`d. **Status:** seeded — obs-2b analyzed as DEV-020
  (Phase 4, 2026-08-26); obs-2/obs-2a expected to be the same mechanism (verify from sweep output).

## DEV-005 — obs-2-3 / obs-3: shared-suite divergence (ch4)
- **Evidence:** issue #1254; tests `[Ignore]`d.
- **Status:** **settled as agreement-on-outcome** (Phase 4 packet 3, 2026-08-26): both tests THROW on
  both sides for the same reason (type not among the base's remaining/any choice types) — same taxonomy
  split as DEV-003. Details: fail-test extract 2026-08-26.

## DEV-006 — obs-4: shared-suite divergence
- **Evidence:** issue #1255; test `[Ignore]`d. **Status:** seeded.

## DEV-007 — obs-5: shared-suite divergence (ch6)
- **Evidence:** issue #1256; test `[Ignore]`d.
- **Status:** **confirmed** (Phase 4 packet 3, 2026-08-26): Java THROWS ("more than one type slice …
  but one of them (valueCodeableConcept) has min = 1, so the other slices cannot exist"); .NET emits
  the arithmetic contradiction as written (`value[x]` 0..1 with a 1..1 and a 0..1 slice) — §5.1.0.14
  slice-cardinality sums are unchecked in .NET (ch6). Part of the DEV-028 validation-gap catalogue.

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
- **`Fix_t23` explained (J-c, 2026-09-01):** the manifest marks t23 `sort="true"`, i.e. the Java driver runs
  `sortDifferential` before generating (the fixture deliberately lists `Patient.contact.gender` before
  `Patient.contact.telecom`; t23a is the *same* differential without the sort flag and is a `fail` test).
  The .NET driver deserializes `sort` (`SnapshotGeneratorManifestTests.cs:916-917`) but never acts on it;
  `Fix_t23` (`:236-252`) hand-swaps the two rows in the checked-in file instead. So this patch is not a
  disagreement with the fixture — it is a manual substitute for a Java-side preprocessing step .NET lacks
  (ch2 Java section; DEV-035). Of the 28 `sort="true"` R5 tests only t23 needed it, so every other sorted
  fixture happens to be in base order already.

## DEV-011 — Runtime-patched manifest rules (`FixManifest`)
- **Evidence:** `SnapshotGeneratorManifestTests.cs:684` — rewrites FHIRPath rules for `t13`, `t15`, `t16`,
  `t22`, `t24b`; injects a missing `t24a` entry. Same treatment as DEV-010. **Status:** seeded.

## DEV-012 — t26: input equals expected
- **Evidence:** `[Ignore]` note "input==expected" on `t26`. Determine what the test intends upstream.
- **Resolution (J-c, 2026-09-01):** t26 is a **sort-only** manifest test (`sort="true"`, no `gen`): the Java
  driver runs `sortDifferential` and deep-compares the *differential* with the expected file (driver
  `:544-556`). Its input is already in base order (identical path sequences in `t26-input.xml` and
  `t26-expected.xml`), so input == expected is the correct outcome — the test asserts that sorting a
  polymorphic-reference differential is a no-op. Not a deviation; .NET has nothing to run here because it
  has no `sortDifferential` (ch2). Keep `[Ignore]` or drop the test.
- **Status:** settled (not a deviation).

## DEV-013 — t14 under R5: manifest/spec version mismatch
- **Evidence:** `#if R5` ignore: "manifest.xml is not representing R5 5.0.0-snapshot3".
- **Status:** seeded — possibly version-artifact.

## DEV-014 — t37, t43 pass with "FAILS!" annotations
- **Evidence:** enabled tests carrying `// FAILS! TODO` / `// FAILS - FIXED` comments documenting divergence
  from Java-produced expected output.
- **Status:** **split** (Phase 4 packet 3, 2026-08-26): **t37** = agreement-on-outcome — both sides throw
  on the path typo (`MedicationRequiest…`), via different detectors (Java sort-count check vs .NET
  "Differential has multiple roots", `DifferentialTreeConstructor.cs:69-78`). **t43a** = genuine
  divergence — Java enforces the type-slice naming convention ("Slice name must be 'valueQuantity' but is
  'Quantity'"), .NET accepts any author-supplied slice name (also t29a; DEV-028 group c).

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
- **Java:** `MappingAssistant` (J-b deep-read 2026-09-01 @ b06c7ee). Element-level: diff mappings first,
  inherited appended unless matched; match = identity + map both equal. **R4 and earlier: identical to
  .NET** (identity+map union — both keep two same-identity mappings). **R5+: same identity, different map
  triggers a merge mode** — default APPEND **comma-appends** the inherited map text into the diff mapping
  (one mapping per identity, eld-27 satisfied, but neither R5 replace-by-identity nor R4 additive:
  a text-level hybrid); DUPLICATE/IGNORE/OVERWRITE configurable (MA:233-262). So the divergence is
  **R5+-only**, and *neither* engine implements the R5 replace rule as written. Java additionally
  reconciles/renames/prunes SD-level `StructureDefinition.mapping` declarations and honors an
  SD-level `suppressed` extension + suppressed-uri list (.NET: none of that; setting-gated per-item
  `elementdefinition-suppress` instead) — see ch5 Java section.
- **Spec basis:** R5 profiling §5.1.0.9 (new in R5): "providing a new mapping with the same identity … means
  that the new mapping replaces a mapping with the same identity in the element being profiled" — .NET does
  not implement the R5 rule (which is correct-for-R4, where mappings were additive-only; .NET runs the same
  merger for all versions).
- **Status:** confirmed both sides — R5+-only divergence; both engines noncompliant with the R5 replace-by-
  identity rule (WGM/RFC material: which of the three behaviors should the spec bless?).

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
  | `min` | 0 (inherited from base) | **1** (literal 1 whenever a slice states `min>0` — not a sum, `PPP:615`) |

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

  **Trigger conditions pinned (Phase 3 packet J-a, 2026-08-31; code @ b06c7ee):** the entry's slicing is
  **rebuilt unconditionally** as `type:$this`/CLOSED/unordered after the entry is processed
  (`PPP:595-598` — the "always closed" comment), then a coverage check (`PPP:646-667`) flips it back to
  **OPEN iff any type still allowed on the entry has no matching type slice** — that reopen is what keeps
  obs-2 `open` (12 of 13 types unsliced) and what the comment overstates. A slice with `min>0` must be the
  *last* diff match (else throw) and raises the entry min **to literal 1** (`PPP:611-616`) while latching
  `fixedType`, which strips all other types from the entry (`PPP:638-645`) — after which nothing is
  unsliced, so obs-2b stays CLOSED; obs-2a gets there directly (entry authored down to CC). Exception
  branch: base path containing `xtension.value` + shortcut form deletes unsliced types instead of
  reopening (`PPP:657-663`). NOTE: the sliced-base variant (`PPP:1584-1588`) has **no reopen logic** —
  type slicing over an already-sliced base stays CLOSED unconditionally. Slice-min arithmetic
  cf. `PPP:801-810`; `type:$this` stamping for slices landing on an unsliced `[x]` element
  cf. `checkToSeeIfSlicingExists` `PPP:955-987` (also CLOSED, no reopen). Full detail: ch6 Java section +
  `java-ch06-simplepath-slicing-2026-08-31.md`.
- **Scope extension (packet 3 min-mining, 2026-08-26)** — the "entry min rewrite" is a family of three
  Java mechanisms, all absent from .NET (26 sweep hits total):
  - **C1 — type-slicing entry min raise** (the obs-2b behavior above): fresh instances `obs-4`,
    `zib-BodyHeight`, `t29`, `t34a` (sparse form: diff writes only `Extension.valueCode min=1`; both
    engines build the identical `value[x]:valueCode` slice, only the entry min diverges). Code:
    `ProfilePathProcessor.java:609-617` (`if (diffMatches.get(i).getMin() > 0) … setMin(1)`).
  - **C2 — auto-added entry min := sum of slice mins** (20 hits): `ProfileUtilities.java:983-1005`
    raises a repeating sliced element's entry min to the sum of the slice mins — but **only when the
    entry carries `SNAPSHOT_auto_added_slicing`** (an explicit authored intro gets a warning instead).
    Extension slicing entries are always auto-added (all `.extension`/`.modifierExtension` hits, sums
    of mandatory sub-extension mins, e.g. j=2/j=4 on complex-extension entries); `au-med-k` gets there
    by re-slicing without restating the intro, which stamps the *copied base slicer* auto-added
    (`ProfilePathProcessor.java:1250` — J-a citation correction: `:343-345` is the *simple-path*
    extension-entry stamping, reachable only for `.extension`/`.modifierExtension` bases) even though the
    base authored the entry. Gate mechanics pinned J-a: flush-based counter sweep `PU:976-1036`,
    `Base.max != "1"` proxy guard, never-flushed tail groups, ERROR only `forPublication` (ch6).
  - **C3 — `xtension.value[x]` min hack** (1 hit, telus-oo): Java zeroes an unstated slice min *except*
    when the sliced path ends in `xtension.value[x]` (`ProfilePathProcessor.java:801-805`, in-code
    comment "hack work around for problems with snapshots in official releases") — so an extension's
    `value[x]:valueString` slice keeps min=1 where .NET's pristine-clone rule resets it to 0.
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

## DEV-021 — New elements seeded from the datatype's snapshot root: .NET enriches, Java doesn't (ch7)
- **Evidence:** Phase-4 sweep 2026-08-26 (versions: .NET = Hl7.Fhir.R5 6.2.1 | Java engine = 6.10.2
  (d06577dbc5c6) | golden = fhir-test-cases 1.7.67 | core = hl7.fhir.r5.core 5.0.0);
  mechanism = `createNewElement` (`SnapshotGenerator.cs:887-964`, ch7): a diff-only new element's
  initial snapshot element **is a deep copy of its type's snapshot root** (via
  `getBaseElementForElementType` → `getSnapshotRootElement`), after which the diff is merged onto it —
  every root property the diff doesn't override survives (comment, alias, binding incl. its extensions,
  and the datatype's own invariants, whose `constraint.source` is back-filled with the type canonical,
  `:988-994`, DEV-002). The code that *would* strip the type's invariants from new elements
  (`removeNewTypeConstraint`, `:966-986`) is dead code — never called (ch7).
- **Sweep footprint:** the four biggest "unexplained" report classes are this one mechanism:
  `comment` .NET-only enrichment (2410), `alias` (740, partly DEV-022), binding extensions (~770),
  and the entire TYPE-CONSTRAINT class (3099 — datatype invariants like `ident-1`, `cpt-2` stamped on
  resource elements with `source` = the datatype url). Java/golden carry none of these.
- **Verified exemplar (cascade included):** ILCorePractitioner — its differential never mentions
  `Practitioner.gender`, yet .NET's snapshot has `comment` = "Note that FHIR strings SHALL NOT exceed
  1,048,576 (1024*1024) characters in size" on it (310 occurrences of that one string across the sweep).
  That text is the root comment of **`string`** — and the shipped root of `code` (gender's type) has *no*
  comment in either core source (verified in both the Java-side core tgz and .NET's `specification.zip`,
  2026-08-26). So the value cascades: the harness (mirroring `SnapshotGeneratorManifestTests`) sets
  `ForceRegenerateSnapshots=true`, .NET regenerates `code` (root inherits `string`'s comment,
  inherit-if-absent, ch5), regenerates core `Practitioner` (whose diff-only `gender` is seeded from the
  regenerated `code` root), and the profile inherits the enriched base element verbatim.
- **Scope:** observable whenever .NET generates a specialization/new element — always for profiles whose
  base chain contains snapshot-less SDs, and for *everything* under `ForceRegenerateSnapshots`. Direct
  corollary: **.NET regeneration of the core package does not reproduce the published core snapshots**
  (which are Java-produced); Java's new-element construction evidently copies a curated property subset
  from the type root, not the whole element (exact Java mechanism: Phase 3, `createBaseDefinition` or
  equivalent).
- **Spec basis:** none — the spec never says what a specialization's snapshot element inherits from its
  type declaration. sdf-3/8b only oblige definition/min/max/base to be present.
- **Status:** confirmed (.NET mechanism traced + sweep-quantified; Java-side code pending Phase 3).

## DEV-022 — Property/extension fidelity when copying elements from external structures (ch7/ch12)
- **Evidence:** Phase-4 sweep 2026-08-26 (same version stamp as DEV-021).
- **.NET:** `copyChildren`/element copies from a datatype or extension snapshot are **verbatim deep
  copies** minus generator annotations and the fixed 17-URL non-inheritable-extensions blocklist
  (ch12, OQ-019). Everything else survives: `alias`, `comment`, `mapping`, and *tooling* extensions.
- **Java:** copies a visibly **filtered** element. Two verified exhibits (ca-patient / ILCorePractitioner):
  1. `Identifier.type.binding` in the shipped core carries three extensions (`tools/…/binding-definition`,
     `elementdefinition-bindingName`, `elementdefinition-isCommonBinding`); after child expansion Java
     keeps only `bindingName` — .NET keeps them all (report classes `binding.extension.*`, ~770).
  2. A sliced `Patient.extension` header: Java emits a reduced synthesized entry (no `alias`, `comment`,
     `mapping` — although the published base `Patient.extension` *has* all three), while .NET merges into
     the full inherited base element. Java evidently rebuilds extension-slicing entries from a minimal
     template rather than copying the base element (relates to DEV-008 and the EXT-SLICING-STAMP noise
     class; Java's `EXT_SNAPSHOT_BEHAVIOR` + four static URL policy lists from the Phase-3 orientation
     are the presumed mechanism — deep-read pending).
- **Spec basis:** none for either policy; OQ-019 asks the question. The Java data point (tooling
  extensions like `binding-definition` filtered on copy) is now recorded there.
- **Status:** confirmed empirically (both outputs); Java-side code citations pending Phase 3.

## DEV-023 — contentReference in a constraint profile's snapshot: form and survival (ch8)
- **Evidence:** Phase-4 sweep 2026-08-26 (same version stamp), report class `contentReference` (78, both
  sides present) + ELEMENT-SET/type side effects.
- **Flavor 1 — absolute vs local form (the bulk, ~50 listed):** on elements copied into a constraint
  profile's snapshot, Java rewrites **every** contentReference to absolute form
  (`http://hl7.org/fhir/StructureDefinition/Observation#Observation.referenceRange`), .NET leaves the
  base's local form (`#Observation.referenceRange`) unless the element sits inside a subtree the
  generator itself merged — .NET's `ensureAbsoluteContentReferences` (`SnapshotGenerator.cs:1113-1152`,
  ch8) only runs over *merged children*, Java's rewrite (updateURLs territory, PU:2135/2179) is global.
  Exemplars: `Observation.component.referenceRange`, `ExplanationOfBenefit.addItem.*` (eob tests).
- **Flavor 2 — survival after child expansion:** when a diff constrains *children* of a referencing
  element, .NET **drops** the contentReference and restores the target's `type` (#3177, ch8 step 4);
  Java **keeps** the contentReference on the expanded element (and does not restore `type`). Exemplar:
  `Composition.section:parentSliceA.section:sliceA` (java = reference kept, .NET = null + type
  restored). This is also a driver of the 435 ".NET-only type entries" and part of the ELEMENT-SET class
  (Java-only expanded children carrying `ele-1`, see the element-set extract).
- **Spec basis:** ch8 spec-gap list — the spec never says whether the reference survives expansion
  (feeds OQ-004) nor when local form must become absolute (eld-5 is silent on both).
- **Status:** confirmed empirically; Java-side code citation pending Phase 3.

> Entries DEV-024 – DEV-031 come from Phase 4 packet 3 (sweep mining, 2026-08-26). Common version stamp:
> .NET = Hl7.Fhir.R5 6.2.1 | Java engine = 6.10.2 (d06577dbc5c6) | golden = fhir-test-cases 1.7.67 |
> core = hl7.fhir.r5.core 5.0.0. Evidence details: `extracts/element-set-2026-08-26.md` and
> `extracts/failtests-2026-08-26.md` in the project materials directory.

## DEV-024 — .NET drops reslice subtrees entirely — silent constraint loss (ch6)
- **Evidence:** ELEMENT-SET mining (138 java-only elements): tests `reslicing-profile` (26),
  `slicing-profile-child` (26), `slice23` (86).
- **Reproducing input:** `reslicing-profile-input` (base AuditEvent) slices `AuditEvent.agent.extension`
  by `url` + `value.system`, declares slice `altid`, then **reslices** `altid/npi` and `altid/ssn` with
  explicit constraints (`altid/npi.value[x].system min=1 patternUri=…us-npi`, etc.).
- **Java/golden:** both reslices emitted, extension fully expanded, `value[x]` expanded as Identifier
  (13 elements each).
- **.NET:** output contains only `AuditEvent.agent.extension` and `…extension:altid` — the reslice
  elements **and the differential's own constraints on them (`min`, `patternUri`) are silently lost**.
  No issue, no warning. `slice23` adds the propagation variant: reslices declared on the *unsliced*
  `agent.extension` also materialize (Java/golden) under the named `agent:user`/`agent:userorg` slices;
  .NET emits none of them.
- **Mechanism (.NET side, ch6):** reslicing support is `'/'-in-slice-name only` — the matcher's reslice
  handling never re-enters the base slice group to merge reslice children (exact drop point to be traced;
  ch6 documents `findSliceAddPosition`'s reslice-group placement as the only reslice-aware code).
- **Consequences:** the strongest data-loss deviation found so far — authored cardinality and pattern
  constraints vanish from the snapshot with no diagnostic. Downstream validators validate against a
  snapshot that misses author constraints.
- **Status:** confirmed empirically; **filed as
  [#3589](https://github.com/FirelyTeam/firely-net-sdk/issues/3589)** (2026-08-26). .NET code trace +
  Java comparison pending (Phase 3 J-a has the anchors).

## DEV-025 — Materialization depth of unconstrained content: Java normalizes more than .NET (ch7/ch8/ch11)
- **Evidence:** ELEMENT-SET mining, three flavors totalling 210 java-only elements:
  1. **contentReference slicing entries re-expanded** (144: t21 70, comp-deep/nested 24+24,
     reslicing-profile-parent 12, t29/params-nested-slices 7+7): when a contentReference element is
     sliced, Java expands the target's full child set under the **unsliced slicing entry** as well;
     .NET expands only under the named slices. 128/144 of these children carry `ele-1` (the 16 without
     are exactly `.id`/`.resource` children). Note: packet 2's "~191×5 java-only constraint.*" estimate
     was **not** reproduced by cref children alone — it likely mixed classes (the java-only element
     populations G1/G2/G3 plus the G4 identity split are the shadow behind it; see the element-set
     extract §4). comp-deep shows the re-expansion repeating per recursion level (ch11 angle).
  2. **Slicing-entry child constraints materialized into named slices** (52: org2a/org2b 16+16,
     on-questionnaire 20): a diff constraining children of the *entry* only — Java copies the modified
     entry children into each named slice (`identifier:NPI.{id,use,type,…}`); .NET leaves the slices as
     bare entry elements.
  3. **Complex-extension nested-slice inlining** (14: pat-xver-extension): Java inlines the extension
     definition's full nested slice structure (`species`/`breed`/`genderStatus` each with
     `{id,extension,url,value[x]}`); .NET emits only the diff-mentioned slice, childless.
- **Java mechanism for flavor 2, located** (min/mustSupport mining, 2026-08-26):
  `SnapshotGenerationPreProcessor.java` (invoked from `ProfileUtilities.java:825`) collects
  "sliceStuff" — the differential elements between a slicing entry and its first named slice — and
  pre-merges it into **each named slice's differential** (`processSlices` → `mergeElements` → `merge`,
  `:688-810`, `:993-1075`): **strict fill-if-absent for all 27 handled properties** — J-a correction: list
  properties (constraint, example, code, alias, type, valueAlternatives) are copied only when the target
  list is *empty*, there is NO append/union semantics; `mapping` and `condition` never propagate on a
  match; `merge` uses no `.copy()`, so matched elements across slices *share object instances* with the
  sliceStuff originals. Elements missing from a slice are **injected** as full copies (id rewritten, marked
  `SNAPSHOT_PREPROCESS_INJECTED`, mapping kept — a match-vs-inject asymmetry) — a chunk of the java-only
  ELEMENT-SET elements. Extension slicing is excluded as a slicer only when explicitly
  `open`/`ordered=false`/single `value:url` on an element named `extension` (the `"modiferExtension"` typo
  excludes modifierExtension; omitted `ordered` disqualifies); entry-level extension slices ride along as
  sliceStuff (t22 `validDate`). Full property table: `java-preprocessor-slicestuff-2026-08-31.md` §3 + ch6.
- **Property-level shadow:** this one mechanism also accounts for **101 of the 131** `min`/`mustSupport`
  NEW diffs in the sweep (M1 groups: sd-comp-hist 45, t22 27, on-questionnaire 21+, ILCorePractitioner 4
  — see the min/mustSupport extract). Notably, the same mining found **zero** genuine per-property
  merge-semantics differences for min/mustSupport (bucket B empty): every .NET value is simply the base
  source's value, every Java extra traces to a mechanism.
- **Common root:** .NET's expansion-depth policy — *expand only where the differential constrains*
  (ch11 §1) — versus Java's normalization, which also materializes inherited/propagated content the diff
  never touched. Both snapshots may be *semantically* equivalent under "slicing-entry constraints apply
  to all slices" reasoning, but consumers that read snapshots literally (most do — that is the point of
  a snapshot) see different element sets. → new [OQ-021](14-open-questions.md#oq-021--how-much-must-a-snapshot-materialize).
- **Java bug candidate found in passing (M1q):** confirmed and graduated to its own entry —
  [DEV-033](#dev-033--java-preprocessor-cross-slice-contamination--silent-constraint-loss-ch6).
- **Status:** confirmed empirically; Java preprocessor deep-read done (Phase 3 packet J-a, 2026-08-31).

## DEV-026 — Renamed-choice constraints: .NET anchors on a synthesized type slice, Java on bare `value[x]` (ch6/ch7)
- **Evidence:** ELEMENT-SET mining G4 (89 elements, both sides: t16 20n+18j, t31 25n+24j, sushi1/2).
- **Reproducing input:** t16 — diff constrains children under a **renamed choice**
  (`…extension:latitude.valueDecimal.extension:Geolocation-latitude-rendered`).
- **Behavior:** both engines express all authored constraints; they disagree on element **identity**:
  .NET materializes an explicit slice `…latitude.value[x]:valueDecimal` and hangs the subtree there
  (keeping bare `value[x]` too); Java/golden hangs the subtree **directly under bare `value[x]`**
  (no type slice synthesized). Golden blesses Java.
- **Spec basis:** R5 requires the *differential* to use `[x]`+type-slice form for choice constraints
  (ch1/ch6 baseline; the R4→R5 reversal) — but says nothing about which snapshot representation a
  generator must produce when the diff uses the (legacy) renamed form. Enriches OQ-018 (which so far
  covered the implicit type-constraint half of the same normalization split).
- **Status:** confirmed empirically (identity-level divergence; constraint content equal).

## DEV-027 — Malformed differentials produce silently corrupt .NET snapshots (ch2)
- **Evidence:** fail-test mining — the two "corrupt output" rows.
- **t23a (out-of-order differential):** diff lists `contact:males.gender` before `contact:males.telecom`
  (behind the base cursor). Java: ERROR "No match found … check that the path and definitions are legal
  in the differential (including order)". .NET: **no diagnostic at all**; `males.gender` merged in place,
  but `males.telecom` appended as a **second `Patient.contact:males.telecom` element** (duplicate
  element id) after `males.period`, carrying a **fabricated `base` component** with the diff's own
  `min=1` as `base.min`. Mechanism: ordering is assumed, never verified (ch2 `:96-98`); the forward-only
  matcher never moves the base cursor backwards (ch4 `:94-107`).
  **Java mechanism (J-c, 2026-09-01):** Java also has no ordering check; its base-driven walk queries the
  remaining diff scope per base row and, on a single match, **jumps** the diff cursor to just past the
  matched row (`PPP:827`). The base reaches `telecom` first, finds the later `telecom` row, and skips
  `gender` for good; `gender` is then an **orphan** reported by the post-walk verification (`PU:908-948`)
  and dropped — never a duplicate. **t23 is the identical differential** with `sort="true"`, so the driver's
  `sortDifferential` repairs it before generation and it passes (ch2 Java section; DEV-035).
- **obs-unit (`..` in path):** single diff element `Observation...unit` with `fixedString="%"`. Java:
  throws "Invalid path … name portion missing ('..')" (`checkDifferential`, `PU:1436-1437` — the
  per-segment grammar check of ch2's Java section; .NET has no path validation at all). .NET: one *warning*
  ("Element Observation. has neither a type nor a nameReference"), then emits a **phantom element**
  `Observation.` (empty-segment stand-in parent, ch2 `:80-90`) and **silently drops the author's
  `fixedString`** — the constraint appears nowhere in the output.
- **Consequences:** these two go beyond DEV-028's missing-validation catalogue: the output is *wrong*,
  not merely unvalidated (duplicate ids violate the element-id algorithm's uniqueness guarantee, ch10;
  fabricated `base.min` corrupts sdf-8b data; a dropped constraint is data loss). Prime OQ-014 exhibit
  for "generators must reject, repair, or propagate — but never corrupt".
- **Status:** confirmed empirically (both inputs in fhir-test-cases; .NET outputs in `harness/out/`);
  **filed as [#3590](https://github.com/FirelyTeam/firely-net-sdk/issues/3590)** (t23a) **and
  [#3591](https://github.com/FirelyTeam/firely-net-sdk/issues/3591)** (obs-unit), 2026-08-26.

## DEV-028 — Author-error detection catalogue: Java validates, .NET emits as written (ch2–ch6, ch9, ch12)
- **Evidence:** fail-test mining over all 21 `fail="true"` tests: Java satisfies the fail expectation on
  21/21; .NET on 8/21 — it **silently generates on 13/21**. Beyond DEV-027's corrupt outputs, the silent
  cases group into distinct absent checks (per-test dossiers in the fail-test extract):

  | group | tests | Java check | .NET behavior |
  |---|---|---|---|
  | (c) type-slice naming convention | t29a, t43a | "Slice name must be 'valueQuantity'" throw | any author name accepted (eld-16 never validated, ch6) |
  | (d) fixed/pattern type compatibility | obs-badfixed, obs-badpattern | ERROR "fixed value has type 'uri' which is not valid" | `fixedUri`/`patternUri` merged as written next to the untouched 13-type list (ch5) |
  | (e) type/targetProfile derivation | ihe2, mi-use-distinct, (ihe1) | "cannot constrain to type Reference from base types Resource" / "target profile … not a valid constraint" | wholesale type/targetProfile replace, no derivation walk (ch5 `:279`); `isValidTypeProfile` runs only for expanded `type.profile` values and **against the replaced type**, never `targetProfile` (proven by ihe2's empty log) |
  | (f) root-element invariants (sdf-15a/20) | ext-recursion-1, ext-ccuk | "Type on first differential element!" / "slicing at the root … is illegal" | root `type` and root `slicing` pass through unexamined (only root `sliceName` is repaired, ch2) |
  | (g) slicing a non-repeating element | simplifier-1 | DefinitionException "Attempt to a slice an element that does not repeat" | check exists but compiled out (`REJECT_SLICE_NONREPEATING_ELEMENT`, issue 10003 unreachable, ch12) |
  | (h) mustSupport direction | obs-ms-bad | ERROR "Illegal constraint [must-support = false] when [must-support = true]" | true→false replaced as written (ch5 `:251`) |
  | (i) SD.type vs base type coherence | t29b | "Base & Derived profiles have different types" | never checked; surfaces indirectly as an unresolvable contentReference (issue 10002) |
  | (j) slice-cardinality arithmetic | obs-5 | see DEV-007 | see DEV-007 |

- **.NET's stated policy** is "the generator should never throw — correctness belongs to the validator"
  (ch4 `:158-164`), so these are gaps *by design*; but the fail-test corpus shows Java treating the same
  inputs as generator-fatal, and the golden files bless that. Which checks belong in a *generator* is
  exactly OQ-014's question — this entry is its evidence table.
- **Caveat rows:** sushi3's apparent agreement is **harness-induced** (`ForceRegenerateSnapshots=true`
  discards the dep's shipped snapshot and dies on its unresolvable base before reaching the duplicate-id
  input; default-settings .NET untested). t15a agrees in substance (unknown extension detected) but not
  severity (6 issues + generated output vs hard throw).
- **Status:** confirmed empirically (13 reproducing inputs, all in fhir-test-cases).

## DEV-029 — Recursion crossover: each side rejects recursive structures the other accepts (ch11)
- **Evidence:** fail-test mining §3 (ext-recursion-1 vs ext-recursion-2 / logical-goo).
- **ext-recursion-1** (fail test — golden expects rejection): extension whose differential **root**
  carries `type=Extension, profile=<its own url>`. Java rejects structurally ("Type on first
  differential element!"). .NET **silently accepts**: root types are never validated (sdf-15a, DEV-028
  group f) nor expanded, so the cycle is never entered — output is an ordinary 5-element Extension
  snapshot with the self-reference merged onto the root.
- **ext-recursion-2** (gen test — golden expects success): a *slice* typed with its own profile url.
  Java generates (one warning; the slice has no diff children, so it never expands into the profile).
  .NET **throws** `NotSupportedException: Recursive profile dependency detected` — the type-profile
  merge eagerly ensures the external profile's snapshot (`GenerateSnapshotForExternalProfiles`) and
  re-enters a url already on the `SnapshotRecursionStack` (ch11 §2).
- **logical-goo** (gen test): input SD has **url == baseDefinition** (both `…/Boo`), and the register
  supplies a *different* SD with that same canonical (snapshot included). Java resolves the base to the
  registered Boo and uses its shipped snapshot. .NET throws the same `NotSupportedException`: the
  recursion guard is keyed **purely by canonical URI**, so url==base-url is a hard failure regardless of
  which SD the resolver would return.
- **Settings caveat:** both .NET *throws* are conditional on the harness config
  (`ForceRegenerateSnapshots` + `GenerateSnapshotForExternalProfiles`); default-settings behavior is
  unverified — one targeted re-run needed before the WGM brief. The ext-recursion-1 asymmetry (Java
  rejects, .NET accepts) is settings-independent.
- **Status:** confirmed under harness settings; default-settings re-run pending.

## DEV-030 — Cross-version bases: .NET rebuilds against R5 core, leaving R5/R4 hybrids (ch3)
- **Evidence:** ELEMENT-SET mining G7/G9. `sd-nested-ext` (base chain `sdc-questionnaire`, fhirVersion
  4.0.1): .NET output contains R5-only elements (`Questionnaire.versionAlgorithm[x]`, `copyrightLabel`,
  `item.answerConstraint`, `item.disabledDisplay`) — it re-expanded the base against the **R5 core**
  Questionnaire; Java/golden preserves the R4 shape. `mr-type-support` (base R4
  us-core-medicationrequest): .NET keeps the stale R4 `MedicationRequest.reported[x]` **alongside** the
  R5 `reported`; Java emits only `reported`.
- **Interpretation:** partly an artifact of running R4-based IGs through the R5-only .NET SDK (a real
  .NET deployment would use the R4 SDK; Java serves all versions from one codebase via conversion) — but
  the *hybrid* outputs (R4 and R5 shapes mixed in one snapshot) are .NET merge products, not just version
  skew.
- **Related repro case (min/mustSupport mining, bucket A):** in the same `mr-type-support` test,
  `MedicationRequest.reported` loses the *base profile's* `mustSupport=true` and its
  `elementdefinition-type-must-support` type extension on the .NET side (which shows core R5's explicit
  `mustSupport=false` — the element looks rebuilt from core rather than from the registered profile
  base). Two-hop ambiguity (base regeneration vs derived merge of a re-typed choice) — needs a targeted
  .NET repro (test description: "Duplicating must-support extensions on type").
- **Status:** version-artifact (suspected) — revisit when documenting cross-version policy (ch3).

## DEV-032 — Java-only merge inputs: additionalBase and obligation profiles (ch3)
- **Evidence:** Phase-3 orientation (Java-only surfaces) + Phase-4 sweep empirical confirmation
  (2026-08-26, same version stamp).
- **additionalBase:** `structuredefinition-additionalBase` makes Java's preprocessor merge a *second*
  base profile's differential into the one being generated
  (`SnapshotGenerationPreProcessor.process:137-152`, `mergeElementsFromAdditionalBase`). Sweep proof:
  `multi-profile` — `Patient.extension:pronouns` exists only in Java/golden (ELEMENT-SET G3c) and
  `Patient.gender mustSupport=true` comes from the additional base (min/MS mining M2). .NET ignores the
  extension entirely. The merge table itself (differential × differential, `mergeElementDefinitions`
  PRE:399-531) is documented in ch5's Java section (J-b, 2026-09-01): profile-wins fill-if-absent for
  descriptive props, nominal intersection for bounds (with min/maxLength picking the *looser* value —
  likely inverted), type intersection, binding×binding unimplemented (`throw new Error("not done yet")`),
  fail-fast throws on conflicts.
- **Obligation inheritance:** SDs carrying `inherit-obligations` make Java merge obligation-profile
  elements and set `mustSupport=true` where any obligation element has it
  (`ProfileUtilities.java:2544-2552`). Sweep proof: `profile-patient-op3` — `Patient.birthDate`/
  `Patient.deceased[x]` MS java-only (min/MS mining M3). .NET has no obligation semantics (ch9 notes the
  same for imposeProfile/interfaces).
- **Spec basis:** both extensions are defined in the extensions pack with generator-affecting semantics
  the core spec never mentions; whether a conformant generator *must* honor them is undecided (WGM
  adjacency: the mi-use-* interface family, DEV-015/DEV-028).
- **Status:** confirmed empirically (Java side); .NET side = verified absence.

## DEV-031 — Logical-model child placement drops a path segment (cdshooks) (ch9)
- **Evidence:** ELEMENT-SET mining G8 — `cdshooks-services`: diff has
  `CDSHooksServices.services.prefetch.key`/`.value`; Java/golden keeps them at
  `services.prefetch.key`/`.value`, .NET emits `services.key`/`services.value` — the `prefetch` segment
  dropped from both id and path.
- **Status:** suspected .NET path-rebasing bug for children of a logical-model element — **untraced**
  (no chapter mechanism explains it; nearest ch9). Trace queued.

## DEV-033 — Java preprocessor cross-slice contamination + silent constraint loss (ch6)
- **Evidence:** Phase-3 packet J-a (2026-08-31): code analysis @ b06c7ee + empirical confirmation in the
  `on-questionnaire` golden file (fhir-test-cases @ 9f495e8; expected-file generator version unpinned —
  commit-pair caveat). Formerly the DEV-025 "M1q" candidate; now **confirmed**. A Java-only bug — .NET has
  no propagation mechanism at all, so it exhibits neither effect.
- **Mechanism:** the preprocessor merges sliceStuff into each named slice by scanning the slice's *entire
  descendant range* with a match key of only **(path modulo `[x]`, sliceName-or-null)**
  (`elementsMatch`/`pathsMatch`, `SnapshotGenerationPreProcessor.java:812-845`) — element ids and
  inner-slice ancestry are never consulted. Extension-slice `value[x]` children (same path, no sliceName)
  are therefore indistinguishable across sibling extension slices.
- **Dual effect:** (1) *contamination* — sliceStuff authored for extension slice E1's `value[x]` fill-if-
  absent-merges into a **different** extension slice E2's `value[x]` inside the named slice; (2) *silent
  loss* — having "matched", the sliceStuff element is marked handled and is **not injected**, so the
  constraints the author wrote for E1's value never reach the named slice at all.
- **Minimal trigger shape:** outer non-extension slicing on `R.x` whose sliceStuff contains an (exempt)
  extension slicing + extension slice `E1` + a sliceName-less `R.x.extension.value[x]` row (e.g.
  `mustSupport=true`); a named slice `R.x:S1` containing a *different* extension slice `E2` with its own
  sliceName-less `value[x]` row. Result: E2's `value[x]` gains E1's `mustSupport` (and any other absent
  property from the 27-property fill list); E1's constraints are never materialized under S1.
- **on-questionnaire exhibits:** `item:group.extension:itemControl.value[x]` gains `mustSupport=true`
  authored nowhere (input has only a binding) *without* the sliceStuff's ontario mappings (`merge` lacks
  `mapping` — the fingerprint of the merge path); the group/question slices contain **no**
  `extension:renderStyle/enableWhenExpression/hidden.value[x]` rows (spuriously handled → never injected);
  the `display` slice — whose range has no `value[x]` at that path — got all three **injected** correctly,
  mappings included. The match-vs-inject asymmetry pins the mechanism.
- **Consequences:** authored conformance data (mustSupport, bindings, cardinalities) appears on the wrong
  elements of golden snapshots and disappears from the right ones — the golden files bless the bug.
  Candidate upstream report (Grahame Grieve / org.hl7.fhir.core); WGM-relevant as a caution against
  treating golden files as normative for the propagation mechanism (OQ-021).
- **Status:** confirmed (code + golden exhibit); minimized standalone repro not yet built (trigger shape
  documented above; on-questionnaire serves as the demo input meanwhile). **Reported upstream 2026-09-01
  as [hapifhir/org.hl7.fhir.core#2584](https://github.com/hapifhir/org.hl7.fhir.core/issues/2584)**
  (with ten more Java snapshot-generation bugs, #2585–#2594 — see the upstream-issues file in the
  project materials).

## DEV-034 — Per-property merge divergence catalogue: .NET `ElementDefnMerger` vs Java `updateFromDefinition` (ch5)
- **Evidence:** Phase 2 packet 1 (.NET, 2026-08-24) × Phase 3 packet J-b (Java @ b06c7ee, 2026-09-01);
  full side-by-side in ch5. All items code-derived; the Phase-4 sweep saw **zero** of them (the corpus's
  differentials stay on the agreeing paths — min/MS mining 2026-08-26 verified the common cases clean),
  so each needs a targeted probe if empirical confirmation is wanted.
- **(a) Frozen-by-omission set (Java) vs merge (NET):** Java's routine simply has no code for `code`,
  `representation`, `orderMeaning`, `meaningWhenMissing`, `defaultValue[x]`, `sliceIsConstraining` — a diff
  supplying them is **silently dropped** (deliberate for the †-frozen trio per PU:2906 comment; collateral
  for `code`, which §5.1.0.8 says is add/removable). .NET merges all of them (replace/union/overlay).
- **(b) Illegal min/max:** Java = diff wins + ERROR message (loosening min exempt for slices); .NET =
  most-restrictive, loosening **silently ignored** → different snapshots from the same illegal input
  (Java's has the loosened value). Related enforcement asymmetry: mustSupport/mustHaveValue true→false =
  Java ERROR (suppressed for slices via `fromSlicer`), .NET silent replace.
- **(c) isSummary:** Java **`throw new Error`** on any change when the base has a value — generation
  aborts (PU:3039-3048; the only hard-throw property rule in either engine). .NET silently replaces.
- **(d) isModifier / isModifierReason:** Java frozen (diff silently ignored) except on
  `extension`/`modifierExtension` elements (`checkExtensionDoco` gate); .NET replaces everywhere.
- **(e) Restated constraint key:** Java **drops** a diff constraint whose key already exists in the base
  (PU:3093; "constraints are cumulative. there is no replacing"); .NET overlay-merges it onto the
  inherited one. RFC-009's two live answers.
- **(f) Binding:** Java rebuilds (base copy → extensions cleared [`COPY_BINDING_EXTENSIONS=false`] →
  diff's extensions/strength/valueSet/description in; inherited **description dropped** unless restated;
  required-strength row + expansion-based value-set subset check enforced with ERRORs); .NET overlays
  (keeps base description/extensions, enforces nothing). Both delete non-bindable-type bindings, from
  independently maintained bindable-type lists.
- **(g) fixed[x]/pattern[x]:** Java wholesale replace + post-merge `checkTypeOk` type-vs-value check;
  .NET partial overlay for same/derived types (OQ-012), no type check.
- **(h) example:** union key = label+value (Java) vs label alone (NET); suppress extension always-on with
  `"$all"` wildcard (Java) vs `RespectSuppressExtension` setting (NET).
- **(i) ED extensions:** Java runs a 4-list policy machine (non-inherited purge / inherit-unless-redeclared /
  diff-ignored / override-in-place, PU:232-302, PU:3199-3217) and **appends duplicates** for unlisted urls
  present on both sides; .NET does a uniform union-by-url overlay. (The two engines' non-inheritable
  blocklists also differ — OQ-019.)
- **(j) label `"..."` append:** Java attempts the append convention on `label` but with swapped operands
  (`mergeStrings` PU:3156-3157: result = diff text with marker kept + CRLF + base text minus 3 chars) —
  broken; .NET doesn't support append on label at all. (definition/comment/requirements append is
  byte-identical in both, OQ-010.)
- **(k) Profile-root override:** for extension slices / single-type-profile elements Java replaces dest
  descriptive text (definition/short/comment/requirements/alias/mapping) from the referenced profile's
  root *before* merging the diff (PU:2619-2688, incl. the `checkExtensionDoco` wipe); .NET's counterpart
  is the ch7 root-merge of the type profile as a full element — different mechanism, overlapping effect.
- **Spec basis:** the interpretation-table footnotes (†/‡/∆) and §5.1.0.8 — the spec states obligations
  but no merge algorithm, and neither engine implements the † rules the same way (OQ-011).
- **Status:** confirmed (code-derived, both sides read). WGM feed: (c)+(b) are the sharpest "what must a
  generator enforce" exhibits; (e) feeds RFC-009; (f) feeds RFC-010. The outright Java-side *bugs* in this
  catalogue were reported upstream 2026-09-01: (j) label append =
  [org.hl7.fhir.core#2592](https://github.com/hapifhir/org.hl7.fhir.core/issues/2592); the obligation
  additional-binding inverted guard (under (f)/DEV-032) =
  [#2593](https://github.com/hapifhir/org.hl7.fhir.core/issues/2593); the additional-binding `any` no-op =
  [#2590](https://github.com/hapifhir/org.hl7.fhir.core/issues/2590); the additional-base pattern operand
  bug = [#2591](https://github.com/hapifhir/org.hl7.fhir.core/issues/2591).

## DEV-035 — Unmatched and out-of-order differential rows: Java drops with ERROR or appends by derivation, .NET silently creates New elements (ch4)
- **Evidence:** Phase 3 packet J-c deep-read 2026-09-01 — `ProfilePathProcessor.java:191-235, 827, 1311-1396`,
  `ProfileUtilities.java:842-867, 908-948, 1221-1226, 2444-2489, 3815-3869` @ `b06c7ee`;
  `ElementMatcher.cs:134, 517-535, 804-853`, `SnapshotGenerator.cs:887`; fail test t23a; the t23/t23a
  fixture pair; `SnapshotGeneratorManifestTests.cs:236-252, 916-917`.
- **The architectural difference:** .NET walks the differential's children and looks each up in the base
  (forward-only cursor); a child with no base sibling is a **`New` element, no issue**, for every derivation
  (`createNewElement`). Java walks the **base** and queries the remaining differential scope per base row
  (`getDiffMatches`); a differential row no base row pulled in is left unmarked, and then:
  - **specialization:** a second pass (`PU:842-867`) merges it onto an existing snapshot row by exact path or
    **appends** it after its parent's last child (inheriting the type's children when the diff walks into it;
    several types → throw);
  - **constraint (and everything else):** the row is an **orphan** — ERROR "No match found for `<id>` in the
    generated snapshot: check that the path and definitions are legal in the differential (including order)"
    per row plus one profile-level ERROR (`PU:908-948`), and the row is **dropped** (a `DefinitionException`
    instead when `setThrowException(true)`, `PU:1221-1226`).
  This is the spec's own split — constraint SDs may not introduce paths [elementdefinition #path]; only
  specializations have "new elements" [structuredefinition §5.4.6] — implemented literally on the Java side
  and not at all on the .NET side.
- **Ordering falls out of the same mechanism.** Neither side validates order (Java's in-walk warning is
  commented out, `PU:2465-2473`). In .NET a misordered sibling is behind the base cursor → `New` (t23a:
  duplicate `Patient.contact:males.telecom` with a fabricated `base.min`, DEV-027). In Java the one-match
  branch **jumps** the diff cursor to just past the matched row (`PPP:827`), so rows skipped over become
  orphans → ERROR + drop (t23a: `males.gender`). Java's *normalization* channel is outside the generator:
  `sortDifferential` (`PU:3815-3869`), which the shared-suite driver runs on every `sort="true"` test and every
  base in a chain — **t23 is t23a's exact differential with the flag set**, and passes. .NET has no sorting;
  its driver ignores the `sort` attribute and `Fix_t23` hand-swaps the fixture rows (DEV-010).
- **Two further matcher asymmetries (code-derived, no shared test):**
  - a differential row naming the `[x]` element when the *base* snapshot has a **renamed** choice element
    (R4-style base): Java matches (`isSameBase` is symmetric, `PU:2487-2489`), .NET does not — New + warning
    (`constructNew`, `ElementMatcher.cs:517-535`);
  - an *existing* slice name appearing out of base order, or a new slice placed before an existing one: Java
    throws `NAMED_ITEMS_ARE_OUT_OF_ORDER_IN_THE_SLICE` (`PPP:1373-1375`, lockstep slice walk); .NET's
    forward-only slice cursor sees a no-match → `Add` (a second slice with an existing name).
  - unnamed duplicate rows of one path with no slicing entry (non-extension): Java throws
    `DIFFERENTIAL_DOES_NOT_HAVE_A_SLICE` (`PPP:313-314`); .NET drops with an issue (`Invalid`).
- **Spec basis:** [elementdefinition #path] (constraints cannot define new paths), [structuredefinition
  §5.4.6] (ordering; new elements only for specializations) — both are shape rules with **no stated
  consequence for violations** (RFC-012 data point).
- **Status:** confirmed (code-derived both sides; t23a is the empirical exhibit). WGM/OQ-014 material: the
  two engines have chosen opposite ends of "reject vs propagate" for the same author error, and only one of
  them produces a diagnostic.

## DEV-036 — `sliceIsConstraining`: .NET enforces, Java ignores (ch4)
- **Evidence:** `ElementMatcher.cs:816-838` (`matchSlice`); grep for `sliceIsConstraining` (case-insensitive)
  over `ProfileUtilities.java`, `ProfilePathProcessor.java`, `SnapshotGenerationPreProcessor.java` @
  `b06c7ee`: **zero occurrences**.
- **.NET:** when a named diff slice carries `sliceIsConstraining`, the flag must agree with the actual
  name match: `true` with no matching base slice → `Invalid` + "no match" issue; `false` with a matching base
  slice → `Invalid` + "conflict" issue; the element is **discarded**. Absent → match ⇒ constrain, no match ⇒
  new slice.
- **Java:** slices are matched to base slices by **name equality alone** (lockstep walk `PPP:1311-1362`);
  unmatched names become new slices (`PPP:1370-1396`). The flag is never read, so both .NET `Invalid` cases
  proceed silently in Java: `true`+no-match → a new slice, `false`+match → a constraint on the inherited
  slice. The flag survives in the output only as an ordinary copied property.
- **Spec basis:** `sliceIsConstraining` (Trial Use): "If set to true, an ancestor profile SHALL have a
  slicing definition with this name. If set to false, no ancestor profile is permitted to have a slicing
  definition with this name" [elementdefinition-definitions] — a SHALL on the *profile author*, with no
  statement about generator behavior; the instance validator side is out of this document's scope.
- **Status:** confirmed (code-derived both sides; no shared test carries the property — verify with a
  targeted harness case if it becomes WGM material). Answers [OQ-006](14-open-questions.md#oq-006--sliceisconstraining)'s
  Java half; the remaining question is whether a *generator* has any obligation here.

