#nullable enable

/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Specification.Terminology
{
    /// <summary>
    /// Exceptions throw by the utility methods used for the implementation of our terminology services,
    /// e.g. the <see cref="ValueSetExpander"/> and the <see cref="CodeSystemFilterProcessor"/>.
    /// </summary>
    /// <remarks>These exceptions are used within the <see cref="LocalTerminologyService"/> and are not
    /// supposed to be thrown by implementations of <see cref="ITerminologyService"/>.</remarks>
    public class TerminologyServiceException : Exception
    {
        public TerminologyServiceException(string message) : base(message)
        {
        }
    }

    public class ValueSetExpansionTooBigException : TerminologyServiceException
    {
        //422 - too costly
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

    public class CodeSystemUnknownException : TerminologyServiceException
    {
        public CodeSystemUnknownException(string message) : base(message)
        {
        }
    }

    public class CodeSystemIncompleteException : TerminologyServiceException
    {
        public CodeSystemIncompleteException(string message) : base(message)
        {
        }
    }

}

#nullable restore