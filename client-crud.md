---
layout: default
title: Doing CRUD with FhirClient
---

### Create a FhirClient
Before we do anything else, we have to create a new FhirClient. This is done by passing the url of the FHIR server's endpoint as a parameter to the constructor:

```csharp
var client = new FhirClient("http://spark.furore.com/fhir");
```

You'll create an instance of a client for every server you want to work with. In fact, every call we'll do on this client will be for resources and operations on this server. Since resources may reference other resources on a different FHIR server, you'll have to inspect any references and direct them to the right FhirClient. Of course, if you're dealing with a single server within your organization or a single cloud-based FHIR server, you don't have to worry about this.

### Create a new Resource
Next, assume we have created a new `Patient` and now we want to ask the server to store it for us. This is done using `Create()`:

```csharp
var pat = new Patient() { /* set up data */ };
var result = this.Create(pat);
```

As you'd probably expect, this operation will throw an Exception when things go wrong, in most cases a `FhirOperationException`. This exception has an `Outcome` property that contains an [OperationOutcome][opoutc], and which you may inspect to find out more information about why the operation failed. Most FHIR servers will return a human-readable error description in the `OperationOutcome` to help you out.

If the operation was successful, it will return an instance of a `ResourceEntry<Patient>`. ResourceEntries are classes that contain both the Resource's data and it's metadata, like it's server-assigned id, version number, last modified date and others. All that's important to realize here is that `Create` does *not* return an instance of  `Patient`, but a `ResourceEntry<T>`. In fact, this ResourceEntry will just have the Patient's newly assigned metadata, not even a copy of the data you just posted to the server. Since you already have the Patient's data at hand, the server will, by default, not bother to return a copy of the data to save bandwith. 

In short, although the ResourceEntry's `Id` and `LastUpdated` and other metadata properties will be set, it's `Resource` property will still be `null`.

 



[opoutc]: http://www.hl7.org/fhir/operationoutcome.html
    