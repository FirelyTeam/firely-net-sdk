/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160818] Aggregate information about invalid external profile dependencies

    /// <summary>Represents the status of an external profile dependency.</summary>
    public enum SnapshotProfileStatus
    {
        /// <summary>The profile is available and valid.</summary>
        Valid = 0,
        /// <summary>The profile is unavailable.</summary>
        Missing = 1,
        /// <summary>The profile is available, but it has no snapshot.</summary>
        NoSnapshot = 2
    }
    /// <summary>Represents information about an external profile dependency.</summary>
    public struct SnapshotProfileInfo
    {
        internal SnapshotProfileInfo(string url, SnapshotProfileStatus status)
        {
            Url = url;
            Status = status;
        }

        /// <summary>Returns the canonical url of the profile.</summary>
        public string Url { get; private set; }

        /// <summary>Returns the profile status.</summary>
        public SnapshotProfileStatus Status { get; private set; }
    }


    public partial class SnapshotGenerator
    {
        private readonly IList<SnapshotProfileInfo> _invalidExternalProfiles = new List<SnapshotProfileInfo>();

        /// <summary>Returns information about missing or invalid external profiles after snapshot generation.</summary>
        public IList<SnapshotProfileInfo> InvalidExternalProfiles { get { return _invalidExternalProfiles; } }

        private void RegisterInvalidProfile(string url, SnapshotProfileStatus status)
        {
            _invalidExternalProfiles.Add(new SnapshotProfileInfo(url, status));
        }

        private void ClearInvalidProfiles()
        {
            _invalidExternalProfiles.Clear();
        }
    }
}
