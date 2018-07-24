/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
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
using System.Diagnostics;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Navigation
{
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public partial class ElementDefinitionNavigator
    {
        internal ElementDefinitionNavigator()
        {
        }

        public ElementDefinitionNavigator(IList<ElementDefinition> elements, StructureDefinition definition)
        {
            if (elements == null) throw Error.ArgumentNull(nameof(elements));

            StructureDefinition = definition;
            Elements = elements.ToList();      // make a *shallow* copy of the list of elements
            OrdinalPosition = null;
        }

        public ElementDefinitionNavigator(StructureDefinition definition)
        {
            if (definition == null) throw Error.ArgumentNull(nameof(definition));
            if (definition.Snapshot == null) throw Error.Argument(nameof(definition), "The specified StructureDefinition has no snapshot component.");

            StructureDefinition = definition;
            Elements = definition.Snapshot.Element.ToList();      // make a *shallow* copy of the list of elements
            OrdinalPosition = null;
        }

        public ElementDefinitionNavigator(ElementDefinitionNavigator other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            Elements = other.Elements.ToList();
            OrdinalPosition = other.OrdinalPosition;
            StructureDefinition = other.StructureDefinition;
        }

        public ElementDefinitionNavigator ShallowCopy()
        {
            var result = new ElementDefinitionNavigator
            {
                Elements = this.Elements,
                OrdinalPosition = this.OrdinalPosition,
                StructureDefinition = this.StructureDefinition
            };

            return result;
        }


        public ElementDefinitionNavigator(IList<ElementDefinition> elements) : this(elements, null)
        {
        }

        public static ElementDefinitionNavigator ForSnapshot(StructureDefinition sd)
        {
            if (sd == null) throw Error.ArgumentNull(nameof(sd));
            if (sd.Snapshot == null) throw Error.Argument(nameof(sd), "The specified StructureDefinition has no snapshot component.");

            return new ElementDefinitionNavigator(sd.Snapshot.Element, sd);
        }

        public static ElementDefinitionNavigator ForDifferential(StructureDefinition sd)
        {
            if (sd == null) throw Error.ArgumentNull(nameof(sd));
            if (sd.Differential == null) throw Error.ArgumentNull(nameof(sd), "The specified StructureDefinition has no differential component.");

            return new ElementDefinitionNavigator(sd.Differential.Element, sd);
        }

        public StructureDefinition StructureDefinition { get; private set; }


        public bool AtRoot { get { return OrdinalPosition == null; } }

        /// <summary>
        /// Get the name of the current node, based on the last part of the part
        /// </summary>
        /// <returns>The name or String.Empty if the navigator is not located on a node</returns>
        public string PathName
        {
            get { return Current != null ? Current.GetNameFromPath() : String.Empty; }
        }

        /// <summary>
        /// Get the parent path of the current node
        /// </summary>
        /// <returns>The name or String.Empty if the navigator is not located on a node</returns>
        public string ParentPath
        {
            get { return Current != null ? Current.GetParentNameFromPath() : String.Empty; }
        }


        /// <summary>
        /// Get the full path of the current node
        /// </summary>
        /// <returns>The path or String.Empty if the navigator is not located on a node</returns>
        public string Path
        {
            get { return Current != null ? Current.Path : String.Empty; }
        }


        internal int? OrdinalPosition { get; private set; }

        public IList<ElementDefinition> Elements { get; private set; }

        public ElementDefinition Current
        {
            get { return OrdinalPosition != null ? Elements[OrdinalPosition.Value] : null; }
        }

        public int Count
        {
            get { return Elements.Count; }
        }




        //IElementNavigator INavigator<IElementNavigator>.Clone()
        //{
        //    return this.ShallowCopy();
        //}

        //string INamedNode.Name
        //{
        //    get
        //    {
        //        return this.PathName;
        //    }
        //}

        //object IValueProvider.Value
        //{
        //    get
        //    {
        //        return Current;
        //    }
        //}


        //string ITypeNameProvider.TypeName
        //{
        //    get { return "ElementDefinition";  }
        //}


        //----------------------------------
        //
        // Basic relative movement methods
        // 
        //----------------------------------

        public bool MoveToNext()
        {
            if (OrdinalPosition == null) return false;

            var searchPos = positionAfter();

            // Check whether found an element that is the next non-child element AND has the same parent
            // (which is then our sibling)
            if (searchPos < Count)
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

        private int positionAfter()
        {
            var searchPos = OrdinalPosition.Value + 1;

            // Skip children of the element
            while (searchPos < Count && isDeeperPath(Path, Elements[searchPos].Path))
                searchPos++;
            return searchPos;
        }

        public bool MoveToPrevious()
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


        public bool HasChildren
        {
            get
            {
                // [WMR 20160903] Handle empty list
                if (Count == 0) { return false; }

                // Special case, at document root
                if (OrdinalPosition == null) // && Count > 0)
                    return true;

                var childPos = OrdinalPosition.Value + 1;

                return childPos < Count && IsDirectChildPath(Path, Elements[childPos].Path);
            }
        }

        public bool MoveToFirstChild()
        {
            if (!HasChildren) return false;

            OrdinalPosition = OrdinalPosition != null ? OrdinalPosition.Value + 1 : 0;

            return true;
        }


        public bool MoveToParent()
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


        public void Reset()
        {
            OrdinalPosition = null;
        }


        public bool JumpToNameReference(string nameReference)
        {
            if (Count == 0) return false;

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

        public Bookmark Bookmark() => new Bookmark(Current, OrdinalPosition);

        public bool IsAtBookmark(Bookmark bookmark)
        {
            var elem = bookmark.data as ElementDefinition;
            if (elem == null)
            {
                return OrdinalPosition == null;
            }
            return object.ReferenceEquals(Current, elem);
        }

        public bool ReturnToBookmark(Bookmark bookmark)
        {
            if (bookmark.data == null)
            {
                OrdinalPosition = null;
                return true;
            }

            var elem = bookmark.data as ElementDefinition;
            if (elem == null) { return false; }

            // If the bookmark has an index, then try to use it
            var index = bookmark.index.GetValueOrDefault(-1);
            if (index > -1 && index < Count)
            {
                // Verify: element at index is a match?
                if (object.ReferenceEquals(Elements[index], bookmark.data))
                {
                    OrdinalPosition = index;
                    return true;
                }
                // No match; e.g. element list was modified after creating bookmark
            }

            // Otherwise scan the element list for the bookmarked element
            index = Elements.IndexOf(elem);
            if (index != -1)
            {
                OrdinalPosition = index;
                return true;
            }

            return false;
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
        public bool InsertBefore(ElementDefinition sibling)
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
        public bool InsertAfter(ElementDefinition sibling)
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
        public bool InsertFirstChild(ElementDefinition child)
        {
            if (Count == 0)
            {
                // Special case, insert a new root
                Elements.Add(child);
                child.Path = child.GetNameFromPath();
                OrdinalPosition = 0;
                return true;
            }
            else if (HasChildren)
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

            var source = Elements.Skip(start).Take(end - start).ToList();

            foreach (var elem in source.Reverse<ElementDefinition>())
                Elements.Insert(dest, (ElementDefinition)elem.DeepCopy());

            OrdinalPosition = dest;
            return true;
        }

        public bool Delete()
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

        // public override string ToString()
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string DebuggerDisplay
        {
            get
            {
                var elem = Current;
                if (elem == null) { return "(not positioned)"; }

                var output = new StringBuilder();
                output.Append(elem.Path);
                if (elem.Name != null) { output.Append(" : '" + elem.Name + "'"); }
                if (elem.Slicing != null) { output.AppendFormat(" (slicing entry: {0})", string.Join(" | ", elem.Slicing.Discriminator)); }
                return output.ToString();
            }
        }
    }
}
