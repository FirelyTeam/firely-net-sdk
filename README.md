|DSTU2|STU3|R4|Released|
|---|---|---|---|
|[![Build status](https://dev.azure.com/firely/fhir-net-api/_apis/build/status/Continuous%20Build?branchName=develop)](https://dev.azure.com/firely/fhir-net-api/_build?view=buildsHistory&definitionId=14)|[![Build status](https://dev.azure.com/firely/fhir-net-api/_apis/build/status/Continuous%20Build?branchName=develop-stu3)](https://dev.azure.com/firely/fhir-net-api/_build?view=buildsHistory&definitionId=14)|[![Build status](https://dev.azure.com/firely/fhir-net-api/_apis/build/status/Continuous%20Build?branchName=develop-r4)](https://dev.azure.com/firely/fhir-net-api/_build?view=buildsHistory&definitionId=14)|[![Release status](https://vsrm.dev.azure.com/firely/_apis/public/Release/badge/d27985be-1c61-41fd-82e7-23e7a2f06dc3/1/2)](https://dev.azure.com/firely/fhir-net-api/_releaseDefinition?definitionId=1&_a=definition-pipeline)|

## Introduction ##
This is the official support API for working with [HL7 FHIR][fhir-spec] on the Microsoft .NET (dotnet) platform.

## Release notes ##
Read the releases notes and the documentation on [simplifier](http://docs.simplifier.net/fhirnetapi/releasenotes.html)

## What's in the box?
This library provides:
* Class models for working with the FHIR data model using POCO's
* Xml and Json parsers and serializers
* A REST client for working with FHIR-compliant servers
* Helper classes to work with the specification metadata, most notably `StructureDefinition` and generation of differentials
* Validation of instances based on profiles
* Evaluation [FhirPath][fhirpath-spec] expressions

**IMPORTANT**
Before installing one of the NuGet packages (or clone the repo) it is important to understand that HL7 has published several updates of the FHIR specification, each with breaking changes - so you need to ensure you use the version that is right for you:

* [DSTU1][dstu1-spec] (published September 2014) is mostly obsolete, and the .NET version for this publication is not maintained anymore.
* [DSTU2][dstu2-spec] (published October 2015) in widespread use, and still supported by this library and other tooling on the market.
* [STU3][stu3-spec] (published March 2017), mature support by this library and most other tooling on the market.
* [R4][r4-spec] (published January 2019), the latest release of the FHIR spec and fully supported by this library.

Planned release DSTU2.1 was never published by HL7, but you will still find traces of it, in particular we still keep the NuGet package for it available.

## Getting Started ##
Get started by reading the [online documentation][netapi-docu]. Depending on the version of FHIR you require, you'll find the relevant links to the packages
and develop branches in this repository below:

|Spec version|Git branch|Core NuGet|
|---|---|---|
|R4|https://github.com/FirelyTeam/fhir-net-api/tree/develop-r4|https://www.nuget.org/packages/Hl7.Fhir.R4/|
|STU3|https://github.com/FirelyTeam/fhir-net-api/tree/develop-stu3|https://www.nuget.org/packages/Hl7.Fhir.STU3/|
|DSTU 2.1|N/A|https://www.nuget.org/packages/Hl7.Fhir.DSTU21/|
|DSTU2| https://github.com/FirelyTeam/fhir-net-api/tree/develop|https://www.nuget.org/packages/Hl7.Fhir.DSTU2/|
|DSTU1| https://github.com/FirelyTeam/fhir-net-api/tree/master-dstu1|https://www.nuget.org/packages/Hl7.Fhir.DSTU/|

Please note that the source code for the FHIR .NET API is split up into two GitHub repos: one (with the branches listed above) with code that is specific to a FHIR release (this repo), and one that contains the code that is applicable across all FHIR releases (a separate [common repository][common-repo]).  This second repository is included in the first one using a Git Submodule, so all you have to do is check out the main repo (this one) and learn how to work with Submodules. To clone this repository with submodules, you can do:

    git clone --recurse-submodules -j8 https://github.com/FirelyTeam/fhir-net-api.git .
    
Please refer to our [submodules overview](https://github.com/FirelyTeam/fhir-net-api/wiki/Clone-this-repository-with-submodule-common) for more details.

## Support 
We actively monitor the issues coming in through the GitHub repository at [https://github.com/FirelyTeam/fhir-net-api/issues](https://github.com/FirelyTeam/fhir-net-api/issues). You are welcome to register your bugs and feature suggestions there. For questions and broader discussions, we use the .NET FHIR Implementers chat on [Zulip][netapi-zulip].

## Contributing ##
We are welcoming contributors!

If you want to participate in this project, we're using [Git Flow][nvie] for our branch management, so please submit your commits using pull requests on the correct `develop`/`develop-stu3`/`develop-r4` branches as mentioned above! 

[common-repo]: https://github.com/FirelyTeam/fhir-net-common
[netapi-docu]: http://docs.simplifier.net/fhirnetapi/index.html
[netapi-zulip]: https://chat.fhir.org/#narrow/stream/dotnet
[nvie]: http://nvie.com/posts/a-successful-git-branching-model/
[fhir-spec]: http://www.hl7.org/fhir
[dstu1-spec]: http://hl7.org/fhir/DSTU1/index.html
[dstu2-spec]: http://hl7.org/fhir/DSTU2/index.html
[stu3-spec]: http://www.hl7.org/fhir
[r4-spec]: http://hl7.org/fhir/R4/index.html
[fhirpath-spec]: http://hl7.org/fhirpath/

### GIT branching strategy 
- [NVIE](http://nvie.com/posts/a-successful-git-branching-model/)
- Or see: [Git workflow](https://www.atlassian.com/git/workflows#!workflow-gitflow)
