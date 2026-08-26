# 10. Element ids & the Base component

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 6,
> 2026-08-26: `ElementIdGenerator`/`SnapshotBaseComponentGenerator` deep-read). Java section pending (Phase 3).

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

## .NET behavior (Phase 2, deep-read 2026-08-26)

### Element ids (`ElementIdGenerator.cs`, `Hl7.Fhir.Shims.Base`)

The generator implements the spec algorithm directly: an id is dot-joined segments, one per path token,
each `elementName[:sliceName]` (`ElementIdSegment.ToString`, `ElementIdGenerator.cs:107-112`); reslice
names occupy a single segment (`/` is just part of the sliceName). One subtlety makes the two derived
structures interdependent: for a segment, the element name is taken from **`Base.path`** whenever that is a
choice (`[x]`) path (`ElementIdSegment(ElementDefinition)`, `:87-95`) — this is what produces
`value[x]:valueString` ids even for a renamed element, and it means **correct ids require Base components
to exist first**. `Update(nav, force, onlyChildren)` regenerates recursively; with `force = false` existing
ids are kept, but the *parent* prefix is always recomputed canonically (`:199-207` — "cannot rely on
nav.Current.ElementId as it may represent a custom id value"), so children of a custom-id parent still get
fully canonical ids (`Patient.identifier:ssn.system`, not `PatientSsnId.system`) — exactly the behavior the
file-header TODO (`:22-32`) asks for.

**Pipeline policy: ids are always regenerated, never inherited or preserved** (default settings):

- Base-profile ids are never inherited: after cloning the base snapshot, `Update(force: true)` runs before
  merging (`SnapshotGenerator.cs:505-514`). The in-code rationale for regenerating *immediately* (rather
  than clearing and regenerating at the end — the disabled `Clear` at `:430-438`) is that id-based back
  references must resolve *during* generation: expanding `Questionnaire.item.item` jumps to the id of the
  already-processed `Questionnaire.item` (see ch8).
- After every merge step the ids are force-regenerated again (`:1066`, `:956`, `:1600`, `:1633`, `:1864`).
- A custom element id in the differential **is merged and then immediately overwritten**: the merge at
  `:1052` passes `mergeElementId: true` (ch5 `mergeId`), but the force-regeneration at `:1066` wipes the
  result. This is deliberate — the comment (`:1063-1065`) reads "R4: Always re-generate Element Ids
  according to standardized format … Ignore user-specified element id's in the differential", and the
  commented-out alternative at `:1061` (preserve diff-specified ids) was abandoned. Author-supplied ids
  survive only with `GenerateElementIds = false`, which also disables all generation. That answers
  [OQ-009](14-open-questions.md#oq-009--element-id-stability)'s .NET side: canonical regeneration, always.

### Base components (`SnapshotBaseComponentGenerator.cs`)

`ElementDefinition.Base` is produced by `EnsureBaseComponent(elem, baseElem, force)` (`:136-181`):

- **Root elements**: Base always references self (`path`/`min`/`max` copied from the element itself,
  `:141-149`) — the R4+ rule.
- **Other elements**: if the matched base element has a Base, it is **inherited** (deep-copied); otherwise
  a new Base is seeded from the base element's own path/min/max (`:159-176`). Base therefore propagates
  transitively toward the original definition. For elements the current profile *introduces*, the Base is
  self-created at creation time: `createNewElement` seeds it from the diff's own min/max (ch7), and a new
  named slice **deep-copies the sliced element's Base** along with the rest of the slice-base clone (ch4
  `initSliceBase` clones the subtree; only the element's *own* `min` is reset to 0, the Base component is
  untouched). This answers spec gap 2 mechanically: an element first introduced by an intermediate profile
  gets a Base pointing at that intermediate's *element path and diff cardinality*, which then propagates
  unchanged into deeper derivations.
- **Slices (spec gap 3), code-derived**: by the clone mechanism above, a named slice's Base carries the
  *sliced element's original* path and cardinality (e.g. `Patient.identifier` slice `bsn`: `Base.min = 0`,
  `Base.max = *`) — not the slice's own constrained values and not `min = 0` by decree. Note the comment at
  `SnapshotGenerator.cs:2004-2007` ("Named slices should get base with Min = 0") *appears* to contradict
  this; we read it as describing the `OnPrepareElement` event's base-element argument (the slice-base clone,
  whose element-`min` *is* 0), not `ElementDefinition.Base`. The test helper `assertBaseDefs` only checks
  `Base.path` compatibility, never `Base.min/max` (`SnapshotGeneratorTest.cs:2548`), and the
  `TestSliceBase_*` group asserts event annotations, explicitly "[disregarding] ElementDefinition.Base"
  (`:4380-4382`) — so the actual output value is unpinned by tests. **Verify in the Phase-4 harness** (also
  against Java).
- **Regeneration gate**: a Base created by the generator is marked with an in-memory
  `CreatedBySnapshotGenerator` annotation; `EnsureBaseComponent` regenerates only when forced, absent, or
  *not* generator-created (`:150`). Consequence: an author-supplied Base component in input **survives**
  (it is never generator-created), while generator output is idempotently refreshable. The file-header
  comment "Behavior is controlled by `_settings.NormalizeElementBase`" (`:22`) is stale — no such setting
  exists (the 6 real settings are in ch12).

`ensureBaseComponents` (`:34-125`) walks the generated snapshot against the base snapshot in parallel,
matching by last path segment or renamed-choice correspondence; on a mismatch it **drills down** to the
base's own base profile (resolving + ensuring snapshots as needed, `:107-120`) so elements inherited from
higher up the chain still find their original. An element that matches nowhere up the chain silently gets
**no Base component** (fall-through at `:123-124`) — no issue is emitted, leaving the sdf-8b obligation
unmet for that element. Children inlined from type profiles or contentReference targets inherit the type
profile's Base components via `copyChildren` → `EnsureBaseComponent(typeElem, false)`
(`SnapshotGenerator.cs:1581-1583`, #1123). Sequencing in `generate()`: ids first (`:513`), then Base
components (`:518`) — on the *base* clone, before merge; external profiles get theirs in `ensureSnapshot`
(`:2348`).

## Java behavior (Phase 3)
*(pending — `setIds`, base-component logic in `ProfileUtilities`)*

## Open questions
- [OQ-009](14-open-questions.md#oq-009--element-id-stability) id stability (.NET side answered 2026-08-26 —
  always regenerated canonically; author ids discarded by design).
- Slice `Base.min/max` output value — code-derived only, unpinned by tests; Phase-4 harness item (see above).
