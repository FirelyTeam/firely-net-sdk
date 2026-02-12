# TypeProfileMergeStrategy Configuration

## Overview

This feature addresses FHIR-9791, which documents a controversial aspect of FHIR snapshot generation: the order in which element constraints should be merged when both a profiled base and a typeref to a profiled datatype bring in changes.

## The Problem

Consider this scenario:
- A base profile (e.g., PatientNL) derives from Patient and constrains `Patient.address` 
- That profile also specifies a type profile on address: `type.profile = "AddressNL"`
- Now there are two sources of constraints for the address element:
  1. Constraints from the base profile (PatientNL.address)
  2. Constraints from the type profile (AddressNL)

**Question**: Which constraints should "win" when they conflict?

The FHIR specification doesn't prescribe a definitive answer, and both orders have valid use cases. This has led to different implementations in different FHIR libraries (notably .NET vs Java HAPI FHIR).

## The Configuration

Starting with this version, you can control the merge strategy using `SnapshotGeneratorSettings.TypeProfileMergeStrategy`:

```csharp
var settings = new SnapshotGeneratorSettings
{
    TypeProfileMergeStrategy = TypeProfileMergeStrategy.TypeFirst // or BaseFirst
};

var generator = new SnapshotGenerator(resolver, settings);
```

### Available Strategies

#### TypeFirst (Default)
Maintains backward compatibility with existing .NET SDK behavior.

**Merge order**: Base profile → Type profile → Differential

**Example**: Patient.address with type.profile = AddressNL
1. Start with base Patient.address constraints
2. Apply AddressNL type profile constraints (can override base)
3. Apply differential constraints (can override everything)

**Result**: Type profile constraints take precedence over base profile constraints.

**Use when**:
- You want backward compatibility with existing .NET-generated snapshots
- You want type profiles to be able to override base profile constraints
- You're working in a .NET-only environment

#### BaseFirst (Not Yet Implemented)
Designed for compatibility with Java HAPI FHIR implementation.

**Merge order**: Type profile → Base profile → Differential

**Example**: Patient.address with type.profile = AddressNL  
1. Start with AddressNL type profile constraints
2. Apply base Patient.address constraints (can override type)
3. Apply differential constraints (can override everything)

**Result**: Base profile constraints take precedence over type profile constraints.

**Use when** (once implemented):
- You need compatibility with Java HAPI FHIR-generated snapshots
- You want base profile constraints to take precedence
- You're working in a mixed .NET/Java environment

**Current Status**: Throws `NotImplementedException` with a clear error message explaining the situation.

## Implementation Status

### ✅ Completed
- Enum and configuration property added
- Default behavior (TypeFirst) works as before
- Validation prevents use of unimplemented mode
- Comprehensive documentation
- Test infrastructure in place

### ⏳ Future Work
- Implement BaseFirst strategy
- Investigate actual Java HAPI FHIR behavior to confirm compatibility
- Community discussion on preferred approach
- Migration guide for Simplifier users

## Migration Considerations

### For Existing Users
The default setting (`TypeFirst`) maintains complete backward compatibility. Existing code will continue to work exactly as before with no changes needed.

### When BaseFirst is Implemented
If you need to switch to BaseFirst mode in the future:

1. **Test thoroughly**: The change in merge order may affect your profiles
2. **Regenerate snapshots**: All snapshots should be regenerated with the new strategy
3. **Version carefully**: Consider the impact on any consumers of your profiles
4. **Document the strategy**: Make it clear which strategy was used to generate snapshots

## Technical Details

### Current Implementation

The merge happens in `SnapshotGenerator.mergeElement()`:
```csharp
// Current TypeFirst behavior:
// 1. snap.Current contains base profile element
// 2. mergeTypeProfiles merges type profile onto snap (base + type)
// 3. mergeElementDefinition merges differential onto snap (base + type + diff)
if (!isRoot)
{
    isMerged = await mergeTypeProfiles(snap, diff).ConfigureAwait(false);
}
mergeElementDefinition(snap.Current, diffElem, true, ...);
```

### Why BaseFirst Requires Refactoring

The current architecture assumes the snapshot navigator (`snap`) already contains the base profile when we enter the merge logic. To implement BaseFirst, we would need to:

1. Start with the type profile as the initial snapshot
2. Merge the base profile onto that
3. Then merge the differential

This requires restructuring the merge pipeline to support both orders, which is non-trivial given the complexity of the snapshot generation algorithm.

## References

- **JIRA Issue**: [FHIR-9791](https://jira.hl7.org/browse/FHIR-9791)
- **Stack Overflow Discussion**: [FHIR Snapshot Base Selection](https://stackoverflow.com/questions/36487281/fhir-snapshot-base-selection)
- **Investigation Doc**: See SNAPSHOT_MERGE_INVESTIGATION.md in repository root

## Getting Help

If you have questions about which strategy to use or need help with migration:

1. Check the investigation document for detailed technical background
2. Review the test cases in `TypeProfileMergeBehaviorTests.cs` for concrete examples
3. Open an issue on GitHub if you encounter problems
4. Join the discussion on Zulip/FHIR chat for community input

## Example Usage

```csharp
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;

// Using default TypeFirst strategy (backward compatible)
var settings = SnapshotGeneratorSettings.CreateDefault();
// settings.TypeProfileMergeStrategy is TypeFirst by default

var generator = new SnapshotGenerator(resolver, settings);
await generator.UpdateAsync(myProfile);

// Attempting to use BaseFirst (will throw until implemented)
var futureSettings = new SnapshotGeneratorSettings
{
    TypeProfileMergeStrategy = TypeProfileMergeStrategy.BaseFirst
};

// This will throw NotImplementedException:
// var futureGenerator = new SnapshotGenerator(resolver, futureSettings);
```

## FAQ

**Q: Do I need to change anything in my existing code?**  
A: No. The default behavior is unchanged.

**Q: When will BaseFirst be implemented?**  
A: That depends on community feedback and requirements. The infrastructure is in place to make implementation easier when needed.

**Q: Which strategy is "correct"?**  
A: Neither. The FHIR specification is ambiguous on this point, and both have valid use cases. The choice depends on your specific requirements and interoperability needs.

**Q: Will my existing snapshots need to be regenerated?**  
A: Only if you choose to switch to BaseFirst mode (once implemented). TypeFirst mode produces the same output as before.

**Q: How do I know which strategy was used to generate a snapshot?**  
A: Currently there's no metadata indicating this. This may be added in a future version.
