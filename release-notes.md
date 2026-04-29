## Intro:

Fixes wrong logic for BaseTerminologyService filtering parameters for duplicates on requests that accept 0..* on some parameters

## Fix: OverflowException when parsing large integer values in JSON

`FhirJsonNode.NormalizeValue` previously used `Convert.ToInt32()` unconditionally on all `long` JSON integers, causing an `OverflowException` for values outside the Int32 range (e.g. timestamps like `20231128235900` used as a `valueQuantity.value`). Values that fit in Int32 are still normalized to `int` for correct JSON serialization; values outside the range are now preserved as `long` without raising an error.
