---
layout: default
title: Getting history
---

There are several ways to retrieve version history for resources with the FHIR client.

### Retrieving the history of a specific resource
The version history of a specific resource can be retrieved with the `History(System.Uri location, [System.DateTimeOffset? since = null], [int? pageSize = null])` function. It returns a bundle with the history for the indicated instance, for example:

```csharp
var location = new Uri("http://spark.furore.com/fhir/Patient/31");
Bundle results = client.History(location);
```

Note that the Bundle may contain both ResourceEntries and DeletedEntries. It is possible to specify a date, to include only the changes that were made after the given date. Also you can specify the maximum number of results returned (see *Paged Results* below).
Additionally, there is a `History()` overload where you can pass the location of the resource as a string in the first parameter instead. 

### Retrieving history for a type of resource
Sometimes you may want to retrieve the history for a **type** of resource instead of an instance (e.g. the versions of all Patients). In this case you can use `TypeHistory<TResource>([System.DateTimeOffset? since = null], [int? pageSize = null])`:

```csharp
Bundle results = client.TypeHistory<Patient>();
``` 

As with the previous function, a date and pagesize can optionally be specified.

### System wide history
When a system wide history is needed, retrieving all versions of all resources, the FhirClient's `WholeSystemHistory([System.DateTimeOffset? since = null], [int? pageSize = null])` is used:

```csharp
var lastMonth = DateTime.Today.AddMonths(-1);
Bundle results = client.WholeSystemHistory(since: lastMonth, pageSize: 20);
```

In this case the function retrieves all changes to all resources that have been done since the last month and limits the results to a maximum of 20. Both these parameters are optional.

### Paged Results
Normally, any FHIR server will limit the number of results returned in the history. In the previous example, we explicitly limited the number of results per page to 20.

The FhirClient has a `Continue` function to browse a search result after the first page has been received using one of the `History` functions:

```csharp
var result = client.TypeHistory<Patient>();

while( result != null )
{
	// Do something useful
	result = client.Continue(result);
}
```

Note that `Continue` supports a second parameter that allows you to browse forward, backward, or go immediately to the first or last page of the result.

###Further reading
* [Back to index](docu-index.html)
* [Next topic: Tag operations]

