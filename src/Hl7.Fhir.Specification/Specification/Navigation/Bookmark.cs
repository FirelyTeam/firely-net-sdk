/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Text;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Navigation
{
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public struct Bookmark
    {
        // [WMR 20160720] Changed to internal, for encapsulation
        internal object data;

        // [WMR 20160720] NEW
        public bool IsEmpty
        {
            get { return data == null; }
        }

        // Singleton
        public static readonly Bookmark Empty = new Bookmark();

        // [WMR 20160802] NEW
        internal static Bookmark FromElement(ElementDefinition element)
        {
            if (element == null) throw Error.ArgumentNull("element");
            return new Bookmark() { data = element };
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay
        {
            get {
                var elemDef = data as ElementDefinition;
                if (elemDef == null) { return "(empty)"; }

                StringBuilder sb = new StringBuilder(128);
                sb.Append(elemDef.Path);
                if (elemDef.Name != null)
                {
                    sb.AppendFormat(" ('{0}')", elemDef.Name);
                }

                //sb.AppendFormat("Path='{0}'", elemDef.Path);
                //if (elemDef.Name != null) { sb.AppendFormat(" Name='{0}'", elemDef.Name); }
                return sb.ToString();
            }
        }
    }
}
