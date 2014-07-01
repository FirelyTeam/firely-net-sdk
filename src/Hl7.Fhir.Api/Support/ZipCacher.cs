/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Support
{
    /// <summary>
    /// Internal class which gives access to files within a zip whilst avoiding unpacking that zip on every access.
    /// The ZipCacher will unpack the zip once and store the contents in a cache directory, serving files from this cache.
    /// When the ZipCacher detects the zip is more recents than its cache, it will update the cache directory automatically.
    /// 
    /// The ZipCacher will either use a different cache for each instance, or -given a persistent "cache key"- reuse the
    /// cache from a previous instantiation of ZipCacher. When using a shared cache over multiple ZipCachers with the/
    /// same cacheKey, keep in mind that it uses the filesystem for its state, and is not thread-safe.
    /// </summary>
    internal class ZipCacher
    {
        private string _cachePath;
        private string _zipPath;

        public ZipCacher(string zipPath, string cacheKey=null)
        {
            if(cacheKey == null) cacheKey = Guid.NewGuid().ToString();
            
            _cachePath = Path.Combine(Path.GetTempPath(), cacheKey);
            _zipPath = zipPath;
        }

        public ZipCacher()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// Returns a list of files present in the zip, returning a full path to the file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetContents()
        {
            if (!IsActual()) Refresh();

            var dir = getCachedZipDirectory();
            return dir.GetFiles().Select(fi => fi.FullName);
        }


        /// <summary>
        /// Returns true if the ZipCacher has an up-to-date cache for the zip file it manages
        /// </summary>
        /// <returns></returns>
        public bool IsActual()
        {
            var dir = getCachedZipDirectory();

            if (!dir.Exists) return false;

            var currentZipFileTime = File.GetLastWriteTimeUtc(_zipPath);

            return dir.CreationTimeUtc >= currentZipFileTime;
        }


        public void Refresh()
        {
            Clear();

            var dir = getCachedZipDirectory();

            dir.Create();

            unzip(_zipPath, dir.FullName);
        }


        public void Clear()
        {
            var dir = getCachedZipDirectory();

            if (dir.Exists) dir.Delete(recursive: true);
        }


        private static void unzip(string zipPath, string outputPath)
        {
            using (var zipfile = ZipFile.Read(zipPath))
            {
                zipfile.ExtractAll(outputPath);
            }
        }


        /// <summary>
        /// Gets the cache directory, but does not create one if it does not yet exist
        /// </summary>
        /// <returns></returns>
        private DirectoryInfo getCachedZipDirectory()
        {
            // First, create the main "cache" directory. This dir can contain the unzipped files for multiple
            // ZipCachers that are using the same cacheKey.
            var cache = new DirectoryInfo(_cachePath);
            if (!cache.Exists) cache.Create();

            // Then, create a new directory *INFO* within the cache named after the zip's filename (without extension)
            // Note: two ZipCachers handling different zips with the same filename, will most certainly interfere
            // with each other.
            var cacheName = Path.GetFileNameWithoutExtension(_zipPath);
            var zipCachePath = Path.Combine(cache.FullName, cacheName);

            var zipCacheDir = new DirectoryInfo(zipCachePath);
                        
            return zipCacheDir;
        }
    }
}
