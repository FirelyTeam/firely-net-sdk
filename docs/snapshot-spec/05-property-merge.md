# 5. Per-property merge semantics

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

## Scope
The heart of the algorithm: for each `ElementDefinition` property, what happens when the differential
provides a value and the base already has one. End state: a complete property table with, per property,
merge class, matching key for collections, spec basis, and both implementations' behavior.

## Spec baseline (R5)

### The headline: there is no merge algorithm

The spec defines what a valid differential element and a valid snapshot element each look like — it never
states how to compute `snapshot = base + differential`. Explicit "absent in differential → inherit from
base" exists for exactly **two** properties: `min`/`max` [elementdefinition #min-max] and `mustSupport`
[elementdefinition #missing, which adds "the snapshot SHOULD always populate the mustSupport value"].
Everything else is inference from the rules below. The general principles:

> "Any changed definitions SHALL be restrictions that are consistent with the rules defined in the
> resource in the FHIR Specification from which the profile is derived." [profiling §5.1.0.9]

> "Note that structure definitions cannot 'remove' mappings and constraints that are defined in the base
> structure, but for purposes of clarity, they can refrain from repeating them." [profiling §5.1.0.9]

**Omission is never removal** — silent differentials carry base content forward.

### The interpretation table and its four footnotes

[elementdefinition #interpretation] classifies every property as prohibited/optional/required per context
(type definition vs constraint definition × first vs following elements). Its four footnotes are the
spec's core merge semantics:

- **†** "The element's presence, and value, must match the definition in the base definition" —
  `defaultValue[x]`, `meaningWhenMissing`, `isModifier`, `isSummary`, and min/max on following elements.
- **‡** "The element content must be consistent with that matching element in the base definition" —
  `short`/`definition` (required), `requirements`/`comments`/`alias` (optional).
- **∆** "Additional constraints and mappings can be defined, but they do not replace the ones in the base
  definition" — `constraint`, `condition`, `mapping`.
- **§** type-root cardinality constrains references to the type (see ch3/ch7).

Known defects of the table (see [RFC-003/004/007](15-spec-rfcs.md)): the `base` row reads "required" in
both constraint columns (resolved by the definitions page: base is a *snapshot* obligation the generator
fulfills, ch10); one row still says `nameReference`; no rows exist for `mustHaveValue`,
`valueAlternatives`, `sliceIsConstraining`, `binding.additional`, `representation`, `orderMeaning`,
`isModifierReason`.

### Property-by-property: what the spec actually pins down

**Cardinality (`min`/`max`)** — differential-absent → inherit [#min-max]; snapshot must populate (also
sdf-3). Narrowing-only, from profiling §5.1.0.6 (verbatim table): derived `[min,max]` must be a sub-range
of base `[min,max]` ("the constraining profile can only allow what the base profile allows"). New R5 rule:
with `min = 0`, fixed/pattern apply only if the element is present.

**Descriptive properties** — profiling §5.1.0.8 (new table in R5): "The meaning and guidance provided in
the base resource or profile can't be invalidated, only constrained or contextualized."

| Property | Revise? | Add? | Remove? |
|---|---|---|---|
| label | Yes | | |
| code.coding | | Yes | Yes |
| short | Yes | | |
| definition | Yes | | |
| comment | Yes | Yes | Yes |
| requirements | Yes | Yes | |
| alias | Yes | Yes | Yes |
| example | Yes | Yes | Yes |
| mapping | Yes | Yes | Yes |

This licenses author intent (including *removal* for some collections — in tension with the ∆-additive
model for mapping) but still doesn't say how a generator merges (does a differential `alias` list replace
or union with the base's? unstated).

**`mapping`** — the one property with explicit replace-by-key semantics: "providing a new mapping with the
same identity … means that the new mapping replaces a mapping with the same identity in the element being
profiled" [profiling §5.1.0.9, new in R5]; otherwise additive (∆); eld-27 (warning) unique-by-identity.

**`constraint`** — additive only: "Profiles may define additional constraints … but they cannot alter or
remove constraints that are already applied" [conformance §2.1.1.0.6]. `constraint.source` = "A reference
to the original source of the constraint, for traceability" — generators are expected to stamp/preserve it
on inherited constraints. `constraint.suppress` (R5, Trial Use) suppresses an inherited warning/hint;
eld-26: errors cannot be suppressed. Tension: constraints "must be unique by key" (eld-14) vs additivity
when a differential restates an inherited key ([RFC-009](15-spec-rfcs.md#rfc-009--eld-14-vs-additive-constraints-restating-an-inherited-constraint-key)).

**`condition`** — additive (∆), union.

**`binding`** — strength lattice [profiling §5.1.0.21]: required→required; extensible→required/extensible;
preferred→anything but example stays; example→anything. "Whatever the constraining profile does, it cannot
make codes valid that are invalid in the base profile." Value-set replacement rules per strength
[§5.1.0.20]. `binding.additional` (R5, Trial Use): "Additional bindings do not replace the main binding".
eld-23 (description or valueSet), eld-11 (bindable types only), eld-12 (valueSet url forms). **Merge
granularity unstated** — wholesale replace vs per-sub-element ([RFC-010](15-spec-rfcs.md#rfc-010--binding-merge-granularity-unstated)).

**`type`** — "The Type of the element can be left blank in a differential constraint, in which case the
type is inherited from the resource" [elementdefinition-definitions]. A profile may "restrict the types of
a choice element" [profiling §5.1.0.9]. `profile`/`targetProfile` are 0..* disjunctions ("content must
conform to at least one"). eld-13 (unique by code), eld-4/17 (aggregation/targetProfile only on
reference-ish types). **List-merge semantics unstated** — subset enforcement, and whether
profile/targetProfile lists on a matching code replace or append ([RFC-008](15-spec-rfcs.md#rfc-008--replace-vs-append-semantics-unstated-for-most-repeating-properties)).

**`fixed[x]` / `pattern[x]`** — optional in constraint differentials, prohibited in type definitions.
Exact matching semantics defined (primitive exact match; pattern on arrays: each pattern item matches at
least one instance item, recursively; complex: per-property recursive). eld-6/7 (single-type only), eld-8
(mutually exclusive), eld-24 (guideline: prefer pattern). **Compatibility with an inherited base
fixed/pattern is unstated.**

**Flags** — `mustSupport`: false→true only, never true→false [profiling §5.1.0.22]; absent → inherited
[#missing]. `isModifier` (+`isModifierReason`, eld-18): frozen — "The value of the flag cannot be changed
by profiles on the resource, in either direction" [conformance §2.1.1.0.4]. `isSummary`: † (must match
base). Obligations may be added, "existing actor obligations cannot be undone or loosened" [profiling
§5.1.0.22].

**Frozen / base-standard-only properties** — `defaultValue[x]` ("can never be changed"; profiles never
define them, logical models may), `meaningWhenMissing` ("can never be changed"; eld-15 mutually exclusive
with defaultValue), `representation` ("profiles must reproduce what the base standard does"),
`orderMeaning` ("if absent in the base type, a profile cannot assert meaning"), `contentReference` (ch8),
`base` (generator-populated, ch10).

**Value-domain constraints** — `minValue[x]`/`maxValue[x]` (inclusive; type-matching rules; canonical
Quantity comparison; **no narrowing rule stated**), `maxLength` (no conformance expectation when absent;
merge unstated), `mustHaveValue`/`valueAlternatives` (R5 Trial Use shortcuts for primitive `.value`
profiling; eld-28 mutually exclusive; valueAlternatives-absent = any extension allowed).

**`code`, `alias`, `example`** — repeating; replace-vs-append **unstated** ([RFC-008](15-spec-rfcs.md)).

**`short`/`definition`/`comment`/`requirements`/`label`** — ‡-consistency with base; universally
implemented as differential-replaces-base, but that is inference, not spec text. *(Note: the `"..."`
append-to-inherited-text convention used by implementations appears nowhere on these pages — see
[OQ-010](14-open-questions.md#oq-010--the--append-convention).)*

### Post-merge validity

No rule says which invariants must be *re-verified after merge* — e.g. base pattern + differential
narrowing the type list to 2 creates an eld-7 violation neither input had. Generator obligations here are
entirely unspecified (ch12).

## R4/R4B deltas

- New in R5: `mustHaveValue`, `valueAlternatives`, `constraint.suppress`, `binding.additional` (all Trial
  Use); `constraint.xpath` **removed**; string→markdown on `binding.description`, `constraint.requirements`,
  `mapping.comment`; fixed/pattern/default/example value types 50→54 (−Contributor, +integer64,
  CodeableReference, RatioRange, Availability, ExtendedContactDetail, Meta).
- **Mapping replace-by-identity is new in R5** — R4 mappings were additive-only. A dual-version generator
  forks behavior here.
- The §5.1.0.8 descriptive-elements table is new in R5 (R4 had only the ‡/∆ footnotes).
- The interpretation table itself is cell-for-cell identical R4→R5 (both contain the same defects).
- New R5: min=0+fixed/pattern presence rule; eld-23..28.

## R6-build note (v6.0.0-ballot4, fetched 2026-08-21)

The interpretation table is **reworked** in R6: headers renamed ("first" → "root element"), a softened
"expected / not expected" category added (the `base` erratum is fixed this way), the
requirements/comments/alias row split into three (comment and alias become optional everywhere), slicing
becomes optional in specialization non-root elements, mustSupport/isModifier/binding cells loosened for
some contexts. New invariants **eld-29..34** (distinct profiles/targetProfiles, additional-binding key
hygiene, `descriptive` binding strength ⇒ no valueSet, >1 bindable type ⇒ no binding). `binding.strength`
gains `descriptive`; `binding.additional` gains `key` (the constrain-by-key mechanism, see RFC-010);
`valueAlternatives` extends beyond primitives (explicit no-propagation-to-children); descriptive-properties
table gains Remove for label/requirements and names `elementdefinition-suppress` as the removal mechanism —
directly relevant to [OQ-008](14-open-questions.md#oq-008--verbosity-of-generated-snapshots).

## .NET behavior (Phase 2)
*(pending — `ElementDefnMerger.cs`; known highlights: diff-wins default; `"..."` append for
definition/comment/requirements; cumulative code/alias/condition/mapping/example/constraint; min = max(bases),
max = most-constrained; type list replace-then-item-merge (#827); Base/contentReference never merged;
binding stripped when no bindable type remains)*

## Java behavior (Phase 3)
*(pending — `ProfileUtilities.updateFromDefinition()` and friends)*

## Deviations
- [DEV-001](13-deviation-register.md#dev-001--type-list-replace-vs-merge-ch5) type list semantics.
- [DEV-002](13-deviation-register.md#dev-002--constraintsource-population-ch5) constraint.source.

## Open questions
- [OQ-001](14-open-questions.md#oq-001--the-cardinality-diamond-problem) cardinality diamond problem.
- [OQ-008](14-open-questions.md#oq-008--verbosity-of-generated-snapshots) suppression vs verbose snapshots.
- [OQ-010](14-open-questions.md#oq-010--the--append-convention) the `"..."` append convention.
