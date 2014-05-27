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
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Api.Profiles
{
    public class ModelSurface
    {
        private Dictionary<string, Profile> _profiles = new Dictionary<string, Profile>();
        private Dictionary<string, ValueSet> _valuesets = new Dictionary<string, ValueSet>();


        public ModelSurface()
        {
            // Initialize with default resolver
            Resolver = new DefaultArtifactResolver();
        }

        public ModelSurface(IArtifactResolver resolver)
        {
            Resolver = resolver;
        }

        public void Load(Bundle profiles)
        {
            if (profiles.Entries != null)
            {
                foreach (var entry in profiles.Entries.FilterResourceEntries())
                {
                    if (entry.Resource is Profile)
                        Load(entry.Id, (Profile)entry.Resource);
                    else if (entry.Resource is ValueSet)
                        Load(entry.Id, (ValueSet)entry.Resource);
                }
            }
        }

        public void Load(Uri location, Profile profile)
        {
            // Index queries, mappings, extensions for quick lookup?
            _profiles.Add(location.ToString().ToLower(), profile);

            // make relative references absolute?
        }

        public void Load(Uri location, ValueSet valueset)
        {
            _valuesets.Add(location.ToString().ToLower(), valueset);
        }

        // Later: 
        // public void Load(Uri location, Assembly modelAssembly ) {}    - loads profiles/valuesets using reflection

        public IArtifactResolver Resolver { get; set; }
    }


    public interface IArtifactResolver
    {
        Profile ResolveProfile(Uri location);
        ValueSet ResolveValueSet(Uri location);
    }


    public class DefaultArtifactResolver : IArtifactResolver
    {
        

        // Todo: could do fallback scenario's to a list of known profile servers if the artifact was not found at its indicated location
        public Profile ResolveProfile(Uri location)
        {
            throw new NotImplementedException();

            //// This is what the entry.id looks like in the profiles-resources, <id>http://hl7.org/fhir/profile/adversereaction</id> (a resource)
            //// or <id>http://hl7.org/fhir/profile/period</id>

            //Profile result = null;

            //// If this profile could be a base profile for a datatype or resource, first try to resolve it in the
            //// profiles-types and profiles-resources xml files
            //if (location.ToString().StartsWith(BASEPROFILE_URI_PREFIX))
            //{
            //    result = ResolveFromSpecFiles(location);

            //    if (result != null) return result;
            //}

            //// Next, try to find a file on the local filesystem with the same name as the logical id.
            //var id = new ResourceIdentity(location);
            //var logicalId = id.Id;

            //if (logicalId != null)
            //{
            //    result = ResolveFromFilesystem(logicalId + ".xml");
            //    if (result != null) return result;
            //}

            //// Next, try to fetch it from the addresss given
            //FhirClient client = new FhirClient(id.Endpoint);

            //try
            //{
            //    result = client.Read<Profile>(location).Resource;
            //}
            //catch
            //{
            //    result = null;  // just to be sure
            //}

            //return result;
        }


        public static Profile ResolveFromSpecFiles(Uri location)
        {
            throw new NotImplementedException();
        }


        public static Profile ResolveFromFileSystem(string filename)
        {
            throw new NotImplementedException();
        }

        public ValueSet ResolveValueSet(Uri location)
        {
            throw new NotImplementedException();
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
