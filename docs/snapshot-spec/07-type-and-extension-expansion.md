# 7. Type-profile and extension expansion

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 4,
> 2026-08-24: `mergeTypeProfiles`/`expandElement` deep-read). Java section pending (Phase 3).

## Scope
Expanding an element's children from its type: when expansion happens, choosing which type to expand for
multi-type elements, merging an *external type profile*'s snapshot under the element (and the resulting
three-way merge order: type profile → base → differential), extension definitions as the prominent special
case, and compatibility checks between a declared type profile and the base type.

## Spec baseline (R5)

### type / profile / targetProfile semantics

- `type.code` 1..1 uri — relative to `http://hl7.org/fhir/StructureDefinition`; absolute URLs only in
  logical models. FHIRPath System types (`http://hl7.org/fhirpath/System.String`) appear on primitives'
  `.value` elements, usually with the `structuredefinition-fhir-type` extension [elementdefinition
  #interpretation].
- `type.profile` 0..* — "If any profiles are specified, then the content must conform to at least one of
  them" (a **disjunction**). Same for `targetProfile` (Reference/canonical/CodeableReference only, eld-17).
- A profile may "require that a typed element or a reference target conform to another profile"
  [profiling §5.1.0.9].
- Aggregation/versioning rules [elementdefinition #aggregation]; eld-4.

### The one stated cross-SD effect: root cardinality

> "if the root element cardinality is 0..1, then an element declared to be of that type cannot have a
> maximum cardinality greater than 1" [structuredefinition §5.4.6.1]

(and the § footnote: profiles referencing a type "must fall within the cardinality bounds of the type
itself" — most common for extensions, e.g. max=1 or min=1 extension definitions
[elementdefinition #interpretation]). Also: "the minimum cardinality of an extension SHALL be a valid
restriction on the minimum cardinality in the definition of the extension" [profiling §5.1.0.18.1].

### Starting expansion below the root: profile-element

[profiling §5.1.0.16] The `elementdefinition-profile-element` extension on `type.profile` names an element
**id** in the target SD: "an instruction to a validator to apply the profile starting at the nominated
element (by its ID)". Consequence for a generator: rebase the referenced SD's subtree at that id instead
of its root.

### Extensions

Structure [extensibility §2.1.5.0.1]: `Extension : Element` with `url : uri 1..1` and `value[x] 0..1` (54
types in R5); "Simple extensions have only a value and no nested extensions. Complex extensions contain
one or more nested extensions and no value" (ext-1). Url rules:

> "The url SHALL be … a reference to the canonical URL of a StructureDefinition that defines the
> extension. Except for child extensions defined within complex extensions, the URL SHALL be an absolute
> URL. … In the case where an extension defines complex content, the identity of the parts of the
> extension are local/relative to the reference to the extension definition."

That is **instance** syntax; nothing on these pages requires a snapshot to carry `fixedUri` on
`Extension.url` elements or says what the fixed value is for nested parts — pure tooling convention
(spec gap, and .NET's `fixExtensionUrl` exists to implement it).

Profiling extensions = slicing the `extension` array by `url` (ch6). An extension definition "defines the
extension element using the same details used to profile the structural elements" [profiling §5.1.0.18] —
i.e. the extension SD is the effective base for the extension slice's content.

Modifier rules [extensibility §2.1.5.0.2]: whether an extension is a modifier "is based on the isModifier
flag on the root element in the extension definition (the element definition with path 'Extension' **in
the generated snapshot**)" — one of the few spec passages that *presumes* snapshot generation. "A regular
extension cannot be 'constrained' to be a modifier." Extension constraints require `context` (sdf-5);
`contextInvariant` only on extensions (sdf-18); context semantics live on defining-extensions.html.

### Merge order — unstated

When an element has a type profile AND the base element carries constraints AND the differential
constrains children, nothing in the spec orders the three sources or resolves conflicts between them (the
"diamond"). See [OQ-002](14-open-questions.md#oq-002--priority-type-profile-constraints-vs-base-constraints)
and [OQ-001](14-open-questions.md#oq-001--the-cardinality-diamond-problem).

### Spec gaps (expansion)

1. Whether/when a generator expands children from the type's SD vs `type.profile` — unstated (beyond
   profile-element).
2. How the Extension type's snapshot merges under a profiled `extension` element — unstated.
3. `extension.url` fixed-value convention in snapshots — unstated (fixedUri vs pattern; nested relative
   values).
4. Which type to expand when multiple type codes remain — unstated.
5. Compatibility checking between a differential's `type.profile` and the base type — unstated.

## R4/R4B deltas

- **Headline:** the root-cardinality rule is new in R5 (R4 called type-root min/max "optional
  (irrelevant)") — an R4 generator ignoring extension-root cardinality was compliant; an R5 one is not.
- FHIRPath System types + `structuredefinition-fhir-type` documentation are new in R5's pages (the
  encoding itself existed in R4 artifacts).
- profile-element extension: supported in both; R5 added normative-page documentation [profiling §5.1.0.16].
- Extension value[x]: 50→54 types (−Contributor); `Extension.url` type string→uri.

## R6-build note (v6.0.0-ballot4, fetched 2026-08-21)

Extension `value[x]` grows 54→55 types (+VirtualServiceDetail); `Extension.url` and `value[x]` become
summary (Σ) elements. defining-extensions.html gains context-matching notes (specialization matching logic;
the element/"Element" context convention) — the context semantics remain on that page, still pending
extraction (see ch9 for the interfaces-as-contexts addition).

## .NET behavior (Phase 2, deep-read 2026-08-24)

All citations `SnapshotGenerator.cs` unless noted. Type-profile merging happens inside `mergeElement`, per
element, in a fixed order (`:997-1077`): first `mergeTypeProfiles` (skipped for the root element, `:1045`),
then the differential element itself (`ElementDefnMerger`, ch5). Combined with the full-expansion mechanics
below, the effective priority is **differential > external type profile > base** — a diamond choice the code
records as unresolved ("Ewout: not defined yet, under discussion; use cases exist for both options",
`:999-1003`; expanded rationale + GForge #9791 reference at `:1183-1203`) —
[OQ-002](14-open-questions.md#oq-002--priority-type-profile-constraints-vs-base-constraints).

### When an external type profile is merged (`mergeTypeProfiles`, `:1208-1503`)

Gates, in order — every early exit means "no external merge, but continue merging diff children":

1. Exactly **one distinct type code** among the diff's types (`DistinctTypeCodes` ignores null codes). Zero
   (no type constraint) or several (unsliced choice) → no type merge (`:1227-1237`).
2. At most **one distinct profile** across *all* diff type entries (R4+ profile lists are flattened,
   `:1242`). Two or more distinct profiles — the spec's disjunction — → the external merge is **silently
   skipped**, no issue; expansion later falls back to the common core type (`:1243-1249`).
3. The diff profile must **differ from the base's implied profile**: an explicit restatement of e.g. the
   core Identifier canonical is ignored (`:1264-1274`). The comparison target is the snap primary type's
   `GetTypeProfile()` = its profile if single, else the type-code canonical
   (`Model/ElementDefinitionExtensions.cs:263-275`); a snap type carrying *multiple* profiles yields null
   here, so a diff restating just one of them proceeds to a full external merge.

The profile value is parsed as `url[#element]` (`ProfileReference`,
`Navigation/ProfileReference.cs:19-59`); a fragment marks a **complex reference** to a named element inside
the target — the extension-child convention (`:1276-1288`). Then:

- **Unresolvable** external profile → `UNAVAILABLE_REFERENCED_PROFILE` issue, continue without merge
  (`:1306-1312`).
- **Compatibility check**: the resolved SD must be equal to or derived from **any of the snap (base)
  element's type codes**, established by walking the target's `baseDefinition` chain through the resolver
  (`isValidTypeProfile`, `:2540-2565`, check at `:1316`). Incompatible →
  `PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE` (10009) issue, continue without merge (`:1317-1322`). The walk
  is null-tolerant in both directions (null type code or null SD → compatible, `:2548-2549`) and **throws**
  `InvalidOperationException` on a cyclic base chain (`:2559-2564`).
- With `GenerateSnapshotForExternalProfiles` set, the external profile's snapshot is eagerly ensured on
  resolution (`:1292-1293`, ch12).

Even when nothing was merged (unresolved/incompatible), the element still gets its ids regenerated and
`OnPrepareElement` raised (`:1491-1499`, with a null type profile in the unresolved case).

### Full expansion — the diff constrains children

"Must expand" = the diff element has children, overridable by the `OnBeforeExpandElement` callback
(`mustExpandElement`, `:1163-1169`). Steps (`:1327-1416`):

- `ensureSnapshot(typeStructure)` (ch3) — failure **aborts the element's entire child merge** (`return
  false` at `:1329-1332` makes `mergeElement` skip the child recursion, `:1079-1082`; the only issues come
  from `ensureSnapshot` itself).
- The **full external snapshot** is deep-copied and `Rebase`d to the diff path (complex reference: to the
  parent path) (`:1334-1342`).
- The external root's sliceName is never copied (`FIX_SLICENAMES_ON_ROOT_ELEMENTS`, `:1352-1359`; the STU3
  SimpleQuantity bug).
- If the diff renamed a choice element while the snap still has `[x]`, the snap element is renamed *first*
  (`:1371-1381`).
- `copyChildren(snap, typeNav)` fills the children if the snap element has none: copied elements are
  scrubbed of non-inheritable extensions and generator annotations and inherit the type profile's `base`
  components (`:1534-1588`).
- The rebased type snapshot is then merged onto the element **as if it were a differential**
  (`mergeElement(snap, typeNav)`, `:1400-1413`). This recursion *is* the priority mechanism: a snapshot has
  a value for nearly every property, so wherever it differs it overrides the base's values (ch5's
  constraint-wins rule). The in-code ISSUE at `:1405-1411` acknowledges this is wrong when the base profile
  itself had already constrained the type's children: the external type snapshot re-applies original type
  values over the base's overrides ("{Address Snap + Diff + **Address Snap (WRONG!)** + MyAddress Diff}") —
  folded into [OQ-002](14-open-questions.md#oq-002--priority-type-profile-constraints-vs-base-constraints).
- **Complex reference**: after rebasing, the generator jumps to the named element
  (`typeNav.JumpToNameReference(profileRef.ElementName)`, `:1364`). **This call cannot succeed in the
  current code**: it passes the bare element name, which `ProfileReference.Parse` classifies as an
  (unknown) canonical url → `NotSupportedException` (`Navigation/ElementDefinitionNavigator.cs:281-303`);
  and even the `#name` form matches by full element **id**, which a short fragment name never equals. See
  [DEV-018](13-deviation-register.md#dev-018--complex-type-profile-references-urlelement-expansion-path-broken-ch7).
- Cleanup: `fixExtensionAnnotationsAfterMerge` removes constrained-by-diff markers on
  short/definition/comment that were merely inherited via `DomainResource.extension`'s defaults
  (`:1505-1527`); `prepareMergedTypeProfileElements` re-generates **all** element ids in the subtree (ids
  are never inherited from a rebased external profile) and raises `OnPrepareElement` per element
  (`:1595-1622`).

### Root-only merge — no diff children

- **Simple reference**: only the external profile's **root** element is merged: `getSnapshotRootElement`
  (ch3's four-step cascade), deep copy, path rebased, merged with `mergeElementId: false` (`:1464-1476`).
  A bare `type.profile` reference thus pulls the type profile's root constraints (cardinality, invariants,
  binding, …) into the referencing element even without any diff children — the documented rationale is
  extensions inheriting cardinality from their definition's root (`:1183-1185`), and this is the exact spot
  where [OQ-001](14-open-questions.md#oq-001--the-cardinality-diamond-problem)'s diamond materializes.
- **Complex reference**: nothing is merged — the target was already merged when the (grand)parent extension
  element was expanded; the generator only verifies that the diff's sliceName equals the fragment name
  (`PROFILE_ELEMENTDEF_INVALID_COMPLEX_REFERENCE` otherwise) (`:1417-1486`).

### Expanding children from the type (`expandElement`, `:626-754`; `expandElementType`, `:756-795`)

Reached from `mergeElement` when the diff dives into an element that has no children in the working
snapshot (`:1083-1108`), and from the public `ExpandElement` API. Already-has-children → no-op (`:636-639`).
Otherwise, by element shape:

- `contentReference` → dereference and copy the referenced subtree (ch8).
- **No type, no contentReference**: logical models return success — the children are defined inline by the
  diff (ch9, `:691-709`); anything else → `PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF` issue and the
  diff subtree is dropped.
- **Several type entries**: expansion uses the **common** type code if all entries agree (the R4+
  multi-profile disjunction case), else falls back to `Element` — so only `id`/`extension` children are
  addressable under an unsliced choice; always the **core** type SD, never a custom profile (`:711-743`).
- **Single type**: `getStructureForTypeRef` prefers the type's custom profile when there is exactly **one**
  (zero or several → the core type's SD, `SafeSingleOrDefault` at `:2202`); FHIRPath `System.*` type codes
  resolve to nothing (R4.0.1 technical correction, `:2216-2218`), so primitive `.value` elements don't
  expand.
- `expandElementType` copies the resolved SD's children under the element (same scrubbing/base-inheritance
  as `copyChildren` above) and raises `OnPrepareElement` per copied element by *matching* snap children
  against type children with the ch4 matcher (`prepareExpandedTypeProfileElements` →
  `prepareExpandedElementsInternal`, `:1627-1740`). The "snap already had children" branch is acknowledged
  untested dead space (in-code `Debug.Fail("TODO...")` + "WRONG...?" comment, `:769-785`).

### New diff-only elements (`createNewElement`, `:887-964`)

Ch4's `New` action lands here: the element's type **root** (via `getBaseElementForElementType` →
`getSnapshotRootElement`, `:2359-2380`) becomes the initial snapshot element — with root extensions stripped
for primitive types (`:898-902`), a self-referential `base` component built from the **diff's own** min/max
(explicitly not the type root's, `:904-914`), and `constraint.source` back-filled with the type's canonical
(`:988-994`; DEV-002). If no type root is resolvable, the diff element is cloned as-is with a
self-referential `base`. The nearby `removeNewTypeConstraint` (`:966-986`) — which would strip the type's
own invariants from a new element — is **dead code: never called anywhere** (verified project-wide,
2026-08-24).

### `extension.url` fixing (`fixExtensionUrl`, `:1743-1785`)

Runs after every element's child merge (`:1119-1124`), for elements whose path name is (case-insensitively)
`extension` — the root `Extension` of an extension definition and any child `extension` element. If the
`url` child exists and has no `fixed[x]` yet:

- extension definition root (derivation = `constraint` only; the core Extension SD itself is excluded) →
  `fixedUri` = the SD's canonical url (`:1757-1766`);
- profile extension element → `fixedUri` = the primary type profile reference, else the slice name — so
  nested complex-extension children get the **relative** name, matching the instance-url convention of the
  spec baseline above (`:1767-1772`). (A complex `url#name` type reference would be copied verbatim,
  absolute-with-fragment; in practice the fixed value is normally already present, inherited from the
  merged extension definition, and the backfill doesn't run.)

The generated value is a `fixedUri`, never a `fixedString`
([Zulip-settled](https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Extension.2Eurl.20-.20fixedString.20or.20fixedUri.3F),
`:1776-1779`). **`modifierExtension` elements never match the path-name test**, so the backfill never runs
for them —
[DEV-019](13-deviation-register.md#dev-019--modifierextensionurl-never-gets-the-fixeduri-backfill-ch7).

### Not implemented: the profile-element extension

The generator never reads `elementdefinition-profile-element` — the R5-documented mechanism for starting
expansion below the target's root (spec baseline above). Its only below-root mechanism is the legacy
`url#name` fragment syntax, whose expansion path is broken (DEV-018). See
[OQ-017](14-open-questions.md#oq-017--starting-expansion-below-the-root-extension-vs-fragment-syntax).

Related, out of scope here: renamed choice elements without an explicit type list get the implied type
constraint applied during slicing (`applyImplicitChoiceTypeConstraint`, `:2022-2050`, ch6);
`CACHE_ROOT_ELEMDEF` reuses recursively pre-generated root elements when merging (`:1005-1037`, ch3).

## Java behavior (Phase 3)
*(pending)*

## Open questions
- [OQ-001](14-open-questions.md#oq-001--the-cardinality-diamond-problem) cardinality diamond (root-only merge).
- [OQ-002](14-open-questions.md#oq-002--priority-type-profile-constraints-vs-base-constraints) merge priority.
- [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors) error taxonomy rows
  from this chapter (silent skip / issue+continue / abort-children / throw).
- [OQ-017](14-open-questions.md#oq-017--starting-expansion-below-the-root-extension-vs-fragment-syntax)
  profile-element extension vs fragment syntax.
