# 8. contentReference handling

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 6,
> 2026-08-26: `expandElement` contentReference branch deep-read). Java section pending (Phase 3).

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

## Java behavior (Phase 3)
*(pending)*

## Deviations
- [DEV-009](13-deviation-register.md#dev-009--contentreference-expansion-details-ch8).
- [DEV-023](13-deviation-register.md#dev-023--contentreference-in-a-constraint-profiles-snapshot-form-and-survival-ch8) —
  Phase-4 sweep: Java absolutizes *all* contentReferences in constraint profiles (.NET only inside merged
  subtrees), and Java keeps the reference on child-expanded elements where .NET drops it and restores `type`.

## Open questions
- [OQ-004](14-open-questions.md#oq-004--contentreference--constraining-children).
