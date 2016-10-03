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
using System.Linq;

namespace Hl7.Fhir.Specification.Snapshot
{
    class SnapshotRecursionStack
    {
        Stack<string> _stack;

        /// <summary>Call this method to initialize the recursion stack before generating a single snapshot element.</summary>
        public void OnStartRecursion()
        {
            if (_stack != null) { throw new InvalidOperationException("Invalid snapshot generator state. Cannot re-initialize while there are remaining snapshots to be generated."); }
            _stack = new Stack<string>();
        }

        /// <summary>Call this method to verify and clear the recursion stack after generating a single snapshot element.</summary>
        public void OnFinishRecursion()
        {
            if (_stack == null)
            {
                throw new InvalidOperationException("Invalid operation in snapshot generator. Cannot finish recursion on idle instance.");
            }
            if (RecursionDepth > 0)
            {
                throw new InvalidOperationException("Invalid snapshot generator state. Cannot finish recursion when the snapshot stack is not empty.");
            }
            _stack = null;
        }

        /// <summary>Call this method to initialize the recursion stack before generating a full snapshot.</summary>
        public void OnBeforeGenerateSnapshot(string profileUri)
        {
            Debug.Print("[SnapshotRecursionStack] OnBeforeExpansion: '{0}'", profileUri);
            OnStartRecursion();
            _stack.Push(profileUri);
        }

        /// <summary>Call this method to verify and clear the recursion stack after generating a full snapshot.</summary>
        public void OnAfterGenerateSnapshot(string profileUri)
        {
            var currentProfileUri = _stack.Pop();
            Debug.Print("[SnapshotRecursionStack] OnAfterExpansion: '{0}'", currentProfileUri);
            if (profileUri != currentProfileUri)
            {
                throw new InvalidOperationException("Invalid snapshot generator state. The specified profile Uri '{0}' does not match the current state: '{1}'".FormatWith(profileUri, CurrentProfileUri));
            }
            OnFinishRecursion();
        }

        /// <summary>Call this method before recursively generating the snapshot of an external element type profile.</summary>
        public void OnBeforeExpandTypeProfile(string typeProfileUri, string path)
        {
            if (IsGenerating(typeProfileUri))
            {
                throw Error.NotSupported(
                    "Error generating snapshot. Recursive profile dependency detected for profile '{0}' on element '{1}'.\r\nProfile url stack:\r\n{2}",
                    typeProfileUri, path, string.Join("\r\n", _stack)
                );
            }
            Debug.Print("[SnapshotRecursionStack] OnBeforeExpandType: '{0}'", typeProfileUri);
            _stack.Push(typeProfileUri);

        }

        /// <summary>Call this method after recursively generating the snapshot of an external element type profile.</summary>
        public void OnAfterExpandTypeProfile(string typeProfileUri, string path)
        {
            Debug.Print("[SnapshotRecursionStack] OnAfterExpandType: '{0}'", typeProfileUri);
            var currentProfileUri = _stack.Pop();
            if (currentProfileUri != typeProfileUri)
            {
                throw Error.InvalidOperation("Invalid snapshot generator state. The profile url '{0}' of the completed snapshot does not match the current state '{1}'.", typeProfileUri, currentProfileUri);
            }

        }

        /// <summary>Returns the profile uri of the currently generating snapshot.</summary>
        public string CurrentProfileUri { get { return RecursionDepth > 0 ? _stack.Peek() : null; } }

        /// <summary>Determines if the snapshot of the profile with the specified uri is being generated.</summary>
        public bool IsGenerating(string profileUri) { return _stack != null && _stack.Any(uri => uri == profileUri); }

        /// <summary>Returns the current recursion depth, i.e. the number of partially generated snapshots on the stack.</summary>
        public int RecursionDepth { get { return _stack != null ? _stack.Count : 0; } }

    }
}
