using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Utility;
using System;
using System.Diagnostics;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    // Design:
    // - SnapshotSource depends on SnapshotGenerator; NOT the other way around (!)
    // - Ownership/composition: SnapshotSource => SnapshotGenerator => InternalSource
    // - SnapshotGenerator rejects SnapshotSource as input argument (throw), to prevent recursion
    // - SnapshotGenerator still supports stand-alone usage (w/o SnapshotSource), i.e. backwards-compatible
    // - SnapshotGenerator class remains responsible for:
    //   - detecting and handling recursion
    //   - caching expanded root elements
    //   - annotating snapshots

    /// <summary>
    /// Resolves resources from an internal <see cref="IResourceResolver"/> instance.
    /// Ensures that resolved <see cref="StructureDefinition"/> resources have a snapshot component (re-generate on demand).
    /// </summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class SnapshotSource : IResourceResolver, IAsyncResourceResolver
    {
        /// <summary>Creates a new instance of the <see cref="SnapshotSource"/> for the specified snapshot generator instance.</summary>
        /// <param name="generator">A <see cref="SnapshotGenerator"/> instance.</param>
        public SnapshotSource(SnapshotGenerator generator)
        {
            Generator = generator ?? throw Error.ArgumentNull(nameof(generator));
        }

        /// <summary>Creates a new instance of the <see cref="SnapshotSource"/> for the specified internal resolver.</summary>
        /// <param name="source">An internal <see cref="IResourceResolver"/> instance. The implementation should be idempotent (i.e. cached), so the generated snapshots are persisted in memory.</param>
        /// <param name="settings">Configuration settings for the snapshot generator.</param>
#pragma warning disable CS0618 // Type or member is obsolete
        public SnapshotSource(ISyncOrAsyncResourceResolver source, SnapshotGeneratorSettings settings)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            // SnapshotGenerator ctor will throw if source or settings are null
            Generator = new SnapshotGenerator(source, settings);
        }

        /// <summary>Creates a new instance of the <see cref="SnapshotSource"/> for the specified internal resolver.</summary>
        /// <param name="source">An internal <see cref="IResourceResolver"/> instance. The implementation should be idempotent (i.e. cached), so the generated snapshots are persisted in memory.</param>
        /// <param name="regenerate">Determines if the source should always discard any existing snapshot components provided by the internal source and force re-generation.</param>
#pragma warning disable CS0618 // Type or member is obsolete
        public SnapshotSource(ISyncOrAsyncResourceResolver source, bool regenerate)
#pragma warning restore CS0618 // Type or member is obsolete
            : this(source, createSettings(regenerate)) { }

        // Create default SnapshotGeneratorSettings, apply the specified regenerate flag
        private static SnapshotGeneratorSettings createSettings(bool regenerate)
        {
            var settings = SnapshotGeneratorSettings.CreateDefault();
#pragma warning disable CS0618 // Type or member is obsolete
            settings.ForceRegenerateSnapshots = regenerate;
#pragma warning restore CS0618 // Type or member is obsolete
            return settings;
        }

        /// <summary>Creates a new instance of the <see cref="SnapshotSource"/> for the specified internal resolver.</summary>
        /// <param name="source">An internal <see cref="IResourceResolver"/> instance. The implementation should be idempotent (i.e. cached), so the generated snapshots are persisted in memory.</param>
#pragma warning disable CS0618 // Type or member is obsolete
        public SnapshotSource(ISyncOrAsyncResourceResolver source)
#pragma warning restore CS0618 // Type or member is obsolete
            : this(source, SnapshotGeneratorSettings.CreateDefault()) { }

        /// <summary>Returns the internal <see cref="SnapshotGenerator"/> instance used by the source.</summary>
        public SnapshotGenerator Generator { get; }

        #region IResourceResolver

        private IAsyncResourceResolver Resolver => Generator.AsyncResolver;

        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <remarks>The source ensures that resolved <see cref="StructureDefinition"/> instances have a snapshot component.</remarks>
        public async Tasks.Task<Resource> ResolveByUriAsync(string uri) => await ensureSnapshot(await Resolver.ResolveByUriAsync(uri).ConfigureAwait(false)).ConfigureAwait(false);

        /// <inheritdoc cref="ResolveByUriAsync(string)"/>
        [Obsolete("SnapshotSource now works best with asynchronous resolvers. Use ResolveByUriAsync() instead.")]
        public Resource ResolveByUri(string uri) => TaskHelper.Await(() => ResolveByUriAsync(uri));

        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <remarks>The source ensures that resolved <see cref="StructureDefinition"/> instances have a snapshot component.</remarks>
        public async Tasks.Task<Resource> ResolveByCanonicalUriAsync(string uri) => await ensureSnapshot(await Resolver.ResolveByCanonicalUriAsync(uri).ConfigureAwait(false)).ConfigureAwait(false);

        /// <inheritdoc cref="ResolveByCanonicalUriAsync(string)"/>
        [Obsolete("SnapshotSource now works best with asynchronous resolvers. Use ResolveByCanonicalUriAsync() instead.")]
        public Resource ResolveByCanonicalUri(string uri) => TaskHelper.Await(() => ResolveByCanonicalUriAsync(uri));

        #endregion

        // If the specified resource is a StructureDefinition,
        // then ensure snapshot component is available, (re-)generate on demand
        private async Tasks.Task<Resource> ensureSnapshot(Resource res)
        {
            if (res is StructureDefinition sd)
            {
                var shouldGenerate = Generator.Settings.RegenerationBehaviour switch
                {
                    RegenerationSettings.TRY_USE_EXISTING => !sd.HasSnapshot,
                    RegenerationSettings.REGENERATE_ONCE => !sd.HasSnapshot || !sd.Snapshot.IsCreatedBySnapshotGenerator(),
#pragma warning disable CS0618 // Type or member is obsolete
                    RegenerationSettings.FORCE_REGENERATE => true,
#pragma warning restore CS0618 // Type or member is obsolete
                    _ => throw Error.NotSupported($"Unknown regeneration behaviour: {Generator.Settings.RegenerationBehaviour}")
                };
                
                if (shouldGenerate)
                {
                    await Generator.UpdateAsync(sd).ConfigureAwait(false);
                }
            }
            return res;
        }

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal protected virtual string DebuggerDisplay => $"{GetType().Name} for {Resolver.DebuggerDisplayString()}";
    }
}