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
    /// <summary>Internal helper class to detect and prevent recursive snapshot generation.</summary>
    sealed class SnapshotRecursionStack : Stack<string>
    {
        /// <summary>Initialize the recursion stack before generating a single snapshot element.</summary>
        public void OnStartRecursion()
        {
            if (Count > 0)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(OnStartRecursion)}). Cannot re-initialize while there are remaining snapshots to be generated.");
            }
        }

        /// <summary>Verify and clear the recursion stack after generating a single snapshot element.</summary>
        public void OnFinishRecursion()
        {
            if (Count > 0)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(OnFinishRecursion)}). Cannot finish recursion when the snapshot stack is not empty.");
            }
            Clear();
        }

        /// <summary>Initialize the recursion stack before generating a full snapshot.</summary>
        public void OnBeforeGenerateSnapshot(string profileUri)
        {
            Debug.Print($"[{nameof(SnapshotRecursionStack)}.{nameof(OnBeforeGenerateSnapshot)}] '{profileUri}'");
            OnStartRecursion();
            Push(profileUri);
        }

        /// <summary>Verify and clear the recursion stack after generating a full snapshot.</summary>
        public void OnAfterGenerateSnapshot(string profileUri)
        {
            var currentProfileUri = Pop();
            Debug.Print($"[{nameof(SnapshotRecursionStack)}.{nameof(OnAfterGenerateSnapshot)}] '{profileUri}'");
            if (profileUri != currentProfileUri)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state. The specified profile Uri '{profileUri}' does not match the current state: '{CurrentProfileUri}'");
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
            Debug.Print($"[{nameof(SnapshotRecursionStack)}.{nameof(OnBeforeExpandTypeProfile)}] '{typeProfileUri}'");
            Push(typeProfileUri);

        }

        /// <summary>Signal that recursive snapshot generation of an external profile has finished.</summary>
        public void OnAfterExpandTypeProfile(string typeProfileUri, string path)
        {
            Debug.Print($"[{nameof(SnapshotRecursionStack)}.{nameof(OnAfterExpandTypeProfile)}] '{typeProfileUri}'");
            var currentProfileUri = Pop();
            if (currentProfileUri != typeProfileUri)
            {
                throw Error.InvalidOperation($"Invalid snapshot generator state ({nameof(OnAfterExpandTypeProfile)}). The profile url '{typeProfileUri}' of the completed snapshot does not match the current state '{currentProfileUri}'.");
            }

        }

        /// <summary>Returns the uri of the profile for which the snapshot component is currently being generated, or <c>null</c>.</summary>
        public string CurrentProfileUri => Count > 0 ? Peek() : null;

        /// <summary>Determines if the snapshot of the profile with the specified uri is being generated.</summary>
        public bool IsGenerating(string profileUri) => this.Any(uri => uri == profileUri);
    }
}
