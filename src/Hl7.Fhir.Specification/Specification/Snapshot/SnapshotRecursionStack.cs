/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>Internal helper class to detect and prevent recursive snapshot generation.</summary>
    sealed class SnapshotRecursionStack
    {
        Stack<SnapshotRecursionStackState> _stack;

        sealed class SnapshotRecursionStackState
        {
            public SnapshotRecursionStackState(string profileUri)
            {
                if (profileUri == null) { throw Error.ArgumentNull(nameof(profileUri)); }
                ProfileUri = profileUri;
                Navigator = null;
            }
            /// <summary>Canonical uri of a profile for which the snapshot is being generated.</summary>
            public readonly string ProfileUri;

            /// <summary>
            /// Reference to the <see cref="ElementDefinitionNavigator"/> that is generating the snapshot.
            /// Allows access to already generated elements.
            /// </summary>
            public ElementDefinitionNavigator Navigator { get; internal set; }
        }

        /// <summary>Initialize the recursion stack before generating a single snapshot element.</summary>
        public void OnStartRecursion()
        {
            if (_stack != null)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(OnStartRecursion)}). Cannot start a new snapshot generation. The previous operation has not finished.");
            }
            _stack = new Stack<SnapshotRecursionStackState>();
        }

        /// <summary>Verify and clear the recursion stack after generating a single snapshot element.</summary>
        public void OnFinishRecursion()
        {
            if (_stack == null)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(OnFinishRecursion)}). Cannot finish snapshot generation. No snapshot is currently being generated.");
            }
            if (_stack.Count > 0)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(OnFinishRecursion)}). Cannot finish the operation while snapshots are still being generated.");
            }
            _stack = null;
        }

        /// <summary>Initialize the recursion stack before generating a full snapshot.</summary>
        public void OnBeforeGenerateSnapshot(string profileUri)
        {
            // Debug.Print($"[{nameof(SnapshotRecursionStack)}.{nameof(OnBeforeGenerateSnapshot)}] '{profileUri}'");
            OnStartRecursion();
            _stack.Push(new SnapshotRecursionStackState(profileUri));
        }

        /// <summary>Verify and clear the recursion stack after generating a full snapshot.</summary>
        public void OnAfterGenerateSnapshot(string profileUri)
        {
            validateStackIsNotEmpty(nameof(OnAfterGenerateSnapshot));
            var currentState = _stack.Pop();
            var currentProfileUri = currentState.ProfileUri;
            // Debug.Print($"[{nameof(SnapshotRecursionStack)}.{nameof(OnAfterGenerateSnapshot)}] '{profileUri}'");
            if (profileUri != currentProfileUri)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(OnAfterGenerateSnapshot)}). The specified profile Uri '{profileUri}' does not match the current state: '{CurrentProfileUri}'");
            }
            OnFinishRecursion();
        }

        /// <summary>Verify recursive snapshot generation of the external profile with the specified url.</summary>
        /// <exception cref="NotSupportedException">Thrown when detecting recursive snapshot generation.</exception>
        public void OnBeforeExpandTypeProfile(string typeProfileUri, string path)
        {
            if (IsGenerating(typeProfileUri))
            {
                throw Error.NotSupported(
                    $"Error generating snapshot. Recursive profile dependency detected for profile '{typeProfileUri}' on element '{path}'.\r\nProfile url stack:\r\n{string.Join("\r\n", this)}"
                );
            }
            // Debug.Print($"[{nameof(SnapshotRecursionStack)}.{nameof(OnBeforeExpandTypeProfile)}] '{typeProfileUri}'");
            _stack.Push(new SnapshotRecursionStackState(typeProfileUri));

        }

        /// <summary>Signal that recursive snapshot generation of an external profile has finished.</summary>
        public void OnAfterExpandTypeProfile(string typeProfileUri, string path)
        {
            // Debug.Print($"[{nameof(SnapshotRecursionStack)}.{nameof(OnAfterExpandTypeProfile)}] '{typeProfileUri}'");
            validateStackIsNotEmpty(nameof(OnAfterExpandTypeProfile));
            var currentState = _stack.Pop();
            var currentProfileUri = currentState.ProfileUri;
            if (currentProfileUri != typeProfileUri)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(OnAfterExpandTypeProfile)}). The profile url '{typeProfileUri}' of the completed snapshot does not match the current state '{currentProfileUri}'.");
            }

        }

        /// <summary>Returns the uri of the profile for which the snapshot component is currently being generated, or <c>null</c>.</summary>
        public string CurrentProfileUri => _stack.Count > 0 ? _stack.Peek().ProfileUri : null;

        public void RegisterSnapshotNavigator(string profileUri, ElementDefinitionNavigator navigator)
        {
            if (navigator == null) { throw Error.ArgumentNull(nameof(navigator)); }
            validateStackIsNotEmpty(nameof(RegisterSnapshotNavigator));
            var state = _stack.Peek();
            if (state.ProfileUri != profileUri)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(RegisterSnapshotNavigator)}). The profile url '{profileUri}' of the completed snapshot does not match the current state '{state.ProfileUri}'.");
            }
            // Navigator reference is write-once
            if (state.Navigator != null) { throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(RegisterSnapshotNavigator)}): The navigator for profile '{profileUri}' is already initialized."); }
            state.Navigator = navigator;
        }

        public ElementDefinitionNavigator ResolveSnapshotNavigator(string profileUri)
        {
            var match = _stack.FirstOrDefault(state => state.ProfileUri == profileUri);
            return match?.Navigator; // Returns null for default state (no match)
        }

        /// <summary>Determines if the snapshot of the profile with the specified uri is being generated.</summary>
        public bool IsGenerating(string profileUri) => _stack.Any(state => state.ProfileUri == profileUri);

        public int RecursionDepth => _stack.Count;

        void validateStackIsNotEmpty([CallerMemberName] string memberName = "")
        {
            if (_stack == null || _stack.Count == 0)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({memberName}). Cannot operate on empty recursion stack.");
            }
        }

    }

}
