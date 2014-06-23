/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/spark/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Api.Introspection
{
    public class ModelSurface
    {
        private Dictionary<string, Profile> _profiles = new Dictionary<string, Profile>();
        private Dictionary<string, ValueSet> _valuesets = new Dictionary<string, ValueSet>();

        public IArtifactSource Resolver { get; set; }

        public ModelSurface()
        {
            // Initialize with default resolver
            Resolver = new MultiArtifactSource(new FileArtifactSource(), new WebArtifactSource());
        }

        public ModelSurface(IArtifactSource resolver)
        {
            Resolver = resolver;
        }

        public void Load(Bundle profiles)
        {
            if (profiles.Entries != null)
            {
                foreach (var entry in profiles.Entries.FilterResourceEntries())
                {
                    loadResourceArtifact(entry.Resource, entry.Id);
                }
            }
        }

        private void loadResourceArtifact(Resource resource, Uri location)
        {
            if (resource is Profile)
                Load((Profile)resource, location);
            else if (resource is ValueSet)
                Load((ValueSet) resource, location);
            else
                throw Error.NotSupported("Don't know how to load artifact {0} of type {1}", location,
                    resource.GetType().Name);
        }

        public void Load(Profile profile, Uri location)
        {
            // Index queries, mappings, extensions for quick lookup?
            _profiles.Add(location.ToString().ToLower(), profile);

            // make relative references absolute?
        }

        public void Load(ValueSet valueset, Uri location)
        {
            _valuesets.Add(location.ToString().ToLower(), valueset);
        }

        public void Load(Uri location)
        {
            var artifact = Resolver.ReadResourceArtifact(location);

            if (artifact != null)
            {
                loadResourceArtifact(artifact, location);
            }
            else
                throw Error.Argument("location", "Cannot load artifact at uri {0}: not found", location);
        }
    }



    public static class StructureExtensions
    {
        /// <summary>
        /// Given a Structure with elements, find all elements that have a path that are one step below the given path.
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        internal static IEnumerable<Profile.ElementComponent> CollectDirectChildren(this Profile.ProfileStructureComponent structure, string parentPath)
        {
            if (structure.Element == null) return new List<Profile.ElementComponent>();
            return structure.Element.Where(comp => comp.IsDirectChild(parentPath) );
        }


        internal static bool IsDirectChild(this Profile.ElementComponent element, string parentPath)
        {
            var prefix = parentPath == null ? "" : parentPath + ".";
            var prefixLength = prefix.Length;

            return element.Path.StartsWith(prefix) && !element.Path.Substring(prefixLength).Contains(".");
        }

        internal static bool IsChild(this Profile.ElementComponent element, string parentPath)
        {
            var prefix = parentPath == null ? "" : parentPath + ".";
            var prefixLength = prefix.Length;

            return element.Path.StartsWith(prefix);
        }

    }
}
