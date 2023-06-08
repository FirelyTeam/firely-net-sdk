# InterfacesAppliedToGeneratedClasses
Include interfaces having to be applied to generated classes during the build process in this directory.

Considerations:
- Use file scoped namespaces to gurantee compatibility with `netstandard2.0`.
- Always use namespace `Hl7.Fhir.Model`.
- Mark interface with attribute `[ApplyInterfaceToGeneratedClasses]`, which is being used for discovery.
- Extension methods can be included in the same file as the interface definition.