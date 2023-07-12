/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable


namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Represents the patient subject.
    /// </summary>
    public interface IPatient
    {
        Date BirthDate { get; }
    }
}
#nullable restore