/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Introspection;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Hl7.Fhir.Model
{
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public partial class ElementDefinition
    {
        public ElementDefinition()
        {

        }

        public ElementDefinition(string path)
        {
            Path = path;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        string DebuggerDisplay
        {
            get {
                StringBuilder sb = new StringBuilder(128);
                sb.AppendFormat("Path='{0}'", Path);
                if (SliceName != null) { sb.AppendFormat(" SliceName='{0}'", SliceName); }
                return sb.ToString();
            }
        }

        [NotMapped]
        [IgnoreDataMemberAttribute]
        [Obsolete("Property was renamed to SliceName", true)]
        public string Name
        {
            get { return SliceName; }
            set
            {
                SliceName = value;
            }
        }
    }
}
