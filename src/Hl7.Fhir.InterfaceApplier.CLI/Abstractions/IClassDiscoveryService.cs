using System.Reflection;

namespace Hl7.Fhir.InterfaceApplier.CLI.Abstractions;

public interface IClassDiscoveryService
{
    public IDictionary<Type, ICollection<Type>> GetClassTypesToApplyInterfaceTo(IEnumerable<Assembly> assemblies,
        ICollection<Type> interfaceTypes);

    public IDictionary<Type, ICollection<Type>> GetClassTypesToApplyInterfaceTo(Assembly assembly,
        IEnumerable<Type> interfaceTypes);
}