/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/spark/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) using a list of other IArtifactSources
    /// </summary>
    public class CachedArtifactSource : IArtifactSource
    {
        public const int DEFAULT_CACHE_DURATION = 4 * 3600;     // 4 hours

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheDuration">Duration before trying to refresh the cache, in seconds</param>
        public CachedArtifactSource(IArtifactSource source, int cacheDuration)
        {
            Source = source;
            CacheDuration = cacheDuration;
        }

        public CachedArtifactSource(IArtifactSource source) : this(source,DEFAULT_CACHE_DURATION)
        {
        }


        public IArtifactSource Source { get; private set; }

        public int CacheDuration { get; set; }

        private bool _prepared = false;

        public void Prepare()
        {
            if (!_prepared)
            {
                Source.Prepare();
                _prepared = true;
            }
        }

        public Stream ReadContentArtifact(string name)
        {                    
            if (!_prepared) Prepare();

            // We don't cache a stream (yet?)
            return Source.ReadContentArtifact(name);
        }


        private class CacheEntry
        {
            public Resource Data;
            public DateTime Last;
            public Uri ArtifactId;
            public Exception LastError;
        }

        private List<CacheEntry> _cache = new List<CacheEntry>();

        public Resource ReadResourceArtifact(Uri artifactId)
        {
            if (!_prepared) Prepare();

            // Check the cache
            var entry = _cache.Where(ce => ce.ArtifactId == artifactId).SingleOrDefault();

            // Remove entry if it's too old
            if(entry != null && entry.Last < DateTime.Now.AddSeconds(-1 * CacheDuration))
            {
                _cache.Remove(entry);
                entry = null;
            }

            // If we still have a fresh entry, return it
            if(entry != null)
                return entry.Data;
            else
            {
                // Otherwise, fetch it and cache it.
                Resource newData = null;
                Exception lastError = null;

                try
                {
                    newData = Source.ReadResourceArtifact(artifactId);
                }
                catch(Exception e)
                {
                    lastError = e;
                }
                
                _cache.Add(new CacheEntry { Data = newData, ArtifactId = artifactId, Last = DateTime.Now, LastError = lastError });

                return newData;
            }
        }
    }
}
