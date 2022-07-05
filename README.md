|STU3|R4|R4B|R5|
|---|---|---|---|
|[![Build Status](https://dev.azure.com/firely/firely-net-sdk/_apis/build/status/FirelyTeam.firely-net-sdk?branchName=develop-stu3)](https://dev.azure.com/firely/firely-net-sdk/_build/latest?definitionId=84&branchName=develop-stu3)|[![Build Status](https://dev.azure.com/firely/firely-net-sdk/_apis/build/status/FirelyTeam.firely-net-sdk?branchName=develop-r4)](https://dev.azure.com/firely/firely-net-sdk/_build/latest?definitionId=84&branchName=develop-r4)|[![Build Status](https://dev.azure.com/firely/firely-net-sdk/_apis/build/status/FirelyTeam.firely-net-sdk?branchName=develop-r4B)](https://dev.azure.com/firely/firely-net-sdk/_build/latest?definitionId=84&branchName=develop-r4B)|[![Build Status](https://dev.azure.com/firely/firely-net-sdk/_apis/build/status/FirelyTeam.firely-net-sdk?branchName=develop-r5)](https://dev.azure.com/firely/firely-net-sdk/_build/latest?definitionId=84&branchName=develop-r5)|
| [![Nuget](https://img.shields.io/nuget/dt/Hl7.Fhir.STU3)](https://www.nuget.org/packages/Hl7.Fhir.STU3) |[![Nuget](https://img.shields.io/nuget/dt/Hl7.Fhir.R4)](https://www.nuget.org/packages/Hl7.Fhir.R4) | [![Nuget](https://img.shields.io/nuget/dt/Hl7.Fhir.R4B)](https://www.nuget.org/packages/Hl7.Fhir.R4B)|[![Nuget](https://img.shields.io/nuget/dt/Hl7.Fhir.R5)](https://www.nuget.org/packages/Hl7.Fhir.R5) |

## Introduction ##
This is the official support SDK for working with [HL7 FHIR][fhir-spec] on the Microsoft .NET (dotnet) platform.

## Release notes ##
Read the releases notes on [firely-net-sdk/releases](https://github.com/FirelyTeam/firely-net-sdk/releases). You can find documentation about this SDK in [the Firely docs site][netsdk-docu].

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
* [DSTU2][dstu2-spec] (published October 2015) in widespread use, and is not supported by this library anymore since version 2.0.
* [STU3][stu3-spec] (published March 2017), mature support by this library and most other tooling on the market.
* [R4][r4-spec] (published January 2019), mature support by this library and fully supported by this library.
* [R4B][r4B-spec] (published May 2022), the latest release of the FHIR spec and fully supported by this library.

Planned release DSTU2.1 was never published by HL7, but you will still find traces of it, in particular we still keep the NuGet package for it available.

## Getting Started ##
Get started by reading the [online documentation][netsdk-docu]. Depending on the version of FHIR you require, you'll find the relevant links to the packages
and develop branches in this repository below:

|Spec version|Git branch|Core NuGet|
|---|---|---|
|R5 (experimental)| https://github.com/FirelyTeam/firely-net-sdk/tree/develop-r5 | https://www.nuget.org/packages/Hl7.Fhir.R5 |
|R4B| https://github.com/FirelyTeam/firely-net-sdk/tree/develop-r4B | https://www.nuget.org/packages/Hl7.Fhir.R4B |
|R4| https://github.com/FirelyTeam/firely-net-sdk/tree/develop-r4 | https://www.nuget.org/packages/Hl7.Fhir.R4 | 
|STU3| https://github.com/FirelyTeam/firely-net-sdk/tree/develop-stu3 | https://www.nuget.org/packages/Hl7.Fhir.STU3 | 
|DSTU2| https://github.com/FirelyTeam/firely-net-sdk/tree/develop | https://www.nuget.org/packages/Hl7.Fhir.DSTU2 | 
|DSTU1| https://github.com/FirelyTeam/firely-net-sdk/tree/master-dstu1 | https://www.nuget.org/packages/Hl7.Fhir.DSTU | 

Please note that the source code for the Firely .NET SDK is split up into two GitHub repos: one (with the branches listed above) with code that is specific to a FHIR release (this repo), and one that contains the code that is applicable across all FHIR releases (a separate [common repository][common-repo]).  This second repository is included in the first one using a Git Submodule, so all you have to do is check out the main repo (this one) and learn how to work with Submodules. To clone this repository with submodules, you can do:

    git clone --recurse-submodules -j8 https://github.com/FirelyTeam/firely-net-sdk.git .
    
Please refer to our [submodules overview](https://github.com/FirelyTeam/firely-net-sdk/wiki/Clone-this-repository-with-submodule-common) for more details.

## Upgrading
Upgrading to 2.x? Breaking changes are [listed here](https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-2.0).

Going further? See [3.x breaking changes](https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-3.0).

## Support 
We actively monitor the issues coming in through the GitHub repository at [https://github.com/FirelyTeam/firely-net-sdk/issues](https://github.com/FirelyTeam/firely-net-sdk/issues). You are welcome to register your bugs and feature suggestions there. For questions and broader discussions, we use the .NET FHIR Implementers chat on [Zulip][netsdk-zulip].

## Contributing ##
We are welcoming contributors!

If you want to participate in this project, we're using [Git Flow][nvie] for our branch management, so please submit your commits using pull requests on the correct `develop-stu3`/`develop-r4`/`develop-r4B`/`develop-r5` branches as mentioned above! 

[common-repo]: https://github.com/FirelyTeam/firely-net-common
[netsdk-docu]: https://docs.fire.ly/projects/Firely-NET-SDK/
[netsdk-zulip]: https://chat.fhir.org/#narrow/stream/dotnet
[nvie]: http://nvie.com/posts/a-successful-git-branching-model/
[fhir-spec]: http://www.hl7.org/fhir
[dstu1-spec]: http://hl7.org/fhir/DSTU1/index.html
[dstu2-spec]: http://hl7.org/fhir/DSTU2/index.html
[stu3-spec]: http://www.hl7.org/fhir/stu3
[r4-spec]: http://hl7.org/fhir/R4/
[r4B-spec]: http://hl7.org/fhir/index.html
[fhirpath-spec]: http://hl7.org/fhirpath/

### GIT branching strategy 
- [NVIE](http://nvie.com/posts/a-successful-git-branching-model/)
- Or see: [Git workflow](https://www.atlassian.com/git/workflows#!workflow-gitflow)
