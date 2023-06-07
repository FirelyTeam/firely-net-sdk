﻿using Hl7.Fhir.InterfaceApplier.CLI.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;

namespace Hl7.Fhir.InterfaceApplier.CLI.Implementations;

public class InterfaceApplierService : IInterfaceApplierService
{
    #region Private Fields

    private readonly ILogger<InterfaceApplierService> _logger;

    #endregion

    #region Constructors

    public InterfaceApplierService(ILogger<InterfaceApplierService> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Public Methods

    public void Apply(IEnumerable<string> sourceFilesDirectories,
        IDictionary<Type, ICollection<Type>> classTypesForInterface)
    {
        var sourceFilePaths = sourceFilesDirectories.SelectMany(sourceFilesDirectory => Directory.GetFiles(sourceFilesDirectory, "*.cs", SearchOption.AllDirectories))
            .Where(path => path.Contains($"{Path.DirectorySeparatorChar}Generated{Path.DirectorySeparatorChar}"));
        var sources = sourceFilePaths.Select(sourceFilePath => CSharpSyntaxTree.ParseText(File.ReadAllText(sourceFilePath), path: sourceFilePath)).ToList();

        foreach ((Type interfaceType, ICollection<Type> classTypes) in classTypesForInterface)
        {
            applyInterfaceToClasses(interfaceType, classTypes, sources);
        }
    }

    #endregion

    #region Private  Methods

    private void applyInterfaceToClasses(Type interfaceType, ICollection<Type> classTypes, List<SyntaxTree> sources)
    {
        foreach (var classType in classTypes)
        {
            applyInterfaceToClass(interfaceType, classType, sources);
        }
        _logger.LogDebug("Applied interface {0} on {1} classes", interfaceType.FullName, classTypes.Count);
    }

    private void applyInterfaceToClass(Type interfaceType, Type classType, ICollection<SyntaxTree> sources)
    {
        var sourcesForClassType = getSourcesForClassType(classType, sources).ToList();
        _logger.LogDebug("Found {0} sources for class type {1}", sourcesForClassType.Count, classType.FullName);
        applyInterfaceToSources(interfaceType, classType, sourcesForClassType);
    }

    private IEnumerable<SyntaxTree> getSourcesForClassType(Type classType, IEnumerable<SyntaxTree> sources) 
        => sources.Where(source => isClassTypeSource(source, classType));

    private static bool isClassTypeSource(SyntaxTree source, Type classType)
    {
        var root = source.GetRoot();
        var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
        return classDeclarations.Any(declaration =>
        {
            if (declaration.Identifier.ValueText != classType.Name)
            {
                return false;
            }

            if (classType.IsNested)
            {
                var currentClassType = classType.DeclaringType;
                while (currentClassType != null)
                {
                    if (declaration.Parent is ClassDeclarationSyntax parentClass
                        && parentClass.Identifier.ValueText == currentClassType.Name)
                    {
                        currentClassType = currentClassType.DeclaringType;
                        continue;
                    }

                    return false;
                }
            }
            

            return true;
        });
    }

    private void applyInterfaceToSources(Type interfaceType, Type classType, IEnumerable<SyntaxTree> sources)
    {
        foreach (var source in sources)
        {
            applyInterfaceToSource(interfaceType, classType, source);
        }
    }

    private void applyInterfaceToSource(Type interfaceType, Type classType, SyntaxTree source)
    {
        var root = source.GetRoot();

        var classDeclaration = root.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Single(syntax => syntax.Identifier.Text == classType.Name);

        var interfaceName = interfaceType.Name;
        var interfaceBaseType = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(interfaceName));
        
        var existingBaseTypes = classDeclaration.BaseList?.Types.ToList() ?? new List<BaseTypeSyntax>();
        var newBaseTypes = existingBaseTypes.Concat(new[] { interfaceBaseType });
        var newBaseList = SyntaxFactory.BaseList(SyntaxFactory.SeparatedList(newBaseTypes));
        var newClassDeclaration = classDeclaration.WithBaseList(newBaseList);
        var newRoot = root.ReplaceNode(classDeclaration, newClassDeclaration);
        var newSourceCode = newRoot.ToFullString();

        var filePath = source.FilePath;
        _logger.LogDebug("Added interface {0} to class {1} in file {2}", interfaceName, classType.Name, filePath);

        File.WriteAllText(filePath, newSourceCode);
    }

    #endregion
}