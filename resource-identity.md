---
layout: default
title: Working with ResourceIdentity
---

Resources have two pieces of metadata involved in identifying a Resource: its Logical Id and its Version Id. Both of these are expressed in the Url that forms a Resource's identifier and version-specific identifier:

```
http://www.someserver.org/fhir/Patient/213
http://www.someserver.org/fhir/Patient/213/_history/2
```

These Resource identifiers are kept in the ``Id`` and ``SelfLink`` property of a `BundleEntry` respectively. As is clear from the examples, these urls are not opaque, and contain the resource's type, it's logical id and its version id. The .NET API provides the ``ResourceIdentity`` class to either build these uris or split them up into their components:

``` csharp
var base = new Uri("http://www.someserver.org/fhir");
entry.Id = ResourceIdentity.Build(base,"Patient", "213");
entry.SelfLink = ResourceIdentity.Build(base,"Patient","213","2");
```

These ``Build()`` methods return a new ``ResourceIdentity`` with the given logical id and version id. Note that `ResourceIdentity` is a subclass of the standard .NET ``Uri`` class, so it can directly be assigned to entry's `Id` and `SelfLink` properties. Note that you do not need to specify a base url, and produce relative resource references instead:

```csharp
client.Read<Patient>(ResourceIdentity.Build("Patient","31"));
```

Conversely, to extract this information from an existing url:

```csharp
var identity = new ResourceIdentity("http://www.someserver.org/fhir/Patient/213");
Console.WriteLine("Logical Id: " + identity.Id);
Console.WriteLine("Type: " + identity.Collection);
```

It's also possible to make an existing ResourceIdentity version specific, or change its version id:

``` csharp
var originalId = new ResourceIdentity(entry.Id);
var withVersion = originalId.WithVersion("2");
```

or the other way around:

``` csharp
var originalVersionId = new ResourceIdentity(entry.SelfLink);
var resourceId = originalVersionId.WithoutVersion();
```

Finally, we can turn absolute identifiers into Uri's relative to the *service base* (in this case: `http://www.someserver.org/fhir`):

``` csharp
var identity = new ResourceIdentity("http://www.someserver.org/fhir/Patient/213");
var operationPath = identity.OperationPath;
// operationPath is "Patient/213"
```