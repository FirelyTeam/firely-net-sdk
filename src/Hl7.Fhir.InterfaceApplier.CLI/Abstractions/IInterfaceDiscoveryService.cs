using System.Reflection;

namespace Hl7.Fhir.InterfaceApplier.CLI.Abstractions;

public interface IInterfaceDiscoveryService
{
    public IEnumerable<Type> GetInterfaceTypesToApply(IEnumerable<Assembly> assemblies);

    public IEnumerable<Type> GetInterfaceTypesToApply(Assembly assembly);
}