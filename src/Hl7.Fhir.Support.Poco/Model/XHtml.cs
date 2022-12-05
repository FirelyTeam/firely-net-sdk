/*
  Copyright (c) 2011-2012, HL7, Inc
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/

using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Xml;

#nullable enable

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Helper functions to work with FHIR XHtml in narrative.
    /// </summary>
    public partial class XHtml
    {
        /// <summary>
        /// Checks whether the given literal is correctly formatted.
        /// </summary>
        [Obsolete("Use the more explicit IsValidNarrativeXhtml function instead (or IsValidXml if that is more appropriate).")]
        public static bool IsValidValue(string value) => IsValidNarrativeXhtml(value);

#if NETSTANDARD1_6
        /// <summary>
        /// Verifies the given string of XML against the FHIR narrative requirements from https://www.hl7.org/fhir/narrative.html. Note
        /// that due to unavailability of XML validation under netstandard1.6, this function always returns true.
        /// </summary>
        public static bool IsValidNarrativeXhtml(string _, out string[] errors)
        {
            errors = new string[0];
            return true;
        }

        /// <inheritdoc cref="IsValidNarrativeXhtml(string, out string[])"/>
        public static bool IsValidNarrativeXhtml(string value) => IsValidNarrativeXhtml(value, out _);
#else

        /// <summary>
        /// Verifies the given string of XML against the FHIR narrative requirements from https://www.hl7.org/fhir/narrative.html. 
        /// </summary>
        public static bool IsValidNarrativeXhtml(string value, out string[] errors)
        {
            errors = SerializationUtil.RunFhirXhtmlSchemaValidation(value);
            return !errors.Any();
        }

        /// <inheritdoc cref="IsValidNarrativeXhtml(string, out string[])"/>
        public static bool IsValidNarrativeXhtml(string value) => IsValidNarrativeXhtml(value, out _);
#endif

        /// <summary>
        /// Validates whether the given string of Xml is well-formatted.
        /// </summary>
        public static bool IsValidXml(string value, out string? error)
        {
            try
            {
                using var reader = SerializationUtil.XmlReaderFromXmlText(value);
                while (reader.Read()) ;
                error = default;
                return true;
            }
            catch (XmlException xmlE)
            {
                error = xmlE.Message;
                return false;
            }
        }
    }
}

#nullable restore