using Hl7.Fhir.Introspection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public partial class Coding
    {
        public Coding()
        {
        }

        public Coding(string system, string code)
        {
            this.System = system;
            this.Code = code;
        }

        public Coding(string system, string code, string display)
        {
            this.System = system;
            this.Code = code;
            this.Display = display;
        }
    }
    
    [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay(null),nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public partial class CodeableConcept
    {
        public string DebuggerDisplay(string prefix)
        {
            if (!String.IsNullOrEmpty(Text))
                return String.Format("{0}Text=\"{1}\"", prefix, Text);
            StringBuilder sb = new StringBuilder();
            if (_Coding != null)
            {
                foreach (var item in _Coding)
                {
                    sb.Append("  ");
                    sb.Append(item.DebuggerDisplay);
                }
            }
            return sb.ToString();
        }

        public CodeableConcept()
        {
        }

        public CodeableConcept(string system, string code, string text = null)
        {
            if (!string.IsNullOrEmpty(system) || !string.IsNullOrEmpty(code))
            {
                this.Coding = new List<Coding>() {
                    new Coding(system,code) };
            }
            this.Text = text;
        }
        public CodeableConcept(string system, string code, string display, string text)
        {
            if (!string.IsNullOrEmpty(system) || !string.IsNullOrEmpty(code) || !string.IsNullOrEmpty(display))
            {
                this.Coding = new List<Coding>() {
                    new Coding(system,code, display) };
            }
            this.Text = text;
        }
    }
    
    public partial class Identifier
    {
        public Identifier()
        {
        }

        public Identifier(string system, string value)
        {
            this.System = system;
            this.Value = value;
        }
    }

    [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public partial class Period
    {
        public Period()
        {
        }

        public Period(FhirDateTime start, FhirDateTime end)
        {
            StartElement = start;
            EndElement = end;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        internal string DebuggerDisplay
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(this.Start))
                    sb.AppendFormat(" Start=\"{0}\"", Start);
                if (!string.IsNullOrEmpty(this.End))
                    sb.AppendFormat(" End=\"{0}\"", End);

                return sb.ToString();
            }
        }
    }
}
