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

        /// <summary>Call this method before expanding a profile.</summary>
        /// <param name="profileUrl"></param>
        public void StartExpansion(string profileUrl)
        {
            if (_expandingProfiles != null)
            {
                throw Error.InvalidOperation("Recursive calls are not supported. Instead, create a new SnapShotGenerator instance.");
            }
            // Register the root profile Url, to detect recursive dependencies
            // _expandingProfiles = new HashSet<string>();
            // _expandingProfiles.Add(profileUrl);
            _expandingProfiles = new Stack<string>();
            _expandingProfiles.Push(profileUrl);
        }

        public bool IsExpanding(string profileUrl)
        {
            return _expandingProfiles.Contains(profileUrl);
        }

        /// <summary>Call this method after the profile has been expanded.</summary>
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

        /// <summary>Call this method before expanding an element type profile.</summary>
        /// <param name="profileUrl"></param>
        /// <param name="path"></param>
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

        /// <summary>Call this method after an element type profile has been expanded.</summary>
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
