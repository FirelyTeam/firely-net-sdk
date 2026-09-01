# 7. Type-profile and extension expansion

> Status: **spec baseline + .NET + Java behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 4,
> 2026-08-24: `mergeTypeProfiles`/`expandElement` deep-read; Phase 3 packet J-d, 2026-09-01: ProfilePathProcessor
> template selection + step-in paths, `updateFromDefinition` profile-doc block, final type sweep, xver, obligations).

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

## Java behavior (Phase 3, deep-read 2026-09-01)

Citations: `PU` = `ProfileUtilities.java`, `PPP` = `ProfilePathProcessor.java` @ `b06c7ee` (re-checked against
master `4f52ba6`: PU is shifted by +1 line from `PU:222` onward, PPP unchanged). Details and the full
suspicious-spots table are in the materials extract `java-ch07-type-expansion-and-ch03-base-resolution-2026-09-01.md`.

Java has no counterpart of `mergeTypeProfiles`. A `type.profile` on a differential row acts in **three
separate places**, none of which merges a datatype profile's snapshot onto the element:

### 1. Template selection — only for Extension and Resource typed elements (`PPP:674-787`)

The one-match path builds the *template* the diff row is merged onto. When the diff row has exactly one type,
that type carries a profile, the code is not `Reference`, and the profile differs from the base's first
profile (`PPP:692-696`), the profile SD is resolved with a raw `context.fetchResource` (not `findProfile`, so
the version parameters of §ch3 do not apply here), falling back to a cross-version (xver) synthesis when the
url has the `http://hl7.org/fhir/<ver>/StructureDefinition/extension-<Type.path>` shape (`PPP:700-712`;
Bad/Invalid/Unknown status → `FHIRException`, Valid → synthesized SD + re-entrant `generateSnapshot`). Then:

- **Type compatibility** (`PPP:714-718`, `isMatchingType` `PU:1643-1668`): walk the profile's base chain; a
  match is a core-url SD whose `type` equals the diff type code, or an SD whose *url* equals the code (logical
  models), or — with the profile-element extension — the nominated element's own types intersecting the diff
  types. Mismatch → **throw** `Type mismatch processing profile {0} at path {1}: The element type is {4}, but
  the profile {3} is for a different type {2}`. Skipped while the profile is itself mid-generation.
- **Snapshot-less profile** → generated on demand (`PPP:725-731`, ch11); a profile still being generated may
  only contribute its already-populated first element (`PPP:719-724`).
