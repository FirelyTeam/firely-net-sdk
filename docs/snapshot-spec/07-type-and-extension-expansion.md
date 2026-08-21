# 7. Type-profile and extension expansion

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

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

## .NET behavior (Phase 2)
*(pending — `mergeTypeProfiles` :1208 (the densest region), `expandElement` :626, `isValidTypeProfile`
:2545, `fixExtensionUrl` :1743, `ProfileReference` url#element parsing; known: type profiles get priority
over base, merge order documented at :1383)*

## Java behavior (Phase 3)
*(pending)*

## Open questions
- [OQ-002](14-open-questions.md#oq-002--priority-type-profile-constraints-vs-base-constraints) merge priority.
