#nullable enable
using Hl7.Fhir.Introspection;
using System;
using System.Reflection;

namespace Hl7.Fhir.Serialization;

[Obsolete("Use BaseFhirXmlDeserializer instead.")]
public class BaseFhirXmlPocoDeserializer : BaseFhirXmlDeserializer
{
    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="assembly">Assembly containing the POCO classes to be used for deserialization.</param>
    public BaseFhirXmlPocoDeserializer(Assembly assembly) : this(assembly, new DeserializerSettings())
    {
        // nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="assembly">Assembly containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirXmlPocoDeserializer(Assembly assembly, DeserializerSettings settings)
        : this(ModelInspector.ForAssembly(assembly), settings)
    {
        // Nothing
    }


    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    public BaseFhirXmlPocoDeserializer(ModelInspector inspector) : this(inspector, new DeserializerSettings())
    {
        // nothing
    }


    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirXmlPocoDeserializer(ModelInspector inspector, DeserializerSettings settings) : base(inspector, settings)
    {
        // Nothing
    }
}