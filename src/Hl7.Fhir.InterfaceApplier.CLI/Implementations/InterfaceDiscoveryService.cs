using Hl7.Fhir.InterfaceApplier.CLI.Abstractions;
using System.Reflection;

namespace Hl7.Fhir.InterfaceApplier.CLI.Implementations;

public class InterfaceDiscoveryService : IInterfaceDiscoveryService
{
    public IEnumerable<Type> GetInterfaceTypesToApply(IEnumerable<Assembly> assemblies)
        => assemblies.SelectMany(GetInterfaceTypesToApply);

    public IEnumerable<Type> GetInterfaceTypesToApply(Assembly assembly)
        => assembly.GetTypes()
            .Where(type => type is { IsInterface: true }
                           && type.CustomAttributes.Any(data => data.AttributeType.FullName ==
                                                                "Hl7.Fhir.Generation.ApplyInterfaceToGeneratedClassesAttribute"));
}