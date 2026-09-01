# 11. Recursion & circularity

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 6,
> 2026-08-26: `SnapshotRecursionStack` + expansion-policy deep-read; Phase 3 packet J-e, 2026-09-01: Java sweep).

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

## .NET behavior (Phase 2, deep-read 2026-08-26)

.NET's answers to the four spec gaps, in order of the inventory above:

### 1. Expansion-depth policy: expand only where constrained

The generator walks the differential, not the type graph: an element's children are expanded **only when
the differential contains child constraints for it** (`mustExpandElement`, `SnapshotGenerator.cs:1163-1169`
— `mustExpand = diffNav.HasChildren`, then merge recurses, `:1083-1111`). Everything else keeps the base
snapshot's structure as-is. This is the universal convention the spec never states, and it is *also* the
main recursion terminator: `Element.extension : Extension` never triggers expansion of Extension unless a
profile actually constrains inside it. A client can override per element via the `BeforeExpandElement`
event (`SnapshotGeneratorEvents.cs:105-123`), whose own doc-comment carries the warning: "recursively
expanding all profile elements may cause infinite recursion!". The public `ExpandElementAsync` API
(`:205-282`) exposes the same machinery for on-demand, single-element expansion (lazy-snapshot scenarios).

### 2. Cycle detection: one guarded channel, one unguarded

All external-profile expansion goes through `ensureSnapshot` (`:2306-2356`), which brackets the recursive
`generate` call with a **profile-URI stack** (`SnapshotRecursionStack`, `Hl7.Fhir.Shims.Base`).
Re-entering a URI already on the stack throws `NotSupportedException` with the full URL stack in the
message (`OnBeforeExpandTypeProfile`, `SnapshotRecursionStack.cs:90-101`) — a hard failure, not an issue.
This one stack guards both base-chain and type-profile recursion (`generate` → `ensureSnapshot(base)`;
`expandTypeProfile` → `:2315`). Note the comment at `:2311-2313` promises a special case ("when recursing
on Element, simply return true and continue") that the code does **not** implement — legitimate re-entry is
instead handled by the root-only channel below.

The **unguarded** channel is `getSnapshotRootElement`'s step-4 cascade (ch3): resolving a root element
recurses up the `baseDefinition` chain (`:2477-2479`) with **no cycle detection** — the TODO at
`:2469-2473` says so explicitly ("an attacker could abuse this") and explains why the main stack can't be
reused: the url may *legitimately* be on it (root-only re-entry). A cyclic `baseDefinition` chain entering
through this path means unbounded recursion (stack overflow), in contrast to the type-compatibility walk
`isValidTypeProfile`, which carries its own `HashSet`-based guard and throws `InvalidOperation` on a cycle
(`:2545-2565`; the throw-on-base-cycle row in
[OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors)). Three behaviors for
the same underlying error — hard throw (main stack), unbounded recursion (root cascade), guarded throw
(compatibility walk).

### 3. On-demand base generation

`ensureSnapshot` generates missing snapshots of resolved base/type profiles recursively, gated by
`GenerateSnapshotForExternalProfiles` (default on) and re-generated under `ForceRegenerateSnapshots` at
most once per profile — completed snapshots are stamped with the in-memory `CreatedBySnapshotGenerator`
annotation (`:2319-2338`), so idempotence holds only while a caching resolver keeps returning the same
instances. Termination relies on the URI stack: a differential-only base chain is walked depth-first, each
level pushed, cycles throwing as above.

### 4. Legitimate self-re-entry: the root-only channel

The system's answer to `Extension : Element → Element.extension : Extension` is that re-entry never needs
the *full* snapshot — only the **root element** of the profile being re-entered, for merging as a child
element's base. `getSnapshotRootElement` therefore resolves, in order (ch3): a cached root-element
annotation on the differential root (`CACHE_ROOT_ELEMDEF`, `SnapshotGeneratorAnnotations.cs:172-296`); an
existing snapshot; the **partial snapshot of a profile currently on the stack**
(`RegisterSnapshotNavigator`/`ResolveSnapshotNavigator`, `SnapshotRecursionStack.cs:120-138` — every
`generate` registers its working navigator at `SnapshotGenerator.cs:549`, and by the time a recursive
resolution can occur its root element exists); else the recursive base-chain merge (the unguarded step 4).
The annotation is scrupulously removed before returning (`:574-582`) and self-defends against surviving a
`DeepCopy` (`GetSnapshotElementAnnotation` discards annotations whose recorded owner is not the element
itself, `SnapshotGeneratorAnnotations.cs:262-278`).

### Statefulness

The stack also makes the generator **non-reentrant**: `OnStartRecursion` throws if a previous operation
has not finished (`SnapshotRecursionStack.cs:43-50`), so one `SnapshotGenerator` instance can run only one
generation at a time (see ch12).

## Java behavior (Phase 3 sweep, 2026-09-01)

Citations `PU`/`PPP` @ `b06c7ee`; detail in the materials extract `java-ch08-12-sweep-2026-09-01.md`.
Java's answers to the four spec gaps:

1. **Expansion-depth policy: the same diff-driven rule as .NET** — children are stepped into only where the
   differential has child rows (`PPP:828-829`, `1077`, `1422-1424`, `1658`) — **plus one exception**: when the
   diff slices a contentReference element and the entry has **no** inner diff rows on a base without
   children, the target's children are materialized inline under the entry (`PPP:402-419`, ch8; DEV-025
   flavor 1) — the one place structure appears that no diff row asked for. No per-element override hook.
