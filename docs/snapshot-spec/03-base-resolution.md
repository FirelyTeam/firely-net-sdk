# 3. Base resolution, rebasing and the root element

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

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

## .NET behavior (Phase 2)
*(pending — `SnapshotGenerator.generate()` :356-566, `getSnapshotRootElement` :2386)*

## Java behavior (Phase 3)
*(pending)*
