using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable enable

namespace Hl7.Fhir.ElementModel;

public partial record PocoNode
{
    string ITypedElement.InstanceType =>
        Poco switch
        {
            DataType => Poco.TypeName,
            BackboneElement => "BackboneElement",
            Element when Poco.TypeName.Contains('.') => "Element",
            _ => Poco.TypeName
        };

    object? ITypedElement.Value => ValueInternal;

    // needed for ITE
    protected virtual object? ValueInternal => null;

    string ITypedElement.Location => (Index, Parent) switch
    {
        // if we have an index, write it
        ({ } idx, { } parent) => $"{((ITypedElement)parent).Location}.{Name}[{idx}]",
        // if we do not, write 0 as idx
        (_, { } parent) => $"{((ITypedElement)parent).Location}.{Name}[0]",
        // if we have neither, we are the root.
        _ => Name
    };
    
    [TemporarilyChanged] // Parent should return PocoNode, not PocoNodeOrList. This will be solved in another branch.
    IElementDefinitionSummary? ITypedElement.Definition
    {
        get
        {
            if (FindInspector() is not { } inspector)
                return null;

            if (this.Parent is not {} node) 
                return ElementDefinitionSummary.ForRoot(inspector.FindOrImportClassMapping(Poco.GetType()), Name);
            
            var classMapping = inspector.FindOrImportClassMapping(node.Poco.GetType());
            return classMapping?.FindMappedElementByName(Name);
        }
    }

    [TemporarilyChanged] // I am refactoring the extensions in another branch. This should go into those extensions. To avoid conflicts, I implement it here for now.
    internal ModelInspector? FindInspector() => ((IAnnotated)this).Annotation<ModelInspector>() ?? Parent?.SingleOrDefault()?.FindInspector();
    
    IEnumerable<ITypedElement> ITypedElement.Children(string? name) => name is null
        ? Children().SelectMany(node => node)
        : Child(name) ?? Enumerable.Empty<PocoNode>();

    protected virtual string? TextInternal => null; 
    string? ISourceNode.Text => TextInternal;
    
    private Lazy<string> SourceName => new (() => 
        Poco is DataType { TypeName: var tn } && 
        ((ITypedElement)this).Definition!.IsChoiceElement 
            ? Name + tn.Capitalize() 
            : Name
    );

    string ISourceNode.Location =>
        (Index, Parent) switch
        {
            // if we have an index, write it
            ({ } idx, { } parent) => $"{((ITypedElement)parent).Location}.{SourceName.Value}[{idx}]",
            // if we do not, write 0 as idx
            (_, { } parent) => $"{((ITypedElement)parent).Location}.{SourceName.Value}[0]",
            // if we have neither, we are the root.
            _ => SourceName.Value
        };

    IEnumerable<ISourceNode> ISourceNode.Children(string? name)
    {
        if (name is null) return Children().SelectMany(node => node);
        
        var trueElementName = FindInspector()?
            .FindOrImportClassMapping(Poco.GetType())?
            .FindMappedElementByChoiceName(name)?.Name;
        
        return Child(trueElementName ?? name) ?? Enumerable.Empty<ISourceNode>();
    }
}