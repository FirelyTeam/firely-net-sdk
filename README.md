|DSTU2|STU3|Released|
|---|---|---|
|[![Build status](https://dev.azure.com/firely/fhir-net-api/_apis/build/status/Continuous%20Build?branchName=develop)](https://dev.azure.com/firely/fhir-net-api/_build?view=buildsHistory&definitionId=14)|[![Build status](https://dev.azure.com/firely/fhir-net-api/_apis/build/status/Continuous%20Build?branchName=develop-stu3)](https://dev.azure.com/firely/fhir-net-api/_build?view=buildsHistory&definitionId=14)|[![Release status](https://vsrm.dev.azure.com/firely/_apis/public/Release/badge/d27985be-1c61-41fd-82e7-23e7a2f06dc3/1/2)](https://dev.azure.com/firely/fhir-net-api/_releaseDefinition?definitionId=1&_a=definition-pipeline)|

## Introduction ##
This is the official support API for working with [HL7 FHIR][fhir-spec] on the Microsoft .NET (dotnet) platform.

This library provides:
* Class models for working with the FHIR data model using POCO's
* Xml and Json parsers and serializers
* A REST client for working with FHIR-compliant servers
* Helper classes to work with the specification metadata, most notably `StructureDefinition` and generation of differentials
* Validation of instances based on profiles
* Evaluation [FhirPath][fhirpath-spec] expressions

The library is currently split up in four parts:
* *Core* (NuGet packages starting with `Hl7.Fhir.<version>`) - contains the FhirClient and parsers
* *Specification* (NuGet packages starting with `Hl7.Fhir.Specification.<version>`) - functionality to work with the specification metadata and validation
* *FhirPath* (NuGet package `Hl7.FhirPath`) - the FhirPath evaluator, used by the Core and Specification assemblies
* *Support* (NuGet package `Hl7.Fhir.Support`) - a library with interfaces, abstractions and utility methods that are used by the other packages

**IMPORTANT**
Before installing one of the NuGet packages (or clone the repo) it is important to understand that HL7 has published several updates of the FHIR specification,
each with breaking changes - so you need to ensure you use the version that is right for you:

* [DSTU1][dstu1-spec] (published September 2014) is mostly obsolete, and the .NET version for this publication is not maintained anymore.
* [DSTU2][dstu2-spec] (published October 2015) in widespread use, and still supported by this library and other tooling on the market.
* [STU3][stu3-spec] (published March 2017) latest release, mature support by this library and most other tooling on the market.
* [R4][r4-spec] (not yet published), support in alpha by this library. Would not be supported by production systems, since this release is still in development. Expected publication in Q3 2018.


Planned release DSTU2.1 was never published by HL7, but you will still find traces of it, in particular we still keep the NuGet package for it available.

## Getting Started ##
Get started by reading the [online documentation][netapi-docu]. Depending on the version of FHIR you require, you'll find the relevant links to the packages
and develop branches in this repository below:

|Spec version|Git branch|Core NuGet|Specification NuGet|
|---|---|---|---|
|R4|https://github.com/FirelyTeam/fhir-net-api/tree/develop-r4|https://www.nuget.org/packages/Hl7.Fhir.R4/|https://www.nuget.org/packages/Hl7.Fhir.Specification.R4/|
|STU3|https://github.com/FirelyTeam/fhir-net-api/tree/develop-stu3|https://www.nuget.org/packages/Hl7.Fhir.STU3/|https://www.nuget.org/packages/Hl7.Fhir.Specification.STU3/|
|DSTU 2.1|N/A|https://www.nuget.org/packages/Hl7.Fhir.DSTU21/|https://www.nuget.org/packages/Hl7.Fhir.Specification.DSTU21/|
|DSTU2| https://github.com/FirelyTeam/fhir-net-api/tree/develop|https://www.nuget.org/packages/Hl7.Fhir.DSTU2/ | https://www.nuget.org/packages/Hl7.Fhir.Specification.DSTU2/ |
|DSTU1| https://github.com/FirelyTeam/fhir-net-api/tree/master-dstu1|https://www.nuget.org/packages/Hl7.Fhir.DSTU/ | https://www.nuget.org/packages/Hl7.Fhir.Specification.DSTU/ |
  
## Support 
We actively monitor the issues coming in through the GitHub repository at [https://github.com/FirelyTeam/fhir-net-api/issues](https://github.com/FirelyTeam/fhir-net-api/issues). You are welcome to register your bugs and feature suggestions there. For questions and broader discussions, we use the .NET FHIR Implementers chat on [Zulip][netapi-zulip].

## Contributing ##
We are welcoming contributors!

If you want to participate in this project, we're using [Git Flow][nvie] for our branch management, so please submit your commits using pull requests no on the develop branches mentioned above! 

[netapi-docu]: http://docs.simplifier.net/fhirnetapi/index.html
[netapi-zulip]: https://chat.fhir.org/#narrow/stream/dotnet
[nvie]: http://nvie.com/posts/a-successful-git-branching-model/
[fhir-spec]: http://www.hl7.org/fhir
[dstu1-spec]: http://hl7.org/fhir/DSTU1/index.html
[dstu2-spec]: http://hl7.org/fhir/DSTU2/index.html
[stu3-spec]: http://www.hl7.org/fhir
[r4-spec]: http://build.fhir.org
[fhirpath-spec]: http://hl7.org/fhirpath/

### GIT branching strategy 
- [NVIE](http://nvie.com/posts/a-successful-git-branching-model/)
- Or see: [Git workflow](https://www.atlassian.com/git/workflows#!workflow-gitflow)
