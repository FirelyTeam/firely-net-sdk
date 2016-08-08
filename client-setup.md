---
layout: default
title: Setting up a FhirClient
---

### Create a FhirClient
Before we can do any of the operations explained in the next topics, we have to create a new FhirClient. This is done by passing the url of the FHIR server's endpoint as a parameter to the constructor:

```csharp
var client = new FhirClient("http://spark.furore.com/fhir");
```

You'll create an instance of a client for every server you want to work with. In fact, every call we'll do on this client will be for resources and operations on this server. Since resources may reference other resources on a different FHIR server, you'll have to inspect any references and direct them to the right FhirClient. Of course, if you're dealing with a single server within your organization or a single cloud-based FHIR server, you don't have to worry about this.

On the next pages, we assume a FhirClient has been created and is ready to be used.

###Further reading
* [Back to index](docu-index.html)
* [Next topic: Doing basic CRUD with FhirClient](client-crud.html)
