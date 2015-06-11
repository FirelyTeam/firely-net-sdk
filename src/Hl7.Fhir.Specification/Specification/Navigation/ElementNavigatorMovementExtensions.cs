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
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Navigation
{
    public static class NamedNavigationExtensions
    {
        public static bool MoveToChild(this BaseElementNavigator nav, string name)
        {
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
            var bm = nav.Bookmark();

            while (nav.MoveToNext())
            {
                if (nav.PathName == name) return true;
            }

            nav.ReturnToBookmark(bm);
            return false;           
        }


        public static bool MoveToPrevious(this BaseElementNavigator nav, string name)
        {
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
            return MoveToNext(nav, name) || MoveToPrevious(nav,name);
        }


        public static bool JumpToFirst(this BaseElementNavigator nav, string path)
        {
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
            var parts = path.Split('.');

            var bm = nav.Bookmark();
            nav.Reset();
            var result = locateChildren(nav, parts, partial: false);
            nav.ReturnToBookmark(bm);

            return result;
        }


        public static IEnumerable<Bookmark> Approach(this BaseElementNavigator nav, string path)
        {
            var parts = path.Split('.');

            var bm = nav.Bookmark();
            nav.Reset();
            var result = locateChildren(nav, parts, partial: true);
            nav.ReturnToBookmark(bm);

            return result;
        }

        private static IEnumerable<Bookmark> locateChildren(BaseElementNavigator nav, IEnumerable<string> path, bool partial)
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
