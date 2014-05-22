/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public static class BundleExtensions
    {
        public static Bundle RefreshBundle(this FhirClient client, Bundle bundle)
        {
            if (bundle == null) throw Error.ArgumentNull("bundle");

            // Clone old bundle, without the entries (so, just the header)
            var oldEntries = bundle.Entries;
            Bundle result;

            try
            {
                bundle.Entries = new List<BundleEntry>();
                var xml = FhirSerializer.SerializeBundleToXml(bundle, summary:false);
                result = FhirParser.ParseBundleFromXml(xml);
            }
            catch
            {
                throw;
            }
            finally
            {
                bundle.Entries = oldEntries;
            }

            result.Id = new Uri("urn:uuid:" + Guid.NewGuid().ToString());
            result.LastUpdated = DateTimeOffset.Now;
            result.Entries = new List<BundleEntry>();
            foreach (var entry in bundle.Entries)
            {
                if (entry is ResourceEntry)
                {
                    var newEntry = client.Read(entry.Id);
                    if (entry.Links.Alternate != null) newEntry.Links.Alternate = entry.Links.Alternate;
                    result.Entries.Add(newEntry);
                }
                else if (entry is DeletedEntry)
                    result.Entries.Add(entry);
                else
                    throw Error.NotSupported("Cannot refresh an entry of type {0}", messageArgs: entry.GetType().Name);
            }

            return result;
        }
    }
}
