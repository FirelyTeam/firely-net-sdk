/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable


namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Marks a resource that is coded.
    /// </summary>
    public interface ICoded
    {
        // Empty marker interface
    }

    /// <summary>
    /// Represents a resource that can be coded.
    /// </summary>
    /// <typeparam name="T">The type that is used to codify the resource, usually a (list of) <see cref="Coding"/> or <see cref="CodeableConcept"/>.</typeparam>
    public interface ICoded<T> : ICoded
    {
        T Code { get; set; }
    }

}

#nullable restore