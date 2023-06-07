namespace Hl7.Fhir.InterfaceApplier.CLI.Abstractions;

public interface IInterfaceApplierService
{
    public void Apply(IEnumerable<string> sourceFilesDirectories, IDictionary<Type, ICollection<Type>> classTypesForInterface);
}