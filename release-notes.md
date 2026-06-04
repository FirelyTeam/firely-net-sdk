## Intro:

Added `RespectSuppressExtension` setting to `SnapshotGeneratorSettings` (default `true`) so consumers can opt out of suppress-extension handling during snapshot generation.  
Added `Base.ClearOverflow()` and corrected the `NoOverflow` docstring.  
`ITypedElement` extension methods now carry `OverloadResolutionPriority(1)` so they are preferred over `ISourceNode` extensions for types that implement both (e.g. `PocoNode`).  
Fixed a cache-key bug in `CachingTerminologyService` where resource or unrecognized parameter values could break the cache key.  
Validation now includes canonicals in failed resolve attempts, and cardinality validation handles the case where the member name was not reported.  
Bumped `System.Text.Json` and `Microsoft.Extensions.Caching.Memory` to 10.0.7.
