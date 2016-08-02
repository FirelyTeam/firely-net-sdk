/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Support;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Navigation
{
    // [WMR 20160802] NEW
    public static class NamedNavigation
    {
        /// <summary>Determines if an element name matches a choice element name in the base profile.</summary>
        /// <example>Match "value[x]" and "valueCodeableConcept"</example>
        internal static bool IsRenamedChoiceElement(string baseName, string newName)
        {
            return baseName != null
                && newName != null
                && baseName.EndsWith("[x]")
                && String.Compare(baseName, 0, newName, 0, baseName.Length - 3) == 0 && newName.Length > baseName.Length;
        }
    }

    public static class NamedNavigationExtensions
    {
        public static bool MoveToChild(this BaseElementNavigator nav, string name)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.MoveToFirstChild())
            {
                do
                {
                    if(nav.PathName == name) return true;
                }
                while (nav.MoveToNext());
                nav.MoveToParent();
            }

            return false;
        }
   
        public static bool MoveToNext(this BaseElementNavigator nav, string name)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            var bm = nav.Bookmark();

            while (nav.MoveToNext())
            {
                if (nav.PathName == name) return true;
            }

            nav.ReturnToBookmark(bm);
            return false;           
        }

        // [WMR 20160802] NEW
        public static bool MoveToNextTypeSlice(this BaseElementNavigator nav, string name)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            var bm = nav.Bookmark();

            while (nav.MoveToNext())
            {
                if (NamedNavigation.IsRenamedChoiceElement(name, nav.PathName)) return true;
            }

            nav.ReturnToBookmark(bm);
            return false;
        }

        /// <summary>Determines if the specified element name is renamed from the current (choice) element.</summary>
        public static bool IsCandidateTypeSlice(this BaseElementNavigator nav, string diffName)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            return NamedNavigation.IsRenamedChoiceElement(nav.PathName, diffName);
        }

        /// <summary>
        /// Move to last direct child element with same path as current element.
        /// Returns true if the cursor has moved at least a single element, false otherwise
        /// </summary>
        public static bool MoveToLastSlice(this BaseElementNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.Current == null) { throw Error.Argument("nav", "Cannot move to last slice. Current node is not set."); }
            if (nav.Current.Base == null) { throw Error.Argument("nav", "Cannot move to last slice. Current node has no Base.path component (path '{0}').".FormatWith(nav.Path)); }

            var bm = nav.Bookmark();
            var basePath = nav.Current.Base.Path;
            if (string.IsNullOrEmpty(basePath)) { throw Error.Argument("nav", "Cannot move to last slice. Current node has no Base.path component (path '{0}').".FormatWith(nav.Path)); }

            var result = false;
            while (nav.MoveToNext())
            {
                var baseComp = nav.Current.Base;
                if (baseComp != null && baseComp.Path == basePath)
                {
                    // Match, advance cursor
                    bm = nav.Bookmark();
                    result = true;
                }
                else
                {
                    // Mismatch, back up to previous element and exit
                    nav.ReturnToBookmark(bm);
                    break;
                }
            }
            return result;
        }

        public static bool MoveToPrevious(this BaseElementNavigator nav, string name)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            var bm = nav.Bookmark();

            while (nav.MoveToPrevious())
            {
                if (nav.PathName == name) return true;
            }

            nav.ReturnToBookmark(bm);
            return false;
        }


        public static bool MoveTo(this BaseElementNavigator nav, string name)
        {
            // MoveNext method performs parameter validation
            return MoveToNext(nav, name) || MoveToPrevious(nav,name);
        }

        public static bool JumpToFirst(this BaseElementNavigator nav, string path)
        {
            // Find method performs parameter validation
            // if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            // if (path == null) { throw Error.ArgumentNull(nameof(path)); }

            var matches = Find(nav, path);

            if (matches.Any())
            {
                nav.ReturnToBookmark(matches.First());
                return true;
            }

            return false;
        }


        public static IEnumerable<Bookmark> Find(this BaseElementNavigator nav, string path)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (path == null) { throw Error.ArgumentNull(nameof(path)); }

            var parts = path.Split('.');

            var bm = nav.Bookmark();
            nav.Reset();
            var result = locateChildren(nav, parts, partial: false);
            nav.ReturnToBookmark(bm);

            return result;
        }


        public static IEnumerable<Bookmark> Approach(this BaseElementNavigator nav, string path)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (path == null) { throw Error.ArgumentNull(nameof(path)); }

            var parts = path.Split('.');

            var bm = nav.Bookmark();
            nav.Reset();
            var result = locateChildren(nav, parts, partial: true);
            nav.ReturnToBookmark(bm);

            return result;
        }

        private static IEnumerable<Bookmark> locateChildren(BaseElementNavigator nav, IEnumerable<string> path, bool partial)
        {
            Debug.Assert(nav != null); // Caller should validate

            var child = path.First();
            var rest = path.Skip(1);

            var bm = nav.Bookmark();

            if (nav.MoveToChild(child))
            {
                var result = new List<Bookmark>();

                do
                {
                    if (!rest.Any())
                    {
                        // Exact match!
                        result.Add(nav.Bookmark());
                    }
                    else if (!nav.HasChildren && partial)
                    {
                        // This is as far as we can get in this structure,
                        // so this is a hit too if partial hits are OK
                        result.Add(nav.Bookmark());
                    }
                    else
                    {
                        // So, no hit, but we have children that might fit the bill.
                        result.AddRange(locateChildren(nav, rest, partial));
                    }

                    // Try this for the other matching siblings too...
                }
                while (nav.MoveToNext(child));

                // We've scanned all my children and collected the results,
                // move the navigator back to where we were before
                nav.ReturnToBookmark(bm);
                return result;
            }
            else
                return Enumerable.Empty<Bookmark>();
        }
    }
}
