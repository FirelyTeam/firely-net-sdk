|STU3|
|---|---|

## Introduction ##
This is an unofficial WebAPI controller implementation for exposing a [HL7 FHIR][fhir-spec] on the Microsoft .NET (dotnet) platform.
It even supports data compression handling out of the box

This library provides:
* An implementation of an ApiController for the STU3 FHIR specification
* An interface for the System Service
* An interface for the Resource Service
* A partial example implementation of a fhir server CRUD that just writes files to C:\Temp\demoserver
* A unit test project that utilizes the FhirClient NuGet packages to test the example Service

The library depends on several NuGet packages (notably):
* *Core* (NuGet packages starting with `Hl7.Fhir.<version>`) - contains the FhirClient, resource object models and parsers
* *Specification* (NuGet packages starting with `Hl7.Fhir.Specification.<version>`) - functionality to work with the specification metadata and validation
* *FhirPath* (NuGet package `Hl7.FhirPath`) - the FhirPath evaluator, used by the Core and Specification assemblies
* *Support* (NuGet package `Hl7.Fhir.Support`) - a library with interfaces, abstractions and utility methods that are used by the other packages
* *Owin*

**IMPORTANT**
Once things settle in, the HL7.Fhir.WebApi.STU3 project may be created into a NuGet package.
Before installing one of the NuGet packages (or clone the repo) it is important to understand that HL7 has published several updates of the FHIR specification,
each with breaking changes - so you need to ensure you use the version that is right for you:

* [STU3][stu3-spec] (published March 2017) latest release, support in alpha by this library.
* [DSTU2][dstu2-spec] (published October 2015) in widespread use, and is not supported by this library.


## Getting Started ##
To create your own server, copy the Hl7.DemoFileSystemFhirServer example project, then start replacing the code in the
DirectorySystemService and DirectoryResourceService classes.
Depending on your implementation needs, you may have one or more Resource classes.

|Spec version|Git branche|Core NuGet|Specification NuGet|
|---|---|---|---|
|STU3|https://github.com/ewoutkramer/fhir-net-api/tree/develop-stu3|https://www.nuget.org/packages/Hl7.Fhir.STU3/|https://www.nuget.org/packages/Hl7.Fhir.Specification.STU3/|
|DSTU2| https://github.com/ewoutkramer/fhir-net-api/tree/develop|https://www.nuget.org/packages/Hl7.Fhir.DSTU2/ | https://www.nuget.org/packages/Hl7.Fhir.Specification.DSTU2/ |
  
## Support 
TBD 
For questions and broader discussions, we use the .NET FHIR Implementers chat on [Zulip][netapi-zulip].

## Contributing ##
We are welcoming contributors!

If you want to participate in this project, we're using [Git Flow][nvie] for our branch management, so please submit your commits using pull requests no on the develop branches mentioned above!

[netapi-docu]: http://ewoutkramer.github.io/fhir-net-api/docu-index.html
[netapi-zulip]: https://chat.fhir.org/#narrow/stream/dotnet
[fhir-spec]: http://www.hl7.org/fhir
[dstu2-spec]: http://hl7.org/fhir/DSTU2/index.html
[stu3-spec]: http://www.hl7.org/fhir
[fhirpath-spec]: http://hl7.org/fhirpath/

### GIT branching strategy
- [NVIE](http://nvie.com/posts/a-successful-git-branching-model/)
- Or see: [Git workflow](https://www.atlassian.com/git/workflows#!workflow-gitflow)
