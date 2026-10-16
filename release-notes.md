## Intro:

This release makes the FHIRPath conversion functions follow the FHIRPath specification for items
of a complex type: they no longer throw, but return empty or `false`. There are no breaking API
changes.

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
