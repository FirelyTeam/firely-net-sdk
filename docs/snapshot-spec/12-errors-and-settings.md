# 12. Error handling & configuration

> Status: **spec baseline filled** (Phase 1, R5 v5.0.0 + R4 v4.0.1 deltas). Implementation sections pending (Phases 2–3).

## Scope
The generator's error philosophy (report-and-continue vs throw), the catalogue of error/warning conditions,
and the configuration surface both implementations expose — every setting is a fork in observable behavior
a spec must pin down or explicitly leave open.

## Spec baseline (R5)

**The spec prescribes no tool behavior for any error condition.** It acknowledges unsatisfiable inputs —
"it is possible for Profile C to make rules that are incompatible with profile B, in which case there is no
set of instances that can be valid against profile C" [profiling §5.1.0.17] — without saying what a
generator should do, and concedes some of its own rules are unenforceable ("others (e.g. alignment of
changes to descriptive text) cannot be automatically enforced" [profiling §5.1.0.9]).

### Input-error catalogue (conditions a generator can detect, consolidated from the corpus)

Structure-level: no differential and no snapshot (sdf-6); non-abstract without baseDefinition (sdf-4);
baseDefinition without derivation (sdf-27); constraint on an abstract `type` [structuredefinition §5.4.5];
extension constraint without `context` (sdf-5); `contextInvariant` on a non-extension (sdf-18).

Differential-level: paths outside the SD's type (sdf-8a); paths not defined in the base type
[elementdefinition #path]; root element carrying type/slicing/sliceName/label/code/requirements
(sdf-15a/20/23/9); missing or duplicate element ids (sdf-14/17); `defaultValue` in a constraint (sdf-21);
elements out of base order [structuredefinition §5.4.6 — a rule without an sdf key]; cardinality outside
the base's range [profiling §5.1.0.6]; binding-strength loosening [profiling §5.1.0.21]; isModifier flip
[conformance §2.1.1.0.4]; mustSupport true→false [profiling §5.1.0.22]; regular extension constrained to
modifier [extensibility §2.1.5.0.2]; slice-cardinality violations [profiling §5.1.0.14]; discriminator
overlap ("SHALL ensure … non-overlapping" [profiling §5.1.0.13]); slice name grammar (eld-16); reserved
`@default` misuse [profiling §5.1.0.15].

### Output obligations (what a *valid generated snapshot* must satisfy)

sdf-1 (path uniqueness for non-constraints), sdf-3 (definition/min/max; logical exempt), sdf-8/8b/11/15
(root + base pinning), sdf-14/16 (ids), sdf-10/28 (binding + slicing entry completeness), sdf-24/25
(CodeableReference placement), eld-2..28 as applicable; guideline sdf-26 (no mustSupport on root),
warnings sdf-29, eld-25, eld-27.

### The unaddressed middle: merge-created invalidity

No rule states which invariants must be **re-verified after merging** — e.g. base has `pattern`,
differential narrows the type list to two types → eld-7 violation neither input had; merged mappings
tripping eld-27; inherited ordered slicing without orderMeaning tripping eld-25. Whether the generator
must detect, repair, or ignore such states is unspecified (see ch5, [RFC-012](15-spec-rfcs.md#rfc-012--minimum-normative-statement-of-snapshot-generation-obligations)).

### Configuration

The spec has **no concept of generator configuration**. The single relevant sentence — tools "generate
complete verbose snapshots; they do not support suppressing mappings or constraints"
[profiling §5.1.0.11] — actually *denies* configurability, in tension with the `elementdefinition-suppress`
extension and R5's `constraint.suppress`
([OQ-008](14-open-questions.md#oq-008--verbosity-of-generated-snapshots)). Everything both implementations
expose as settings is implementation-defined.

## R4/R4B deltas

- R5 added invariants that can fire on merged content: eld-23..28, sdf-24..29 (see ch5/ch6).
- eld-1 (R4: slicing needs discriminator or description, applied to *all* ElementDefinitions incl.
  differentials) was retired in favor of sdf-28 (snapshot-only) — R5 differentials are more permissive.
- `constraint.suppress` is new in R5 (R4 had only the suppress *extension* convention).

## R6-build note (v6.0.0-ballot4, fetched 2026-08-21)

New invariants a generator's output meets in R6: **eld-29..34** and **sdf-30** (contentReference only in
specializations). **sdf-9 relaxed**: label and code become legal on root elements (only `requirements`
stays prohibited) — an R5-invalid input can be R6-valid. The `elementdefinition-suppress` extension is now
named on the profiling page as the sanctioned removal mechanism for descriptive properties, softening the
"complete verbose snapshots" stance (see [OQ-008](14-open-questions.md#oq-008--verbosity-of-generated-snapshots)).

## .NET behavior (Phase 2)
*(pending — `SnapshotGeneratorOutcome.cs` (OperationOutcome accumulation, generator reports rather than
throws), `SnapshotGeneratorSettings` (6 flags: GenerateSnapshotForExternalProfiles,
ForceRegenerateSnapshots, GenerateExtensionsOnConstraints, GenerateAnnotationsOnConstraints,
GenerateElementIds, RespectSuppressExtension), events in `SnapshotGeneratorEvents.cs`)*

## Java behavior (Phase 3)
*(pending — exceptions vs messages list, `setAutoFixSliceNames`, `setThrowException`, ProfileUtilities
flags inventory; see [DEV-016](13-deviation-register.md#dev-016--java-oracle-caveat-autofixslicenames))*
