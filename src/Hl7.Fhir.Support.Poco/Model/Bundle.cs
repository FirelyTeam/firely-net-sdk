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
using System.Diagnostics;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Model
{
    public partial class Bundle
    {
        [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        public partial class EntryComponent
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string DebuggerDisplay =>
                    Request?.Method.HasValue == true
                        ? string.Format("{0}: {1}", Request.Method.Value, FullUrl)
                        : string.Format("FullUrl = \"{0}\"", FullUrl);
        }

        public const string ATOM_LINKREL_SELF = "self";
        public const string ATOM_LINKREL_PREVIOUS = "previous";
        public const string ATOM_LINKREL_PREV = "prev";
        public const string ATOM_LINKREL_NEXT = "next";
        public const string ATOM_LINKREL_FIRST = "first";
        public const string ATOM_LINKREL_LAST = "last";
        public const string ATOM_LINKREL_SEARCH = "search";
        public const string ATOM_LINKREL_PREDVERSION = "predecessor-version";
        public const string ATOM_LINKREL_ALTERNATE = "alternate";

        public Uri? SelfLink
        {
            get { return getLink(ATOM_LINKREL_SELF); }
            set { setLink(ATOM_LINKREL_SELF, value); }
        }

        public Uri? FirstLink
        {
            get { return getLink(ATOM_LINKREL_FIRST); }
            set { setLink(ATOM_LINKREL_FIRST, value); }
        }

        public Uri? PreviousLink
        {
            get { return getLink(ATOM_LINKREL_PREVIOUS) ?? getLink(ATOM_LINKREL_PREV); }
            set { setLink(ATOM_LINKREL_PREVIOUS, value); }
        }

        public Uri? NextLink
        {
            get { return getLink(ATOM_LINKREL_NEXT); }
            set { setLink(ATOM_LINKREL_NEXT, value); }
        }

        public Uri? LastLink
        {
            get { return getLink(ATOM_LINKREL_LAST); }
            set { setLink(ATOM_LINKREL_LAST, value); }
        }

        public Uri? SearchLink
        {
            get { return getLink(ATOM_LINKREL_SEARCH); }
            set { setLink(ATOM_LINKREL_SEARCH, value); }
        }

        public Uri? PredecessorVersionLink
        {
            get { return getLink(ATOM_LINKREL_PREDVERSION); }
            set { setLink(ATOM_LINKREL_PREDVERSION, value); }
        }

        public Uri? Alternate
        {
            get { return getLink(ATOM_LINKREL_ALTERNATE); }
            set { setLink(ATOM_LINKREL_ALTERNATE, value); }
        }

        private Uri? getLink(string rel)
        {
            if (Link is null) return null;

            var entry = Link.FirstOrDefault(e => rel.Equals(e.Relation, StringComparison.OrdinalIgnoreCase));

            return entry != null ? new Uri(entry.Url, UriKind.RelativeOrAbsolute) : null;
        }

        private void setLink(string rel, Uri? uri)
        {
            if (uri is null) throw new ArgumentNullException(nameof(uri));

            if (Link == null) Link = new List<LinkComponent>();

            var entry = Link.FirstOrDefault(e => rel.Equals(e.Relation, StringComparison.OrdinalIgnoreCase));

            // Setting the link to null removes the entry
            if (uri is null)
            {
                if (entry is not null)
                    Link.Remove(entry);

                return;
            }

            var uriString = uri.IsAbsoluteUri ? uri.AbsoluteUri : uri.OriginalString;
            if (entry != null)
                entry.Url = uriString;
            else
                Link.Add(new LinkComponent() { Relation = rel, Url = uriString });
        }



    }
}

#nullable restore