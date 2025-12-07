# Proposed Fix for Issue #3359

## Important Note
**This fix is for code in the `Firely.Fhir.Packages` repository, NOT the `firely-net-sdk` repository.**

This directory contains the proposed fix for issue #3359. The actual code that needs to be changed exists in:
- Repository: https://github.com/FirelyTeam/Firely.Fhir.Packages
- Path: `Firely.Fhir.Packages/Extensions/PackageReferenceExtensions.cs`

## Files in this directory
- `PackageReferenceExtensions.cs` - The fixed version of the file from Firely.Fhir.Packages repository

## How to apply this fix
1. Clone the `Firely.Fhir.Packages` repository
2. Replace `/Firely.Fhir.Packages/Extensions/PackageReferenceExtensions.cs` with the fixed version
3. Run tests to verify the fix
4. Create a PR in the `Firely.Fhir.Packages` repository

## What was changed
Changed lines 24 and 35 from:
```csharp
dict.Add(reference.Name!, reference.Version);
```
To:
```csharp
dict[reference.Name!] = reference.Version;
```

This allows the method to handle duplicate dependencies gracefully by overwriting with the latest value instead of throwing an `ArgumentException`.

## See also
- `ISSUE_3359_RESOLUTION.md` in the root of this repository for full details
