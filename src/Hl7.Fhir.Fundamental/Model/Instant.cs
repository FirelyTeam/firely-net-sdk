/*
  Copyright (c) 2011+, HL7, Inc.
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

#nullable enable

using System;

namespace Hl7.Fhir.Model
{
    public partial class Instant
    {
        public static Instant FromLocalDateTime(int year, int month, int day,
                    int hour, int min, int sec, int millis = 0) =>
            new(new DateTimeOffset(year, month, day, hour, min, sec, millis, DateTimeOffset.Now.Offset));


        public static Instant FromDateTimeUtc(int year, int month, int day,
                                            int hour, int min, int sec, int millis = 0) =>
            new(new DateTimeOffset(year, month, day, hour, min, sec, millis,
                                   TimeSpan.Zero));

        /// <summary>
        /// Returns an Instant initialized with the current date and time.
        /// </summary>
        /// <returns></returns>
        public static Instant Now() => new(DateTimeOffset.Now);

        /// <summary>
        /// Checks whether the given literal is correctly formatted.
        /// </summary>
        public static bool IsValidValue(string value) => ElementModel.Types.DateTime.TryParse(value, out var dateTime) &&
            dateTime.Precision >= ElementModel.Types.DateTimePrecision.Second && dateTime.HasOffset;
    }
}

#nullable restore