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

        internal string ParentPath
        {
            get
            {
                var dot = Path.LastIndexOf(".");                
                return dot != -1 ? Path.Substring(0, dot) : String.Empty;
            }
        }

        public bool MoveToNext()
        {
            var searchPos = OrdinalPosition + 1;
            
            // Skip children of the element
            while (searchPos < Structure.Element.Count && 
                Structure.Element[searchPos].Path.StartsWith(Path + "."))
                    searchPos++;

            // If we found an element that is the next non-child element
            if (searchPos < Structure.Element.Count)
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

        private bool isDirectChildPath(string parent, string child)
        {
            return child.StartsWith(parent + ".") &&
                    child.IndexOf('.', parent.Length + 1) == -1;         
        }


        public bool MoveToFirstChild()
        {
            var childPos = OrdinalPosition + 1;

            if (childPos < Structure.Element.Count && isDirectChildPath(Path, Structure.Element[childPos].Path))
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

        public void MoveToRoot()
        {
            OrdinalPosition = 0;
        }

        public bool MoveTo(ElementNavigator other)
        {
            if (other.OrdinalPosition < Structure.Element.Count)
            {
                this.OrdinalPosition = other.OrdinalPosition;
                return true;
            }

            return false;
        }
        
        public bool JumpTo(string path)
        {
            for (var ix = 0; ix < Structure.Element.Count; ix++ )
            {
                if (Structure.Element[ix].Path == path)
                {
                    OrdinalPosition = ix;
                    return true;
                }
            }

            return false;
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

    }
}