- **Source element** (`PPP:732-762`): with `elementdefinition-profile-element`, the element with that **id** in
  the profile snapshot (`PPP:734-749`; not found → `log.info("… consult Grahame Grieve")` if the profile is
  mid-generation, throw `Unable to find element {0} in {1}` for Extension/Resource bases, else silently
  nothing); otherwise the profile's **root** (`PPP:754`) — a RESOURCE profile's root **constraints are
  cleared** ("the sense of %resource changes when the root resource is treated as an element. The validator
  will enforce the constraint", `PPP:756-760`).
- **The template is used only when the base element's type is `Extension` or `Resource`** (`PPP:763-772`).
  For a Resource the template's min/max are reset to the base's ("temporary work around"). For every other
  type — Address, Quantity, Identifier profiles — the template stays null and the plain base row is used.
- The template is then filled from the base row **fill-if-absent** for 27 properties (`fillOutFromBase`,
  `PU:1886-1945`: sliceName, label, code, definition, short, comment, requirements, alias, min, max, fixed,
  pattern, example, min/maxValue, maxLength, mustSupport, isSummary, isModifier(+reason), mustHaveValue,
  binding, constraint by key, extension by url), so an extension definition's root wins over the base's
  `extension` row wherever it states a value — the mirror image of .NET's root-only merge, but restricted to
  extensions and inline resources.

**Extension cardinality (OQ-001, Java side).** The template carries the extension definition's root `min`/`max`
(`PPP:768` exempts `Extension` from the min/max reset). But the slice-min rule at `PPP:801-805` then sets
`min = 0` for any named slice whose diff states no `min` under a non-`closed` slicing (hack exemption: paths
ending `xtension.value[x]`) — so an extension root's `min=1` does **not** reach an open extension slice; its
`max` does, capped afterwards to the slicer's max (`PPP:816-818`). New slices under an already-sliced base use a
separate pick-up (`PPP:1398-1420`): with exactly one declared profile, the slice `min` is raised to the profile
root's `min` if the root is mandatory and the slice is not, and `max` lowered if the root does not repeat and
the slice does ("todo: should we consider other constraints?"); **two or more profiles → `throw new
Error("Not handled: multiple profiles at …")`** — a `java.lang.Error` for the spec's legal disjunction.

### 2. The profile-doc override in `updateFromDefinition` (`PU:2619-2688`)

For every merged element the diff's `type[0].profile[0]` (or, for an inherited named slice, the base row's own
profile) is resolved via `findProfile` + xver. A resolved profile that is **neither type `Extension` nor kind
RESOURCE/LOGICAL is discarded** (`PU:2643-2648`, in-code: "we sometimes want the details from the profile to
override the inherited attributes, and sometimes not"). For Extension/RESOURCE profiles the root's
`definition`, `binding.description`, `short`, `comment`, `requirements` are copied onto the element and
`alias`/`mapping` **replaced** (`PU:2650-2671`) — refilling what `checkExtensionDoco` had just blanked
(`PU:1948-1961`). Unresolvable profile: `log.warn`, then the `allowUnknownProfile` gate (`PU:2676-2687`) —
Extension → throw unless `ALL_TYPES`; other types → throw only if `NONE`; profile-element references are
exempt ("todo: should we change down the profile_element if there's one?", `PU:2673`). **Effective default:
nothing throws** — the field default is `ALL_TYPES` (`PU:448`) although the enum comment calls `NONE` "the
default" (`PU:223`; the shared-test driver also defaults to `ALL_TYPES` and only `t15a` sets `allow="none"`).
Filed upstream as a doc/default mismatch
([hapifhir/org.hl7.fhir.core#2597](https://github.com/hapifhir/org.hl7.fhir.core/issues/2597)).

### 3. Walking into children — the profile's snapshot is used only when the base has none

Every step-in re-enters the walk on the type SD's snapshot **skipping its root** (`ProfilePathProcessorState(dt,
snapshot, 1, …)`) with the paths remapped (`contextPathSource/Target`) and the diff window limited to the
element's child rows. The type SD comes from `getProfileForDataType(tr, …)` (`PU:2073-2093`): `findProfile(
profile[0])` → xver (Valid only, re-entrant generation) → `fetchTypeDefinition(workingCode)`; resolution
failures are only `log.debug`/`log.warn`ed. Rules shared by the paths:

- **Base children win.** The one-match path steps into the type only when the base snapshot has no children
  at this element (`!baseWalksInto`, `PPP:829`; same test at `PPP:1080`, `1423`, `1672`). If a parent profile
  already expanded `Patient.address` (because it constrained `address.city`), Java walks the base children and
  **never opens the type profile's snapshot** — the profile url stays on `type.profile` for the validator.
  This is the inverse of .NET's "type beats base" (OQ-002).
- **Type choice under multiple types**: >1 distinct codes → the core `Element` SD (`PPP:908`, `1158` — only
  `id`/`extension` addressable, as in .NET); equal codes → first TypeRef; a TypeRef with **>1 profiles → the
  core type** (`PPP:914-915`, = .NET's `SafeSingleOrDefault`); one profile → that profile's snapshot.
- **Multi-type guard** (`{0} has children ({1}) and multiple types ({2}) in profile {3}`): **live** in
  `getTypeForElement` (`PU:1621-1625`, reached from the slicing-entry step-in `PPP:385` and the sliced-base
  empty-diff path `PPP:1680`) and for new slices under a sliced base (`PPP:1425-1428`); **dead** at the two
  most common sites — one-match `PPP:846-851` and empty-match `PPP:1097-1116` — where the `nonExtension` flag is
  computed over a `diffMatches` list that has at most one element. There the multi-type parent silently
  expands against `Element` and its non-extension child rows orphan as "No match found …" ERRORs (ch4). Filed
  upstream with all four sites
  ([hapifhir/org.hl7.fhir.core#2596](https://github.com/hapifhir/org.hl7.fhir.core/issues/2596)).
- **Renamed choice** in the one-match path: base `value[x]` + diff `valueString` narrows the type list to the
  suffix type before stepping in (`PPP:830-842`, ch6/OQ-018).
- Other throws on this path: no type and no contentReference → `{0} has no children ({1}) and no types in
  profile {2}`; unresolvable type → `Unknown type {0} at {1}` / `{0} has children ({1}) for type {2} in
  profile {3}, but can't find type`.

### 4. The final profile/type consistency sweep (`PU:1038-1077`)

After `setIds`, every `type.profile` in the snapshot is resolved (`findProfile` → xver): unknown → **WARNING**
"The type of profile … cannot be checked as the profile is not known" (this is `ext-recursion-2`'s single
message: its own snapshot is mid-generation, ch11); known → the profile's `type` (or, with profile-element,
the nominated element's type summary) must equal the element's working code (`Bundle.entry.response.outcome`
hard-wired to `OperationOutcome`), else `isCompatibleType` walks the base chain (`PU:1464-1480` — dereferences
`fetchTypeDefinition(workingCode)` unchecked: NPE for a working code that is not a loaded type) → ERROR
message. This is the only place Java checks profile/type consistency for elements whose profile drove neither
a template nor a step-in.

### `Extension.url` — Java never fixes it

There is **no** generator-side `fixedUri` backfill on `Extension.url` anywhere in PU/PPP/PRE (the only
`setFixed(new UriType(url))` is the legacy xver template synthesizer `makeExtensionForVersionedURL`,
`PU:4715`). Test evidence: `ext-recursion-2`'s input and expected both contain **zero** `fixedUri`;
`ext-sort-issue`'s single expected `fixedUri` is inherited from its base extension's snapshot (and keeps the
*base's* url — .NET would too, since `fixExtensionUrl` only fills an absent `fixed[x]`). The convention is
produced by authoring tools and inherited; .NET's `fixExtensionUrl` is .NET-only —
[DEV-037](13-deviation-register.md#dev-037--extensionurl-fixeduri-net-synthesizes-java-inherits-only-ch7).
The `isSimpleExtension`/`isComplexExtension`/`isModifierExtension` helpers (`PU:4933-4964`) are not used by
the generation walk — ext-1's simple/complex split has no generator effect in Java either.

### profile-element vs fragment syntax (OQ-017, Java side)

Java **honors `elementdefinition-profile-element`** by element **id**, for template selection (`PPP:734-749`),
the type-compatibility check (`PPP:715`, `PU:1650-1662`) and the final sweep (`PU:1058-1066`) — but **not** for
the children walk-in, which always opens the profile at its root (`PU:2673` todo). The legacy `url#fragment`
form is **silently stripped** by `findProfile` (`PU:4100-4102`): the whole SD is used and the fragment is never
read. No shared test exercises the extension (grep over `fhir-test-cases r5/snapshot-generation`: empty).

### Cross-version extensions and obligation profiles (Java-only surfaces)

- **xver**: the `XVerExtensionManager` (outside the profile package) recognises the versioned extension url
  shape and synthesizes an extension SD on demand (base `Extension`, snapshot generated re-entrantly at
  `PPP:710`/`PU:2080`/`PU:2638`); consulted at template selection, `getProfileForDataType`, the doc override
  and the final sweep. `PPP:700` tests the raw field (`getXver()`) while the PU sites use the lazy
  `makeXVer()` — for a caller that never calls `setXver` (only the test driver does) the template-selection
  branch is skipped until a later lazy initialisation (order-dependent; needs-verification, JI-19). Shared
  tests: `pat-xver-extension`, `es-xver`. .NET resolves such urls like any canonical (the harness pre-loads
  the xver package for parity).
- **Obligation profiles**: `findInheritedObligationProfiles` (`PU:1205-1215`) accepts an `inherit-obligations`
  target only if it is flagged `obligation-profile` **and shares the derived SD's `baseDefinition`** (version
  stripped); the list lives on the `ProfileUtilities` instance and is never cleared. The `mustSupport` /
  additional-binding fold (`updateFromObligationProfiles`, `PU:2529-2582`) runs **only on the copy-through path**
  (`PPP:1068`); elements the differential touches get only the obligation *extensions* re-added
  (`PU:2608-2614`) plus the inverted additional-binding fold (JI-13). A diff row that merely edits `short` on an
  obligated element therefore loses the obligation's `mustSupport` (asymmetry; `profile-patient-op3` cannot
  show it — its diff touches other elements; JI-18 needs-verification). Refines
  [DEV-032](13-deviation-register.md#dev-032--java-only-merge-inputs-additionalbase-and-obligation-profiles-ch3).

## Deviations
- [DEV-037](13-deviation-register.md#dev-037--extensionurl-fixeduri-net-synthesizes-java-inherits-only-ch7) —
  J-d: `Extension.url` fixed-value convention is synthesized by .NET only; Java inherits or expects it authored.
- [DEV-038](13-deviation-register.md#dev-038--type-profile-scope-net-merges-any-type-profile-java-only-extensionresource-roots-and-only-when-the-base-has-no-children-ch7) —
  J-d: .NET merges any type profile (root-only or full snapshot, type beats base); Java merges only
  Extension/Resource roots as templates and opens a profile's snapshot only where the base has no children.
- [DEV-021](13-deviation-register.md#dev-021--new-elements-seeded-from-the-datatypes-snapshot-root-net-enriches-java-doesnt-ch7) —
  Phase-4 sweep: `createNewElement`'s seed-from-type-root design makes .NET enrich new elements with
  datatype root properties (comment/alias/binding extensions/invariants); Java/golden don't. Explains
  the four biggest sweep classes (~7,000 property diffs) incl. all of TYPE-CONSTRAINT.
- [DEV-022](13-deviation-register.md#dev-022--propertyextension-fidelity-when-copying-elements-from-external-structures-ch7ch12) —
  Phase-4 sweep: .NET's child copies are verbatim (minus the 17-URL blocklist); Java filters tooling
  extensions and synthesizes reduced extension-header entries.

## Open questions
- [OQ-001](14-open-questions.md#oq-001--the-cardinality-diamond-problem) cardinality diamond (root-only merge).
- [OQ-002](14-open-questions.md#oq-002--priority-type-profile-constraints-vs-base-constraints) merge priority.
- [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors) error taxonomy rows
  from this chapter (silent skip / issue+continue / abort-children / throw).
- [OQ-017](14-open-questions.md#oq-017--starting-expansion-below-the-root-extension-vs-fragment-syntax)
  profile-element extension vs fragment syntax.
