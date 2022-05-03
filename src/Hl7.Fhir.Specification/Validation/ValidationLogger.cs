/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// This class is used to track which resources/contained resources/bundled resources are already
    /// validated, and what the previous result was. Since it keeps track of running validations, it
    /// can also be used to detect loops.
    /// </summary>
    internal class ValidationLogger
    {
        private enum ValidationStatus
        {
            Started,
            Success,
            Failure
        }

        private class Entry
        {
            public Entry(string location, string profileUrl, ValidationStatus status)
            {
                Location = location;
                ProfileUrl = profileUrl;
                Status = status;
            }

            public string Location;
            public string ProfileUrl;
            public ValidationStatus Status;
        }

        private readonly Dictionary<string, Entry> _data = new();

        public OperationOutcome Start(string location, string profileUrl, Func<OperationOutcome> validator)
        {
            var key = $"{location}:{profileUrl}";

            if (_data.TryGetValue(key, out var existing))
            {
                if (existing.Status == ValidationStatus.Started)
                    throw new InvalidOperationException($"Detected a loop: instance data inside '{location}' refers back to itself.");

                // If the validation has been run before, return an outcome with the same result.
                // Note: we don't keep a copy of the original outcome since it has been included in the
                // total result (at the first run) and keeping them around costs a lot of memory.
                var repeatedOutcome = new OperationOutcome();
                if (existing.Status == ValidationStatus.Failure)
                    repeatedOutcome.AddIssue("Validation of this resource has been performed before with a failure result.",
                        Issue.PROCESSING_REPEATED_ERROR);

                return repeatedOutcome;
            }
            else
            {
                // This validation is run for the first time
                var newEntry = new Entry(location, profileUrl, ValidationStatus.Started);
                _data.Add(key, newEntry);

                var result = validator();
                newEntry.Status = result.Success ? ValidationStatus.Success : ValidationStatus.Failure;
                return result;
            }
        }

        public int Count => _data.Values.Count;
    }
}
