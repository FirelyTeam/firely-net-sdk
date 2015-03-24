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
            var oldEntries = bundle.Entry;
            Bundle result;

            try
            {
                bundle.Entry = new List<Bundle.BundleEntryComponent>();
                var xml = FhirSerializer.SerializeResourceToXml(bundle, summary:false);
                result = (Bundle)FhirParser.ParseResourceFromXml(xml);
            }
            catch
            {
                throw;
            }
            finally
            {
                bundle.Entry = oldEntries;
            }

            result.Id = "urn:uuid:" + Guid.NewGuid().ToString("n");
            result.Meta = new Meta();
            result.Meta.LastUpdated = DateTimeOffset.Now;
            result.Entry = new List<Bundle.BundleEntryComponent>();

            foreach (var entry in bundle.Entry)
            {
                if (entry.Resource != null)
                {
                    if (!entry.IsDeleted())
                    {
                        Resource newEntry = client.Read<Resource>(entry.GetResourceLocation());
                        result.Entry.Add(new Bundle.BundleEntryComponent() { Resource = newEntry, Base = bundle.Base, ElementId = entry.ElementId });
                    }
                }
                else
                    throw Error.NotSupported("Cannot refresh an entry of type {0}", messageArgs: entry.GetType().Name);
            }

            return result;
        }
    }
}
