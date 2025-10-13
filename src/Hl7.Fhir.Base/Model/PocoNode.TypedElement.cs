#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model;

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
    
    IElementDefinitionSummary? ITypedElement.Definition
    {
        get
        {
            if (this.FindInspector() is not { } inspector)
                return null;

            // we could get definitions with FindOrImportClassMapping, but then we're modifying inspector mappings
            // which either should already have the type, or is expected to be immutable!
            if (this.Parent is {} node && inspector.FindClassMapping(node.Poco.GetType()) is {} classMapping)
                return classMapping.FindMappedElementByName(Name);

            if (inspector.FindClassMapping(Poco.GetType()) is {} cm)
                return ElementDefinitionSummary.ForRoot(cm, Name);

            return null;
        }
    }
    
    IEnumerable<ITypedElement> ITypedElement.Children(string? name) => name is null
        ? Children().SelectMany(node => node)
        : Child(name) ?? Enumerable.Empty<PocoNode>();

    protected virtual string? TextInternal => null; 
    string? ISourceNode.Text => TextInternal;
    
    private Lazy<string> SourceName => new (() =>
    {
        if (Poco is not DataType dt)
            return Name;
        
        return ((ITypedElement)this).Definition switch
        {
            { IsChoiceElement: true } => Name + dt.TypeName.Capitalize(),
            null when dt.HasAnnotation<ChoiceElementAnnotation>() => Name + dt.TypeName.Capitalize(),
            _ => Name
        };
    });
    
    string ISourceNode.Name => SourceName.Value;

    string ISourceNode.Location =>
        (Index, Parent) switch
        {
            // if we have an index, write it
            ({ } idx, { } parent) => $"{((ISourceNode)parent).Location}.{SourceName.Value}[{idx}]",
            // if we do not, write 0 as idx
            (_, { } parent) => $"{((ISourceNode)parent).Location}.{SourceName.Value}[0]",
            // if we have neither, we are the root.
            _ => SourceName.Value
        };

    IEnumerable<ISourceNode> ISourceNode.Children(string? name)
    {
        if (name is null) return Children().SelectMany(node => node);
        
        var trueElementName = this.FindInspector()?
            .FindOrImportClassMapping(Poco.GetType())?
            .FindMappedElementByChoiceName(name)?.Name;
        
        return Child(trueElementName ?? name) ?? Enumerable.Empty<ISourceNode>();
    }
}