/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

namespace Hl7.Fhir.Validation
{
    internal class Counter
    {
        public int Increase() => Value += 1;

        public int Value { get; private set; }
    }


    /// <summary>
    /// Hold the state relevant to a single run of the validator.
    /// </summary>
    internal class ValidationState
    {
        /// <summary>
        /// State to be kept for one full run of the validator.
        /// </summary>
        public class GlobalState
        {
            /// <summary>
            /// This keeps track of all validations done on external resources
            /// referenced from the original root resource passed to the Validate() call.
            /// </summary>
            public ValidationLogger ExternalValidations { get; set; } = new();

            public Counter ResourcesValidated { get; set; } = new();
        }

        public GlobalState Global { get; private set; } = new();

        /// <summary>
        /// State to be kept for an instance encountered during validation
        /// (e.g. if the resource under validation references an external resource,
        /// the validation of that resource will have its own <see cref="InstanceState"/>.
        /// </summary>
        public class InstanceState
        {
            /// <summary>
            /// The URL where the current instance was retrieved (if known).
            /// </summary>
            public string? ExternalUrl { get; set; }

            public ValidationLogger InternalValidations { get; set; } = new();
        }

        public InstanceState Instance { get; private set; } = new();

        public ValidationState NewInstanceScope() =>
            new()
            {
                Global = Global
            };
    }
}
