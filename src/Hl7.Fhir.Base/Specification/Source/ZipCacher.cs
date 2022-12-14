#nullable enable

/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Internal class which gives access to files within a zip whilst avoiding unpacking that zip on every access.
    /// The ZipCacher will unpack the zip once and store the contents in a cache directory, serving files from this cache.
    /// When the ZipCacher detects the zip is more recent than its cache, it will update the cache directory automatically.
    /// 
    /// The ZipCacher will normally reuse the cache from a previous instantiation of ZipCacher. When using a shared cache over
    /// multiple ZipCachers, keep in mind that it uses the filesystem for its state, and is not thread-safe.
    /// </summary>
    internal class ZipCacher
    {
        /// <summary>
        /// The full path to the directory where the files from the zip will be cached.
        /// </summary>
        /// <remarks>Note that actual extraction will take place in a directory with the name of the zip file
        /// to make the same cache path useable for different zip files.</remarks>
        public string CachePath { get; private set; }

        /// <summary>
        /// The full path to the zip with the contents to extract/cache.
        /// </summary>
        public string ZipPath { get; private set; }

        /// <summary>
        /// Creates a ZipCaches that maintains the extracted files of a zip file in the given directory.
        /// </summary>
        public ZipCacher(string zipPath, string cachePath)
        {
            ZipPath = zipPath;
            CachePath = cachePath;
        }

        /// <summary>
        /// Returns a list of files present in the zip, returning a full path to the file
        /// </summary>
        public IEnumerable<string> GetContents()
        {
            if (!IsActual()) Refresh();

            var dir = getCachedZipDirectory();
            return dir.GetFiles().Select(fi => fi.FullName);
        }

        /// <summary>
        /// Returns true if the ZipCacher has an up-to-date cache for the zip file it manages.
        /// </summary>
        public bool IsActual()
        {
            var dir = getCachedZipDirectory();

            if (!dir.Exists) return false;

            // Sometimes unzipping fails after creating the directory, try to fix that by
            // checking if there are any files at all.
            var dirIsEmpty = !dir.EnumerateFileSystemInfos().Any();
            if (dirIsEmpty) return false;

            var currentZipFileTime = File.GetLastWriteTimeUtc(ZipPath);

            return dir.CreationTimeUtc >= currentZipFileTime;
        }

        /// <summary>
        /// Makes sure the cache is extracted and up to date.
        /// </summary>
        public void EnsureActual()
        {
            if (!IsActual()) Refresh();
        }

        /// <summary>
        /// Returns the directory where the contents of the zip are extracted. 
        /// </summary>
        /// <remarks>Note that this function will update the cache before returning the directory name.</remarks>
        public string GetContentDirectory()
        {
            if (!IsActual()) Refresh();

            return getCachedZipDirectory().FullName;
        }

        /// <summary>
        /// Clears the cache and re-extracts the zip into the cache directory.
        /// </summary>
        public void Refresh()
        {
            Clear();

            var dir = getCachedZipDirectory();

            dir.Create();


            ZipFile.ExtractToDirectory(ZipPath, dir.FullName);

            // and also extract the contained zip in there too with all the xsds in there
            if (File.Exists(Path.Combine(dir.FullName, "fhir-all-xsd.zip")))
                ZipFile.ExtractToDirectory(Path.Combine(dir.FullName, "fhir-all-xsd.zip"), dir.FullName);

            // Set the last write time to be equal to the write time of the zip file,
            // this way, we can compare this time to the write times of newer zips and
            // detect we need a refresh
            Directory.SetCreationTimeUtc(dir.FullName, File.GetLastWriteTimeUtc(ZipPath));
        }


        public void Clear()
        {
            var dir = getCachedZipDirectory();

            if (dir.Exists) dir.Delete(recursive: true);
        }


        /// <summary>
        /// Gets the cache directory, but does not create one if it does not yet exist
        /// </summary>
        /// <returns></returns>
        private DirectoryInfo getCachedZipDirectory()
        {
            // First, create the main "cache" directory.
            var cache = new DirectoryInfo(CachePath);
            if (!cache.Exists) cache.Create();

            // Then, create a new directory *INFO* within the cache named after the zip's filename (without extension)
            // Note: two ZipCachers handling different zips with the same filename, will most certainly interfere
            // with each other.
            var cacheName = Path.GetFileNameWithoutExtension(ZipPath);
            var zipCachePath = Path.Combine(cache.FullName, cacheName);

            var zipCacheDir = new DirectoryInfo(zipCachePath);

            return zipCacheDir;
        }

        /// <summary>
        /// Builds a directory name to use as the target of the unzipped data. The name is based
        /// on the product and version information of the assembly given.
        /// </summary>
        /// <param name="satellite">The assembly from which to take product and version information.</param>
        public static string BuildDefaultCacheDirectoryName(Assembly satellite)
        {
            var versionInfo = satellite.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var productInfo = satellite.GetCustomAttribute<AssemblyProductAttribute>();

            //  var cleanedInformationalVersion = new string(versionInfo!.InformationalVersion.TakeWhile(c => c != '+').ToArray());
            var cleanedInformationalVersion = versionInfo!.InformationalVersion;
            return $"FhirArtifactCache-{cleanedInformationalVersion}-{productInfo!.Product}";
        }

    }
}

#nullable restore