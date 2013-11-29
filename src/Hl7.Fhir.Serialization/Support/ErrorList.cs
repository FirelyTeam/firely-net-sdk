/*
  Copyright (c) 2011-2013, HL7, Inc.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Hl7.Fhir.Model;


namespace Hl7.Fhir.Support
{
    public class ErrorList : List<ErrorList.Error>
    {
        public static readonly ErrorList EMPTY = new ErrorList();

        public class Error
        {
            public int? Line { get; set; }
            public int? Pos { get; set; }
            public string Message { get; set; }
            public string Context { get; set; }

            public override string ToString()
            {
                StringBuilder result = new StringBuilder();

                result.Append( Message );
                result.Append( " (");
                bool hasPos = false;

                if( Line != null )
                {
                    result.Append("line " + Line.ToString());
                    if (Pos != null) result.Append(", pos " + Pos.ToString());
                    hasPos = true;
                }

               
                if (Context != null)
                {
                    if (hasPos) result.Append(": ");
                    result.Append(Context);
                }

                result.Append( ")" );
                return result.ToString();
            }
        }

        public string DefaultContext { get; set; }

        //internal void Add(string message, string context, IFhirReader reader)
        //{         
        //    this.Add(message, context, reader.LineNumber, reader.LinePosition);
        //}

        public void Add(string message, string context, int? line, int? pos)
        {
            this.Add( new Error { Message = message, Context = context,
                                Line = line, Pos = pos } );
        }

        public void Add(string message, string context)
        {
            this.Add(message,context,null,null);
        }

        public void Add(string message, int line, int pos)
        {
            this.Add(message, DefaultContext, null, null);
        }

        //internal void Add(string message, IFhirReader reader)
        //{
        //    this.Add(message, DefaultContext, reader);
        //}

        public void Add(string message)
        {
            this.Add(message, DefaultContext);
        }

        public override string ToString()
        {
            if(this.Count() == 0)
                return "No errors.";

            int errNr = 0;

            return String.Join(System.Environment.NewLine, 
                this.Select(e => String.Format("({0}) {1}", ++errNr, e.ToString())));
        }
    }
}
