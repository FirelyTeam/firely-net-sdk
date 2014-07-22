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
    public static class NavModificationExtensions
    {
        public static bool AppendChild(this BaseElementNavigator nav, Profile.ElementComponent child)
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


        public static bool DeleteTree(this BaseElementNavigator nav)
        {
            var parent = nav.Bookmark();

            if(nav.MoveToFirstChild())
            {
                while(!nav.IsAtBookmark(parent)) nav.DeleteTree();
            }

            return nav.Delete();
        }




        /// <summary>
        /// Insert the children of the current source node under the node pointed to by the destination.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool CopyChildren(this BaseElementNavigator dest, ElementNavigator source)
        {
            if (dest.HasChildren) return false;   // Protect children from being overwritten
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
                if (source.HasChildren) dest.CopyChildren(source);
            }
            while (source.MoveToNext());

            // Bring both source & destination back one step to the original parents
            source.MoveToParent();
            dest.MoveToParent();

            return true;
        }
    }
}
