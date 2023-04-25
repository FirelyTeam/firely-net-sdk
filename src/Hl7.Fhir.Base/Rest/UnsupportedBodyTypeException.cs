#nullable enable

/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Rest
{
    public class UnsupportedBodyTypeException : Exception
    {
        public string? BodyType { get; set; }

        public string? Body { get; set; }
        public UnsupportedBodyTypeException(string message, string? mimeType, string? body) : base(message)
        {
            BodyType = mimeType;
            Body = body;
        }
    }
}

#nullable enable