2. **Cycle detection: one stack, one flag.** `snapshotStack` (instance list of **derived** urls) is checked and
   pushed per `generateSnapshot` (`PU:774-778`, throw `Circular snapshot references detected … (stack = …)`),
   popped in `finally` (`PU:1089`); the SD object itself is flagged `generatingSnapshot` for the duration
   (`PU:777`, cleared `1083`/`1088`), which `checkNotGenerating` (`PU:1694-1698`, `FHIRException` "Attempt to use
   a snapshot on profile {0} as {1} before it is generated") and the template selection (`PPP:714/719/744`)
   consult. Five re-entry sites, all synchronous on the same instance: `PU:767` (base of a snapshot-less
   base), `PU:2080` and `PU:2638` (xver synthesis), `PPP:710` (xver template), `PPP:730` (snapshot-less type
   profile). Any `Exception` → the half-built snapshot is **nulled** (`PU:1078-1084`; a `java.lang.Error` is
   not caught there, so those paths do leave a partial snapshot behind — ch12). Unguarded: the `findProfile`
   base-chain walks in `isMatchingType`/`isCompatibleType`/`checkTypeParameters`/`checkTypeDerivation`
   (`PU:1665`, `1477`, `1175`, `3280`) loop forever on a cyclic `baseDefinition` chain (cf. .NET's unguarded
   root cascade).
3. **On-demand generation is unconditional** (no settings gate): snapshot-less bases and type profiles are
   generated recursively at the sites above; idempotence via the SD's `generatedSnapshot` flag/`hasSnapshot()`
   rather than an annotation.
4. **Legitimate self-re-entry: the first-element rule.** A type profile that is *currently being generated*
   is accepted without the type-compatibility check, and only its already-built **first element** may be
   used as the template (`PPP:714-724`, throw if empty) — Java's equivalent of .NET's root-only channel,
   without caching. This is exactly what lets `ext-recursion-2` (a slice typed with its own url) generate:
   the self-reference is met mid-generation, the root exists, and the slice has no diff children so no
   step-in follows. `logical-goo` passes because the caller hands `generateSnapshot` the base as an
   *object* with a snapshot — the stack holds only derived urls, so url == baseDefinition-url is never
   checked. `ext-recursion-1` (self-type on the root) is rejected structurally by `checkDifferentialBaseType`
   (`PU:1322`) before any recursion. Together these pin DEV-029's Java mechanisms.

**Statefulness**: `snapshotStack`, `messages`, `obligationProfiles`, `childMapCache`, `xver`, `defWebRoot`
are per-instance; the `generatingSnapshot` flag lives on the shared SD object, visible to every
`ProfileUtilities` sharing the context. No overlapping-call detection; not thread-safe.

## Deviations
- [DEV-029](13-deviation-register.md#dev-029--recursion-crossover-each-side-rejects-recursive-structures-the-other-accepts-ch11) —
  Phase-4 recursion crossover: .NET silently accepts the self-typed-root extension Java rejects
  (ext-recursion-1), and (under harness settings) hard-refuses the recursive structures Java generates
  and golden blesses (ext-recursion-2, logical-goo — the URI-keyed guard makes url==baseDefinition-url a
  hard failure). Default-settings .NET re-run pending.
- [DEV-025](13-deviation-register.md#dev-025--materialization-depth-of-unconstrained-content-java-normalizes-more-than-net-ch7ch8ch11)
  flavor 1 — Java re-expands sliced contentReference entries per recursion level the diff reaches
  (comp-deep); .NET expands only the named slices (→ OQ-021).
