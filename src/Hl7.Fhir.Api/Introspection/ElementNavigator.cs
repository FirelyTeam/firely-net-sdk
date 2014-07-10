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
    public class ElementNavigator
    {
        public ElementNavigator(Profile.ProfileStructureComponent structure) : this(structure.Element)
        {
        }

        private ElementNavigator(IList<Profile.ElementComponent> elements)
        {
            if (elements == null || elements.Count == 0)
                throw Error.Argument("structure", "Structure does not contain any Elements");

            Elements = elements.ToList();      // make a copy of the *list of* elements
            OrdinalPosition = 0;
        }

        public ElementNavigator(ElementNavigator other) : this(other.Elements)
        {
            OrdinalPosition = other.OrdinalPosition;
        }

        public int OrdinalPosition { get; private set;  }

        public IList<Profile.ElementComponent> Elements { get; private set; }

        public ElementNavigator Clone()
        {
            return new ElementNavigator(this);
        }

        public Profile.ElementComponent Current
        {
            get { return Elements[OrdinalPosition]; }
        }

        public string Name
        {
            get { return Current.GetNameFromPath(); }
        }

        public string Path
        {
            get { return Current.Path; }
        }

        public int Count
        {
            get { return Elements.Count; }
        }

        public string ParentPath
        {
            get { return Current.GetParentNameFromPath(); }
        }


//----------------------------------
//
// Basic relative movement methods
// 
//----------------------------------

        public bool MoveToNext()
        {
            var searchPos = OrdinalPosition + 1;
            
            // Skip children of the element
            while (searchPos < Count && isDeeperPath(Path, Elements[searchPos].Path))
                    searchPos++;

            // Check whether found an element that is the next non-child element AND has the same parent
            // (which is then our sibling)
            if (searchPos < Count)
            {
                var searchPath = Elements[searchPos].Path;

                if (isDirectChildPath(ParentPath,searchPath))
                {
                    OrdinalPosition = searchPos;
                    return true;
                }
            }

            return false;
        }


        private int numberOfParts(string path)
        {
            var count = 0;
            for (var i = 0; i < path.Length; i++)
                if (path[i] == '.') count++;

            return count;
        }

        public bool MoveToPrevious()
        {
            var searchPos = OrdinalPosition - 1;

            // Skip children of the previous sibling (if any)
            while (searchPos >= 0 && isDeeperPath(Path, Elements[searchPos].Path))
                searchPos--;


            // Check whether found an element that is the next non-child element AND has the same parent
            // (which is then our sibling)
            if (searchPos >= 0)
            {
                var searchPath = Elements[searchPos].Path;

                if (isDirectChildPath(ParentPath, searchPath))
                {
                    OrdinalPosition = searchPos;
                    return true;
                }
            }

            return false;
        }


        private bool isDeeperPath(string me, string that)
        {
            return numberOfParts(that) > numberOfParts(me);
        }

        private bool isDirectChildPath(string parent, string child)
        {
            return child.StartsWith(parent + ".") && child.IndexOf('.', parent.Length + 1) == -1;
        }


        public bool MoveToFirstChild()
        {
            var childPos = OrdinalPosition + 1;

            if (childPos < Count && isDirectChildPath(Path, Elements[childPos].Path))
            {
                OrdinalPosition = childPos;
                return true;
            }

            return false;
        }


        public bool MoveToParent()
        {
            if (ParentPath == String.Empty) return false;

            return JumpTo(ParentPath);
        }


        public bool MoveToChild(string name)
        {
            if (MoveToFirstChild())
            {
                do
                {
                    if(Name == name) return true;
                }
                while (MoveToNext());
                MoveToParent();
            }

            return false;
        }

        public bool MoveToNext(string name)
        {
            bookmark();

            while (MoveToNext())
            {
                if (Name == name) return true;
            }

            returnToBookmark();
            return false;           
        }


        public bool MoveToPrevious(string name)
        {
            bookmark();

            while (MoveToPrevious())
            {
                if (Name == name) return true;
            }

            returnToBookmark();
            return false;
        }


        public bool HasChildren
        {
            get
            {
                if (MoveToFirstChild())
                {
                    MoveToParent();
                    return true;
                }
                return false;
            }
        }

        public bool IsSamePosition(ElementNavigator other)
        {
            return this.OrdinalPosition == other.OrdinalPosition;
        }


        private int? _bookmarkPos;

        private void bookmark()
        {
            _bookmarkPos = OrdinalPosition;
        }

        public bool returnToBookmark()
        {
            if (_bookmarkPos == null) return false;

            OrdinalPosition = _bookmarkPos.Value;
            return true;
        }


//----------------------------------
//
// Methods that move to absolute locations
//
//----------------------------------

        public void MoveToRoot()
        {
            OrdinalPosition = 0;
        }


        public bool MoveTo(ElementNavigator other)
        {
            if (other.OrdinalPosition < Count)
            {
                this.OrdinalPosition = other.OrdinalPosition;
                return true;
            }

            return false;
        }

        public bool Approach(string path)
        {
            var bestMatch = -1;
            var matchSize = -1;

            for (var ix = 0; ix < Count; ix++)
            {
                var scanPath = Elements[ix].Path; 

                // If we found a path that is exactly the same, we're ready, this is the best match
                if (scanPath == path)
                {
                    bestMatch = ix;
                    break;
                }

                // Else, if the path of this element is "on the way" to the path given
                else if (path.StartsWith(scanPath + "."))
                {
                    // ...check whether it's a longer match than any previous found before
                    if (scanPath.Length > matchSize)
                    {
                        matchSize = scanPath.Length;
                        bestMatch = ix;
                    }
                }
            }

            if (bestMatch == -1)
                return false;   // No match found
            else
            {
                // Move to the best match found
                OrdinalPosition = bestMatch;
                return true;
            }
        }

        public bool JumpTo(string path)
        {
            bookmark();

            if (Approach(path))
            {
                if (Path == path) return true;
            }

            returnToBookmark();
            return false;
        }



//----------------------------------
//
// Methods that alter the list of elements
// 
//----------------------------------

        public void InsertBefore(Profile.ElementComponent sibling)
        {
            Elements.Insert(OrdinalPosition, sibling);
            OrdinalPosition++;      // make sure we are still the active node, even after the insert

            // Adjust the sibling's path so it's parent is the same as the current node
            sibling.Path = ParentPath + "." + sibling.GetNameFromPath();
        }

        public void InsertAfter(Profile.ElementComponent sibling)
        {
            if (OrdinalPosition == Count - 1) // At last position
                Elements.Add(sibling);
            else
                Elements.Insert(OrdinalPosition+1, sibling);

            // Adjust the sibling's path so it's parent is the same as the current node
            sibling.Path = ParentPath + "." + sibling.GetNameFromPath();
        }

        public void AppendChild(Profile.ElementComponent child)
        {
            if (HasChildren)
            {
                bookmark();
                MoveToFirstChild();
                while (MoveToNext()) ;
                InsertAfter(child);
                returnToBookmark();
            }
            else
            {
                var name = Name;
                InsertAfter(child);

                // Correct the name to be a true child, not a sibling -> this actually creates a child
                child.Path = Path + "." + child.GetNameFromPath();
            }
        }

        public bool Delete()
        {
            if (Elements.Count == 0) return false;

            // Can't delete a parent, need to delete children first
            if (HasChildren) return false;

            // Special case, just the root is left
            if (Elements.Count == 1)
            {
                Elements.Clear();
            }
            else if (MoveToNext())
            {
                // I have a next sibling, delete me and let my next sibling take my place as "current"
                MoveToPrevious();
                Elements.RemoveAt(OrdinalPosition);
            }
            else if (MoveToPrevious())
            {
                // I am the last of the children, but I have preceding siblings, let the prevous take my place as "current"
                MoveToNext();
                Elements.RemoveAt(OrdinalPosition);
                OrdinalPosition--;
            }
            else
            {
                // I am the last child of a parent, after I disappear, my parent will be "current"
                Elements.RemoveAt(OrdinalPosition);
                OrdinalPosition--;
            }

            return true;
        }
    }
}
