## Intro:

This release adds a way to get the range of time a FHIR `date` or `dateTime` covers at its
precision, and makes the FHIRPath conversion functions follow the FHIRPath specification for items
of a complex type: they no longer throw, but return empty or `false`. There are no breaking API
changes.

**Date and time ranges**
- New `Interval<T>` and `DateTimeRange` types in `Hl7.Fhir.ElementModel.Types`. `Interval<T>` is a
  generic interval whose sides are closed, open or unbounded, with `Contains`, `Overlaps` and
  `Covers`. `DateTimeRange` is a half-open range [`Start`, `End`) that also keeps the `Precision`.
- `Date` and `DateTime` get `TryGetRange(out range)`, `GetRange(TimeZoneInfo)` and
  `GetRange(TimeSpan)`. For example, `2026-03` covers all of March 2026, and `10:00:00+01:00` covers
  one second. A value without an offset is read in the given time zone, and the range is widened
  where daylight saving skips or repeats a time.
- `Period` gets `TryGetRange(out range)` and `TryGetRange(TimeZoneInfo, out range)`, from the start
  of `start` to the end of `end`. A missing side is unbounded.
- `DateTime` now keeps the number of fraction digits from `Parse()`, so `.000` spans 1 ms. See issue
  [#3628](https://github.com/FirelyTeam/firely-net-sdk/issues/3628).

**FHIRPath**
- The conversion functions `toBoolean()`, `toInteger()`, `toLong()`, `toDecimal()`, `toQuantity()`,
  `toString()`, `toDate()`, `toDateTime()` and `toTime()` now return empty for an item of a type
  they cannot convert, such as a complex type or a resource, and the matching `convertsTo*()`
  functions return `false`, as the [FHIRPath specification](https://hl7.org/fhirpath/STU3/#conversion)
  requires. `Patient.name.first().convertsToString()` used to throw an `InvalidOperationException`
  ("There is no known Cql/FhirPath type corresponding to the .NET type PocoNode") and now returns
  `false`. The same goes for a `Coding` without a `code`, and a `Quantity` without a value or with a
  comparator. Items with a System type equivalent, like `Quantity`, convert as before. See issue [#3636](https://github.com/FirelyTeam/firely-net-sdk/issues/3636).

  > **Behavioural note:** if you added your own overload of one of these functions to a `SymbolTable`
  > that already has the standard functions, with a `PocoNode` parameter, the standard function now
  > takes precedence for complex types too. Register your overload in a child `SymbolTable` to keep
  > it in effect.

**Terminology**
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
