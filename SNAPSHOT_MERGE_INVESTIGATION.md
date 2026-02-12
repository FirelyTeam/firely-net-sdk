# Snapshot Generator Datatype Merge Behavior Investigation

## Issue
FHIR-9791: Differences in behavior between .NET and Java StructureDefinition snapshotters when both a profiled base and a typeref to a profiled datatype bring in changes to element properties.

## Current .NET SDK Implementation

### Code Locations
- **SnapshotGenerator.cs**: Lines 938-1020 (mergeElement method)
- **SnapshotGenerator.cs**: Lines 1149-1444 (mergeTypeProfiles method)  
- **ElementDefnMerger.cs**: Lines 31-194 (merge method)
- **SnapshotGeneratorSettings.cs**: Configuration settings

### Current Merge Flow

When processing an element (e.g., Patient.address with type.profile = AddressNL):

1. **Input State**: `snap.Current` contains base profile element (Patient.address from Patient base)
2. **Step 1** (line 988): Call `mergeTypeProfiles(snap, diff)` 
   - Resolves and expands the type profile (AddressNL)
   - Merges type profile constraints onto snap
   - Result: snap now has Base + TypeProfile
3. **Step 2** (line 993): Call `mergeElementDefinition(snap.Current, diffElem, ...)`
   - Merges differential constraints onto snap
   - Result: snap now has Base + TypeProfile + Differential

**Effective Order**: Base → Type Profile → Differential

### Property-Level Merge Behavior (ElementDefnMerger)

For individual properties like `definition`, `short`, etc.:
- `mergePrimitiveElement()` (line 782): Diff value **replaces** snap value (last-wins)
- Special case for `definition`, `comment`, `requirements`: Can **append** if diff starts with "..."
- Collections are merged by matching items and combining

### Known Issues

Comment at lines 1346-1352 documents a problem:
```
// [WMR 20170428] ISSUE
// typeNav refers to type Snapshot, e.g. { Address Snap + MyAddress Diff }
// snap may already include Address Snap + Diff
// We need to determine { Address Snap + Diff + MyAddress Diff }
// But this performs { Address Snap + Diff + Address Snap (WRONG!) + MyAddress Diff }
```

This suggests the current implementation may re-apply base constraints incorrectly.

## Controversial Aspect (FHIR-9791)

Lines 1128-1144 document the controversy:

**Example Scenario**: Patient.Address with type.profile = AddressNL

Two possible merge strategies:
- **Option A**: Base profile first, then type profile
  - Order: Patient.Address base → AddressNL → Differential
  - Result: Type profile can override base constraints
  
- **Option B**: Type profile first, then base profile  
  - Order: AddressNL → Patient.Address base → Differential
  - Result: Base profile can override type constraints

**Current Comment** (line 1140): "Ewout: no clear answer, valid use cases exist for both options"

**Historical Note**: 
- A `MergeTypeProfiles` boolean setting existed to control this but was removed in 2016
- Comment at line 1143: "By default, use strategy (A): ignore custom type profile, merge from base"
- But actual implementation does the opposite! (merges type profile, doesn't ignore it)

## Current .NET Behavior Analysis

Based on code review:
- Current .NET SDK uses **Option A** (Base → Type → Diff)
- Type profile constraints can override base profile constraints
- Differential constraints can override everything

## Java HAPI FHIR Behavior

**Status**: Not yet investigated (requires looking at Java codebase)

## Proposed Solution

### 1. Add Configuration Enum

```csharp
public enum TypeProfileMergeStrategy
{
    /// <summary>
    /// Legacy .NET behavior: Base → Type → Differential
    /// Type profile constraints can override base profile constraints
    /// </summary>
    TypeFirst = 0, // Actually "BaseFirst" in execution order
    
    /// <summary>
    /// Alternative strategy: Type → Base → Differential  
    /// Base profile constraints can override type profile constraints
    /// </summary>
    BaseFirst = 1  // Actually "TypeFirst" in execution order
}
```

**Note**: The naming is confusing because:
- "TypeFirst" means base is merged first, THEN type (so type is applied on top)
- "BaseFirst" means type is merged first, THEN base (so base is applied on top)

### 2. Update SnapshotGeneratorSettings

Add property:
```csharp
public TypeProfileMergeStrategy TypeProfileMergeStrategy { get; set; } = TypeProfileMergeStrategy.TypeFirst;
```

### 3. Implement in SnapshotGenerator

Modify `mergeElement` method to:
- Check `_settings.TypeProfileMergeStrategy`
- If TypeFirst (current default): Keep existing logic (Base → Type → Diff)
- If BaseFirst: Swap order to (Type → Base → Diff)

## Open Questions

1. **What is the actual Java HAPI FHIR behavior?**
   - Need to examine Java code
   - Or create test cases and compare outputs

2. **Which strategy aligns with FHIR specification intent?**
   - Spec appears ambiguous (hence FHIR-9791)
   - May need community discussion

3. **Migration Strategy**
   - How to handle existing snapshots generated with current behavior?
   - Should we add a warning when using non-default strategy?
   - Should Simplifier indicate which strategy was used?

4. **Are there edge cases where the order doesn't matter?**
   - If type profile and base profile constrain different properties
   - If differential overrides both anyway

## Next Steps

1. ✅ Document current .NET behavior (this file)
2. ⬜ Investigate Java HAPI FHIR implementation
3. ⬜ Create test cases demonstrating the difference
4. ⬜ Implement configuration option
5. ⬜ Add comprehensive tests for both modes
6. ⬜ Update documentation
7. ⬜ Plan migration strategy for Simplifier users
8. ⬜ Consider starting Zulip/Rotterdam discussion as suggested in issue
