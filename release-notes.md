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
