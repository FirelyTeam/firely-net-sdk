# 10. Element ids & the Base component

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

## Scope
Two derived bookkeeping structures the generator must produce: element ids and `ElementDefinition.base`.

## Spec baseline (R5)

### Element ids — the one fully-specified algorithm in this domain

[elementdefinition #id], verbatim:

> "In addition to the path, every ElementDefinition SHALL have a populated id, and the id SHALL have a
> unique value populated by following this algorithm:
> - The id will be constructed as a dot separated string, each part corresponding to a token in the path
> - For each token in the path, use the syntax pathpart:slicename/reslicename
> - For type choice elements, the id reflects the type slice. e.g. For path = Patient.deceased[x], the id
>   of the boolean slice type element is Patient.deceased[x]:deceasedBoolean
>
> … id values constructed in this fashion are unique, and persistent, and may be used as the target of
> external references into the definition, where necessary."

Supporting invariants: ids exist on every element in both views (sdf-14) and are unique per view
(sdf-16/17). The root element's id is "just the type name" [structuredefinition §5.4.6.1]. Stable ids are
load-bearing elsewhere: the profile-element extension addresses elements *by id* [profiling §5.1.0.16].

Notes: reslices occupy a single token (`pathpart:slicename/reslicename`); type slices keep the `[x]` path
with the rendered type name after `:`. Because the algorithm is deterministic from path + sliceNames, a
generator can recompute ids wholesale — but whether it *must* (vs preserving author-supplied deviating
ids) is unstated ([OQ-009](14-open-questions.md#oq-009--element-id-stability)).

### The Base component

[elementdefinition-definitions, base]:

> "Information about the base definition of the element, provided to make it unnecessary for tools to
> trace the deviation of the element through the derived and related profiles. When the element definition
> is not the original definition of an element … then the information in provided in the element
> definition may be different to the base definition. On the original definition of the element, it will
> be same."

> "For tooling simplicity, the base information must always be populated in element definitions in snap
> shots, even if it is the same." [Comments]

> "The Path that identifies the base element - this matches the ElementDefinition.path for that element.
> Across FHIR, there is only one base definition of any element" [base.path]

sdf-8b: "All snapshot elements must have a base definition." So `base.path`/`min`/`max` record the
**original** definition's identity and cardinality; the generator populates base on every snapshot element
— propagating from the base snapshot, or seeding from the element itself where the element is first
defined.

**Interpretation-table erratum**: the `base` row reads "required" for both constraint-definition columns —
read literally, differentials must populate base, contradicting universal practice; the definitions page
resolves the intent (snapshot obligation, generator fills it). See
[RFC-003](15-spec-rfcs.md#rfc-003--elementdefinition-interpretation-table-base-row-wrong-for-differentials).

### Spec gaps (ids & base)

1. Id regeneration vs preservation across generation runs — unstated ([OQ-009](14-open-questions.md)).
2. `base` through *multi-level* derivation: "original definition" is clear for core-defined elements, but
   what base points to for elements first introduced by an intermediate profile (e.g. a slice added by a
   parent profile, then constrained) is unstated.
3. `base.min/max` for slices: whether they carry the sliced element's original cardinality or the slice
   entry's — unstated (the .NET test group `TestSliceBase_*` probes exactly this).

## R4/R4B deltas

- The id algorithm text is identical **except the choice-type example** (see ch2/ch4 headline): R4's
  example uses `path = Patient.deceasedBoolean` (renamed path), R5's uses `path = Patient.deceased[x]`
  with the type-slice id. The grammar itself did not change.
- `base` definition: identical (typo fix only); sdf-8b unchanged.

## .NET behavior (Phase 2)
*(pending — `ElementIdGenerator.cs` (force-regeneration), `SnapshotBaseComponentGenerator.cs`,
`TestSliceBase_*` test group)*

## Java behavior (Phase 3)
*(pending — `setIds`, base-component logic in `ProfileUtilities`)*

## Open questions
- [OQ-009](14-open-questions.md#oq-009--element-id-stability) id stability.
