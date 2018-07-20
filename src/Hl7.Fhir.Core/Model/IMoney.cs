namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Common interface for different version-specific variants of ResourceReference
    /// </summary>
    /// <remarks>It is defined - and in this way - because in the newest R4 version of the standard Money becomes an independent type with just value and code.
    /// Other sub-types of Quantity like Age, Count etc. are remaning sub-types and so they can be handled in a common way via their Quantity base class.</remarks>
    public interface IMoney
    {
        decimal? Value { get; set; }

        string Currency { get; set; }
    }
}
