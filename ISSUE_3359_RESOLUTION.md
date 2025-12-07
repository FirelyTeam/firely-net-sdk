# Issue #3359: System.ArgumentException Resolution

## Issue Summary
User reports a `System.ArgumentException` with message "An item with the same key has already been added. Key: hl7.terminology.r4" when using `FhirPackageSource` from `Firely.Fhir.Packages` library version 5.0.0.

## Root Cause Analysis
The error occurs in the `Firely.Fhir.Packages` repository (NOT in firely-net-sdk), specifically in:
- File: `Firely.Fhir.Packages/Extensions/PackageReferenceExtensions.cs`
- Methods: `ToDictionary(this IEnumerable<PackageReference> references)` and `ToDictionary(this IEnumerable<PackageDependency> references)`

### The Problem
Both `ToDictionary` methods use `Dictionary.Add()` which throws an `ArgumentException` when attempting to add a duplicate key:

```csharp
internal static Dictionary<string, string?> ToDictionary(this IEnumerable<PackageReference> references)
{
    var dict = new Dictionary<string, string?>();
    foreach (var reference in references.Where(r => r.Name is not null))
    {
        dict.Add(reference.Name!, reference.Version);  // ← Throws on duplicate
    }
    return dict;
}
```

When a FHIR package (like the NGS TW package mentioned in the issue) has duplicate dependencies in its dependency tree, this method fails.

## Proposed Solution

### Option 1: Use Indexer Assignment (Recommended)
Replace `dict.Add()` with indexer assignment to overwrite duplicates with the latest value:

```csharp
internal static Dictionary<string, string?> ToDictionary(this IEnumerable<PackageReference> references)
{
    var dict = new Dictionary<string, string?>();
    foreach (var reference in references.Where(r => r.Name is not null))
    {
        dict[reference.Name!] = reference.Version;  // Overwrites if exists
    }
    return dict;
}

internal static Dictionary<string, string?> ToDictionary(this IEnumerable<PackageDependency> references)
{
    var dict = new Dictionary<string, string?>();
    foreach (var reference in references)
    {
        dict[reference.Name] = reference.Range;  // Overwrites if exists
    }
    return dict;
}
```

### Option 2: Use TryAdd (for .NET Standard 2.1+)
Use `TryAdd` to skip duplicates:

```csharp
internal static Dictionary<string, string?> ToDictionary(this IEnumerable<PackageReference> references)
{
    var dict = new Dictionary<string, string?>();
    foreach (var reference in references.Where(r => r.Name is not null))
    {
        dict.TryAdd(reference.Name!, reference.Version);  // Skips if exists
    }
    return dict;
}
```

### Option 3: Use LINQ with Duplicate Handling
Use LINQ's `ToDictionary` with a key selector that handles duplicates:

```csharp
internal static Dictionary<string, string?> ToDictionary(this IEnumerable<PackageReference> references)
{
    return references
        .Where(r => r.Name is not null)
        .GroupBy(r => r.Name!)
        .ToDictionary(g => g.Key, g => g.Last().Version);  // Takes last occurrence
}
```

## Recommended Action
**Option 1 (Indexer Assignment)** is recommended because:
1. It's simple and clear
2. It has no additional dependencies
3. It follows the principle of "last write wins" which is common for dependency resolution
4. It maintains backward compatibility

## Impact Analysis
- **Breaking Change**: No
- **Behavior Change**: Yes - duplicate dependencies will now be handled gracefully by keeping the last occurrence instead of throwing an exception
- **Performance**: Negligible impact

## Testing Recommendations
1. Create a test with a package manifest containing duplicate dependencies
2. Verify that `ToDictionary` doesn't throw an exception
3. Verify that the resulting dictionary contains the expected entry (last occurrence)
4. Test with the specific NGS TW package mentioned in the issue

## Repository Note
**IMPORTANT**: This fix needs to be applied to the `Firely.Fhir.Packages` repository, NOT the `firely-net-sdk` repository. Issue #3359 appears to have been filed in the wrong repository.

### Correct Repository
- Repository: https://github.com/FirelyTeam/Firely.Fhir.Packages
- File: `/Firely.Fhir.Packages/Extensions/PackageReferenceExtensions.cs`
- Lines: 19-27 and 29-37

## Next Steps
1. Move/recreate this issue in the `Firely.Fhir.Packages` repository
2. Apply the fix in that repository
3. Create unit tests
4. Release a new version of `Firely.Fhir.Packages`
5. Update `firely-net-sdk` documentation if it references this behavior
