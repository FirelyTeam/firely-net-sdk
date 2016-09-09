/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160810] NEW
    // Suggestions for improvement:
    // 1. Don't throw exception, but register OutcomeOperation issue and return boolean success/failure flag.
    // 2. Allow specification of a maximum recursion depth

    /// <summary>Detect recursive element type profile dependencies.</summary>
    class SnapshotRecursionChecker
    {
        // HashSet is optimized for finding duplicates
        // private HashSet<string> _expandingProfiles;
        Stack<string> _expandingProfiles;

        /// <summary>
        /// Call this method before generating a profile snapshot component. 
        /// Call the <see cref="FinishExpansion"/> method when snapshot generation has completed.
        /// </summary>
        /// <param name="profileUrl">The canonical url of the base profile.</param>
        public void StartExpansion(string profileUrl)
        {
            StartExpansion();
            _expandingProfiles.Push(profileUrl);
        }

        /// <summary>
        /// Call this method before expanding a single snapshot element.
        /// Call the <see cref="FinishExpansion"/> method when snapshot generation has completed.
        /// </summary>
        public void StartExpansion()
        {
            if (_expandingProfiles != null)
            {
                throw Error.InvalidOperation("Recursive calls are not supported. Instead, create a new SnapShotGenerator instance.");
            }
            // Register the root profile Url, to detect recursive dependencies
            // _expandingProfiles = new HashSet<string>();
            // _expandingProfiles.Add(profileUrl);
            _expandingProfiles = new Stack<string>();
        }

        /// <summary>Determines if the snapshot of the specified profile is in the process of being generated.</summary>
        /// <param name="profileUrl">The canonical url of a profile.</param>
        /// <returns><c>true</c> if the profile snapshot is being generated, or <c>false</c> otherwise.</returns>
        public bool IsExpanding(string profileUrl) { return _expandingProfiles.Contains(profileUrl); }

        /// <summary>Returns the url of the profile that is currently being processed by the snapshot generator.</summary>
        public string CurrentProfileUrl { get { return _expandingProfiles.Peek(); } }

        /// <summary>Call this method after profile snapshot generation has completed.</summary>
        public void FinishExpansion()
        {
            Debug.Assert(_expandingProfiles != null);
            _expandingProfiles = null;
        }

        /// <summary>Returns <c>true</c> if the recursion stack is empty.</summary>
        public bool IsCompleted
        {
            get { return _expandingProfiles.Count == 1; }
        }

        /// <summary>
        /// Call this method before generating the snapshot component of a referenced type profile.
        /// Call the <see cref="OnAfterExpandType(string)"/> method on completion.
        /// </summary>
        /// <param name="profileUrl">The canonical url of the type profile.</param>
        /// <param name="path">The path of the element that references the specified type profile.</param>
        public void OnBeforeExpandType(string profileUrl, string path)
        {
            Debug.Assert(_expandingProfiles != null);
            // if (!_expandingProfiles.Add(profileUrl))
            if (IsExpanding(profileUrl))
            {
                throw Error.NotSupported(
                    "Recursive profile snapshot generation detected on element '{0}'.\r\nProfile url stack:\r\n{1}", path, string.Join("\r\n", _expandingProfiles)
                );
            }
            _expandingProfiles.Push(profileUrl);
        }

        /// <summary>Call this method after completing snapshot generation of a referenced type profile.</summary>
        public void OnAfterExpandType(string profileUrl)
        {
            Debug.Assert(_expandingProfiles != null);
            // if (!_expandingProfiles.Remove(profileUrl))
            if (_expandingProfiles.Pop() != profileUrl)
            {
                // _expandingProfiles.Push(profileUrl);
                // Shouldn't happen... indicates an error in the snapshot expansion logic
                throw Error.InvalidOperation("Invalid operation. The completed snapshot profile url '{0}' is not equal to the currently generating snapshot profile url '{1}'.", profileUrl, _expandingProfiles.Peek());
            }
        }
    }
}
