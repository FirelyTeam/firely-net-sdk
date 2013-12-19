/*
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

using Hl7.Fhir.Introspection;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Model
{
    public enum BundleType
    {
        Unspecified,
        Document,
        Message
    }

    public static class TagListExtensions
    {
        private const string TAG_TERM_TEXT = "http://hl7.org/fhir/tag/text/";
        private const string TAG_TERM_DOCUMENT = "http://hl7.org/fhir/tag/document";
        private const string TAG_TERM_MESSAGE = "http://hl7.org/fhir/tag/message";

        public static void SetTextTag(this BundleEntry entry, string text, string mimeType = "text/plain")
        {
            var result = new List<Tag>();

            if (entry.Tags != null) result.AddRange(entry.Tags);

            result.RemoveAll(t => Equals(t.Scheme,Tag.FHIRTAGSCHEME_GENERAL) &&
                    (t.Term != null && t.Term.StartsWith(TAG_TERM_TEXT)));

            result.Add(new Tag(TAG_TERM_TEXT + mimeType, Tag.FHIRTAGSCHEME_GENERAL, text));

            entry.Tags = result;
        }

        public static string GetTextTag(this BundleEntry entry, out string mimeType)
        {
            mimeType = null;

            var textTag = entry.Tags.FilterByScheme(Tag.FHIRTAGSCHEME_GENERAL).
                Where(t => t.Term.StartsWith(TAG_TERM_TEXT)).SingleOrDefault();

            if (textTag == null) return null;

            mimeType = textTag.Term.Substring(TAG_TERM_TEXT.Length);
            return textTag.Label;
        }

        private static readonly Tag MESSAGE_TAG = new Tag(TAG_TERM_MESSAGE, Tag.FHIRTAGSCHEME_GENERAL);
        private static readonly Tag DOCUMENT_TAG = new Tag(TAG_TERM_DOCUMENT, Tag.FHIRTAGSCHEME_GENERAL);

        public static void SetBundleType(this Bundle bundle, BundleType type)
        {
            List<Tag> result = new List<Tag>(bundle.Tags.Exclude(new Tag[] { MESSAGE_TAG, DOCUMENT_TAG }));

            if (type == BundleType.Document)
                result.Add(DOCUMENT_TAG);
            else if (type == BundleType.Message)
                result.Add(MESSAGE_TAG);

            bundle.Tags = result;
        }

        public static BundleType GetBundleType(this Bundle bundle)
        {
            if (bundle.Tags.Contains(DOCUMENT_TAG))
                return BundleType.Document;
            else if (bundle.Tags.Contains(MESSAGE_TAG))
                return BundleType.Message;
            else
                return BundleType.Unspecified;
        }

        public static void AddProfileAssertion(this ResourceEntry entry, Uri profileUri)
        {
            entry.Tags.Add(new Tag(profileUri.ToString(), Tag.FHIRTAGSCHEME_PROFILE));
        }

        public static IEnumerable<Uri> GetAssertedProfiles(this ResourceEntry entry)
        {
            return entry.Tags.Where(t => Equals(t.Scheme, Tag.FHIRTAGSCHEME_PROFILE) && t.Term != null)
                    .Select(t => new Uri(t.Term));
        }

        public static void RemoveProfileAssertion(this ResourceEntry entry, Uri profileUri)
        {
            var result = new List<Tag>(entry.Tags);
            result.RemoveAll(t => Equals(t, new Tag(profileUri.ToString(), Tag.FHIRTAGSCHEME_PROFILE)));
            entry.Tags = result;
        }

        public static void AddSecurityLabel(this ResourceEntry entry, Uri label)
        {
            entry.Tags.Add(new Tag(label.ToString(), Tag.FHIRTAGSCHEME_SECURITY));
        }

        public static IEnumerable<Uri> GetSecurityLabels(this ResourceEntry entry)
        {
            return entry.Tags.Where(t => Equals(t.Scheme, Tag.FHIRTAGSCHEME_SECURITY) && t.Term != null)
                    .Select(t => new Uri(t.Term));
        }

        public static void RemoveSecurityLabel(this ResourceEntry entry, Uri label)
        {
            var result = new List<Tag>(entry.Tags);
            result.RemoveAll(t => Equals(t, new Tag(label.ToString(), Tag.FHIRTAGSCHEME_SECURITY)));
            entry.Tags = result;
        }

        public static IEnumerable<Tag> FilterByScheme(this IEnumerable<Tag> tags, Uri scheme)
        {
            if (scheme == null) Error.ArgumentNull("scheme");

            return tags.Where(e => Uri.Equals(e.Scheme, scheme));
        }
        public static bool UriIsFhirScheme(Uri scheme)
        {
            return
                Uri.Equals(scheme, Tag.FHIRTAGSCHEME_GENERAL)
                || Uri.Equals(scheme, Tag.FHIRTAGSCHEME_PROFILE)
                || Uri.Equals(scheme, Tag.FHIRTAGSCHEME_SECURITY);
        }
        public static bool HasFhirScheme(this Tag tag)
        {
            return UriIsFhirScheme(tag.Scheme);
        }

        public static IEnumerable<Tag> FilterOnFhirSchemes(this IEnumerable<Tag> tags)
        {
            return tags.Where(e => e.HasFhirScheme());
        }

        public static IEnumerable<Tag> Exclude(this IEnumerable<Tag> tags, IEnumerable<Tag> that)
        {
            if (that == null) Error.ArgumentNull("that");

            return tags.Where(t => !that.Contains(t)); 
        }

        public static IEnumerable<Tag> Exclude(this IEnumerable<Tag> tags, Tag tag)
        {
            if (tag == null) Error.ArgumentNull("tag");

            return tags.Where(t => t != tag);
        }

    }
}

