using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    public enum Resolution { Unknown, Unresolvable, Resolved }

    public class Tracker
    {
        Dictionary<string, Resolution> dictionary = new Dictionary<string, Resolution>();

        public void Add(string key, Resolution resolution)
        {
            if (Knows(key))
            {
                dictionary[key] = resolution;
            }
            else
            {
                dictionary.Add(key, resolution);
            }
        }


        public void Add(Uri uri, Resolution resolution)
        {
            Add(uri.ToString(), resolution);
        }

        public void MarkResolved(Uri uri)
        {
            Add(uri.ToString(), Resolution.Resolved);
        }

        public void Resolve(IEnumerable<Uri> uris)
        {
            foreach(Uri uri in uris)
            {
                MarkResolved(uri);
            }
        }

        public bool Knows(string key)
        {
            Resolution resolution = Resolution.Unknown;
            dictionary.TryGetValue(key, out resolution);
            return resolution != Resolution.Unknown;
        }
        public bool Knows(Uri uri)
        {
            return Knows(uri.ToString());
        }

    }

}
