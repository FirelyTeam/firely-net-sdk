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
        public ElementNavigator(IList<ElementDefinition> elements)
        {
            if (elements == null) throw Error.ArgumentNull("elements");

            Elements = elements.ToList();      // make a *shallow* copy of the list of elements
            OrdinalPosition = null;
        }

        public ElementNavigator(ElementNavigator other)
        {
            if (other == null) throw Error.ArgumentNull("other");

            Elements = other.Elements.ToList();
            OrdinalPosition = other.OrdinalPosition;
        }


        internal int? OrdinalPosition { get; private set; }

        public IList<ElementDefinition> Elements { get; private set; }

        public override ElementDefinition Current
        {
            get { return OrdinalPosition != null ? Elements[OrdinalPosition.Value] : null; }
        }

        public override int Count
        {
            get { return Elements.Count; }
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
                var searchPath = Elements[searchPos].Path;

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
            while (searchPos < Count && isDeeperPath(Path, Elements[searchPos].Path))
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
                var searchPath = Elements[searchPos].Path;

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
            while (searchPos >= 0 && isDeeperPath(Path, Elements[searchPos].Path))
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

                return childPos < Count && IsDirectChildPath(Path, Elements[childPos].Path);
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
                while (searchPos >= 0 && !IsDirectChildPath(Elements[searchPos].Path, Path))
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
                if (Elements[pos].Name == nameReference)
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

            var elem = bookmark.data as ElementDefinition;

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
                var elem = bookmark.data as ElementDefinition;

                if (elem == null) return false;

                var index = Elements.IndexOf(elem);

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
        public override bool InsertBefore(ElementDefinition sibling)
        {
            if (!canInsertSiblingHere()) return false;

            var newSiblingPath = ParentPath + "." + sibling.GetNameFromPath();

            Elements.Insert(OrdinalPosition.Value, sibling);

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
        public override bool InsertAfter(ElementDefinition sibling)
        {
            if (!canInsertSiblingHere()) return false;

            var insertPosition = positionAfter();
            var newSiblingPath = ParentPath + "." + sibling.GetNameFromPath();

            if (insertPosition == Count) // At last position
                Elements.Add(sibling);
            else
                Elements.Insert(insertPosition, sibling);

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
        /// <param name="child"></param>
        /// <returns></returns>
        /// <remarks>You can only insert a child for an element does not have children yet.</remarks>
        public override bool InsertFirstChild(ElementDefinition child)
        {
            if(Count == 0)
            {
                // Special case, insert a new root
                Elements.Add(child);
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
                    Elements.Add(child);
                else
                    Elements.Insert(OrdinalPosition.Value + 1, child);

                // Set the name to be a true child -> this actually creates a child
                child.Path = newSiblingPath;

                // Navigate to newly inserted child
                OrdinalPosition += 1;

                return true;
            }
        }


        public bool Duplicate()
        {
            var bm = Bookmark();
            return DuplicateAfter(bm);
        }

        public bool DuplicateAfter(Bookmark target)
        {
            if (OrdinalPosition == null) return false;
            var start = OrdinalPosition.Value;
            var end = positionAfter();

            if (!ReturnToBookmark(target)) return false;
            var dest = positionAfter();

            var source = Elements.Skip(start).Take(end-start).ToList();

            foreach (var elem in source.Reverse<ElementDefinition>())
                Elements.Insert(dest, (ElementDefinition)elem.DeepCopy());

            OrdinalPosition = dest;
            return true;
        }
    
        public override bool Delete()
        {
            if (OrdinalPosition == null) return false;

            // Can't delete a parent, need to delete children first
            if (HasChildren) return false;

            // Special case, just the root is left
            if (Elements.Count == 1)
            {
                Elements.Clear();
                OrdinalPosition = null;
            }
            else if (MoveToNext())
            {
                // I have a next sibling, delete me and let my next sibling take my place as "current"
                MoveToPrevious();
                Elements.RemoveAt(OrdinalPosition.Value);
            }
            else if (MoveToPrevious())
            {
                // I am the last of the children, but I have preceding siblings, let the prevous take my place as "current"
                MoveToNext();
                Elements.RemoveAt(OrdinalPosition.Value);
                OrdinalPosition--;
            }
            else
            {
                // I am the last child of a parent, after I disappear, my parent will be "current"
                Elements.RemoveAt(OrdinalPosition.Value);
                OrdinalPosition--;
            }

            return true;
        }

        /// <summary>
        /// Returns the list of elements passed to the constructor, including any changes made to the list using
        /// the modification functions of the navigator.
        /// </summary>
        /// <returns></returns>
        public List<ElementDefinition> ToListOfElements()
        {
            return new List<ElementDefinition>(Elements);
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


        public override string ToString()
        {
            var output = new StringBuilder();

            foreach (var elem in Elements)
            {
                output.AppendFormat("{0}{1}" + Environment.NewLine, elem == Current ? "*" : "", elem.Path);
            }

            return output.ToString();
        }

    } 
}
