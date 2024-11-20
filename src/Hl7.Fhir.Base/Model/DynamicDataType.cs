#nullable enable
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

    public object this[string key] { get; set; }
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

    public void Add(string arg1, object arg2) => this.SetValue(arg1, arg2);

    // TODO: One may wonder whether normal resources should have this as well.
    public object this[string key]
    {
        get => this.AsReadOnlyDictionary()[key];
        set => SetValue(key, value);
    }
}



/// <summary>
/// A dynamic resource that can hold any element.
/// </summary>
[Serializable]
[DataContract]
[FhirType("DynamicDataType","http://fire.ly/fhir/StructureDefinition/DynamicResource")]
public class DynamicResource : Resource, IDynamicType
{
    public string? DynamicTypeName { get; set; }

    public override string TypeName => DynamicTypeName ?? base.TypeName;

    public void Add(string arg1, object arg2) => this.SetValue(arg1, arg2);

    // TODO: One may wonder whether normal resources should have this as well.
    public object this[string key]
    {
        get => this.AsReadOnlyDictionary()[key];
        set => SetValue(key, value);
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

    public void Add(string arg1, object arg2) => this.SetValue(arg1, arg2);

    // TODO: One may wonder whether normal resources should have this as well.
    public object this[string key]
    {
        get => this.AsReadOnlyDictionary()[key];
        set => SetValue(key, value);
    }
}