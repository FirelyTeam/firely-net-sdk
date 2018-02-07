/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using System.Text;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Navigation
{
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx

    /// <summary>Represents a bookmarked position of an <see cref="ElementDefinitionNavigator"/> instance.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public struct Bookmark : IEquatable<Bookmark>
    {
        /// <summary>Singleton. Represents an unpositioned navigator.</summary>
        public static readonly Bookmark Empty = new Bookmark();

        internal Bookmark(ElementDefinition data) : this(data, null) { }
        internal Bookmark(ElementDefinition data, int? index)
        {
            this.data = data;
            this.index = index;
        }

        internal readonly object data;

        // [WMR 20161219] Optimization
        internal readonly int? index;

        /// <summary>Indicates if the bookmark is empty, i.e. represents an unpositioned navigator.</summary>
        public bool IsEmpty => data == null;
        
        // Equality

        public override bool Equals(object obj) => obj is Bookmark && Equals((Bookmark)obj);

        public override int GetHashCode()
        {
            var d = data != null ? data.GetHashCode() : 0;
            var i = index.GetValueOrDefault(0);
            return (d * 17) ^ i;
        }

        // IEquatable

        public bool Equals(Bookmark other) => object.ReferenceEquals(data, other.data) && index.Equals(other.index);

        // Equality operators

        public static bool operator ==(Bookmark x, Bookmark y) => x.Equals(y);

        public static bool operator !=(Bookmark x, Bookmark y) => !x.Equals(y);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay
        {
            get
            {
                var elemDef = data as ElementDefinition;
                if (elemDef == null) { return "(empty)"; }

                StringBuilder sb = new StringBuilder(128);
                sb.Append(elemDef.Path);
                if (elemDef.Name != null)
                {
                    sb.Append(":");
                    sb.Append(elemDef.Name);
                }
                return sb.ToString();
            }
        }


    }
}
