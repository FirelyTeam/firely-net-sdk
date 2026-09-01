# 3. Base resolution, rebasing and the root element

> Status: **spec baseline + .NET + Java behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 3,
> 2026-08-24: `SnapshotGenerator.generate`/`ensureSnapshot`/`getSnapshotRootElement` deep-read; Phase 3 packet
> J-d, 2026-09-01: `generateSnapshot` preamble/epilogue, `cloneSnapshot`, `findProfile`, `makeBaseDefinition`).

## Scope
Resolving `baseDefinition` (and generating *its* snapshot on demand), deep-copying the base snapshot as the
starting point, rebasing paths when the derived type's root path differs, root element special-casing, and
what is inherited vs stripped from the base before merging. Also: missing differential handling and
validity requirements on the base.

## Spec baseline (R5)

### The derivation chain

Profiles derive from profiles arbitrarily deep ("Re-profiling", complexity warning only, no depth limit)
[profiling §5.1.0.17]. A constraint SD's `type` names the constrained type, equals the base's type, and
"cannot be an abstract type" [structuredefinition §5.4.5]. Nothing states that the base must already have
a snapshot — on-demand recursive generation is implied by the existence of differential-only SDs, never
described.

### What pins the snapshot root (non-logical models)

| Key | Rule |
|---|---|
| sdf-11 | "If there's a type, its content must match the path name in the first element of a snapshot" |
| sdf-8 | all snapshot elements start with the SD's `type`; tail paths start with `root.path + '.'` |
| sdf-15 | "The first element in a snapshot has no type unless model is a logical model" |

For `derivation = specialization` these rules *force* path rebasing (base `Resource.id` becomes
`Patient.id` in Patient's snapshot) — but **the renaming step is never described**: what happens to the
inherited elements' ids and `base` components during specialization is unstated (spec gap; logical models
inherit the same problem, see ch9).

### Root element semantics

> "The very first element in a snapshot (the one with an element id of just the type name) defines
> characteristics that apply to the type as a whole. Some of these characteristics affect the constraints
> that can hold on an element that references the type. For example, if the root element cardinality is
> 0..1, then an element declared to be of that type cannot have a maximum cardinality greater than 1."
> [structuredefinition §5.4.6.1]

