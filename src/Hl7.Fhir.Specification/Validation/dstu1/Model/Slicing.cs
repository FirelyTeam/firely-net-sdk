using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Model
{
    // This class is only for keeping track of slicings while reading a profile into structure.
    internal class Slicing
    {
        internal int Count = 0;
        internal Path Path;
        internal Path Discriminator { get; set; }
    }
}
