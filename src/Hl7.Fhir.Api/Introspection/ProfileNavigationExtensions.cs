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

        public static Profile.ElementComponent FindChild(this Profile.ProfileStructureComponent root, string path)
        {
            var nav = new ElementNavigator(root);

            return nav.JumpToFirst(path) ? nav.Current : null;
        }


        public static Profile.ElementComponent FindChild(this Profile.ProfileStructureComponent root, Profile.ElementComponent element)
        {
            return FindChild(root, element.Path);
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

        public static IEnumerable<Profile.ElementComponent> GetChildren(this Profile.ProfileStructureComponent root, string path, bool includeGrandchildren = false)
        {
            var nav = new ElementNavigator(root);

            if (nav.JumpToFirst(path) && nav.MoveToFirstChild())
            {
                do
                {
                    yield return nav.Current;

                    if (nav.HasChildren() && includeGrandchildren)
                    {
                        foreach (var child in GetChildren(root, nav.CurrentPath(), includeGrandchildren))
                            yield return child;
                    }
                }
                while (nav.MoveToNext());
            }
            else
            {
                yield break;
            }

        }
    }
}
    
    
