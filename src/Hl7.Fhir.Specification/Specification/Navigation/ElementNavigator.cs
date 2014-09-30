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
    public class ElementNavigator : BaseElementNavigator
    {
        public ElementNavigator(Profile.ProfileStructureComponent structure)
        {
            setupElems(structure.Element);
            Structure = structure;
        }

        public ElementNavigator(ElementNavigator other)
        {
            setupElems(other._elements);
            Structure = other.Structure;
            OrdinalPosition = other.OrdinalPosition;
        }

        private void setupElems(IList<Profile.ElementComponent> elements)
        {
            if (elements == null) throw Error.ArgumentNull("elements");

            _elements = elements.ToList();      // make a *shallow* copy of the list of elements
            OrdinalPosition = null;
        }

        internal int? OrdinalPosition { get; private set;  }

        public Profile.ProfileStructureComponent Structure { get; private set; }

        private IList<Profile.ElementComponent> _elements;

        public override Profile.ElementComponent Current
        {
            get { return OrdinalPosition != null ? _elements[OrdinalPosition.Value] : null; }
        }

        public override int Count
        {
            get { return _elements.Count; }
        }


//----------------------------------
//
// Basic relative movement methods
// 
//----------------------------------

        public override bool MoveToNext()
        {
            if (OrdinalPosition == null) return false;

            var searchPos = positionAfter();

            // Check whether found an element that is the next non-child element AND has the same parent
            // (which is then our sibling)
            if (searchPos < Count)
            {
                var searchPath = _elements[searchPos].Path;

                if (IsDirectChildPath(ParentPath,searchPath))
                {
                    OrdinalPosition = searchPos;
                    return true;
                }
            }

            return false;
        }

        private int positionAfter()
        {
            var searchPos = OrdinalPosition.Value + 1;

            // Skip children of the element
            while (searchPos < Count && isDeeperPath(Path, _elements[searchPos].Path))
                searchPos++;
            return searchPos;
        }
     

        public override bool MoveToPrevious()
        {
            if (OrdinalPosition == null) return false;

            var searchPos = positionBefore();


            // Check whether found an element that is the next non-child element AND has the same parent
            // (which is then our sibling)
            if (searchPos >= 0)
            {
                var searchPath = _elements[searchPos].Path;

                if (IsDirectChildPath(ParentPath, searchPath))
                {
                    OrdinalPosition = searchPos;
                    return true;
                }
            }

            return false;
        }

        private int positionBefore()
        {
            var searchPos = OrdinalPosition.Value - 1;

            // Skip children of the previous sibling (if any)
            while (searchPos >= 0 && isDeeperPath(Path, _elements[searchPos].Path))
                searchPos--;
            return searchPos;
        }


        public override bool HasChildren
        {
            get 
            {
                // Special case, at document root
                if (OrdinalPosition == null && Count > 0) 
                    return true;

                var childPos = OrdinalPosition.Value + 1;

                return childPos < Count && IsDirectChildPath(Path, _elements[childPos].Path);
            }
        }


        public override bool MoveToFirstChild()
        {
            if (!HasChildren) return false;

            OrdinalPosition = OrdinalPosition != null ? OrdinalPosition.Value + 1 : 0;

            return true;
        }


        public override bool MoveToParent()
        {
            if (OrdinalPosition == null)
                return false;
            else if (OrdinalPosition == 0)
            {
                OrdinalPosition = null; // move to "root"
                return true;
            }
            else
            {
                var searchPos = OrdinalPosition.Value - 1;

                // Skip back until we find a node with a one step shorter path that is our prefix
                while (searchPos >= 0 && !IsDirectChildPath(_elements[searchPos].Path, Path))
                    searchPos--;

                if (searchPos == -1)
                    throw Error.InvalidOperation("Element list is inconsistent and does not contain a parent for " + Path);

                OrdinalPosition = searchPos;
                return true;
            }
        }


        public override void Reset()
        {
            OrdinalPosition = null;
        }


        public bool JumpToNameReference(string nameReference)
        {
            if(Count == 0) return false;

            for (int pos = 0; pos < Count; pos++)
            {
                if (_elements[pos].Name == nameReference)
                {
                    OrdinalPosition = pos;
                    return true;
                }
            }

            return false;
        }


        //----------------------------------
        //
        // Bookmark operations
        //
        //----------------------------------


        public override Bookmark Bookmark()
        {
            return new Bookmark() { data = Current };
        }


        public override bool IsAtBookmark(Bookmark bookmark)
        {
            if (bookmark.data == null)
                 return OrdinalPosition == null;

            var elem = bookmark.data as Profile.ElementComponent;

            return this.Current == elem;
        }

        public override bool ReturnToBookmark(Bookmark bookmark)
        {
            if (bookmark.data == null)
            {
                OrdinalPosition = null;
                return true;
            }
            else
            {
                var elem = bookmark.data as Profile.ElementComponent;

                if (elem == null) return false;

                var index = _elements.IndexOf(elem);

                if (index != -1)
                {
                    OrdinalPosition = index;
                    return true;
                }
                else
                    return false;
            }
        }


   

//----------------------------------
//
// Methods that alter the list of elements
// 
//----------------------------------
        
        /// <summary>
        /// Inserts the element passed in as a sibling to the element the navigator is currently on. 
        /// The navigator will move to the inserted element.
        /// </summary>
        /// <param name="sibling"></param>
        /// <returns></returns>
        public override bool InsertBefore(Profile.ElementComponent sibling)
        {
            if (!canInsertSiblingHere()) return false;

            var newSiblingPath = ParentPath + "." + sibling.GetNameFromPath();

            _elements.Insert(OrdinalPosition.Value, sibling);

            // Adjust the sibling's path so it's parent is the same as the current node
            sibling.Path = newSiblingPath;

            // Note: we're now positioned on the newly inserted element

            return true;
        }

        private bool canInsertSiblingHere()
        {
            // We're not positioned anywhere...
            if (OrdinalPosition == null) return false;

            // Cannot insert a sibling to the unique root element
            if (OrdinalPosition == 0) return false;

            return true;
        }


        /// <summary>
        /// Inserts the element passed in as a sibling to the element the navigator is currently on. 
        /// The navigator will move to the inserted element.
        /// </summary>
        /// <param name="sibling"></param>
        /// <returns></returns>
        public override bool InsertAfter(Profile.ElementComponent sibling)
        {
            if (!canInsertSiblingHere()) return false;

            var insertPosition = positionAfter();
            var newSiblingPath = ParentPath + "." + sibling.GetNameFromPath();

            if (insertPosition == Count) // At last position
                _elements.Add(sibling);
            else
                _elements.Insert(insertPosition, sibling);

            // Adjust the sibling's path so it's parent is the same as the current node
            sibling.Path = newSiblingPath;

            // Navigate to newly inserted node
            OrdinalPosition = insertPosition;

            return true;
        }


        /// <summary>
        /// Inserts the element passed in as a child of the element the navigator is currently on. 
        /// The navigator will move to the inserted element.
        /// </summary>
        /// <param name="sibling"></param>
        /// <returns></returns>
        /// <remarks>You can only insert a child for an element does not have children yet.</remarks>
        public override bool InsertFirstChild(Profile.ElementComponent child)
        {
            if(Count == 0)
            {
                // Special case, insert a new root
                _elements.Add(child);
                child.Path = child.GetNameFromPath();
                OrdinalPosition = 0;
                return true;
            }
            else if(HasChildren)
                return false;       // Cannot insert another child, there's already one.
            else
            {
                var newSiblingPath = Path + "." + child.GetNameFromPath();
                
                if (OrdinalPosition == Count - 1) // At last position
                    _elements.Add(child);
                else
                    _elements.Insert(OrdinalPosition.Value + 1, child);

                // Set the name to be a true child -> this actually creates a child
                child.Path = newSiblingPath;

                // Navigate to newly inserted child
                OrdinalPosition += 1;

                return true;
            }
        }

    
        public override bool Delete()
        {
            if (OrdinalPosition == null) return false;

            // Can't delete a parent, need to delete children first
            if (HasChildren) return false;

            // Special case, just the root is left
            if (_elements.Count == 1)
            {
                _elements.Clear();
                OrdinalPosition = null;
            }
            else if (MoveToNext())
            {
                // I have a next sibling, delete me and let my next sibling take my place as "current"
                MoveToPrevious();
                _elements.RemoveAt(OrdinalPosition.Value);
            }
            else if (MoveToPrevious())
            {
                // I am the last of the children, but I have preceding siblings, let the prevous take my place as "current"
                MoveToNext();
                _elements.RemoveAt(OrdinalPosition.Value);
                OrdinalPosition--;
            }
            else
            {
                // I am the last child of a parent, after I disappear, my parent will be "current"
                _elements.RemoveAt(OrdinalPosition.Value);
                OrdinalPosition--;
            }

            return true;
        }

        public void CommitChanges()
        {
            Structure.Element = new List<Profile.ElementComponent>(_elements);
        }

        private static bool isDeeperPath(string me, string that)
        {
            return NumberOfParts(that) > NumberOfParts(me);
        }

        public static bool IsSibling(string me, string him)
        {
            return GetParentPath(me) == GetParentPath(him);
        }

        public static bool IsDirectChildPath(string parent, string child)
        {
            // A child with a single path segment, is "root" and child of "no" parent
            //if (parent == String.Empty && child.IndexOf('.') == -1) return true;

            return child.StartsWith(parent + ".") && child.IndexOf('.', parent.Length + 1) == -1;
        }

        public static string GetParentPath(string child)
        {
            var dot = child.LastIndexOf(".");
            return dot != -1 ? child.Substring(0, dot) : String.Empty;
        }

        internal static int NumberOfParts(string path)
        {
            var count = 1;
            for (var i = 0; i < path.Length; i++)
                if (path[i] == '.') count++;

            return count;
        }

    } 
}
