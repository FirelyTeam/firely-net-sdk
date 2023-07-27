## Intro:
Highlights of this new release:

- The `MultiTerminologyService` has been extended with a routing mechanism, so you can customize which ValueSets should be handled by which service.
- The FhirDateTime, Date and Time types now avoid re-parsing their values for every operation, increasing their performance dramatically.
- Resources now implement `IIdentifiable` and `ICoded` for easy polymorphic access to the identifiers and codes in a resource. See https://docs.fire.ly/projects/Firely-NET-SDK/model/other-features.html.
- Work has been done to use the SDK's POCO's in the upcoming .NET CQL engine.
  
