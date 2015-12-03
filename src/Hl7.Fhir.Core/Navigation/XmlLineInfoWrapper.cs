using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.Navigation
{
    internal class XmlLineInfoWrapper : IPositionInfo
    {
        private IXmlLineInfo _wrapped;

        public XmlLineInfoWrapper(IXmlLineInfo lineInfo)
        {
            if (!lineInfo.HasLineInfo())
                throw Error.InvalidOperation("No lineinfo available. Please read the Xml document using LoadOptions.SetLineInfo.");

            _wrapped = lineInfo;
        }

        public int LineNumber
        {
            get
            {
                return _wrapped.LineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                return _wrapped.LinePosition;
            }
        }
    }

}
