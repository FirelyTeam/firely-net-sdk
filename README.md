[![Build Status](https://dev.azure.com/firely/firely-net-sdk/_apis/build/status/FirelyTeam.firely-net-sdk?branchName=develop)](https://dev.azure.com/firely/firely-net-sdk/_build/latest?definitionId=84&branchName=develop)

> **IMPORTANT** The 5.0 version of the SDK contains substantial changes to the way we have organized the NuGet packages and source code. Please read before installing this new 5.0 version.

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

* [STU3][stu3-spec] (published March 2017) is older, but still widely in use and fully supported by the SDK.
* [R4][r4-spec] (published January 2019) is in active use and fully supported by the SDK.
* [R4B][r4B-spec] (published May 2022) is in active use and fully supported by the SDK.
* [R5][r5-spec] (published March 26 2023) is the latest official release of the FHIR spec and is fully supported by the SDK.

Read the [online documentation][netsdk-docu], and download the correct for your FHIR release. Depending on the version of FHIR you require, you'll find the relevant link to the package below. For most developers, just including this NuGet package is enough to get started. 

|Spec version|Git branch| NuGet|
|---|---|---|
|R5 | https://github.com/FirelyTeam/firely-net-sdk/tree/release/5.0.0 | https://www.nuget.org/packages/Hl7.Fhir.R5 |
|R4B| https://github.com/FirelyTeam/firely-net-sdk/tree/release/5.0.0 | https://www.nuget.org/packages/Hl7.Fhir.R4B |
|R4| https://github.com/FirelyTeam/firely-net-sdk/tree/release/5.0.0 | https://www.nuget.org/packages/Hl7.Fhir.R4 | 
|STU3| https://github.com/FirelyTeam/firely-net-sdk/tree/release/5.0.0 | https://www.nuget.org/packages/Hl7.Fhir.STU3 | 

### Using a pre-release NuGet package
Every release of the SDK results in a NuGet package on the normal NuGet feed. However, each commit on our develop branch also results in a pre-release package.
These are public too. So if you want to be brave and use a pre-release packages, you can do so by adding ```https://nuget.pkg.github.com/FirelyTeam/index.json``` to your NuGet sources:

- Get a Personal Access token (PAT) from [github.com][github-pat] with scope ```read:packages```

- Next open a console on your machine and run ```dotnet nuget add source --name github --username <USERNAME> --password <PAT> https://nuget.pkg.github.com/FirelyTeam/index.json```

```USERNAME```: your username on GitHub
```PAT```: your Personal access token with at least the scope ```read:packages```


## Upgrading
We spend a lot of effort trying to maintain *compile* compatibility (not binary compatibility) between minor releases of the SDK. We do, however, publish a new major version with breaking changes about once a year. The table below lists the breaking changes for each of the major upgrades.

|SDK version|Breaking changes
|---|---|
|2.x|https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-2.0|
|3.x|https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-3.0|
|4.x|https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-4.0|
|5.x|https://github.com/FirelyTeam/firely-net-sdk/wiki/Breaking-changes-in-5.0|

The SDK has been restructured for the 5.0 release. Please take note of the following changes if you are upgrading:
* You should only reference the main package (`Hl7.Fhir.<release>`). 
* If you need the `specification.zip` (for validation, if you are using the `ZipSource` resolver), add `Hl7.Fhir.Specification.Data.<release>`.
* The "old" `Hl7.Fhir.Specification.<release>` package is now a metapackage that will include these two packages.
* You should not reference any other packages that existed pre-5.0 (`Hl7.Fhir.ElementModel` etc.)

The profile validator has been split off into its own [repository](https://github.com/FirelyTeam/Hl7.Fhir.Validation.Legacy). The NuGet packages for the validator that are compatible with the SDK 5.0 release can be found on [NuGet](https://www.nuget.org/packages?q=Hl7.Fhir.Validation.Legacy).

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
[github-pat]: https://github.com/settings/apps
