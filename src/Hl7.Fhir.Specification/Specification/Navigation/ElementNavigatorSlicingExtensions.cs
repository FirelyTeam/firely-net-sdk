using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Navigation
{
    public static class ElementNavigatorSlicingExtensions
    {

        /// <summary>Determines if the element with the specified name represents a type slice for the current (choice) element.</summary>
        /// <returns><c>true</c> if the element name represents a type slice of the current element, <c>false</c> otherwise.</returns>
        internal static bool IsCandidateTypeSlice(this ElementDefinitionNavigator nav, string diffName)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            return ElementDefinitionNavigator.IsRenamedChoiceElement(nav.PathName, diffName);
        }

        /// <summary>Move the navigator to the next type slice of the (choice) element with the specified name, if it exists.</summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToNextTypeSlice(this ElementDefinitionNavigator nav, string name)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            var bm = nav.Bookmark();

            while (nav.MoveToNext())
            {
                if (ElementDefinitionNavigator.IsRenamedChoiceElement(name, nav.PathName)) return true;
            }

            nav.ReturnToBookmark(bm);
            return false;
        }

        /// <summary>Move to last direct child element with same path as current element.</summary>
        /// <param name="nav">An <see cref="ElementDefinitionNavigator"/> instance.</param>
        /// <param name="sliceName">The optional target slice name, or <c>null</c>. Used for reslicing.</param>
        /// <returns><c>true</c> if the cursor has moved at least a single element, <c>false</c> otherwise</returns>
        public static bool MoveToLastSlice(this ElementDefinitionNavigator nav, string sliceName)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            if (nav.Current == null) { throw Error.Argument("nav", "Cannot move to last slice. Current node is not set."); }
            // if (nav.Current.Base == null) { throw Error.Argument("nav", "Cannot move to last slice. Current node has no Base.path component (path '{0}').".FormatWith(nav.Path)); }

            var bm = nav.Bookmark();
            var basePath = nav.Current.Base != null ? nav.Current.Base.Path : nav.Path;
            // if (string.IsNullOrEmpty(basePath)) { throw Error.Argument("nav", "Cannot move to last slice. Current node has no Base.path component (path '{0}').".FormatWith(nav.Path)); }

            var result = false;
            // while (nav.MoveToNext())
            do
            {
                var baseComp = nav.Current.Base != null ? nav.Current.Base.Path : nav.Path;
                if (baseComp != null && (baseComp == basePath || ElementDefinitionNavigator.IsRenamedChoiceElement(basePath, baseComp)))
                {
                    if (sliceName == null || nav.Current.SliceName == sliceName)
                    {
                        // Match, advance cursor
                        bm = nav.Bookmark();
                        result = true;
                    }
                    // Otherwise advance to next slice entry
                }
                else
                {
                    // Mismatch, back up to previous element and exit
                    nav.ReturnToBookmark(bm);
                    break;
                }
            } while (nav.MoveToNext());
            return result;
        }

        /// <summary>
        /// If the current element has the specified name, then maintain position and return true.
        /// Otherwise move to the next sibling element with the specified slice name, if it exists.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToNextSlice(this ElementDefinitionNavigator nav, string sliceName)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            if (nav.Current == null) { throw Error.Argument("nav", "Cannot move to next slice. Current node is not set."); }

            var bm = nav.Bookmark();
            var basePath = nav.Current.Base != null ? nav.Current.Base.Path : nav.Path;

            var result = false;
            do
            {
                var baseComp = nav.Current.Base != null ? nav.Current.Base.Path : nav.Path;
                if (baseComp != null && (baseComp == basePath || ElementDefinitionNavigator.IsRenamedChoiceElement(basePath, baseComp)))
                {
                    if (nav.Current.SliceName == sliceName)
                    {
                        // Match!
                        result = true;
                        break;
                    }
                }
                else
                {
                    // Mismatch, back up to previous element and exit
                    nav.ReturnToBookmark(bm);
                    break;
                }
            } while (nav.MoveToNext());
            return result;
        }


        //TODO: Discuss with Michel why he uses Base path (or definition path), instead of just definition path
        internal static IEnumerable<Bookmark> FindMemberSlices(this ElementDefinitionNavigator intro)
        {
            var bm = intro.Bookmark();
            var path = intro.Current.Path;
            var name = intro.Current.SliceName;
            var result = new List<Bookmark>();

            while (intro.MoveToNext() && intro.Path == path)
            {
                if (name == null)
                {
                    // If this is the "root" slice-intro for the group (the un-resliced original element), my slices
                    // would be the unnamed slices (though this is strictly seen not correct, every slice needs a name)
                    // and every slice with a non-resliced name
                    if (intro.Current.SliceName == null) yield return intro.Bookmark();
                    if (!ElementDefinitionNavigator.IsResliceName(intro.Current.SliceName)) yield return intro.Bookmark();
                }
                else
                {
                    // Else, if I am a named slice, I am a slice myself, but also the intro to a nested re-sliced group,
                    // so include only my children in my group...
                    if (ElementDefinitionNavigator.GetBaseSliceName(intro.Current.SliceName) == name)
                        yield return intro.Bookmark();
                }
                
                // Else...there might be something wrong, I need to add logic here to find slices that are out of 
                // order, of have a resliced name that does not start with the last slice intro we found.
            }

            intro.ReturnToBookmark(bm);
        }

    }
}
