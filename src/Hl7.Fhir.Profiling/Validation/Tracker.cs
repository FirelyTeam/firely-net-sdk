using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhir.Profiling
{
    public enum Resolution { Unknown, Unresolvable, Resolved }

    public class Tracker
    {
        Dictionary<Uri, Resolution> dictionary = new Dictionary<Uri, Resolution>();

        public void Add(Uri uri, Resolution resolution)
        {
            if (Knows(uri))
            {
                dictionary[uri] = resolution;
            }
            else
            {
                dictionary.Add(uri, resolution);
            }
        }

        public void Add(string uri, Resolution resolution)
        {
            Add(new Uri(uri), resolution);
        }

        public void Resolved(Uri uri)
        {
            Add(uri, Resolution.Resolved);
        }

        public bool Knows(Uri uri)
        {
            Resolution resolution = Resolution.Unknown;
            dictionary.TryGetValue(uri, out resolution);
            return resolution != Resolution.Unknown;
        }

        public bool Knows(string uri)
        {
            return Knows(new Uri(uri));
        }
    }

}
