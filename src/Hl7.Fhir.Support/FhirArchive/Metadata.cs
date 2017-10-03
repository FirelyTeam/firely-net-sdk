using Newtonsoft.Json.Linq;
using System.Collections.Generic;

#if NET_COMPRESSION
namespace Hl7.Fhir.Support.FhirArchive
{
    public struct SummaryItem
    {
        public string Name;
        public string Value;
        public bool Indexed;
    }


    internal struct EntrySummary
    {
        public string EntryName;
        public List<SummaryItem> Summary;
    }

    internal static class Serialization
    {
        internal static string ToJson(this EntrySummary entry)
        {
            var obj = new JObject();
            foreach(var item in entry.Summary)
            {
                obj.Add(new JProperty(item.Name, item.Value));
            }

            return obj.ToString();
        }

        internal static string ToJson(this Index index)
        {
            var obj = new JObject();
            JProperty lastProp = null;
            foreach(var entry in index)
            {
                if (lastProp?.Name == entry.Value)
                    ((JArray)(lastProp.Value)).Add(entry.EntryName);
                else
                {
                    lastProp = new JProperty(entry.Value, new JArray(entry.EntryName));
                    obj.Add(lastProp);
                }
            }

            return obj.ToString();
        }
    }

}
#endif