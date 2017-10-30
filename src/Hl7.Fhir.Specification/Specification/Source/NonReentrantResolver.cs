/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using Hl7.Fhir.Specification.Snapshot;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Hl7.Fhir.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Source
{
    // [WMR 20171028] NEW
    // TODO: Synchronize resource updates, e.g. assigning a new snapshot component
    // Implement IUpdatingResourceResolver
    // Use reader/writer lock? Or ConcurrentQueue
    // Note: PoCos are not protected themselves; clients must strictly conform to locking...
    // i.e. always call resolver to request updated snapshot
    //
    

    /// <summary>
    /// <see cref="IResourceResolver"/> wrapper to detect and prevent reentrant operations.
    /// </summary>
    /// <remarks>
    /// This decorator class is designed to detect and prevent recursive attempts to resolve a specific resource.
    /// This is useful to safely perform recursive operations on resources, such as
    /// the <see cref="SnapshotGenerator"/> class.
    /// <para>
    /// This class is designed to guard against reentrancy from a single consumer.
    /// If multiple consumers require access to a shared source, then create a separate
    /// <see cref="NonReentrantResolver"/> wrapper instance for each consumer.
    /// This prevents reentrancy by a single consumer, while still allowing non-reentrant
    /// multi-threaded access to a common resource.
    /// </para>
    /// </remarks>
    public class NonReentrantResolver : IResourceResolver
    {
        readonly IResourceResolver _source;
        readonly KeyedReentrancyGuard<string, Resource> _resolveByUri;
        readonly KeyedReentrancyGuard<string, Resource> _resolveByCanonicalUri;

        /// <summary>
        /// Creates a new instance of the <see cref="NonReentrantResolver"/>
        /// for the specified internal <see cref="IResourceResolver"/> instance.
        /// </summary>
        /// <param name="source">A <see cref="IResourceResolver"/> instance.</param>
        public NonReentrantResolver(IResourceResolver source)
        {
            _source = source ?? throw Error.ArgumentNull(nameof(source));
            _resolveByUri = new KeyedReentrancyGuard<string, Resource>(_source.ResolveByUri);
            _resolveByCanonicalUri = new KeyedReentrancyGuard<string, Resource>(_source.ResolveByCanonicalUri);
        }

        public static async Task<Resource> TestAsync(string path)
        // public static Resource Load(string path)
        {
            // TODO: Async load & process large file (bundle) in chunks

            int bufSize = (int)new FileInfo(path).Length;

            using (var ms = new MemoryStream(bufSize))
            {
#if DOTNETFW
                var sw = new Stopwatch();
                sw.Start();
                Debug.WriteLine($"[{nameof(NonReentrantResolver)}.{nameof(TestAsync)}] Thread {Thread.CurrentThread.ManagedThreadId} | {sw.ElapsedMilliseconds} ms | Before load...");
#endif

                await loadAsync(ms, path).ConfigureAwait(false);
                //load(ms, path);

#if DOTNETFW
                Debug.WriteLine($"[{nameof(NonReentrantResolver)}.{nameof(TestAsync)}] Thread {Thread.CurrentThread.ManagedThreadId} | {sw.ElapsedMilliseconds} ms | After load...");
#endif

                ms.Seek(0, SeekOrigin.Begin);

#if DOTNETFW
                Debug.WriteLine($"[{nameof(NonReentrantResolver)}.{nameof(TestAsync)}] Thread {Thread.CurrentThread.ManagedThreadId} | {sw.ElapsedMilliseconds} ms | Before read...");
#endif
                var result = readFromStream(ms);
#if DOTNETFW
                Debug.WriteLine($"[{nameof(NonReentrantResolver)}.{nameof(TestAsync)}] Thread {Thread.CurrentThread.ManagedThreadId} | {sw.ElapsedMilliseconds} ms | After read...");
                sw.Stop();
#endif
                return result;
            }

            void load(MemoryStream ms, string p)
            {
                using (FileStream fs = File.OpenRead(p)) { fs.CopyTo(ms); }
            }

            async Task loadAsync(MemoryStream ms, string p)
            {
                using (FileStream fs = File.OpenRead(p)) { await fs.CopyToAsync(ms); }
            }

            Resource readFromStream(MemoryStream ms)
            {
                using (var reader = new StreamReader(ms))
                using (var xmlReader = SerializationUtil.XmlReaderFromStream(ms))
                {
                    return read(xmlReader);
                }
            }

            Resource read(XmlReader reader)
            {
                if (reader.EOF) return null;

                // Root elem
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == XmlNs.FHIR)
                    {
                        var resType = reader.LocalName;
                        break;
                    }
                }

                // Move to first real content node
                if (reader.NodeType != XmlNodeType.Element)
                {
                    while (reader.Read() && reader.NodeType != XmlNodeType.Element) ;
                    if (reader.NodeType != XmlNodeType.Element) return null;
                }
                var resourceNode = (XElement)XElement.ReadFrom(reader);
                var nav = XmlDomFhirNavigator.Create(resourceNode);
                var parser = new BaseFhirParser();
                var result = parser.Parse<Resource>(nav);
                return result;
            }
        }

        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <exception cref="InvalidReentrantOperationException{T}">
        /// Attempt to make a reentrant call to <see cref="ResolveByUri(string)"/> for the specified argument
        /// </exception>
        public Resource ResolveByUri(string uri)
        {
            return _resolveByUri.Invoke(uri);
        }

        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <exception cref="InvalidReentrantOperationException{T}">
        /// Attempt to make a reentrant call to <see cref="ResolveByCanonicalUri(string)"/> for the specified argument
        /// </exception>
        public Resource ResolveByCanonicalUri(string uri)
        {
            return _resolveByCanonicalUri.Invoke(uri);
        }

        /// <summary>Try to find a resource based on its relative or absolute uri.</summary>
        /// <returns><c>true</c> if succesful, or <c>false</c> if the call is reentrant.</returns>
        public bool TryResolveByUri(string uri, out Resource resource)
        {
            return _resolveByUri.TryInvoke(uri, out resource);
        }

        /// <summary>Try to find a (conformance) resource based on its canonical uri.</summary>
        /// <returns><c>true</c> if succesful, or <c>false</c> if the call is reentrant.</returns>
        public bool TryResolveByCanonicalUri(string uri, out Resource resource)
        {
            return _resolveByCanonicalUri.TryInvoke(uri, out resource);
        }

        /// <summary>Exception that is thrown for a reentrant call attempt.</summary>
        /// <typeparam name="T"></typeparam>
        public class InvalidReentrantOperationException<T> : InvalidOperationException
        {
            public InvalidReentrantOperationException(string message, T key) : base(message) { Key = key; }
            public T Key { get; }
        }

        // Helper class to detect reentrant calls for specific arguments

        class KeyedReentrancyGuard<T, TResult>
        {
            readonly object _syncLock = new object();
            readonly HashSet<T> _keys = new HashSet<T>();
            readonly Func<T, TResult> _function;

            public KeyedReentrancyGuard(Func<T, TResult> function)
            {
                _function = function ?? throw new ArgumentNullException(nameof(function));
            }

            public TResult Invoke(T key)
            {
                bool success = false;
                lock (_syncLock)
                {
                    success = _keys.Add(key);
                }
                if (!success)
                {
                    throw new InvalidReentrantOperationException<T>($"Invalid operation. Reentrant call attempt for key '{key}'.", key);
                }
                try
                {
                    return _function(key);
                }
                finally
                {
                    lock (_syncLock)
                    {
                        _keys.Remove(key);
                    }
                }
            }

            public bool TryInvoke(T key, out TResult result)
            {
                bool success;
                result = default(TResult);
                lock (_syncLock)
                {
                    success = _keys.Add(key);
                }
                if (success)
                {
                    try
                    {
                        result = _function(key);
                    }
                    finally
                    {
                        lock (_syncLock)
                        {
                            _keys.Remove(key);
                        }
                    }
                }
                return success;
            }

        }
    }
}