The interpretation-table § footnote says the same from the ElementDefinition side (profiles referencing a
type must fall within the type root's cardinality bounds — most common for extensions)
[elementdefinition #interpretation]. This is directly load-bearing for extension expansion (ch7).

Root constraints from invariants: no slicing / sliceName / label / code / requirements on the root
(sdf-20/23/9); a root may carry `constraint` and `mapping` (interpretation table: optional on first
elements). The root's `definition` may duplicate `StructureDefinition.description` [structuredefinition
§5.4.6.1].

### Derivation rules that constrain the merge start-point

- "A regular extension cannot be 'constrained' to be a modifier" [extensibility §2.1.5.0.2] — a constraint
  must not flip the root Extension element's `isModifier` false→true.
- Constraint SDs may not contain paths not defined in the base type [elementdefinition #path] — matching
  failures are input errors, not new elements (ch4, ch12).
- `defaultValue` may appear only in specializations (sdf-21); FHIR-spec models never have defaults (sdf-22).

### What the generator must fill in

sdf-3 + sdf-8b define the *minimum fill obligation*: every snapshot element must carry `definition`,
`min`, `max` (sdf-3, logical models exempt) and `base` (sdf-8b, no exemption) — even where the
differential is silent. See ch10 for `base` semantics.

### Spec gaps (base resolution)

1. On-demand snapshot generation of bases (and cycles among them) — never described (ch11).
2. Path/id/base-component rewriting during specialization — never described.
3. Whether a *missing differential* means "snapshot = base snapshot" is not stated anywhere.
4. Extension `context`/`contextInvariant` inheritance under constraint is unspecified (sdf-5 requires
   *some* context on extension constraints, unrelated to the base's).

## R4/R4B deltas

- **Headline:** R4 treated the type root's min/max as "optional (irrelevant)"; R5 introduced the
  root-cardinality-constrains-referencing-elements rule (§ footnote + structuredefinition §5.4.6.1). An
  R4-semantics generator that ignores extension-root cardinality is spec-compliant *for R4*; an R5 one is
  not.
- sdf-27 (baseDefinition ⇒ derivation) is new in R5.

## .NET behavior (Phase 2, deep-read 2026-08-24)

All citations `SnapshotGenerator.cs` unless noted.

### Resolving the base (`resolveBaseDefinition`, `:328-343`; `generate`, `:356-542`)

A constraint SD without `baseDefinition` **throws** (`:372-375`). The base canonical is resolved through the
configured `IAsyncResourceResolver`; any resolved SD carrying the `structuredefinition-interface` extension
(the R5-preview `CanonicalResource`/`MetadataResource` "interface classes") is **skipped**: resolution walks
on to *its* base until a non-interface SD is found (`:334-340`; interface semantics in ch9). An unresolvable
base is fatal (issue + abort, `:398-403`) with one carve-out (#3576): a **logical model** deriving from the
`Base` canonical — unresolvable before R5 because `Base` wasn't published as a StructureDefinition — gets a
non-fatal issue and proceeds base-less, which is functionally identical since Base is an empty abstract root
(`:385-397`, `BASE_TYPE_CANONICAL` `:313`).

### On-demand snapshot generation of the base (`ensureSnapshot`, `:2306-2356`)

If the resolved base lacks a snapshot (or has a foreign one under `ForceRegenerateSnapshots`), the generator
recurses into `generate` for the base — gated by `GenerateSnapshotForExternalProfiles`; with that setting
off, a snapshot-less base is a fatal issue. A snapshot produced this way is annotated
`createdBySnapshotGenerator` to prevent repeated regeneration (`:2338`). Recursion/cycle detection wraps the
expansion via the recursion stack (`OnBeforeExpandTypeProfile`, ch11).

### The merge start-point: deep copy + rebase

The working snapshot is a **deep copy of the base's snapshot** (`:428`). Then, per derivation kind:

- **Constraints:** no rebasing — paths already match the type.
- **Specializations:** `Rebase(structure.Type)` (`:478-497`); a missing `type` throws.
- **Logical models:** rebased onto the last segment of `structure.Type` (a full url is allowed); missing
  `type` → issue, root name parsed from the first differential element instead; still undeterminable →
  throw (`:441-476`).

`Rebase` rewrites **paths only** (root becomes the new path, descendants re-prefixed segment-wise —
`ProfileNavigationExtensions.cs:35-53`). The spec's unstated companions of the rename (ch3 spec gap 2) are
handled separately: when `GenerateElementIds` is set, element **ids** are force-regenerated, never inherited
from the base (`:510-514`, with the `Questionnaire.item.item` caveat behind
[OQ-009](14-open-questions.md#oq-009--element-id-stability)); with the setting off, the deep copy keeps the
base's ids;
**`base` components** are inherited when present, generated when missing (`ensureBaseComponents`, `:518`,
ch10). Finally the copied base snapshot is scrubbed of non-inheritable extensions (changed-by-diff markers)
and internal annotations (`:523-524`).

A missing differential was already replaced by an empty one (`:362-369`, ch2), so "snapshot = rebased base
snapshot + generator fill" is .NET's de-facto answer to spec gap 3 — for full generation, but *not* for root
resolution, see below.

### Root element resolution (`getSnapshotRootElement`, `:2386-2502`)

Used when only a profile's *root* is needed (notably type-profile expansion, `getBaseElementForTypeRef`
`:2373`). A four-step cascade:

1. cached root-element annotation on the SD (`CACHE_ROOT_ELEMDEF`, `:2398-2402`);
2. root of an existing (trusted) snapshot (`:2405-2410`);
3. root of a *partially generated* snapshot higher up the recursion stack (`:2412-2421`);
4. recursive resolution: resolve the base's root the same way, deep-copy it, rebase its path to the
   differential root's path, and merge the differential root on top (`mergeElementId: true`, `:2496-2502`) —
   which also stamps `constraint.source`, though only when the diff root declares constraints (ch5). A core
   type (no base) takes its root directly from the differential and stamps `constraint.source` explicitly
   (`:2446-2464`, `:2461`).

Notable asymmetry: an SD **without any differential is rejected here** (issue "profile has no differential",
`:2391-2396` — the in-code TODO acknowledges it should return the base root) even though full generation
accepts the same SD via the synthesized empty differential. Observable consequence: a differential-less SD
can be fully snapshotted, but cannot serve as a type profile whose root must be merged —
[OQ-016](14-open-questions.md#oq-016--what-does-a-differential-less-structuredefinition-mean). The
base-chain walk here also has **no cycle detection** (in-code TODO `:2469-2473`; the main recursion stack
does not cover this path — ch11).

## Java behavior (Phase 3, deep-read 2026-09-01)

Citations `PU` = `ProfileUtilities.java` @ `b06c7ee` (master `4f52ba6` is shifted +1 line from `PU:222`
onward). Detail in the materials extract `java-ch07-type-expansion-and-ch03-base-resolution-2026-09-01.md` §F.

### Entry checks (`generateSnapshot`, `PU:740-776`)

Java's API takes the **already-resolved base** as a parameter (`generateSnapshot(base, derived, url, webUrl,
profileName)`): resolving `baseDefinition` is the caller's job (test driver / validator / IG publisher), not
the generator's. All failures are `DefinitionException`s:

- null base or derived; either side `isGeneratingSnapshot()` → `FHIRException` "Attempt to use a snapshot on
  profile {0} as {1} before it is generated" (`checkNotGenerating`, `PU:1694-1698`).
- **`type` is mandatory on both SDs** (`Base profile {0} has no type` / `Derived profile {0} has no type`) — no
  logical-model repair (contrast .NET's parse-from-differential warning path, ch9); missing `derivation` →
  throw.
- **Constraint profiles must have `base.type == derived.type`** (`PU:759-760`, "Base & Derived profiles have
  different types ({0} = {1} vs {2} = {3})") — the check .NET lacks (`t29b`, DEV-028 (i)). Specializations exempt.
- **On-demand base generation is unconditional** (`PU:762-768`): a snapshot-less base has *its* base resolved
  via `findProfile` (null → "Unable to find base {0} for {1}"), `checkNotGenerating`, and is generated
  recursively — no `GenerateSnapshotForExternalProfiles`-style gate, no `createdBy` annotation (the SD's own
  `generatedSnapshot` flag is set instead, `PU:1094`).
- `fixTypeOfResourceId(base)` (`PU:1290-1307`): for RESOURCE kinds at R4+, every element with
  `base.path == "Resource.id"` gets type `http://hl7.org/fhirpath/System.String` + `fhir-type = id` — written
  into the **base SD's snapshot and differential**, i.e. the generator mutates the context's base artifact.
- Type parameters (`http://hl7.org/fhir/tools/StructureDefinition/type-parameter` on the base,
  `checkTypeParameters` `PU:1159-1180`): the derived SD must declare a parameter whose type descends from the
  base's, else throw — a Java-only surface (R6 generic-type tooling) with no .NET counterpart.
- **Cycle guard**: `snapshotStack.contains(derived.url)` → "Circular snapshot references detected; cannot
  generate snapshot (stack = …)" (`PU:774-776`), keyed by canonical url like .NET's; pushed/popped around the
  whole generation, and on *any* exception the half-built snapshot is **nulled** (`PU:1078-1084`) — no partial
  output ever escapes (ch11, J-e).

### The merge start-point

Java does **not** deep-copy the base snapshot as a whole. The walk (`ProfilePathProcessor.processPaths`,
`PPP:155-183`) reads the base snapshot in place — base cursor 0, diff cursor 0, diff limit **`-1` for an empty
differential** — and emits fresh copies row by row (`currentBase.copy()` + `updateURLs` + `updateFromBase` per
row, ch4/ch10). Consequences relative to .NET's copy-then-merge:

- **Constraints**: no rebasing; paths are remapped only during step-ins (`fixedPathDest`).
- **Specializations**: `cloneSnapshot(baseSnapshot, baseType, derivedType)` (`PU:1493-1508`) copies every base
  row rewriting **both `id` and `path`** by `replaceFirst(baseType, derivedType)` — a first-occurrence string
  replace on the type *name* (safe because the type name is always the first segment). .NET's `Rebase`
  rewrites paths only and regenerates ids separately (OQ-009).
- **Logical models**: no rebase at all — Java requires the differential's root path to be the type name
  (`checkDifferential`, ch2) rather than renaming the base.
- Ids are always regenerated afterwards (`setIds`, `PU:886`, ch10); `base` components are inherited/derived
  per row (`updateFromBase`, ch10) and, for specializations, any element still lacking one gets a
  self-referential `base` (`PU:969-975`, sdf-8b fill).
- SD-level extensions of the base are copied to the derived SD per the extension definition's
  `snapshot-behavior` policy (`copyInheritedExtensions`, `PU:1228-1253`: `ignore` / `add` / `overwrite` /
  default add-if-absent; markdown values get relative-url processing) — the SD-level twin of the element-level
  extension policy machine (ch5, DEV-034); .NET has no SD-level inheritance.
- A `snapshot-base-version` extension recording `base.version` is stamped on the generated snapshot component
  (`PU:1091-1093`) — provenance .NET does not record.

### Missing differential

A differential-less SD walks with diff limit `-1`: every base row is copied with the fill obligations —
snapshot = base copy (same answer as .NET's main path, OQ-016). As a **type profile** the same SD is also
accepted: the template is its snapshot root (`PPP:754`), generated on demand if absent (`PPP:725-731`), with no
"has no differential" refusal — so Java answers OQ-016 consistently in both roles where .NET refuses the second.

### Root resolution for type profiles

Java has no `getSnapshotRootElement` cascade: a type profile's root is always its snapshot's first element
(`PPP:754`, `PU:2654`), generated on demand if the snapshot is missing (`PPP:725-731`) and, for a profile still
mid-generation, read from the partially built snapshot **only if its first element is already populated**
(`PPP:719-724`, throw otherwise). Where .NET caches roots (`CACHE_ROOT_ELEMDEF`), Java relies on the
`isGeneratingSnapshot` flag + this first-element rule.

### Resolving canonicals (`findProfile`, `PU:4095-4145`)

The central resolver strips any `#fragment` **silently** (so a legacy `url#element` type profile resolves to
the whole SD, ch7/OQ-017), splits `url|version`, then applies three expansion `Parameters`:
`default-profile-version` (used only when the reference has no version), `force-profile-version` (always
overrides), `check-profile-version` (mismatch → `FHIRException` "Profile resolves to … which does not match
required profile version …"). References originating from a **core** package drop their source-package pin
("switch the extension pack in", `PU:4141-4143`). Used for bases, obligation profiles and most type-profile
lookups — but the PPP template selection (`PPP:698`, `726`), the sliced-base min/max pick-up (`PPP:1406`) and the
extension-policy lookup (`PU:1256`) call the context directly, so version handling is **inconsistent within
one generation**. .NET has no version-parameter concept; `|version` passes to the resolver unparsed.

### `Base` and interfaces

- Pre-R5 contexts lacking a `Base` StructureDefinition get one synthesized (`makeBaseDefinition`,
  `PU:4777-4808`: abstract COMPLEXTYPE, root `Base 0..*` in snapshot and differential) — Java's answer to the
  problem .NET solves with the #3576 issue-and-proceed carve-out.
- **No interface skip**: nothing in PU/PPP/PRE reads `structuredefinition-interface` (grep: only
  `getAbstract()`/LOGICAL in `checkTypeDerivation`, `PU:3276`). Java's generator walks whatever base the caller
  hands it; whether interface SDs are hidden by the context loader is outside the profile package (ch9, J-e).

### Relative urls (`updateURLs`, `PU:2135-2176`)

Every emitted row has `#local` `binding.valueSet`, `type.profile`, `type.targetProfile` prefixed with the
profile url (nb `PU:2143`/`2147` concatenate the *list*'s `toString()` rather than the element value — latent,
`#`-relative profiles are essentially unused) and markdown fields rewritten against the web url — a
publishing concern the .NET generator does not have.

## Deviations
- [DEV-030](13-deviation-register.md#dev-030--cross-version-bases-net-rebuilds-against-r5-core-leaving-r5r4-hybrids-ch3) —
  Phase-4 sweep: R4-based base chains re-expanded against R5 core (R5-only elements injected; stale R4
  `reported[x]` kept beside R5 `reported`). Suspected version-artifact of running R4 IGs through the
  R5-only SDK; the hybrid outputs are .NET merge products.
- [DEV-032](13-deviation-register.md#dev-032--java-only-merge-inputs-additionalbase-and-obligation-profiles-ch3) —
  Java-only merge inputs confirmed empirically: `structuredefinition-additionalBase` (multi-profile) and
  `inherit-obligations` obligation profiles (profile-patient-op3); .NET ignores both.
- Fail-test evidence for this chapter: SD.type vs base-type coherence never checked (t29b), DEV-028
  group (i).

## Open questions
- [OQ-009](14-open-questions.md#oq-009--element-id-stability) element id regeneration vs inheritance.
- [OQ-016](14-open-questions.md#oq-016--what-does-a-differential-less-structuredefinition-mean)
  differential-less StructureDefinitions.
