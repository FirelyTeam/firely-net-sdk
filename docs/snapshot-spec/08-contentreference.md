# 8. contentReference handling

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 6,
> 2026-08-26: `expandElement` contentReference branch deep-read; Phase 3 packet J-e, 2026-09-01: Java sweep).

## Scope
Elements defined by reference to another element (`Questionnaire.item.item` being the canonical case):
what a snapshot contains for them when unconstrained, what happens when a differential constrains their
*children*, local vs absolute references, and which properties of the referenced element carry over.

## Spec baseline (R5)

From the detailed definitions [elementdefinition-definitions, contentReference]:

> "Identifies an element defined elsewhere in the definition whose content rules should be applied to the
> current element. ContentReferences bring across all the rules that are in the ElementDefinition for the
> element, including definitions, cardinality constraints, bindings, invariants etc."

> "ContentReferences can only be defined in specializations, not constrained types, and they cannot be
> changed and always reference the non-constrained definition."

eld-5: an element with contentReference cannot have `type`, `defaultValue`, `fixed`, `pattern`, `example`,
`minValue`, `maxValue`, `maxLength`, or `binding`.

Recursion semantics [profiling §5.1.0.10, **new section in R5**]:

> "Some backbone elements recurse. E.g. Questionnaire.item. When a profile defines constraints on such
> elements, the constraints apply to the recursive references to those elements as well. I.e. If
> Questionnaire.item is constrained to have a type of 'group', that will cause Questionnaire.item.item,
> Questionnaire.item.item.item, etc. to all have the same constraint."

Level-specific constraints must instead be FHIRPath invariants limiting themselves "to only the root or
other specific levels of nesting".

### What this pins down for a generator

- In an *unconstrained* snapshot, a content-referenced element keeps its `contentReference` and carries no
  expanded children (the reference *is* the definition of its content).
- "cannot be changed and always reference the non-constrained definition" — a profile's differential never
  rewrites the reference target.
- "bring across all the rules" + §5.1.0.10 propagation: constraints on the referenced element apply at
  every recursion level.

### Spec gaps (contentReference)

1. **Expansion mechanics are unstated.** When a profile constrains *children* of a content-referenced
   element, nothing describes dereferencing + inlining the target's children into the snapshot (both
   implementations do some form of this), nor whether the `contentReference` survives on the expanded
   element.
