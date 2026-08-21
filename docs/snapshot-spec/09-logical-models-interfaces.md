# 9. Logical models & interfaces

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

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

## .NET behavior (Phase 2)
*(pending — logical-model branches in `generate()` (#1090, #3576), `Rebase`, interface extension support in
`SnapshotGeneratorExtensions.cs`)*

## Java behavior (Phase 3)
*(pending)*
