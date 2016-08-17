/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Snapshot
{
    // Event declarations for the SnapshotGenerator class

    public partial class SnapshotGenerator
    {
        /// <summary>An event that notifies clients when a differential constraint has been processed.</summary>
        public event SnapshotConstraintHandler Constraint;

        /// <summary>Raise the <see cref="Constraint"/> event to notify the client that a differential constraint has been processed.</summary>
        /// <param name="element">A reference to the snapshot element with merged differential constraints.</param>
        internal void OnConstraint(Element element)
        {
            if (element == null) { throw new ArgumentNullException("element"); }
            
            // Configurable default behavior: mark changed elements
            if (_settings.MarkChanges)
            {
                element.SetExtension(CHANGED_BY_DIFF_EXT, new FhirBoolean(true));
            }

            var handler = Constraint;
            if (handler != null)
            {
                var args = new SnapshotConstraintEventArgs(element);
                handler(this, args);
            }
        }

        /// <summary>An event that notifies clients that the base profile has been resolved.</summary>
        public event SnapshotProfileHandler BaseProfileResolved;

        /// <summary>Raise the <see cref="BaseProfileResolved"/> event to notify the client that the associated base profile has been resolved.</summary>
        /// <param name="profile"></param>
        internal void OnBaseProfileResolved(StructureDefinition profile)
        {
            if (profile == null) { throw new ArgumentNullException("profile"); }
            var handler = BaseProfileResolved;
            if (handler != null)
            {
                var args = new SnapshotProfileEventArgs(profile);
                handler(this, args);
            }
        }

        /// <summary>
        /// An event that notifies clients when a base profile element definition has been resolved.
        /// After the event handler returns, the snapshot generator merges any associated
        /// differential constraints and adds the resulting element definition to the snapshot.
        /// </summary>
        public event SnapshotElementHandler BaseElementResolved;

        /// <summary>Raise the <see cref="BaseElementResolved"/> event to notify the client when a base element definition has been resolved.</summary>
        internal void OnBaseElementResolved(ElementDefinition element)
        {
            if (element == null) { throw new ArgumentNullException("element"); }
            var handler = BaseElementResolved;
            if (handler != null)
            {
                var args = new SnapshotElementEventArgs(element);
                handler(this, args);
            }
        }
    }

    /// <summary>Event arguments for the <see cref="SnapshotConstraintHandler"/> event delegate.</summary>
    public class SnapshotConstraintEventArgs : EventArgs
    {
        private readonly Element _element;

        public SnapshotConstraintEventArgs(Element element) : base()
        {
            _element = element;
        }

        /// <summary>Returns a reference to a constrained snapshot element definition or property.</summary>
        public Element Element { get { return _element; } }
    }

    /// <summary>A delegate type for hooking up <see cref="SnapshotGenerator.Constraint"/> events.</summary>
    public delegate void SnapshotConstraintHandler(object sender, SnapshotConstraintEventArgs e);


    /// <summary>Event arguments for the <see cref="SnapshotProfileHandler"/> event delegate.</summary>
    public class SnapshotProfileEventArgs : EventArgs
    {
        private readonly StructureDefinition _profile;

        public SnapshotProfileEventArgs(StructureDefinition profile) : base()
        {
            _profile = profile;
        }

        /// <summary>Returns a reference to a profile.</summary>
        public StructureDefinition Profile { get { return _profile; } }
    }

    /// <summary>A delegate type for hooking up <see cref="SnapshotGenerator.BaseProfileResolved"/> events.</summary>
    public delegate void SnapshotProfileHandler(object sender, SnapshotProfileEventArgs e);


    /// <summary>Event arguments for the <see cref="SnapshotElementHandler"/> event delegate.</summary>
    public class SnapshotElementEventArgs : EventArgs
    {
        private readonly ElementDefinition _element;

        public SnapshotElementEventArgs(ElementDefinition element) : base()
        {
            _element = element;
        }

        /// <summary>Returns a reference to an element definition.</summary>
        public ElementDefinition Element { get { return _element; } }
    }

    public delegate void SnapshotElementHandler(object sender, SnapshotElementEventArgs e);

}
