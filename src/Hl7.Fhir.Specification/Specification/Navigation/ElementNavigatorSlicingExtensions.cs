/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
 
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;

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
                if (StringComparer.Ordinal.Equals(nav.Current.SliceName, sliceName))
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
            if (nav.AtRoot) { throw Error.Argument(nameof(nav), "Cannot move navigator to previous slice. Current node is not set."); }
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
            if (nav.AtRoot) { throw Error.Argument(nameof(nav), "Cannot move navigator to next slice. Current node is not set."); }

            var bm = nav.Bookmark();

            var name = nav.PathName;
            var startSliceName = nav.Current.SliceName;
            var startBaseSliceName = ElementDefinitionNavigator.GetBaseSliceName(startSliceName);
            while (nav.MoveToNext(name))
            {
                var sliceName = nav.Current.SliceName;
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
        /// Advance the navigator to the next sibling slice with the specified <paramref name="sliceName"/>, if it exists.
        /// Skip any existing child elements and/or child reslicing constraints.
        /// Otherwise remain positioned at the current element.
        /// </summary>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool MoveToNextSlice(this ElementDefinitionNavigator nav, string sliceName)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.AtRoot) { throw Error.Argument(nameof(nav), "Cannot move navigator to next slice. Current node is not set."); }

            var bm = nav.Bookmark();
            while (nav.MoveToNextSlice())
            {
                if (StringComparer.Ordinal.Equals(nav.Current.SliceName, sliceName))
                {
                    return true;
                }
            }
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
            if (nav.AtRoot) { throw Error.Argument(nameof(nav), "Cannot move navigator to previous slice. Current node is not set."); }

            var bm = nav.Bookmark();

            var name = nav.PathName;
            var startSliceName = nav.Current.SliceName;
            var startBaseSliceName = ElementDefinitionNavigator.GetBaseSliceName(startSliceName);
            while (nav.MoveToPrevious(name))
            {
                var sliceName = nav.Current.SliceName;
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
            if (nav.AtRoot) { throw Error.Argument(nameof(nav), "Cannot move navigator to previous slice. Current node is not set."); }

            var sliceName = nav.Current.SliceName;
            if (string.IsNullOrEmpty(sliceName)) { throw Error.Argument(nameof(nav), "The current element is not a named slice."); }

            var bm = nav.Bookmark();

            if (nav.MoveToNextSliceAtAnyLevel())
            {
                if (ElementDefinitionNavigator.IsResliceOf(nav.Current.SliceName, sliceName))
                {
                    return true;
                }
            }

            // No match, restore original position
            nav.ReturnToBookmark(bm);
            return false;
        }

        /// <summary>
        /// Returns <c>true</c> if the specified <paramref name="sliceName"/> matches
        /// the sliceName of the current element, or represents a direct child reslice.
        /// </summary>
        internal static bool IsSliceBase(this ElementDefinitionNavigator nav, string sliceName)
        {
            var baseSliceName = nav.Current.SliceName;
            return StringComparer.Ordinal.Equals(sliceName, baseSliceName)
                   || ElementDefinitionNavigator.IsDirectResliceOf(sliceName, baseSliceName);

        }

        /// <summary>
        /// Try to position the navigator on a (sibling) slice that is a base for the specified (derived) sliceName.
        /// Also handles re-slices. Maintain current position if match.
        /// </summary>
        internal static bool MoveToSliceBase(this ElementDefinitionNavigator nav, string sliceName)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.AtRoot) { throw Error.Argument(nameof(nav), "Cannot move navigator to base slice. Current node is not set."); }

            //if (string.IsNullOrEmpty(nav.Current.SliceName) && nav.Current.Slicing is null) { return false; }
            // throw Error.Argument(nameof(nav), "Cannot move navigator to base slice. Current node is not a named slice.");

            var bm = nav.Bookmark();

            do
            {
                if (nav.IsSliceBase(sliceName))
                {
                    return true;
                }
            } while (nav.MoveToNextSlice());

            nav.ReturnToBookmark(bm);
            return false;
        }


        /// <summary>
        /// Enumerate any succeeding direct child slices of the current slice intro.
        /// Skip any intermediate child elements and re-slice elements.
        /// When finished, return the navigator to the initial position.
        /// </summary>
        /// <param name="intro"></param>
        /// <returns>A sequence of <see cref="Bookmark"/> instances for the positions of the child slices.</returns>
        public static IEnumerable<Bookmark> FindMemberSlices(this ElementDefinitionNavigator intro)
        {
            if (!intro.IsSlicing()) throw new ArgumentException("Member slices can only be found relative to an intro slice.");

            var bm = intro.Bookmark();

            var pathName = intro.PathName;
            var introSliceName = intro.Current.SliceName;

            while (intro.MoveToNext(pathName))
            {
                var currentSliceName = intro.Current.SliceName;
                if (ElementDefinitionNavigator.IsDirectSliceOf(currentSliceName, introSliceName))
                {
                    yield return intro.Bookmark();
                }
            }

            intro.ReturnToBookmark(bm);
        }


        /// <summary>Recursively clone the current element and all it's children and return a new navigator for the resulting subtree.</summary>
        /// <returns>A new <see cref="ElementDefinitionNavigator"/> instance that wraps the cloned element list.</returns>
        internal static ElementDefinitionNavigator CloneSubtree(this ElementDefinitionNavigator nav)
        {
            nav.ThrowIfNullOrNotPositioned(nameof(nav));

            var result = new ElementDefinitionNavigator(new ElementDefinition[] { (ElementDefinition)nav.Current.DeepCopy() });
            result.MoveToFirstChild();
            result.CopyChildren(nav);
            return result;
        }

    }
}
