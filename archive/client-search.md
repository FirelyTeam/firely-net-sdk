---
layout: default
title: Searching for Resources
---

FHIR has extensive support for searching resources through the use of the REST interface. Describing all the possibilities is outside the scope of this document, but much more details can be found online in the [specification][fhir-search].

The FHIR client has a few operations to do basic search.

### Searching within a specific type of resource
The most basic search is the client's `Search<T>(string[] criteria = null, string[] includes = null, int? pageSize = null)` function. It searches all resources of a specific type based on zero or more criteria. Criteria must conform to the parameters as they would be specified on the search URL in the REST interface, so for example searching for all patients named 'Eve' would look like this

```csharp
Bundle results = client.Search<Patient>(new string[] { "family:exact=Eve" });
```

The search will return a Bundle containing entries for each resource found. It is even possible to leave out all criteria, effectively resulting in a search that returns all resources of the given type. Additionally, there is a `Search()` overload that does not use the generic `T` argument, you can pass the type of resource as a string in the first parameter instead. 


### Searching for a resource with a specific id
In some cases you may already have the id of a specific resource (e.g. an Observation with logical id `123`, corresponding to the url `Observation/123`). In this case you can use `SearchById<T>(string id, string[] includes = null, int? pageSize = null)`.

Note that this function still returns a Bundle. The operation differs from a `Read<T>()` operation because it can return *included* resources as well. E.g. given an id `123` for an Observation, you can ask a FHIR server to not only look for the indicated Observation but to return the associated `subject` as well:

```csharp
var incl = new string[] { "Observation.subject" };
Bundle results = client.SearchById<Observation>("123", incl);

``` 

### System wide search
Some servers allow you to execute searches across *all* resource types. This would use FhirClient's `WholeSystemSearch(string[] criteria = null, string[] includes = null, int? pageSize = null)`.

Doing this search:

```csharp
Bundle results = client.WholeSystemSearch(new string[] { "name=foo" });
```

would then not only return Patients with "foo" in their name, but Devices named "foo" as well.

### Complex searches
An alternative way to specify a query is by creating a `Query` resource and pass this to the client's `Search(Query q)` overload. The `Query` resource has a set of fluent calls to allow you to easily construct more complex queries:

```csharp
var q = new Query()
         .For("Patient").Where("name:exact=ewout")
         .OrderBy("birthDate", SortOrder.Descending)
         .SummaryOnly().Include("Patient.managingOrganization")
         .LimitTo(20);

Bundle result = client.Search(q);
 ```

Note that unlike the search options shown before, you can specify search ordering and the use of a summary result. As well, this syntax avoids the need to create arrays of strings as parameters and tends to be more readable. 

### Paged Results
Normally, any FHIR server will limit the number of search results returned. In the previous example, we explicitly limited the number of results per page to 20.

The FhirClient has a `Continue` function to browse a search result after the first page has been received using a `Search`:

```csharp
var result = client.Search(q);

while( result != null )
{
	// Do something useful
	result = client.Continue(result);
}
```

Note that `Continue` supports a second parameter that allows you to browse forward, backward, or go immediately to the first or last page of the search result.

###Further reading
* [Back to index](docu-index.html)
* [Next topic: Getting history]

[fhir-search]: http://www.hl7.org/implement/standards/fhir/search.html