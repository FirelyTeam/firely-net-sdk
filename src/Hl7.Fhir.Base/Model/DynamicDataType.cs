#nullable enable
using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Introspection;
using System;
using System.Runtime.Serialization;

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
/// A dynamic resource that can hold any element.
/// </summary>
[Serializable]
[DataContract]
[FhirType("DynamicResource","http://fire.ly/fhir/StructureDefinition/DynamicResource")]
public class DynamicResource : Resource, IDynamicType
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
/// A dynamic primitive that can hold any element.
/// </summary>
[Serializable]
[DataContract]
[FhirType("DynamicPrimitive","http://fire.ly/fhir/StructureDefinition/DynamicPrimitive")]
public class DynamicPrimitive : PrimitiveType, IDynamicType
{
    public string? DynamicTypeName { get; set; }

    public override string TypeName => DynamicTypeName ?? base.TypeName;
    
    protected internal override Base DeepCopyInternal()
    {
        var instance = new DynamicPrimitive { DynamicTypeName = DynamicTypeName };
        CopyToInternal(instance);
        return instance;
    }

    protected internal override Any? TryConvertToSystemTypeInternal() => null;
}