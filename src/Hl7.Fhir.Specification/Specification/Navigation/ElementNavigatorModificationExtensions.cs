/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Navigation
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


        public static bool DeleteChildren(this BaseElementNavigator nav)
        {
            var parent = nav.Bookmark();

            if (nav.MoveToFirstChild())
            {
                while (!nav.IsAtBookmark(parent)) nav.DeleteTree();
            }

            return true;
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


        public static bool ExpandElement(this ElementNavigator nav, StructureLoader source)
        {
            if(source == null) throw Error.ArgumentNull("source");
            if(nav.Current == null) throw Error.ArgumentNull("Navigator is not positioned on an element");

            if (nav.Current.Definition == null) throw Error.Argument("Cannot move down into element {0} since it has no element definition information", nav.Path);

            if(nav.HasChildren) return true;     // already has children, we're not doing anything extra

            if (nav.Current.Definition != null)
            {
                var defn = nav.Current.Definition;
                if (!String.IsNullOrEmpty(defn.NameReference))
                {
                    var sourceNav = resolveNameReference(nav.Structure, defn.NameReference);
                    nav.CopyChildren(sourceNav);
                }
                else if (defn.Type != null && defn.Type.Count > 0)
                {
                    if (defn.Type.Count > 1)
                        throw new NotImplementedException("Don't know how to implement navigation into choice types yet at node " + nav.Path);
                    else
                    {
                        var sourceNav = resolveStructureReference(source, defn.Type[0].CodeElement);

                        if (sourceNav != null)
                        {
                            sourceNav.MoveToFirstChild();
                            nav.CopyChildren(sourceNav);
                        }
                        else
                            throw new FileNotFoundException("Cannot locate base-structure for datatype " + defn.Type[0].Code);
                    }
                }

                return true;
            }

            return false;
        }

        private static ElementNavigator resolveStructureReference(StructureLoader loader, Code code)
        {
            var result = loader.LocateBaseStructure(code);
            return result != null ? new ElementNavigator(result) : null;
        }


        private static ElementNavigator resolveNameReference(Profile.ProfileStructureComponent structure, string nameReference)
        {
            return structure.JumpToNameReference(nameReference);
        }
    }
}
