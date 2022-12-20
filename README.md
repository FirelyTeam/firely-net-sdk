[![Build Status](https://dev.azure.com/firely/firely-net-sdk/_apis/build/status/FirelyTeam.firely-net-sdk?branchName=develop-stu3)](https://dev.azure.com/firely/firely-net-sdk/_build/latest?definitionId=84&branchName=develop-stu3)

> **IMPORTANT** The 5.0 version of the SDK contains substantial changes to the way we have organized the NuGet packages and source code. Please read before installing this new 5.0 version.

> **IMPORTANT** Version 5.0 of the SDK is in beta. Navigate back to the [main repository](https://github.com/FirelyTeam/firely-net-sdk/) for the stable 4.x release.

## Introduction ##
This is Firely's official support SDK for working with [HL7 FHIR][fhir-spec] on the Microsoft .NET (dotnet) platform.

This SDK provides:
* Class models for working with the FHIR data model using POCO's
* Xml and Json parsers and serializers
* A REST client for working with FHIR-compliant servers
* Helper classes to work with the specification metadata, most notably `StructureDefinition` and generation of differentials
* Validation of instances based on profiles
* Evaluation [FhirPath][fhirpath-spec] expressions

## Release notes ##
Read the releases notes on [firely-net-sdk/releases](https://github.com/FirelyTeam/firely-net-sdk/releases). You can find documentation about this SDK in [the Firely docs site][netsdk-docu].


## Getting Started ##
Before installing one of the NuGet packages (or clone the repo) it is important to understand that HL7 has published several updates of the FHIR specification, each with breaking changes - so you need to ensure you use the version that is right for you:

* [DSTU1][dstu1-spec] (published September 2014) is mostly obsolete and we are no longer maintaining this library for DSTU1.
* [DSTU2][dstu2-spec] (published October 2015) is mostly obsolete and and we are no longer maintaining this library for DSTU2.
* [STU3][stu3-spec] (published March 2017) is older, but still widely in use. Is fully supported by this library.
* [R4][r4-spec] (published January 2019) is in active use and fully supported by this library.
* [R4B][r4B-spec] (published May 2022) is the latest official release of the FHIR spec and is fully supported by this library.
* [R5][r5-spec] (not yet published) is work-in-progress. This library currently supports "snapshot-1".

Planned release DSTU2.1 was never published by HL7, but you will still find traces of it. We are not maintaining the version, but the NuGet package is still available.

Read the [online documentation][netsdk-docu], and download the correct NuGet package for your FHIR release. Depending on the version of FHIR you require, you'll find the relevant link to the package below. For most developers, just including this NuGet package is enough to get started. 

|Spec version|Git branch| NuGet|
|---|---|---|
|R5 | https://github.com/FirelyTeam/firely-net-sdk/tree/release/5.0.0 | https://www.nuget.org/packages/Hl7.Fhir.R5 |
|R4B| https://github.com/FirelyTeam/firely-net-sdk/tree/release/5.0.0 | https://www.nuget.org/packages/Hl7.Fhir.R4B |
|R4| https://github.com/FirelyTeam/firely-net-sdk/tree/release/5.0.0 | https://www.nuget.org/packages/Hl7.Fhir.R4 | 
|STU3| https://github.com/FirelyTeam/firely-net-sdk/tree/release/5.0.0 | https://www.nuget.org/packages/Hl7.Fhir.STU3 | 
|DSTU2| https://github.com/FirelyTeam/firely-net-sdk/tree/develop-dstu2 | https://www.nuget.org/packages/Hl7.Fhir.DSTU2 | 

## Upgrading
We spend a lot of effort trying to maintain *compile* compatibility (not binary compatibility) between minor releases of the SDK. We do, however, publish a new major version with breaking changes about once a year. The table below lists the breaking changes for each of the major upgrades.

|SDK version|Breaking changes
|---|---|
|2.x|https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-2.0|
|3.x|https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-3.0|
|4.x|https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-4.0|
|5.x|https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-5.0|

The SDK has been totally restructured for the 5.0 release. Please take not of the following changes if you are upgrading:
* The additional `Hl7.Fhir.Specification` packages are no longer relevant starting at version 5.0 of the SDK and should not be included in your projects anymore.
* The main NuGet package no longer contains the `specification.zip` file that contains all the conformance resources used in the FHIR specification. If you are using the `ZipSource` resolvers (e.g. for profile validation), you will need to include the correct `HL7.Fhir.Specification.Data.*` NuGet packages. We recommend you look at the newer [`FhirPackageResolver`](https://www.nuget.org/packages/Firely.Fhir.Packages) that directly use the HL7-published NPM packages. Documentation can be found [here](https://docs.fire.ly/projects/Firely-NET-SDK/fhir-package-source/package-source.html).
* The profile validator has been split off into its own [repository](https://github.com/FirelyTeam/Hl7.Fhir.Validation.Legacy). The NuGet packages for the 5.0 version of the profile validator will be posted soon.


## Support 
We actively monitor the issues coming in through the GitHub repository at [https://github.com/FirelyTeam/firely-net-sdk/issues](https://github.com/FirelyTeam/firely-net-sdk/issues). You are welcome to register your bugs and feature suggestions there. For questions and broader discussions, we use the .NET FHIR Implementers chat on [Zulip][netsdk-zulip].

## Contributing ##
We are welcoming contributions!

If you want to participate in this project, we're using [Git Flow][nvie] for our branch management. Please submit PRs with changes against the `develop` branche.

> Note: Since the 5.0 release of the SDK, the branches for STU3 and newer have been combined in a single `develop` branch. This branch now contains the code for all FHIR releases from STU3 and up. We have also refactored all the common code out to projects within that branch, so the separate `common` repository (at https://github.com/FirelyTeam/firely-net-common) is no longer in use. This greatly simplifies management and creating PRs for these projects.


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
[r5-spec]: http://build.fhir.org/index.html
[fhirpath-spec]: http://hl7.org/fhirpath/

