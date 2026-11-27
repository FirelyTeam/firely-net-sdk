## Intro:

This release adds flat and not-for-UI value set expansions, the `lenient-display-validation` parameter for
`$validate-code`, and more ways to build a `DateTimeRange`. It also makes `IsValidValue` stricter about FHIR
date/time and primitive literals, and fixes several parsing and `base64Binary` issues. There are no breaking API
changes.

**Date and time ranges**
- `DateTimeRange` can be built from two instants and a precision with a public constructor, and
  `TryBetween(start, end, out range)` reports a reversed pair, or two `null` arguments, instead of throwing. `StartOrMinValue`
  and `EndOrMaxValue` give the bounds with `DateTimeOffset.MinValue`/`MaxValue` for an unbounded side,
  for code that compares instants, and `LastInstant` is the last instant the range covers, one tick
  before `End`. See issue [#3657](https://github.com/FirelyTeam/firely-net-sdk/issues/3657).

**Terminology**
- `ValueSetExpander.ExpandAsync(ValueSet, ExpandParameters)` and a public `SupportedParameters`. `excludeNested=true`
  (or the new `ValueSetExpanderSettings.AlwaysExpandFlat`) returns a flat, depth-first expansion, and `excludeNotForUI=true`
  leaves out not-selectable and abstract codes. Unsupported parameters are rejected; applied ones are echoed in
  `expansion.parameter`. The size cap now counts nested codes, intersections keep nested codes, and `Total` is recomputed
  after excludes. The `ExpandParameters` builders for `system-version`, `check-system-version` and `force-system-version` now
  use the OperationDefinition names. See issue [#3632](https://github.com/FirelyTeam/firely-net-sdk/issues/3632).
- `$validate-code` supports the `lenient-display-validation` parameter of the
  [ValueSet/$validate-code OperationDefinition](https://build.fhir.org/valueset-operation-validate-code.html).
  `ValidateCodeParameters` and `CodeSystemValidateCodeParameters` have a `LenientDisplayValidation` property and a
  `WithLenientDisplayValidation()` builder method. When it is `false`, `LocalTerminologyService` (ValueSet `$validate-code`)
  reports a display that does not match the code as an `invalid-display` error and the code is not valid. Terminology services
  that call a terminology server send the parameter along. When it is `true` or absent, a wrong display stays
  a warning and the code stays valid, as before.
- `ValidateCodeResult.AddInvalidDisplay(details, lenient, expression)` adds the `invalid-display` issue as a warning or an error.
  The overload without `lenient` is obsolete. See issue [#3640](https://github.com/FirelyTeam/firely-net-sdk/issues/3640).

  > **Upcoming change:** in the next major version, a missing `lenient-display-validation` parameter will mean strict
  > validation, so a wrong display will be an error by default. The OperationDefinition says: "If the
  > 'lenient-display-validation' parameter is false or absent, then an invalid display will cause the 'result' output
  > parameter to be 'false', i.e. the validation will fail." Pass `true` to keep it a warning.

**Blazor WebAssembly**
- `TaskHelper.Await` now runs the delegate inline on the browser and WASI platforms instead of wrapping it in `Task.Run`, which cannot be blocked on in single-threaded WebAssembly ("Cannot wait on monitors on this runtime"). Resolvers that return an already completed task now work there; a task that is not complete throws a `PlatformNotSupportedException`. Other platforms are unchanged.

**Performance**
- Reading annotations from a POCO or an `AnnotationList` (`HasAnnotation`, `TryGetAnnotation`, `Annotations(type)`, `RemoveAnnotations`) no longer creates an empty annotation list, so `DeepCopy()`/`CopyTo()` no longer propagate one from elements that were only inspected. The list (and its dictionary) is now created on the first write only, and the dictionary is sized for a few annotation types. This removes a `ConcurrentDictionary` allocation per element that was only inspected, which speeds up work like snapshot generation. The public API is unchanged. See issue [#3665](https://github.com/FirelyTeam/firely-net-sdk/issues/3665).
- FHIRPath `as`/`is` and `ResourceIdentity` do less work per call.

**Fixes**
- `FhirDateTime.IsValidValue` and `Time.IsValidValue` now require seconds, as the FHIR `dateTime` and `time` formats do.
  Parsing and deserialization still accept a time without seconds. See issue [#3650](https://github.com/FirelyTeam/firely-net-sdk/issues/3650).
- `IsValidValue` and the element-model parsers for date/time, `id`, `code`, `oid`, `uuid` and `Quantity` no longer accept a
  trailing line break. See issue [#3659](https://github.com/FirelyTeam/firely-net-sdk/issues/3659).
- Validating a `Base64Binary` no longer rewrites a literal that contains whitespace. See issue [#3648](https://github.com/FirelyTeam/firely-net-sdk/issues/3648).
- The JSON deserializer merges `x` and `_x` when only one of them is an array, instead of throwing or dropping elements, and
  reports the new non-fatal `JSON136`. Property-level validation errors are now reported at the element itself. See issue
  [#3655](https://github.com/FirelyTeam/firely-net-sdk/issues/3655).
- Building a POCO from an `ISourceNode` (`ToPoco()`) stored a `base64Binary` value as a decoded `byte[]` in `JsonValue` instead of keeping the base64 text, so validation reported an incorrect literal type (e.g. on `Binary.data`), and an invalid base64 literal threw a `FormatException` instead of being reported by validation. The literal is now kept as-is. This was broken since 6.2.1.
- Reading a `Code<T>`, `FhirDateTime`, `Date`, `Time`, `Instant`, `Integer64` or `Base64Binary` on several threads at once,
  while its `JsonValue` has not been parsed yet (as after deserializing without validation), no longer returns `null` or `false`,
  or throws, for a valid value. See issue [#3688](https://github.com/FirelyTeam/firely-net-sdk/issues/3688).
