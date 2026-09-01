# 9. Logical models & interfaces

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 6,
> 2026-08-26: logical-model branches in `generate()`/`expandElement`; Phase 3 packet J-e, 2026-09-01: Java sweep).

## Scope
Snapshot generation for `kind = logical`: root path derivation from `type`, bases other than FHIR
resources, anonymous nested elements, unresolvable base types, rebasing. Plus the R5 interface mechanism
and the imposeProfile/compliesWithProfile extensions probed by the upstream `mi-use-*` tests.

## Spec baseline (R5)

### Logical models

> "The same definition structure can also be used to define any arbitrary structures that are a directed
> acyclic graph with typed nodes, where the primitive types are those defined by the FHIR specification."
> [structuredefinition §5.4.6.5]

Everything else is encoded in invariant carve-outs rather than prose:

| Rule (non-logical) | Logical-model carve-out |
|---|---|
| `type` is a relative name | `type` is an **absolute URL, required** [structuredefinition §5.4.5] |
| snapshot root path = `type` (sdf-11, sdf-8) | root path = the *type name* (last segment), not the full url |
| root has no `type` component (sdf-15/15a) | root **may** have a `type` |
| every snapshot element has definition/min/max (sdf-3) | exempt |
| FHIR-defined types only (sdf-19, spec models) | FHIRPath `System.*` type urls permitted |
| bindings on bindable FHIR types (eld-11) | escape hatch for absolute-URL (`:`-containing) type codes |
| profiles cannot define default values | "default values may be defined for logical models" [profiling §5.1.0.7]; specialization-only (sdf-21) |

**Rebasing/merge mechanics for logical models are unstated**: how elements merge with `Base` / `Element` /
another logical model when `baseDefinition` is present is not described anywhere (same gap as
specialization rebasing, ch3). Anonymous nested elements (no type, children defined inline) are implied by
sdf-3's exemption but never discussed.

### Interfaces

The R5 surface is minimal: §5.4.6.2 example 9 shows the shape (`abstract: true`, `kind: resource`,
`type: CanonicalResource`) and marks interfaces as spec-only definitions. The `structuredefinition-interface`
extension mechanism and how implementing an interface affects derivation/snapshots appear **nowhere** on the
extracted pages. (The upstream test ids `mi-use-derived`, `mi-use-distinct`, `mi-use-imposed` probe this
area — the semantics they test come from the extensions pack and tooling practice, not the core pages.)

### imposeProfile / compliesWithProfile / obligations

