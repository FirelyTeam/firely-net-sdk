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
    public abstract class BaseElementNavigator
    {
        /// <summary>
        /// Get the name of the current node, based on the last part of the part
        /// </summary>
        /// <returns>The name or String.Empty if the navigator is not located on a node</returns>
        public string PathName
        {
            get{ return Current != null ? Current.GetNameFromPath() : String.Empty; }
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
            get{ return Current != null ? Current.Path : String.Empty; }
        }


        public abstract ElementDefinition Current { get; }

        public abstract int Count { get; }

//----------------------------------
//
// Basic relative movement methods
// 
//----------------------------------

        public abstract bool MoveToNext();
     
        public abstract bool MoveToPrevious();

        public abstract bool MoveToFirstChild();

        public abstract bool MoveToParent();

        public abstract void Reset();

        public abstract bool HasChildren { get; }


//----------------------------------
//
// Bookmark operations
//
//----------------------------------

        public abstract Bookmark Bookmark();

        public abstract bool IsAtBookmark(Bookmark bookmark);

        public abstract bool ReturnToBookmark(Bookmark bookmark);
   

//----------------------------------
//
// Methods that alter the list of elements
// 
//----------------------------------

        public abstract bool InsertBefore(ElementDefinition sibling);

        public abstract bool InsertAfter(ElementDefinition sibling);

        public abstract bool InsertFirstChild(ElementDefinition child);
    
        public abstract bool Delete();       
    }


    public struct Bookmark
    {
        public object data;
    }
}
