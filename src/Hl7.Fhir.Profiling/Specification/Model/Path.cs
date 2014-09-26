/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Model
{
    public class Segment : IEquatable<Segment>
    {
        public string Name;
        public bool Multi;

        public bool Match(string name)
        {
            if (this.Multi)
            {
                return name.StartsWith(this.Name);
            }
            else
            {
                return name == Name;
            }
        }
        
        public override string ToString()
        {
            string s = Name;
            if (Multi) s += "[x]";
            return s;
        }

        public bool Equals(Segment other)
        {
            return this.Match(other.Name);
        }
    }

    public class Path : IEquatable<Path>
    {
        public List<Segment> Segments { get; private set; }

        public Segment Tail
        {
            get
            {
                return Segments.Last();
            }
        }

        private List<Segment> parsePath(string path)
        {
            var list = new List<Segment>();

            foreach (string s in path.Split('.'))
            {
                Segment segment = new Segment();
                string name = Regex.Replace(s, @"\[x\]", "");
                if (name != s) segment.Multi = true;
                segment.Name = name;
                list.Add(segment);
            }
            return list;
        }

        public Path(string path)
        {
            Segments = parsePath(path);
        }

        public Path(IEnumerable<Segment> segments)
        {
            this.Segments = segments.ToList();
        }

        public Path ForChild()
        {
            Path child = new Path(this.Segments.Skip(1));
            return child;
        }

        public Path Parent()
        {
            Path parent = new Path(this.Segments);
            parent.Segments.RemoveAt(parent.Segments.Count - 1);
            return parent;
        }

        public string ElementName
        {
            get
            {
                return Segments.Last().Name;
            }
        }

        public string ToXPath()
        {
            var parts = Segments.Select(s => "f:" + s.Name);
            return string.Join("/", parts);
        }

        public int Count
        {
            get
            {
                return Segments.Count;
            }
        }

        public override string ToString()
        {
            return string.Join(".", Segments);
        }

        public bool Equals(Path other)
        {
            bool equal = this.Segments.SequenceEqual(other.Segments);
            return equal;
        }
    }

}
