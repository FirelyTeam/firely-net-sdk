/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Introspection;
using System;
using System.Reflection;

namespace Hl7.Fhir.Serialization;

[Obsolete("Use BaseFhirJsonDeserializer instead.")]
public class BaseFhirJsonPocoDeserializer : BaseFhirJsonDeserializer
{
    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="assembly">Assembly containing the POCO classes to be used for deserialization.</param>
    [Obsolete("Use the constructor that takes a ModelInspector instead.")]
    public BaseFhirJsonPocoDeserializer(Assembly assembly) : this(ModelInspector.ForAssembly(assembly),
        new FhirJsonConverterOptions())
    {
        // Nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    public BaseFhirJsonPocoDeserializer(ModelInspector inspector) : this(inspector, new FhirJsonConverterOptions())
    {
        // nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirJsonPocoDeserializer(ModelInspector inspector, FhirJsonConverterOptions settings)
        : base(inspector, settings)
    {
        // nothing
    }
}