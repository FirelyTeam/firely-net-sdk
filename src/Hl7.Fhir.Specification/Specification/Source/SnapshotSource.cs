using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Utility;
using System;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Source
{
    // Requirements:
    // - Use of SnapshotSource should be transparent
    // - SnapshotGenerator should be useable *without* SnapshotSource

    // SnapshotGenerator remains responsible for:
    // - detecting and handling recursion
    // - caching expanded root elements
    // - annotating snapshots

    /// <summary>
    /// Resolves resources from an internal <see cref="IResourceResolver"/> instance.
    /// Ensures that resolved <see cref="StructureDefinition"/> resources have a snapshot component (re-generate on demand).
    /// </summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class SnapshotSource : IResourceResolver
    {
        /// <summary>Creates a new instance of the <see cref="SnapshotSource"/> for the specified internal resolver.</summary>
        /// <param name="source">An internal <see cref="IResourceResolver"/> instance. The implementation should be idempotent (i.e. cached), so the generated snapshots are persisted in memory.</param>
        /// <param name="regenerate">Determines if the source should always re-generate the snapshot component (<c>true</c>), or return the existing snapshot if available (<c>false</c>).</param>
        /// <param name="settings">Optional settings for the snapshot generator (or <c>null</c> to use the default settings).</param>
        public SnapshotSource(IResourceResolver source, bool regenerate, SnapshotGeneratorSettings settings)
        {
            // TODO: Specified source should be cacheable...
            // Maybe add some interface property to detect behavior?
            // i.e. IsIdemPotent (repeated calls return same instance)
            // Note: cannot add new property to IResourceResolver, breaking change...
            // Alternatively, add new secondary interface IResolverProperties

            Source = source ?? throw Error.ArgumentNull(nameof(source));
            Regenerate = regenerate;
            Generator = settings != null 
                ? new SnapshotGenerator(this, settings)
                : new SnapshotGenerator(this);
        }

        /// <summary>Creates a new instance of the <see cref="SnapshotSource"/> for the specified internal resolver.</summary>
        /// <param name="source">An internal <see cref="IResourceResolver"/> instance. The implementation should be idempotent (i.e. cached), so the generated snapshots are persisted in memory.</param>
        /// <param name="regenerate">Determines if the source should always re-generate the snapshot component (<c>true</c>), or return the existing snapshot if available (<c>false</c>).</param>
        public SnapshotSource(IResourceResolver source, bool regenerate) : this(source, regenerate, null) { }

        /// <summary>Creates a new instance of the <see cref="SnapshotSource"/> for the specified internal resolver.</summary>
        /// <param name="source">An internal <see cref="IResourceResolver"/> instance.</param>
        public SnapshotSource(IResourceResolver source) : this(source, true) { }

        /// <summary>Returns a reference to the internal artifact source.</summary>
        public IResourceResolver Source { get; }

        /// <summary>Determines if the source should always re-generate the snapshot component (<c>true</c>), or return the existing snapshot if available (<c>false</c>).</summary>
        public bool Regenerate { get; } = true;

        /// <summary>Returns the internal <see cref="SnapshotGenerator"/> instance used by the source.</summary>
        public SnapshotGenerator Generator { get; }

        #region IResourceResolver

        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <remarks>The source ensures that resolved <see cref="StructureDefinition"/> instances have a snapshot component.</remarks>
        public Resource ResolveByUri(string uri) => ensureSnapshot(Source.ResolveByUri(uri));

        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <remarks>The source ensures that resolved <see cref="StructureDefinition"/> instances have a snapshot component.</remarks>
        public Resource ResolveByCanonicalUri(string uri) => ensureSnapshot(Source.ResolveByCanonicalUri(uri));

        #endregion

        // If the specified resource is a StructureDefinition,
        // then ensure snapshot component is available, (re-)generate on demand
        Resource ensureSnapshot(Resource res)
        {
            if (res is StructureDefinition sd)
            {
                // [WMR 20171219] 
                // - Re -use annotation defined by SnapshotGenerator
                // - OR: define custom annotation for SnapshotSource
                if (Regenerate || !sd.HasSnapshot || !sd.Snapshot.IsCreatedBySnapshotGenerator())
                {
                    Generator.Update(sd);
                }
            }
            return res;
        }

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal protected virtual string DebuggerDisplay => $"{GetType().Name} for {Source.DebuggerDisplayString()} | Regenerate = {Regenerate}";
    }
}
