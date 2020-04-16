/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Summary;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Configuration settings for the <see cref="DirectorySource"/> class.</summary>
    public class DirectoryTreeWalker
    {
        public readonly static string[] DefaultMasks = new[] { "*.*" };

        /// <summary>Default constructor. Creates a new <see cref="DirectorySourceSettings"/> instance with default property values.</summary>
        public DirectoryTreeWalker(string[] masks, string[] includes, string[] excludes, bool includeSubdirs)
        {
            Masks = masks;
            Includes = includes;
            Excludes = excludes;
            IncludeSubdirectories = includeSubdirs;
        }

        public IReadOnlyCollection<string> Masks { get; }
        public IReadOnlyCollection<string> Includes { get; }
        public IReadOnlyCollection<string> Excludes { get; }
        public bool IncludeSubdirectories { get; }

        /// <summary>
        /// List all files present in the directory (matching the mask, if given)
        /// </summary>
        public IList<string> ListAll(string directory)
        {
            // Add files present in the content directory
            var filePaths = safeGetFiles(directory, Masks, IncludeSubdirectories);

            if (Includes?.Count > 0)
            {
                var includeFilter = new FilePatternFilter(Includes);
                filePaths = includeFilter.Filter(directory, filePaths);
            }

            if (Excludes?.Count > 0)
            {
                var excludeFilter = new FilePatternFilter(Excludes, negate: true);
                filePaths = excludeFilter.Filter(directory, filePaths);
            }

            return filePaths;
        }

        // [WMR 20170817]
        // Safely enumerate files in specified path and subfolders, recursively
        // Ignore files & folders with Hidden and/or System attributes
        // Ignore subfolders with insufficient access permissions
        // https://stackoverflow.com/a/38959208
        // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-enumerate-directories-and-files

        private static string[] safeGetFiles(string path, IEnumerable<string> masks, bool searchSubfolders)
        {
            if (File.Exists(path))
            {
                return new string[] { path };
            }

            if (!Directory.Exists(path))
            {
                return new string[0];
            }
         
            var folders = new Queue<string>();
            // Use HashSet to remove duplicates; different masks could match same file(s)
            var files = new HashSet<string>();
            folders.Enqueue(path);

            while (folders.Count != 0)
            {
                string currentFolder = folders.Dequeue();
                var currentDirInfo = new DirectoryInfo(currentFolder);

                // local helper function to validate file/folder attributes, exclude system and/or hidden
                bool isValid(FileAttributes attr) => (attr & (FileAttributes.System | FileAttributes.Hidden)) == 0;

                // local helper function to filter executables (*.exe, *.dll)
                bool isExtensionSafe(string extension) => !ExecutableExtensions.Contains(extension, PathComparer);

                foreach (var mask in masks)
                {
                    try
                    {
                        // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-enumerate-directories-and-files
                        // "Although you can immediately enumerate all the files in the subdirectories of a
                        // parent directory by using the AllDirectories search option provided by the SearchOption
                        // enumeration, unauthorized access exceptions (UnauthorizedAccessException) may cause the
                        // enumeration to be incomplete. If these exceptions are possible, you can catch them and
                        // continue by first enumerating directories and then enumerating files."

                        // Explicitly ignore system & hidden files
                        var curFiles = currentDirInfo.EnumerateFiles(mask, SearchOption.TopDirectoryOnly);
                        foreach (var file in curFiles)
                        {
                            // Skip system & hidden files
                            // Exclude executables (*.exe, *.dll)
                            if (isValid(file.Attributes) && isExtensionSafe(file.Extension))
                            {
                                files.Add(file.FullName);
                            }
                        }
                    }
#if DEBUG
                    catch (Exception ex)
                    {
                        // Do Nothing
                        Debug.WriteLine($"[{nameof(DirectorySource)}.{nameof(harvestSummaries)}] {ex.GetType().Name} while enumerating files in '{currentFolder}':\r\n{ex.Message}");
                    }
#else
                    catch { }
#endif
                }

                if (searchSubfolders)
                {
                    try
                    {
                        var subFolders = currentDirInfo.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
                        foreach (var subFolder in subFolders)
                        {
                            // Skip system & hidden folders
                            if (isValid(subFolder.Attributes))
                            {
                                folders.Enqueue(subFolder.FullName);
                            }
                        }
                    }
#if DEBUG
                    catch (Exception ex)
                    {
                        // Do Nothing
                        Debug.WriteLine($"Error enumerating subfolders of '{currentFolder}': {ex.Message}");
                    }
#else
                    catch { }
#endif

                }
            }

            return files.ToArray();
        }
    }
}