/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Introspection
{
    public static class ProfileNavigationExtensions
    {
        public const string STRUCTURE_PROFILE_URI = "http://fhir.furore.com/Profiles/navigation-extensions#profile-location";

        public static string GetProfileLocation(this Element structure)
        {
            var val = structure.GetExtensionValue(STRUCTURE_PROFILE_URI) as FhirUri;
            return val != null ? val.Value : null;
        }

        public static string GetProfileLocation(this Profile profile)
        {
            var val = profile.GetExtensionValue(STRUCTURE_PROFILE_URI) as FhirUri;
            return val != null ? val.Value : null;
        }


        public static void SetProfileLocation(this Element structure, string profileAddress)
        {
            structure.SetExtension(STRUCTURE_PROFILE_URI, new FhirUri(profileAddress));
        }

        public static void RemoveProfileLocation(this Element structure)
        {
            structure.RemoveExtension(STRUCTURE_PROFILE_URI);
        }

        public static void SetProfileLocation(this Profile profile, Uri profileUri)
        {
            var uri = profileUri.ToString();

            profile.SetExtension(STRUCTURE_PROFILE_URI, new FhirUri(profileUri));

            if(profile.Structure != null) 
                foreach (var structure in profile.Structure)
                    structure.SetProfileLocation(uri);

            if (profile.Extension != null)
                foreach (var extension in profile.Extension)
                    extension.SetProfileLocation(uri);
        }

        public static void RemoveProfileLocation(this Profile profile)
        {
            profile.RemoveExtension(STRUCTURE_PROFILE_URI);

            if (profile.Structure != null)
                foreach (var structure in profile.Structure)
                    structure.RemoveProfileLocation();

            if (profile.Extension != null)
                foreach (var extension in profile.Extension)
                    extension.RemoveProfileLocation();
        }

        /// <summary>
        /// Rewrites the Path's of the elements in a structure so they are based on the given path: the root
        /// of the given structure will become the given path, it's children will be relocated below that path
        /// </summary>
        /// <param name="root">The structure that will be rebased on the path</param>
        /// <param name="path">The path to rebase the structure on</param>
        public static void Rebase(this Profile.ProfileStructureComponent root, string path)
        {
            var nav = new ElementNavigator(root);

            if (nav.MoveToFirstChild())
            {
                var newPaths = new List<string>();
                newPaths.Add(path);

                rebaseChildren(nav, path, newPaths);

                // Can only change the paths after navigating the tree, otherwise the
                // navigation functions (which are based on the paths) won't function correctly
                for (var i = 0; i < root.Element.Count; i++)
                    root.Element[i].Path = newPaths[i];
            }
        }


        private static void rebaseChildren(BaseElementNavigator nav, string path, List<string> newPaths)
        {
            var bm = nav.Bookmark();

            if (nav.MoveToFirstChild())
            {
                do
                {
                    var newPath = path + "." + nav.Current.GetNameFromPath();

                    newPaths.Add(newPath);

                    if(nav.HasChildren) 
                        rebaseChildren(nav, newPath, newPaths);
                }
                while (nav.MoveToNext());

                nav.ReturnToBookmark(bm);
            }
        }

        public static bool InRange(this Profile.ElementDefinitionComponent defn, int count)
        {
            int min = Convert.ToInt32(defn.Min);
            if (count < min)
                return false;

            if (defn.Max == "*")
                return true;

            int max = Convert.ToInt32(defn.Max);
            if (count > max)
                return false;

            return true;
        }

        public static bool IsRepeating(this Profile.ElementDefinitionComponent defn)
        {
            return defn.Max != "1" && defn.Max != "0";
        }

        public static bool IsExtension(this Profile.ElementComponent elem)
        {
            return elem.Path.EndsWith(".extension") || elem.Path.EndsWith(".modifierExtension");
        }

        public static string CardinalityAsString(this Profile.ElementDefinitionComponent defn)
        {
            return defn.Min + ".." + defn.Max;
        }

        public static string GetNameFromPath(this Profile.ElementComponent element)
        {
 	        var pos = element.Path.LastIndexOf(".");

            return pos != -1 ? element.Path.Substring(pos+1) : element.Path;
        }

        public static string GetParentNameFromPath(this Profile.ElementComponent element)
        {
            var dot = element.Path.LastIndexOf(".");
            return dot != -1 ? element.Path.Substring(0, dot) : String.Empty;
        }      
    }
}
    
    
