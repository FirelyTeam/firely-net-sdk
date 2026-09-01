# 12. Error handling & configuration

> Status: **spec baseline + .NET behavior filled** (Phase 1: R5 v5.0.0 + R4 v4.0.1 deltas; Phase 2 packet 6,
> 2026-08-26: issue catalog, settings, events deep-read; Phase 3 packet J-e, 2026-09-01: Java sweep).

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

## .NET behavior (Phase 2, deep-read 2026-08-26)

### Public API and outcome model

Four public operations: `UpdateAsync` (generate + assign snapshot), `GenerateAsync` (generate, return
element list, leave input's snapshot untouched), `ExpandElementAsync` (3 overloads — on-demand expansion of
one element's children), `MergeElementDefinition` (single-pair merge)
(`SnapshotGenerator.cs:144-295`). Reported problems accumulate in a single `OperationOutcome`
(`SnapshotGeneratorOutcome.cs:34-58`), **cleared at the start of each public call** and shared across all
recursive external-profile expansions of that call (`:32` — child issues land in the caller's outcome).
`Outcome` is `null` iff no issues. Per issue: location = element id if present, else `path[:sliceName]`
(`FormatLocation`, `:60-65`); the profile being processed when the issue arose goes in
`IssueComponent.Diagnostics` (`:55`) — needed because recursive expansion means an issue's subject profile
is often not the requested one.

### Issue catalog (`SnapshotGeneratorOutcome.cs`)

Generator-specific issues (type `Issue.Create(code, severity, type)`) plus shared `Issue.*` codes:

| Code | Name | Severity | Fired when |
|---|---|---|---|
| 10002 | INVALID_TYPEPROFILE_NAMEREF | error | contentReference / `url#name` fragment target not found (ch7/ch8) |
| 10003 | INVALID_SLICE | error | *unreachable* — only fired under disabled `REJECT_SLICE_NONREPEATING_ELEMENT` (ch6) |
| 10004 | MISSING_SLICE_ENTRY | error | slice group without entry (non-extension; ch6 — likely unreachable via current matcher) |
| 10006 | INVALID_EXTENSION_DISCRIMINATOR | error | extension slicing discriminator ≠ `value:url` (ch4; matching proceeds on url anyway) |
| 10007 | TYPESLICE_WITHOUT_TYPE | error | `@type`-sliced element without type (ch4, `Invalid` match) |
| 10008 | INVALID_SLICE_WITHOUT_NAME | error | unnamed slice outside extension/type slicing (ch4, `Invalid` match) |
| 10009 | INVALID_PROFILE_TYPE | error | type profile target incompatible with element type (ch7, issue + continue) |
| 10010 | INVALID_SLICENAME_ON_ROOT | error | root `sliceName` repaired (ch2; suppressed for the STU3 SimpleQuantity core bug) |
| 10012 | INVALID_CHOICETYPE_NAME | error | diff uses `[x]` name where base renamed the element (ch4, New + warning) |
| 10012 | SLICENAME_NOMATCH | error | `sliceIsConstraining = true` without matching base slice (ch4, `Invalid`) — **duplicate code** |
| 10013 | SLICENAME_CONFLICT | error | `sliceIsConstraining = false` but base has that slice (ch4, `Invalid`) |
| 10014 | INVALID_CHOICE_RENAME | error | renamed choice element for a type the base doesn't allow (ch6) |
| 10014 | STRUCTURE_TYPE_MISSING | warning | logical model without `type` — root parsed from first diff element (ch9) — **duplicate code** |
| 10015 | INVALID_COMPLEX_REFERENCE | error | `url#name` fragment ≠ slice name of constrained child (ch7, DEV-018) |
| 10016 | SLICENAME_GENERATED | information | generated missing type-slice name (ch4, OQ-015) |
| 10017 | BASE_TYPE_UNRESOLVED | warning | logical model deriving from unresolvable `Base` (#3576, ch3/ch9) |
| — | `Issue.UNAVAILABLE_REFERENCED_PROFILE` | — | unresolvable profile reference (fatal for the *own* base at top level, `:400-402`; issue + continue elsewhere) |
| — | `Issue.UNAVAILABLE_NEED_SNAPSHOT` | — | external profile has no snapshot and on-demand generation is off/failed |
| — | `Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED` | — | recursive generation returned nothing |
| — | `Issue.UNAVAILABLE_NEED_DIFFERENTIAL` | — | external profile has no differential (root resolution, OQ-016) |
| — | `Issue.PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF` | — | element with neither type nor contentReference (non-logical, ch9 exempts logical) |

Code-archaeology notes: codes **10012 and 10014 are each assigned twice** (`:91/:319`, `:348/:410`) —
consumers filtering by code conflate unrelated conditions (filed as
[#3587](https://github.com/FirelyTeam/firely-net-sdk/issues/3587)); 10000/10001/10005/10011 are
retired/disabled (dead code); 10011 (`INVALID_SLICENAME_ON_SPECIALIZATION`) sits behind the never-defined
`FIX_SLICENAMES_ON_SPECIALIZATIONS`.

### Throw catalogue

Alongside report-and-continue, these conditions **throw** (the full taxonomy question is
[OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors)): SD without `url`
(`:171-174`); constraint SD without `baseDefinition` (`:372-375`); missing `type` on a non-logical SD
(`:490-493`) or underivable root path on a logical one (`:468-472`); preprocessing errors (ch2 — element
without path, root not first); illegal choice-type widening (ch4); renamed-path merge onto a non-choice
base (ch5/ch6); recursive profile dependency (`NotSupportedException`, ch11); plus internal-state errors
(bookmark failures, slice-insert failures, recursion-stack misuse) which indicate generator bugs rather
than input errors.

### Settings (`SnapshotGeneratorSettings`, `Hl7.Fhir.Shims.Base`)

Six flags — the SDK's whole configuration surface, all implementation-defined (the spec has no concept of
generator configuration):

| Setting | Default | Effect |
|---|---|---|
| `GenerateSnapshotForExternalProfiles` | `true` | generate missing snapshots of referenced profiles on demand (`ensureSnapshot`, ch11); off → external profiles without snapshots produce `UNAVAILABLE_NEED_SNAPSHOT` and their merge is skipped |
| `ForceRegenerateSnapshots` | `false` | regenerate even pre-existing snapshots of *all* referenced profiles (once per run, via the `CreatedBySnapshotGenerator` annotation; effective only with a caching resolver) |
| `GenerateExtensionsOnConstraints` | `false` | stamp diff-constrained elements/properties with the `CONSTRAINED_BY_DIFF_EXT` extension (persisted; see below) |
| `GenerateAnnotationsOnConstraints` | `false` | same signal as in-memory annotation (ephemeral) |
| `GenerateElementIds` | `true` | (re-)generate element ids — canonically, always, discarding author ids (ch10) |
| `RespectSuppressExtension` | `true` | honor `elementdefinition-suppress` on inherited mappings/examples (ch5, OQ-008) |

A seventh, `MergeTypeProfiles`, was removed in 2016 — type-profile merging is unconditionally on
(`SnapshotGeneratorSettings.cs:86-94`, `SnapshotGenerator.cs:1042-1043`); the `NormalizeElementBase`
setting referenced by a stale comment (`SnapshotBaseComponentGenerator.cs:22`) no longer exists either.

### Events

Four events (`SnapshotGeneratorEvents.cs`) — three of which can **alter generation**, making them part of
the observable behavior surface: `PrepareElement` (fires per element before diff merge; handler may modify
the element; also drives Forge-style base-element tracking) and `Constraint` (fires per diff-constrained
element; may modify) are read-write hooks; `BeforeExpandElement` changes expansion coverage (ch11);
`PrepareBaseProfile` is notification-only but hands out the *original cached* base profile instance with an
explicit doc warning that modifying it corrupts the shared artifact cache (`:48-53`).

### Extension/annotation hygiene

Inherited snapshot content is scrubbed of **non-inheritable extensions** — a hard-coded blocklist of 17
core extension urls (`structuredefinition-fmm`, `-standards-status`, `-wg`, `-interface`,
`-normative-version`, `resource-approvalDate` etc., plus the generator's own marker;
`SnapshotGeneratorExtensions.cs:137-156`), applied when cloning the base snapshot (`:523`) and when
copying type-profile children (`:1578`). This is metadata-inheritance policy the spec never mentions:
without it, every derived profile's snapshot would claim its base's maturity/WG/normative status
([OQ-019](14-open-questions.md#oq-019--which-extensions-are-non-inheritable)). Note the generator's own
marker extension uses the canonical `http://hl7.org/fhir/StructureDefinition/constrainedByDifferentialExtension`
— an hl7.org url that **is not a registered HL7 extension** (a Firely-invented url in HL7's namespace;
HTTP 404 verified 2026-08-26, filed as
[#3588](https://github.com/FirelyTeam/firely-net-sdk/issues/3588)).
Ephemeral bookkeeping uses in-memory annotations instead (`SnapshotGeneratorAnnotations.cs`):
`CreatedBySnapshotGenerator` (idempotence gates, ch10/ch11), `ConstrainedByDiff`, `AppendedText` (the "..."
convention, OQ-010), and the internal snapshot-root cache (ch11); all generator annotations are removed
from inherited content before reuse (`:524`).

### Concurrency

One generation at a time per instance: the recursion stack throws on overlapping calls (ch11). The
`Outcome` property is likewise per-instance shared state.

## Java behavior (Phase 3 sweep, 2026-09-01)

Citations `PU`/`PPP`/`PRE` @ `b06c7ee`; detail in the materials extract `java-ch08-12-sweep-2026-09-01.md`.

### Outcome model

`ProfileUtilities` is constructed with an **injected `List<ValidationMessage>`** (`PU:456-491`); passing
`null` switches the instance to **throw mode** (`wantThrowExceptions`, toggled by `setThrowException` /
`setMessages`, `PU:4729`, `4990-4994`): every ERROR-severity message becomes a `DefinitionException` at
`addMessage` (`PU:1221-1226`). The shared-test driver runs in message mode (`setThrowException(false)`).
Two messages bypass the gate (`ATTEMPT_TO_CHANGE_SLICING`, `PPP:351/355`, added straight to the list) and
never throw. Messages are tagged `Source.ProfileValidator` (23 sites) or `Source.InstanceValidator` (5) with
no evident rule; severities in the cone: 17 ERROR, 10 WARNING, 3 INFORMATION; three ERRORs are
`setIgnorableError(true)` (slice-min-sum, slice-without-slicing, duplicate slice name); `forPublication`
flips the slice-min-sum severity INFORMATION → ERROR (`PU:1003`). Locations are index strings
(`StructureDefinition.snapshot.element[i]`) or paths. Messages are also attached to the SD as user data
(`SNAPSHOT_GENERATED_MESSAGES`) and the SD is flagged `generatedSnapshot` (`PU:1094-1096`).

### Throw census

`grep "throw new"` over PU/PPP/PRE: `DefinitionException` 37 + 31 + 2, `FHIRException` 23 + 25 + 20, and
**`java.lang.Error` 17 + 9 + 11**. Author-reachable `Error`s: slicing entry with children on a base that has
children (`PPP:382`, "please report issue to grahame@fhir.org"), two profiles on a new slice (`PPP:1419`),
`type` on the first snapshot/differential element (`PU:883`, `1322`), element path not under the type name
(`PU:1021`), choice-group duplicate child names (`PU:1353`, `"huh?"`) and two mandatory members (`PU:1362`),
unresolved contentReference in `getTypeForElement` (`PU:1616`), the preprocessor's "not done yet" cases
(ch5). Because only `Exception` is caught at `PU:1078`, an `Error` **skips the snapshot-nulling** cleanup and
leaves a half-built snapshot on the SD (the `finally` still resets the flag and stack). Java has no
report-and-continue class for these — [OQ-014](14-open-questions.md#oq-014--inconsistent-error-taxonomy-for-author-errors).

### Settings inventory

Instance fields (`PU:428-454` + setters), none of which the spec knows about:

| Setting | Default | Effect |
|---|---|---|
| `newSlicingProcessing` | false (driver: **true**) | R4+ type-slicing synthetic slicer untyped vs typed (`PPP:504/1504`, ch6) |
| `autoFixSliceNames` | false (validator CLI: **true**, DEV-016) | generate a missing type-slice name instead of erroring — a single site, `PPP:565` |
| `allowUnknownProfile` | `ALL_TYPES` (comment says `NONE`, hapifhir#2597) | throw vs warn on unresolvable type/extension profiles (ch7) |
| `wantFixDifferentialFirstElementType` | false | clear a root `type` equal to the base's instead of throwing (`PU:1319`, OQ-015) |
| `mappingMergeMode` | `APPEND` | R5+ mapping merge (ch5, DEV-017) |
| `suppressedMappings` | empty | mapping identities dropped (ch5) |
| `forPublication` | false | severity flip above |
| `wantThrowExceptions` | derived from `messages == null` | throw mode |
| `parameters` | context expansion parameters | `default/force/check-profile-version` in `findProfile` (ch3) |
| `debug` | false | extra logging; surfaces `sortDifferential` errors (`PU:3916`, JI-16) |
| `masterSourceFileNames` / `localFileNames` / `defWebRoot` | — | markdown relative-link rewriting (`updateURLs`, ch3) |
| `xver` | null until `makeXVer()`/`setXver` | cross-version extension synthesis (ch7, JI-19) |
| `terminologyServiceOptions`, `pkp`, `fpe` | — | binding/link rendering knowledge; FHIRPath engine for choice groups (ch9) |

Statics: `suppressIgnorableExceptions` (two sites — `PPP:680` Reference type-constraint check, `PU:3312`
illegal type derivation — turn throws into silence), `COPY_BINDING_EXTENSIONS = false`, `DONT_DO_THIS = false`,
`APPLY_PROPERTIES_FROM_SLICER = false` (PPP, ch6), `XVerExtensionManagerFactory.newLoader = false`. **No
events or callbacks**; `ProfileKnowledgeProvider` is the only injection point and serves rendering. Contrast
.NET: six flags, four events, one shared `OperationOutcome`.

### Extension policy — four lists plus a per-extension policy

Java's inheritance policy is spread over four static url lists (`PU:232-302`) and one extension-defined
policy: `NON_INHERITED_ED_URLS` (15 urls stripped from inherited content — but only on the **copy-through
path**, `checkExtensions` at `PPP:1066`, and at SD level `PU:1230` / specialization inheritance `PU:1276`),
`DEFAULT_INHERITED_ED_URLS` (5 questionnaire-* + mimeType), `NON_OVERRIDING_ED_URLS` (15 json/xml-naming,
type-specifier, xml-choice-group, translatable, defaulttype — ignored when found in differentials),
`OVERRIDING_ED_URLS` (21 questionnaire-*/maxSize/minLength/allowedUnits/… — replace the ancestor's; ch5
DEV-034), and the per-extension-SD `snapshot-behavior` extension (`ignore`/`add`/`overwrite`/defer,
`PU:1255-1261`, ch3). Against .NET's single 18-entry blocklist the **non-inheritable sets differ**: common —
isCommonBinding, fmm, standards-status, category, security-category, wg, normative-version, summary; Java-only
— tools/binding-definition, tools/no-binding, implements, explicit-type-name, obligation-profile (core +
tools), standards-status-reason; .NET-only — fmm-no-warnings, hierarchy, **interface**, applicable-version,
codegen-super, replaces, resource-approvalDate/-effectivePeriod/-lastReviewDate, plus .NET's own marker
([OQ-019](14-open-questions.md#oq-019--which-extensions-are-non-inheritable)). Java has no persisted
"constrained by differential" marker; provenance is user data (`SNAPSHOT_GENERATED_IN_SNAPSHOT`,
`SNAPSHOT_DERIVATION_POINTER/EQUALS/DIFF`, `SNAPSHOT_BASE_MODEL/PATH`, `SNAPSHOT_IS_DERIVED`,
`SNAPSHOT_auto_added_slicing`) plus the persisted `snapshot-base-version` extension (`PU:1091-1093`).

### Concurrency

Per-instance mutable state (`snapshotStack`, `messages`, `obligationProfiles` — never cleared between calls —,
`childMapCache`) and a `generatingSnapshot` flag on the shared SD object; no overlapping-call guard (ch11).
A hard-coded Nictiz-url debug probe survives in production (`debugCheck`, `PPP:1488-1492`).
