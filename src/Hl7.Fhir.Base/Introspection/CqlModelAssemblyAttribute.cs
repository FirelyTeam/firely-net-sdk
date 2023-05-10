/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Introspection
{
    /// <summary>
    /// Signals that the assembly contains classes that define metadata for 
    /// types used in CQL.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    public sealed class CqlModelAssemblyAttribute : Attribute
    {
        public CqlModelAssemblyAttribute(string name, string version, string url, Type patientClass, string birthdatePropertyName)
        {
            Name = name;
            Version = version;
            Url = url;
            PatientClass = patientClass;
            BirthdatePropertyName = birthdatePropertyName;
        }

        /// <summary>
        /// The namespace name for all types in the assembly. E.g. "FHIR".
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The version of the model this assembly represents.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// The url that uniquely identifies this namespace.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The class in this model that serves as the Patient class in the Patient context.
        /// </summary>
        public Type PatientClass { get; }

        /// <summary>
        /// The property within the Patient class that contains the birthdate. Used to
        /// implement the <c>Age()</c> related functions.
        /// </summary>
        public string BirthdatePropertyName { get; }
    }
}
