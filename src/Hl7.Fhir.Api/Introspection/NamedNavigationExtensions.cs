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
        public static bool MoveToChild(this ElementNavigator nav, string name)
        {
            if (nav.MoveToFirstChild())
            {
                do
                {
                    if(nav.Name == name) return true;
                }
                while (nav.MoveToNext());
                nav.MoveToParent();
            }

            return false;
        }

        public static bool MoveToParent(this ElementNavigator nav)
        {
            if (nav.ParentPath == String.Empty) return false;

            return nav.JumpTo(nav.ParentPath);
        }

        public static bool MoveToNext(this ElementNavigator nav, string name)
        {
            nav.Bookmark();

            while (nav.MoveToNext())
            {
                if (nav.Name == name) return true;
            }

            nav.ReturnToBookmark();
            return false;           
        }


        public static bool MoveToPrevious(this ElementNavigator nav, string name)
        {
            nav.Bookmark();

            while (nav.MoveToPrevious())
            {
                if (nav.Name == name) return true;
            }

            nav.ReturnToBookmark();
            return false;
        }


        public static bool JumpTo(this ElementNavigator nav, string path)
        {
            nav.Bookmark();

            if (nav.Approach(path))
            {
                if (nav.Path == path) return true;
            }

            nav.ReturnToBookmark();
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

        public static void AppendChild(this ElementNavigator nav, Profile.ElementComponent child)
        {
            if (nav.HasChildren())
            {
                nav.Bookmark();
                nav.MoveToFirstChild();
                while (nav.MoveToNext()) ;
                nav.InsertAfter(child);
                nav.ReturnToBookmark();
            }
            else
            {
                var name = nav.Name;
                nav.InsertAfter(child);

                // Correct the name to be a true child, not a sibling -> this actually creates a child
                child.Path = nav.Path + "." + child.GetNameFromPath();
            }
        }
    }
}
