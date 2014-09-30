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

### Get Started ###
Get started by reading the [online documentation][3] or downloading the [NuGet package][2] 

If you want to parcitipate in this project, we're using [Git Flow][4] for our branche management, so new development is done on (feature branches from) /develop.

[1]: http://www.hl7.org/fhir
[2]: http://www.nuget.org/packages/Hl7.Fhir
[3]: http://ewoutkramer.github.io/fhir-net-api
[4]: http://nvie.com/posts/a-successful-git-branching-model/

### GIT branching strategy 
- [NVIE](http://nvie.com/posts/a-successful-git-branching-model/)
- Or see: [Git workflow](https://www.atlassian.com/git/workflows#!workflow-gitflow)
