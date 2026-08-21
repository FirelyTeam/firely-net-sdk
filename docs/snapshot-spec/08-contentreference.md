# 8. contentReference handling

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

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

## .NET behavior (Phase 2)
*(pending — `expandElement` contentReference branch :643, `ensureAbsoluteContentReferences`,
issue #3177 handling :664-683)*

## Java behavior (Phase 3)
*(pending)*

## Deviations
- [DEV-009](13-deviation-register.md#dev-009--contentreference-expansion-details-ch8).

## Open questions
- [OQ-004](14-open-questions.md#oq-004--contentreference--constraining-children).
