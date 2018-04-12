/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
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
                // sb.AppendFormat("Path='{0}'", Path);
                // if (Name != null) { sb.AppendFormat(" Name='{0}'", Name); }
                sb.Append(Path);
                if (SliceName != null)
                {
                    sb.Append(":");
                    sb.Append(SliceName);
                }
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

        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
        public partial class DiscriminatorComponent
        {
            public static DiscriminatorComponent ForTypeSlice()
            {
                // [WMR 20170823] Also initialize Discriminator.Path = "$this", as defined here:
                // https://www.hl7.org/fhir/profiling.html#discriminator
                return new DiscriminatorComponent { Type = DiscriminatorType.Type, Path = "$this" };
            }

            public static DiscriminatorComponent ForValueSlice(string path)
            {
                return new DiscriminatorComponent { Type = DiscriminatorType.Value, Path = path };
            }

            public List<DiscriminatorComponent> ToList()
            {
                return new List<DiscriminatorComponent> { this };
            }

            // [WMR 20170414] Added
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [NotMapped]
            string DebuggerDisplay
            {
                get
                {
                    StringBuilder sb = new StringBuilder(128);
                    sb.Append(TypeName);
                    sb.Append(" | ");
                    sb.Append(Type.HasValue ? Type.Value.GetLiteral() : "undefined");
                    if (Path != null)
                    {
                        sb.AppendFormat(" : '{0}'", Path);
                    }
                    return sb.ToString();
                }
            }
        }

        // [WMR 20170414] Added DebuggerDisplay

        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
        public partial class TypeRefComponent
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [NotMapped]
            string DebuggerDisplay
            {
                get
                {
                    StringBuilder sb = new StringBuilder(128);
                    sb.Append(TypeName);
                    sb.Append(" | ");
                    sb.Append(Code ?? "undefined");
                    if (Profile != null)
                    {
                        sb.AppendFormat(" : '{0}'", Profile);
                    }
                    return sb.ToString();
                }
            }
        }


    }
}
