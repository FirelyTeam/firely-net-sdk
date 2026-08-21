# 4. Element matching

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

## Scope
Pairing each differential element with its base-snapshot counterpart, one tree level at a time: exact path
matching, choice-type matching, the decision space per pair (merge in place / add as slice / open a slice
group / introduce a new element / remove), and ordering requirements. Slicing-specific matching detail
lives in [chapter 6](06-slicing.md).

## Spec baseline (R5)

**There is no element-matching algorithm in the spec.** What exists is raw material the algorithm must be
consistent with:

### Identity: path, id, sliceName

> "The path element is the most important property of the element definition. It both names the element,
> and locates the element within a hierarchy … Within the FHIR specification, there is only one original
> definition for each path." [elementdefinition #path]

- Constraint SDs "are not allowed to define or include ElementDefinitions with a path not defined within
  the base type definition from which they derive" [elementdefinition #path] — an unmatched differential
  path is an **error**, never a new element (contrast specializations/logical models).
- Element ids are deterministic from path + sliceNames (`pathpart:slicename/reslicename` per token,
  [elementdefinition #id]; see ch10) — so id-based and path+sliceName-based matching are formally
  equivalent when ids follow the algorithm.
- `sliceName` is "required for slices, else prohibited" in constraint differentials
  [elementdefinition #interpretation], and unique within the structure per sliced element (eld-16 grammar).
- `sliceIsConstraining` (Trial Use) *implies* the cross-profile matching rule: "If set to true, an ancestor
  profile SHALL have a slicing definition with this name" [elementdefinition-definitions] — i.e. derived
  slices match ancestor slices **by sliceName**. That is the closest the spec comes to stating a matching
  rule.

### Ordering as a matching aid

Differential elements "must be in the same order as the baseDefinition, children depth-first; unsliced
descendants of sliced elements appear before slices" [structuredefinition §5.4.6] — so a conforming
differential can be matched in a single forward walk of the base. Slice-group adjacency: entries "must be
adjacent … or, if there are any intervening elements, those elements must be 'compatible with' the group"
(paths starting with the group's path) [profiling §5.1.0.13]. The slice-set boundary: "The set of slices is
any elements that come after this in the element sequence that have the same path, until a shorter path
occurs" [elementdefinition-definitions, slicing].

### Choice-type (`value[x]`) matching

R5 rules [elementdefinition #typesx], verbatim core:

> "- Constraints limiting the acceptable list of types must be applied to the original '[x]' element …
> - The inclusion of a type specific element (such as 'Patient.deceased[x]:deceasedBoolean') SHALL NOT be
>   interpreted as constraining allowed types, but instead, it constrains the use of a particular type
> - the original element SHALL always be represented in a snapshot; the type specific variants are only
>   represented when needed"

So in R5, a type-specific constraint is a **type slice**: path stays `…[x]`, sliceName is the rendered
type name, id is `…[x]:valueBoolean`-style. Renamed paths (`Patient.deceasedBoolean`) are neither
sanctioned nor prohibited in R5 (spec gap; see
[RFC-013](15-spec-rfcs.md#rfc-013--sanction-or-prohibit-renamed-choice-type-paths-in-snapshots)).

### The decision space (implied, not specified)

From the rules above, a matcher confronts per differential child: same path & no slicing → constrain in
place; sliceName present → match base slice by name, else new slice; slicing component present → the
slicing entry; renamed/type-sliced choice → the `[x]` element; unmatched path → error (constraints) or new
element (specializations/logical models). None of this taxonomy is spec text — it is what both
implementations independently derived (Phases 2–3 document each).

### Spec gaps (matching)

1. No matching algorithm; no statement on resolving ambiguity (e.g. duplicate paths without sliceNames).
2. Matching semantics for a differential that constrains "all slices of X" (no sliceName on a sliced
   element, sanctioned by [profiling §5.1.0.17]) — how it distributes over base slices is unstated.
3. Whether matching may rely on ids when they deviate from the deterministic algorithm — unstated.

## R4/R4B deltas

- **Headline (same as ch2):** R4 prescribed *renamed paths* for single-type choice constraints; R5 requires
  the `[x]`-path type-slice form. R4 id-algorithm example even used `path = Patient.deceasedBoolean`
  [R4 ed §2.30.0.3]. Matchers must accept both, per version.
- `sliceIsConstraining` exists in R4 4.0.1 already (verified) — no delta.
- Slice-group adjacency, boundary rule, ordering: identical.

## .NET behavior (Phase 2)
*(pending — `ElementMatcher.cs`: `Match`, `matchBase`, `constructMatch`, `constructChoiceTypeMatch`,
MatchAction = Merge/Add/Slice/New/Remove/Invalid)*

## Java behavior (Phase 3)
*(pending — `ProfilePathProcessor.java`)*

## Open questions
- [OQ-006](14-open-questions.md#oq-006--sliceisconstraining) sliceIsConstraining in matching.
