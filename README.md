This is the support API for working with the DSTU version of [HL7 FHIR][1] on the Microsoft .NET (dotnet) platform. The API deals with the HTTP and wire format, so you can write code like this to manipulate a patient's data: 

	var client = new FhirClient("http://spark.furore.com/fhir");

	var pat = client.Read<Patient>("Patient/1");
	pat.Resource.Name.Add(HumanName.ForFamily("Kramer")
    	 .WithGiven("Ewout"));

	client.Update<Patient>(pat);

This library provides:
* Class models for working with the FHIR data model using POCO's
* Xml and Json parsers and serializers
* A REST client for working with FHIR-compliant servers
* Helper classes to work with the specification metadata, most notably `StructureDefinition` and generation of differentials

## Getting Started ##
Get started by reading the [online documentation][3] or downloading the NuGet packages:

|Spec version|Core|Specification|
|---|---|---|
|DSTU 2.1|https://www.nuget.org/packages/Hl7.Fhir.DSTU21/|https://www.nuget.org/packages/Hl7.Fhir.Specification.DSTU21/|
|DSTU2 | https://www.nuget.org/packages/Hl7.Fhir.DSTU2/ | https://www.nuget.org/packages/Hl7.Fhir.Specification.DSTU2/ |
|DSTU1| https://www.nuget.org/packages/Hl7.Fhir.DSTU/ | https://www.nuget.org/packages/Hl7.Fhir.Specification.DSTU/ |
 
**Please note that the DSTU1 release is not maintained anymore.**

## Contributing ##
We are welcoming contributors!

If you want to participate in this project, we're using [Git Flow][4] for our branch management, so new development is done on (feature branches from) /develop.

[1]: http://www.hl7.org/fhir
[dstu1]: http://www.nuget.org/packages/Hl7.Fhir
[3]: http://ewoutkramer.github.io/fhir-net-api
[4]: http://nvie.com/posts/a-successful-git-branching-model/
[dstu2]: http://www.nuget.org/packages/Hl7.

### GIT branching strategy 
- [NVIE](http://nvie.com/posts/a-successful-git-branching-model/)
- Or see: [Git workflow](https://www.atlassian.com/git/workflows#!workflow-gitflow)
