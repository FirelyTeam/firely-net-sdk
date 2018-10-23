using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
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
        internal static bool IsRenamedChoiceTypeElement(this ElementDefinitionNavigator nav, string diffName)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            return ElementDefinitionNavigator.IsRenamedChoiceTypeElement(nav.PathName, diffName);
        }

        /// <summary>
        /// Advance the navigator to the immediately following slicing constraint in the current slice group, at any (re)slicing level.
        /// Skip any existing child elements.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToNextSliceAtAnyLevel(this ElementDefinitionNavigator nav) => nav.MoveToNext(nav.PathName);

        /// <summary>
        /// Advance the navigator forward to the slicing constraint in the current slice group with the specified slice name.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToNextSliceAtAnyLevel(this ElementDefinitionNavigator nav, string sliceName)
        {
            var bm = nav.Bookmark();
            while (nav.MoveToNextSliceAtAnyLevel())
            {
                if (StringComparer.Ordinal.Equals(nav.Current.Name, sliceName))
                {
                    return true;
                }
            }
            nav.ReturnToBookmark(bm);
            return false;
        }

/*
        /// <summary>
        /// If the current element is a slice entry, then advance the navigator to the first associated named slice.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToFirstSlice(this ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.Current == null) { throw Error.Argument(nameof(nav), "Cannot move navigator to previous slice. Current node is not set."); }
            if (nav.Current.Slicing != null)
            {
                var bm = nav.Bookmark();
                if (nav.MoveToNextSliceAtAnyLevel()) { return true; }
                nav.ReturnToBookmark(bm);
            }
            return false;
        }
*/

        /// <summary>
        /// Advance the navigator to the next slice in the current slice group and on the current slicing level.
        /// Skip any existing child elements and/or child reslicing constraints.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToNextSlice(this ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.Current == null) { throw Error.Argument(nameof(nav), "Cannot move navigator to next slice. Current node is not set."); }

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

        /// <summary>
        /// Advance the navigator to the previous slice in the current slice group and on the current slicing level.
        /// Skip any existing child elements and/or child reslicing constraints.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToPreviousSlice(this ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.Current == null) { throw Error.Argument(nameof(nav), "Cannot move navigator to previous slice. Current node is not set."); }

            var bm = nav.Bookmark();

            var name = nav.PathName;
            var startSliceName = nav.Current.Name;
            var startBaseSliceName = ElementDefinitionNavigator.GetBaseSliceName(startSliceName);
            while (nav.MoveToPrevious(name))
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

        /// <summary>
        /// Advance the navigator to the first reslice of the current named slice.
        /// Skip any existing child elements and/or child reslicing constraints.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToFirstReslice(this ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.Current == null) { throw Error.Argument(nameof(nav), "Cannot move navigator to previous slice. Current node is not set."); }

            var sliceName = nav.Current.Name;
            if (string.IsNullOrEmpty(sliceName)) { throw Error.Argument(nameof(nav), "The current element is not a named slice."); }

            var bm = nav.Bookmark();

            if (nav.MoveToNextSliceAtAnyLevel())
            {
                if (ElementDefinitionNavigator.IsResliceOf(nav.Current.Name, sliceName))
                {
                    return true;
                }
            }

            // No match, restore original position
            nav.ReturnToBookmark(bm);
            return false;
        }

        /// <summary>
        /// Enumerate any succeeding direct child slices of the specified element.
        /// Skip any intermediate child elements and re-slice elements.
        /// When finished, return the navigator to the initial position.
        /// </summary>
        /// <param name="intro"></param>
        /// <param name="atRoot">Specify <c>true</c> for finding direct (simple) slices, or <c>false</c> for finding re-slices.</param>
        /// <returns>A sequence of <see cref="Bookmark"/> instances.</returns>
        internal static IEnumerable<Bookmark> FindMemberSlices(this ElementDefinitionNavigator intro, bool atRoot)
        {
            var bm = intro.Bookmark();

            var path = intro.Current.Path;
            var pathName = intro.PathName;
            var name = intro.Current.Name;

            while (intro.MoveToNext(pathName))
            {
                var curName = intro.Current.Name;
                if (atRoot)
                {
                    // Is this the slice-intro of the un-resliced original element? Then my name == null or
                    // (in DSTU2) my name has no slicing separator (since name is used both for slicing and
                    // re-use of constraints, e.g. Composition.section.name, just == null is not enough)
                    // I am the root slice, my slices would be the unnamed slices (though this is strictly seen not correct, every slice needs a name)
                    // and every slice with a non-resliced name
                    if (curName == null)
                    {
                        yield return intro.Bookmark();
                    }
                    if (!ElementDefinitionNavigator.IsResliceName(curName))
                    {
                        yield return intro.Bookmark();
                    }
                }
                else
                {
                    // Else, if I am a named slice, I am a slice myself, but also the intro to a nested re-sliced group,
                    // so include only my children in my group...
                    if (ElementDefinitionNavigator.IsResliceOf(curName, name))
                    {
                        yield return intro.Bookmark();
                    }
                }
                
                // Else...there might be something wrong, I need to add logic here to find slices that are out of 
                // order, of have a resliced name that does not start with the last slice intro we found.
            }

            intro.ReturnToBookmark(bm);
        }

        /// <summary>Recursively clone the current element and all it's children and return a new navigator for the resulting subtree.</summary>
        /// <returns>A new <see cref="ElementDefinitionNavigator"/> instance that wraps the cloned element list.</returns>
        internal static ElementDefinitionNavigator CloneSubtree(this ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw new ArgumentNullException(nameof(nav)); }
            if (nav.Current == null) { throw new ArgumentException(nameof(nav)); }

            var result = new ElementDefinitionNavigator(new ElementDefinition[] { (ElementDefinition)nav.Current.DeepCopy() });
            result.MoveToFirstChild();
            result.CopyChildren(nav);
            return result;
        }

    }
}
