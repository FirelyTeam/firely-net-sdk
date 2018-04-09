/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Specification.Terminology
{
    public class TerminologyServiceException : Exception
    {
        public TerminologyServiceException(string message) : base(message)
        {
        }
    }

    public class ValueSetExpansionTooBigException : TerminologyServiceException
    {
        public ValueSetExpansionTooBigException(string message) : base(message)
        {
        }
    }

    public class ValueSetExpansionTooComplexException : TerminologyServiceException
    {
        public ValueSetExpansionTooComplexException(string message) : base(message)
        {
        }
    }

    public class ValueSetUnknownException : TerminologyServiceException
    {
        public ValueSetUnknownException(string message) : base(message)
        {
        }
    }
}
