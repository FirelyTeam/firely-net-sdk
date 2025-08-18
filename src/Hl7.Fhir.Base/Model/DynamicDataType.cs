#nullable enable
using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model;

/// <summary>
/// An interface for dynamic data types that hold any element.
/// </summary>
public interface IDynamicType
{
    public string? DynamicTypeName { get; set; }
}

/// <summary>
/// A dynamic data type that can hold any element.
/// </summary>
[Serializable]
[DataContract]
[FhirType("DynamicDataType","http://fire.ly/fhir/StructureDefinition/DynamicDataType")]
public class DynamicDataType : DataType, IDynamicType
{
    public string? DynamicTypeName { get; set; }

    public override string TypeName => DynamicTypeName ?? base.TypeName;
    
    protected internal override Base DeepCopyInternal()
    {
        var instance = new DynamicDataType { DynamicTypeName = DynamicTypeName };
        CopyToInternal(instance);
        return instance;
    }
}



/// <summary>
/// A dynamic resource that can hold any element and is a domain resource.
/// </summary>
[Serializable]
[DataContract]
[FhirType("DynamicResource","http://fire.ly/fhir/StructureDefinition/DynamicResource")]
public class DynamicResource : DomainResource, IDynamicType
{
    public string? DynamicTypeName { get; set; }

    public override string TypeName => DynamicTypeName ?? base.TypeName;
    
    protected internal override Base DeepCopyInternal()
    {
        var instance = new DynamicResource { DynamicTypeName = DynamicTypeName };
        CopyToInternal(instance);
        return instance;
    }
}


/// <summary>
/// A dynamic resource that can hold any element.
/// </summary>
[Serializable]
[DataContract]
[FhirType("DynamicInfraResource","http://fire.ly/fhir/StructureDefinition/DynamicInfraResource")]
public class DynamicInfraResource : Resource, IDynamicType
{
    public string? DynamicTypeName { get; set; }

    public override string TypeName => DynamicTypeName ?? base.TypeName;
    
    protected internal override Base DeepCopyInternal()
    {
        var instance = new DynamicInfraResource { DynamicTypeName = DynamicTypeName };
        CopyToInternal(instance);
        return instance;
    }
}


/// <summary>
/// A dynamic primitive that can hold any element.
/// </summary>
[Serializable]
[DataContract]
[FhirType("DynamicPrimitive","http://fire.ly/fhir/StructureDefinition/DynamicPrimitive")]
public class DynamicPrimitive : PrimitiveType, IDynamicType
{
    public string? DynamicTypeName { get; set; }

    public override string TypeName => DynamicTypeName ?? base.TypeName;

    [FhirElement("value", IsPrimitiveValue=true, XmlSerialization=XmlRepresentation.XmlAttr, InSummary=true, Order=1000)]
    [DataMember]
    public object? Value
    {
        get => JsonValue;
        set { JsonValue = value; OnPropertyChanged("Value"); }
    }

    protected internal override Base DeepCopyInternal()
    {
        var instance = new DynamicPrimitive { DynamicTypeName = DynamicTypeName };
        CopyToInternal(instance);
        return instance;
    }

    protected internal override Any? TryConvertToSystemTypeInternal() => null;

    protected internal override COVE? ValidateObjectValue(PocoValidationContext? validationContext) =>
        JsonValue is string or bool or decimal or int
            ? null
            : COVE.INCORRECT_LITERAL_VALUE_TYPE(validationContext, JsonValue, TypeName);
}