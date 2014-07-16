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
using System.Text;
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Introspection
{
    public static class NamedNavigationExtensions
    {
        public static string CurrentName(this ElementNavigator nav)
        {
            return nav.Current != null ? nav.Current.GetNameFromPath() : String.Empty;
        }

        public static string CurrentPath(this ElementNavigator nav)
        {
            return nav.Current != null ? nav.Current.Path : String.Empty; 
        }

        public static string CurrentParentPath(this ElementNavigator nav)
        {
            return nav.Current != null ? nav.Current.GetParentNameFromPath() : String.Empty;
        }


        public static bool MoveToChild(this ElementNavigator nav, string name)
        {
            if (nav.MoveToFirstChild())
            {
                do
                {
                    if(nav.CurrentName() == name) return true;
                }
                while (nav.MoveToNext());
                nav.MoveToParent();
            }

            return false;
        }
   
        public static bool MoveToNext(this ElementNavigator nav, string name)
        {
            var bm = nav.Bookmark();

            while (nav.MoveToNext())
            {
                if (nav.CurrentName() == name) return true;
            }

            nav.ReturnToBookmark(bm);
            return false;           
        }


        public static bool MoveToPrevious(this ElementNavigator nav, string name)
        {
            var bm = nav.Bookmark();

            while (nav.MoveToPrevious())
            {
                if (nav.CurrentName() == name) return true;
            }

            nav.ReturnToBookmark(bm);
            return false;
        }


        public static bool JumpToFirst(this ElementNavigator nav, string path)
        {
            throw new NotImplementedException();

            //var bm = nav.Bookmark();

            //if (nav.Approach(path))
            //{
            //    if (nav.Path == path) return true;
            //}

            //nav.ReturnToBookmark(bm);
            //return false;
        }


        public static bool MoveTo(this ElementNavigator nav, ElementNavigator other)
        {
            return nav.ReturnToBookmark(other.Bookmark());
        }


        public static bool HasChildren(this ElementNavigator nav)
        {
            if (nav.MoveToFirstChild())
            {
                nav.MoveToParent();
                return true;
            }
            return false;
        }

        public static void AppendChild(this ElementNavigator nav, Profile.ElementComponent child)
        {
            if (nav.HasChildren())
            {
                var bm = nav.Bookmark();
                
                nav.MoveToFirstChild();
                while (nav.MoveToNext()) ;
                nav.InsertAfter(child);
                
                nav.ReturnToBookmark(bm);
            }
            else
            {
                nav.InsertFirstChild(child);
            }
        }

        public static IEnumerable<object> Find(this ElementNavigator nav, string path)
        {
            var parts = path.Split('.');

            var bm = nav.Bookmark();
            nav.Reset();

            if (path == nav.CurrentPath())
            {
                yield return nav.Bookmark();
            }
            else
            {
                nav.ReturnToBookmark(bm);
                yield break;
            }
        }

        private static bool approachChild(ElementNavigator nav, IEnumerable<string> path)
        {
            var child = path.First();
            var rest = path.Skip(1);

            if (nav.MoveToChild(child))
            {
                var longest = nav.Bookmark();
                var longestMatch = nav.CurrentPath();

                do
                {
                    if(!rest.Any()) return true;      // found an exact match

                    var bm = nav.Bookmark();

                    // See if we can get any closer, by having a matching child under me
                    if (approachChild(nav, rest))
                    {
                        if (nav.CurrentPath().Length > longestMatch.Length)
                        {
                            longestMatch = nav.CurrentPath();
                            longest = nav.Bookmark();
                        }

                        // approachChild() will have moved to the longest match so far, but we
                        // want to go on with my siblings, they may have even better matches,
                        // so move back and go to the next sibling
                        nav.ReturnToBookmark(bm);
                    }
                }
                while (nav.MoveToNext(child));

                // We've scanned all my children with a matching name for the best match,
                // move the navigator to the longest match found in my children
                return nav.ReturnToBookmark(longest);  // should always be true
            }
            else
                return false;
        }
    }
}