`structuredefinition-imposeProfile` and `structuredefinition-compliesWithProfile` are referenced **by link
only** [structuredefinition §5.4.4] — no semantics on the page (and the rendered link text "Compiles With
Profile" is a typo). Obligations are not mentioned on either page; mustSupport's definition notes it "is
being phased out and replaced by obligations", with obligations addable but never loosened
[profiling §5.1.0.22].

### Spec gaps (logical models & interfaces)

1. Logical-model derivation mechanics (rebasing onto Base/Element/other logical models) — unstated.
2. Whether a logical-model snapshot generator must synthesize definition/min/max is *version-forked*
   (see delta below).
3. Interface/imposeProfile semantics for snapshot generation — outside the core spec entirely; must be
   sourced from the extension definitions, the Java implementation, and the `mi-use-*` tests.

## R4/R4B deltas

- **sdf-3 relaxed in R5**: R4 required definition/min/max on *every* snapshot element including logical
  models; R5 exempts logical models. A dual-version generator must synthesize these for R4 logical models
  but may omit them in R5. (Behavior fork.)
- Interfaces and logical-models-rooted-on-`Base` are **new derivation patterns in R5** §5.4.6.2; R4 had
  neither.
- Logical-models narrative (§5.4.6.5): otherwise verbatim identical.

## R6-build note (v6.0.0-ballot4, fetched 2026-08-21)

defining-extensions.html gains notes on **interfaces as extension contexts** — the first real spec text
touching how interfaces participate in profiling mechanics (this chapter's thinnest area). Also new in R6:
structuredefinition §5.4.6.6 rules for defining *additional (custom) resources*. Both pages are in the
local corpus (`spec-html/R6-build/`) pending extraction.

## .NET behavior (Phase 2, deep-read 2026-08-26)

Logical models run through the same pipeline with four dedicated carve-outs (`kind == Logical`):

1. **Root path from `type`** (#1090, `SnapshotGenerator.cs:440-476`): the base snapshot is rebased onto the
   *last segment* of the (absolute-url) `type`. A missing `type` is non-fatal for logical models only:
   warning 10014 (STRUCTURE_TYPE_MISSING) and the root name is parsed from the first differential element;
   if that fails too, throw. (Non-logical structures throw immediately on missing `type`, `:490-493`.)
2. **Unresolvable `Base`** (#3576, `:386-404`): a logical model deriving from
   `…/StructureDefinition/Base` — resolvable only since R5, though tooling stamps the version-independent
   canonical regardless of release — gets warning 10017 and the derivation chain terminates there
   (functionally identical, since Base is an empty abstract root); the snapshot then contains only the
   model's own content, and `ensureBaseComponents` silently skips too (ch3,
   `SnapshotBaseComponentGenerator.cs:41-49`). Any other unresolvable base, or Base on a non-logical
   structure, stays fatal.
3. **Anonymous nested elements** (`expandElement`, `:691-710`): an element with neither `type` nor
   `contentReference` is an error for ordinary structures (`PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF`,
   children dropped) but valid inside logical models — "their children are defined inline by the
   differential (optionally described by the type-specifier extension)"; expansion reports success without
   base children so the differential children merge as **new elements** (ch7 `createNewElement`,
   self-referential Base from the diff's own min/max).
4. **No sdf-3 synthesis** (verified absence): nothing outside the ordinary merge writes
   `definition`/`min`/`max`, so elements whose base chain never supplied them (possible exactly in the
   logical/anonymous cases above) stay unfilled. Legal in R5 (logical exempt), **a gap for R4 logical
   models** where sdf-3 still applies — Phase-4 check against Java.

**Interfaces**: the generator's only interface awareness is *derivation-chain skipping* (ch3): when
resolving a base for **specializations**, `getParent` walks past ancestors marked with the
`structuredefinition-interface` extension (e.g. `CanonicalResource`, `MetadataResource`) so the effective
base is the first non-interface ancestor (`:297-343`); constraint profiles resolve their base normally.
That same extension is on the non-inheritable blocklist (ch12), so an interface marker is never inherited
into derived snapshots. There is **no support** for `imposeProfile`/`compliesWithProfile` or any
interface-conformance semantics — the upstream `mi-use-*` tests are among the 102 never integrated
(ch1/ch13 context; Java comparison in Phase 3 will define the gap).

## Java behavior (Phase 3 sweep, 2026-09-01)

Citations `PU`/`PPP` @ `b06c7ee`; detail in the materials extract `java-ch08-12-sweep-2026-09-01.md`.

- **No carve-outs for missing metadata**: `type` and `derivation` are mandatory on every SD (`PU:750-758`,
  throw) — no parse-from-differential repair; an absolute-url `type` is reduced to its tail by
  `getTypeName()` for LOGICAL kinds, which drives both the differential path check and the specialization
  rebase (ch3). A `type` on the root element is allowed only for LOGICAL kinds (`PU:882-883` snapshot side,
  `PU:1318-1323` differential side).
- **Logical types in the walk**: a child typed with a logical model passes the step-in gate via
  `isBaseResource` ("types non-empty and none is `Resource`", `PU:1700-1709`, gate `PPP:828`), not
  `isDataType` (COMPLEXTYPE + specialization only); `isMatchingType`'s url-equals-code branch (`PU:1650`) is the
  logical-model type check. The type SD is fetched by working code like any datatype.
- **New elements in specializations** (`PU:842-867`, after the walk): differential rows the walk did not consume
  are merged onto a same-path element if one exists in the current context, else copied and **inserted after
  the last child of their parent**; a row whose next diff row is a child either throws ("Unsupported scenario:
  specialization walks into multiple types") or gets the type's snapshot children copied under it
  (`addInheritedElementsForSpecialization`, `PU:1263-1283`: path string-replace, type root constraints appended,
  non-inherited extensions added if absent). Constraint profiles never reach this code (their orphans are
  ERRORs, ch4/DEV-035). .NET's counterpart is `createNewElement` + inline children (ch7).
- **No sdf-3 synthesis** either: `PU:969-975` fills only a missing `base` from the element's own path/min/max.
  Code-derived agreement with .NET — R4 logical models get no definition/min/max from either generator.
- **Choice-group pruning — Java-only** (`checkGroupConstraints`, `PU:1333-1405`, right after the walk): for
  each non-sliced, non-prohibited element, every invariant whose FHIRPath is a parenthesised union of child
  names compared to the constant `1` (`= 1` mandatory group, `<= 1` optional group; `readChoices`/
  `processConstraint`, `PU:4820-4870`) defines a choice group; if exactly one member has `min = 1`, every other
  member is set `max = 0` and its **subtree removed** from the snapshot; two mandatory members → `throw new
  Error`; duplicate child names → `throw new Error("huh?")`. A logical-model/xml-choice-group idiom with no
  spec basis and no .NET counterpart.
- **Interfaces**: nothing in the profile package, the context package or the rest of `conformance/` reads
  `structuredefinition-interface` (only the constant is defined). Java does not skip interface bases (ch3);
  the nearest analogue is `checkTypeDerivation` treating **abstract or LOGICAL** base types as walkable
  ancestors for type-derivation checks (`PU:3276`).
- **imposeProfile / compliesWithProfile**: read only inside the *targetProfile* derivation check
  (`sdConformsToTargets`, `PU:3333`: a derived target may satisfy the base's target through an imposed
  profile); PRE:607 "we ignore impose and compliesWith - for now?". `mi-use-imposed` passes in Java because
  the imposed profile satisfies that check — nothing is merged from it. `mi-use-distinct` fails on the same
  check (DEV-028 (e)).
- `populateLogicalSnapshot` (`PU:4546-4567`) is a separate utility (diff root + base children + diff children
  re-rooted by string prefix) with no caller in the generator cone — a publisher-side fallback, not part of
  `generateSnapshot`. Type parameters (`checkTypeParameters`, ch3) are the other Java-only logical-model surface.
- DEV-031 (cdshooks segment drop) has no Java-side counterpart mechanism: logical-model children go through
  the ordinary path remap (`fixedPathDest`, `PU:2051-2071`); the .NET trace is still open.

## Deviations
- [DEV-031](13-deviation-register.md#dev-031--logical-model-child-placement-drops-a-path-segment-cdshooks-ch9) —
  Phase-4 sweep, untraced: cdshooks-services diff children `services.prefetch.key`/`.value` emitted by
  .NET as `services.key`/`services.value` (the `prefetch` segment dropped).
- Phase-4 open trace: `xt-logical` — .NET cannot resolve the EHDS logical-model base chain
  (`EHDSDataSet` unresolvable → generation fails) where Java resolves it from the same universe;
  harness-health vs real ch9 resolution gap to be determined (element-set extract §G5).
