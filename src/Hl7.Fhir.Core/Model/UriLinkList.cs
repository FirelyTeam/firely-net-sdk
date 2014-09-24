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
using Hl7.Fhir.Model;
using System.IO;


namespace Hl7.Fhir.Model
{
    public class UriLinkList : List<UriLinkEntry>
    {
        public const string ATOM_LINKREL_SELF = "self";
        public const string ATOM_LINKREL_PREVIOUS = "previous";
        public const string ATOM_LINKREL_NEXT = "next";
        public const string ATOM_LINKREL_FIRST = "first";
        public const string ATOM_LINKREL_LAST = "last";
        public const string ATOM_LINKREL_SEARCH = "search";
        public const string ATOM_LINKREL_PREDVERSION = "predecessor-version";
        public const string ATOM_LINKREL_FHIRBASE = "fhir-base";
        public const string ATOM_LINKREL_ALTERNATE = "alternate";


        public UriLinkList() : base() { }

        public UriLinkList(IEnumerable<UriLinkEntry> collection) : base(collection) {} 

        public Uri SelfLink
        {
            get { return getEntry(ATOM_LINKREL_SELF); }
            set { setEntry(ATOM_LINKREL_SELF, value); }
        }

        public Uri FirstLink
        {
            get { return getEntry(ATOM_LINKREL_FIRST); }
            set { setEntry(ATOM_LINKREL_FIRST, value); }
        }

        public Uri PreviousLink
        {
            get { return getEntry(ATOM_LINKREL_PREVIOUS); }
            set { setEntry(ATOM_LINKREL_PREVIOUS, value); }
        }

        public Uri NextLink
        {
            get{ return getEntry(ATOM_LINKREL_NEXT); }
            set{ setEntry(ATOM_LINKREL_NEXT, value); }
        }

        public Uri LastLink
        {
            get { return getEntry(ATOM_LINKREL_LAST); }
            set { setEntry(ATOM_LINKREL_LAST, value); }
        }

        public Uri SearchLink
        {
            get { return getEntry(ATOM_LINKREL_SEARCH); }
            set { setEntry(ATOM_LINKREL_SEARCH, value); }
        }

        public Uri PredecessorVersionLink
        {
            get { return getEntry(ATOM_LINKREL_PREDVERSION); }
            set { setEntry(ATOM_LINKREL_PREDVERSION, value); }
        }

        public Uri Base
        {
            get { return getEntry(ATOM_LINKREL_FHIRBASE); }
            set { setEntry(ATOM_LINKREL_FHIRBASE, value); }
        }

        public Uri Alternate
        {
            get { return getEntry(ATOM_LINKREL_ALTERNATE); }
            set { setEntry(ATOM_LINKREL_ALTERNATE, value); }
        }


        private Uri getEntry(string rel)
        {
            UriLinkEntry entry = this.FirstOrDefault(e => rel.Equals(e.Rel, StringComparison.OrdinalIgnoreCase));

            if (entry != null)
                return entry.Uri;
            else
                return null;
        }

        private void setEntry(string rel, Uri uri)
        {
            UriLinkEntry entry = this.FirstOrDefault(e => rel.Equals(e.Rel, StringComparison.OrdinalIgnoreCase));

            if (entry != null)
                entry.Uri = uri;
            else
                this.Add( new UriLinkEntry { Rel = rel, Uri = uri } );
        }
    }


    public class UriLinkEntry
    {
        public string Rel { get; set; }
        public Uri Uri { get; set; }
    }
}

