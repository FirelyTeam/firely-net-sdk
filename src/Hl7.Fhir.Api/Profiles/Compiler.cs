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

namespace Hl7.Fhir.Api.Profiles
{
    /// <summary>
    /// Takes a base profile containing definitions for a resource, a possibly "differential" profile containing additional
    /// constraints on that profile, and combines them into a "flattened" profile.
    /// </summary>
    public class Compiler
    {
        private Dictionary<string, Profile.ProfileStructureComponent> _structures = new Dictionary<string,Profile.ProfileStructureComponent>();

        public void LoadDefinitions(Bundle profiles)
        {
            if (profiles.Entries != null)
            {
                foreach (var profile in profiles.Entries.OfType<Profile>())
                    LoadDefinitions(profile);
            }
        }

        public void LoadDefinitions(Profile profile)
        {
            if (profile.Structure != null)
                foreach (var structure in profile.Structure) LoadStructure(structure);
        }

        internal void LoadStructure(Profile.ProfileStructureComponent structure)
        {
            if (_structures.ContainsKey(structure.Type))
                throw Error.Argument("structure", "A structure of type '{0}' was already added to the compiler's base definitions", structure.Type);

            _structures.Add(structure.Type, structure);
        }

        public void Fillout(Profile profile)
        {
            if (profile.Structure != null)
                foreach (var structure in profile.Structure) Fillout(structure);
        }

        internal void Fillout(Profile.ProfileStructureComponent structure)
        {
            if (!_structures.ContainsKey(structure.Type))
                throw Error.Argument("structure", "Don't know how to fillout structure '{0}', since it is based on a base type '{1}' that has not been loaded",
                                        structure.Name ?? "(unknown)", structure.Type);

            var baseStruct = _structures[structure.Type];

            if(baseStruct.Element != null)
            {
                // Special case: this profiled structure has no overriding elements defined, in that case
                // just take *all* of the base struct's elements.
                if(structure.Element == null || structure.Element.Count == 0)
                {
                    structure.Element = baseStruct.Element;
                    return;
                }

                Fillout(baseStruct, null, structure.Element, 0);
            }
        }


        internal static int Fillout(Profile.ProfileStructureComponent source, string parentPath, 
                                        IList<Profile.ElementComponent> dest, int destIndex)
        {
            var children = source.CollectDirectChildren(parentPath);

            // If the base does not have children, but the profiled structure has,
            // do nothing and skip over them in the destination list
            if (children.Count() == 0)
            {
                while (dest[destIndex].IsChild(parentPath)) destIndex++;
            }


            foreach(var element in children)
            {
                if (element.Path != dest[destIndex].Path)
                {
                    dest.Insert(destIndex, element);
                }

                do
                {
                    destIndex++;
                    destIndex = Fillout(source, element.Path, dest, destIndex);
                }
                while (element.Path == dest[destIndex].Path);
            }

            return destIndex;
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
