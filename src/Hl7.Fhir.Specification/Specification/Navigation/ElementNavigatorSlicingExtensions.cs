using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Navigation
{
    public static class ElementNavigatorSlicingExtensions
    {

        /// <summary>Determines if the element with the specified name represents a type slice for the current (choice) element.</summary>
        /// <returns><c>true</c> if the element name represents a type slice of the current element, <c>false</c> otherwise.</returns>
        internal static bool IsRenamedChoiceElement(this ElementDefinitionNavigator nav, string diffName)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            return ElementDefinitionNavigator.IsRenamedChoiceElement(nav.PathName, diffName);
        }

        // [WMR 20161212] NEW

        // '' => ''                             found unnamed sibling (extension)
        // '' => A                              found sibling
        // '' => A/1                            not a sibling => return false
        // A => ( A/1 => A/2 => ) B             found sibling
        // A/1 => ( A/1/1 => A/1/2 => ) A/2     found sibling
        // A/1 => ( A/1/1 => A/1/2 => ) B       not a sibling => return false

        /// <summary>
        /// Advance the navigator the the immediately following slicing constraint in the current slice group, at any (re)slicing level.
        /// Skip any existing child elements.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToNextSliceAtAnyLevel(this ElementDefinitionNavigator nav) => nav.MoveToNext(nav.PathName);

        /// <summary>
        /// Advance the navigator to the next slice in the current slice group and on the current slicing level.
        /// Skip any existing child elements and/or child reslicing constraints.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToNextSlice(this ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            if (nav.Current == null) { throw Error.Argument("nav", "Cannot move to navigator next slice. Current node is not set."); }

            var bm = nav.Bookmark();

            var name = nav.PathName;
            var startSliceName = nav.Current.Name;
            var startBaseSliceName = ElementDefinitionNavigator.GetBaseSliceName(startSliceName);
            while (nav.MoveToNext(name))
            {
                var sliceName = nav.Current.Name;
                // Handle unnamed slices, eg. extensions
                if (startSliceName == null && sliceName == null) { return true; }

                if (ElementDefinitionNavigator.IsSiblingSliceOf(startSliceName, sliceName))
                {
                    return true;
                }

                if (startBaseSliceName != null && sliceName.Length > startBaseSliceName.Length && !sliceName.StartsWith(startBaseSliceName))
                {
                    break;
                }
            }

            // No match, restore original position
            nav.ReturnToBookmark(bm);
            return false;
        }

#if false
        // [WMR 20161212] OBSOLETE

        /// <summary>
        /// Advance to the first reslice of the current slice element, if it exists.
        /// Specifically, move to the next sibling element with the same path as the current element, if it exists and represents a reslice.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToFirstReslice(this ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            if (nav.Current == null) { throw Error.Argument("nav", "Cannot move navigator to first reslice. Current node is not set."); }

            var bm = nav.Bookmark();
            var sliceName = nav.Current.Name;
            if (sliceName == null) { throw Error.Argument("nav", "Cannot move navigator to first reslice. Current node has no slice name."); }

            if (nav.MoveToNextSliceAtAnyLevel()
                && ElementDefinitionNavigator.IsDirectResliceOf(nav.Current.Name, sliceName))
            {
                return true;
            }

            // No match, restore original position
            nav.ReturnToBookmark(bm);
            return false;
        }

        /// <summary>
        /// Reposition the navigator to the base slice of the current slice element, if it exists.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToBaseSlice(this ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            if (nav.Current == null) { throw Error.Argument("nav", "Cannot move navigator to parent slice. Current node is not set."); }

            var bm = nav.Bookmark();
            var sliceName = nav.Current.Name;
            if (sliceName == null) { throw Error.Argument("nav", "Cannot move navigator to parent slice. Current node has no slice name."); }
            // if (sliceName == null) { return false; }

            var baseName = ElementDefinitionNavigator.GetBaseSliceName(sliceName);
            if (baseName == null) { return false; }

            var name = nav.PathName;
            while (nav.MoveToPrevious(name))
            {
                var currentSliceName = nav.Current.Name;

                // Base slice?
                if (currentSliceName == baseName)
                {
                    return true; // Found it!
                }
            }

            // No match, restore original position
            nav.ReturnToBookmark(bm);
            return false;
        }

        public static bool MoveToLastSlice(this ElementDefinitionNavigator nav) => nav.MoveToLastSlice(nav.Current.Name);

        /// <summary>
        /// Advance to the last slice in the current (re-)slice group.
        /// Specifically, move to the last sibling element with the exact same path as the current element
        /// (and optionally with the same base slice name), if it exists.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToLastSlice(this ElementDefinitionNavigator nav, string baseSliceName)
        {
            if (nav == null) { throw Error.ArgumentNull("nav"); }
            if (nav.Current == null) { throw Error.Argument("nav", "Cannot move navigator to last slice. Current node is not set."); }

            var result = nav.MoveToNextSlice(baseSliceName);
            if (result)
            {
                while (nav.MoveToNextSlice(baseSliceName)) ;
            }
            return result;
        }

        /// <summary>Move the navigator to the next type slice of the (choice) element with the specified name, if it exists.</summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        [Obsolete("Multiple type constraints are not allowed; type constraint is NOT a type slice!")]
        public static bool MoveToNextRenamedChoiceElement(this ElementDefinitionNavigator nav, string name)
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
#endif

        //TODO: Discuss with Michel why he uses Base path (or definition path), instead of just definition path
        internal static IEnumerable<Bookmark> FindMemberSlices(this ElementDefinitionNavigator intro, bool atRoot)
        {
            var bm = intro.Bookmark();
            var path = intro.Current.Path;
            var name = intro.Current.Name;
            var result = new List<Bookmark>();

            while (intro.MoveToNext() && intro.Path == path)
            {
                if (atRoot)
                {
                    // Is this the slice-intro of the un-resliced original element? Then my name == null or
                    // (in DSTU2) my name has no slicing separator (since name is used both for slicing and
                    // re-use of constraints, e.g. Composition.section.name, just == null is not enough)
                    // I am the root slice, my slices would be the unnamed slices (though this is strictly seen not correct, every slice needs a name)
                    // and every slice with a non-resliced name
                    if (intro.Current.Name == null) yield return intro.Bookmark();
                    if (!ElementDefinitionNavigator.IsResliceName(intro.Current.Name)) yield return intro.Bookmark();
                }
                else
                {
                    // Else, if I am a named slice, I am a slice myself, but also the intro to a nested re-sliced group,
                    // so include only my children in my group...
                    if (ElementDefinitionNavigator.GetBaseSliceName(intro.Current.Name) == name)
                        yield return intro.Bookmark();
                }
                
                // Else...there might be something wrong, I need to add logic here to find slices that are out of 
                // order, of have a resliced name that does not start with the last slice intro we found.
            }

            intro.ReturnToBookmark(bm);
        }

    }
}
