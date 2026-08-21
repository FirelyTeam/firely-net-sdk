# 11. Recursion & circularity

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

## Scope
Recursion is inherent: generating a snapshot requires the base's snapshot, type-profile expansion requires
other profiles' snapshots, and some structures legitimately reference themselves (`Extension.extension` of
type Extension; `Identifier.assigner` → Reference(Organization) → Organization.identifier …). This chapter
covers legal vs illegal cycles, cycle detection, on-demand generation, expansion-depth strategy, and
caching.

## Spec baseline (R5)

**The spec is almost entirely silent here.** The complete inventory:

1. **contentReference recursion** [profiling §5.1.0.10, new in R5]: constraints on a recursive backbone
   element (Questionnaire.item) "apply to the recursive references … as well" (see ch8).
2. **Structural self-reference in datatypes** is a fact of the type system: `Extension.extension :
   Extension` (complex extensions [extensibility §2.1.5.0.1]); Reference-typed elements close cycles
   between resources. No page discusses what this means for a generator expanding children.
3. **Acyclicity is demanded only of logical models** — "a directed acyclic graph with typed nodes"
   [structuredefinition §5.4.6.5] — the sole acyclicity statement in the corpus, and it is about the
   model's node graph, not profile-reference cycles.
4. Arbitrary-depth derivation chains ("Re-profiling") come with a complexity warning only
   [profiling §5.1.0.17].

### What a generator must therefore invent (all spec gaps)

1. **Expansion-depth policy**: snapshots would be infinite if every element's children were expanded
   through recursive types. The universal convention — expand children only where constrained (plus one
   level of declared structure), leave deeper structure to the type definitions — appears nowhere in the
   spec. The only hint is "the tools generate complete verbose snapshots" [profiling §5.1.0.11], which cuts
   the *other* way.
2. **Cycle detection among profiles**: profile A's type profile references B which references A — legal?
   error? No spec text.
3. **On-demand base generation**: differential-only bases must have snapshots generated recursively;
   termination/ordering unstated.
4. **Legitimate self-re-entry**: generating Extension's own profile touches `Extension.extension : Extension`
   — distinguishable from an illegal cycle only by convention.

## R4/R4B deltas

- profiling §5.1.0.10 (contentReference recursion propagation) is **new in R5**; otherwise no relevant
  change — the silence is stable across versions.

## .NET behavior (Phase 2)
*(pending — `SnapshotRecursionStack.cs` (throws on cyclic profile dependency), local guard in
`isValidTypeProfile` :2545, `CACHE_ROOT_ELEMDEF` root-element cache for legitimate re-entry,
`BeforeExpandElement` event with its "may cause infinite recursion" warning)*

## Java behavior (Phase 3)
*(pending)*
