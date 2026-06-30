## Intro:

This is a bugfix release with several stability and correctness improvements.

**Serialization**
- Fixed an `OverflowException` when serializing large decimal values with old parsing stack
- Switched `FhirClient` to use the modern POCO serializer for better consistency
- Fixed size limitations when serializing base64-encoded data
- Fixed the `pretty` option being ignored when calling `ToXml()`

**Snapshot generation**
- Fixed incorrect behavior in collection merge when suppression extensions were involved, which could cause an index-out-of-range error or suppression being silently ignored

**POCO / type system**
- `ISourceNode` now has a direct `ToPoco()` extension method
- Fixed `instant` values being stored incorrectly when converting from an untyped source
- Untyped sources are now correctly associated with the model when building POCOs
- Custom resources and datatypes are now easier to define and build as a `ClassMapping` for `ModelInspector`
- Restored collection initializer syntax support on the `Parameters` class
- Added `IVersionableConformanceResource` to R5 `NamingSystem`

**Other fixes**
- Resolver base class now allows returning `null`, leaving it to the caller to decide whether to throw
- Dependencies downgraded to align with the target framework version
