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
        public ElementNavigator(Profile.ProfileStructureComponent structure)
        {
            if (structure.Element == null || structure.Element.Count == 0)
                throw Error.Argument("structure", "Structure does not contain any Elements");

            Structure = structure;
            OrdinalPosition = 0;
        }

        public int OrdinalPosition { get; private set;  }

        public Profile.ProfileStructureComponent Structure { get; private set; }

        public ElementNavigator Clone()
        {
            var result = new ElementNavigator(this.Structure);
            result.OrdinalPosition = this.OrdinalPosition;
            return result;
        }

        public Profile.ElementComponent Current
        {
            get { return Structure.Element[OrdinalPosition]; }
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
            get { return Structure.Element.Count; }
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
            while (searchPos < Count && isDeeperPath(Path, Structure.Element[searchPos].Path))
                    searchPos++;

            // Check whether found an element that is the next non-child element AND has the same parent
            // (which is then our sibling)
            if (searchPos < Count)
            {
                var searchPath = Structure.Element[searchPos].Path;

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
            while (searchPos >= 0 && isDeeperPath(Path, Structure.Element[searchPos].Path))
                searchPos--;


            // Check whether found an element that is the next non-child element AND has the same parent
            // (which is then our sibling)
            if (searchPos >= 0)
            {
                var searchPath = Structure.Element[searchPos].Path;

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

            if (childPos < Count && isDirectChildPath(Path, Structure.Element[childPos].Path))
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
            var navClone = this.Clone();

            while (MoveToNext())
            {
                if (Name == name) return true;
            }
            
            MoveTo(navClone);
            return false;           
        }


        public bool MoveToPrevious(string name)
        {
            var navClone = this.Clone();

            while (MoveToPrevious())
            {
                if (Name == name) return true;
            }

            MoveTo(navClone);
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
                var scanPath = Structure.Element[ix].Path; 
                if (scanPath == path)
                {
                    bestMatch = ix;
                    break;
                }
                else if (path.StartsWith(scanPath + "."))
                {
                    if (scanPath.Length > matchSize)
                    {
                        matchSize = scanPath.Length;
                        bestMatch = ix;
                    }
                }
            }

            if (bestMatch == -1)
                return false;
            else
            {
                OrdinalPosition = bestMatch;
                return true;
            }
        }

        public bool JumpTo(string path)
        {
            var navClone = this.Clone();

            if (Approach(path))
            {
                if (Path == path) return true;
            }

            MoveTo(navClone);
            return false;
        }



//----------------------------------
//
// Methods that alter the list of elements
// 
//----------------------------------

        public void InsertBefore(Profile.ElementComponent sibling)
        {
            Structure.Element.Insert(OrdinalPosition, sibling);
            OrdinalPosition++;      // make sure we are still the active node, even after the insert

            // Adjust the sibling's path so it's parent is the same as the current node
            sibling.Path = ParentPath + "." + sibling.GetNameFromPath();
        }

        public void InsertAfter(Profile.ElementComponent sibling)
        {
            if (OrdinalPosition == Count - 1) // At last position
                Structure.Element.Add(sibling);
            else
                Structure.Element.Insert(OrdinalPosition+1, sibling);

            // Adjust the sibling's path so it's parent is the same as the current node
            sibling.Path = ParentPath + "." + sibling.GetNameFromPath();
        }

        public void AppendChild(Profile.ElementComponent child)
        {
            if (HasChildren)
            {
                var navCopy = this.Clone();
                MoveToFirstChild();
                while (MoveToNext()) ;
                InsertAfter(child);
                MoveTo(navCopy);
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
            if (Structure.Element.Count == 0) return false;
            
            // Special case, just the root is left
            if (Structure.Element.Count == 1)
            {
                Structure.Element.Clear();
                return true;
            }

            // Can't delete a parent, need to delete children first
            if (HasChildren) return false;
            
            if (MoveToNext())
            {
                // I have a next sibling, delete me and let my next sibling take my place as "current"
                MoveToPrevious();
                Structure.Element.RemoveAt(OrdinalPosition);
            }
            else if (MoveToPrevious())
            {
                // I am the last of the children, but I have preceding siblings, let the prevous take my place as "current"
                MoveToNext();
                Structure.Element.RemoveAt(OrdinalPosition);
                OrdinalPosition--;
            }
            else
            {
                // I am the last child of a parent, after I disappear, my parent will be "current"
                Structure.Element.RemoveAt(OrdinalPosition);
                OrdinalPosition--;
            }
            
            return true;
        }
    }
}
