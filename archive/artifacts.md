---
layout: default
title: Loading artifacts
---

The FHIR distribution contains a host of support files that augment FHIR's functionality and help developers do validation and other tasks. This are files like validation XSD schemas, schematron files, profile files etcetera.

Some of these files can be found on the web, some of those inside zip files in the FHIR distribution and possibly some of them are provided as individual files as part of the distribution of other FHIR software.

To help accessing these files, the FHIR .NET API contains the `ArtifactResolver` class, which has two main methods:

1. To locate a non-resource artifact, like an XSD file etcetera, call the resolver's `ReadContentArtifact` method, passing it the name of the file you are looking for:

```csharp
var resolver = new ArtifactResolver();
using(var fileStream = resolver.ReadContentArtifact("patient.sch"))
{
	// read the schematron file from the stream, make sure to Dispose() it
	// or use a using() block
}
```

2. To load a Profile, ValueSet or other artifact Resource, call `ReadResourceArtifact`. Pass the full url of the artifact to the call:

```csharp
var resolver = new ArtifactResolver();
var address = new Uri("http://hl7.org/fhir/v2/vs/0292");
ValueSet vs = resolver.ReadResourceArtifact(address);
```

To locate the indicated artifact, the ArtifactResolver executes the following steps:
1. Try to find a file with the given filename in the current directory (this will often be the location the application's executable is running from). For Uri-based artifacts, the uri is interpreted as a FHIR endpoint and the "logical id" is used as the filename. This implies that if you try to locate a Profile called `http://hl7.org/fhir/Profiles/us-core`, the resolver will try to read `us-core.xml` or `us-core.json` and parse it as a Profile.
2. Failing this, the resolver will try to find the artifact inside the `validation.zip` file which is part of the standard distribution. This zip contains all the FHIR core profiles, valuesets and schemas.
3. Failing this, the resolver will try to reach the endpoint (for Uri-based artifacts) and download the indicated artifact resource.

### Tuning ArtifactResolver's behaviour
If the standard resolution steps described above does not fit your situation, you can manipulate the ArtifactResolver's `Sources` property. This property contains a list of objects implementing `IArtifactSource` and which are tried in order to locate the desired artifact. By default, the first source in this list is the `FileArtifactSource`, the second the `CoreZipArtifactSource` and last is the `WebArtifactSource`. These sources can be manipulated or you can plug-in your own `IArtifactSource` implementation to change how the resolver works.


### Using the FHIR XSD schemas
The FHIR distribution contains a set of XSD files supplied as an aid for the validation of XML content. These files can be used to validate individual resources and (atom) feeds.

Although you can get to these schema files using the `ArtifactResolver`, the API facilitates access to the exact subset of schemas you need for validation by means of the `SchemaCollection` class. The class has a single static property `ValidationSchemaSet` which returns a pre-compiled .NET XmlSchemaSet. This XmlSchemaSet can then be used to validate resources, like so:

```csharp
var doc = XDocument.Parse(value);
doc.Validate(SchemaCollection.ValidationSchemaSet, validationEventHandler: null);
``` 
