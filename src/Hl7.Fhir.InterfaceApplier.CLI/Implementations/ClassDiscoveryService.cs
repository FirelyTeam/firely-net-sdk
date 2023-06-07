using Hl7.Fhir.InterfaceApplier.CLI.Abstractions;
using System.Reflection;

namespace Hl7.Fhir.InterfaceApplier.CLI.Implementations;

public class ClassDiscoveryService : IClassDiscoveryService
{
    #region Public Methods

    public IDictionary<Type, ICollection<Type>> GetClassTypesToApplyInterfaceTo(IEnumerable<Assembly> assemblies,
        ICollection<Type> interfaceTypes)
    {
        var result = new Dictionary<Type, ICollection<Type>>();
        foreach (var assembly in assemblies)
        {
            var classTypesToApplyInterfaceTo = GetClassTypesToApplyInterfaceTo(assembly, interfaceTypes);

            foreach ((Type interfaceType, ICollection<Type> classTypesToAdd) in classTypesToApplyInterfaceTo)
            {
                if (!result.TryGetValue(interfaceType, out var classTypesForInterface))
                {
                    // Create a new entry if we haven't previously added an interface with its classes
                    classTypesForInterface = new List<Type>();
                    result.Add(interfaceType, classTypesForInterface);
                }

                foreach (var classTypeToAdd in classTypesToAdd)
                {
                    classTypesForInterface.Add(classTypeToAdd);
                }
            }
        }

        return result;
    }

    public IDictionary<Type, ICollection<Type>> GetClassTypesToApplyInterfaceTo(Assembly assembly,
        IEnumerable<Type> interfaceTypes)
    {
        var relevantClasses = assembly.GetTypes()
            .Where(type => type is { IsClass: true }
                           && type.CustomAttributes
                               .Any(data => data.AttributeType.FullName == "Hl7.Fhir.Introspection.FhirTypeAttribute"))
            .ToList();

        var result = new Dictionary<Type, ICollection<Type>>();
        foreach (var interfaceType in interfaceTypes)
        {
            var classTypesForInterface = getClassTypesForInterfaces(interfaceType, relevantClasses).ToList();
            
            if (classTypesForInterface.Any())
            {
                result.Add(interfaceType, classTypesForInterface);
            }
        }

        return result;
    }

    #endregion

    #region Private Methods

    private static IEnumerable<Type> getClassTypesForInterfaces(Type interfaceType, IEnumerable<Type> classTypes)
        => classTypes.Where(classType => shouldClassApplyInterface(interfaceType, classType));

    private static bool shouldClassApplyInterface(Type interfaceType, Type classType)
        => !classImplementsInterface(interfaceType, classType) &&
           classPropertiesMatchInterface(interfaceType, classType) &&
           classMethodsMatchInterface(interfaceType, classType);

    private static bool classImplementsInterface(Type interfaceType, Type classType)
        => classType.GetInterface(interfaceType.FullName!) != null;


    private static bool classPropertiesMatchInterface(Type interfaceType, Type classType)
    {
        var classProperties = classType.GetProperties();
        var interfaceProperties = interfaceType.GetProperties();

        return interfaceProperties.All(interfacePi => classProperties.Any(
                                           classPi => classPi.PropertyType.FullName == interfacePi.PropertyType.FullName
                                                      && classPi.Name == interfacePi.Name));
    }

    private static bool classMethodsMatchInterface(Type interfaceType, Type classType)
    {
        var classMethods = classType.GetMethods();
        var interfaceMethods = interfaceType.GetMethods();

        return interfaceMethods.All(interfaceMi =>
                                        classMethods.Any(classMi => methodSignatureMatch(interfaceMi, classMi)));
    }

    private static bool methodSignatureMatch(MethodInfo interfaceMethod, MethodInfo classMethod)
    {
        if (interfaceMethod.ReturnType != classMethod.ReturnType
            || interfaceMethod.Name != classMethod.Name)
            return false;

        var interfaceParameters = interfaceMethod.GetParameters();
        var classParameters = classMethod.GetParameters();

        var signaturesMatch = true;
        if (interfaceParameters.Length == classParameters.Length)
        {
            for (int i = 0; i < interfaceParameters.Length && signaturesMatch; i++)
            {
                signaturesMatch &= interfaceParameters[i].ParameterType == classParameters[i].ParameterType;
            }
        }
        else
        {
            signaturesMatch = false;
        }

        return signaturesMatch;
    }

    #endregion
}