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
            var matches = Find(nav, path);

            if (matches.Any())
            {
                nav.ReturnToBookmark(matches.First());
                return true;
            }

            return false;
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

        public static bool AppendChild(this ElementNavigator nav, Profile.ElementComponent child)
        {
            var bm = nav.Bookmark();

            if (nav.MoveToFirstChild())
            {
                while (nav.MoveToNext()) ;
                var result = nav.InsertAfter(child);
                
                if(!result) nav.ReturnToBookmark(bm);
                return result;
            }
            else
            {
                return nav.InsertFirstChild(child);
            }
        }


        public static bool DeleteTree(this ElementNavigator nav)
        {
            var parent = nav.Bookmark();

            if(nav.MoveToFirstChild())
            {
                while(!nav.IsAtBookmark(parent)) nav.DeleteTree();
            }

            return nav.Delete();
        }


        public static IEnumerable<Bookmark> Find(this ElementNavigator nav, string path)
        {
            var parts = path.Split('.');

            var bm = nav.Bookmark();
            nav.Reset();
            var result = locateChildren(nav, parts, partial: false);
            nav.ReturnToBookmark(bm);

            return result;
        }


        public static IEnumerable<Bookmark> Approach(this ElementNavigator nav, string path)
        {
            var parts = path.Split('.');
            
            var bm = nav.Bookmark();
            nav.Reset();
            var result = locateChildren(nav, parts, partial: true);
            nav.ReturnToBookmark(bm);

            return result;
        }

        private static IEnumerable<Bookmark> locateChildren(ElementNavigator nav, IEnumerable<string> path, bool partial)
        {
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
                    else if (!nav.HasChildren() && partial)
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


        /// <summary>
        /// Insert the children of the current source node under the node pointed to by the destination.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool CopyChildren(this ElementNavigator dest, ElementNavigator source)
        {
            if (dest.HasChildren()) return false;   // Protect children from being overwritten
            if (!source.MoveToFirstChild()) return true;    // Nothing to copy, but successful anyway

            bool firstChild = true;

            do
            {
                var copiedChild = (Profile.ElementComponent)source.Current.DeepCopy();

                if (firstChild)
                {
                    // The first time, create a new child in the destination                    
                    dest.InsertFirstChild(copiedChild);
                    firstChild = false;
                }
                else
                    // Then insert other childs after that
                    dest.InsertAfter(copiedChild);
                
                // If there are nested children in the source, insert them under
                // the newly inserted node in the destination
                if (source.HasChildren()) dest.CopyChildren(source);
            }
            while (source.MoveToNext());

            // Bring both source & destination back one step to the original parents
            source.MoveToParent();
            dest.MoveToParent();

            return true;
        }
    }
}
