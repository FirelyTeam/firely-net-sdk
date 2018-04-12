/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

#define BEFORE_EXPAND_ELEMENT_EVENT

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Specification.Snapshot
{
    // Event declarations for the SnapshotGenerator class

    public partial class SnapshotGenerator
    {
        /// <summary>
        /// An event that notifies clients when a differential constraint has been processed.
        /// The specified <see cref="Element"/> instance is the result of merging the base
        /// element with the associated differential constraint(s).
        /// The event handler can inspect and optionally modify the element.
        /// The result will be included in the snapshot.
        /// </summary>
        public event SnapshotConstraintHandler Constraint;

        /// <summary>Raise the <see cref="Constraint"/> event to notify the client that a differential constraint has been processed.</summary>
        /// <param name="element">A reference to the snapshot element with merged differential constraints.</param>
        internal void OnConstraint(Element element)
        {
            if (element == null) { throw new ArgumentNullException(nameof(element)); }

            // Configurable default behavior: mark changed elements
            // [WMR 20160915] Mark element as chaged
            markChangedByDiff(element);

            var handler = Constraint;
            if (handler != null)
            {
                var args = new SnapshotConstraintEventArgs(element);
                handler(this, args);
            }
        }

        /// <summary>
        /// An event that notifies clients when a base profile has been resolved.
        /// </summary>
        /// <remarks>
        /// The <see cref="SnapshotBaseProfileEventArgs.BaseProfile"/> event argument returns a
        /// reference to the original base profile instance as returned by the artifact source.
        /// Modifications to this instance will affect the original cached artifact
        /// and will be visible to other consumers of the artifact source.
        /// </remarks>
        public event SnapshotBaseProfileHandler PrepareBaseProfile;

        /// <summary>
        /// Raise the <see cref="PrepareBaseProfile"/> event to notify the client
        /// when the associated base profile has been resolved and prepared for merging.
        /// </summary>
        /// <param name="profile">A profile <see cref="StructureDefinition"/> instance.</param>
        /// <param name="baseProfile">The associated base profile <see cref="StructureDefinition"/> instance.</param>
        internal void OnPrepareBaseProfile(StructureDefinition profile, StructureDefinition baseProfile)
        {
            if (profile == null) { throw new ArgumentNullException(nameof(profile)); }
            if (baseProfile == null) { throw new ArgumentNullException(nameof(baseProfile)); }
            var handler = PrepareBaseProfile;
            if (handler != null)
            {
                var args = new SnapshotBaseProfileEventArgs(profile, baseProfile);
                handler(this, args);
            }
        }

        /// <summary>
        /// An event that notifies clients when the generator initializes a new snapshot element.
        /// The specified element is cloned from the base profile and the base path has been fixed.
        /// The event handler can inspect and optionally modify the element.
        /// After the event handler returns, the snapshot generator merges the associated
        /// differential constraints, if they exist.
        /// </summary>
        public event SnapshotElementHandler PrepareElement;

        /// <summary>Raise the <see cref="PrepareElement"/> event to notify the client when an element definition is being prepared for merging.</summary>
        internal void OnPrepareElement(ElementDefinition element, StructureDefinition baseStructure, ElementDefinition baseElement)
        {
            if (element == null) { throw new ArgumentNullException(nameof(element)); }
            var handler = PrepareElement;
            if (handler != null)
            {
                var args = new SnapshotElementEventArgs(element, baseStructure, baseElement);
                handler(this, args);
            }
        }

        /// <summary>Indicates if the <see cref="PrepareElement"/> event has any active subscribers.</summary>
        bool MustRaisePrepareElement => PrepareElement != null;


        // [WMR 20170105] NEW

        /// <summary>
        /// An event that notifies clients when the snapshot generator must determine wether to expand a specific profile element.
        /// The event handler can inspect and optionally modify the <see cref="SnapshotExpandElementEventArgs.MustExpand"/> flag.
        /// If the flag equals <c>true</c>, then the snapshot generator will expand the current element.
        /// </summary>
        public event SnapshotExpandElementHandler BeforeExpandElement;

        /// <summary>
        /// Raise the <see cref="BeforeExpandElement"/> event to notify the client when deciding wether to expand the current element.
        /// The client can modify the value of the <paramref name="mustExpand"/> parameter to control expansion of specific elements.
        /// Warning: recursively expanding all profile elements may cause infinite recursion!
        /// </summary>
        internal void OnBeforeExpandElement(ElementDefinition element, bool hasChildren, ref bool mustExpand)
        {
            if (element == null) { throw new ArgumentNullException(nameof(element)); }

            var handler = BeforeExpandElement;
            if (handler != null)
            {
                var args = new SnapshotExpandElementEventArgs(element, hasChildren, mustExpand);
                handler(this, args);
                mustExpand = args.MustExpand;
            }
        }
    }


    /// <summary>Event arguments for the <see cref="SnapshotConstraintHandler"/> event delegate.</summary>
    public sealed class SnapshotConstraintEventArgs : EventArgs
    {
        public SnapshotConstraintEventArgs(Element element) : base() { Element = element; }

        /// <summary>Returns a reference to a constrained snapshot element definition or property.</summary>
        public Element Element { get; }
    }

    /// <summary>A delegate type for hooking up <see cref="SnapshotGenerator.Constraint"/> events.</summary>
    public delegate void SnapshotConstraintHandler(object sender, SnapshotConstraintEventArgs e);


    /// <summary>Event arguments for the <see cref="SnapshotBaseProfileHandler"/> event delegate.</summary>
    public sealed class SnapshotBaseProfileEventArgs : EventArgs
    {
        public SnapshotBaseProfileEventArgs(StructureDefinition profile, StructureDefinition baseProfile) : base()
        {
            Profile = profile;
            BaseProfile = baseProfile;
        }

        /// <summary>Returns a reference to a profile.</summary>
        public StructureDefinition Profile { get; }

        /// <summary>Returns a reference to the associated base profile.</summary>
        public StructureDefinition BaseProfile { get; }

    }

    /// <summary>A delegate type for hooking up <see cref="SnapshotGenerator.PrepareBaseProfile"/> events.</summary>
    public delegate void SnapshotBaseProfileHandler(object sender, SnapshotBaseProfileEventArgs e);


    /// <summary>Event arguments for the <see cref="SnapshotElementHandler"/> event delegate.</summary>
    public sealed class SnapshotElementEventArgs : EventArgs
    {
        public SnapshotElementEventArgs(ElementDefinition element, StructureDefinition baseStructure, ElementDefinition baseElement) : base()
        {
            Element = element;
            BaseElement = baseElement;
            BaseStructure = baseStructure;
        }

        /// <summary>Returns a reference to an element definition.</summary>
        public ElementDefinition Element { get; }

        /// <summary>Returns a reference to the associated base element definition.</summary>
        public ElementDefinition BaseElement { get; }

        /// <summary>Returns a reference to the associated base structure definition. The snapshot component contains the <see cref="BaseElement"/> instance.</summary>
        public StructureDefinition BaseStructure { get; }
    }

    public delegate void SnapshotElementHandler(object sender, SnapshotElementEventArgs e);


    /// <summary>Event arguments for the <see cref="SnapshotExpandElementHandler"/> event delegate.</summary>
    public sealed class SnapshotExpandElementEventArgs : EventArgs
    {
        public SnapshotExpandElementEventArgs(ElementDefinition element, bool hasChildren, bool mustExpand) : base()
        {
            Element = element;
            HasChildren = hasChildren;
            MustExpand = mustExpand;
        }

        /// <summary>Returns a reference to the current element.</summary>
        public ElementDefinition Element { get; }

        /// <summary>Indicates wether the current element has any child elements.</summary>
        public bool HasChildren { get; }

        /// <summary>Gets or sets a boolean value that determines wether the snapshot generator should expand children of the current element.</summary>
        public bool MustExpand { get; set; }
    }

    /// <summary>A delegate type for hooking up <see cref="SnapshotGenerator.BeforeExpandElement"/> events.</summary>
    public delegate void SnapshotExpandElementHandler(object sender, SnapshotExpandElementEventArgs e);


}
