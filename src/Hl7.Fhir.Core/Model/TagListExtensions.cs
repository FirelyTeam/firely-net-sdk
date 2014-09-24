/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using System.IO;

using Hl7.Fhir.Introspection;
using System.ComponentModel.DataAnnotations;
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

        public static void SetTextTag(this BundleEntry entry, string text)
        {
            var result = new List<Tag>();

            if (entry.Tags != null) result.AddRange(entry.Tags);

            result.RemoveAll(t => Equals(t.Scheme,Tag.FHIRTAGSCHEME_GENERAL) &&
                    (t.Term != null && t.Term.StartsWith(TAG_TERM_TEXT)));

            result.Add(new Tag(TAG_TERM_TEXT + Uri.EscapeUriString(text), Tag.FHIRTAGSCHEME_GENERAL, text));

            entry.Tags = result;
        }

        public static string GetTextTag(this BundleEntry entry)
        {
            var textTag = entry.Tags.FilterByScheme(Tag.FHIRTAGSCHEME_GENERAL).
                Where(t => t.Term.StartsWith(TAG_TERM_TEXT)).SingleOrDefault();

            if (textTag == null) return null;

            return Uri.UnescapeDataString(textTag.Term.Substring(TAG_TERM_TEXT.Length));
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

            return tags.Where(t => !Equals(t,tag));
        }

    }
}