2. Which value-domain properties of the referenced element meaningfully carry onto the referencing element
   after expansion — eld-5 bars them *on the reference*, but post-expansion is unaddressed
   ([OQ-004](14-open-questions.md#oq-004--contentreference--constraining-children), issue #3177).
3. Local (`#name`) vs absolute (`url#name`) reference forms: the definitions imply same-structure
   references; cross-structure references (needed once a profile's snapshot embeds another structure's
   elements) are tooling territory.
4. Interaction with §5.1.0.10: if constraints propagate to all recursion levels implicitly, must the
   snapshot *materialize* that propagation or leave it to validators? Unstated.

## R4/R4B deltas

- contentReference element definition and eld-5: **identical** R4→R5.
- profiling §5.1.0.10 "Recursive Elements" (constraints propagate to recursive references) is **new in
  R5** — R4 said nothing; a dual-version generator must decide whether to apply R5 semantics retroactively.

## R6-build note (v6.0.0-ballot4, fetched 2026-08-21)

**The recursion semantics flip again in R6**: constraints on a recursive element apply to the **literal
path only**, with a new `contentReferenceProfile` extension to opt in to propagation — the exact opposite
of R5's automatic propagation. This is the highest-impact R6 delta found for a generator: three versions,
three behaviors (R4 silent, R5 propagate, R6 opt-in). Also new in R6: **sdf-30** promotes
"contentReference only in specializations" from prose to invariant. Track in the WGM question brief —
whichever semantics the new generator implements must be version-aware.

## .NET behavior (Phase 2, deep-read 2026-08-26)

A content-referenced element is dereferenced **only when the differential constrains its children** (the
ch11 expansion policy) — otherwise the element passes into the snapshot untouched, with its
`contentReference` intact. The expansion branch (`expandElement`, `SnapshotGenerator.cs:643-690`):

1. **Resolve the containing structure**: `getCoreType` (`:2279-2289`) uses the current
   StructureDefinition's `type` when set (the normal case for a constraint profile — e.g. "Questionnaire"),
   else parses the reference itself (local `#Questionnaire.item` → root name; absolute `url#name` → the
   url); the result goes through `FindStructureDefinitionForCoreTypeAsync` (`:2258-2277`). For the normal
   local case this resolves the **core resource** — *not* the profile being generated. The absolute-url
   path is untraced (Phase-4 item).
2. **Jump by element id**: `JumpToNameReference("#" + name)` in the target's snapshot; failure → issue
   10002 and the element's children are dropped (`:653-659`).
3. **Inline the target's children**: `copyChildren` copies the target's subtree (bounded to the subtree,
   `:1564-1573`), scrubbing annotations/non-inheritable extensions and inheriting the target's Base
   components (#1123, `:1577-1583`); then ids are regenerated and `PrepareElement` fires per child.
4. **Drop the reference, restore the type** (#3177, `:664-683`): the expanded element's
   `contentReference` is set to `null` and its `type` deep-copied from the target — so a snapshot never
   carries both children and a contentReference. This answers spec gap 1's "does the reference survive"
   for .NET: **no, expansion replaces it.** The value-domain properties eld-5 forbids on the reference are
   deliberately **not** copied from the target — the in-code deliberation is preserved verbatim
   (`:670-683`, "a contentReference always points to a BackboneElement, and none of these properties make
   real sense" — [OQ-004](14-open-questions.md#oq-004--contentreference--constraining-children)).

**R5 §5.1.0.10 propagation is not implemented.** Because step 1 dereferences into the *core* structure,
constraints the same profile places on the referenced element (e.g. `Questionnaire.item`) do **not** flow
into the expansion of the referencing element (`Questionnaire.item.item`) — each level must be constrained
explicitly, and unconstrained deeper levels simply keep the (now-absolute) reference. .NET's behavior is
closest to the R6 literal-path semantics; it never matched R5's automatic-propagation text. Prime
harness/WGM material given the three-versions-three-behaviors situation (see R6-build note above).

**Absolute-reference rewriting** (MS 20220425, `ensureAbsoluteContentReferences`, `:1113-1152`): in
constraint profiles, all local (`#`-prefixed) contentReferences among the merged children are rewritten to
absolute `url#path` form against the profile's base *type* canonical — required once a profile's snapshot
inlines elements whose references point at the core structure rather than the profile itself.
Specializations keep local references.

## Java behavior (Phase 3 sweep, 2026-09-01)

Citations `PU`/`PPP` @ `b06c7ee`; detail in the materials extract `java-ch08-12-sweep-2026-09-01.md`.

Java has **three** behaviors for a referencing element, chosen by which walk path meets it:

| Walk path | Reference | `type` | Children |
|---|---|---|---|
| step-in when the diff constrains children and the base has none (one-match `PPP:858-896`, empty-match `PPP:1118-1156`) → `replaceFromContentReference` (`PU:1870-1874`) | **nulled** | **replaced by the target's types** | the target's subtree becomes the base for the diff rows (redirector stack) — same as .NET step 4 |
| the **last-emitted element** of a sliced-base path — the last new slice, or the entry when there are none (`PPP:1477-1479`; `outcome` is reassigned per new slice at `PPP:1385`) | **kept** | **cleared** | a typed new slice has already stepped in via `PPP:1422-1469` before the clear, so it *has* children; sibling slices emitted earlier keep their types |
| the diff slices a contentReference element, the entry has **no** inner diff rows, and the base has no children (`PPP:402-419`, the `else` of the `hasInnerDiffMatches` test) | kept on the entry | (empty) | the target's children are **copied inline** under the entry from the base snapshot (`resolveContentReference` walks backwards to the nearest same-path non-slice element; paths rewritten by string replace; `updateFromBase` per row) — fires precisely **because** there are no diff rows |

So DEV-023's flavor 2 (Java keeps the reference on child-expanded *slices*) and DEV-009's .NET-like
null-and-retype are both Java — on different paths. The third row is the mechanism behind DEV-025 flavor 1
(comp-deep/t21: Java materializes the sliced entry's children where .NET expands only the named slices).

- **Target resolution** (`getElementById`, `PU:3524-3541`): `#id` → the element with that **id** in the base
  snapshot being walked (a *profile* when the base is a profile); `url#id` with a foreign url → `findProfile`
  + that SD's snapshot; not found → throw `Unable to resolve reference to {0}`. Because `setIds` absolutizes
  every local reference at the end of each generation (next bullet), profile-on-profile chains carry absolute
  core urls and Java — like .NET's `getCoreType` — dereferences into the **core** definition. Only a base whose
  references are still local resolves within the base itself. **R5 §5.1.0.10 propagation is implemented by
  neither engine**: both expand the referencing element from the core target and merge only the diff rows
  placed under it.
- **Absolute form** (DEV-023 flavor 1): `generateIdForElement` (`PU:4359-4363`) prefixes every `#`-local
  contentReference with `http://hl7.org/fhir/StructureDefinition/<type>` (the SD's own url for logical models,
  `PU:4367-4373`) — over the **whole snapshot and the caller's differential** (`setIds(derived, false)`,
  `PU:886`), not just merged children. The core base url is hard-coded: a constraint profile on a non-core
  specialization gets a wrong target (needs-verification, JI-20).
- **eld-5 properties**: `replaceFromContentReference` copies only `type`; fixed/pattern/binding/defaultValue/
  example/minValue/maxValue/maxLength never come across — agreement with .NET by omission (OQ-004).
- The `contentReference` helpers outside the walk (`getChildMap` `PU:513-560`; sort's `find()` with its
  `MAX_RECURSION_LIMIT = 10`, `PU:3786`) serve renderers/validators/sorting, not generation.

## Deviations
- [DEV-009](13-deviation-register.md#dev-009--contentreference-expansion-details-ch8).
- [DEV-023](13-deviation-register.md#dev-023--contentreference-in-a-constraint-profiles-snapshot-form-and-survival-ch8) —
  Phase-4 sweep: Java absolutizes *all* contentReferences in constraint profiles (.NET only inside merged
  subtrees), and Java keeps the reference on child-expanded elements where .NET drops it and restores `type`.

## Open questions
- [OQ-004](14-open-questions.md#oq-004--contentreference--constraining-children).
