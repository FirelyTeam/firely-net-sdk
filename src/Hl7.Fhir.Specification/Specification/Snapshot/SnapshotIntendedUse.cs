namespace Hl7.Fhir.Specification.Snapshot
{
    public enum SnapshotIntendedUse
    {
        Snapshot = 1 << 0,
        Differential = 1 << 1,
        BackwardsCompatible = Snapshot | Differential
    }
}
