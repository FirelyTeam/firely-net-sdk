---
layout: default
title: Doing basic CRUD with FhirClient
---

A FhirClient named `client` has been setup in the previous topic, now let's do something with it.

### Create a new Resource
Assume we have created a new `Patient` and now we want to ask the server to store it for us. This is done using `Create()`:

```csharp
var pat = new Patient() { /* set up data */ };
var patEntry = client.Create(pat);
```

As you'd probably expect, this operation will throw an Exception when things go wrong, in most cases a `FhirOperationException`. This exception has an `Outcome` property that contains an [OperationOutcome][opoutc], and which you may inspect to find out more information about why the operation failed. Most FHIR servers will return a human-readable error description in the `OperationOutcome` to help you out.

If the operation was successful, it will return an instance of a `ResourceEntry<Patient>`. ResourceEntries are classes that contain both the Resource's data and its metadata, like its server-assigned id, version number, last modified date and others. All that's important to realize here is that `Create` does *not* return an instance of  `Patient`, but a `ResourceEntry<T>`. In fact, this ResourceEntry will just have the Patient's newly assigned metadata, not even a copy of the data you just posted to the server. Since you already have the Patient's data at hand, the server will, by default, not bother to return a copy of the data to save bandwidth. 

In short, although the ResourceEntry's `Id` and `LastUpdated` and other metadata properties will be set, its `Resource` property will still be `null`. To get a copy of the resource as it was stored on the server, pass `true` as the `refresh` parameter to the `Create` operation. 

### Refreshing data
Whenever you have held a Resource for some time, its data may have changed on the server because of changes made by others. At any time, you can refresh your local copy of the data by using the `Refresh` call, passing it the ResourceEntry as returned by a previous `Read` or `Create`:

```csharp
var newEntry = client.Refresh(oldEntry);

```

This call will go to the server and fetch the latest version and metadata of the Resource as pointed to by the `Id` property in the ResourceEntry passed as the parameter.

### Getting an existing Resource
To read the data for a given Resource instance from a server, you'll need its web address (url). You may have previously stored this reference, or you have found its address in a ResourceReference (e.g. `Observation.subject.reference`).

The `Read` operation on the FhirClient has two overloads to cover both cases. Furthermore, it accepts both relative paths and absolute paths (as long as they are within the endpoint passed to the constructor of the FhirClient). As with the other operations, Read returns a typed ResourceEntry rather than the Resource itself:

``` csharp
// Read the current version of a Resource
var location = new Uri("http://spark.furore.com/fhir/Patient/31");
var patEntryA = client.Read<Patient>(location);
var patEntryA = client.Read<Patient>("Patient/31");

// Read a specific version of a Resource
var locationB = new Uri("http://spark.furore.com/fhir/Patient/32/_history/4");
var patEntryB = client.Read<Patient>(locationB);
var patEntryB = client.Read<Patient>("Patient/32/_history/4");

```

`Read` only takes urls as parameters, so if you have the resource name and its Id as distinct data variables, use `ResourceIdentity`:

```csharp
var patEntryA = client.Read<Patient>(ResourceIdentity.Build("Patient","31"));
```


Note that Read can be used to get the most recent version of a Resource as well as a specific version, and thus covers the two 'logical' REST operations `read` and `vread`.

### Updating a Resource
Once you have retrieved a Resource, you may edit its contents and send it back to the server. This is done using the `Update` operation. It takes the ResourceEntry previously retrieved as a parameter:

```csharp
var patEntry = client.Read<Patient>(location);
// Add a name to the patient, and update
patEntry.Resource.Name.Add(HumanName.ForFamily("Kramer").WithGiven("Ewout"));
client.Update(patEntry);
```

There's always a chance that between retrieving the resource and sending an update, someone else has updated the resource as well. Servers supporting version-aware updates may refuse your update in this case and return a HTTP status code 409 (Conflict), which causes the `Update` operation to throw a `FhirOperationException` with the same status code.  

### Deleting a Resource
The `Delete` operation on the FhirClient deletes a resource from the server. It is up to the server to decide whether the resource is actually removed from storage, or whether previous versions are still available for retrieval. The `Delete` operation has multiple overloads to allow you to delete based on an url or a ResourceEntry:

```csharp
var location = new Uri("http://spark.furore.com/fhir/Patient/34");
client.Delete(location);

// You may also delete based on an existing ResourceEntry
client.Delete(patEntry);
```

The `Delete` operation will fail and throw a `FhirOperationException` if the Resource was already deleted or if the Resource did not exist before deletion. 


Note that sending an update to a Resource after it has been deleted is not considered an error and may effectively "undelete" it. 

###Further reading
* [Back to index](docu-index.html)
* [Next topic: Searching for resources](client-search.html)

[opoutc]: http://www.hl7.org/fhir/operationoutcome.html
