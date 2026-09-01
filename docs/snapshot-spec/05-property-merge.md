# 5. Per-property merge semantics

> Status: **spec baseline + .NET + Java behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2
> packet 1, 2026-08-24: `ElementDefnMerger.cs` deep-read; Phase 3 packet J-b, 2026-09-01:
> `ProfileUtilities.updateFromDefinition` + `MappingAssistant` + preprocessor additional-base merge deep-read).

## Scope
The heart of the algorithm: for each `ElementDefinition` property, what happens when the differential
provides a value and the base already has one. End state: a complete property table with, per property,
merge class, matching key for collections, spec basis, and both implementations' behavior.

## Spec baseline (R5)

### The headline: there is no merge algorithm

The spec defines what a valid differential element and a valid snapshot element each look like — it never
states how to compute `snapshot = base + differential`. Explicit "absent in differential → inherit from
base" exists for exactly **two** properties: `min`/`max` [elementdefinition #min-max] and `mustSupport`
[elementdefinition #missing, which adds "the snapshot SHOULD always populate the mustSupport value"].
Everything else is inference from the rules below. The general principles:

> "Any changed definitions SHALL be restrictions that are consistent with the rules defined in the
> resource in the FHIR Specification from which the profile is derived." [profiling §5.1.0.9]

> "Note that structure definitions cannot 'remove' mappings and constraints that are defined in the base
> structure, but for purposes of clarity, they can refrain from repeating them." [profiling §5.1.0.9]

**Omission is never removal** — silent differentials carry base content forward.

### The interpretation table and its four footnotes

[elementdefinition #interpretation] classifies every property as prohibited/optional/required per context
(type definition vs constraint definition × first vs following elements). Its four footnotes are the
spec's core merge semantics:

- **†** "The element's presence, and value, must match the definition in the base definition" —
  `defaultValue[x]`, `meaningWhenMissing`, `isModifier`, `isSummary`, and min/max on following elements.
- **‡** "The element content must be consistent with that matching element in the base definition" —
  `short`/`definition` (required), `requirements`/`comments`/`alias` (optional).
- **∆** "Additional constraints and mappings can be defined, but they do not replace the ones in the base
  definition" — `constraint`, `condition`, `mapping`.
- **§** type-root cardinality constrains references to the type (see ch3/ch7).

Known defects of the table (see [RFC-003/004/007](15-spec-rfcs.md)): the `base` row reads "required" in
both constraint columns (resolved by the definitions page: base is a *snapshot* obligation the generator
fulfills, ch10); one row still says `nameReference`; no rows exist for `mustHaveValue`,
`valueAlternatives`, `sliceIsConstraining`, `binding.additional`, `representation`, `orderMeaning`,
`isModifierReason`.

### Property-by-property: what the spec actually pins down

**Cardinality (`min`/`max`)** — differential-absent → inherit [#min-max]; snapshot must populate (also
sdf-3). Narrowing-only, from profiling §5.1.0.6 (verbatim table): derived `[min,max]` must be a sub-range
of base `[min,max]` ("the constraining profile can only allow what the base profile allows"). New R5 rule:
with `min = 0`, fixed/pattern apply only if the element is present.

**Descriptive properties** — profiling §5.1.0.8 (new table in R5): "The meaning and guidance provided in
the base resource or profile can't be invalidated, only constrained or contextualized."

| Property | Revise? | Add? | Remove? |
|---|---|---|---|
| label | Yes | | |
| code.coding | | Yes | Yes |
| short | Yes | | |
| definition | Yes | | |
| comment | Yes | Yes | Yes |
| requirements | Yes | Yes | |
| alias | Yes | Yes | Yes |
| example | Yes | Yes | Yes |
| mapping | Yes | Yes | Yes |

This licenses author intent (including *removal* for some collections — in tension with the ∆-additive
model for mapping) but still doesn't say how a generator merges (does a differential `alias` list replace
or union with the base's? unstated).

**`mapping`** — the one property with explicit replace-by-key semantics: "providing a new mapping with the
same identity … means that the new mapping replaces a mapping with the same identity in the element being
profiled" [profiling §5.1.0.9, new in R5]; otherwise additive (∆); eld-27 (warning) unique-by-identity.

**`constraint`** — additive only: "Profiles may define additional constraints … but they cannot alter or
remove constraints that are already applied" [conformance §2.1.1.0.6]. `constraint.source` = "A reference
to the original source of the constraint, for traceability" — generators are expected to stamp/preserve it
on inherited constraints. `constraint.suppress` (R5, Trial Use) suppresses an inherited warning/hint;
eld-26: errors cannot be suppressed. Tension: constraints "must be unique by key" (eld-14) vs additivity
when a differential restates an inherited key ([RFC-009](15-spec-rfcs.md#rfc-009--eld-14-vs-additive-constraints-restating-an-inherited-constraint-key)).

**`condition`** — additive (∆), union.

**`binding`** — strength lattice [profiling §5.1.0.21]: required→required; extensible→required/extensible;
preferred→anything but example stays; example→anything. "Whatever the constraining profile does, it cannot
make codes valid that are invalid in the base profile." Value-set replacement rules per strength
[§5.1.0.20]. `binding.additional` (R5, Trial Use): "Additional bindings do not replace the main binding".
eld-23 (description or valueSet), eld-11 (bindable types only), eld-12 (valueSet url forms). **Merge
granularity unstated** — wholesale replace vs per-sub-element ([RFC-010](15-spec-rfcs.md#rfc-010--binding-merge-granularity-unstated)).

**`type`** — "The Type of the element can be left blank in a differential constraint, in which case the
type is inherited from the resource" [elementdefinition-definitions]. A profile may "restrict the types of
a choice element" [profiling §5.1.0.9]. `profile`/`targetProfile` are 0..* disjunctions ("content must
conform to at least one"). eld-13 (unique by code), eld-4/17 (aggregation/targetProfile only on
reference-ish types). **List-merge semantics unstated** — subset enforcement, and whether
profile/targetProfile lists on a matching code replace or append ([RFC-008](15-spec-rfcs.md#rfc-008--replace-vs-append-semantics-unstated-for-most-repeating-properties)).

**`fixed[x]` / `pattern[x]`** — optional in constraint differentials, prohibited in type definitions.
Exact matching semantics defined (primitive exact match; pattern on arrays: each pattern item matches at
least one instance item, recursively; complex: per-property recursive). eld-6/7 (single-type only), eld-8
(mutually exclusive), eld-24 (guideline: prefer pattern). **Compatibility with an inherited base
fixed/pattern is unstated.**

**Flags** — `mustSupport`: false→true only, never true→false [profiling §5.1.0.22]; absent → inherited
[#missing]. `isModifier` (+`isModifierReason`, eld-18): frozen — "The value of the flag cannot be changed
by profiles on the resource, in either direction" [conformance §2.1.1.0.4]. `isSummary`: † (must match
base). Obligations may be added, "existing actor obligations cannot be undone or loosened" [profiling
§5.1.0.22].

**Frozen / base-standard-only properties** — `defaultValue[x]` ("can never be changed"; profiles never
define them, logical models may), `meaningWhenMissing` ("can never be changed"; eld-15 mutually exclusive
with defaultValue), `representation` ("profiles must reproduce what the base standard does"),
`orderMeaning` ("if absent in the base type, a profile cannot assert meaning"), `contentReference` (ch8),
`base` (generator-populated, ch10).

**Value-domain constraints** — `minValue[x]`/`maxValue[x]` (inclusive; type-matching rules; canonical
Quantity comparison; **no narrowing rule stated**), `maxLength` (no conformance expectation when absent;
merge unstated), `mustHaveValue`/`valueAlternatives` (R5 Trial Use shortcuts for primitive `.value`
profiling; eld-28 mutually exclusive; valueAlternatives-absent = any extension allowed).

**`code`, `alias`, `example`** — repeating; replace-vs-append **unstated** ([RFC-008](15-spec-rfcs.md)).

**`short`/`definition`/`comment`/`requirements`/`label`** — ‡-consistency with base; universally
implemented as differential-replaces-base, but that is inference, not spec text. *(Note: the `"..."`
append-to-inherited-text convention used by implementations appears nowhere on these pages — see
[OQ-010](14-open-questions.md#oq-010--the--append-convention).)*

### Post-merge validity

No rule says which invariants must be *re-verified after merge* — e.g. base pattern + differential
narrowing the type list to 2 creates an eld-7 violation neither input had. Generator obligations here are
entirely unspecified (ch12).

## R4/R4B deltas

- New in R5: `mustHaveValue`, `valueAlternatives`, `constraint.suppress`, `binding.additional` (all Trial
  Use); `constraint.xpath` **removed**; string→markdown on `binding.description`, `constraint.requirements`,
  `mapping.comment`; fixed/pattern/default/example value types 50→54 (−Contributor, +integer64,
  CodeableReference, RatioRange, Availability, ExtendedContactDetail, Meta).
- **Mapping replace-by-identity is new in R5** — R4 mappings were additive-only. A dual-version generator
  forks behavior here.
- The §5.1.0.8 descriptive-elements table is new in R5 (R4 had only the ‡/∆ footnotes).
- The interpretation table itself is cell-for-cell identical R4→R5 (both contain the same defects).
- New R5: min=0+fixed/pattern presence rule; eld-23..28.

## R6-build note (v6.0.0-ballot4, fetched 2026-08-21)

The interpretation table is **reworked** in R6: headers renamed ("first" → "root element"), a softened
"expected / not expected" category added (the `base` erratum is fixed this way), the
requirements/comments/alias row split into three (comment and alias become optional everywhere), slicing
becomes optional in specialization non-root elements, mustSupport/isModifier/binding cells loosened for
some contexts. New invariants **eld-29..34** (distinct profiles/targetProfiles, additional-binding key
hygiene, `descriptive` binding strength ⇒ no valueSet, >1 bindable type ⇒ no binding). `binding.strength`
gains `descriptive`; `binding.additional` gains `key` (the constrain-by-key mechanism, see RFC-010);
`valueAlternatives` extends beyond primitives (explicit no-propagation-to-children); descriptive-properties
table gains Remove for label/requirements and names `elementdefinition-suppress` as the removal mechanism —
directly relevant to [OQ-008](14-open-questions.md#oq-008--verbosity-of-generated-snapshots).

## .NET behavior (Phase 2, deep-read 2026-08-24)

All per-property merging lives in one place: `ElementDefnMerger` (`ElementDefnMerger.cs`, a private helper
struct of `SnapshotGenerator`), invoked through `SnapshotGenerator.mergeElementDefinition()`
(`SnapshotGenerator.cs:1176`). `merge(snap, diff)` mutates `snap` in place; `snap` is the working snapshot
element (already carrying base and possibly type-profile content) and diff wins except where noted. Two
call-site arguments modulate behavior:

- **`mergeElementId`** — `true` when merging a differential of the profile being generated (main merge path
  `SnapshotGenerator.cs:1052`, newly added child elements `:920`, root rebase `:2502`); `false` when merging
  an *external type profile's* root (`:1476`) or a generator-created slicing entry (`:1859`). See `mergeId`
  below and ch10.
- **`baseUrl`** — canonical of the SD whose constraints are being merged in; used only to stamp
  `constraint.source` (see below).

Every property actually changed raises the `OnConstraint` callback (`SnapshotGenerator.cs:2171-2175`), which
decorates the result with the "constrained-by-diff" extension and/or annotation when
`SnapshotGeneratorSettings.GenerateExtensionsOnConstraints` / `GenerateAnnotationsOnConstraints` are set
(ch12).

### The five generic merge primitives

1. **`mergePrimitiveElement(snap, diff, allowAppend=false)`** (`ElementDefnMerger.cs:802`) — diff *value*
   replaces snap value. Wrinkles: a diff primitive carrying only extensions (no value) does **not** wipe the
   snap value (`:849`, comment credits "Java validator logic"); element id merges diff-wins (`mergeString`,
   `:891`); extensions merge by url, with special handling for the `translation` extension, matched on its
   `lang` sub-extension instead of url alone (`:399-454`, string/markdown primitives only). With
   `allowAppend`, the `"..."` append convention applies (below).
2. **`mergePrimitiveCollection(snap, diff, matchItems)`** (`:317`) — union: unmatched diff items append,
   matched items merge per primitive rule 1.
3. **`mergeComplexAttribute(snap, diff)`** (`:760`) — the *overlay*: if diff's type equals or derives from
   snap's, clone snap and `CopyTo` diff over it — generated `CopyTo` copies only **non-null, top-level**
   properties, and replaces nested lists wholesale (e.g. `SlicingComponent.CopyToInternal`,
   `Generated/ElementDefinition.cs:477`). Properties absent in diff are inherited from snap; there is no
   recursive per-subproperty merge (in-code TODO `:779`). If diff's type is *incompatible* with snap's
   (e.g. `fixedCode` over `fixedString`), diff replaces wholesale.
4. **`mergeCollection(snap, diff, matchItems)`** (`:622`) — union keyed by the matcher: unmatched diff items
   append, matched items merge by overlay (rule 3). Items are never removed.
5. **`mergeCollectionWithSuppression(snap, diff, matchItems)`** (`:528`) — rule 4 plus honoring the
   `elementdefinition-suppress` extension (`SnapshotGeneratorExtensions.cs:61`): a diff item carrying
   suppress *removes* the matching inherited item (or is itself dropped if unmatched). Used only for
   `mapping` and `example`, and only when `SnapshotGeneratorSettings.RespectSuppressExtension` is set
   (`:604`, `:613`).

Rules that fall out of the primitives, for every property below: an absent/empty diff property never
removes anything; a diff exactly equal to snap is a no-op (no `OnConstraint`); and collection matching runs
against the *original* snap list, so two diff items with the same key overwrite each other (last one wins).

### Per-property table

Source order of `merge()` (`ElementDefnMerger.cs:47-194`). Behavior shorthand: **replace** = diff value wins
(primitive rule 1) · **union(key)** = collection merge keyed as stated (rules 2/4/5) · **overlay** =
non-null top-level diff properties win, rest inherited (rule 3) · **frozen** = never touched by diff.

| Property | .NET behavior | Collection key | Code |
|---|---|---|---|
| `id` (element id) | special — see `mergeId` below | — | `:52`, `:864` |
| `extension` | union(url) | url, ordinal | `:55`, `:913` |
| `modifierExtension` | union(url) — semantics unclear, in-code question | url | `:57-59` |
| `path` | never merged; a last-segment rename is *validated* (allowed only if the snap element is a choice type, else `InvalidOperation`) — snapshot keeps the `[x]` form (`NORMALIZE_RENAMED_TYPESLICE` active, ch2/ch6) | — | `:61-93`, `:11` |
| `representation` | union(exact value) — spec says reproduce base; not enforced | full value | `:96` |
| `sliceName` | replace | — | `:99` |
| `sliceIsConstraining` | replace (merged twice — duplicate call, idempotent) | — | `:101`, `:114` |
| `label` | replace | — | `:103` |
| `code` | union(system+version+code; display if both codes absent) | Coding | `:107`, `:896` |
| `slicing` | overlay (`discriminator` list replaced wholesale when diff has one; an *empty* diff discriminator list is patched back to base — `correctListMerge`) | — | `:110-112`, `:196` |
| `short` | replace | — | `:116` |
| `definition` | replace, `"..."` append | — | `:117` |
| `comment` | replace, `"..."` append | — | `:118` |
| `requirements` | replace, `"..."` append | — | `:119` |
| `alias` | union(string value) | string | `:122` |
| `min` | most-restrictive: `max(snap, diff)`; a loosening diff is **silently ignored** | — | `:125`, `:666` |
| `max` | most-restrictive: numeric compare, `*` = unbounded; unparseable diff → keep snap; `max < min` not corrected (`constrainMax` exists, unused `:738`) | — | `:126`, `:696` |
| `base` | frozen (generator-populated, ch10) | — | `:128` |
| `contentReference` | frozen (ch8) | — | `:129` |
| `type` | diff list **replaces** (removal allowed); surviving items merged by code — see below | type.code (null matches null) | `:134`, `:222` |
| `defaultValue[x]` | overlay — spec says frozen; not enforced (deliberate: "validator can detect invalid constraints" `:152`) | — | `:136` |
| `meaningWhenMissing` | replace — spec says frozen; not enforced | — | `:137` |
| `orderMeaning` | replace | — | `:139` |
| `fixed[x]` | overlay (same/derived type) or wholesale replace (other type) — see OQ-012 | — | `:141` |
| `pattern[x]` | overlay / wholesale replace — same as fixed | — | `:142` |
| `example` | union(label), suppress honored | label | `:147`, `:924` |
| `minValue[x]` / `maxValue[x]` | overlay; no narrowing check | — | `:149-150` |
| `maxLength` | replace | — | `:153` |
| `condition` | union(string value) | string | `:156` |
| `constraint` | union(key), matched items overlay; `source` stamped afterwards | constraint.key | `:167`, `:465` |
| `mustHaveValue` | replace | — | `:170` |
| `valueAlternatives` | union(canonical value) | canonical | `:171` |
| `mustSupport` | replace — `true`→`false` possible; spec allows only `false`→`true`; not enforced | — | `:174` |
| `isModifier` | replace — spec says frozen; not enforced | — | `:177` |
| `isModifierReason` | replace | — | `:179` |
| `isSummary` | replace — spec says must match base; not enforced | — | `:181` |
| `binding` | sub-property overlay — see below | — | `:183-189` |
| `mapping` | union(identity **+ map**), suppress honored — deviates from R5 replace-by-identity, see DEV-017 | identity+map | `:193`, `:918` |

### Element id (`mergeId`, `:864-889`)

With `mergeElementId = false` (type-profile roots, slicing entries): always `null` → regenerate. With
`true`: a diff-supplied id wins; a newly introduced named slice (diff sliceName ≠ snap sliceName) never
inherits — id is cleared for regeneration; otherwise the snap id is inherited. Interacts with
`GenerateElementIds` (ch10, OQ-009).

### The `"..."` append convention (`:817-846`)

For `definition`, `comment` and `requirements` only (`allowAppend: true`), a diff string starting with
`"..."` means *append to inherited text*: result = snap text + CRLF + diff text minus the marker. An
`AppendedTextAnnotation` (`SnapshotGeneratorAnnotations.cs:67`) guards against double-append when the same
element is merged repeatedly (type-profile expansion re-merges). Not in the spec — pure .NET convention
([OQ-010](14-open-questions.md#oq-010--the--append-convention)).

### Element types (`mergeElementTypes`, `:222-314`; issue #827, DEV-001)

The one collection with replace semantics: a non-empty diff `type` list defines the result — inherited
types not restated are *removed*. Each diff type is then item-merged with the base type of the same `code`
(null code matches null code, for `[primitive].value` / `Extension.url`), preserving base extensions such as
the json/xml/rdf "compiler magic" on primitive value types. Within a matched type: `profile` and
`targetProfile` lists **replace wholesale** when the diff has any (`mergeCanonicals` `:387-391`, explicit R4
decision: differentials may remove profiles); `aggregation` replaces wholesale (`:297-306`); `versioning`
replaces; extensions union by url.

### Constraints and `constraint.source` (`mergeConstraints`, `:465-524`; issue #1052, DEV-002)

Diff constraints match inherited ones on `key` alone; a restated inherited key produces an *overlay merge*,
not a duplicate (the .NET answer to [RFC-009](15-spec-rfcs.md#rfc-009--eld-14-vs-additive-constraints-restating-an-inherited-constraint-key)).
Unmatched keys append. Afterwards `InitializeConstraintSource` stamps `source` = `baseUrl` on **every**
constraint in the result that lacks one — including constraints inherited from the base whose own generation
never set `source`, which thereby get attributed to the *derived* profile's url (at `SnapshotGenerator.cs:1052`
the stamp url is the differential's SD). Type-profile merges stamp with the type profile's url (`:1476`).
The stamping only runs at all when the diff declares at least one constraint on that element.

### Binding (`mergeBinding`, `:358-380`)

Sub-property overlay: `strength` and `description` replace (so a diff can silently *loosen* strength — the
§5.1.0.21 lattice is not enforced), `valueSet` overlays (introduced to keep diff extensions on
`binding.valueSet` merged with base, MS 20201211), `additional` unions by exact value, extensions union by
url. After the merge, the whole binding is **removed** if no remaining type is bindable
(`ModelInspector.IsBindable` = the type implements `ICoded`, `ClassMapping.cs:179`; MV 20220803, `:186-189`)
— the .NET data point for [RFC-010](15-spec-rfcs.md#rfc-010--binding-merge-granularity-unstated).

### Quirks and non-behaviors worth recording

- `sliceIsConstraining` is merged twice (`:101`, `:114`) — harmless (idempotent), plain duplication.
- `correctListMerge` (`:196-203`) exists solely to repair `slicing.discriminator` being wiped by an empty
  (auto-instantiated) diff list during overlay; the same hazard is latent in every overlay-merged property
  with nested lists.
- `modifierExtension` merging carries an unanswered in-code question ("Q: What does this mean? How should
  consumers handle these?", `:57-58`).
- No † rule of the interpretation table is enforced anywhere in the merger; the only properties where .NET
  enforces *most-restrictive* rather than diff-wins are `min`/`max` — and there an illegally loosening
  differential is silently ignored rather than reported ([OQ-011](14-open-questions.md#oq-011--what-must-a-generator-enforce)).

## Java behavior (Phase 3 packet J-b, deep-read 2026-09-01)

All citations against clone commit `b06c7ee`; full working notes in the materials extract
`java-ch05-merge-updateFromDefinition-2026-09-01.md`. Abbreviations: PU = `ProfileUtilities.java`,
PPP = `ProfilePathProcessor.java`, PRE = `SnapshotGenerationPreProcessor.java`, MA = `MappingAssistant.java`.

Java's counterpart of `ElementDefnMerger.merge()` is **`ProfileUtilities.updateFromDefinition(dest, source, …)`**
(PU:2585-3128). Naming trap: inside, `base` = `dest` (the working snapshot element, already seeded by
`updateFromBase`) and `derived` = `source` (the differential element). Unlike .NET, the **differential is
mutated too**: diff properties that deep-equal the base are stamped with `SNAPSHOT_DERIVATION_EQUALS` user
data (rendering aid; the diff is a clone, PU:824, with user data migrated back). Call sites: PPP:373 (simple
path), PPP:813 (slice content, `fromSlicer=true` — relaxes the mustSupport/mustHaveValue direction checks),
PPP:1246 (slicing entry), PPP:1396 (reslice template), PU:849 (specialization leftovers).

Two parameters .NET has no analog for:

- **`trimDifferential`** — intent: delete equal-valued diff properties from the differential instead of
  marking them. It is hard-`false` at every call site (PPP:139/176) **except PPP:1246, which passes the
  closed-slicing flag** (`base.slicing.rules == CLOSED`) into it — reads like parameter abuse; net effect on
  the *snapshot* is nil (trim branches only touch the diff clone, and only when values already match), so the
  parameter is near-dead within snapshot generation. Its branches carry a copy-paste asymmetry (comment/label/
  requirements write `base.setX(derived.getX().copy())` where the others write `derived.setX(null)`,
  PU:2716/2725/2734) — harmless for the same reason.
- **`fromSlicer`** — `true` only when merging slice content under a slicer (PPP:675/813); suppresses the
  "illegal constraint mustSupport=false / mustHaveValue=false when base says true" errors for slices.

### Before the property loop (PU:2586-2688)

- **Extension-doco wipe** (`checkExtensionDoco`, PU:1948-1961, task 3970): when dest is `Extension` root /
  `*.extension` / `*.modifierExtension`, its definitional text is *discarded before merging* (definition = "An
  Extension", short = "Extension", comment/requirements null, alias/mapping cleared). The returned flag later
  gates isModifier merging (below).
- **Obligation profiles** (Java-only surface, [DEV-032](13-deviation-register.md)): same-id elements from
  registered obligation profiles contribute obligation extensions (PU:2608-2614), mustSupport aggregation
  (PU:2859-2870) and additional bindings (PU:2936-2954 — where the guard is inverted: `hasAdditional` instead
  of `!hasAdditional` at PU:2949, so obligation-profile additional bindings are only copied when already
  present; upstream-issue candidate).
- **Profile-root override** (PU:2619-2688): for a named slice with a single `type.profile` (or a source whose
  first type carries one, with xver fallback for cross-version extension urls), and the resolved profile is an
  Extension or a resource/logical: the **profile root's** definition (relative-url-rewritten), short
  (unconditionally), comment, requirements, binding.description replace dest's, and dest's alias + mapping
  lists are cleared and replaced by the profile root's — the referenced profile's text beats the base *before*
  the diff applies. Unresolvable profiles: warn, then throw unless `allowUnknownProfile` permits (PU:2672-2688).
- ED-level **extensions** merge via `updateExtensionsFromDefinition` (PU:3199-3217) — see table row and OQ-019.

### Per-property table (Java column)

Pattern per property: `if (diff.hasX) { if (!deepEqual) apply; else mark DERIVATION_EQUALS }`. As in .NET, an
absent diff property never removes anything and an equal diff is a no-op — but several properties .NET merges
are **absent from the Java routine entirely** and get silently dropped from the differential (verified by
regex sweep of PU:2585-3128): `code`, `representation`, `orderMeaning`, `meaningWhenMissing`,
`defaultValue[x]`, `sliceIsConstraining`, `contentReference` (frozen there too, ch8). For
isModifier/defaultValue/meaningWhenMissing this is deliberate frozen-rule enforcement (comment PU:2906); for
`code` it contradicts the §5.1.0.8 add/remove license. (Code-derived; no shared test supplies these in a diff —
carried-forward empirical check.)

| Property | Java behavior | Cite |
|---|---|---|
| `sliceName` | replace | PU:2690 |
| `short` | replace | PU:2694 |
| `definition`, `comment`, `requirements` | replace; `"..."` append via `mergeMarkdown` (identical convention to .NET: base + CRLF + diff minus marker, `Utilities.appendDerivedTextToBase`); extension-only diff doesn't wipe text; requirements then stripped from root elements (sdf-9) | PU:2703-2742, 3134 |
| `label` | replace; `"..."` append via `mergeStrings` — **operands swapped**: result = diff text (marker kept) + CRLF + base text minus first 3 chars. Broken; .NET has no label append at all | PU:2721, 3152-3168 |
| `alias` | union (string value) | PU:2744 |
| `min` | **diff wins**, even when loosening — loosening non-slice diff also raises an ERROR message. Contrast .NET: most-restrictive, loosening silently ignored → *different snapshots on illegal input* | PU:2757-2766 |
| `max` | **diff wins**; ERROR message when larger than base (`*`-aware, PU:3380) | PU:2768-2777 |
| `fixed[x]` / `pattern[x]` | **wholesale replace** (no .NET-style partial overlay — the Java answer to [OQ-012](14-open-questions.md#oq-012--partial-overlay-of-fixedxpatternx-values)); post-merge `checkTypeOk` errors when the value's type isn't among the element's types | PU:2779-2796, 3121-3126, 3342 |
| `example` | union keyed on **label + value** (both must match; .NET keys label only); `elementdefinition-suppress` honored **always** (no setting), incl. label `"$all"` = drop all inherited examples | PU:2798-2827 |
| `maxLength`, `minValue[x]`, `maxValue[x]` | replace, no narrowing check (= .NET) | PU:2829-2854 |
| `condition` | union (id value) (= .NET) | PU:3099 |
| `mustSupport` | aggregated with obligation profiles (any true wins); **ERROR on true→false** unless `fromSlicer`; diff still wins | PU:2859-2880 |
| `mustHaveValue` | replace; ERROR on true→false unless `fromSlicer` | PU:2882 |
| `valueAlternatives` | union (canonical value) (= .NET) | PU:2893 |
| `isModifier`/`isModifierReason` | **frozen — diff silently ignored — except on extension elements** (the `checkExtensionDoco` gate); modifier extensions get a boilerplate reason auto-filled. Contrast .NET: replace | PU:2906-2927 |
| `binding` | rebuild + enforcement, see below | PU:2929-3037 |
| `isSummary` | change with a base value present → **`throw new Error` — generation aborts** (hardest enforcement anywhere in either engine; .NET silently replaces) | PU:3039-3048 |
| `type` | wholesale replace (= .NET direction, [DEV-001](13-deviation-register.md#dev-001--type-list-replace-vs-merge-ch5) agreement) after per-type `checkTypeDerivation`: unknown code vs base → **DefinitionException**; targetProfiles must trace to a base targetProfile (via base-chain or imposeProfile; specialization exempt) else ERROR. Matched base type's `type-must-support` (if new) + `pattern`/obligation extensions copied down; otherwise the diff item is taken verbatim (inherited per-code profiles NOT preserved) | PU:3053-3080, 3262-3315 |
| `mapping` | `MappingAssistant.merge()`, see below | PU:3082, MA |
| `constraint` | inherited constraints stamped `source` = **base SD url** (when absent) *before* the diff is appended; diff constraints appended **only when the key is new — a restated inherited key is silently dropped** (in-code: "constraints are cumulative. there is no replacing"). The Java answer to [RFC-009](15-spec-rfcs.md#rfc-009--eld-14-vs-additive-constraints-restating-an-inherited-constraint-key); three-way contrast with .NET in [DEV-002](13-deviation-register.md#dev-002--constraintsource-population-ch5) | PU:3084-3098 |
| `extension` (on the ED itself) | policy-list machine (PU:232-302): 15-url non-inherited purge list, 5-url inherit-unless-redeclared list, ~14-url diff-ignored list, ~20-url override-in-place list; **any other url present on both sides is appended as a duplicate**. Contrast .NET: uniform union-by-url overlay | PU:3199-3217 |

### Binding (PU:2929-3037)

When the diff (or an obligation profile) has a binding and it differs from the base's:

- **Enforcement** (the only §5.1.0.21 lattice row either engine enforces): base strength `required` + diff
  strength weaker → ERROR message. Both `required` with valueSets → full terminology **subset check**: expand
  both (WARNINGs when unresolvable/unexpandable/too big), then every derived code is validated against the
  base VS; failures → ERROR "not a subset" (PU:2960-2999). Other strength changes pass silently.
- **Merge = rebuild**, not overlay: `nb = base.binding.copy()`; extensions **cleared**
  (`COPY_BINDING_EXTENSIONS = false`, PU:428) and replaced by the diff's; **description reset to null** and
  taken from the diff only if present — an inherited `binding.description` is *dropped* whenever a diff
  constrains the binding without restating it; strength/valueSet: diff wins else inherited. `additional`
  merged by (valueSet, purpose, diff-has-no-usage) via `mergeAdditionalBinding` (PU:3219: usage union,
  shortDoco/documentation replace; the `any` flag is a self-assignment no-op — upstream candidate).
- Diff with no binding: base binding kept, minus non-inherited extensions (PU:3032-3036).
- Post-merge (both paths): binding **deleted** when no remaining type is bindable (task 8477) — Java's
  bindable = hardcoded {Coding, CodeableConcept, Quantity, uri, string, code, CodeableReference} +
  `binding-style`/`type-characteristics can-bind` extensions (PU:3362-3377); .NET's = `ICoded` types. Same
  rule, independently maintained type lists ([RFC-010](15-spec-rfcs.md#rfc-010--binding-merge-granularity-unstated)
  data point; the "two lists" theme of OQ-019/DEV-022).

### Mapping — `MappingAssistant` ([DEV-017](13-deviation-register.md#dev-017--mapping-matched-on-identitymap-vs-r5-replace-by-identity-ch5) enrichment)

One instance per generation (PU:837), configured by `mappingMergeMode` (default **APPEND**) and a
`suppressedMappings` uri list — surfaces .NET doesn't have. Three jobs:

1. **SD-level reconciliation** (MA:45-96): base SD `StructureDefinition.mapping` declarations are folded into
   the derived SD's (match by URI first, else identity+name); identity collisions are renamed with numeric
   suffixes and recorded in a `renames` table. (.NET never touches SD-level mappings.)
2. **Element-level merge** (MA:173-213, called PU:3082): result = diff mappings first, then inherited ones
   appended unless matched. Match = identity + map-text equal. **On R5+ only**, same identity with different
   map text triggers the merge mode: APPEND (default) **comma-appends** the inherited map text into the diff
   mapping — one mapping per identity; DUPLICATE keeps both; IGNORE/OVERWRITE pick one. **On R4 and earlier
   the fallthrough keeps both items — exactly .NET's identity+map union**, so DEV-017 is an R5+-only
   divergence. Suppression via the `suppressed` extension on SD-level declarations (always on) — vs .NET's
   setting-gated per-item `elementdefinition-suppress`. Caveat: the `renames` table is applied to the *diff's*
   mappings rather than the inherited ones (MA:175-177) — looks inverted; parked as a needs-verification
   upstream candidate.
3. **Post-generation pruning** (`update()`, MA:150-170): SD-level mapping declarations not referenced by any
   snapshot element (and not declared by the derived SD itself) are removed; map texts trimmed.

### The preprocessor's additional-base merge table (PRE:399-531)

When a profile declares `EXT_ADDITIONAL_BASE` extensions (Java-only, DEV-032), each additional base's
*differential* is recursively preprocessed and merged into the profile's differential before path processing —
a third merge table (differential × differential, both constraint sets on one type), distinct from both this
chapter's base×diff table and the slice-propagation fill-if-absent table
([DEV-025](13-deviation-register.md), [OQ-021](14-open-questions.md)). Semantics: profile side wins
fill-if-absent for descriptive properties and flags (`chooseProp`, no conflict detection); cardinality/value
bounds nominally intersect — but **min takes the *lower*** (looser; arguably inverted, PRE:421) and
**maxLength the larger** (looser, PRE:466) while max/minValue/maxValue correctly take the stricter;
`alias`/`code`/`example`/`constraint`/`mapping` union; `valueAlternatives` and `type` **intersect** (type by
working code, with a specialization-aware single-profile merge and a joint-profile search; empty intersection
throws); fixed×fixed must be equal (throw), fixed×pattern checked for compatibility, **pattern×pattern hits an
operand bug** (passes the `getFixed()` values, PRE:450); **binding×binding = `throw new Error("not done
yet")`**. Incompatibilities throw `FHIRException` — this pathway is fail-fast where the main merge is
warn-and-continue.

**Empirical verification (Phase 4 packet 3, 2026-08-26):** the full-sweep min/mustSupport mining found
**zero** per-property merge-semantics divergences for `min` and `mustSupport` (all 131 diffs traced to
Java-side *mechanisms* — slice-content propagation, entry-min arithmetic, additionalBase, obligations —
never to the merge routine itself; bucket B empty by evidence). Where a slice-child differential exists,
both engines merge it identically. One passing observation for this chapter's Java table: in Java's
preprocessor propagation pathway, `example` **appends** rather than replaces (entry-child example placed
ahead of the slice's authored one — ILCorePractitioner). Fail-test evidence for this chapter's absent
checks (fixed/pattern type-compat, type/targetProfile derivation, mustSupport direction): DEV-028
groups (d), (e), (h).

## Deviations
- [DEV-034](13-deviation-register.md#dev-034--per-property-merge-divergence-catalogue-net-elementdefnmerger-vs-java-updatefromdefinition-ch5)
  **per-property merge divergence catalogue** — the consolidated .NET↔Java delta list for this chapter
  (frozen-by-omission set, isSummary abort, isModifier gate, restated-constraint-key drop, binding rebuild,
  extension policy lists, example key, label append, min/max illegal-input outputs).
- [DEV-001](13-deviation-register.md#dev-001--type-list-replace-vs-merge-ch5) type list semantics.
- [DEV-028](13-deviation-register.md#dev-028--author-error-detection-catalogue-java-validates-net-emits-as-written-ch2ch6-ch9-ch12)
  groups (d)/(e)/(h) — merge-time validation Java performs and .NET doesn't.
- [DEV-002](13-deviation-register.md#dev-002--constraintsource-population-ch5) constraint.source.
- [DEV-017](13-deviation-register.md#dev-017--mapping-matched-on-identitymap-vs-r5-replace-by-identity-ch5)
  mapping keyed on identity+map vs R5 replace-by-identity (R5+-only divergence, see Java section).

## Open questions
- [OQ-001](14-open-questions.md#oq-001--the-cardinality-diamond-problem) cardinality diamond problem.
- [OQ-008](14-open-questions.md#oq-008--verbosity-of-generated-snapshots) suppression vs verbose snapshots.
- [OQ-010](14-open-questions.md#oq-010--the--append-convention) the `"..."` append convention.
- [OQ-011](14-open-questions.md#oq-011--what-must-a-generator-enforce) what must a generator enforce?
- [OQ-012](14-open-questions.md#oq-012--partial-overlay-of-fixedxpatternx-values) partial overlay of
  fixed/pattern values.
